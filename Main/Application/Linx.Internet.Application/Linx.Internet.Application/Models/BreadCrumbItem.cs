using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.Internet.Application.Models
{
    public class BreadCrumbItem
    {
        public byte order { get; set; }
        public string moduleKey { get; set; }
        public string displayName { get; set; }
        public string urlRoute { get; set; }
    }
}