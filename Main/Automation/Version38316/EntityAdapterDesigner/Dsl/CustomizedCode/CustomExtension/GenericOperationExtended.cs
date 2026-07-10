using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using System.Xml;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Linx.EntityAdapterDesigner
{
    public partial class GenericOperation
    {
        public string GetParentName()
        {
            if (this is EntityAdapterOperation)
            {
                return ((EntityAdapterOperation)this).EntityAdapter.Name;
            }
            else if (this is EntityAdapterEvent)
            {
                return ((EntityAdapterEvent)this).EntityAdapter.Name;
            }
            else if (this is DomainServiceOperation)
            {
                return ((DomainServiceOperation)this).DomainServiceExtension.Name;
            }
            else
                return String.Empty;
        }

        public string GetParentClassName(string contextName = "")
        {
            if (this is EntityAdapterOperation)
            {
                return ((EntityAdapterOperation)this).EntityAdapter.Name;
            }
            else if (this is EntityAdapterEvent)
            {
                return ((EntityAdapterEvent)this).EntityAdapter.Name;
            }
            else if (this is DomainServiceOperation)
            {
                return (contextName.IsNullOrEmpty() ? ((DomainServiceOperation)this).DomainServiceExtension.EntityAdapterDesignerRoot.GetContextName() : contextName) + "DomainService";
            }
            else
                return String.Empty;
        }
    }
}
