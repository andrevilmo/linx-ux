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

namespace Linx.Framework.BV.Parametro
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsParametroValorVariacaoGenerica
	{
        public string GetNomeTabela()
        {
            string nomeTabela = string.Empty;

            TabelaAutorizacao.TabelaAutorizacaoDomainService ds = new TabelaAutorizacao.TabelaAutorizacaoDomainService();

            var tcsTabela = (from result in ds.GetTcsTabelaAutorizacaoNoAssociations().Where(i => i.UidTabela == this.UidTabela)
                             select result.NomeTabela
                             ).FirstOrDefault();

            if (!tcsTabela.IsNullOrEmpty())
                nomeTabela = tcsTabela;

            return nomeTabela;
        }
    }
}
