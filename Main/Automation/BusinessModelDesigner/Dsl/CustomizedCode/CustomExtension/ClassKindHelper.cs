using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.BusinessModelDesigner
{
    public static class ClassKindHelper
    {
        public static bool IsModelViewBehavior(ClassKind value, bool onlyIfNeverUpdatable = false)
        {
            if (onlyIfNeverUpdatable)
                return (value == ClassKind.ModelView);
            else 
                return (value == ClassKind.ModelView || value == ClassKind.DatabaseScript);
        }
    }
}
