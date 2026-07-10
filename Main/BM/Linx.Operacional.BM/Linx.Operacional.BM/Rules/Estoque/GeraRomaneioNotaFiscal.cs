using Linx.Documento.BM.Rules.Sequencial;
using Linx.Operacional.BM.Rules.PedidoEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Transactions;
using System.Data.Entity.Infrastructure;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class GeraRomaneioNotaFiscal
    {
        public BM.GERA_ROMANEIO_RETORNO GeraRomaneioToRomaneio(GERA_ROMANEIO geraRomaneio)
        {
            BM.GERA_ROMANEIO_RETORNO retorno = new GERA_ROMANEIO_RETORNO() { ID_GERA_ROMANEIO_RETORNO = 1 };

            using (LinxOperacional contexto = new LinxOperacional())
            {
                //aumentando timeout até termos solução definitiva na trigger da STK_ROMANEIO_ITEM 
                ((IObjectContextAdapter)contexto).ObjectContext.CommandTimeout = 600;

                RepositorioRomaneio repositorioRomaneio = new RepositorioRomaneio(contexto);

                #region Validações


                long idNota; bool indicaSaida;

                if (geraRomaneio.EX_ID_NOTA_FISCAL_ENTRADA != null && geraRomaneio.EX_ID_NOTA_FISCAL_SAIDA != null)
                {
                    if (geraRomaneio.EX_ID_NOTA_FISCAL_SAIDA != null)
                    {
                        idNota = (long)geraRomaneio.EX_ID_NOTA_FISCAL_SAIDA;
                        indicaSaida = true;
                    }
                    else
                    {
                        idNota = (long)geraRomaneio.EX_ID_NOTA_FISCAL_ENTRADA;
                        indicaSaida = false;
                    }
                    if (this.ExisteRomaneio(repositorioRomaneio, idNota, indicaSaida))
                        throw new Exception("Já existe um Romaneio para a nota");
                }

                TBC_TRANSPORTADORA transportadora = new TBC_TRANSPORTADORA();

                if (geraRomaneio.CNPJ_FILIAL_ROMANEIO.IsNullOrEmpty())
                    throw new Exception("CNPJ da filial não foi informado");

                TBC_FILIAL filial = contexto.TBC_FILIAL.FirstOrDefault(p => p.TBC_PFJ.CNPJ_CPF == geraRomaneio.CNPJ_FILIAL_ROMANEIO);
                if (filial == null)
                    throw new Exception("Não existe filial cadastrada para o CNPJ informado");

                if (geraRomaneio.ID_LOJA_ROMANEIO.IsNullOrEmpty())
                    throw new Exception("Id da loja não foi informado");

                LJV_LOJA lojaRomaneio = contexto.LJV_LOJA.FirstOrDefault(p => p.ID_FILIAL_PFJ == filial.ID_FILIAL_PFJ && p.ID_LOJA == geraRomaneio.ID_LOJA_ROMANEIO);
                if (lojaRomaneio == null)
                    throw new Exception("Não existe loja cadastrada para o código informado");

                if (!geraRomaneio.CNPJ_TRANSPORTADORA.IsNullOrEmpty())
                {
                    transportadora = contexto.TBC_TRANSPORTADORA.FirstOrDefault(p => p.TBC_PFJ.CNPJ_CPF == geraRomaneio.CNPJ_TRANSPORTADORA);
                    if (transportadora == null)
                        throw new Exception("Não existe transportadora cadastrada para o CNPJ informado");
                }

                Int64 idTerceiro = 0;
                bool eClienteVarejo = false;
                TBC_PFJ pfjTerceiro = null;
                if (!geraRomaneio.CPF_CNPJ_TERCEIRO.IsNullOrEmpty())
                {
                    pfjTerceiro = contexto.TBC_PFJ.FirstOrDefault(p => p.CNPJ_CPF == geraRomaneio.CPF_CNPJ_TERCEIRO);
                    if (pfjTerceiro == null)
                    {
                        var clienteVarejo = contexto.CRM_PFJ.FirstOrDefault(p => p.CNPJ_CPF == geraRomaneio.CPF_CNPJ_TERCEIRO);
                        if (clienteVarejo == null)
                            throw new Exception("Não existe Terceiro cadastrado para o CNPJ informado.");
                        else
                        {
                            eClienteVarejo = true;
                            idTerceiro = clienteVarejo.ID_CRM_PFJ;
                        }
                    }
                    else
                        idTerceiro = pfjTerceiro.ID_PFJ;
                }

                if (lojaRomaneio.ID_STK_DEPOSITO.IsNullOrEmpty())
                    throw new Exception("Não existe Depósito para a Loja.");
                #endregion

                var txOptions = new System.Transactions.TransactionOptions();
                txOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txOptions))
                {
                    try
                    {
                        //Preenche os dados do romaneio
                        STK_ROMANEIO romaneio = new STK_ROMANEIO();
                        this.preencheStkRomaneio(romaneio, geraRomaneio, filial, lojaRomaneio);
                        contexto.STK_ROMANEIO.Add(romaneio);
                        contexto.SaveChanges();

                        //Preenche os dados do Romaneio NF
                        STK_ROMANEIO_NF romaneioNF = new STK_ROMANEIO_NF();
                        this.preencheStkRomaneioNf(romaneioNF, geraRomaneio, filial, eClienteVarejo, idTerceiro, transportadora, romaneio.ID_STK_ROMANEIO);
                        contexto.STK_ROMANEIO_NF.Add(romaneioNF);
                        contexto.SaveChanges();

                        bool indicaNotaImportacao = false;
                        if (romaneioNF.LX_TIPO_EMISSAO == Convert.ToByte(Domains.LX_TIPO_EMISSAO.Propria.Value) && pfjTerceiro != null && pfjTerceiro.UF == "EX") indicaNotaImportacao = true;

                        if (romaneioNF.EX_ID_NOTA_FISCAL_ENTRADA == null && romaneioNF.EX_ID_NOTA_FISCAL_SAIDA == null) //Quando não for romaneio de nota - é utilizado esse método na geração de romaneio pela sugestão de compra 
                        {
                            romaneio.LX_STATUS_DOCUMENTO = Convert.ToByte(Domains.LX_STATUS_DOCUMENTO_ROMANEIO.EmElaboracao.Value);
                            romaneio.LX_STATUS_ROMANEIO = Convert.ToByte(Domains.LX_STATUS_ROMANEIO.NaoConsideraEstoque.Value);
                        }
                        else
                        {
                            bool indicaRecebimentoNF = (romaneioNF.LX_STATUS_NFE == Convert.ToByte(Domains.LX_STATUS_NFE.RecebimentoNFeEntrada.Value));
                            var situacaoRomaneio = AtualizacaoNotaRomaneio.RetornaSituacaoStatusRomaneio((romaneioNF.LX_STATUS_NFE ?? 1), romaneioNF.LX_TIPO_EMISSAO, indicaNotaImportacao, indicaRecebimentoNF);
                            romaneio.LX_STATUS_ROMANEIO = situacaoRomaneio.LX_STATUS_ROMANEIO;
                            romaneio.LX_STATUS_DOCUMENTO = situacaoRomaneio.LX_STATUS_DOCUMENTO;
                        }


                        if (geraRomaneio.GERA_ROMANEIO_ITEM_LISTA.Count(f => f.ID_SKU.IsNullOrEmpty()) > 0)
                            throw new Exception("Existem itens na Nota Fiscal sem SKU informado.");

                        PreencheSTK_ROMANEIO_ITEM(contexto, romaneio, geraRomaneio);
                        List<STK_ROMANEIO_ITEM> romaneioItens = contexto.STK_ROMANEIO_ITEM.Where(f => f.ID_STK_ROMANEIO == romaneio.ID_STK_ROMANEIO).ToList();
                        PreencheSTK_ROMANEIO_ITEM_RELACAO(contexto, geraRomaneio, romaneioItens);
                        romaneioItens = contexto.STK_ROMANEIO_ITEM.Where(f => f.ID_STK_ROMANEIO == romaneio.ID_STK_ROMANEIO).ToList();
                        PreencheSTK_ROMANEIO_DETALHE(contexto, geraRomaneio, romaneioItens);
                        romaneioItens = contexto.STK_ROMANEIO_ITEM.Where(f => f.ID_STK_ROMANEIO == romaneio.ID_STK_ROMANEIO).ToList();

                        contexto.SaveChanges();

                        retorno.COD_DEPOSITO = lojaRomaneio.STK_DEPOSITO.COD_DEPOSITO;
                        retorno.ID_LINX = filial.ID_LINX;
                        retorno.ID_STK_DEPOSITO = lojaRomaneio.ID_STK_DEPOSITO;
                        retorno.ID_STK_ROMANEIO = romaneio.ID_STK_ROMANEIO;
                        retorno.LX_STATUS_ROMANEIO = romaneio.LX_STATUS_ROMANEIO;
                        retorno.NUMERO_ROMANEIO = romaneio.NUMERO_ROMANEIO;

                        List<GERA_ROMANEIO_RETORNO_ITEM> lstRomaneioRetornoItem = new List<GERA_ROMANEIO_RETORNO_ITEM>();
                        int count = 1;
                        foreach (var item in romaneio.STK_ROMANEIO_ITEM_LISTA)
                        {
                            lstRomaneioRetornoItem.Add(new GERA_ROMANEIO_RETORNO_ITEM()
                            {
                                ID_GERA_ROMANEIO_RETORNO = 1,
                                ID_GERA_ROMANEIO_RETORNO_ITEM = count,
                                ID_SKU = item.ID_SKU,
                                ID_STK_ROMANEIO_ITEM = item.ID_STK_ROMANEIO_ITEM,
                                ID_LGE_PEDIDO_ITEM = item.ID_LGE_PEDIDO_ITEM,
                                QTDE_ROMANEIO_ITEM = item.QTDE_ROMANEIO_ITEM
                            });
                            count++;
                        }
                        retorno.GERA_ROMANEIO_RETORNO_ITEM_LISTA = lstRomaneioRetornoItem;

                        transaction.Complete();

                        return retorno;
                    }
                    catch
                    {
                        transaction.Dispose();
                        throw;
                    }
                }
            }
        }

        private static void PreencheSTK_ROMANEIO_DETALHE(LinxOperacional contexto, GERA_ROMANEIO geraRomaneio, List<STK_ROMANEIO_ITEM> romaneioItens)
        {
            List<STK_ROMANEIO_DETALHE> romaneioItensDetalhe = new List<STK_ROMANEIO_DETALHE>();

            //Tratamento para Número de Série
            foreach (GERA_ROMANEIO_ITEM itemGeraRomaneio in geraRomaneio.GERA_ROMANEIO_ITEM_LISTA.Where(f => !f.NUMERO_SERIE.IsNullOrEmpty()).ToList())
            {
                var romaneioItem = romaneioItens.Where(f => f.ID_SKU == itemGeraRomaneio.ID_SKU && f.QTDE_ROMANEIO_ITEM == itemGeraRomaneio.QTDE_ROMANEIO_ITEM && f.VALOR_ROMANEIO_ITEM == Math.Round((decimal)itemGeraRomaneio.VALOR_ROMANEIO_ITEM, 2) && f.QTDE_ROMANEIO_ITEM_RETORNADO == itemGeraRomaneio.QTDE_ROMANEIO_ITEM_RETORNADO).FirstOrDefault();
                romaneioItens.Remove(romaneioItem);

                STK_ROMANEIO_DETALHE romaneioDetalhe = new STK_ROMANEIO_DETALHE()
                {
                    ID_LINX = romaneioItem.ID_LINX,
                    ID_STK_ROMANEIO_ITEM = romaneioItem.ID_STK_ROMANEIO_ITEM,
                    NUMERO_SERIE = itemGeraRomaneio.NUMERO_SERIE
                };
                romaneioItem.STK_ROMANEIO_DETALHE_LISTA = romaneioDetalhe;
                romaneioItensDetalhe.Add(romaneioDetalhe);
            }

            List<string> comandos = GetComandos_STK_ROMANEIO_DETALHE(romaneioItensDetalhe);
            foreach (var comando in comandos)
            {
                contexto.Database.ExecuteSqlCommand(comando);
            }
        }

        private static void PreencheSTK_ROMANEIO_ITEM_RELACAO(LinxOperacional contexto, GERA_ROMANEIO geraRomaneio, List<STK_ROMANEIO_ITEM> romaneioItens)
        {
            RepositorioRomaneio repositorioRomaneio = new RepositorioRomaneio(contexto);
            List<STK_ROMANEIO_NF_RELACIONADA> notasRelacionadasContexto = new List<STK_ROMANEIO_NF_RELACIONADA>();
            List<STK_ROMANEIO_ITEM_RELACAO> romaneioItensRelacao = new List<STK_ROMANEIO_ITEM_RELACAO>();

            foreach (GERA_ROMANEIO_ITEM itemGeraRomaneio in geraRomaneio.GERA_ROMANEIO_ITEM_LISTA.Where(f => f.GERA_ROMANEIO_ITEM_NF_RELACIONADA_LISTA != null).ToList())
            {
                var romaneioItem = romaneioItens.Where(f => f.ID_SKU == itemGeraRomaneio.ID_SKU && f.QTDE_ROMANEIO_ITEM == itemGeraRomaneio.QTDE_ROMANEIO_ITEM && f.VALOR_ROMANEIO_ITEM == itemGeraRomaneio.VALOR_ROMANEIO_ITEM && f.QTDE_ROMANEIO_ITEM_RETORNADO == itemGeraRomaneio.QTDE_ROMANEIO_ITEM_RETORNADO).FirstOrDefault();
                romaneioItens.Remove(romaneioItem);

                var idsRomaneioItem = itemGeraRomaneio.GERA_ROMANEIO_ITEM_NF_RELACIONADA_LISTA.Where(f => f.ID_STK_ROMANEIO_ITEM_RELACIONADO != null).Select(f => (Int64)f.ID_STK_ROMANEIO_ITEM_RELACIONADO).ToList();
                if (idsRomaneioItem.Count > 0)
                {
                    var romaneiosItemOrigem = repositorioRomaneio.GetRomaneioItens(idsRomaneioItem);

                    decimal qtdeARetornar = itemGeraRomaneio.QTDE_ROMANEIO_ITEM;
                    foreach (var item in romaneiosItemOrigem)
                    {
                        var qtdePendente = (item.QTDE_ROMANEIO_ITEM - (item.QTDE_ROMANEIO_ITEM_RETORNADO ?? (decimal)0));
                        if (qtdeARetornar > 0 && qtdePendente > 0)
                        {
                            STK_ROMANEIO_ITEM_RELACAO romaneioItemRelacionada = new STK_ROMANEIO_ITEM_RELACAO();

                            romaneioItemRelacionada = new STK_ROMANEIO_ITEM_RELACAO()
                            {
                                ID_LINX = romaneioItem.ID_LINX,
                                ID_STK_ROMANEIO_ITEM = romaneioItem.ID_STK_ROMANEIO_ITEM,
                                ID_STK_ROMANEIO_ITEM_RELACIONADO = item.ID_STK_ROMANEIO_ITEM
                            };

                            if (qtdeARetornar <= qtdePendente)
                            {
                                item.QTDE_ROMANEIO_ITEM_RETORNADO = (item.QTDE_ROMANEIO_ITEM_RETORNADO ?? 0) + qtdeARetornar;
                                romaneioItemRelacionada.QTDE_ROMANEIO_ITEM = qtdeARetornar;
                                qtdeARetornar = 0;
                            }
                            else
                            {
                                item.QTDE_ROMANEIO_ITEM_RETORNADO = (item.QTDE_ROMANEIO_ITEM_RETORNADO ?? 0) + qtdePendente;
                                romaneioItemRelacionada.QTDE_ROMANEIO_ITEM = qtdePendente;
                                qtdeARetornar = qtdeARetornar - qtdePendente;
                            }

                            romaneioItensRelacao.Add(romaneioItemRelacionada);
                        }
                    }
                }

                foreach (var relacionadas in itemGeraRomaneio.GERA_ROMANEIO_ITEM_NF_RELACIONADA_LISTA.Where(f => f.ID_STK_ROMANEIO_ITEM_RELACIONADO == null))
                {
                    STK_ROMANEIO_ITEM_RELACAO romaneioItemRelacionada = new STK_ROMANEIO_ITEM_RELACAO();
                    var notaRelacionada = repositorioRomaneio.GetRomaneioNFRelacionada((relacionadas.NUMERO_NF == null ? (Int64)0 : (Int64)relacionadas.NUMERO_NF), relacionadas.SERIE_NF, relacionadas.CNPJ_EMITENTE_NF, relacionadas.ID_MODELO_FISCAL, relacionadas.CHAVE_ACESSO_NF);
                    if (notaRelacionada == null)
                        notaRelacionada = GetRomaneioNFRelacionadaNoContexto(notasRelacionadasContexto, (relacionadas.NUMERO_NF == null ? (Int64)0 : (Int64)relacionadas.NUMERO_NF), relacionadas.SERIE_NF, relacionadas.CNPJ_EMITENTE_NF, relacionadas.ID_MODELO_FISCAL, relacionadas.CHAVE_ACESSO_NF);

                    if (notaRelacionada == null)
                    {
                        notaRelacionada = new STK_ROMANEIO_NF_RELACIONADA()
                        {
                            CHAVE_ACESSO_NF = relacionadas.CHAVE_ACESSO_NF,
                            CNPJ_EMITENTE_NF = relacionadas.CNPJ_EMITENTE_NF,
                            ID_MODELO_FISCAL = (int)relacionadas.ID_MODELO_FISCAL,
                            DATA_EMISSAO_NF = relacionadas.DATA_EMISSAO_NF,
                            SERIE_NF = relacionadas.SERIE_NF,
                            NUMERO_NF = (relacionadas.NUMERO_NF == null ? (Int64)0 : (Int64)relacionadas.NUMERO_NF),
                            ID_LINX = romaneioItem.ID_LINX
                        };

                        var idPfj = repositorioRomaneio.GetIdPfj(relacionadas.CNPJ_EMITENTE_NF);
                        if (!idPfj.IsNullOrEmpty())
                            notaRelacionada.ID_PFJ_EMITENTE = idPfj;

                        notasRelacionadasContexto.Add(notaRelacionada);
                    }

                    romaneioItemRelacionada = new STK_ROMANEIO_ITEM_RELACAO()
                    {
                        ID_LINX = romaneioItem.ID_LINX,
                        ID_STK_ROMANEIO_ITEM = romaneioItem.ID_STK_ROMANEIO_ITEM,
                        STK_ROMANEIO_NF_RELACIONADA = notaRelacionada
                    };

                    romaneioItensRelacao.Add(romaneioItemRelacionada);
                }
            }

            List<string> comandos = GetComandos_STK_ROMANEIO_ITEM_RELACAO(romaneioItensRelacao);
            foreach (var comando in comandos)
            {
                contexto.Database.ExecuteSqlCommand(comando);
            }
        }

        private static void PreencheSTK_ROMANEIO_ITEM(LinxOperacional contexto, STK_ROMANEIO romaneio, GERA_ROMANEIO geraRomaneio)
        {
            List<STK_ROMANEIO_ITEM> romaneioItens = new List<STK_ROMANEIO_ITEM>();
            foreach (GERA_ROMANEIO_ITEM itemGeraRomaneio in geraRomaneio.GERA_ROMANEIO_ITEM_LISTA)
            {
                STK_ROMANEIO_ITEM romaneioItem = new STK_ROMANEIO_ITEM();
                romaneioItem.ID_LINX = romaneio.ID_LINX;
                romaneioItem.ID_STK_ROMANEIO = romaneio.ID_STK_ROMANEIO;
                romaneioItem.LX_STATUS_ROMANEIO = romaneio.LX_STATUS_ROMANEIO;
                romaneioItem.DATA_STK_MOV = romaneio.DATA_STK_MOV;
                romaneioItem.ID_SKU = itemGeraRomaneio.ID_SKU;
                romaneioItem.ID_STK_DEPOSITO = romaneio.LJV_LOJA.ID_STK_DEPOSITO.GetValueOrDefault();
                romaneioItem.LX_FATOR_STK_MOV_QTDE = itemGeraRomaneio.LX_FATOR_STK_MOV_QTDE;
                romaneioItem.LX_FATOR_STK_MOV_VALOR = itemGeraRomaneio.LX_FATOR_STK_MOV_VALOR;
                romaneioItem.QTDE_ROMANEIO_ITEM = itemGeraRomaneio.QTDE_ROMANEIO_ITEM;
                romaneioItem.VALOR_ROMANEIO_ITEM = itemGeraRomaneio.VALOR_ROMANEIO_ITEM;
                romaneioItem.VALOR_COMPOE_MEDIO = itemGeraRomaneio.VALOR_COMPOE_MEDIO;
                romaneioItem.VALOR_DESCONTO_ITEM = itemGeraRomaneio.VALOR_DESCONTO_ITEM;
                romaneioItem.VALOR_ACRESCIMO_ITEM = itemGeraRomaneio.VALOR_ACRESCIMO_ITEM;
                romaneioItem.QTDE_ROMANEIO_ITEM_RETORNADO = itemGeraRomaneio.QTDE_ROMANEIO_ITEM_RETORNADO;
                romaneioItem.VALOR_ADICIONAL_CM_GERENCIAL = itemGeraRomaneio.VALOR_ADICIONAL_CM_GERENCIAL;

                if (itemGeraRomaneio.NUMERO_PEDIDO_COMPRA != null && itemGeraRomaneio.NUMERO_PEDIDO_COMPRA != 0)
                {
                    if (!itemGeraRomaneio.ID_LGE_PEDIDO_ITEM.IsNullOrEmpty())
                    {
                        new RegraBaixaItemPedido().BaixarItemPedidoPorIdLgePedidoItem((int)itemGeraRomaneio.ID_LGE_PEDIDO_ITEM, itemGeraRomaneio.QTDE_ROMANEIO_ITEM, contexto);
                        romaneioItem.ID_LGE_PEDIDO_ITEM = itemGeraRomaneio.ID_LGE_PEDIDO_ITEM;
                    }
                    else
                    {
                        int? idLgePedidoItem = new RegraBaixaItemPedido().BaixarItemPedidoPorIdSku(itemGeraRomaneio.NUMERO_PEDIDO_COMPRA.GetValueOrDefault(), romaneio.ID_FILIAL_PFJ, itemGeraRomaneio.ID_SKU, itemGeraRomaneio.QTDE_ROMANEIO_ITEM, contexto);
                        if (idLgePedidoItem != null)
                            romaneioItem.ID_LGE_PEDIDO_ITEM = idLgePedidoItem;
                    }
                }
                romaneioItens.Add(romaneioItem);
            }

            List<string> comandos = GetComandos_STK_ROMANEIO_ITEM(romaneioItens);
            foreach (var comando in comandos)
            {
                contexto.Database.ExecuteSqlCommand(comando);
            }
        }

        private static List<string> GetComandos_STK_ROMANEIO_ITEM(List<STK_ROMANEIO_ITEM> romaneioItemLista)
        {
            List<string> comandos = new List<string>();
            string comandoFixo = "";
            string comandoAux = "";

            foreach (var item in romaneioItemLista)
            {
                comandoAux = "(";
                comandoAux += "" + item.ID_LINX.ToString();
                comandoAux += ", " + item.ID_STK_ROMANEIO.ToString();
                comandoAux += ", " + item.ID_SKU.ToString();
                comandoAux += ", " + item.ID_STK_DEPOSITO.ToString();
                comandoAux += ", " + item.LX_FATOR_STK_MOV_QTDE.ToString();
                comandoAux += ", " + item.LX_FATOR_STK_MOV_VALOR.ToString();
                comandoAux += ", " + (item.QTDE_ROMANEIO_ITEM.IsNullOrEmpty() ? "0" : item.QTDE_ROMANEIO_ITEM.ToString().Replace(",", "."));
                comandoAux += ", " + (item.VALOR_COMPOE_MEDIO.IsNullOrEmpty() ? "NULL" : item.VALOR_COMPOE_MEDIO.ToString().Replace(",", "."));
                comandoAux += ", " + (item.VALOR_ROMANEIO_ITEM.IsNullOrEmpty() ? "NULL" : item.VALOR_ROMANEIO_ITEM.ToString().Replace(",", "."));
                comandoAux += ", " + (item.VALOR_DESCONTO_ITEM.IsNullOrEmpty() ? "NULL" : item.VALOR_DESCONTO_ITEM.ToString().Replace(",", "."));
                comandoAux += ", " + (item.VALOR_ACRESCIMO_ITEM.IsNullOrEmpty() ? "NULL" : item.VALOR_ACRESCIMO_ITEM.ToString().Replace(",", "."));
                comandoAux += ", " + (item.ID_STK_CUSTO.IsNullOrEmpty() ? "NULL" : item.ID_STK_CUSTO.ToString());
                comandoAux += ", " + (item.ID_STK_DEPOSITO_DESTINO.IsNullOrEmpty() ? "NULL" : item.ID_STK_DEPOSITO_DESTINO.ToString());
                comandoAux += ", " + (item.ID_STK_ROMANEIO_ITEM_RELACIONADO.IsNullOrEmpty() ? "NULL" : item.ID_STK_ROMANEIO_ITEM_RELACIONADO.ToString());
                comandoAux += ", " + (item.QTDE_ROMANEIO_ITEM_RETORNADO.IsNullOrEmpty() ? "NULL" : item.QTDE_ROMANEIO_ITEM_RETORNADO.ToString().Replace(",", "."));
                comandoAux += ", " + (item.ID_LGE_PEDIDO_ITEM.IsNullOrEmpty() ? "NULL" : item.ID_LGE_PEDIDO_ITEM.ToString());
                comandoAux += ", " + (item.LX_STATUS_ROMANEIO.IsNullOrEmpty() ? "NULL" : item.LX_STATUS_ROMANEIO.ToString());
                comandoAux += ", '" + ((DateTime)item.DATA_STK_MOV).ToString("yyyyMMdd") + "'";
                comandoAux += ", " + (item.INDICA_CONSUMO_FINAL == true ? "1" : "0");
                comandoAux += ", " + (item.VALOR_ADICIONAL_CM_GERENCIAL.IsNullOrEmpty() ? "0" : item.VALOR_ADICIONAL_CM_GERENCIAL.ToString().Replace(",", "."));
                comandoAux += ")";
                comandos.Add(comandoAux);
            }

            comandoFixo = "INSERT INTO [LX_STK].[STK_ROMANEIO_ITEM]";
            comandoFixo += "([ID_LINX]";
            comandoFixo += ",[ID_STK_ROMANEIO]";
            comandoFixo += ",[ID_SKU]";
            comandoFixo += ",[ID_STK_DEPOSITO]";
            comandoFixo += ",[LX_FATOR_STK_MOV_QTDE]";
            comandoFixo += ",[LX_FATOR_STK_MOV_VALOR]";
            comandoFixo += ",[QTDE_ROMANEIO_ITEM]";
            comandoFixo += ",[VALOR_COMPOE_MEDIO]";
            comandoFixo += ",[VALOR_ROMANEIO_ITEM]";
            comandoFixo += ",[VALOR_DESCONTO_ITEM]";
            comandoFixo += ",[VALOR_ACRESCIMO_ITEM]";
            comandoFixo += ",[ID_STK_CUSTO]";
            comandoFixo += ",[ID_STK_DEPOSITO_DESTINO]";
            comandoFixo += ",[ID_STK_ROMANEIO_ITEM_RELACIONADO]";
            comandoFixo += ",[QTDE_ROMANEIO_ITEM_RETORNADO]";
            comandoFixo += ",[ID_LGE_PEDIDO_ITEM]";
            comandoFixo += ",[LX_STATUS_ROMANEIO]";
            comandoFixo += ",[DATA_STK_MOV]";
            comandoFixo += ",[INDICA_CONSUMO_FINAL]";
            comandoFixo += ",[VALOR_ADICIONAL_CM_GERENCIAL])";
            comandoFixo += "VALUES \n";

            return ExecutaComandosPaginados(comandos, comandoFixo);
        }

        private static List<string> GetComandos_STK_ROMANEIO_ITEM_RELACAO(List<STK_ROMANEIO_ITEM_RELACAO> romaneioItemRelacaoLista)
        {
            List<string> comandos = new List<string>();
            string comandoFixo = "";
            string comandoAux = "";

            foreach (var item in romaneioItemRelacaoLista)
            {
                comandoAux = "(";
                comandoAux += "" + item.ID_LINX.ToString();
                comandoAux += ", " + item.ID_STK_ROMANEIO_ITEM.ToString();
                comandoAux += ", " + (item.ID_ROMANEIO_NF_RELACIONADA.IsNullOrEmpty() ? "NULL" : item.ID_ROMANEIO_NF_RELACIONADA.ToString());
                comandoAux += ", " + (item.ID_STK_ROMANEIO_ITEM_RELACIONADO.IsNullOrEmpty() ? "NULL" : item.ID_STK_ROMANEIO_ITEM_RELACIONADO.ToString());
                comandoAux += ", " + (item.QTDE_ROMANEIO_ITEM.IsNullOrEmpty() ? "NULL" : item.QTDE_ROMANEIO_ITEM.ToString().Replace(",", "."));
                comandoAux += ")";
                comandos.Add(comandoAux);
            }

            comandoFixo = "INSERT INTO [LX_STK].[STK_ROMANEIO_ITEM_RELACAO]";
            comandoFixo += "([ID_LINX]";
            comandoFixo += ",[ID_STK_ROMANEIO_item]";
            comandoFixo += ",[ID_ROMANEIO_NF_RELACIONADA]";
            comandoFixo += ",[ID_STK_ROMANEIO_ITEM_RELACIONADO]";
            comandoFixo += ",[QTDE_ROMANEIO_ITEM])";
            comandoFixo += "VALUES \n";

            return ExecutaComandosPaginados(comandos, comandoFixo);
        }

        private static List<string> GetComandos_STK_ROMANEIO_DETALHE(List<STK_ROMANEIO_DETALHE> romaneioItemRelacaoLista)
        {
            List<string> comandos = new List<string>();
            string comandoFixo = "";
            string comandoAux = "";

            foreach (var item in romaneioItemRelacaoLista)
            {
                comandoAux = "(";
                comandoAux += "" + item.ID_LINX.ToString();
                comandoAux += ", " + item.ID_STK_ROMANEIO_ITEM.ToString();
                comandoAux += ", '" + item.NUMERO_SERIE.ToString() + "'";
                comandoAux += ")";
                comandos.Add(comandoAux);
            }

            comandoFixo = "INSERT INTO [LX_STK].[STK_ROMANEIO_DETALHE]";
            comandoFixo += "([ID_LINX]";
            comandoFixo += ",[ID_STK_ROMANEIO_item]";
            comandoFixo += ",[NUMERO_SERIE])";
            comandoFixo += "VALUES \n";

            return ExecutaComandosPaginados(comandos, comandoFixo);
        }

        private static List<string> ExecutaComandosPaginados(List<string> listaComandos, string comandoInsertFixo = null)
        {
            List<string> listaComandosNew = new List<string>();
            List<string> instrucoes = listaComandos;
            int qtdPag = 1000;
            int numReg = instrucoes.Count;
            int numPag = Convert.ToInt32(numReg / qtdPag) + 1;

            if (numReg > 0)
            {
                for (int pagina = 0; pagina < numPag; pagina++)
                {
                    string comandoLinha = "";
                    for (int i = (pagina * qtdPag); i < ((pagina * qtdPag) + qtdPag); i++)
                    {
                        if (instrucoes.Count > i)
                        {
                            if (comandoLinha != "") comandoLinha = comandoLinha + ", ";
                            comandoLinha = comandoLinha + instrucoes[i] + "\n ";
                        }
                        else
                            break;
                    }
                    if (!string.IsNullOrEmpty(comandoLinha))
                    {
                        listaComandosNew.Add(comandoInsertFixo + comandoLinha);
                    }
                }
            }

            return listaComandosNew;
        }

        void preencheStkRomaneioNf(STK_ROMANEIO_NF romaneioNF, GERA_ROMANEIO geraRomaneio, TBC_FILIAL filial, bool clienteVarejo, Int64 idTerceiro, TBC_TRANSPORTADORA transportadora, long idStkRomaneio)
        {
            romaneioNF.ESPECIE_VOLUMES = geraRomaneio.ESPECIE_VOLUMES;
            romaneioNF.EX_ID_NOTA_FISCAL_ENTRADA = geraRomaneio.EX_ID_NOTA_FISCAL_ENTRADA;
            romaneioNF.EX_ID_NOTA_FISCAL_SAIDA = geraRomaneio.EX_ID_NOTA_FISCAL_SAIDA;
            romaneioNF.ID_LINX = filial.ID_LINX;
            if (clienteVarejo)
                romaneioNF.ID_CRM_PFJ = idTerceiro;
            else
                romaneioNF.ID_PFJ = Convert.ToInt32(idTerceiro);
            romaneioNF.ID_STK_ROMANEIO = idStkRomaneio;
            if (transportadora != null && transportadora.ID_TRANSPORTADORA != 0)
                romaneioNF.ID_TRANSPORTADORA = transportadora.ID_TRANSPORTADORA;
            romaneioNF.LX_MODALIDADE_FRETE = geraRomaneio.LX_MODALIDADE_FRETE;
            romaneioNF.LX_STATUS_NFE = geraRomaneio.LX_STATUS_NFE;
            romaneioNF.LX_TIPO_EMISSAO = geraRomaneio.LX_TIPO_EMISSAO;
            romaneioNF.MARCA_VOLUMES = geraRomaneio.MARCA_VOLUMES;
            romaneioNF.NF_DANFE_CHAVE = geraRomaneio.NF_DANFE_CHAVE;
            romaneioNF.NF_NUMERO = geraRomaneio.NF_NUMERO;
            romaneioNF.NF_SERIE = geraRomaneio.NF_SERIE;
            romaneioNF.NR_AVISO_RECEBTO = geraRomaneio.NR_AVISO_RECEBTO;
            romaneioNF.NUMERO_VOLUMES = geraRomaneio.NUMERO_VOLUMES;
            romaneioNF.OBS_NOTA_FISCAL = geraRomaneio.OBS_NOTA_FISCAL;
            romaneioNF.PLACA_VEICULO = geraRomaneio.PLACA_VEICULO;
            romaneioNF.QTDE_VOLUMES = geraRomaneio.QTDE_VOLUMES;
            romaneioNF.UF_PLACA_VEICULO = geraRomaneio.UF_PLACA_VEICULO;
            romaneioNF.VALOR_DESPESA = geraRomaneio.VALOR_DESPESA;
            romaneioNF.VALOR_FRETE = geraRomaneio.VALOR_FRETE;
            romaneioNF.VALOR_SEGURO = geraRomaneio.VALOR_SEGURO;

            //preecho dados de exportação

            romaneioNF.LOCAL_DESPACHO = !string.IsNullOrEmpty(geraRomaneio.LOCAL_DESPACHO) ? geraRomaneio.LOCAL_DESPACHO : null;
            romaneioNF.LOCAL_EMBARQUE = !string.IsNullOrEmpty(geraRomaneio.LOCAL_EMBARQUE) ? geraRomaneio.LOCAL_EMBARQUE : null;

            if (!string.IsNullOrEmpty(geraRomaneio.SIGLA_UF_EMBARQUE))
            {
                using (LinxOperacional contexto = new LinxOperacional())
                {
                    RepositorioRomaneio repRomaneio = new RepositorioRomaneio(contexto);

                    var uf = repRomaneio.GetUF(geraRomaneio.SIGLA_UF_EMBARQUE);

                    if (uf != null)
                        romaneioNF.ID_UF_EMBARQUE = uf.ID_UF;

                }
            }


            if (geraRomaneio.DATA_EMISSAO_NF != null)
            {
                romaneioNF.DATA_EMISSAO = geraRomaneio.DATA_EMISSAO_NF;
            }
        }

        void preencheStkRomaneio(STK_ROMANEIO romaneio, GERA_ROMANEIO geraRomaneio, TBC_FILIAL filial, LJV_LOJA ljvLoja)
        {
            RegraSequencial regra = new Linx.Documento.BM.Rules.Sequencial.RegraSequencial((int)Aplicativo.Operacional);
            romaneio.NUMERO_ROMANEIO = (int)regra.GerarSequencial((int)filial.ID_GPECON, geraRomaneio.ID_DOCUMENTO_TIPO, filial.ID_FILIAL_PFJ);

            romaneio.DATA_ROMANEIO = geraRomaneio.DATA_ROMANEIO;
            romaneio.DATA_STK_MOV = geraRomaneio.DATA_STK_MOV;
            romaneio.ID_DOCUMENTO_TIPO = geraRomaneio.ID_DOCUMENTO_TIPO;
            romaneio.ID_FILIAL_PFJ = filial.ID_FILIAL_PFJ;
            romaneio.ID_GPECON = filial.ID_GPECON;
            romaneio.ID_LINX = filial.ID_LINX;
            romaneio.ID_LOJA = ljvLoja.ID_LOJA;
            romaneio.LJV_LOJA = ljvLoja;
            romaneio.ID_OPERACAO_FINALIDADE = geraRomaneio.ID_OPERACAO_FINALIDADE;
        }

        public static STK_ROMANEIO_NF_RELACIONADA GetRomaneioNFRelacionadaNoContexto(List<STK_ROMANEIO_NF_RELACIONADA> notasRelacionadas, Int64 nfNumero, string nfSerie, string cnpj, int? idModeloFiscal, string chaveAcessoNF)
        {
            var romaneioNf = notasRelacionadas.Where(f => f.NUMERO_NF == nfNumero &&
                f.SERIE_NF == nfSerie &&
                f.CNPJ_EMITENTE_NF == cnpj &&
                f.ID_MODELO_FISCAL == idModeloFiscal).FirstOrDefault();

            if (romaneioNf == null && !chaveAcessoNF.IsNullOrEmpty())
                romaneioNf = notasRelacionadas.Where(f => f.CHAVE_ACESSO_NF == chaveAcessoNF && f.ID_MODELO_FISCAL == idModeloFiscal).FirstOrDefault();

            return romaneioNf;
        }

        public void ExcluiRomaneioNotaFiscal(Int64 idNotaFiscal, bool indicaSaida)
        {
            using (LinxOperacional contexto = new LinxOperacional())
            {
                var romaneio = contexto.STK_ROMANEIO.Include("STK_ROMANEIO_NF_LISTA").Include("STK_ROMANEIO_ITEM_LISTA").Where(f => idNotaFiscal == (indicaSaida ? f.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_SAIDA : f.STK_ROMANEIO_NF_LISTA.EX_ID_NOTA_FISCAL_ENTRADA)).FirstOrDefault();
                if (romaneio != null)
                {
                    RegraRomaneio regraRomaneio = new RegraRomaneio();
                    regraRomaneio.ExcluiRomaneioNotaFiscal(romaneio.ID_STK_ROMANEIO, romaneio.ID_LINX);
                }
            }
        }

        private bool ExisteRomaneio(RepositorioRomaneio rep, long idNotaFiscal, bool indicaSaida)
        {
            if (rep.GetRomaneioNf(idNotaFiscal, indicaSaida) != null)
                return true;
            else
                return false;
        }

    }
}