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
using Linx.Framework.BV.Domains;
using Linx.Framework.BV.Autorizacao;
using Linx.Framework.BV.MultimidiaAutorizacao;

namespace Linx.Framework.BV.Multimidia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class MultimidiaDomainService
    {
        [Ignore()]
        public void SyncMedia(DocTabelaSync mediaToSync)
        {
            MultimidiaDomainService ds = this;

            var tcsTabelaInfo = ds.GetTcsTabelaInfo(mediaToSync.NomeTabela).Split(new string[] { "||" }, StringSplitOptions.None);

            Guid uidTabela = Guid.Parse(tcsTabelaInfo[0]);
            Int64 idChave = mediaToSync.IdChave.GetValueOrDefault();
            Guid uidChave = mediaToSync.UidChave.GetValueOrDefault();


            if (Convert.ToBoolean(tcsTabelaInfo[1]))
            {
                MultimidiaAutorizacaoDomainService dsAutorizacao = new MultimidiaAutorizacaoDomainService();

                List<DocMultimidiaTabelaAutorizacaoChild> midiasRemoverA =
                    (from result in dsAutorizacao.GetDocMultimidiaTabelaAutorizacaoChildNoAssociations()
                     where result.UidTabela == uidTabela && result.IdChave == idChave && result.UidChave == uidChave && !mediaToSync.Midias.Contains(result.UidDocumento)
                     select result).ToList();

                if (midiasRemoverA.Count() > 0)
                {
                    foreach (DocMultimidiaTabelaAutorizacaoChild midiaTabela in midiasRemoverA)
                    {
                        dsAutorizacao.AddCustomChanges(midiaTabela, null, ChangeOperation.Delete);

                        //verifica se midia está relacionada com outro registro, senão apaga.
                        if (dsAutorizacao.GetDocMultimidiaTabelaAutorizacaoNoAssociations().Where(i => i.UidDocumento == midiaTabela.UidDocumento).Count() <= 1)
                            dsAutorizacao.AddCustomChanges(new DocMultimidiaAutorizacao() { UidDocumento = midiaTabela.UidDocumento }, null, ChangeOperation.Delete);
                    }
                    dsAutorizacao.SaveCustomChanges();
                }

                if (mediaToSync.Midias.IndexOf(Guid.Empty) >= 0)
                    mediaToSync.Midias.Remove(Guid.Empty);

                if (mediaToSync.Midias.Count() > 0)
                {
                    //Insert - Update
                    short order = 0;
                    foreach (Guid uidDocumento in mediaToSync.Midias)
                    {
                        DocMultimidiaTabelaAutorizacaoChild docTabela = new DocMultimidiaTabelaAutorizacaoChild()
                        {
                            UidTabela = uidTabela,
                            UidDocumento = uidDocumento,
                            UidChave = uidChave,
                            IdChave = idChave,
                            OrdemApresentacao = order
                        };
                        order++;
                        dsAutorizacao.AddCustomChanges(docTabela, null, ChangeOperation.Insert);
                    }
                    dsAutorizacao.SaveCustomChanges();
                }


            }
            else
            {
                //Delete
                List<DocMultimidiaTabelaChild> midiasRemover =
                    (from result in ds.GetDocMultimidiaTabelaChildNoAssociations()
                     where result.UidTabela == uidTabela && result.IdChave == idChave && result.UidChave == uidChave && !mediaToSync.Midias.Contains(result.UidDocumento)
                     select result).ToList();

                if (midiasRemover.Count() > 0)
                {
                    foreach (DocMultimidiaTabelaChild midiaTabela in midiasRemover)
                    {
                        ds.AddCustomChanges(midiaTabela, null, ChangeOperation.Delete);

                        //verifica se midia está relacionada com outro registro, senão apaga.
                        if (ds.GetDocMultimidiaTabelaNoAssociations().Where(i => i.UidDocumento == midiaTabela.UidDocumento).Count() <= 1)
                            ds.AddCustomChanges(new DocMultimidia() { UidDocumento = midiaTabela.UidDocumento }, null, ChangeOperation.Delete);

                    }
                    ds.SaveCustomChanges();
                }

                if (mediaToSync.Midias.IndexOf(Guid.Empty) >= 0)
                    mediaToSync.Midias.Remove(Guid.Empty);

                if (mediaToSync.Midias.Count() > 0)
                {
                    //Insert - Update
                    short order = 0;
                    foreach (Guid uidDocumento in mediaToSync.Midias)
                    {
                        DocMultimidiaTabelaChild docTabela = new DocMultimidiaTabelaChild()
                        {
                            UidTabela = uidTabela,
                            UidDocumento = uidDocumento,
                            UidChave = uidChave,
                            IdChave = idChave,
                            OrdemApresentacao = order
                        };
                        order++;
                        ds.AddCustomChanges(docTabela, null, ChangeOperation.Insert);
                    }
                    ds.SaveCustomChanges();
                }
            }
        }

        [Invoke(HasSideEffects = true)]
        public List<DocMultimidiaInfo> GetMultimedia(string nomeTabela, Int64? idChave, Guid? uidChave, byte? tipoDocumento, Guid? uidUsuario, Dictionary<string, string> headers)
        {
            MultimidiaDomainService ds = new MultimidiaDomainService(headers);

            var tcsTabelaInfo = ds.GetTcsTabelaInfo(nomeTabela).Split(new string[] { "||" }, StringSplitOptions.None);

            Guid uidTabela = Guid.Parse(tcsTabelaInfo[0]);

            EntitySearch search = new EntitySearch();
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
            search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));

            if (!tipoDocumento.IsNullOrEmpty())
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "LxTipoDocumento"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, tipoDocumento));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
            }

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
                var docMultimidiaInfo = (from result in dsAutorizacao.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { search }))
                                         select new
                                         {
                                             UidDocumento = result.UidDocumento,
                                             Url = result.Url,
                                             OrdemApresentacao = result.OrdemApresentacao,
                                             TipoDocumento = result.LxTipoDocumento,
                                             TipoExtensao = result.LxTipoExtensao,
                                             TipoMidia = result.LxTipoMidia,
                                             NomeArquivo = result.NomeArquivo,
                                             TipoConteudoHttp = result.TipoConteudoHttp,
                                             TamanhoMidia = result.TamanhoMidia
                                         }).ToList();

                return (from result in docMultimidiaInfo
                        select new DocMultimidiaInfo
                        {
                            UidDocumento = result.UidDocumento,
                            Url = result.Url.IsNullOrEmpty() ? Utils.GetMediaUrl(result.UidDocumento, headers) : result.Url,
                            OrdemApresentacao = result.OrdemApresentacao,
                            TipoDocumento = result.TipoDocumento,
                            DescricaoTipoDocumento = TipoDocumento.GetValues()[result.TipoDocumento.ToString()],
                            TipoMidia = result.TipoMidia,
                            DescricaoTipoMidia = TipoMidia.GetValues()[result.TipoMidia.ToString()],
                            NomeArquivo = result.NomeArquivo,
                            TipoConteudoHttp = result.TipoConteudoHttp,
                            TamanhoMidia = result.TamanhoMidia,
                            UrlThumbnail = Utils.GetMediaThumbnailUrl(result.UidDocumento, headers),
                            UrlServiceBus = Utils.GetMediaServiceBusUrl(result.UidDocumento, result.NomeArquivo)
                        }).OrderBy(i => i.OrdemApresentacao).ToList();

            }
            else
            {
                var docMultimidiaInfo = (from result in ds.GetDocMultimidiaTabelaByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { search }))
                                         select new
                                         {
                                             UidDocumento = result.UidDocumento,
                                             Url = result.Url,
                                             OrdemApresentacao = result.OrdemApresentacao,
                                             TipoDocumento = result.LxTipoDocumento,
                                             TipoExtensao = result.LxTipoExtensao,
                                             TipoMidia = result.LxTipoMidia,
                                             NomeArquivo = result.NomeArquivo,
                                             TipoConteudoHttp = result.TipoConteudoHttp,
                                             TamanhoMidia = result.TamanhoMidia
                                         }).ToList();

                return (from result in docMultimidiaInfo
                        select new DocMultimidiaInfo
                        {
                            UidDocumento = result.UidDocumento,
                            Url = result.Url.IsNullOrEmpty() ? Utils.GetMediaUrl(result.UidDocumento, headers) : result.Url,
                            OrdemApresentacao = result.OrdemApresentacao,
                            TipoDocumento = result.TipoDocumento,
                            DescricaoTipoDocumento = TipoDocumento.GetValues()[result.TipoDocumento.ToString()],
                            TipoMidia = result.TipoMidia,
                            DescricaoTipoMidia = TipoMidia.GetValues()[result.TipoMidia.ToString()],
                            NomeArquivo = result.NomeArquivo,
                            TipoConteudoHttp = result.TipoConteudoHttp,
                            TamanhoMidia = result.TamanhoMidia,
                            UrlThumbnail = Utils.GetMediaThumbnailUrl(result.UidDocumento, headers),
                            UrlServiceBus = Utils.GetMediaServiceBusUrl(result.UidDocumento, result.NomeArquivo)
                        }).OrderBy(i => i.OrdemApresentacao).ToList();
            }

        }

        [Invoke(HasSideEffects = true)]
        public string GetTcsTabelaInfo(string nomeTabela)
        {
            TabelaAutorizacao.TabelaAutorizacaoDomainService ds = new TabelaAutorizacao.TabelaAutorizacaoDomainService();
            var query =
                (from result in ds.GetTcsTabelaAutorizacaoNoAssociations().Where(i => i.NomeTabela == nomeTabela)
                 select new { result.UidTabela, result.TabelaAutorizacao }).FirstOrDefault();

            if (query.IsNullOrEmpty())
                throw new Exception(string.Format("Não foi encontrado o cadastro para a tabela: {0}.", nomeTabela));

            return query.UidTabela.ToString() + "||" + query.TabelaAutorizacao.ToString();
        }

        [Invoke(HasSideEffects = true)]
        public DocMultimidiaInfo UploadMedia(DocMultimidiaUpload uploadedMedia)
        {
            MultimidiaDomainService ds = this;
            var tcsTabelaInfo = ds.GetTcsTabelaInfo(uploadedMedia.NomeTabela).Split(new string[] { "||" }, StringSplitOptions.None);

            if (Convert.ToBoolean(tcsTabelaInfo[1]))
            {
                MultimidiaAutorizacaoDomainService dsAutorizacao = new MultimidiaAutorizacaoDomainService();

                DocMultimidiaAutorizacao midia = new DocMultimidiaAutorizacao()
                {
                    UidDocumento = Guid.NewGuid(),
                    IdDocClassificador = 1,
                    LxTipoDocumento = uploadedMedia.TipoDocumento,
                    NomeArquivo = uploadedMedia.NomeArquivo,
                    Conteudo = System.Convert.FromBase64String(uploadedMedia.Conteudo),
                    LxTipoMidia = Utils.GetTipoMidia(uploadedMedia.NomeArquivo),
                    DescDocumento = "Multimedia",
                    TipoConteudoHttp = uploadedMedia.TipoConteudoHttp,
                    TamanhoMidia = uploadedMedia.Tamanho,
                    DataCriacao = DateTime.Now
                };

                if (midia.LxTipoMidia == 1)
                {
                    midia.Thumbnail = Utils.CreateThumbnail(midia.Conteudo, midia.NomeArquivo);
                }

                dsAutorizacao.AddCustomChanges(midia, null, ChangeOperation.Insert);
                dsAutorizacao.SaveCustomChanges();

                return MultimidiaUploadInfo(midia.UidDocumento, midia.LxTipoDocumento, midia.LxTipoMidia, midia.NomeArquivo, midia.TipoConteudoHttp, midia.TamanhoMidia);

            }
            else
            {
                DocMultimidia midiaLocal = new DocMultimidia()
                {
                    UidDocumento = Guid.NewGuid(),
                    IdDocClassificador = 1,
                    LxTipoDocumento = uploadedMedia.TipoDocumento,
                    NomeArquivo = uploadedMedia.NomeArquivo,
                    Conteudo = System.Convert.FromBase64String(uploadedMedia.Conteudo),
                    LxTipoMidia = Utils.GetTipoMidia(uploadedMedia.NomeArquivo),
                    DescDocumento = "Multimedia",
                    TipoConteudoHttp = uploadedMedia.TipoConteudoHttp,
                    TamanhoMidia = uploadedMedia.Tamanho,
                    DataCriacao = DateTime.Now
                };

                //if (!uploadedMedia.JExpression.IsNullOrEmpty())
                //{
                //    Guid uidTabela = Guid.Parse("C37ACCFE-0CB7-476A-8F5C-7BF134F00205");
                //    Guid uidChave = Guid.Empty;
                //    int idChave = 8527;

                //    DocMultimidiaTabelaChild midiaTabela = new DocMultimidiaTabelaChild()
                //    {
                //        UidDocumento = midia.UidDocumento,
                //        UidTabela = uidTabela,
                //        UidChave = uidChave,
                //        IdChave = idChave,
                //        OrdemApresentacao = 0
                //    };

                //    ds.AddCustomChanges(midiaTabela, null, ChangeOperation.Insert);
                //}

                if (midiaLocal.LxTipoMidia == 1)
                {
                    midiaLocal.Thumbnail = Utils.CreateThumbnail(midiaLocal.Conteudo, midiaLocal.NomeArquivo);
                }

                ds.AddCustomChanges(midiaLocal, null, ChangeOperation.Insert);
                ds.SaveCustomChanges();

                return MultimidiaUploadInfo(midiaLocal.UidDocumento, midiaLocal.LxTipoDocumento, midiaLocal.LxTipoMidia, midiaLocal.NomeArquivo, midiaLocal.TipoConteudoHttp, midiaLocal.TamanhoMidia);
            }
        }

        private DocMultimidiaInfo MultimidiaUploadInfo(Guid uidDocumento, byte tipoDocumento, byte tipoMidia, string nomeArquivo, string tipoConteudoHttp, int? tamanhoMidia)
        {
            return new DocMultimidiaInfo()
            {
                UidDocumento = uidDocumento,
                Url = Utils.GetMediaUrl(uidDocumento, null),
                OrdemApresentacao = 0,
                TipoDocumento = tipoDocumento,
                DescricaoTipoDocumento = TipoDocumento.GetValues()[tipoDocumento.ToString()],
                TipoMidia = tipoMidia,
                DescricaoTipoMidia = TipoMidia.GetValues()[tipoMidia.ToString()],
                NomeArquivo = nomeArquivo,
                TipoConteudoHttp = tipoConteudoHttp,
                TamanhoMidia = tamanhoMidia,
                UrlThumbnail = Utils.GetMediaThumbnailUrl(uidDocumento, null),
                UrlServiceBus = Utils.GetMediaServiceBusUrl(uidDocumento, nomeArquivo)
            };
        }

    }
}
