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
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Utilitarios
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class UtilitariosDomainService
	{
        [Invoke(HasSideEffects = true)]
        public bool CleanCache()
        {
            Utils.CleanCache();
            return true;
        }

        [Invoke(HasSideEffects = true)]
        public bool CleanUserModulesCache(Guid userUid)
        {
            if (userUid.IsNullOrEmpty())
                Utils.RemoveModulesFromCache();
            else
            {
                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();

                var ambientes = (from result in ds.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.UidUsuario == userUid)
                                 select result.IdTcsAmbiente).Distinct().ToList();

                ambientes.ForEach(idAmbiente =>
                {
                    Utils.RemoveUserModulesFromCache(userUid, idAmbiente);
                });
            }

            return true;
        }

        [Invoke(HasSideEffects = true)]
        public bool CleanUserBandeiraRedeCache(Guid userUid)
        {
            if (userUid.IsNullOrEmpty())
            {
                Utils.RemoveBandeiraRedeFromCache();
                Utils.RemoveBrandInfoFromCache(null);
                Utils.RemoveGpeconInfoFromCache(null);
            }
            else
            {
                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                var ambientes = (from result in ds.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.UidUsuario == userUid)
                                 select result.IdTcsAmbiente).Distinct().ToList();

                ambientes.ForEach(idAmbiente =>
                {
                    Utils.RemoveUserBandeiraRedeFromCache(userUid, idAmbiente);
                });
            }

            return true;
        }

        [Invoke(HasSideEffects = true)]
        public bool CleanConnectionsCache()
        {
            Utils.RemoveConnectionsFromCache();
            return true;
        }

        [Invoke(HasSideEffects = true)]
        public bool CleanTelerikReportsCache()
        {
            Utils.RemoveTelerikReportsFromCache();
            return true;
        }

        [Invoke(HasSideEffects = true)]
        public bool CleanUserInfoCache(Guid userUid)
        {
            Utils.RemoveUserInfoFromCache(userUid);
            Utils.RemoveBrandInfoFromCache(userUid);
            Utils.RemoveGpeconInfoFromCache(userUid);
            return true;
        }
    }
}
