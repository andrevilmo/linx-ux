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

namespace Linx.Framework.BV.Ambiente
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class AmbienteDomainService
    {
        [Invoke(HasSideEffects = true)]
        public List<ServicoExcecaoInfo> GetServicoExcecaoMultiEnvironment(EnvironmentInfo[] environments)
        {
            List<ServicoExcecaoInfo> info = new List<ServicoExcecaoInfo>();
            string cacheHash = string.Empty;

            if (LocalServiceBus.Enabled)
            {
                cacheHash = Guid.NewGuid().ToString();

                foreach (var item in LocalServiceBus.BusinessAddresses)
                {
                    info.Add(new ServicoExcecaoInfo() { IdTcsAmbiente = LocalServiceBus.Environment, Servico = item.Key, Url = item.Value });
                }
            }
            else
            {
                string ambientes = string.Empty;

                foreach (EnvironmentInfo item in environments)
                {
                    ambientes = ambientes + (ambientes.IsNullOrEmpty() ? string.Empty : "_") + item.EnvironmentId.ToString();
                    cacheHash = item.Hash.ToString();
                }

                string cacheKey = string.Format("EnvironmentAlternativeServices_{0}", ambientes);
                AmbienteServicoInfo cache = WebCacheHelper.GetWebCache<AmbienteServicoInfo>(cacheKey);

                if (cache.IsNull())
                {
                    foreach (EnvironmentInfo item in environments)
                    {
                        info.AddRange(this.GetTcsAmbienteServicoExcecaoNoAssociations().Where(i => i.IdTcsAmbiente == item.EnvironmentId).Select(i => new ServicoExcecaoInfo() { IdTcsAmbiente = i.IdTcsAmbiente, Servico = i.NomeServico, Url = i.Url }).ToList());
                    }

                    cache = new AmbienteServicoInfo() { Hash = Guid.NewGuid().ToString(), Servicos = info };
                    WebCacheHelper.UpdateWebCache(cacheKey, cache, 720); //Expiração em 30 dias
                }

                if (cacheHash.IsNullOrEmpty() || cacheHash != cache.Hash)
                    info = cache.Servicos;
                else
                    info = null;

                cacheHash = cache.Hash;
            }

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", cacheHash);

            return info;

        }
   }
}
