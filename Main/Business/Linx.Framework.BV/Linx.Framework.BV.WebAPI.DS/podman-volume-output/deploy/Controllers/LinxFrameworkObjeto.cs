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


using Linx.Framework.BV.Objeto;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Linx.Business.Tools;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkObjetoController
    {
        [HttpGet()]
        [Route("GetConfiguracaoExportacao")]
        public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacao(bool isExcel, string parentFullName)
        {
            return repository.Context.GetConfiguracaoExportacao(isExcel, parentFullName, UserServiceHelper.GetCurrentUserUid().Value);
        }

        [Route("SaveConfiguracaoExportacao"), System.Web.Http.HttpPost()]
        public void SaveConfiguracaoExportacao(JObject configuracaoExportacao)
        {
            if (configuracaoExportacao.IsNull())
                throw new ArgumentNullException("configuracaoExportacao");

            repository.Context.SaveConfiguracaoExportacao(configuracaoExportacao.ToObject<ConfiguracaoExportacao>(), configuracaoExportacao.ToString(), UserServiceHelper.GetCurrentUserUid().Value);
        }

        [HttpDelete()]
        [Route("DeleteConfiguracaoExportacao")]
        public void DeleteConfiguracaoExportacao(Int64 idConfiguracaoExportacao)
        {
            if (idConfiguracaoExportacao == 0)
                throw new ArgumentNullException("uidConfiguracaoExportacao");
            var uidUsuario = UserServiceHelper.GetCurrentUserUid();

            if (uidUsuario.IsNullOrEmpty() || !repository.Context.CanDeleteConfiguracaoExportacao(idConfiguracaoExportacao, uidUsuario.Value))
                throw new Exception("Exclusão não permitida !");

            repository.Context.DeleteConfiguracaoExportacao(idConfiguracaoExportacao);

        }

        [LinxFrameworkAutorizacaoControllerAuthorize]
        [Route("GetPivotLayouts"), System.Web.Http.HttpGet]
        public IEnumerable<ConfiguracaoExportacao> GetPivotLayouts(string rootNameSpace, string viewName, string pivotName, string pivotDataSource)
        {
            var user = UserServiceHelper.GetCurrentUserId();
            return repository.Context.GetPivotLayouts(rootNameSpace, viewName, pivotName, pivotDataSource, user);
        }

        [Route("GetPivotLayout"), System.Web.Http.HttpGet]
        public ConfiguracaoExportacao GetPivotLayout(Int64 uidObjetoConteudo)
        {
            return repository.Context.GetPivotLayout(uidObjetoConteudo);
        }

        [Route("SavePivotLayout"), System.Web.Http.HttpPost]
        public long SavePivotLayout(JObject configuracaoExportacao)
        {
            if (configuracaoExportacao.IsNull())
                throw new ArgumentNullException("configuracaoExportacao");

            return repository.Context.SavePivotLayout(configuracaoExportacao.ToObject<ConfiguracaoExportacao>(), configuracaoExportacao.ToString(), UserServiceHelper.GetCurrentUserUid().Value);
        }


        [HttpGet()]
        [Route("GetUsersPermissionLayout")]
        public List<object> GetUsersPermissionLayout(long idObjetoConteudo)
        {
            int idGpecon = BusinessUserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
            int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment().GetValueOrDefault();
            bool isMultiGpecon = BusinessUserServiceHelper.IsUserMultiGpecon();

            Usuario.UsuarioDomainService ds = new Usuario.UsuarioDomainService();

            var users = ds.GetTcsUsuario().OrderBy(i => i.NomeUsuario).Select(i => new { i.NomeUsuario, i.IdUsuario, Selected = false, i.IdLinx }).ToList();
            List<long?> usersSelected = repository.Context.GetTcsObjetoPermissao().Where(x => x.IdObjetoConteudo == idObjetoConteudo).Select(i => i.IdUsuario).ToList();

            //se não é MultiGpecon filtra somente os usuários do Gpecon atual
            if (!isMultiGpecon || idLinx != idGpecon) {
                users = users.Where(i => i.IdLinx == idGpecon).ToList();
            }

            var result = new List<object>();

            foreach (var item in users)
            {
                result.Add(new
                {
                    NomeUsuario = item.NomeUsuario,
                    IdUsuario = item.IdUsuario,
                    Selected = usersSelected.Where(x => x == item.IdUsuario).Count() > 0
                });
            }
            return result;
        }

        [HttpGet()]
        [Route("GetProfilesPermissionLayout")]
        public List<object> GetProfilesPermissionLayout(long idObjetoConteudo)
        {
            var result = new List<object>();

            var idUsuario = UserServiceHelper.GetCurrentUserId();
            Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
            var profiles = dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == idUsuario).
                Select(i => new { NomePerfil = i.DescPerfil, IdPerfil = i.IdPerfil }).OrderBy(i => i.NomePerfil).ToList();


            if (idObjetoConteudo == 0)
            {
                foreach (var item in profiles)
                {
                    result.Add(new
                    {
                        NomePerfil = item.NomePerfil,
                        IdPerfil = item.IdPerfil,
                        Selected = false
                    });
                }
            }
            else
            {
                var profilesSelected = repository.Context.GetTcsObjetoPermissao().Where(x => x.IdObjetoConteudo == idObjetoConteudo).ToList();
                foreach (var item in profiles)
                {
                    result.Add(new
                    {
                        NomePerfil = item.NomePerfil,
                        IdPerfil = item.IdPerfil,
                        Selected = profilesSelected.Where(x => x.IdPerfil == item.IdPerfil).Count() > 0 ? true : false
                    });
                }
            }
            return result;
        }

        [Route("GetAllLayoutGenericos")]
        [HttpGet()]
        public IEnumerable<LayoutInfo> GetAllLayoutGenericos(string modulo, string nomeObjeto)
        {
            var user = UserServiceHelper.GetCurrentUserId();
            return repository.Context.GetAllLayoutGenericos(modulo, nomeObjeto, user.Value);
        }

        [Route("GetLayoutGenerico")]
        [HttpGet()]
        public LayoutInfo GetLayoutGenerico(long IdLayout)
        {
            return repository.Context.GetLayoutGenerico(IdLayout);
        }

        [Route("SaveLayoutGenerico")]
        [HttpPost()]
        public LayoutInfo SaveLayoutGenerico(LayoutInfo LayoutInfo)
        {
            var user = UserServiceHelper.GetCurrentUserId();
            return repository.Context.SaveLayoutGenerico(LayoutInfo, user.Value);
        }

        [HttpDelete()]
        [Route("DeleteLayoutGenerico")]
        public void DeleteLayoutGenerico(long IdLayout, string modulo, string nomeObjeto)
        {
            var user = UserServiceHelper.GetCurrentUserId();
            repository.Context.DeleteLayoutGenerico(IdLayout, modulo, nomeObjeto, user.Value);
        }

        [Route("DeleteLayoutPivot")]
        [HttpDelete()]
        public void DeleteLayoutPivot(long IdLayout, Guid uidUsuario)
        {
            if (uidUsuario.IsNullOrEmpty() || !repository.Context.CanDeleteLayoutPivot(IdLayout, uidUsuario))
                throw new Exception("Exclusão não permitida!");

            repository.Context.DeleteLayoutPivot(IdLayout);
        }

        [Route("GetLayoutPadrao")]
        [HttpGet()]
        public LayoutInfo GetLayoutPadrao(string modulo, string nomeObjeto)
        {
            var user = UserServiceHelper.GetCurrentUserId();
            return repository.Context.GetLayoutPadrao(modulo, nomeObjeto, user.Value);
        }
    }
}
