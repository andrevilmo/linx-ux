using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomCode;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Forms.Editors
{
    //Linx.EntityAdapterDesigner.CustomizedCode.Forms.Editors.MacroEditor
    public class MacroEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (svc != null)
            {
                var editor = new FrmMacroScriptEditor();
                editor.AddMacroDomain(MacroDomain.ServerAll, MacroDomain.ServerContext, MacroDomain.ServerEntity);
                if (context.Instance is IAditionalInformation)
                    editor.SetEnvPart(((IAditionalInformation)context.Instance).GetEnvPart());
                //Set value control
                editor.ValueText = value as string;

                svc.ShowDialog(editor);

                //Get value Control
                value = editor.ValueText;
            }
            return value;
        }
    }
}
