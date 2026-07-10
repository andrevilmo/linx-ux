using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AppApiCore.Controllers
{

    // [Authorize]   
    [Route("api/VendasContext")]
    public class VendasContextController : Controller
    {
        private readonly VendasContext _context = new VendasContext();

        //Security Connection String
        //***GetConnectionString
        private static string GetConnectionString(IConfiguration config, string connectionName)
        {
            return config.GetConnectionString(connectionName);
        }

        [EnableQuery()]
        [HttpGet("CLIENTE")]
        [HttpGet("GetClienteNoAssociations")]
        public IEnumerable<CLIENTE> GetClienteNoAssociations(string p1 = "")
        {

            this.HttpContext.AdjustOdataFeature(Modules.ModuleInitializer.Model, "api/VendasContext", "CLIENTE", Modules.ModuleInitializer.ServiceProvider);
            var result = _context.CLIENTE;

            return result;
        }
        
        [HttpGet("GetCliente")]
        public IEnumerable<CLIENTE> GetCliente(string p1 = "")
        {
            var result = _context.CLIENTE;

            return result;
        }

        // POST api/Products
        [HttpPost("CLIENTE")]
        public IActionResult InsertCLIENTE([FromBody]CLIENTE value)
        {
            return null;
        }

        // PUT api/Products/5
        [HttpPut(("CLIENTE"))]
        public IActionResult UpdateCLIENTE([FromBody]CLIENTE value)
        {


            return new NoContentResult();
        }

        // DELETE api/Products/5
        [HttpDelete(("CLIENTE/{id}"))]
        public IActionResult DeleteCLIENTE(int id)
        {


            return new NoContentResult();
        }

    }

}

