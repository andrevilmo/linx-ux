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


using Linx.Framework.BV.Rede;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkRedeController
    {
        [Route("GetTbcBandeiraRedeList"), System.Web.Http.HttpGet()]
        public List<TbcBandeiraRede> GetTbcBandeiraRedeList(string cacheHash)
        {
            List<TbcBandeiraRede> tbcBandeiraRede = new List<TbcBandeiraRede>();

            string cacheKey = string.Format("UserBandeiraRede_{0}_{1}", BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault());
            BandeiraRedeCache cache = WebCacheHelper.GetWebCache< BandeiraRedeCache>(cacheKey);

            if (cache.IsNull())
            {
                tbcBandeiraRede = this.repository.Context.GetTbcBandeiraRede().ToList();
                cache = new BandeiraRedeCache() { Hash = Guid.NewGuid().ToString(), UserBandeiraRede = tbcBandeiraRede };
                WebCacheHelper.UpdateWebCache(cacheKey, cache, 720); //Expiração em 30 dias
            }

            if (cacheHash.IsNullOrEmpty() || cacheHash != cache.Hash)
                tbcBandeiraRede = cache.UserBandeiraRede;
            else
                tbcBandeiraRede = null;

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", cache.Hash);

            return tbcBandeiraRede;
        }

        [Route("CleanUserBandeiraRedeCache"), System.Web.Http.HttpGet()]
        public void CleanUserBandeiraRedeCache()
        {
            this.repository.Context.CleanUserBandeiraRedeCache();
        }

        [Route("TbcBandeiraRedeMultiEnvironment"), System.Web.Http.HttpPost()]
        public List<TbcBandeiraRede> TbcBandeiraRedeMultiEnvironment(Modulo.EnvironmentInfo[] environments)
        {
            List<TbcBandeiraRede> bandeiraRedeFull = new List<TbcBandeiraRede>();
            string cacheHash = string.Empty;

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode() == "POSUX")
            {
                var idLoja = environments[0].IdLoja;
                var idTcsAmbiente = environments[0].EnvironmentId;

                if (!idLoja.IsNullOrEmpty())
                {
                    Loja.LojaDomainService dsLoja = new Loja.LojaDomainService();
                    TbcBandeiraRede bandeira = (from result in dsLoja.GetLjvLojaNoAssociations().Where(i => i.IdLoja == idLoja)
                                                select new TbcBandeiraRede()
                                                {
                                                    CodBandeiraRede = result.CodBandeiraRede,
                                                    DescBandeiraRede = result.DescBandeiraRede,
                                                    IdBandeiraRede = result.IdBandeiraRede.Value,
                                                    IdTcsAmbiente = idTcsAmbiente
                                                }).FirstOrDefault();

                    if (!bandeira.IsNullOrEmpty())
                        bandeiraRedeFull.Add(bandeira);
                }
            }
            else
            {

                string ambientes = string.Empty;
                Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
                Guid? economicGroup = BusinessUserServiceHelper.GetCurrentEconomicGroupId();

                foreach (Modulo.EnvironmentInfo item in environments)
                {
                    ambientes = ambientes + (ambientes.IsNullOrEmpty() ? string.Empty : "_") + item.EnvironmentId.ToString();
                    cacheHash = item.Hash.ToString();
                }

                string cacheKey = string.Format("UserBandeiraRede_{0}_{1}", BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault(), ambientes);
                BandeiraRedeCache cache = WebCacheHelper.GetWebCache<BandeiraRedeCache>(cacheKey);

                if (cache.IsNull())
                {
                    foreach (Modulo.EnvironmentInfo item in environments)
                    {
                        Dictionary<string, string> headers = new Dictionary<string, string>();
                        headers.Add("CurrentUser", currentUser.ToString());
                        headers.Add("EconomicGroup", economicGroup.ToString());
                        headers.Add("Environment", item.EnvironmentId.ToString());
                        headers.Add("CurrentCompany", item.CompanyUid.ToString());
                        headers.Add("Application", item.ApplicationUid.ToString());

                        RedeDomainService dsRede = new RedeDomainService(headers);

                        bandeiraRedeFull.AddRange((from result in dsRede.GetTbcBandeiraRedeNoAssociations()
                                                   select new TbcBandeiraRede()
                                                   {
                                                       CodBandeiraRede = result.CodBandeiraRede,
                                                       DataAtualizacao = result.DataAtualizacao,
                                                       DataCadastro = result.DataCadastro,
                                                       DescBandeiraRede = result.DescBandeiraRede,
                                                       IdBandeiraRede = result.IdBandeiraRede,
                                                       Midia = result.Midia,
                                                       IdTcsAmbiente = item.EnvironmentId
                                                   }).ToList());
                    }

                    cache = new BandeiraRedeCache() { Hash = Guid.NewGuid().ToString(), UserBandeiraRede = bandeiraRedeFull };
                    WebCacheHelper.UpdateWebCache(cacheKey, cache, 720); //Expiração em 30 dias
                }

                if (cacheHash.IsNullOrEmpty() || cacheHash != cache.Hash)
                {
                    bandeiraRedeFull = cache.UserBandeiraRede;
                    cacheHash = cache.Hash;
                }
                else
                    bandeiraRedeFull = null;
            }

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", cacheHash);

            return bandeiraRedeFull;
        }

    }
}
