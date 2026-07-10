using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tom;

namespace LinxSistemas.JsEditor
{
    class TextSelection : ITextSelection
    {
         EditorTextBox _editor;
         public TextSelection(EditorTextBox editor)
        {
            _editor = editor;
        }

        public int CanEdit()
        {
            return 1;
        }

        public int CanPaste(ref object pVar, int Format)
        {
            return 1;
        }

        public void ChangeCase(int Type)
        {
            
        }

        public int Char
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public void Collapse(int bStart)
        {
            
        }

        public void Copy(out object pVar)
        {
            pVar = _editor.SelectedText;
        }

        public void Cut(out object pVar)
        {
            pVar = _editor.SelectedText;
        }

        public int Delete(int Unit, int Count)
        {
            _editor.DeleteRange(Unit, Count);
            return 1;
        }

        public ITextRange Duplicate
        {
            get { return null; }
        }

        public int End
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public int EndKey(int Unit, int Extend)
        {
            return 0;
        }

        public int EndOf(int Unit, int Extend)
        {
            return 0;
        }

        public int Expand(int Unit)
        {
            return 0;
        }

        public int FindText(string bstr, int cch, int Flags)
        {
            return 0;
        }

        public int FindTextEnd(string bstr, int cch, int Flags)
        {
            return 0;
        }

        public int FindTextStart(string bstr, int cch, int Flags)
        {
            return 0;
        }

        public int Flags
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public ITextFont Font
        {
            get
            {
                return null;
            }
            set
            {                
            }
        }

        public ITextRange FormattedText
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public dynamic GetEmbeddedObject()
        {
            return null;
        }

        public int GetIndex(int Unit)
        {
            return 0;
        }

        public void GetPoint(int Type, out int px, out int py)
        {
            px = 0;
            py = 0;
        }

        public int HomeKey(int Unit, int Extend)
        {
            return 0;
        }

        public int InRange(ITextRange pRange)
        {
            return 0;
        }

        public int InStory(ITextRange pRange)
        {
            return 0;
        }

        public int IsEqual(ITextRange pRange)
        {
            return 0;
        }

        public int Move(int Unit, int Count)
        {
            return 0;
        }

        public int MoveDown(int Unit, int Count, int Extend)
        {
            return 0;
        }

        public int MoveEnd(int Unit, int Count)
        {
            return 0;
        }

        public int MoveEndUntil(ref object Cset, int Count)
        {
            return 0;
        }

        public int MoveEndWhile(ref object Cset, int Count)
        {
            return 0;
        }

        public int MoveLeft(int Unit, int Count, int Extend)
        {
            throw new NotImplementedException();
        }

        public int MoveRight(int Unit, int Count, int Extend)
        {
            return 0;
        }

        public int MoveStart(int Unit, int Count)
        {
            return 0;
        }

        public int MoveStartUntil(ref object Cset, int Count)
        {
            return 0;
        }

        public int MoveStartWhile(ref object Cset, int Count)
        {
            return 0;
        }

        public int MoveUntil(ref object Cset, int Count)
        {
            return 0;
        }

        public int MoveUp(int Unit, int Count, int Extend)
        {
            return 0;
        }

        public int MoveWhile(ref object Cset, int Count)
        {
            return 0;
        }

        public ITextPara Para
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public void Paste(ref object pVar, int Format)
        {
            pVar = _editor.SelectedText;
        }

        public void ScrollIntoView(int Value)
        {
            
        }

        public void Select()
        {
            
        }

        public void SetIndex(int Unit, int Index, int Extend)
        {
            
        }

        public void SetPoint(int x, int y, int Type, int Extend)
        {
            
        }

        public void SetRange(int cpActive, int cpOther)
        {
            
        }

        public int Start
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public int StartOf(int Unit, int Extend)
        {
            return 0;
        }

        public int StoryLength
        {
            get { return 0; }
        }

        public int StoryType
        {
            get { return 0; }
        }

        public string Text
        {
            get
            {
                return _editor.Text;
            }
            set
            {
                
            }
        }

        public int Type
        {
            get { return 0; }
        }

        public void TypeText(string bstr)
        {
            
        }
    }
}
