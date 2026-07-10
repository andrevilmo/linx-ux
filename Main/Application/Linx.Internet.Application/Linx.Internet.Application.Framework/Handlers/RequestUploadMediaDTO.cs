using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Internet.Application.Framework.Handlers
{
    public class RequestUploadMediaDTO
    {
        public int TipoDocumento { get; set; }
        public string Conteudo { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoConteudoHttp { get; set; }
        public int Tamanho { get; set; }
        public string JExpression { get; set; }
        public string NomeTabela { get; set; }
    }
}
