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

namespace Linx.BusinessModelDesigner
{
    public partial class ModelInterface
    {
        public void CheckDefaultImplementation()
        {
            if (this.ModelImplementations.Count > 0 && this.ModelImplementations.Where(u => u.HasFocus).Count() == 0)
                this.ModelImplementations[0].HasFocus = true;
        }
    }
}
