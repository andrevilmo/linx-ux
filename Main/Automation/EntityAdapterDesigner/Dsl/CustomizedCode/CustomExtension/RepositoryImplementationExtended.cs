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
using System.Collections;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.EntityAdapterDesigner
{
    public partial class RepositoryImplementation
    {
        private bool _hasFocus;
        public bool HasFocus
        {
            get { return _hasFocus; }
            set
            {
                _hasFocus = value;
                UpdateShape();
            }
        }

        private bool GetIsSelectedValue()
        {
            return _hasFocus;
        }

        public void UpdateShape()
        {
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as RepositoryImplementationShape;
            if (shape != null)
            {
                shape.Invalidate(true);
            }
        }
    }

}
