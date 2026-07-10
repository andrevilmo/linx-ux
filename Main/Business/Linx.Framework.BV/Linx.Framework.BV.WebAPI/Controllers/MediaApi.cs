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
using Linx.Business.Tools;

namespace Linx.Framework.BV.WebAPI.Controllers
{
    
    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class MediaApiController
    {
        [Route("GetPendingMedias"), System.Web.Http.HttpGet()]
        public List<MediaElement> GetPendingMedias(byte documentoType = 0)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            var result = (from r in service.GetDocMultimidiaNoAssociations()
                          where r.Conteudo != null && r.DescDocumento == "Multimedia" && (r.Url == null || r.Url.Trim() == "") && (documentoType == 0 || r.LxTipoDocumento == documentoType)
                          orderby r.UidDocumento
                          select new MediaElement { Id = r.UidDocumento, ExtensionType = r.LxTipoExtensao, Url = "", LxTipoDocumento = r.LxTipoDocumento }).ToList();

            return result;
        }

        [Route("GetMediaContent"), System.Web.Http.HttpGet()]
        public byte[] GetMediaContent(Guid id)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            var media = (from r in service.GetDocMultimidiaNoAssociations()
                         where r.UidDocumento == id
                         select r.Conteudo).FirstOrDefault();

            return (media == null ? new byte[] { } : media);
        }

        [Route("UpdateMedias"), System.Web.Http.HttpPost()]
        public string UpdateMedias(List<MediaElement> medias)
        {
            string message = "";

            if (medias != null && medias.Count > 0)
            {
                bool delete;
                DocMultimidia oEntity, newEntity;
                MultimidiaDomainService service = new MultimidiaDomainService();
                foreach (var media in medias.Where(e => e != null && !e.Id.IsNullOrEmpty()).ToArray())
                {
                    if (media.Url.IsNullOrEmpty())
                        delete = service.GetDocMultimidia().Where(e => e.UidDocumento == media.Id && e.Conteudo == null).Count() > 0;
                    else
                        delete = false;

                    //Defining entities
                    //oEntity = new DocMultimidia() { UidDocumento = media.Id, Url = ".", DescDocumento = "", Conteudo = (media.KeepContent ? null : new byte[] { }), Thumbnail = (media.KeepContent ? null : new byte[] { }), ChecksumConteudo = (media.KeepContent ? "" : "."), ChecksumThumbnail = (media.KeepContent ? "" : ".") };
                    //newEntity = new DocMultimidia() { UidDocumento = media.Id, Url = media.Url, DescDocumento = "", Conteudo = null, Thumbnail = null, ChecksumConteudo = "", ChecksumThumbnail = "" };

                    oEntity = new DocMultimidia() { UidDocumento = media.Id, Url = ".", DescDocumento = "", Conteudo = (media.KeepContent ? null : new byte[] { }), Thumbnail = (media.KeepContent ? null : new byte[] { }) };
                    newEntity = new DocMultimidia() { UidDocumento = media.Id, Url = media.Url, DescDocumento = "", Conteudo = null, Thumbnail = null };


                    //Add change
                    if (delete)
                        service.AddCustomChanges(newEntity, null, System.ServiceModel.DomainServices.Server.ChangeOperation.Delete);
                    else
                        service.AddCustomChanges(newEntity, oEntity, System.ServiceModel.DomainServices.Server.ChangeOperation.Update);
                }
                try
                {
                    service.SaveCustomChanges();
                }
                catch (Exception excep)
                {
                    message = excep.Message;
                }
            }

            return message;
        }

        [Route("GetMediaUrlById"), System.Web.Http.HttpGet()]
        public List<string> GetMediaUrlById(string tableName, int pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);

            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.IdChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Url).ToList();

            return (media == null ? new List<string>() : media);
        }

        [Route("GetMediaUrlByUid"), System.Web.Http.HttpGet()]
        public List<string> GetMediaUrlByUid(string tableName, Guid pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.UidChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Url).ToList();

            return (media == null ? new List<string>() : media);
        }

        [Route("GetEffectiveMedias"), System.Web.Http.HttpGet()]
        public List<MediaElement> GetEffectiveMedias(byte documentoType = 0)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            var result = (from r in service.GetDocMultimidiaNoAssociations()
                          where r.DescDocumento == "Multimedia" && r.Url != null && r.Url.Trim() != "" && (documentoType == 0 || r.LxTipoDocumento == documentoType)
                          orderby r.UidDocumento
                          select new MediaElement { Id = r.UidDocumento, ExtensionType = r.LxTipoExtensao, Url = r.Url, LxTipoDocumento = r.LxTipoDocumento }).ToList();

            return result;
        }

        [Route("GetMediaContentById"), System.Web.Http.HttpGet()]
        public List<byte[]> GetMediaContentById(string tableName, int pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);

            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.IdChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Conteudo).ToList();

            return (media == null ? new List<byte[]>() : media);
        }

        [Route("GetMediaContentByUid"), System.Web.Http.HttpGet()]
        public List<byte[]> GetMediaContentByUid(string tableName, Guid pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.UidChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Conteudo).ToList();

            return (media == null ? new List<byte[]>() : media);
        }

        [Route("GetMediaThumbnailById"), System.Web.Http.HttpGet()]
        public List<byte[]> GetMediaThumbnailById(string tableName, int pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.IdChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Thumbnail).ToList();

            return (media == null ? new List<byte[]>() : media);
        }

        [Route("GetMediaThumbnailByUid"), System.Web.Http.HttpGet()]
        public List<byte[]> GetMediaThumbnailByUid(string tableName, Guid pkValue)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.UidChave == pkValue
                         orderby r.OrdemApresentacao
                         select r.Thumbnail).ToList();

            return (media == null ? new List<byte[]>() : media);
        }

        [Route("GetMediaUrlThumbByUid"), System.Web.Http.HttpGet()]
        public List<string> GetMediaUrlThumbByUid(string tableName, Guid pkValue, int usabilityId)
        {
            int? applicativeId = UserServiceHelper.GetApplicativeIdByMediaUse(usabilityId);

            if (applicativeId.IsNullOrEmpty())
                return new List<string>();

            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.UidChave == pkValue && r.Url != null && r.Url != ""
                         orderby r.OrdemApresentacao
                         select r.Url).ToArray().Select(e => e.ToUrlMediaThumb(applicativeId.Value, usabilityId)).ToList();

            return (media == null ? new List<string>() : media);
        }

        [Route("GetMediaUrlThumbById"), System.Web.Http.HttpGet()]
        public List<string> GetMediaUrlThumbById(string tableName, int pkValue, int usabilityId)
        {
            int? applicativeId = UserServiceHelper.GetApplicativeIdByMediaUse(usabilityId);

            if (applicativeId.IsNullOrEmpty())
                return new List<string>();

            MultimidiaDomainService service = new MultimidiaDomainService();
            Guid uidTabela = service.GetTcsTabelaUid(tableName);
            var media = (from r in service.GetMultimidiaCompact2BO()
                         where r.UidTabela == uidTabela && r.IdChave == pkValue && r.Url != null && r.Url != ""
                         orderby r.OrdemApresentacao
                         select r.Url).ToArray().Select(e => e.ToUrlMediaThumb(applicativeId.Value, usabilityId)).ToList();

            return (media == null ? new List<string>() : media);
        }

        [Route("GetMediaConfigLength"), System.Web.Http.HttpGet()]
        public List<MediaConfigLength> GetMediaConfigLength()
        {
            //aqui
            MultimidiaDomainService service = new MultimidiaDomainService();
            var result = (from r in service.GetDocMultimidiaConfig()
                          where r.DocAltura != null && r.DocAltura > 0 && r.DocLargura != null && r.DocLargura > 0
                          orderby r.IdTcsAplicativo, r.LxUsoMultimidia
                          select new MediaConfigLength { IdApp = r.IdTcsAplicativo, IdUse = r.LxUsoMultimidia, Height = (int)r.DocAltura, Width = (int)r.DocLargura }).ToList();

            return result;
        }

        [Route("UpdateMedia"), System.Web.Http.HttpPost()]
        public string UpdateMedia(MediaElement media)
        {
            return UpdateMedias((new MediaElement[] { media }).ToList());
        }

        [Route("GetMediaThumbnail"), System.Web.Http.HttpGet()]
        public byte[] GetMediaThumbnail(Guid id)
        {
            MultimidiaDomainService service = new MultimidiaDomainService();
            var media = (from r in service.GetDocMultimidiaNoAssociations()
                         where r.UidDocumento == id
                         select r.Thumbnail).FirstOrDefault();

            return (media == null ? new byte[] { } : media);
        }
    }
}
