using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel.Composition;
using Linx.Framework.BV.TratamentoErros;

namespace Linx.Framework.BV.Implementations
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////// Business Repository /////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    [Export(typeof(IExceptionLogger))]
    [ExportMetadata("ImplementationName", "ErrorHandlingRepositoryImplementation")]
    public partial class ErrorHandlingRepositoryImplementation : IExceptionLogger
    {
        private bool AddError(TcsLogErros logErro)
        {
            var domain = new Linx.Framework.BV.TratamentoErros.TratamentoErrosDomainService();
            domain.IsSecure = true;
            try
            {
                domain.AddCustomChanges(logErro, null, System.ServiceModel.DomainServices.Server.ChangeOperation.Insert);
                domain.SaveCustomChanges();
                return true;
            }
            catch (Exception oException)
            {
                return false;
            }

        }

        public bool addLog(DateTime DataErro, string NomeControlador, string MetodoHttp, string NomeAcao, string EnderecoWeb, string MensagemExcecao, string MensagemExcecaoInterna, string PilhaExcecao, string UsuarioWindows, string NomeServidor, Guid? UsuarioSistema, Guid? Empresa, Guid? GrupoEconomico, Guid? Aplicacao, int? Ambiente)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"CurrentUser", UsuarioSistema.ToString() },
                {"EconomicGroup", GrupoEconomico.ToString() },
                {"Environment", Ambiente.ToString() },
                {"CurrentCompany", Empresa.ToString() },
                {"Application", Aplicacao.ToString() }
            };

            long? idUsuario = BusinessUserServiceHelper.GetCurrentUserId(headers);
            int? idAplicacao = BusinessUserServiceHelper.GetApplicationId(Aplicacao.GetValueOrDefault());
            int? idLinx = BusinessUserServiceHelper.GetCurrentCompanyIdLinx(headers);
            int? idGpecon = BusinessUserServiceHelper.GetCurrentIdGpecon(headers);

            return AddError(new TcsLogErros()
            {
                DataErro = DataErro,
                NomeControlador = NomeControlador,
                MetodoHttp = MetodoHttp,
                NomeAcao = NomeAcao,
                EnderecoWeb = EnderecoWeb,
                MensagemExcecao = MensagemExcecao,
                MensagemExcecaoInterna = MensagemExcecaoInterna,
                PilhaExcecao = PilhaExcecao,
                UsuarioWindows = UsuarioWindows,
                IdUsuario = idUsuario,
                IdAplicacao = idAplicacao,
                IdLinxEmpresa = idLinx,
                IdLinxGpecon = idGpecon,
                IdTcsAmbiente = Ambiente,
                NomeServidor = NomeServidor
            }
           );
        }
    }
}
