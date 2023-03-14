using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest;
using ObjectToTest.Arguments;
using ObjectToTest.CodeFormatting;
using ObjectToTest.Constructors;
using ObjectToTest.Infrastructure;
// ReSharper disable CheckNamespace

public static class ObjectExtensions
{
    public static string ToTest(this object @object, bool wellFormatted = true)
    {
        if (wellFormatted)
        {
            return @object.ToTestWellFormatted();
        }
        
        return new ObjectAsConstructor(
            @object ?? throw new ArgumentNullException(nameof(@object))
        ).ToString();
    }

    public static string ToTestWellFormatted(this object @object, ILogger? logger = null)
    {
        var objectAsConstructor = new ObjectAsConstructor(
            @object ?? throw new ArgumentNullException(nameof(@object))
        );
        if (objectAsConstructor.Constructor.IsValid)
        {
            return new WellFormattedCode(objectAsConstructor.ToString(), logger ?? new SilentLogger()).ToString();
        }

        return objectAsConstructor.ToString();
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

    internal static IConstructor Constructor(
        this object? @object,
        IArguments sharedArguments)
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

        return @object
            .Constructors(sharedArguments)
            .FirstOrDefault(c => c.IsValid)
            ?? new InvalidConstructor(@object, sharedArguments);
    }

    internal static IEnumerable<IConstructor> Constructors(
       this object @object,
       IArguments sharedArguments)
    {
        return @object.Constructors(
            constructor =>
            constructor.GetParameters().Any()
                ? new ParametrizedConstructor(@object, constructor, sharedArguments)
                : new DefaultConstructor(@object, sharedArguments)
        );
    }

    internal static IEnumerable<IConstructor> Constructors(
        this object @object,
        Func<ConstructorInfo, IConstructor> map)
    {
        return @object
            .Constructors()
            .Select(map);
    }

    internal static IArgument AsSharedArgument(this object @object, IArguments sharedArguments)
    {
        return new SharedArgument(
            new Argument(
                VariableName(@object),
                @object.GetType(),
                @object,
                @object.Constructor(sharedArguments)
            ),
            sharedArguments
        );
    }

    internal static List<object> SharedObjects(this object @object, bool recursive)
    {
        var sharedObjects = new SharedObjects(
            @object,
            recursive
        ).ToList();
        sharedObjects.Sort(new SortByDelegates());
        return sharedObjects;
    }

    internal static bool HasCircularReference(this object @object)
    {
        if (!@object.IsSingleton() && !@object.IsDelegate())
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
        if (@object is Delegate @delegate)
        {
            return $"{VariableName(@delegate.Target)}.{@delegate.Method.Name}";
        }
        return Char.ToLower(@object.GetType().Name[0]) + @object.GetType().Name.Substring(1);
    }
}
