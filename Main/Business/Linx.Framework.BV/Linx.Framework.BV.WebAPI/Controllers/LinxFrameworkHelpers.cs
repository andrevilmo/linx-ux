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


namespace Linx.Framework.BV.WebAPI.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkHelpersController
    {
        [HttpGet()]
        [Route("GetMediaHeaders")]
        public Dictionary<string, string> GetMediaHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //uidGrupoAcesso
            headers.Add("uidGrupoAcesso", BusinessUserServiceHelper.GetCurrentAccessGroupId().ToString());
            //uidEmpresa
            headers.Add("uidEmpresa", BusinessUserServiceHelper.GetCurrentCompanyId().ToString());
            //uidGrupoEconomico
            headers.Add("uidGrupoEconomico", BusinessUserServiceHelper.GetCurrentEconomicGroupId().ToString());
            //idAmbiente
            headers.Add("idAmbiente", BusinessUserServiceHelper.GetCurrentEnvironmentId().ToString());
            //uidUsuario
            headers.Add("uidUsuario", BusinessUserServiceHelper.GetCurrentUserUid().ToString());

            return headers;
        }
    }
}
