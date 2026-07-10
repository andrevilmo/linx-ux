using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapter
    {
        public string GetCoreAssociation(bool isForeignKey)
        {
            return GetAssociation(isForeignKey, false, true);
        }
    }
}
