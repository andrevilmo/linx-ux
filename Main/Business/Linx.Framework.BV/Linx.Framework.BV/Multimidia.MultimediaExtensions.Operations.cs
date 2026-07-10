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
using Linx.Framework.ControleSistema.BM;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Multimidia
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class MultimidiaDomainService
	{
        [Query(HasSideEffects = true)]
        public IEnumerable<MultimidiaCompact2BO> GetListMultimediaSV(string filter)
        {
            return from r in this.GetMultimidiaCompact2BOByEntitySearch(this.AdjustFilter(filter))
                   select new MultimidiaCompact2BO
                   {
                       Conteudo = null
                       ,
                       DescDocumento = r.DescDocumento
                       ,
                       IdChave = r.IdChave
                       ,
                       IdDocClassificador = r.IdDocClassificador
                       ,
                       LxTipoDocumento = r.LxTipoDocumento
                      ,
                       LxTipoExtensao = r.LxTipoExtensao
                       ,
                       Obs = null
                       ,
                       OrdemApresentacao = r.OrdemApresentacao
                       ,
                       Thumbnail = null
                       ,
                       UidChave = r.UidChave
                       ,
                       UidDocumento = r.UidDocumento
                      ,
                       UidTabela = r.UidTabela
                      ,
                       Url = String.Empty
                      ,
                       XmlMapeamento = null
                      ,
                       DescTabela = null
                       ,
                       NomeTabela = r.NomeTabela


                   }; 
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<MultimidiaCompact2BO> GetListMultimediaThumb(string filter)
        {
            return from r in this.GetMultimidiaCompact2BOByEntitySearch(this.AdjustFilter(filter))
                   select new MultimidiaCompact2BO
                   {
                       Conteudo = null
                        ,
                       DescDocumento = r.DescDocumento
                       ,
                       IdChave = r.IdChave
                       ,
                       IdDocClassificador = r.IdDocClassificador
                       ,
                       LxTipoDocumento = r.LxTipoDocumento
                      ,
                       LxTipoExtensao = r.LxTipoExtensao
                       ,
                       Obs = null
                       ,
                       OrdemApresentacao = r.OrdemApresentacao
                       ,
                       Thumbnail = r.Thumbnail
                       ,
                       UidChave = r.UidChave
                       ,
                       UidDocumento = r.UidDocumento
                      ,
                       UidTabela = r.UidTabela
                       ,
                       Url = r.Url
                      ,
                       XmlMapeamento = null
                       ,
                       DescTabela = null
                       ,
                       NomeTabela = r.NomeTabela


                   };
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<MultimidiaCompact2BO> GetListMultimediaContent(string filter)
        {
            return from r in this.GetMultimidiaCompact2BOByEntitySearch(this.AdjustFilter(filter))
                   select new MultimidiaCompact2BO
                   {
                       Conteudo = r.Conteudo
                       ,
                       DescDocumento = r.DescDocumento
                       ,
                       IdChave = r.IdChave
                       ,
                       IdDocClassificador = r.IdDocClassificador
                        ,
                       LxTipoDocumento = r.LxTipoDocumento
                      ,
                       LxTipoExtensao = r.LxTipoExtensao
                       ,
                       Obs = null
                       ,
                       OrdemApresentacao = r.OrdemApresentacao
                       ,
                       Thumbnail = null
                       ,
                       UidChave = r.UidChave
                       ,
                       UidDocumento = r.UidDocumento
                      ,
                       UidTabela = r.UidTabela
                      ,
                       Url = r.Url
                      ,
                       XmlMapeamento = null
                       ,
                       DescTabela = null
                       ,
                       NomeTabela = r.NomeTabela

                   };
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<MultimidiaCompact2BO> GetListMultimediaComplete(string filter)
        {
            return from r in this.GetMultimidiaCompact2BOByEntitySearch(this.AdjustFilter(filter))
                   select new MultimidiaCompact2BO
                   {
                       Conteudo = r.Conteudo
                       ,
                       DescDocumento = r.DescDocumento
                       ,
                       IdChave = r.IdChave
                       ,
                       IdDocClassificador = r.IdDocClassificador
                       ,
                       LxTipoDocumento = r.LxTipoDocumento
                      ,
                       LxTipoExtensao = r.LxTipoExtensao
                       ,
                       Obs = r.Obs
                       ,
                       OrdemApresentacao = r.OrdemApresentacao
                       ,
                       Thumbnail = r.Thumbnail
                       ,
                       UidChave = r.UidChave
                       ,
                       UidDocumento = r.UidDocumento
                      ,
                       UidTabela = r.UidTabela
                      ,
                       Url = r.Url
                      ,
                       XmlMapeamento = r.XmlMapeamento
                      ,
                       DescTabela = r.DescTabela
                       ,
                       NomeTabela = r.NomeTabela

                   };
        }

        [Ignore()]
        public Guid GetTcsTabelaUid(string tableName)
        {
            TabelaAutorizacao.TabelaAutorizacaoDomainService ds = new TabelaAutorizacao.TabelaAutorizacaoDomainService();
            return
                (from result in ds.GetTcsTabelaAutorizacaoNoAssociations().Where(i => i.NomeTabela == tableName)
                 select result.UidTabela).FirstOrDefault();
        }

        [Invoke(HasSideEffects = true)]
        private string AdjustFilter(string filter)
        {
            List<EntitySearch> lstFilter = SerializationManager<List<EntitySearch>>.StringToObject(filter);

            foreach (EntitySearch entity in lstFilter)
            {
                List<EntitySearchExpression> expressions = entity.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "NomeTabela").ToList();

                foreach (EntitySearchExpression expression in expressions)
                {
                    int fieldPosition = entity.Expressions.IndexOf(expression);
                    Guid uidTabela = this.GetTcsTabelaUid(entity.Expressions[fieldPosition + 2].Value.ToString());

                    if (!uidTabela.IsNull())
                    {
                        entity.Expressions[fieldPosition].Value = "UidTabela";
                        entity.Expressions[fieldPosition + 2].Value = uidTabela;
                    }
                }
            }
            return SerializationManager<List<EntitySearch>>.ObjectToString(lstFilter);
        }
    }
}
