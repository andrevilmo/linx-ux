using Linx.Data;
using Linx.LinqExtensions.Dynamic;
using Linx.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using Linx.DataService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Linx.License.Client;

namespace Linx.License.Client.WebAPICore.Controllers
{
    
    //Examples:
    // Feed OData Call: http://localhost:1710/Licenciamento/$metadata
    // Documentation: http://localhost:1710/swagger/ui
    [EnableQuery()]
    [Route("Licenciamento")]
    public partial class LicenciamentoController : Controller
    {
        private LicenseContext _context;
        public LicenseContext Context { get {  if (_context == null) { _context = new LicenseContext("", null as Dictionary<string, string>); } return _context; }  }
    }
    
}
