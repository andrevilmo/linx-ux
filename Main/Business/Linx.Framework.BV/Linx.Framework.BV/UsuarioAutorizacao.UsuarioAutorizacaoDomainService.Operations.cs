using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Web.Security;

namespace Linx.Framework.BV.UsuarioAutorizacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class UsuarioAutorizacaoDomainService
    {
        [Invoke(HasSideEffects = true)]
        public bool CheckLoginAvailability(string loginName)
        {
            bool aspNetExistence = (Membership.GetUser(loginName) != null);
            bool userExistence = GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == loginName).Count() > 0;

            return (!aspNetExistence && !userExistence);
        }

        [Invoke(HasSideEffects = true)]
        public string[] GetAvailableLogins(string userName, string companyName)
        {
            string[] availableLogins = new string[3];
            string[] splittedName = userName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //first name
            string loginName = splittedName[0];
            availableLogins[0] = getLoginName(loginName);

            //first + last name
            loginName = splittedName[0] + "." +  splittedName[splittedName.Count() - 1];
            availableLogins[1] = getLoginName(loginName);

            //first name + first company name
            string[] splittedCompanyName = companyName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            loginName = splittedName[0] + "." + splittedCompanyName[0];
            availableLogins[2] = getLoginName(loginName);

            return availableLogins;
        }

        private string getLoginName(string loginName)
        {
            if (!CheckLoginAvailability(loginName))
            {
                int counter = 1;
                while (true)
                {
                    loginName = loginName + counter.ToString();
                    if (CheckLoginAvailability(loginName))
                    {
                        break;
                    }
                }
            }
            return loginName;
        }

        /// <summary>
        /// Scopes export / data-source queries to the logged economic group (EconomicGroup header).
        /// Used by "Nova Configuração Exportação e Fonte de Dados" (Excel, OData, report, EntitySearch).
        /// </summary>
        private void ApplyCurrentGpeconFilter(ref IQueryable<TcsUsuarioAutenticacao> query)
        {
            int idGpecon = CurrentIdGpEcon();
            if (idGpecon <= 0) return;

            var multiGpeconUserIds = this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON
                .Where(g => g.TCS_EMPRESA_AUTENTICACAO.ID_LINX == idGpecon)
                .Select(g => g.TCS_USUARIO_AUTENTICACAO.ID_USUARIO);

            query = query.Where(e => e.IdLinx == idGpecon || multiGpeconUserIds.Contains(e.IdUsuario));
        }

        private void ApplyCurrentGpeconFilter(ref IQueryable<TcsUsuarioAcessoParentComposition> query)
        {
            int idGpecon = CurrentIdGpEcon();
            if (idGpecon <= 0) return;

            var multiGpeconUserIds = this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON
                .Where(g => g.TCS_EMPRESA_AUTENTICACAO.ID_LINX == idGpecon)
                .Select(g => g.TCS_USUARIO_AUTENTICACAO.ID_USUARIO);

            // Users of the logged economic group (home company or multi-gpecon link)
            query = query.Where(e => e.IdLinx == idGpecon || multiGpeconUserIds.Contains(e.IdUsuario));
        }

        private void ApplyCurrentGpeconFilter(ref IQueryable<TcsIdentidadeExternaParentComposition> query)
        {
            int idGpecon = CurrentIdGpEcon();
            if (idGpecon <= 0) return;

            var multiGpeconUserIds = this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON
                .Where(g => g.TCS_EMPRESA_AUTENTICACAO.ID_LINX == idGpecon)
                .Select(g => g.TCS_USUARIO_AUTENTICACAO.ID_USUARIO);

            query = query.Where(e => e.IdLinx == idGpecon || multiGpeconUserIds.Contains(e.IdUsuario));
        }

        private void ApplyCurrentGpeconFilter(ref IQueryable<TcsUsuarioGpeconParentComposition> query)
        {
            int idGpecon = CurrentIdGpEcon();
            if (idGpecon <= 0) return;

            // IdLinx on this composition is the linked economic group
            query = query.Where(e => e.IdLinx == idGpecon);
        }

        // Senha/Confirmação are UI-only; clients may still send them in jEntitySearch.
        // EntitySearch.AdjustExcludedFilters alone is not enough because Get* also applies raw jEntitySearch via JExpressionToEntitySql.
        private static readonly Regex UiOnlyPasswordFilterRegex = new Regex(
            @"(^|;)ConfirmacaoUsuario1?#[^;}]*",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private string StripUiOnlyPasswordFilters(string jEntitySearch)
        {
            if (jEntitySearch.IsNullOrEmpty()) return jEntitySearch;

            var cleaned = UiOnlyPasswordFilterRegex.Replace(jEntitySearch, string.Empty);
            cleaned = cleaned.Replace("{;", "{").Replace(";;", ";");
            while (cleaned.Contains(";}"))
                cleaned = cleaned.Replace(";}", "}");
            return cleaned;
        }

    }
}
