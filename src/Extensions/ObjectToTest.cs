using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.Extensions
{
    internal static class ObjectToTest
    {
        public static string GetResultStringForObject<T>(T obj, ref int objectNameCounter, out string variableName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            variableName = $"o{objectNameCounter++}";

            var resultStatements = new List<string>();
            var type = obj.GetType();
            var constructors = type.GetConstructors();
            var typeName = type.Name;

            var defaultConst = constructors.FirstOrDefault(x => x.GetParameters().Length == 0);

            if (defaultConst != null)
            {
                resultStatements.Add(DefaultContructorStatement(variableName, typeName));
            }
            else
            {
                var withClassParams = constructors.FirstOrDefault(x => x.GetParameters().Any(p => !IsSimple(p.ParameterType)));

                if (withClassParams != null)
                {
                    var parameters = withClassParams.GetParameters();
                    var paramsList = new List<string>();
                    foreach (var parameter in parameters)
                    {
                        if (IsSimple(parameter.ParameterType))
                        {
                            var value = GetValueForConstructorParam(parameter, type, obj);
                            var valueStr = GetValueForInitialization(value);

                            paramsList.Add(valueStr);
                        }
                        else
                        {
                            var typeValue = GetValueForConstructorParam(parameter, type, obj);
                            var typeValueStr = GetResultStringForObject(typeValue, ref objectNameCounter, out string typeParamName);

                            resultStatements.Add(typeValueStr);

                            paramsList.Add(typeParamName);
                        }
                    }

                    var paramsStr = string.Join(",", paramsList);

                    resultStatements.Add($"var {variableName}=new {type.Name}({paramsStr});");
                }
                else
                {
                    var costructorStr = GetContructorWithParams(constructors, type, obj);
                    resultStatements.Add($"var {variableName}={costructorStr};");
                }
            }

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (!property.CanWrite)
                {
                    continue;
                }

                var value = property.GetValue(obj, null);

                resultStatements.Add(GetPublicPropertyInitialization(variableName, property.Name, value));
            }

            return string.Join(string.Empty, resultStatements);
        }

        private static string DefaultContructorStatement(string variableName, string typeName)
            => $"var {variableName}=new {typeName}();";

        private static string GetPublicPropertyInitialization<T>(string objectName, string propertyName, T value)
        {
            var valueStr = GetValueForInitialization(value);

            return $"{objectName}.{propertyName} = {valueStr};";
        }

        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive
              || type == typeof(string);
        }
        private static string GetValueForInitialization<T>(T value)
        {
            string valueStr;
            if (value == null)
            {
                valueStr = "null";
            }
            else if (value is string)
            {
                valueStr = $"\"{value}\"";
            }
            else
            {
                valueStr = value.ToString();
            }

            return valueStr;
        }

        private static string GetContructorWithParams<T>(ConstructorInfo[] constructors, Type type, T obj)
        {
            constructors = constructors.OrderByDescending(x => x.GetParameters().Length).ToArray();

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var withFullMatch = parameters.All(x => ContainsConstructorValueInType(x, type));

                if (withFullMatch)
                {
                    var paramsToStr = parameters
                        .Select(x => GetValueForConstructorParam(x, type, obj))
                        .Select(x => GetValueForInitialization(x));
                    var paramsStr = string.Join(",", paramsToStr);

                    return $"new {type.Name}({paramsStr})";
                }
            }

            return string.Empty;
        }

        private static bool ContainsConstructorValueInType(ParameterInfo info, Type type)
        {
            var typeProperties = type.GetProperties();
            var typeFields = type.GetRuntimeFields().ToList();

            var containsInProperties = typeProperties.Any(p => ContainsIgnoringCase(p.Name, info.Name));
            var containsInFields = typeFields.Any(f => ContainsIgnoringCase(f.Name, info.Name));

            return containsInProperties || containsInFields;
        }

        private static bool ContainsIgnoringCase(string firstStr, string secondStr)
            => firstStr.Replace("_", string.Empty).ToLower().Contains(secondStr.ToLower().Replace("_", string.Empty));

        private static object GetValueForConstructorParam<T>(ParameterInfo info, Type type, T obj)
        {
            var typeProperties = type.GetProperties();
            var typeFields = type.GetRuntimeFields().ToList();

            var propertyInfo = typeProperties.FirstOrDefault(p => IsVariableNameEqual(p.Name, info.Name));
            var fieldInfo = typeFields.FirstOrDefault(f => IsVariableNameEqual(f.Name, info.Name));

            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(obj);

                return fieldValue;
            }
            else if (propertyInfo != null)
            {
                var propertyValue = propertyInfo.GetValue(obj);

                return propertyValue;
            }

            throw new ArgumentException($"Can not get value for parameter with name {info.Name} in type {type.Name}");
        }

        private static bool IsVariableNameEqual(string fromTypeName, string fromParamName)
        {
            fromTypeName = fromTypeName.Replace("_", string.Empty);
            fromParamName = fromParamName.Replace("_", string.Empty);

            return fromTypeName.Equals(fromParamName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
