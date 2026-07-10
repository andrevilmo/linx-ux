using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Helpers
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
