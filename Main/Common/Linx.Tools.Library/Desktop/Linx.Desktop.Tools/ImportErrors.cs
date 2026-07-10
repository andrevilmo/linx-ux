using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    /// <summary>
    /// Structure of the error list
    /// </summary>
    public struct ImportErrors
    {
        public string FileName { get; set; }
        public string ErrorType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
