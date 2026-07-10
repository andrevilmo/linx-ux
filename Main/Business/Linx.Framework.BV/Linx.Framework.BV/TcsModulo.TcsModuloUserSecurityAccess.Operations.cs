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
using Linx;
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.Autorizacao.BM;


namespace Linx.TCS0101.BO.TcsModulo
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsModuloDomainService
    {

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsModulo> GetTcsModuloByUserAccess(Guid uidUsuario, int idModuloGrupo)
        {
            List<TcsModuloAccess> allowedModules = new List<TcsModuloAccess>();

            try
            {
                TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
                TCS_USUARIO_AUTENTICACAO usuario = ds.GetUser(uidUsuario);

                if (usuario.IsNull())
                    return new List<TcsModulo>();

                //TcsPerfil
                List<Int32> tcsPerfil =
                     (from result1 in this.DbContext.TCS_USUARIO_PERFIL
                      let result = result1.TCS_PERFIL
                      where !result.INATIVO && result1.UID_USUARIO == uidUsuario
                      select result.ID_PERFIL).ToList();

                //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                List<TcsModuloAccess> listaModulo =
                    ((from result in this.DbContext.TCS_USUARIO_REGRA_MODULO
                      where result.TCS_USUARIO.UID_USUARIO == uidUsuario
                      select new TcsModuloAccess() { UidModulo = result.UID_MODULO, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 3 }
                    ).ToList().Union
                    (from result in this.DbContext.TCS_PERFIL_REGRA_MODULO
                     where tcsPerfil.Contains(result.TCS_PERFIL.ID_PERFIL)
                     select new TcsModuloAccess() { UidModulo = result.UID_MODULO, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 4 }
                     ).ToList()
                     ).OrderBy(i => i.UidModulo).OrderBy(i => i.Origem).OrderBy(i => i.RegraAcesso).ToList();

                foreach (TcsModuloAccess item in listaModulo)
                {
                    List<TcsModuloAccess> moduloList = allowedModules.Where(i => i.UidModulo == item.UidModulo).ToList();
                    if (moduloList.Count() == 0)
                        allowedModules.Add(new TcsModuloAccess(item.UidModulo, item.RegraAcesso, item.Origem));
                    else
                    {
                        TcsModuloAccess modulo = moduloList.First();

                        if ((modulo.Origem == item.Origem) && (modulo.RegraAcesso < item.RegraAcesso && item.RegraAcesso == 2))
                            modulo.RegraAcesso = item.RegraAcesso;
                    }
                }

                allowedModules = allowedModules.Where(i => i.RegraAcesso != 1).ToList();

                List<Guid> tcsModulodoGrupo =
                    (from result in this.DbContext.TCS_MODULO_DO_GRUPO
                     where result.ID_GRUPO_MODULO == idModuloGrupo
                     select result.UID_MODULO).ToList();

                //TcsModulo - TcsModuloAutorizacao
                var modulos =
                    ((from result in this.DbContext.TCS_MODULO
                      where !result.INATIVO && tcsModulodoGrupo.Contains(result.UID_MODULO)
                      select new { UidModulo = result.UID_MODULO, DescModulo = result.DESC_MODULO }
                     ).ToList().Union
                     (from result in ds.GetTcsModuloAutorizacaoNoAssociations().Where(i => !i.Inativo && tcsModulodoGrupo.Contains(i.UidModulo))
                      select new { UidModulo = result.UidModulo, DescModulo = result.DescModulo }
                     ).ToList()).Distinct().ToList();

                return
                    (from result in modulos
                     join result1 in allowedModules on result.UidModulo equals result1.UidModulo
                     select new TcsModulo() { UidModulo = result.UidModulo, DescModulo = result.DescModulo }).OrderBy(i => i.DescModulo);

            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message);
            }
        }


        [Query(HasSideEffects = true)]
        public IEnumerable<TcsModuloMenu> GetUserTcsModuloMenu(Guid UidModulo)
        {
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();

            var retorno =
                    (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.UidModulo == UidModulo)
                     select new TcsModuloMenu()
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                         OrdemNavegacao = result.OrdemNavegacao,
                         UidModulo = result.UidModulo,
                         UidModuloMenu = result.UidModuloMenu,
                         UidModuloMenuSuperior = result.UidModuloMenuSuperior
                     }).ToList();
            var retorno1 =
                    (from result in this.GetTcsModuloMenuNoAssociations().Where(i => i.UidModulo == UidModulo)
                     select new TcsModuloMenu()
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                         OrdemNavegacao = result.OrdemNavegacao,
                         UidModulo = result.UidModulo,
                         UidModuloMenu = result.UidModuloMenu,
                         UidModuloMenuSuperior = result.UidModuloMenuSuperior
                     }).ToList();
            //).OrderBy(i => i.OrdemNavegacao).ThenBy(i => i.DescModuloMenu);


            return
                (
                    (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.UidModulo == UidModulo)
                     select new TcsModuloMenu()
                      {
                          DescModuloMenu = result.DescModuloMenu,
                          DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                          OrdemNavegacao = result.OrdemNavegacao,
                          UidModulo = result.UidModulo,
                          UidModuloMenu = result.UidModuloMenu,
                          UidModuloMenuSuperior = result.UidModuloMenuSuperior
                      }).ToList().Union
                    (from result in this.GetTcsModuloMenuNoAssociations().Where(i => i.UidModulo == UidModulo)
                     select new TcsModuloMenu()
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                         OrdemNavegacao = result.OrdemNavegacao,
                         UidModulo = result.UidModulo,
                         UidModuloMenu = result.UidModuloMenu,
                         UidModuloMenuSuperior = result.UidModuloMenuSuperior
                     }).ToList()
                 ).OrderBy(i => i.OrdemNavegacao).ThenBy(i => i.DescModuloMenu);
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsModulo> GetTcsModuloByUserAccessJson(Guid uidUsuario, int idModuloGrupo)
        {
            return GetTcsModuloByUserAccess(uidUsuario, idModuloGrupo);
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsModuloMenu> GetUserTcsModuloMenuJson(Guid UidModulo)
        {
            return GetUserTcsModuloMenu(UidModulo);
        }
    }

    public class TcsModuloAccess
    {

        public TcsModuloAccess()
        {

        }

        public TcsModuloAccess(Guid uidModulo, int regraAcesso, int origem)
        {
            UidModulo = uidModulo;
            RegraAcesso = regraAcesso;
            Origem = origem;

            //1 -> Tcs_Usuario_Regra_Transacao
            //2 -> Tcs_Perfil_Regra_Transacao
            //3 -> Tcs_Usuario_Regra_Modulo
            //4 -> Tcs_perfil_Regra_Modulo

        }
        [Key]
        public Guid UidModulo { get; set; }
        public int RegraAcesso { get; set; }
        public int Origem { get; set; }
    }
}
