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

namespace Linx.EntityAdapterDesigner
{

    public partial class RepositoryInterface
    {
        public void CheckDefaultImplementation()
        {
            if (this.RepositoryImplementations.Count > 0 && this.RepositoryImplementations.Where(u => u.IsDefault).Count() == 0)
                this.RepositoryImplementations[0].IsDefault = true;

            if (this.RepositoryImplementations.Count > 0 && this.RepositoryImplementations.Where(u => u.HasFocus).Count() == 0)
                this.RepositoryImplementations[0].HasFocus = true;
        }
    }
    
}
