using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.ServiceBus.Starter.Areas.HelpPage.Models
{
    public partial class HelpPageApiModel
    {
        private IDictionary<string, object> _AdditionalInformation = new Dictionary<string, object>();
        public IDictionary<string, object> AdditionalInformation { get { return _AdditionalInformation; } }

        public bool HasAdditionalInformation
        {
            get { return AdditionalInformation.Count > 0; }
        }
    }
}