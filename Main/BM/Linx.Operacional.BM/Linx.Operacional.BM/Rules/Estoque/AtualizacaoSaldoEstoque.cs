using Linx.Operacional.BM.Rules.OperacaoFinalidade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Data.Entity.Infrastructure;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class AtualizacaoSaldoEstoque
    {

        public AtualizacaoSaldoEstoque(DbContext context)
        {
            LinxOperacional operacionalContext = context as LinxOperacional;

            var poc = ((IObjectContextAdapter)context).ObjectContext;

            List<ControleEstoque> listaAtualizacaoSaldos = new List<ControleEstoque>();

            var listaOperacoes = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added || c.State == EntityState.Deleted || c.State == EntityState.Modified);

            var listaOperacoesRomaneio = listaOperacoes.Where(p => p.Entity.IsTypeOf("STK_ROMANEIO"));
            var listaOperacoesRomaneioItem = listaOperacoes.Where(p => p.Entity.IsTypeOf("STK_ROMANEIO_ITEM"));
            List<Int64> listaIdsRomaneiosAtualizados = new List<Int64>();
            bool exclusao = false;

            #region [Romaneio]

            foreach (var romaneio in listaOperacoesRomaneio)
            {
                exclusao = false;
                STK_ROMANEIO rom = romaneio.Entity as STK_ROMANEIO;

                if (romaneio.State == EntityState.Deleted)
                {
                    List<STK_ROMANEIO_ITEM> listaItensDoRomaneio = new RepositorioRomaneio(operacionalContext).GetItensRomaneio(rom.ID_STK_ROMANEIO);

                    foreach (STK_ROMANEIO_ITEM itemRomaneio in listaItensDoRomaneio)
                    {
                        exclusao = true;
                        listaAtualizacaoSaldos.Add(CreateRemoveOldBalance(itemRomaneio, rom, operacionalContext, exclusao));
                    }
                }

                else if (romaneio.State == EntityState.Modified)
                {
                    exclusao = false;
                    var CamposEditadosRomaneio = poc.ObjectStateManager.GetObjectStateEntry(romaneio.Entity).GetModifiedProperties();

                    if (CamposEditadosRomaneio.Contains("LX_STATUS_ROMANEIO"))
                    {
                        rom.STK_ROMANEIO_ITEM_LISTA = new RepositorioRomaneio(operacionalContext).GetItensRomaneio(rom.ID_STK_ROMANEIO);

                        STK_ROMANEIO romAux = new STK_ROMANEIO();
                        romAux.CopyInstanceFrom(rom);
                        romAux.LX_STATUS_ROMANEIO = Convert.ToInt32(romaneio.OriginalValues.GetValue<object>("LX_STATUS_ROMANEIO"));

                        foreach (STK_ROMANEIO_ITEM itemRomaneio in rom.STK_ROMANEIO_ITEM_LISTA)
                        {
                            STK_ROMANEIO_ITEM romItemAux = new STK_ROMANEIO_ITEM();
                            romItemAux.CopyInstanceFrom(itemRomaneio);

                            if (listaOperacoesRomaneioItem.FirstOrDefault(p => Convert.ToInt64(p.Entity.GetPropertyValue("ID_STK_ROMANEIO_ITEM")) == itemRomaneio.ID_STK_ROMANEIO_ITEM) != null)
                            {
                                var item = listaOperacoesRomaneioItem.FirstOrDefault(p => Convert.ToInt64(p.Entity.GetPropertyValue("ID_STK_ROMANEIO_ITEM")) == itemRomaneio.ID_STK_ROMANEIO_ITEM);

                                if (item.State == EntityState.Modified)
                                {
                                    var CamposEditadosRomaneioItem = poc.ObjectStateManager.GetObjectStateEntry(item.Entity).GetModifiedProperties();

                                    foreach (var campoEditado in CamposEditadosRomaneioItem)
                                    {
                                        romItemAux.SetPropertyValue(campoEditado, item.OriginalValues.GetValue<object>(campoEditado));
                                    }
                                }
                            }

                            listaAtualizacaoSaldos.Add(CreateAddNewBalance(itemRomaneio, rom, operacionalContext));
                            listaAtualizacaoSaldos.Add(CreateRemoveOldBalance(romItemAux, romAux, operacionalContext, exclusao));
                        }

                        listaIdsRomaneiosAtualizados.Add(rom.ID_STK_ROMANEIO);
                    }
                }
            }

            #endregion

            #region [RomaneioItem]

            foreach (var item in listaOperacoesRomaneioItem/*.Where(p => !listaIdsRomaneiosAtualizados.Contains(Convert.ToInt64(p.Entity.GetPropertyValue("ID_STK_ROMANEIO"))))*/)
            {
                exclusao = false;

                STK_ROMANEIO_ITEM romItem = item.Entity as STK_ROMANEIO_ITEM;
                STK_ROMANEIO rom = romItem.STK_ROMANEIO;

                if (rom == null)
                {
                    rom = new RepositorioRomaneio(operacionalContext).GetRomaneio(romItem.ID_STK_ROMANEIO);
                }

                if (rom == null)
                {
                    rom = listaOperacoesRomaneio.First(p => romItem.ID_STK_ROMANEIO == (long)p.Entity.GetPropertyValue("ID_STK_ROMANEIO")).Entity as STK_ROMANEIO;
                }

                if (item.State == EntityState.Added)
                {
                    listaAtualizacaoSaldos.Add(CreateAddNewBalance(romItem, rom, operacionalContext));
                }

                else if (item.State == EntityState.Deleted)
                {
                    exclusao = true;
                    listaAtualizacaoSaldos.Add(CreateRemoveOldBalance(romItem, rom, operacionalContext, exclusao));
                }

                else if (item.State == EntityState.Modified
                        && !listaIdsRomaneiosAtualizados.Contains(Convert.ToInt64(item.Entity.GetPropertyValue("ID_STK_ROMANEIO"))))
                {
                    var CamposEditadosRomaneioItem = poc.ObjectStateManager.GetObjectStateEntry(item.Entity).GetModifiedProperties();

                    STK_ROMANEIO_ITEM romItemAux = new STK_ROMANEIO_ITEM();

                    romItemAux.CopyInstanceFrom(romItem);

                    foreach (var campoEditado in CamposEditadosRomaneioItem)
                    {
                        romItemAux.SetPropertyValue(campoEditado, item.OriginalValues.GetValue<object>(campoEditado));
                        romItem.SetPropertyValue(campoEditado, item.CurrentValues.GetValue<object>(campoEditado));
                    }

                    listaAtualizacaoSaldos.Add(CreateAddNewBalance(romItem, rom, operacionalContext));
                    listaAtualizacaoSaldos.Add(CreateRemoveOldBalance(romItemAux, rom, operacionalContext, exclusao));
                }
            }

            AtualizaSaldos(listaAtualizacaoSaldos, operacionalContext);

            #endregion
        }

        public static ControleEstoque CreateAddNewBalance(STK_ROMANEIO_ITEM itemRomaneio, STK_ROMANEIO rom, LinxOperacional operacionalContext)
        {
            ControleEstoque controleEstoque = new ControleEstoque();

            int idFator = Convert.ToInt32(itemRomaneio.LX_FATOR_STK_MOV_QTDE);

            STK_SALDO_SKU saldo = VerificaSaldo(operacionalContext, itemRomaneio);

            if (saldo.DATA_BLOQUEIO != null && saldo.DATA_BLOQUEIO >= rom.DATA_STK_MOV)
                throw new Exception("A data de movimentação do romaneio é menor que a data de bloqueio do saldo.");

            if (saldo.DATA_AJUSTE != null && saldo.DATA_AJUSTE > rom.DATA_STK_MOV)
                throw new Exception("A movimentação do romaneio é menor que a data do último ajuste.");

            controleEstoque.IdDeposito = itemRomaneio.ID_STK_DEPOSITO;
            controleEstoque.IdSku = itemRomaneio.ID_SKU;

            if (rom.LX_STATUS_ROMANEIO == Convert.ToInt32(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value))
            {
                controleEstoque.Saldo = itemRomaneio.QTDE_ROMANEIO_ITEM * idFator;
                if (idFator == Convert.ToInt32(Domains.LX_FATOR_STK_MOV.Entrada.Value))
                {
                    controleEstoque.DataEntrada = DateTime.Now;
                }
                else
                {
                    controleEstoque.DataSaida = DateTime.Now;
                }
                controleEstoque.EntradaPendente = 0;
                controleEstoque.SaidaPendente = 0;
            }
            else
            {
                if (idFator == Convert.ToInt32(Domains.LX_FATOR_STK_MOV.Entrada.Value))
                {
                    controleEstoque.EntradaPendente = itemRomaneio.QTDE_ROMANEIO_ITEM;
                    controleEstoque.SaidaPendente = 0;
                }
                else
                {
                    controleEstoque.SaidaPendente = itemRomaneio.QTDE_ROMANEIO_ITEM;
                    controleEstoque.EntradaPendente = 0;
                }
                controleEstoque.Saldo = 0;
            }

            return controleEstoque;
        }

        public static ControleEstoque CreateRemoveOldBalance(STK_ROMANEIO_ITEM itemRomaneio, STK_ROMANEIO rom, LinxOperacional operacionalContext, bool exclusao)
        {
            ControleEstoque controleEstoque = new ControleEstoque();

            controleEstoque.Exclusao = exclusao;

            int idFator = Convert.ToInt32(itemRomaneio.LX_FATOR_STK_MOV_QTDE);

            STK_SALDO_SKU saldo = VerificaSaldo(operacionalContext, itemRomaneio);

            if (saldo.DATA_BLOQUEIO != null && saldo.DATA_BLOQUEIO >= rom.DATA_STK_MOV)
                throw new Exception("A data de movimentação do romaneio é menor que a data de bloqueio do saldo.");

            if (saldo.DATA_AJUSTE != null && saldo.DATA_AJUSTE > rom.DATA_STK_MOV)
                throw new Exception("A movimentação do romaneio é menor que a data do último ajuste.");

            controleEstoque.IdDeposito = itemRomaneio.ID_STK_DEPOSITO;
            controleEstoque.IdSku = itemRomaneio.ID_SKU;

            if (rom.LX_STATUS_ROMANEIO == Convert.ToInt32(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value))
            {
                controleEstoque.Saldo = itemRomaneio.QTDE_ROMANEIO_ITEM * idFator * (-1);
                if (idFator == Convert.ToInt32(Domains.LX_FATOR_STK_MOV.Entrada.Value))
                {
                    controleEstoque.DataEntrada = DateTime.Now;
                }
                else
                {
                    controleEstoque.DataSaida = DateTime.Now;
                }
                controleEstoque.EntradaPendente = 0;
                controleEstoque.SaidaPendente = 0;
            }
            else
            {
                if (idFator == Convert.ToInt32(Domains.LX_FATOR_STK_MOV.Entrada.Value))
                {
                    controleEstoque.EntradaPendente = itemRomaneio.QTDE_ROMANEIO_ITEM * (-1);
                    controleEstoque.SaidaPendente = 0;
                }
                else
                {
                    controleEstoque.SaidaPendente = itemRomaneio.QTDE_ROMANEIO_ITEM * (-1);
                    controleEstoque.EntradaPendente = 0;
                }
                controleEstoque.Saldo = 0;
            }

            return controleEstoque;
        }

        public static STK_SALDO_SKU VerificaSaldo(LinxOperacional operacionalContext, STK_ROMANEIO_ITEM itemRomaneio)
        {
            STK_SALDO_SKU saldo = operacionalContext.STK_SALDO_SKU.FirstOrDefault(p => p.ID_STK_DEPOSITO == itemRomaneio.ID_STK_DEPOSITO && p.ID_SKU == itemRomaneio.ID_SKU);

            if (saldo == null)
            {
                saldo = new STK_SALDO_SKU();
                saldo.ID_LINX = itemRomaneio.ID_LINX;
                saldo.ID_SKU = itemRomaneio.ID_SKU;
                saldo.ID_STK_DEPOSITO = itemRomaneio.ID_STK_DEPOSITO;
                if (itemRomaneio.LX_FATOR_STK_MOV_QTDE == Convert.ToInt32(Domains.LX_FATOR_STK_MOV.Entrada.Value))
                {
                    saldo.PRIMEIRA_ENTRADA = DateTime.Now;
                    saldo.ULTIMA_ENTRADA = DateTime.Now;
                    saldo.PRIMEIRA_SAIDA = null;
                    saldo.ULTIMA_SAIDA = null;
                }
                else
                {
                    saldo.PRIMEIRA_SAIDA = DateTime.Now;
                    saldo.ULTIMA_SAIDA = DateTime.Now;
                    saldo.PRIMEIRA_ENTRADA = null;
                    saldo.ULTIMA_ENTRADA = null;
                }
                saldo.QTDE_ENTRADA_PENDENTE = 0;
                saldo.QTDE_ESTOQUE = 0;
                saldo.QTDE_SAIDA_PENDENTE = 0;
                saldo.DATA_BLOQUEIO = null;

                operacionalContext.STK_SALDO_SKU.Add(saldo);
            }

            return saldo;
        }

        public static void AtualizaSaldos(List<ControleEstoque> listaControleEstoque, LinxOperacional contexto)
        {
            foreach (ControleEstoque controleEstoque in listaControleEstoque)
            {
                STK_SALDO_SKU saldo;

                if (controleEstoque.Exclusao)
                {
                    saldo = contexto.STK_SALDO_SKU.FirstOrDefault(p => p.ID_STK_DEPOSITO == controleEstoque.IdDeposito && p.ID_SKU == controleEstoque.IdSku);
                }

                else
                {
                    saldo = contexto.STK_SALDO_SKU.Local.First(p => p.ID_STK_DEPOSITO == controleEstoque.IdDeposito && p.ID_SKU == controleEstoque.IdSku);
                }

                saldo.QTDE_ENTRADA_PENDENTE = saldo.QTDE_ENTRADA_PENDENTE + controleEstoque.EntradaPendente;
                saldo.QTDE_ESTOQUE = saldo.QTDE_ESTOQUE + controleEstoque.Saldo;

                if (saldo.STK_DEPOSITO == null)
                {
                    saldo.STK_DEPOSITO = contexto.STK_DEPOSITO.First(p => p.ID_STK_DEPOSITO == saldo.ID_STK_DEPOSITO);
                }

                if (!saldo.STK_DEPOSITO.PERMITE_SALDO_NEGATIVO && saldo.QTDE_ESTOQUE < 0)
                {
                    throw new Exception("O depósito não permite saldos negativos.");
                }

                saldo.QTDE_SAIDA_PENDENTE = saldo.QTDE_SAIDA_PENDENTE + controleEstoque.SaidaPendente;

                if (controleEstoque.DataEntrada != null && (saldo.ULTIMA_ENTRADA < controleEstoque.DataEntrada || saldo.ULTIMA_ENTRADA == null))
                {
                    saldo.ULTIMA_ENTRADA = controleEstoque.DataEntrada;
                }
                if (controleEstoque.DataEntrada != null && (saldo.PRIMEIRA_ENTRADA > controleEstoque.DataEntrada || saldo.PRIMEIRA_ENTRADA == null))
                {
                    saldo.PRIMEIRA_ENTRADA = controleEstoque.DataEntrada;
                }
                if (controleEstoque.DataSaida != null && (saldo.ULTIMA_SAIDA < controleEstoque.DataSaida || saldo.ULTIMA_SAIDA == null))
                {
                    saldo.ULTIMA_SAIDA = controleEstoque.DataSaida;
                }
                if (controleEstoque.DataSaida != null && (saldo.PRIMEIRA_SAIDA > controleEstoque.DataSaida || saldo.PRIMEIRA_SAIDA == null))
                {
                    saldo.PRIMEIRA_SAIDA = controleEstoque.DataSaida;
                }

                if (contexto.Entry(saldo).State == EntityState.Unchanged)
                {
                    contexto.Entry(saldo).State = EntityState.Modified;
                }
            }
        }
    }
}
