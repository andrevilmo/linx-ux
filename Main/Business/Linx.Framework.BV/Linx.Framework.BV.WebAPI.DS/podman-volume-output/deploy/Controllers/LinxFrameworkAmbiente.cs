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
using Linx.Framework.BV.Ambiente;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkAmbienteController
    {
        [Route("GetAmbienteServicoExcecao"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetAmbienteServicoExcecao()
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return Linx.Tools.LocalServiceBus.BusinessAddresses;
            else
            {
                var idAmbiente = Linx.Business.Tools.UserServiceHelper.GetCurrentEnvironmentId();
                return this.GetTcsAmbienteServicoExcecao().Where(i => i.IdTcsAmbiente == idAmbiente).Select(i => new { i.NomeServico, i.Url }).ToDictionary(i => i.NomeServico, i => i.Url);
            }
        }

        [Route("GetServicoExcecaoMultiEnvironment"), System.Web.Http.HttpPost()]
        public List<ServicoExcecaoInfo> GetServicoExcecaoMultiEnvironment(EnvironmentInfo[] environments)
        {
            return this.repository.Context.GetServicoExcecaoMultiEnvironment(environments);
        }
    }
}
