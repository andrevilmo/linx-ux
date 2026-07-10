using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using System.Reflection;
using System.IO;
using System.Collections;
using Microsoft.VisualStudio.Modeling;
using System.Xml.XPath;

namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmMacroScriptEditor : Form
    {
        private List<Linx.Builder.Resources.MacroDomain> _domains = new List<Builder.Resources.MacroDomain>();
        public List<Linx.Builder.Resources.MacroDomain> Domains { get { return _domains; } }
        private Linx.Builder.Resources.MacroNode[] macros;
        private System.Xml.XmlDocument SamplesDocument = new System.Xml.XmlDocument();
        private ClientEvent clientEvent;


        public ClientEvent ClientEvent
        {
            get { return this.clientEvent; }
            set
            {
                this.clientEvent = value;
                string envPart = "";
                if (this.clientEvent is EntityAdapterClientEvent)
                    envPart = ((EntityAdapterClientEvent)this.clientEvent).EntityAdapter.EntityAdapterDesignerRoot.GetDirectorySourcePart();
                else if (this.clientEvent is UserInterfaceClientEvent)
                    envPart = ((UserInterfaceClientEvent)this.clientEvent).EntityAdapterUserInterface.EntityAdapterDesignerRoot.GetDirectorySourcePart();



                //Get Script
                this.scriptBox.Text = this.clientEvent.MacroScript;
                //Populate Available Macros
                Linx.Builder.Resources.MacroScriptEngine macro = new Builder.Resources.MacroScriptEngine();
                //GetSamples
                SamplesDocument.Load(macro.GetSamplesPath(envPart));

                macros = macro.GetMacros(envPart).Where(e => e.Value.Domain == Builder.Resources.MacroDomain.All || _domains.Contains(e.Value.Domain)).Select(e => e.Value).OrderBy(e => e.Name).ToArray();
                this.Text = "Macro Script Editor: " + clientEvent.ReturnType + " " + clientEvent.Name + "(" + clientEvent.Parameters.Replace("#", ",") + ")";
                this.FillMacros();



                configureScriptBox(macros);
            }
        }

        private void configureScriptBox(Builder.Resources.MacroNode[] macros)
        {
            var autoCompleteList = macros.Select(m => "@" + m.Name).ToList();
            autoCompleteList.Sort();

            this.scriptBox.AutoComplete.AutomaticLengthEntered = true;
            this.scriptBox.AutoComplete.AutoHide = false;
            this.scriptBox.AutoComplete.IsCaseSensitive = false;
            this.scriptBox.AutoComplete.DropRestOfWord = true;


            this.scriptBox.Lexing.SetKeywords(3, string.Join(" ", autoCompleteList));
            this.scriptBox.Lexing.Keywords[1] = ";;";


            foreach (var m in macros.OrderBy(m => m.Name))
                this.scriptBox.Snippets.List.Add(new ScintillaNET.Snippet("@" + m.Name, m.ParameterCount == 0 ? m.Document : m.Document.Replace("(", "($").Replace(")", "$)").Replace(";;", "$;;$")));

            //this code was commented, because throw error if user typed í'
            //this.scriptBox.CharAdded += (sender, e) =>
            //{
            //    if (e.Ch == ' ')
            //        return;
            //    try
            //    {
            //        string word = getCurrentWord(this.scriptBox.NativeInterface.GetCurrentPos(), this.scriptBox.Text);

            //        if (word == string.Empty)
            //            return;
            //        List<string> list = autoCompleteList.Where(item => (item).ToLower().StartsWith(word.ToLower())).Select(i => i.Substring(1)).ToList();
            //        if (list.Count > 0)
            //            this.scriptBox.AutoComplete.Show(list);
            //    }
            //    catch (Exception ex) { 
            //        MessageBox.Show(ex.Message +"\r\n"+ex.ToString());
            //    }
            //};
        }

        string getCurrentWord(int pos, string texto)
        {
            int start = pos, end = pos;

            while (start > 0 && (texto[start - 1] != ' ' && texto[start - 1] != '\n'))
                --start;

            while (end > texto.Length && texto[end] == ' ')
                end++;

            return texto.Substring(start, end - start);
        }

        public FrmMacroScriptEditor()
        {
            InitializeComponent();
        }

        private void FillMacros()
        {
            if (textSearch.Text.IsNullOrEmpty())
                macroNodeDataGridView.DataSource = macros;
            else
                macroNodeDataGridView.DataSource = macros.Where(e => e.Name.ToLower().Contains(textSearch.Text.ToLower()) || e.Description.ToLower().Contains(textSearch.Text.ToLower())).ToArray();


            labelMacros.Text = "Available Macros (" + ((Linx.Builder.Resources.MacroNode[])macroNodeDataGridView.DataSource).Length.ToString() + "):";

        }


        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApplyScript_Click(object sender, EventArgs e)
        {
            if (this.clientEvent.MacroScript != this.scriptBox.Text)
            {
                using (Transaction tran = this.clientEvent.Store.TransactionManager.BeginTransaction("Change MacroScript"))
                {
                    this.clientEvent.MacroScript = this.scriptBox.Text;
                    tran.Commit();
                }
            }
            this.Close();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            this.FillMacros();
        }

        private void macroNodeDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            string document = string.Empty;

            if (macroNodeDataGridView.SelectedCells.Count > 0)
            {
                var macro = (Linx.Builder.Resources.MacroNode)macroNodeDataGridView.Rows[macroNodeDataGridView.SelectedCells[0].RowIndex].DataBoundItem;
                if (!macro.IsNull() && !SamplesDocument.IsNull())
                {
                    var node = SamplesDocument.SelectSingleNode("/MacroSamples/MacroSample[@Macro='" + macro.Name + "']");
                    if (!node.IsNull())
                        document = node.InnerText;
                }
            }
            DocumentTextBox.Text = document;

        }
    }
}
