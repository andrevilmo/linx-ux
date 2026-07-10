using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using System.Web.Security;

namespace Linx.TCS0101.BO.TcsAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacao
    {
        /// Execute on transaction context ending.
        public static void OnTransactedContextChanges(TcsAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {

           //Update NomeUsuario
            entities.Where(i => i.Entity is TcsUsuarioAutenticacao && i.Operation == DomainOperation.Update).Select(i => i.Entity as TcsUsuarioAutenticacao).ToList().ForEach(entity =>
                {
                    TcsUsuarioAutenticacao oldValue = context.GetChangeSet().GetOriginal<TcsUsuarioAutenticacao>(entity);
                    if (!oldValue.IsNullOrEmpty() && entity.NomeUsuario != oldValue.NomeUsuario)
                    {
                        entity.TcsUsuarioAcessoList.Select(i => i.UidGrupoAcesso).Distinct().ToList().ForEach(accessGroup =>
                            {
                                Dictionary<string, string> headers = new Dictionary<string, string>();
                                headers.Add("AccessGroup", accessGroup.ToString());
                                TcsUsuario.TcsUsuarioDomainService usuarioCtx = new TcsUsuario.TcsUsuarioDomainService(headers);
                                TcsUsuario.TcsUsuario original = new TcsUsuario.TcsUsuario() { UidUsuario = entity.UidUsuario, NomeUsuario = oldValue.NomeUsuario };
                                TcsUsuario.TcsUsuario changed = new TcsUsuario.TcsUsuario() { UidUsuario = entity.UidUsuario, NomeUsuario = entity.NomeUsuario };
                                usuarioCtx.AddCustomChanges(changed, original, ChangeOperation.Update);
                                usuarioCtx.SaveCustomChanges();
                            });
                    }
                });

            //Insert TcsUsuarioAcesso
            List<TcsUsuarioAutenticacao> parentEntities = entities.Where(i => i.Entity is TcsUsuarioAutenticacao).Select(i => i.Entity as TcsUsuarioAutenticacao).ToList();
            parentEntities.ForEach(parent =>
                {
                    parent.TcsUsuarioAcessoList.Where(i => context.GetChangeSet().GetChangeOperation(i) == ChangeOperation.Insert).Select(i => i.UidGrupoAcesso).Distinct().ToList().ForEach(item =>
                        {
                            Dictionary<string, string> headers = new Dictionary<string, string>();
                            headers.Add("AccessGroup", item.ToString());
                            TcsUsuario.TcsUsuarioDomainService usuarioCtx = new TcsUsuario.TcsUsuarioDomainService(headers);
                            usuarioCtx.AddCustomChanges(new TcsUsuario.TcsUsuario() { UidUsuario = parent.UidUsuario, NomeUsuario = parent.NomeUsuario }, null, ChangeOperation.Insert);
                            usuarioCtx.SaveCustomChanges();
                        });
                });

            //AspNetUser
            entities.Where(i => i.Entity is TcsUsuarioAutenticacao && i.Operation != DomainOperation.None).ToList().ForEach(entity =>
                {
                    TcsUsuarioAutenticacao usuarioAutenticacao = entity.Entity as TcsUsuarioAutenticacao;

                    bool isWindowsAuthentication = usuarioAutenticacao.NomeAutenticacao.Contains(@"\");
                    bool userExists = !isWindowsAuthentication && Membership.GetUser(usuarioAutenticacao.NomeAutenticacao) != null;
                    //TcsAutorizacao.TcsAutorizacaoDomainService autorizacao = new TcsAutorizacao.TcsAutorizacaoDomainService();

                    //Delete
                    if (entity.Operation == DomainOperation.Delete)
                    {
                        //TcsUsuarioAcesso == 0 && AspNetUser exists.
                        //Delete AspNetUser
                        if (context.GetTcsUsuarioAcesso().Where(i => i.UidUsuario == usuarioAutenticacao.UidUsuario).Count() == 0 && userExists)
                        {
                            if (!Membership.DeleteUser(usuarioAutenticacao.NomeAutenticacao))
                                throw new DomainException("Erro ao excluir Usuário ASP Net Security".Translate());
                        }
                    }
                    else // Insert or Update
                    {
                        //Add AspNetUser
                        if (!userExists && !isWindowsAuthentication)
                            usuarioAutenticacao.AddAspNetUser();
                    }
                });
        }

        /// Execute on transaction context starting.
        public static void OnTransactingContextChanges(TcsAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            //Delete TcsUsuarioAutenticacao
            entities.Where(i => i.Entity is TcsUsuarioAutenticacao && i.Operation == DomainOperation.Delete).Select(i => i.Entity as TcsUsuarioAutenticacao).ToList().ForEach(entity =>
            {
                context.GetTcsUsuarioAcessoNoAssociations().Where(i => i.UidUsuario == entity.UidUsuario).Select(i => i.UidGrupoAcesso).Distinct().ToList().ForEach(accessGroup =>
                {
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("AccessGroup", accessGroup.ToString());
                    TcsUsuario.TcsUsuarioDomainService usuarioCtx = new TcsUsuario.TcsUsuarioDomainService(headers);
                    TcsUsuario.TcsUsuario tcsUsuario = usuarioCtx.GetTcsUsuarioNoAssociations().Where(i => i.UidUsuario == entity.UidUsuario).FirstOrDefault();

                    if (!tcsUsuario.IsNull())
                    {
                        try
                        {
                            usuarioCtx.AddCustomChanges(tcsUsuario, null, ChangeOperation.Delete);
                            usuarioCtx.SaveCustomChanges();
                        }
                        catch 
                        {
                            throw new DomainException("Usuário possui movimentações e não pode ser excluído, deve ser inativado.".Translate());
                        }
                    }
                });
            });
        }
    }
}
