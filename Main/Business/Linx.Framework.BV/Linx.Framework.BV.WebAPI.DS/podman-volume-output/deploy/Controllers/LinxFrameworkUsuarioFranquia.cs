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
using Linx.Framework.BV.UsuarioFranquia;
using System.ServiceModel.DomainServices.Server;
using System.Transactions;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkUsuarioFranquiaController
    {
        [Route("UsuarioPerfilSync")]
        [HttpPost()]
        public void UsuarioPerfilSync(UsuarioPerfilInfo usuarioPerfil)
        {
            try
            {
                Dictionary<string, string> headers = repository.Context.GetHeaders(usuarioPerfil.IdLinx);
                UsuarioFranquiaDomainService ds = new UsuarioFranquiaDomainService(headers);

                List<Int64> perfilLst = usuarioPerfil.PerfilList.Select(i => i.IdPerfil).Distinct().ToList();
                List<TcsUsuarioPerfil> perfilRemover = ds.GetTcsUsuarioPerfilNoAssociations().Where(i => i.IdUsuario == usuarioPerfil.IdUsuario && !perfilLst.Contains(i.IdPerfil)).ToList();

                using (TransactionScope transaction = new TransactionScope())
                {

                    if (perfilRemover.Count > 0)
                    {
                        foreach (TcsUsuarioPerfil perfilItem in perfilRemover)
                        {
                            ds.AddCustomChanges(perfilItem, null, ChangeOperation.Delete);
                        }
                        ds.SaveCustomChanges();
                    }

                    if (usuarioPerfil.PerfilList.Count > 0)
                    {
                        foreach (TcsUsuarioPerfil item in usuarioPerfil.PerfilList)
                        {
                            ds.AddCustomChanges(item, null, ChangeOperation.Insert);
                        }
                        ds.SaveCustomChanges();
                    }

                    transaction.Complete();
                }
            }
            catch (Exception oException)
            {
                throw new Exception(oException.Message);
            }
        }
    }
}
