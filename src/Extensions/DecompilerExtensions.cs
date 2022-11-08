using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.TypeSystem;

namespace ObjectToTest.Extensions
{
    public static class DecompilerExtensions
    {
        public static ITypeDefinition? Type(this CSharpDecompiler decompiler, object @object)
        {
            return decompiler.Type(
                @object.GetType().FullName
            );
        }
        
        public static ITypeDefinition? Type(this CSharpDecompiler decompiler, string typeName)
        {
            return decompiler.TypeSystem.FindType(
                new FullTypeName(typeName)
            ).GetDefinition();
        }
        
        public static IMethod Method(this CSharpDecompiler decompiler, Delegate @delegate)
        {
            return decompiler.Method(
                @delegate.Target,
                @delegate.Method.Name
            ) ?? throw new InvalidOperationException($"Method '{@delegate.Method.Name}' not found in '{@delegate.Target}'");
        }
        
        public static IMethod? Method(this CSharpDecompiler decompiler, object @object, string methodName)
        {
            return decompiler
                .Type(@object)
                ?.Methods
                .First(m => m.Name == methodName);
        }
        
        public static string AsCode(this Delegate @delegate)
        {
            /*
             * @todo #54:60m/LEAD
             * Using new MethodBody(_object.Method) as a helper
             * DelegateBody method should be able to generate the code base for the delegate to
             * be able to pass the tests that reference DelegateConstructor class.
             * See ObjectToTest.ILReader.MethodBody class which is able to generate
             * IL code instructions based on byte array received from _object.Method.
             * 
             * TARGET:
             * - NEED TO FIND a way to convert IL instructions into C# code.
             * - To test the result DelegateConstructorTests skip marker should be removed.
             * - Ideally all the tests should pass.
            */
            
            var decompiler = new CSharpDecompiler(
                @delegate.Target.GetType().Module.Assembly.Location,
                new DecompilerSettings(LanguageVersion.Latest)
            );
            
            var decompiledCode = decompiler.Decompile(
                    decompiler
                        .Method(@delegate)
                        .MetadataToken
            );
            var method = (MethodDeclaration?)decompiledCode.LastChild ?? throw new InvalidOperationException("Method not found");
            if (method.InAnonymous())
            {
                if (method.Parameters.Count == 1)
                {
                    return $"{method.ParametersAsString()} => {method.BodyAsString()}";
                }
                return $"({method.ParametersAsString()}) => {method.BodyAsString()}";
            }
            return method.ToString();
        }

        private static string BodyAsString(this MethodDeclaration method)
        {
            var body = method.Body.ToString();
            if (method.Body.Statements.Count <= 1)
            {
                body = body
                    .Replace(Environment.NewLine, string.Empty)
                    .Replace("\t", string.Empty);

                if (method.Body.Statements.Count == 1)
                {
                    body = body
                        .Replace("{return ", string.Empty)
                        .Replace(";}", string.Empty);
                }
            }

            return body;
        }
        
        private static string ParametersAsString(this MethodDeclaration method)
        {
            return
                string.Join(", ", method.Parameters.Select(p => p.Name));
        }
        
        private static bool InAnonymous(this MethodDeclaration method)
        {
            if (method.Name.Contains("<"))
            {
                return true;
            }

            return false;
        }
    }
}