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
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Multimidia
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class MultimidiaCompact2BO
    {
        /// Execute after save changes.
        public void OnSavedChanges(MultimidiaDomainService context, ChangeOperation changeOperation)
        {
            AdjustArtigoMultimidia(context, changeOperation);
        }

        /// Execute before save changes.
        public void OnSavingChanges(MultimidiaDomainService context, ChangeOperation changeOperation)
        {
            TabelaAutorizacao.TabelaAutorizacaoDomainService ds = new TabelaAutorizacao.TabelaAutorizacaoDomainService();

            var uidTabela = ds.GetTcsTabelaAutorizacaoNoAssociations().Where(i => i.NomeTabela == this.NomeTabela).Select(t => t.UidTabela).FirstOrDefault();
            if (!uidTabela.IsNull())
                this.UidTabela = uidTabela;

            var dominios = MultimidiaCompact2BO.GetMultimediasDomainValues("LX_TIPO_EXTENSAO");
            if (dominios.Count > 0 && dominios.ContainsKey(this._TipoExtensao.Replace(".", "").ToLower()))
                this.LxTipoExtensao = (byte)dominios[this._TipoExtensao.Replace(".", "").ToLower()];

            if (this.Url.IsNull())
                this.Url = String.Empty;

            if (!this.Conteudo.IsNull() && this.Conteudo.Length > 0)
            {
                this.Thumbnail = Linx.Tools.ImageExtension.ResizeImage(this.Conteudo, 64, 64);
                //this.ChecksumConteudo = Linx.Tools.ImageExtension.CreateChecksum(this.Conteudo);
                //this.ChecksumThumbnail = Linx.Tools.ImageExtension.CreateChecksum(this.Thumbnail);
            }
            //else
            //    this.ChecksumConteudo = this.ChecksumThumbnail = String.Empty;



            if (this.UidTabela == Guid.Empty)
                throw new DomainException("Não foi encontrada a tabela correspondente para o multimídia.".Translate());
        }

        private void AdjustArtigoMultimidia(MultimidiaDomainService context, ChangeOperation changeOperation)
        {
            //Treat replication data for product
            if (this.NomeTabela.InList("PRD_ARTIGO", "PRD_ARTIGO_VARIANTE_VALOR", "PRD_SKU_PRODUTO") && this.IdChave > 0 && changeOperation == ChangeOperation.Update && this.Conteudo != null && this.Conteudo.Length > 0)
            {
                bool hasVariantes, hasSkus, hasArtigo;
                Int64[] variantes, skus;
                int artigo;
                Guid uidTabela;
                MultimidiaCompact2BO newElement;

                TabelaAutorizacao.TabelaAutorizacaoDomainService ctxTabela = new TabelaAutorizacao.TabelaAutorizacaoDomainService();
                var tables = ctxTabela.GetTcsTabelaAutorizacao().Where(e => e.DescTabela == "PRD_ARTIGO" || e.DescTabela == "PRD_ARTIGO_VARIANTE_VALOR" || e.DescTabela == "PRD_SKU_PRODUTO").ToDictionary(e => e.DescTabela, e => e.UidTabela);

                if (tables.Keys.Count != 3)
                    return;

                var edmCtx = context.GetEDM();
                switch (this.NomeTabela)
                {
                    case "PRD_ARTIGO":
                        variantes = edmCtx.PRD_ARTIGO_VARIANTE_VALOR.Where(e => e.ID_ARTIGO == this.IdChave).Select(e => Convert.ToInt64(e.ID_PRD_VARIANTE_VALOR)).ToArray();
                        skus = edmCtx.PRD_SKU_PRODUTO.Where(e => e.ID_ARTIGO == this.IdChave).Select(e => Convert.ToInt64(e.ID_SKU)).ToArray();

                        uidTabela = tables["PRD_ARTIGO_VARIANTE_VALOR"];
                        hasVariantes = variantes.Length == 0 || (variantes.Length > 0 && edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && variantes.Contains(e.ID_CHAVE)).Count() > 0);
                        uidTabela = tables["PRD_SKU_PRODUTO"];
                        hasSkus = skus.Length == 0 || (skus.Length > 0 && edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && skus.Contains(e.ID_CHAVE)).Count() > 0);

                        if (!hasSkus)
                        {
                            foreach (int sku in skus)
                            {
                                newElement = new MultimidiaCompact2BO();
                                newElement.CopyInstanceFrom(this);
                                newElement.UidDocumento = Guid.NewGuid();
                                newElement.UidTabela = tables["PRD_SKU_PRODUTO"];
                                newElement.IdChave = sku;
                                newElement.Conteudo = this.Conteudo;
                                newElement.Thumbnail = this.Thumbnail;
                                context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                            }
                        }

                        if (!hasVariantes)
                        {
                            foreach (int variante in variantes)
                            {
                                newElement = new MultimidiaCompact2BO();
                                newElement.CopyInstanceFrom(this);
                                newElement.UidDocumento = Guid.NewGuid();
                                newElement.UidTabela = tables["PRD_ARTIGO_VARIANTE_VALOR"];
                                newElement.IdChave = variante;
                                newElement.Conteudo = this.Conteudo;
                                newElement.Thumbnail = this.Thumbnail;
                                context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                            }
                        }

                        if (!hasSkus || !hasVariantes)
                            context.SaveCustomChanges();


                        break;
                    case "PRD_ARTIGO_VARIANTE_VALOR":


                        artigo = edmCtx.PRD_ARTIGO_VARIANTE_VALOR.Where(e => e.ID_PRD_VARIANTE_VALOR == this.IdChave).Select(e => e.ID_ARTIGO).First();
                        skus = edmCtx.PRD_SKU_PRODUTO.Where(e => e.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_01 == this.IdChave || e.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_02 == this.IdChave || e.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_03 == this.IdChave || e.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_04 == this.IdChave || e.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_05 == this.IdChave).Select(e => Convert.ToInt64(e.ID_SKU)).ToArray();

                        uidTabela = tables["PRD_ARTIGO"];
                        hasArtigo = edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && e.ID_CHAVE == artigo).Count() > 0;
                        uidTabela = tables["PRD_SKU_PRODUTO"];
                        hasSkus = skus.Length == 0 || (skus.Length > 0 && edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && skus.Contains(e.ID_CHAVE)).Count() > 0);

                        if (!hasSkus)
                        {
                            foreach (int sku in skus)
                            {
                                newElement = new MultimidiaCompact2BO();
                                newElement.CopyInstanceFrom(this);
                                newElement.UidDocumento = Guid.NewGuid();
                                newElement.UidTabela = tables["PRD_SKU_PRODUTO"];
                                newElement.IdChave = sku;
                                newElement.Conteudo = this.Conteudo;
                                newElement.Thumbnail = this.Thumbnail;
                                context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                            }
                        }

                        if (!hasArtigo)
                        {
                            newElement = new MultimidiaCompact2BO();
                            newElement.CopyInstanceFrom(this);
                            newElement.UidDocumento = Guid.NewGuid();
                            newElement.UidTabela = tables["PRD_ARTIGO"];
                            newElement.IdChave = artigo;
                            newElement.Conteudo = this.Conteudo;
                            newElement.Thumbnail = this.Thumbnail;
                            context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                        }

                        if (!hasArtigo || !hasSkus)
                            context.SaveCustomChanges();



                        break;
                    case "PRD_SKU_PRODUTO":


                        artigo = edmCtx.PRD_SKU_PRODUTO.Where(e => e.ID_SKU == this.IdChave).Select(e => e.ID_ARTIGO).First();
                        var prdVariante = edmCtx.PRD_SKU_PRODUTO.Where(e => e.ID_SKU == this.IdChave).Select(e => e.PRD_ARTIGO_VARIANTE).FirstOrDefault();
                        if (prdVariante != null)
                        {
                            List<Int64> variantesList = new List<Int64>();
                            if (!prdVariante.ID_PRD_VARIANTE_VALOR_01.IsNullOrEmpty())
                                variantesList.Add(prdVariante.ID_PRD_VARIANTE_VALOR_01.Value);
                            if (!prdVariante.ID_PRD_VARIANTE_VALOR_02.IsNullOrEmpty())
                                variantesList.Add(prdVariante.ID_PRD_VARIANTE_VALOR_02.Value);
                            if (!prdVariante.ID_PRD_VARIANTE_VALOR_03.IsNullOrEmpty())
                                variantesList.Add(prdVariante.ID_PRD_VARIANTE_VALOR_03.Value);
                            if (!prdVariante.ID_PRD_VARIANTE_VALOR_04.IsNullOrEmpty())
                                variantesList.Add(prdVariante.ID_PRD_VARIANTE_VALOR_04.Value);
                            if (!prdVariante.ID_PRD_VARIANTE_VALOR_05.IsNullOrEmpty())
                                variantesList.Add(prdVariante.ID_PRD_VARIANTE_VALOR_05.Value);
                            variantes = variantesList.ToArray();
                        }
                        else variantes = new Int64[] { };

                        uidTabela = tables["PRD_ARTIGO"];
                        hasArtigo = edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && e.ID_CHAVE == artigo).Count() > 0;
                        uidTabela = tables["PRD_ARTIGO_VARIANTE_VALOR"];

                        hasVariantes = variantes.Length == 0 || (variantes.Length > 0 && edmCtx.DOC_MULTIMIDIA_TABELA.Where(e => e.UID_TABELA == uidTabela && variantes.Contains(e.ID_CHAVE)).Count() > 0);

                        if (!hasVariantes)
                        {
                            foreach (int variante in variantes)
                            {
                                newElement = new MultimidiaCompact2BO();
                                newElement.CopyInstanceFrom(this);
                                newElement.UidDocumento = Guid.NewGuid();
                                newElement.UidTabela = tables["PRD_ARTIGO_VARIANTE_VALOR"];
                                newElement.IdChave = variante;
                                newElement.Conteudo = this.Conteudo;
                                newElement.Thumbnail = this.Thumbnail;
                                context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                            }

                        }

                        if (!hasArtigo)
                        {
                            newElement = new MultimidiaCompact2BO();
                            newElement.CopyInstanceFrom(this);
                            newElement.UidDocumento = Guid.NewGuid();
                            newElement.UidTabela = tables["PRD_ARTIGO"];
                            newElement.IdChave = artigo;
                            newElement.Conteudo = this.Conteudo;
                            newElement.Thumbnail = this.Thumbnail;
                            context.AddCustomChanges(newElement, null, ChangeOperation.Insert);
                        }

                        if (!hasArtigo || !hasVariantes)
                            context.SaveCustomChanges();

                        break;
                    default:
                        break;
                }

            }
        }
    }
}
