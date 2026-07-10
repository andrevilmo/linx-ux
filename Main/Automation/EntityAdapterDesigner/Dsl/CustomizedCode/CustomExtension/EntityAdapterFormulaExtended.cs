using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;

namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterFormula
    {
        public string GetFormulaDefinition()
        {
            return MacroEngineHelper.ReplaceMacros(this.Formula, Builder.Resources.MacroOutputType.CSharp, this);
        }
    }
}
