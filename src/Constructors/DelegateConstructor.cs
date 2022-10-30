using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class DelegateConstructor : IConstructor
    {
        private readonly Delegate _object;

        public DelegateConstructor(object @object)
            : this((Delegate)@object)
        {
        }
        
        public DelegateConstructor(Delegate @object)
        {
            _object = @object;
        }

        public bool IsValid => true;
        
        public IList<IArgument> Argumetns => new List<IArgument>();

        public override string ToString()
        {
            var actionMethod = _object.GetInvocationList()[0].Method;
            var parameters = actionMethod.GetParameters();
            var names = string.Join(", ", parameters.Select(p => p.Name));
            if (parameters.Length == 1)
            {
                return $"{names} => {DelegateBody()}";
            }
            return $"({names}) => {DelegateBody()}";
        }

        private string DelegateBody()
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
                _object.Target.GetType().Module.Assembly.Location,
                new DecompilerSettings(LanguageVersion.Latest)
            );
            
            var nameOfUniResolver =  new FullTypeName(_object.Target.GetType().FullName);
            ITypeDefinition typeInfo = decompiler.TypeSystem.FindType(nameOfUniResolver).GetDefinition();
            var tokenOfFirstMethod = typeInfo.Methods.First(m => m.Name == _object.Method.Name).MetadataToken;
            var test = decompiler.DecompileAsString(tokenOfFirstMethod);
            return "{ }";
        }
    }
}