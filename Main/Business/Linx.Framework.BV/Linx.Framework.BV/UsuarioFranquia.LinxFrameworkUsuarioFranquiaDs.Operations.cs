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
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.UsuarioFranquia
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class UsuarioFranquiaDomainService
    {
        [Invoke(HasSideEffects = true)]
        public Dictionary<string, string> GetHeaders(int? idLinx)
        {
            string strEmpresa;
            string strAmbiente;

            if (idLinx.IsNull())
            {
                strEmpresa = BusinessUserServiceHelper.GetCurrentCompanyId().ToString();
                strAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().ToString();
            }
            else
            {
                Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
                Ambiente.TcsAmbiente tcsAmbiente = dsAmbiente.GetTcsAmbienteNoAssociations().Where(i => i.IdLinx == idLinx).FirstOrDefault();

                strEmpresa = tcsAmbiente.UidEmpresa.ToString();
                strAmbiente = tcsAmbiente.IdTcsAmbiente.ToString();
            }

            return new Dictionary<string, string>
                    {
                        {"EconomicGroup", strEmpresa },
                        {"CurrentCompany", strEmpresa },
                        {"Environment", strAmbiente}
                    };

        }
    }
}
