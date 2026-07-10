using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Util
{
    public class ClientServiceExposing
    {
        public string Name { get; set; }

        private List<string> _outputMessages;
        public List<string> OutputMessages { get { if (_outputMessages == null) _outputMessages = new List<string>(); return _outputMessages; } set { _outputMessages = value; } }

        private List<string> _exposedMethods;
        public List<string> ExposedMethods { get { if (_exposedMethods == null) _exposedMethods = new List<string>(); return _exposedMethods; } set { _exposedMethods = value; } }
    }
}
