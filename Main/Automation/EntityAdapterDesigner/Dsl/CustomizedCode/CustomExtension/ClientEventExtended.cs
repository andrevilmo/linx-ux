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
            editor.AddMacroDomain(MacroDomain.ClientAll);
            if (this is UserInterfaceClientEvent)
            {
                editor.AddMacroDomain(MacroDomain.ClientContext);
                var ui = ((UserInterfaceClientEvent)this).EntityAdapterUserInterface;
                if (ui != null)
                    editor.SetLanguage(ui.VisualType == InterfaceType.Mobile ? MacroOutputType.JavaScriptMobile : MacroOutputType.JavaScript);

            }
            else if (this is ServiceClientEvent)
            {
                editor.AddMacroDomain(MacroDomain.ClientContext);
                editor.SetLanguage(MacroOutputType.JavaScriptMobile);
            }
            else
            {
                editor.AddMacroDomain(MacroDomain.ClientEntity);

            }

            editor.ClientEvent = this;
            editor.ShowDialog();
        }
    }
}
