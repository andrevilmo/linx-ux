using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;
using System.Data;
using System.Reflection;


namespace Linx.TCS0101.BO.TcsExecutaImportacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsExecutaImportacaoDomainService
	{
        [Invoke()]
        public void ImportRangeFile(string[] pfileCodes, int pUserID)
        {
            try
            {
                LinxBusinessImportFile ImportFile = new LinxBusinessImportFile();
                foreach (var file in pfileCodes)
                {
                    ImportFile.fvImportFile(file, pUserID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
