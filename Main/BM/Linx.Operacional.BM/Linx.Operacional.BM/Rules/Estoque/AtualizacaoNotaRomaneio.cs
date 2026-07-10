using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class SituacaoRomaneio
    {
        public int LX_STATUS_ROMANEIO { get; set; }
        public int LX_STATUS_DOCUMENTO { get; set; }
    }
    public class AtualizacaoNotaRomaneio
    {
        /// <summary>
        /// A partir dos parâmetros passados, o sistema identifica qual deve ser o LX_STATUS_DOCUMENTO e LX_STATUS_ROMANEIO, retornando para atualização desses campos na tabela STK_ROMANEIO
        /// </summary>
        /// <param name="lxStatusNfe">LX_STATUS_NFE da nota</param>
        /// <param name="lxTipoEmissao">LX_TIPO_EMISSAO do Romaneio</param>
        /// <param name="indicaNotaImportacao">Indica se é nota de importação (Enviar true quando LX_TIPO_EMISSAO = Propria e STM_ROMANEIO_NF_LISTA.TBC_PFJ.UF = EX)</param>
        /// <param name="confirmaEstoqueNotaTerceiroRecebida">Enviar true quando for nota recebida de terceiro e o produto já deve entrar em estoque</param>
        /// <returns></returns>
        public static SituacaoRomaneio RetornaSituacaoStatusRomaneio(short lxStatusNfe, byte lxTipoEmissao, bool indicaNotaImportacao, bool indicaRecebimentoNF = false)
        {
            var statusRomaneio = new SituacaoRomaneio
            {
                LX_STATUS_DOCUMENTO = 0,
                LX_STATUS_ROMANEIO = 0
            };

            if (lxTipoEmissao == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Terceiro.Value) || indicaRecebimentoNF)
            {
                statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.AguardandoMercadoria.Value);
                statusRomaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_ROMANEIO.EstoquePendente.Value);
            }
            else
            {
                if (lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Cancelado.Value) ||
                    lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Denegado.Value) ||
                    lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Inutilizado.Value))
                {
                    statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.Cancelado.Value);
                    statusRomaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_ROMANEIO.NaoConsideraEstoque.Value);
                }
                else
                {
                    if (indicaNotaImportacao)
                    {
                        statusRomaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_ROMANEIO.EstoquePendente.Value);
                        if (lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Autorizado.Value) || lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.NfNaoEletronica.Value))
                            statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.AguardandoMercadoria.Value);
                        else
                            statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.AguardandoAutorizacao.Value);
                    }
                    else
                    {
                        if (lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Autorizado.Value) || lxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.NfNaoEletronica.Value))
                        {
                            statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.Finalizado.Value);
                            statusRomaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value);
                        }
                        else
                        {
                            statusRomaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_DOCUMENTO_ROMANEIO.AguardandoAutorizacao.Value);
                            statusRomaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Linx.Operacional.BM.Domains.LX_STATUS_ROMANEIO.EstoquePendente.Value);
                        }
                    }
                }
            }

            return statusRomaneio;
        }

        public bool AtualizaNotaRomaneio(NfRomaneio romaneio)
        {
            LinxOperacional contextoOperacional = new LinxOperacional();

            var rom = contextoOperacional.STK_ROMANEIO
                .Include("STK_ROMANEIO_NF_LISTA")
                .Include("STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA")
                .Include("STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.NTS_ROMANEIO_ITEM_RELACIONADO")
                .Where(r => r.ID_STK_ROMANEIO == romaneio.IdStkRomaneio)
                .FirstOrDefault();

            if (rom == null)
                return false;
            else
            {
                bool indicaNotaImportacao = false;
                if (!romaneio.ExidNotaFiscalEntrada.IsNullOrEmpty() && rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Propria.Value) && rom.STK_ROMANEIO_NF_LISTA.TBC_PFJ != null && rom.STK_ROMANEIO_NF_LISTA.TBC_PFJ.UF == "EX")
                    indicaNotaImportacao = true;
                bool indicaRecebimentoNF = false;
                if (rom.STK_ROMANEIO_NF_LISTA != null)
                    indicaRecebimentoNF = rom.STK_ROMANEIO_NF_LISTA.LX_STATUS_NFE == Convert.ToByte(Domains.LX_STATUS_NFE.RecebimentoNFeEntrada.Value);
                var statusRomaneio = RetornaSituacaoStatusRomaneio(romaneio.LxStatusNfe, rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO, indicaNotaImportacao, indicaRecebimentoNF);

                ////se o romaneio é de entrada de terceiro e já estava finalizado, não atualiza os status do romaneio, pois o usuário pode ter finalizado manualmente, senão, atualiza
                //if (!(rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Terceiro.Value) &&
                //    rom.LX_STATUS_ROMANEIO == Convert.ToByte(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value) && 
                //    statusRomaneio.LX_STATUS_ROMANEIO == Convert.ToByte(Domains.LX_STATUS_ROMANEIO.EstoquePendente.Value) &&
                //    statusRomaneio.LX_STATUS_DOCUMENTO == Convert.ToByte(Domains.LX_STATUS_DOCUMENTO_ROMANEIO.AguardandoMercadoria.Value)))
                //{
                //    rom.LX_STATUS_ROMANEIO = statusRomaneio.LX_STATUS_ROMANEIO;
                //    rom.LX_STATUS_DOCUMENTO = statusRomaneio.LX_STATUS_DOCUMENTO;
                //}

                if (rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO != Convert.ToByte(Domains.LX_TIPO_EMISSAO.Terceiro.Value))
                {
                    rom.LX_STATUS_ROMANEIO = statusRomaneio.LX_STATUS_ROMANEIO;
                    rom.LX_STATUS_DOCUMENTO = statusRomaneio.LX_STATUS_DOCUMENTO;
                }
                else if (rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Terceiro.Value) && rom.LX_STATUS_ROMANEIO != Convert.ToByte(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value))
                {
                    //se for romaneio de terceiro e já estava finalizado, não atualiza o status do romaneio para não voltar o estoque
                    rom.LX_STATUS_ROMANEIO = statusRomaneio.LX_STATUS_ROMANEIO;
                    rom.LX_STATUS_DOCUMENTO = statusRomaneio.LX_STATUS_DOCUMENTO;
                }

                if (romaneio.LxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Autorizado.Value) || romaneio.LxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.NfNaoEletronica.Value))
                {
                    rom.STK_ROMANEIO_NF_LISTA.LX_STATUS_NFE = romaneio.LxStatusNfe;
                    rom.STK_ROMANEIO_NF_LISTA.NF_DANFE_CHAVE = romaneio.NfDanfeChave;
                    rom.STK_ROMANEIO_NF_LISTA.NF_NUMERO = romaneio.NfNumero;
                    rom.STK_ROMANEIO_NF_LISTA.NF_SERIE = romaneio.NfSerie;
                    rom.STK_ROMANEIO_NF_LISTA.NR_AVISO_RECEBTO = romaneio.NrAvisoRecebto;
                    rom.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_ENTRADA = romaneio.ExidNotaFiscalEntrada;
                    rom.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_SAIDA = romaneio.ExidNotaFiscalSaida;

                    if (rom.STK_ROMANEIO_NF_LISTA.DATA_EMISSAO == null)
                        rom.STK_ROMANEIO_NF_LISTA.DATA_EMISSAO = rom.DATA_STK_MOV;

                }
                else
                {
                    rom.STK_ROMANEIO_NF_LISTA.LX_STATUS_NFE = romaneio.LxStatusNfe;
                    if (romaneio.LxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Cancelado.Value) || romaneio.LxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Inutilizado.Value) || romaneio.LxStatusNfe == Convert.ToByte(Domains.LX_STATUS_NFE.Denegado.Value))
                    {
                        foreach (var itemRom in rom.STK_ROMANEIO_ITEM_LISTA)
                        {
                            foreach (var itemRelacao in itemRom.STK_ROMANEIO_ITEM_RELACAO_LISTA)
                            {
                                if (itemRelacao.NTS_ROMANEIO_ITEM_RELACIONADO != null)
                                    itemRelacao.NTS_ROMANEIO_ITEM_RELACIONADO.QTDE_ROMANEIO_ITEM_RETORNADO = itemRelacao.NTS_ROMANEIO_ITEM_RELACIONADO.QTDE_ROMANEIO_ITEM_RETORNADO - itemRelacao.QTDE_ROMANEIO_ITEM;
                            }
                        }
                    }
                    rom.STK_ROMANEIO_NF_LISTA.NF_DANFE_CHAVE = romaneio.NfDanfeChave;
                    rom.STK_ROMANEIO_NF_LISTA.NF_NUMERO = romaneio.NfNumero;
                    rom.STK_ROMANEIO_NF_LISTA.NF_SERIE = romaneio.NfSerie;
                    rom.STK_ROMANEIO_NF_LISTA.NR_AVISO_RECEBTO = romaneio.NrAvisoRecebto;
                    rom.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_ENTRADA = romaneio.ExidNotaFiscalEntrada;
                    rom.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_SAIDA = romaneio.ExidNotaFiscalSaida;

                    if (rom.STK_ROMANEIO_NF_LISTA.DATA_EMISSAO == null)
                        rom.STK_ROMANEIO_NF_LISTA.DATA_EMISSAO = rom.DATA_STK_MOV;
                }
            }

            contextoOperacional.ChangeTracker.DetectChanges();
            contextoOperacional.SaveChanges();
            return true;
        }

        /// <summary>
        /// Utilizar para confirmação de entrada no estoque para notas recebidas
        /// </summary>
        /// <param name="idStkRomaneio"></param>
        /// <param name="dataMovimentacaoEstoque"></param>
        public void ConfirmarRomaneioRecebimentoEntrada(Int64 idStkRomaneio, DateTime? dataMovimentacaoEstoque = null)
        {
            LinxOperacional contextoOperacional = new LinxOperacional();

            //aumentando timeout até termos solução definitiva na trigger da STK_ROMANEIO_ITEM 
            ((IObjectContextAdapter)contextoOperacional).ObjectContext.CommandTimeout = 600;

            var rom = contextoOperacional.STK_ROMANEIO
                .Include("STK_ROMANEIO_NF_LISTA")
                .Include("STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA")
                .Include("STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.NTS_ROMANEIO_ITEM_RELACIONADO")
                .Where(r => r.ID_STK_ROMANEIO == idStkRomaneio)
                .FirstOrDefault();

            //comentado pois existe a situação de nota própria emitida por um outro sistema e a nota é importada no UX - nesse caso será um recebimento de XML
            ////se o romaneio é de entrada de terceiro e já estava finalizado, não atualiza os status do romaneio, pois o usuário pode ter finalizado manualmente, senão, atualiza
            //if (rom.STK_ROMANEIO_NF_LISTA.LX_TIPO_EMISSAO == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Terceiro.Value))
            //{
            rom.DATA_STK_MOV = (dataMovimentacaoEstoque ?? DateTime.Now);
            rom.LX_STATUS_ROMANEIO = Convert.ToByte(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value);
            rom.LX_STATUS_DOCUMENTO = Convert.ToByte(Domains.LX_STATUS_DOCUMENTO_ROMANEIO.Finalizado.Value);

            contextoOperacional.ChangeTracker.DetectChanges();
            contextoOperacional.SaveChanges();
            //}
        }
    }
}
