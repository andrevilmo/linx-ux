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
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using Linx.Framework.ControleSistema.BM;
using Linx.Framework.Domains.BM.Domains;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Multimidia
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class MultimidiaCompact2BO
	{
        private string GetDocumentDomainString(byte p)
        {
            var result = GetDomainDocumentTypes().Where(e => e.Value == p);
            if (result == null || result.Count() == 0)
                return String.Empty;
            else
                return result.First().Key.ToLower();
        }

        private static Dictionary<String, Int16> GetDomainDocumentTypes()
        {
            return GetMultimediasDomainValues("LX_TIPO_DOCUMENTO");
        }

        private string GetExtensionDomainString(byte p)
        {
            var result = GetDomainExtensionTypes().Where(e => e.Value == p);
            if (result == null || result.Count() == 0)
                return String.Empty;
            else
                return result.First().Key.ToLower(); 
        }

        private static Dictionary<String, Int16> GetDomainExtensionTypes()
        {
            return GetMultimediasDomainValues("LX_TIPO_EXTENSAO");
        }

        public static Dictionary<String, Int16> GetMultimediasDomainValues(string p)
        {
            Dictionary<string, Int16> domains = new Dictionary<string, short>();

            foreach (var element in (p == "LX_TIPO_DOCUMENTO" ? TipoDocumento.GetValues() : Linx.Framework.Domains.BM.Domains.TipoExtensao.GetValues()))
            {
                domains.Add(element.Value.ToLower(), Int16.Parse(element.Key));
            }

            return domains;
        }

        public string GetDescTabela()
        {
            if (!this._DescTabela.IsNullOrEmpty() || this.UidTabela.IsNullOrEmpty())
                return this._DescTabela;

            return this.Values(1);
        }

        public string GetNomeTabela()
        {
            if (!this._NomeTabela.IsNullOrEmpty() || this.UidTabela.IsNullOrEmpty())
                return this._NomeTabela;

            return this.Values(2); 
        }

        private string Values(int valueType)
        {
            if (this._DescTabela.IsNullOrEmpty() && !this.UidTabela.IsNullOrEmpty())
            {
                TabelaAutorizacao.TabelaAutorizacaoDomainService ds = new TabelaAutorizacao.TabelaAutorizacaoDomainService();
                var query = (from result in ds.GetTcsTabelaAutorizacaoNoAssociations().Where(i => i.UidTabela == this.UidTabela)
                             select new { DescTabela = result.DescTabela, NomeTabela = result.NomeTabela }).FirstOrDefault();

                if (!query.IsNull())
                {
                    this._DescTabela = query.DescTabela;
                    this._NomeTabela = query.NomeTabela;
                }

            }

            string value = string.Empty;
            switch (valueType)
            {
                case 1:
                    value = this._DescTabela;
                    break;

                case 2:
                    value = this._NomeTabela;
                    break;
            }
            return value;
        }

    }
}
