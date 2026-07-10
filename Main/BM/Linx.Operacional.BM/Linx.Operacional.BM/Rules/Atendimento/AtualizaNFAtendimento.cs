using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Operacional.BM.Rules.Atendimento
{
    public class AtualizaNFAtendimento
    {
        public LJV_ATENDIMENTO AtualizaNFe(Int64 idAtendimento, 
            string cnpjFilial,
            Int64 nfNumero,
            string nfSerie,
            Int64 idNotaFiscal,
            Int32? idOperacaoFinalidade,
            short? lxStatusNFe,
            string chaveAcesso,
            DateTime? dhAutorizacao = null,
            string protocoloAutorizacao = null,
            DateTime? dhCancInutDeneg = null,
            string protocoloCancInutDeneg = null,
            string cStat = null,
            string xMotivo = null)
        {
            LinxOperacional bm = new LinxOperacional();

            var atend = bm.LJV_ATENDIMENTO.Where(f => f.ID_ATENDIMENTO == idAtendimento).Select(f => new { ID_GPECON = f.ID_GPECON, COD_LOJA = f.LJV_LOJA.COD_LOJA, LX_TIPO_ATENDIMENTO = f.LX_TIPO_ATENDIMENTO, ATENDIMENTO_CANCELADO = f.ATENDIMENTO_CANCELADO, ATENDIMENTO_CANCELADO_ECF = f.ATENDIMENTO_CANCELADO_ECF , ID_TERMINAL = f.LJV_CAIXA_CTRL.ID_TERMINAL, LX_TIPO_FISCAL = f.LX_TIPO_FISCAL }).FirstOrDefault();
            if (atend == null)
                throw new Exception("Atendimento não encontrado.");
            
            var atendDocFiscal = bm.LJV_ATENDIMENTO_DOC_FISCAL.Where(f =>
                f.ID_ATENDIMENTO == idAtendimento &&
                f.NUMERO_DOCUMENTO == nfNumero &&
                f.SERIE_DOCUMENTO == nfSerie &&
                f.MODELO_DOCUMENTO == "55" &&
                f.LJV_ATENDIMENTO.TBC_FILIAL.TBC_PFJ.CNPJ_CPF == cnpjFilial).FirstOrDefault();

            if (atendDocFiscal == null)
            {
                atendDocFiscal = new LJV_ATENDIMENTO_DOC_FISCAL()
                {
                    ID_ATENDIMENTO = idAtendimento,
                    ID_ATENDIMENTO_DOC_FISCAL = Linx.Operacional.BM.Rules.KeyBigInt.GetBigIntFromGuid("LX_LJV.LJV_ATENDIMENTO_DOC_FISCAL", "ID_ATENDIMENTO_DOC_FISCAL", Guid.NewGuid()),
                    LX_STATUS_INTEGRACAO_FISCAL = Convert.ToByte(Domains.LX_STATUS_INTEGRACAO_FISCAL.NaoIntegrado.Value),
                    MODELO_DOCUMENTO = "55",
                    SERIE_DOCUMENTO = nfSerie,
                    NUMERO_DOCUMENTO = nfNumero
                };
                bm.LJV_ATENDIMENTO_DOC_FISCAL.Add(atendDocFiscal);
            }
            else
                bm.Entry(atendDocFiscal).State = System.Data.Entity.EntityState.Modified;
            
            atendDocFiscal.EX_ID_NOTA_FISCAL = idNotaFiscal;
            atendDocFiscal.ID_OPERACAO_FINALIDADE_NOTA_FISCAL = idOperacaoFinalidade;
            atendDocFiscal.CHAVE_ACESSO = chaveAcesso;
            atendDocFiscal.DATA_HORA_AUTORIZACAO = dhAutorizacao;
            atendDocFiscal.PROTOCOLO_AUTORIZACAO = protocoloAutorizacao;
            if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Cancelado.Value))
            {
                atendDocFiscal.DATA_HORA_CANCELAMENTO = dhCancInutDeneg;
                atendDocFiscal.PROTOCOLO_CANCELAMENTO = protocoloCancInutDeneg;
            }
            if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Inutilizado.Value))
            {
                atendDocFiscal.DATA_HORA_INUTILIZACAO = dhCancInutDeneg;
                atendDocFiscal.PROTOCOLO_INUTILIZACAO = protocoloCancInutDeneg;
            }
            if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Denegado.Value))
            {
                atendDocFiscal.DATA_HORA_DENEGACAO = dhCancInutDeneg;
                atendDocFiscal.PROTOCOLO_DENEGACAO = protocoloCancInutDeneg;
            }
            
            if (!cStat.IsNullOrEmpty())
                atendDocFiscal.STATUS = cStat;
            if (!xMotivo.IsNullOrEmpty())
                atendDocFiscal.MOTIVO = (xMotivo.Length <= 250) ? xMotivo : xMotivo.Left(250);

            short lxStatusNfOriginal = atendDocFiscal.LX_STATUS_NF;

            if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Autorizado.Value))
                atendDocFiscal.LX_STATUS_NF = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_NF_DOC_FISCAL.Autorizado.Value);
            else if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Cancelado.Value))
                atendDocFiscal.LX_STATUS_NF = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_NF_DOC_FISCAL.Cancelado.Value);
            else if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Inutilizado.Value))
                atendDocFiscal.LX_STATUS_NF = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_NF_DOC_FISCAL.Inutilizado.Value);
            else if (lxStatusNFe == Convert.ToByte(Domains.LX_STATUS_NFE.Denegado.Value))
                atendDocFiscal.LX_STATUS_NF = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_NF_DOC_FISCAL.Denegado.Value);
            else
                atendDocFiscal.LX_STATUS_NF = Convert.ToByte(Domains.LX_STATUS_NF_DOC_FISCAL.Pendente.Value);

            bm.SaveChanges();

            if (lxStatusNfOriginal != atendDocFiscal.LX_STATUS_NF) //se o status mudou precisa criar ponteiro
            {

                bool gravaPonteiroETL = false;
                try
                {
                    Dictionary<string, string> dicVariacoesParametro = new Dictionary<string, string>();
                    dicVariacoesParametro.Add("TBC_GRUPO_ECONOMICO", atend.ID_GPECON.ToString());
                    gravaPonteiroETL = Linx.Business.Tools.LinxParameters.GetParameter<bool>("ATENDIMENTO_DOC_FISCAL_CRIA_PONTEIRO_ETL", dicVariacoesParametro, (int)Aplicativo.Operacional);
                }
                catch
                {
                    gravaPonteiroETL = false;
                }

                if (gravaPonteiroETL)
                {
                    int exp = bm.LJ_ETL_EXP_PROCESSO.Where(x => x.DESC_PROCESSO == "LjvAtendimentoDocFiscal").Select(x => x.ID_ETL_EXP_PROCESSO).FirstOrDefault();

                    LJ_ETL_EXP_LOTE_PROCESSAMENTO processo = new LJ_ETL_EXP_LOTE_PROCESSAMENTO();
                    processo.VALOR_CAMPO_TABELA = atendDocFiscal.ID_ATENDIMENTO_DOC_FISCAL.ToString();
                    processo.NUMERO_LOTE = null;
                    processo.ID_ETL_EXP_PROCESSO = (exp != null && exp != 0) ? exp : 13;
                    processo.PROCESSADO = 0;
                    processo.DATA_PROCESSAMENTO = null;
                    processo.LOG_ERRO = null;
                    processo.DATA_HORA_CRIACAO = System.DateTime.Now;
                    processo.COD_LOJA = atend.COD_LOJA;
                    processo.CNPJ = cnpjFilial;
                    processo.ID_ATENDIMENTO = idAtendimento;
                    processo.ATENDIMENTO_CANCELADO = atend.ATENDIMENTO_CANCELADO;
                    processo.COO = null;
                    processo.CCF = null;
                    processo.NF_NUMERO = nfNumero;
                    processo.NF_SERIE = nfSerie;
                    processo.MODELO_DOCUMENTO = "55";
                    processo.TIPO_DOCUMENTO = Domains.LX_TIPO_FISCAL.GetValues()[atend.LX_TIPO_FISCAL.ToString()];
                    processo.LX_STATUS_DADO = null;
                    processo.SESSIONID_ORIGEM = null;
                    processo.SESSIONID_DESTINO = null;

                    bm.LJ_ETL_EXP_LOTE_PROCESSAMENTO.Add(processo);
                    bm.SaveChanges();
                }
            }


            return new LJV_ATENDIMENTO() { LX_TIPO_ATENDIMENTO = atend.LX_TIPO_ATENDIMENTO, ATENDIMENTO_CANCELADO = atend.ATENDIMENTO_CANCELADO, ATENDIMENTO_CANCELADO_ECF = atend.ATENDIMENTO_CANCELADO_ECF };
        }

    }
}
