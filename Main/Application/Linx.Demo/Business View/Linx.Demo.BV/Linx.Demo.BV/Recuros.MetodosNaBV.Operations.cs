using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Demo.BM;
using System.IO;
using System.Web;

namespace Linx.Demo.BV.Recuros
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class RecurosDomainService
    {
        [Invoke(HasSideEffects = true)]
        public void MetodosNaBV(string fName)
        {
           
                FileInfo fInfo = new FileInfo(fName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
                HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.WriteFile(fInfo.FullName);
                fInfo = null;
           

        }
    }
}
