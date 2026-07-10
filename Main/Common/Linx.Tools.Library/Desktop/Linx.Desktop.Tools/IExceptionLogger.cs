using System;

namespace Linx.Tools
{
    public interface IExceptionLogger
    {
        bool addLog(DateTime DataErro, string NomeControlador, string MetodoHttp, string NomeAcao, string EnderecoWeb, string MensagemExcecao, string MensagemExcecaoInterna, string PilhaExcecao, string UsuarioWindows, string NomeServidor, Nullable<Guid> UsuarioSistema, Nullable<Guid> Empresa, Nullable<Guid> GrupoEconomico, Nullable<Guid> Aplicacao, Nullable<int> Ambiente);
    }
}
