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
using Linx.Framework.BV.Multimidia;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using Linx.Framework.BV.Domains;
using System.Text.RegularExpressions;
using System.ServiceModel.DomainServices.Server;

using Linx.Framework.BV.MultimidiaAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkMultimidiaController
    {
        [Route("GetMultimedia"), System.Web.Http.HttpGet()]
        public List<DocMultimidiaInfo> GetMultimedia(string nomeTabela, int? idChave, Guid? uidChave, byte? tipoDocumento, Guid? uidUsuario)
        {
            return repository.Context.GetMultimedia(nomeTabela, idChave, uidChave, tipoDocumento, uidUsuario, null);
        }

        [Route("GetMedia"), System.Web.Http.HttpGet()]
        public HttpResponseMessage GetMedia(Guid uidDocumento, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, Guid? uidUsuario)
        {
            return GetMedia(uidDocumento, uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, false, uidUsuario);
        }

        [Route("GetMediaThumbnail"), System.Web.Http.HttpGet()]
        public HttpResponseMessage GetMediaThumbnail(Guid uidDocumento, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, Guid? uidUsuario)
        {
            return GetMedia(uidDocumento, uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, true, uidUsuario);
        }

        private HttpResponseMessage GetMedia(Guid uidDocumento, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, bool isThumbnail, Guid? uidUsuario)
        {
            Dictionary<string, string> headers = UpdateHeaders(uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, uidUsuario);

            MultimidiaDomainService ds = new MultimidiaDomainService(headers);

            var localMedia = (from result in ds.GetDocMultimidiaNoAssociations().Where(i => i.UidDocumento == uidDocumento)
                              select result).FirstOrDefault();
            if (!localMedia.IsNullOrEmpty())
                return MediaResponseContent(localMedia.Url, localMedia.Conteudo, localMedia.Thumbnail, localMedia.TipoConteudoHttp, localMedia.LxTipoExtensao, localMedia.LxTipoMidia, localMedia.NomeArquivo, uidDocumento, isThumbnail, headers, false);
            else
            {
                MultimidiaAutorizacaoDomainService dsAutorizacao = new MultimidiaAutorizacaoDomainService();
                var media = (from result in dsAutorizacao.GetDocMultimidiaAutorizacaoNoAssociations().Where(i => i.UidDocumento == uidDocumento)
                             select result).FirstOrDefault();

                if (media.IsNullOrEmpty())
                    return EmptyMediaResponseContent();
                else
                    return MediaResponseContent(media.Url, media.Conteudo, media.Thumbnail, media.TipoConteudoHttp, media.LxTipoExtensao, media.LxTipoMidia, media.NomeArquivo, uidDocumento, isThumbnail, headers, true);
            }
        }

        [Route("GetMediaByKey"), System.Web.Http.HttpGet()]
        public HttpResponseMessage GetMediaByKey(string nomeTabela, int? idChave, Guid? uidChave, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, Guid? uidUsuario)
        {
            return GetMediabyKey(nomeTabela, idChave, uidChave, uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, false, uidUsuario);
        }

        [Route("GetMediaThumbnailByKey"), System.Web.Http.HttpGet()]
        public HttpResponseMessage GetMediaThumbnailByKey(string nomeTabela, int? idChave, Guid? uidChave, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, Guid? uidUsuario)
        {
            return GetMediabyKey(nomeTabela, idChave, uidChave, uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, true, uidUsuario);
        }

        [Route("UploadMedia"), System.Web.Http.HttpPost()]
        public DocMultimidiaInfo UploadMedia(DocMultimidiaUpload uploadedMedia)
        {
            return this.repository.Context.UploadMedia(uploadedMedia);
        }

        [Route("DeleteMedia"), System.Web.Http.HttpDelete()]
        public void DeleteMedia(Guid uidDocumento)
        {
            DocMultimidia multimidia = (from result in this.repository.Context.GetDocMultimidiaNoAssociations().Where(i => i.UidDocumento == uidDocumento)
                                        select result).FirstOrDefault();

            if (!multimidia.IsNullOrEmpty())
            {
                this.repository.Context.AddCustomChanges(multimidia, null, ChangeOperation.Delete);
                this.repository.Context.SaveCustomChanges();
            }
            else
            {
                MultimidiaAutorizacaoDomainService ds = new MultimidiaAutorizacaoDomainService();
                DocMultimidiaAutorizacao midia = (from result in ds.GetDocMultimidiaAutorizacaoNoAssociations().Where(i => i.UidDocumento == uidDocumento)
                                                  select result).FirstOrDefault();

                if (!midia.IsNullOrEmpty())
                {
                    ds.AddCustomChanges(midia, null, ChangeOperation.Delete);
                    ds.SaveCustomChanges();
                }
                else
                    throw new DomainException("Multimidia não encontrada !".Translate());
            }
        }

        [Route("SyncMedia"), System.Web.Http.HttpPost()]
        public void SyncMedia(DocTabelaSync mediaToSync)
        {
            repository.Context.SyncMedia(mediaToSync);
        }

        private Dictionary<string, string> UpdateHeaders(Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, Guid? uidUsuario)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            if (!uidGrupoAcesso.IsNull())
            {
                headers.Add("AccessGroup", uidGrupoAcesso.ToString());
            }

            if (!uidEmpresa.IsNull())
            {
                headers.Add("CurrentCompany", uidEmpresa.ToString());
            }

            if (!uidGrupoEconomico.IsNull())
            {
                headers.Add("EconomicGroup", uidGrupoEconomico.ToString());
            }

            if (!idAmbiente.IsNull())
            {
                headers.Add("Environment", idAmbiente.ToString());
            }

            if (!uidUsuario.IsNull())
            {
                headers.Add("CurrentUser", uidUsuario.ToString());
            }

            if (headers.Count() == 0)
                return null;

            return headers;
        }

        private HttpResponseMessage GetMediabyKey(string nomeTabela, int? idChave, Guid? uidChave, Guid? uidGrupoAcesso, Guid? uidEmpresa, Guid? uidGrupoEconomico, int? idAmbiente, bool isThumbnail, Guid? uidUsuario)
        {
            Dictionary<string, string> headers = UpdateHeaders(uidGrupoAcesso, uidEmpresa, uidGrupoEconomico, idAmbiente, uidUsuario);

            var tcsTabelaInfo = this.repository.Context.GetTcsTabelaInfo(nomeTabela).Split(new string[] { "||" }, StringSplitOptions.None);
            Guid uidTabela = Guid.Parse(tcsTabelaInfo[0]);

            EntitySearch search = new EntitySearch();
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));

            if (idChave.IsNullOrEmpty())
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidChave"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidChave));
            }
            else
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdChave"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idChave));
            }

            if (Convert.ToBoolean(tcsTabelaInfo[1]))
            {
                MultimidiaAutorizacaoDomainService dsAutorizacao = new MultimidiaAutorizacaoDomainService();
                var media = (from result in dsAutorizacao.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { search }))
                             select result).OrderBy(i => i.OrdemApresentacao).FirstOrDefault();

                if (media.IsNullOrEmpty())
                    return EmptyMediaResponseContent();

                return MediaResponseContent(media.Url, media.Conteudo, media.Thumbnail, media.TipoConteudoHttp, media.LxTipoExtensao, media.LxTipoMidia, media.NomeArquivo, media.UidDocumento, isThumbnail, headers, false);
            }
            else
            {
                MultimidiaDomainService ds = new MultimidiaDomainService(headers);
                var localMedia = (from result in ds.GetDocMultimidiaTabelaByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { search }))
                                  select result).OrderBy(i => i.OrdemApresentacao).FirstOrDefault();

                if (localMedia.IsNullOrEmpty())
                    return EmptyMediaResponseContent();

                return MediaResponseContent(localMedia.Url, localMedia.Conteudo, localMedia.Thumbnail, localMedia.TipoConteudoHttp, localMedia.LxTipoExtensao, localMedia.LxTipoMidia, localMedia.NomeArquivo, localMedia.UidDocumento, isThumbnail, headers, true);
            }
        }

        private HttpResponseMessage EmptyMediaResponseContent()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            httpResponseMessage.StatusCode = HttpStatusCode.Redirect;
            httpResponseMessage.Headers.Location = new Uri(Utils.GetUrl() + @"image/no-image.png");
            return httpResponseMessage;
        }

        private HttpResponseMessage MediaResponseContent(string url, byte[] conteudo, byte[] thumbnail, string tipoConteudoHttp, byte lxTipoExtensao, byte lxTipoMidia, string nomeArquivo, Guid uidDocumento, bool isThumbnail, Dictionary<string, string> headers, bool isAutorizacao)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };

            if (url.IsNullOrEmpty() && conteudo.IsNullOrEmpty())
            {
                return EmptyMediaResponseContent();
            }

            if (isThumbnail)
                return MediaThumbnailResponse(httpResponseMessage, url, conteudo, thumbnail, tipoConteudoHttp, lxTipoExtensao, lxTipoMidia, nomeArquivo, uidDocumento, headers, isAutorizacao);
            else
                return MediaResponse(httpResponseMessage, url, conteudo, tipoConteudoHttp, lxTipoExtensao);
        }

        private HttpResponseMessage MediaResponse(HttpResponseMessage httpResponseMessage, string url, byte[] conteudo, string tipoConteudoHttp, byte lxTipoExtensao)
        {
            if (url.IsNullOrEmpty())
            {
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                httpResponseMessage.Content = new StreamContent(new MemoryStream(conteudo));
                string contentType = tipoConteudoHttp.IsNullOrEmpty() ? (lxTipoExtensao == 4 ? "video" : "image") + "/" + (TipoExtensao.GetValues()[lxTipoExtensao.ToString()]) : tipoConteudoHttp;
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }
            else
            {
                httpResponseMessage.StatusCode = HttpStatusCode.Redirect;
                httpResponseMessage.Headers.Location = new Uri(url);
            }
            return httpResponseMessage;
        }

        private HttpResponseMessage MediaThumbnailResponse(HttpResponseMessage httpResponseMessage, string url, byte[] conteudo, byte[] thumbnail, string tipoConteudoHttp, byte lxTipoExtensao, byte lxTipoMidia, string nomeArquivo, Guid uidDocumento, Dictionary<string, string> headers, bool isAutorizacao)
        {

            if (lxTipoMidia != 1)
            {
                httpResponseMessage.StatusCode = HttpStatusCode.Redirect;
                httpResponseMessage.Headers.Location = new Uri(Utils.GetUrl() + @"image/document.png");
                return httpResponseMessage;
            }

            byte[] content = null;

            if (thumbnail.IsNullOrEmpty())
            {
                if (!conteudo.IsNullOrEmpty())
                {
                    content = Utils.CreateThumbnail(conteudo, nomeArquivo);

                    try
                    {
                        if (isAutorizacao)
                        {
                            DocMultimidiaAutorizacao docMultimidiaA = new DocMultimidiaAutorizacao() { UidDocumento = uidDocumento, Thumbnail = content };
                            MultimidiaAutorizacaoDomainService dsAutorizacao = new MultimidiaAutorizacaoDomainService();
                            dsAutorizacao.AddCustomChanges(docMultimidiaA, null, ChangeOperation.Insert);
                            dsAutorizacao.SaveCustomChanges();
                        }
                        else
                        {
                            DocMultimidia docMultimidia = new DocMultimidia() { UidDocumento = uidDocumento, Thumbnail = content };
                            MultimidiaDomainService ds = new MultimidiaDomainService(headers);
                            ds.AddCustomChanges(docMultimidia, null, ChangeOperation.Insert);
                            ds.SaveCustomChanges();
                        }
                    }
                    catch (Exception oException)
                    {
                    }
                } // 
            }
            else
                content = thumbnail;

            if (content.IsNullOrEmpty())
                return MediaResponse(httpResponseMessage, url, conteudo, tipoConteudoHttp, lxTipoExtensao);

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            httpResponseMessage.Content = new StreamContent(new MemoryStream(content));
            string contentType = tipoConteudoHttp.IsNullOrEmpty() ? (lxTipoExtensao == 4 ? "video" : "image") + "/" + (TipoExtensao.GetValues()[lxTipoExtensao.ToString()]) : tipoConteudoHttp;
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return httpResponseMessage;
        }

    }
}
