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
using Linx.EntityAdapterDesigner.CustomCode;

namespace Linx.EntityAdapterDesigner
{
    public partial class ClientEvent
    {
        public void EditScript()
        {
            FrmMacroScriptEditor editor = new FrmMacroScriptEditor();
            editor.Domains.Add(MacroDomain.ClientAll);
            if (this is UserInterfaceClientEvent)
                editor.Domains.Add(MacroDomain.ClientContext);                
            else 
                editor.Domains.Add(MacroDomain.ClientEntity);
            editor.ClientEvent = this;
            editor.ShowDialog();
        }
    }    
}
