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
using Linx.Framework.BV.UsuarioExterno;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkUsuarioExternoController
    {
        [Route("AddTcsUsuarioExterno"), System.Web.Http.HttpPost()]
        public TcsUsuarioExterno AddTcsUsuarioExterno(TcsUsuarioExterno usuarioExterno)
        {
            this.repository.Context.AddCustomChanges(usuarioExterno, null, ChangeOperation.Insert);
            this.repository.Context.SaveCustomChanges();
            usuarioExterno.RefreshKeys();
            return usuarioExterno;
        }

        [Route("GetUsuarioInfo"), System.Web.Http.HttpGet()]
        public TcsUsuarioExterno GetUsuarioInfo(string idDispositivo, string idExterno)
        {
            return
            (from result in this.repository.Context.GetTcsUsuarioExternoNoAssociations().Where(i => i.IdDispositivo == idDispositivo && i.IdentidadeExterna == idExterno)
             select result).FirstOrDefault();
        }
    }
}
