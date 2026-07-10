using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tom;

namespace LinxSistemas.JsEditor
{
    internal class TextDocument : ITextDocument
    {
        EditorTextBox _editor;
        public TextDocument(EditorTextBox editor)
        {
            _editor = editor;
            _selection = new TextSelection(editor);
        }

        public void BeginEditCollection()
        {
            
        }

        private float _defaultTabStop = 0;
        public float DefaultTabStop
        {
            get
            {
                return _defaultTabStop;
            }
            set
            {
                _defaultTabStop = value;
            }
        }

        public void EndEditCollection()
        {
            
        }

        public int Freeze()
        {
            return 0;
        }

        public string Name
        {
            get { return _editor.Name; }
        }

        public void New()
        {
            
        }

        public void Open(ref object pVar, int Flags, int CodePage)
        {
            
        }

        public ITextRange Range(int cp1, int cp2)
        {
            return null;
        }

        public ITextRange RangeFromPoint(int x, int y)
        {
            return null;
        }

        public int Redo(int Count)
        {
            return 0;
        }

        public void Save(ref object pVar, int Flags, int CodePage)
        {
            
        }

        private int _saved;
        public int Saved
        {
            get
            {
                return _saved;
            }
            set
            {
                _saved = value;
            }
        }

        private ITextSelection _selection;
        public ITextSelection Selection
        {
            get { return _selection; }
        }

        public int StoryCount
        {
            get { return 0; }
        }

        public ITextStoryRanges StoryRanges
        {
            get { return null; }
        }

        public int Undo(int Count)
        {
            return 0;
        }

        public int Unfreeze()
        {
            return 0; ;
        }
    }
}
