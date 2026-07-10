
using System;
using System.Windows.Forms;
using System.Security.Permissions;
using ScintillaNET;
using System.Drawing;

namespace LinxSistemas.JsEditor
{
    public partial class EditorTextBox : ScintillaNET.Scintilla  //RichTextBox
    {
        private bool m_FilterMouseClickMessages;

        public HorizontalAlignment SelectionAlignment { get; set; }
        public bool SelectionBullet { get; set; }
        public int SelectionLength { get { return this.SelectedText.Length; } set { } }
        public tom.ITextSelection Selection { get; set; }

        public int GetFirstCharIndexFromLine(int index)
        {
            var line = this.Lines[index];
            return line.EndPosition - line.Length;
        }

        public int GetFirstCharIndexOfCurrentLine()
        {
            return this.GetFirstCharIndexFromLine(this.LineFromPosition(this.CurrentPosition));
        }

        public int GetLineFromCharIndex(int index)
        {
            return this.LineFromPosition(index);
        }

        public void LoadFile(string pszFilename, RichTextBoxStreamType type)
        {
            this.Text = System.IO.File.ReadAllText(pszFilename);
        }

        public void SaveFile(string pszFilename, string text)
        {
            System.IO.File.WriteAllText(pszFilename, text);
        }

        public void SaveFile(string pszFilename, RichTextBoxStreamType streamTypoe = RichTextBoxStreamType.PlainText)
        {
            this.SaveFile(pszFilename, this.Text);
        }

        public bool FilterMouseClickMessages
        {
            get { return m_FilterMouseClickMessages; }
            set { m_FilterMouseClickMessages = value; }
        }

        public EditorTextBox()
        {
            InitializeComponent();

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lexer = ScintillaNET.Lexer.Cpp;

            this.IndentWidth = 2;
            this.UseTabs = false;
            this.Margins[0].Width = 20;


            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            this.StyleResetDefault();
            this.Styles[Style.Default].Font = "Consolas";
            this.Styles[Style.Default].Size = 10;
            this.StyleClearAll();

            // Config1ure the CPP (C#) lexer styles
            this.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            this.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            this.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            this.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            this.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            this.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            this.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            this.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            this.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            this.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            this.Styles[Style.BraceLight].BackColor = Color.LightGray;
            this.Styles[Style.BraceLight].ForeColor = Color.BlueViolet;
            this.Styles[Style.BraceBad].ForeColor = Color.Red;
            this.IndentationGuides = IndentView.LookBoth;
            this.Styles[Style.Cpp.GlobalClass].ForeColor = Color.Blue;

            this.SetKeywords(0, "abstract arguments boolean break byte case catch char class const continue debugger default delete do double else enum eval export extends false final finally float for function goto if implements import in instanceof int interface let long native new null package private protected public return short static super switch synchronized this throw throws transient true try typeof var void volatile while with yield");
            this.SetKeywords(1, "Array Date eval function hasOwnProperty Infinity isFinite isNaN isPrototypeOf length Math NaN name Number Object prototype String toString undefined valueOf");
            this.SetKeywords(3, "require angular");

            // Instruct the lexer to calculate folding
            this.SetProperty("fold", "1");
            this.SetProperty("fold.compact", "1");


            // Configure a margin to display folding symbols
            this.Margins[2].Type = MarginType.Symbol;
            this.Margins[2].Mask = Marker.MaskFolders;
            this.Margins[2].Sensitive = true;
            this.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                this.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                this.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            this.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            this.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            this.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            this.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            this.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            this.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            this.UpdateUI += scintilla_UpdateUI;

            this.TextChanged += Scintilla_TextChanged;


        }


        string AClist = "abstract,arguments,boolean,break,byte,case,catch,char,class,const,continue,debugger,default,delete,do,double,else,enum,eval,export,extends,false,final,finally,float,for,function,goto,if,implements,import,in,instanceof,int,interface,let,long,native,new,null,package,private,protected,public,return,short,static,super,switch,synchronized,this,throw,throws,transient,true,try,typeof,var,void,volatile,while,with,yield";
        private void Scintilla_TextChanged(object sender, EventArgs e)
        {


            //var lista 
            //scintilla.AutoCShow(2, );
            //scintilla.ShowToolTip(reader);
        }

        private static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '<':
                case '>':
                    return true;
            }

            return false;
        }
        int lastCaretPos = 0;

        private void scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = this.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(this.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(this.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = this.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                        this.BraceBadLight(bracePos1);
                    else
                        this.BraceHighlight(bracePos1, bracePos2);
                }
                else
                {
                    // Turn off brace matching
                    this.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                }
            }
        }


        // Override WndProc so that we can ignore the mouse clicks when macro recording
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_LBUTTONDOWN:
                case NativeMethods.WM_RBUTTONDOWN:
                case NativeMethods.WM_MBUTTONDOWN:
                case NativeMethods.WM_LBUTTONDBLCLK:
                    if (m_FilterMouseClickMessages)
                    {
                        Focus();
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void richTextBoxCtrl_MouseRecording(object sender, EventArgs e)
        {
            SetCursor(m_FilterMouseClickMessages);
        }

        private void richTextBoxCtrl_MouseLeave(object sender, EventArgs e)
        {
            if (m_FilterMouseClickMessages)
                SetCursor(!m_FilterMouseClickMessages);
        }

        private void SetCursor(bool cursorNo)
        {
            Cursor = cursorNo ? Cursors.No : Cursors.Default;
        }
    }
}
