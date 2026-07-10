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
using Linx;

namespace Linx.Report.Access.BV.ReportAccess
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class ReportAccessDomainService
	{
        [Invoke(HasSideEffects = true)]
        public void CleanTelerikReportsCache()
        {
            WebCacheHelper.RemoveWebCache("TelerikReportsList");
        }
    }
}
