using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Internet.Application.Framework.Handlers
{
    public class ResponseUploadMediaDTO
    {
        public Guid UidDocumento { get; set; }
        public string Url { get; set; }
        public int OrdemApresentacao { get; set; }
        public int TipoDocumento { get; set; }
        public string DescricaoTipoDocumento { get; set; }
        public int TipoMidia { get; set; }
        public string DescricaoTipoMidia { get; set; }
        public string NomeArquivo { get; set; }
        public int TamanhoMidia { get; set; }
        public string UrlThumbnail { get; set; }
        public string UrlDelete { get; set; }
    }
}
