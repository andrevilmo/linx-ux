using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using Linx.Tools;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.BusinessDataModelDesigner
{
    public partial class ModelImplementation
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
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as ModelImplementationShape;
            if (shape != null)
            {
                shape.Invalidate(true);
            }
        }
    }
}
