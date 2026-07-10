using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterFormula
    {
        public string GetFormulaDefinition()
        {
            return MacroEngine.ReplaceMacros(this.Formula, false);
        }
    }
}
