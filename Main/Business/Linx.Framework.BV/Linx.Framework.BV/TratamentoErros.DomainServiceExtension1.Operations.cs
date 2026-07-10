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
using Linx.Framework.Autorizacao.BM;
using System.IO;

namespace Linx.Framework.BV.TratamentoErros
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TratamentoErrosDomainService
    {
        [Invoke(HasSideEffects = true)]
        public string GetFullPathLog()
        {
            return System.Web.HttpContext.Current.Server.MapPath("/Logs");
        }

        [Invoke(HasSideEffects = true)]
        public bool HasLogFile()
        {
            var logPath = this.GetFullPathLog();
            return Directory.Exists(logPath) && Directory.GetFiles(logPath).Length > 0;
        }

        [Invoke(HasSideEffects = true)]
        public LogFile[] GetAllLogFiles()
        {
            return this.HasLogFile() ?
             Directory.GetFiles(this.GetFullPathLog()).Select(f => new LogFile { FileName = new FileInfo(f).Name }).ToArray()
             : new LogFile[] { };
        }
    }
}
