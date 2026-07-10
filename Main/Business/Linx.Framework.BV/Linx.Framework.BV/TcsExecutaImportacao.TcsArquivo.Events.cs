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
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.TCS0101.BO.TcsExecutaImportacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsArquivo
	{
        /// Replace the automatic search method.
        public static IQueryable<TcsArquivo> OnSearchingReplacement(Linx.Framework.ControleSistema.BM.ControleSistemaContext context, string dynQuery, List<ObjectParameter> parameters, List<EntitySearch> entitySearchList)
        {
            return from a in context.TCS_ARQUIVO
                   select new TcsArquivo
                   {
                       IdArquivo = 1,
                       TcsArquivoImportarList = from arq in context.TCS_ARQUIVO
                                                where arq.INATIVO == false
                                                select new TcsArquivoImportar
                                                    {
                                                        IdArquivoFk = 1,
                                                        IdArquivo = arq.ID_ARQUIVO,
                                                        CaminhoArquivo = arq.CAMINHO_ARQUIVO,
                                                        CodArquivo = arq.COD_ARQUIVO,
                                                        DescArquivo = arq.DESC_ARQUIVO,
                                                        LxTipoArquivo = arq.LX_TIPO_ARQUIVO,
                                                        NomeArquivo = arq.NOME_ARQUIVO,
                                                        TcsArquivoLogList = from log in arq.TCS_ARQUIVO_LOG_LISTA
                                                                            select new TcsArquivoLog
                                                                            {
                                                                                DataLog = log.DATA_LOG,
                                                                                DescLog = log.DESC_LOG,
                                                                                IdArquivoFk = log.ID_ARQUIVO_FK,
                                                                                IdArquivoLog = log.ID_ARQUIVO_LOG,
                                                                                LxTipoLog = log.LX_TIPO_LOG
                                                                            }
                                                    }
                   };
        }
    }
}
