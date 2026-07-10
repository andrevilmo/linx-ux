using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Linx.Framework.BV.TratamentoErros;
using System.Web;
using System.IO;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkTratamentoErrosController
    {



        [HttpGet()]
        [Route("HasLogFile")]
        [LinxFrameworkTratamentoErrosControllerAuthorize()]
        public bool HasLogFile()
        {
            return this.repository.Context.HasLogFile();
        }



        [HttpGet()]
        [Route("ClearLogFiles")]
        [LinxFrameworkTratamentoErrosControllerAuthorize()]
        public void ClearLogFiles()
        {
            if (this.HasLogFile())
            {
                foreach (var f in Directory.GetFiles(this.repository.Context.GetFullPathLog()))
                {
                    File.Delete(f);
                }
            }
        }

        [HttpGet()]
        [Route("GetLogFile")]
        [LinxFrameworkTratamentoErrosControllerAuthorize()]
        public string GetLogFile(string fileName)
        {
            if (fileName.IsNullOrEmpty())
                throw new ArgumentNullException("fileName");

            var pathFileName = Path.Combine(this.repository.Context.GetFullPathLog(), fileName);

            if (!File.Exists(pathFileName))
                throw new FileNotFoundException();

            return File.ReadAllText(pathFileName);
        }

        [Route("GetAllLogFiles")]
        [HttpGet()]
        [LinxFrameworkTratamentoErrosControllerAuthorize()]
        public string[] GetAllLogFiles()
        {
            return this.repository.Context.GetAllLogFiles().Select(i => i.FileName).ToArray();
        }
    }
}
