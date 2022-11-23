using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
        public static string ToTest(this object @object)
        {
            return new ObjectAsConstructor(
                @object ?? throw new ArgumentNullException(nameof(@object))
            ).ToString();
        }

        internal static string ToStringForInitialization(this object? @object)
        {
            string valueStr;
            if (@object is null)
            {
                valueStr = "null";
            }
            else if (@object.IsPrimitive())
            {
                if (@object is string)
                {
                    return $"\"{@object}\"";
                }

                if (@object is Enum)
                {
                    return $"{@object.GetType().Name}.{@object}";
                }
                valueStr = @object.ToString();
            }
            else
            {
                valueStr = @object.ToTest();
            }

            return valueStr;
        }

        internal static IConstructor ValidConstructor(this object? @object, IArguments sharedArguments)
        {
            if (@object == null)
            {
                return new NullConstructor();
            }

            if (@object.IsDelegate())
            {
                return new DelegateConstructor(@object);
            }
            
            if (@object.IsPrimitive())
            {
                return new ValueTypeConstructor(@object);
            }

            if (@object.IsCollection())
            {
                return new CollectionConstructor(@object);
            }
            
            if (@object.IsSingleton())
            {
                return new SingletonConstructor(@object);
            }

            foreach (var constructor in @object.Constructors())
            {
                var ctor = constructor.GetParameters().Any()
                    ? new ParametrizedConstructor(@object, constructor, sharedArguments)
                    : (IConstructor)new DefaultConstructor(@object, sharedArguments);
                if (ctor.IsValid)
                {
                    return ctor;
                }
            }

            throw new NoConstructorException(@object.GetType());
        }

        internal static IArgument AsSharedArgument(this object @object, IArguments sharedArguments)
        {
            return new SharedArgument(
                new Argument(
                    VariableName(@object),
                    @object,
                    @object.ValidConstructor(sharedArguments)
                )
            );
        }

        internal static List<object> SharedObjects(this object @object)
        {
            return new SharedObjects(@object).ToList();
        }

        internal static bool HasCircularReference(this object @object)
        {
            if (!@object.IsSingleton())
            {
                foreach (var value in @object.Values())
                {
                    if (value != null && value.ContainsDeep(@object))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        internal static bool IsDelegate(this object @object)
        {
            return @object is Delegate;
        }
        
        internal static bool IsSingleton(this object? @object)
        {
            if (@object != null)
            {
                var objectType = @object.GetType();
                if (!objectType.IsValueType && !objectType.Constructors().Any(c => c.IsPublic))
                {
                    return objectType
                        .GetProperties(BindingFlags.Static | BindingFlags.Public)
                        .Any(p => p.PropertyType == objectType && p.CanRead);
                }
            }

            return false;
        }

        private static string VariableName(object @object)
        {
            return Char.ToLower(@object.GetType().Name[0]) + @object.GetType().Name.Substring(1);
        }
    }
}
