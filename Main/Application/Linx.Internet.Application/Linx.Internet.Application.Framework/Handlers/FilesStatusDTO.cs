using System;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Flurl;

namespace Linx.Internet.Application.Framework.Handlers
{
    public class FilesStatusDTO
    {
        public const string HandlerPath = "/";

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string deleteType { get; set; }
        public string error { get; set; }
        public int tipoMidia { get; set; }
        public string JExpression { get; set; }
        public string nomeTabela { get; set; }
       

        public ResponseUploadMediaDTO midia { get; set; }

        public FilesStatusDTO() { }

        public FilesStatusDTO(FileInfo fileInfo)
        {
            this.SetValues(fileInfo.Name, (int)fileInfo.Length);
        }

        public FilesStatusDTO(HttpPostedFile file, string tipoMidia, string jExpression, string nomeTabela)
        {
            this.tipoMidia = Convert.ToInt32(tipoMidia);
            this.JExpression = jExpression;
            this.nomeTabela = nomeTabela;

            var r = this.UploadFileApi(file);

            var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710/");
            this.name = r.NomeArquivo;
            this.size = file.ContentLength;
            progress = "1.0";

            this.url = r.Url;
            //this.deleteUrl = HandlerPath + "upload.axd?f=" + r.NomeArquivo;

            this.deleteUrl = _serviceBus
                .AppendPathSegment("linxframeworkmultimidia/deletemedia")
                .SetQueryParams(new
                {
                    uidDocumento = r.UidDocumento
                });

            this.deleteType = "DELETE";
            this.thumbnailUrl = r.UrlThumbnail;
            r.UrlDelete = this.deleteUrl;
            midia = r;

        }

        private void SetValues(string fileName, int fileLength)
        {
            name = fileName;
            type = "image/png";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "upload.axd?f=" + fileName;
            deleteUrl = HandlerPath + "upload.axd?f=" + fileName;
            deleteType = "DELETE";

            var ext = Path.GetExtension(fileName);

            //var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            //if (fileSize > 3 || !IsImage(ext)) thumbnailUrl = "/Content/img/generalFile.png";
            //else thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullPath);

            //if (IsImage(ext))
            //    thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpeg" || ext == ".jpg" || ext == ".png";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private ResponseUploadMediaDTO UploadFileApi(HttpPostedFile file)
        {
            var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");

            byte[] contentBytes;
            using(var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                contentBytes = memoryStream.ToArray();
            }

            var dto = new RequestUploadMediaDTO()
            {
                TipoDocumento = this.tipoMidia,
                Conteudo = Convert.ToBase64String(contentBytes),
                NomeArquivo = file.FileName,
                TipoConteudoHttp = file.ContentType,
                Tamanho = file.ContentLength,
                JExpression = this.JExpression,
                NomeTabela = this.nomeTabela
            };

            // chama API para persistencia da midia
            var client = new RestClient(_serviceBus);

            var request = new RestRequest("linxframeworkmultimidia/uploadmedia", Method.POST);
            request.AlwaysMultipartFormData = false;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(dto);

            //request.AddHeader("AccessGroup", HttpContext.Current.Request.Headers["AccessGroup"]);
            request.AddHeader("Application", HttpContext.Current.Request.Headers["Application"]);
            //request.AddHeader("AuthorizationToken", HttpContext.Current.Request.Headers["AuthorizationToken"]); // erro no IE
            request.AddHeader("AuthorizationToken", "");
            request.AddHeader("CurrentCompany", HttpContext.Current.Request.Headers["CurrentCompany"]);
            request.AddHeader("CurrentUser", HttpContext.Current.Request.Headers["CurrentUser"]);
            request.AddHeader("EconomicGroup", HttpContext.Current.Request.Headers["EconomicGroup"]);
            request.AddHeader("Environment", HttpContext.Current.Request.Headers["Environment"]);  // erro no IE
            
            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ResponseUploadMediaDTO>(response.Content);
            }
            else
                throw new Exception(response.Content);

        }
    }
}
