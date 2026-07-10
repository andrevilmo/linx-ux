using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class EstoqueRegra
    {
        private RepositorioRomaneio repositorioRomaneio = null;
        private LinxOperacional contexto = null;

        public EstoqueRegra()
        {
            this.contexto = new LinxOperacional();
            this.repositorioRomaneio = new RepositorioRomaneio(this.contexto);
        }

        /// <summary>
        /// Método para gerar o log de erros ao importar arquivo de coleta de inventário
        /// </summary>
        /// <param name="idDocumento">ID do registro que será gravado o log</param>
        /// <param name="idDocumentoTipo">ID do tipo de documento</param>
        /// <param name="numeroDocumento">Código do registro que será gravado o log</param>
        /// <param name="idFilial">ID da filial do log</param>
        /// <param name="classe">Nome da entidade</param>
        /// <param name="logDetalhe">Descrição de detalhe para log</param>
        /// <param name="lstErros">Lista dos erros</param>
        public void GerarLogInventario(int idDocumento, short? idDocumentoTipo, int numeroDocumento, int idFilial, string classe, string logDetalhe, List<string> lstErros)
        {
            try
            {
                //Preencho o log Ocorrencia
                ADT_LOG_OCORRENCIA logOcorrencia = new ADT_LOG_OCORRENCIA()
                {
                    DATA_HORA_LOG = DateTime.Now,
                    ID_DOCUMENTO_TIPO = idDocumentoTipo,
                    LX_TIPO_OCORRENCIA = 1, // Inclusão
                    ID_DOCUMENTO = idDocumento,
                    CLASSE = classe,
                    NUMERO_DOCUMENTO = numeroDocumento,
                    ID_FILIAL_PFJ = idFilial,
                    ID_GPECON = (int)LinxOperacional.SecurityHelper.GetCurrentIdGpecon(),
                    ID_USUARIO = LinxOperacional.SecurityHelper.GetCurrentUserId().GetValueOrDefault()
                };

                //Preencho os detalhes com os erros

                List<ADT_LOG_OCORRENCIA_DETALHE> lstLogOcorrenciaDetalhe = new List<ADT_LOG_OCORRENCIA_DETALHE>();

                foreach (var erro in lstErros)
                {
                    ADT_LOG_OCORRENCIA_DETALHE logOcorrenciaDetalhe = new ADT_LOG_OCORRENCIA_DETALHE();

                    if (!string.IsNullOrEmpty(logDetalhe))
                        logOcorrenciaDetalhe.LOG_DETALHE = logDetalhe;

                    logOcorrenciaDetalhe.LOG_DESCRICAO = erro;

                    lstLogOcorrenciaDetalhe.Add(logOcorrenciaDetalhe);
                }

                logOcorrencia.ADT_LOG_OCORRENCIA_DETALHE_LISTA = lstLogOcorrenciaDetalhe;

                //Salvo os logs

                repositorioRomaneio.Add(logOcorrencia);

                repositorioRomaneio.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Linx.Operacional.BM.Exceptions.BusinessModelException("Erro ao gravar logs");
            }
        }
    }
}
