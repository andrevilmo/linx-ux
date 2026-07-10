using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Business.Common.Autenticacao
{
    public class DadosAutenticacao
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string application { get; set; }
        public string url { get; set; }
        private bool _autenticaTcsAutorizacaoDomainService = true;
        public bool autenticaTcsAutorizacaoDomainService
        {
            get
            {
                return _autenticaTcsAutorizacaoDomainService;
            }
            set
            {
                _autenticaTcsAutorizacaoDomainService = value;
            }
        }
        public bool proxyUtiliza { get; set; }
        public int? proxyPorta { get; set; }
        public string proxyServidor { get; set; }
        public string proxyDominioUsuario { get; set; }
        public string proxyUsuario { get; set; }
        public string proxySenhaUsuario { get; set; }
    }
}
