using Linx.Builder.Resources;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class FrmMacroScriptEditor : Form
    {
        private List<MacroDomain> _macroDomains = new List<MacroDomain>();
        public MacroOutputType? _macroOutput { get; set; }
        private MacroNode[] _macros;
        private XmlDocument _samplesDocument = new XmlDocument();
        private string _envPart;

        #region ValueText
        /// <summary>
        /// Get or Set the value of the control MacroEditor
        /// </summary>
        public string ValueText
        {
            get { return this.scriptBox.Text; }
            set { this.scriptBox.Text = value; }
        }
        #endregion

        #region ClientEvent
        private ClientEvent _clientEvent;
        public ClientEvent ClientEvent
        {
            get { return this._clientEvent; }
            set
            {
                this._clientEvent = value;
                this.scriptBox.Text = this._clientEvent.MacroScript;
            }
        }
        #endregion

        #region Ctor
        public FrmMacroScriptEditor()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                LoadMacros();
            };
        }
        #endregion

        #region Private Methods
        private void LoadMacros()
        {
            if (this._envPart.IsNullOrEmpty() && !this._clientEvent.IsNull())
            {
                if (this._clientEvent is EntityAdapterClientEvent)
                    _envPart = ((EntityAdapterClientEvent)this._clientEvent).EntityAdapter.GetEnvPart();
                else if (this._clientEvent is UserInterfaceClientEvent)
                    _envPart = ((UserInterfaceClientEvent)this._clientEvent).EntityAdapterUserInterface.EntityAdapterDesignerRoot.GetEnvPart();
                else if (this._clientEvent is ServiceClientEvent)
                    _envPart = ((ServiceClientEvent)this._clientEvent).ClientLocalService.EntityAdapterDesignerRoot.GetEnvPart();
            }

            //Populate Available Macros
            Linx.Builder.Resources.MacroScriptEngine macro = new Builder.Resources.MacroScriptEngine();
            //GetSamples
            _samplesDocument.Load(macro.GetSamplesPath(_envPart));

            _macros = macro.GetMacros(_envPart).Where(e => e.Value.Domain == Builder.Resources.MacroDomain.All || _macroDomains.Contains(e.Value.Domain)).Select(e => e.Value).OrderBy(e => e.Name).ToArray();

            if (this._macroOutput.HasValue) {
                _macros = _macros.Where(m => m.Outputs.Any(mo => mo.Type == this._macroOutput.Value)).ToArray();
            }

            if (!this._clientEvent.IsNull())
                this.Text = "Macro Script Editor: " + _clientEvent.ReturnType + " " + _clientEvent.Name + "(" + _clientEvent.Parameters.Replace("#", ",") + ")";
            else
                this.Text = "Macro Script Editor: DataContext Related Property ";

            this.FillMacros();

            configureScriptBox(_macros);
            this.scriptBox.Refresh();
        }

        private void configureScriptBox(MacroNode[] macros)
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

        private string getCurrentWord(int pos, string texto)
        {
            int start = pos, end = pos;

            while (start > 0 && (texto[start - 1] != ' ' && texto[start - 1] != '\n'))
                --start;

            while (end > texto.Length && texto[end] == ' ')
                end++;

            return texto.Substring(start, end - start);
        }

        private void FillMacros()
        {
            if (textSearch.Text.IsNullOrEmpty())
                macroNodeDataGridView.DataSource = _macros;
            else
                macroNodeDataGridView.DataSource = _macros.Where(e => e.Name.ToLower().Contains(textSearch.Text.ToLower()) || e.Description.ToLower().Contains(textSearch.Text.ToLower())).ToArray();


            labelMacros.Text = "Available Macros (" + ((Linx.Builder.Resources.MacroNode[])macroNodeDataGridView.DataSource).Length.ToString() + "):";

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApplyScript_Click(object sender, EventArgs e)
        {
            if (this._clientEvent != null)
            {
                if (this._clientEvent.MacroScript != this.scriptBox.Text)
                {
                    using (Transaction tran = this._clientEvent.Store.TransactionManager.BeginTransaction("Change MacroScript"))
                    {
                        this._clientEvent.MacroScript = this.scriptBox.Text;
                        tran.Commit();
                    }
                }
            }
            else
                this.ValueText = this.scriptBox.Text;
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
                if (!macro.IsNull() && !_samplesDocument.IsNull())
                {
                    var node = _samplesDocument.SelectSingleNode("/MacroSamples/MacroSample[@Macro='" + macro.Name + "']");
                    if (!node.IsNull())
                        document = node.InnerText;
                }
            }
            DocumentTextBox.Text = document;

        }
        #endregion

        #region public Methods
        public void SetEnvPart(string envPart)
        {
            this._envPart = envPart;
        }

        public void AddMacroDomain(params MacroDomain[] domains)
        {
            this._macroDomains.AddRange(domains);
        }

        public void SetLanguage(MacroOutputType? macroOutput) {
            this._macroOutput = macroOutput;
        }
        #endregion

        
    }
}
