using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ServiceModel.DomainServices.Server;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.CodeDom;
using System.Linq;

namespace Linx.Tools
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectClientsideCodeAttribute : Attribute
    {
        public InjectClientsideCodeAttribute(string methodName, string code)
        {
            Code = code;
            MethodName = methodName;
        }
        public string Code;
        public string MethodName;
    }

    public class MethodPatchingCodeProcessor : CodeProcessor
    {
        public MethodPatchingCodeProcessor(CodeDomProvider codeDomProvider) : base(codeDomProvider) { }

        public override void ProcessGeneratedCode(DomainServiceDescription domainServiceDescription,
            System.CodeDom.CodeCompileUnit codeCompileUnit,
            IDictionary<Type, CodeTypeDeclaration> typeMapping)
        {
            Dictionary<Type, CodeTypeDeclaration> typesToPatch = typeMapping.Where(tm => tm.Key.GetCustomAttributes(typeof(InjectClientsideCodeAttribute), false).Length > 0).ToDictionary(p => p.Key, p => p.Value);

            foreach (var typeToPatch in typesToPatch)
            {
                foreach (InjectClientsideCodeAttribute injectionAttribute in typeToPatch.Key.GetCustomAttributes(typeof(InjectClientsideCodeAttribute), false))
                {
                    var methodsToPatch = typeToPatch.Value.Members.OfType<CodeMemberMethod>().ToList();
                    methodsToPatch = methodsToPatch.Where(p => p.Name == injectionAttribute.MethodName).ToList();

                    foreach (var methodToPatch in methodsToPatch)
                    {
                        methodToPatch.Statements.Insert(0, new CodeSnippetStatement(injectionAttribute.Code));
                    }
                }
            }
        }
    }



}
