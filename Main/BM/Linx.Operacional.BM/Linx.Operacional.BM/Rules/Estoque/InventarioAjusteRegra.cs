using Linx.Operacional.BM.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class InventarioAjusteRegra
    {
        private static object syncLock = new object();
        private RepositorioRomaneio repositorioRomaneio = null;
        private LinxOperacional contexto = null;
        private byte lxStatusRomaneioFinalizado = Convert.ToByte(Domains.LX_STATUS_ROMANEIO.EstoqueFinalizado.Value);

        public InventarioAjusteRegra()
        {
            this.contexto = new LinxOperacional();
            this.repositorioRomaneio = new RepositorioRomaneio(this.contexto);
        }

        public bool AtualizarSaldoItemRomaneioAjuste(LinxOperacional contexto, DateTime dataMov, int idStkDeposito, long idStkRomaneio)
        {
            STK_ROMANEIO_AJUSTE ajuste = contexto.STK_ROMANEIO_AJUSTE.FirstOrDefault(ra => ra.ID_STK_ROMANEIO == idStkRomaneio);
            if (ajuste.LX_TIPO_AJUSTE == 2) //Parcial
                AtualizarSaldoAjusteParcial(contexto, dataMov, idStkDeposito, idStkRomaneio);
            else if (ajuste.LX_TIPO_AJUSTE == 1) //Completo
                AtualizarSaldoAjusteCompleto(contexto, dataMov, idStkDeposito, idStkRomaneio);
            contexto.SaveChanges();

            BuscarPrecoCusto(contexto, dataMov, idStkRomaneio);            
            contexto.SaveChanges();

            return true;
        }

        private void BuscarPrecoCusto(LinxOperacional contexto, DateTime dataMov, long idStkRomaneio)
        {

            var precoCustoAux = from r in contexto.STK_ROMANEIO_AJUSTE_ITEM
                                join c in contexto.STK_CUSTO
                                   on new
                                   {
                                       r.ID_SKU,
                                       r.STK_ROMANEIO_AJUSTE.STK_DEPOSITO.ID_FILIAL_PFJ
                                   } equals new
                                   {
                                       c.ID_SKU,
                                       c.ID_FILIAL_PFJ
                                   }
                                where r.ID_STK_ROMANEIO_AJUSTE == idStkRomaneio
                                && c.DATA_CUSTO <= dataMov
                                select new
                                {
                                    ID_FILIAL_PFJ = c.ID_FILIAL_PFJ,
                                    ID_SKU = c.ID_SKU,
                                    DATA = c.DATA_CUSTO,
                                    PRECO = c.CUSTO_MEDIO
                                };

            var precoCusto = (from c1 in precoCustoAux
                              join c2 in (from c in precoCustoAux
                                          group c by new
                                          {
                                              c.ID_FILIAL_PFJ,
                                              c.ID_SKU
                                          } into grupo
                                          select new
                                          {
                                              grupo.Key.ID_FILIAL_PFJ,
                                              grupo.Key.ID_SKU,
                                              DATA = grupo.Max(f => f.DATA)
                                          }) on new
                                          {
                                              c1.ID_FILIAL_PFJ,
                                              c1.ID_SKU,
                                              c1.DATA
                                          } equals new
                                          {
                                              c2.ID_FILIAL_PFJ,
                                              c2.ID_SKU,
                                              c2.DATA
                                          }
                              select new
                              {
                                  c1.ID_FILIAL_PFJ,
                                  c1.ID_SKU,
                                  c1.PRECO
                              }).ToList();

            var LxTipoPreco = Convert.ToByte(Domains.LX_TIPO_PRECO.Custo_Reposicao.Value);
            var precoCompraAux = from r in contexto.STK_ROMANEIO_AJUSTE_ITEM
                                 join ph in contexto.PRD_SKU_PRECO_HISTORICO
                              on new
                              {
                                  r.ID_SKU,
                                  r.STK_ROMANEIO_AJUSTE.STK_DEPOSITO.ID_FILIAL_PFJ
                              } equals new
                              {
                                  ph.ID_SKU,
                                  ph.ID_FILIAL_PFJ
                              }
                                 where r.ID_STK_ROMANEIO_AJUSTE == idStkRomaneio
                                 && ph.DATA_SKU_PRECO <= dataMov
                                 && ph.PRD_TABELA_PRECO.LX_TIPO_PRECO == LxTipoPreco
                                 select new
                                 {
                                     ID_FILIAL_PFJ = ph.ID_FILIAL_PFJ,
                                     ID_SKU = ph.ID_SKU,
                                     DATA = ph.DATA_SKU_PRECO,
                                     PRECO = ph.PRECO
                                 };

            var precoCompra = (from c1 in precoCompraAux
                               join c2 in (from c in precoCompraAux
                                           group c by new
                                           {
                                               c.ID_FILIAL_PFJ,
                                               c.ID_SKU
                                           } into grupo
                                           select new
                                           {
                                               grupo.Key.ID_FILIAL_PFJ,
                                               grupo.Key.ID_SKU,
                                               DATA = grupo.Max(f => f.DATA)
                                           }) on new
                                           {
                                               c1.ID_FILIAL_PFJ,
                                               c1.ID_SKU,
                                               c1.DATA
                                           } equals new
                                           {
                                               c2.ID_FILIAL_PFJ,
                                               c2.ID_SKU,
                                               c2.DATA
                                           }
                               select new
                               {
                                   c1.ID_FILIAL_PFJ,
                                   c1.ID_SKU,
                                   c1.PRECO
                               }).ToList();

            var romaneioAjusteItem = contexto.STK_ROMANEIO_AJUSTE_ITEM.Where(f => f.ID_STK_ROMANEIO_AJUSTE == idStkRomaneio).ToList();



            foreach (var item in romaneioAjusteItem)
            {
                var preco = precoCusto.Where(f => f.ID_SKU == item.ID_SKU).Select(f => f.PRECO).FirstOrDefault();
                if (preco != null)
                    item.VALOR_CUSTO = preco;
                else
                {
                    preco = precoCompra.Where(f => f.ID_SKU == item.ID_SKU).Select(f => f.PRECO).FirstOrDefault();
                    item.VALOR_CUSTO = preco;
                }
                contexto.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void AtualizarSaldoAjusteParcial(LinxOperacional contexto, DateTime dataMov, int idStkDeposito, long idStkRomaneio)
        {
            List<STK_ROMANEIO_AJUSTE_ITEM> listAjusteItem = contexto.STK_ROMANEIO_AJUSTE_ITEM.Where(p => p.ID_STK_ROMANEIO_AJUSTE == idStkRomaneio).ToList();
            foreach (var ajusteItem in listAjusteItem)
            {
                decimal saldo = 0;

                List<STK_ROMANEIO_ITEM> listRomaneioItem = contexto.STK_ROMANEIO_ITEM
                    .Where(p => p.ID_STK_DEPOSITO == idStkDeposito
                        && p.ID_SKU == ajusteItem.ID_SKU
                        && p.STK_ROMANEIO.LX_STATUS_ROMANEIO == lxStatusRomaneioFinalizado
                        && p.STK_ROMANEIO.DATA_STK_MOV < dataMov)
                    .ToList();

                listRomaneioItem.OrderBy(p => p.STK_ROMANEIO.DATA_STK_MOV);
                foreach (var romItem in listRomaneioItem)
                {
                    saldo += romItem.QTDE_ROMANEIO_ITEM * (Convert.ToInt32(romItem.LX_FATOR_STK_MOV_QTDE));
                }

                ajusteItem.QTDE_SALDO = saldo;

                contexto.Entry(ajusteItem).State = System.Data.Entity.EntityState.Modified;
            }
        }
        
        void AtualizarSaldoAjusteCompleto(LinxOperacional contexto, DateTime dataMov, int idStkDeposito, long idStkRomaneio)
        {
            var res = (from romItem in contexto.STK_ROMANEIO_ITEM
                       join rom in contexto.STK_ROMANEIO on romItem.ID_STK_ROMANEIO equals rom.ID_STK_ROMANEIO
                       where romItem.ID_STK_DEPOSITO == idStkDeposito && rom.DATA_STK_MOV < dataMov && rom.LX_STATUS_ROMANEIO == lxStatusRomaneioFinalizado
                       group romItem by new { romItem.ID_SKU, rom.ID_LINX } into grp
                       select new
                       {
                           grp.Key.ID_SKU,
                           Total = grp.Sum(s => s.QTDE_ROMANEIO_ITEM * s.LX_FATOR_STK_MOV_QTDE),
                           grp.Key.ID_LINX
                       }).ToList();
            
            //Inserir todos
            foreach (var item in res)
            {
                STK_ROMANEIO_AJUSTE_ITEM romAjusteItem = contexto.STK_ROMANEIO_AJUSTE_ITEM.FirstOrDefault(rai => rai.ID_SKU == item.ID_SKU && rai.ID_STK_ROMANEIO_AJUSTE == idStkRomaneio);
                if (romAjusteItem == null) //Este produto não possui ajuste, portanto cria um novo.
                {
                    if (item.Total != 0) //Quer dizer que este produto tem sido movimentado ultimamente, então regera ajuste
                    {
                        romAjusteItem = new STK_ROMANEIO_AJUSTE_ITEM();
                        romAjusteItem.ID_SKU = item.ID_SKU;
                        romAjusteItem.ID_LINX = item.ID_LINX;
                        romAjusteItem.ID_STK_ROMANEIO_AJUSTE = idStkRomaneio;
                        romAjusteItem.ID_STK_ROMANEIO_AJUSTE_ITEM = -1;
                        romAjusteItem.QTDE_CONTAGEM = 0;
                        romAjusteItem.QTDE_SALDO = item.Total;
                        contexto.STK_ROMANEIO_AJUSTE_ITEM.Add(romAjusteItem);
                    }
                }
                else
                {
                    romAjusteItem.QTDE_SALDO = item.Total;
                    contexto.Entry(romAjusteItem).State = System.Data.Entity.EntityState.Modified;
                }
            }
        }

        public List<RomaneioAjuste> GetColetas(int idLoja)
        {
            List<STK_ROMANEIO_AJUSTE> coletas = repositorioRomaneio.GetColetas(idLoja);

            List<RomaneioAjuste> lColetasRet = new List<RomaneioAjuste>();
            RomaneioAjuste coletaRet = null;

            if (coletas != null && coletas.Count() > 0)
            {
                foreach (var col in coletas)
                {
                    coletaRet = new RomaneioAjuste()
                    {
                        DESC_INVENTARIO = col.DESC_INVENTARIO,
                        ID_LINX = col.ID_LINX,
                        ID_STK_DEPOSITO = col.ID_STK_DEPOSITO,
                        ID_STK_ROMANEIO = col.ID_STK_ROMANEIO,
                        INDICA_AJUSTADO = col.INDICA_AJUSTADO,
                        LX_METODO_RECONTAGEM = col.LX_METODO_RECONTAGEM,
                        LX_STATUS_INVENTARIO = col.LX_STATUS_INVENTARIO,
                        LX_TIPO_AJUSTE = col.LX_TIPO_AJUSTE,
                        NOME_RESPONSAVEL = col.NOME_RESPONSAVEL,
                        NUMERO_CONTAGENS = col.NUMERO_CONTAGENS,
                        OBS_INVENTARIO = col.OBS_INVENTARIO,
                    };

                    Romaneio romaneio = new Romaneio()
                    {
                        DATA_ROMANEIO = col.STK_ROMANEIO.DATA_ROMANEIO,
                        DATA_STK_MOV = col.STK_ROMANEIO.DATA_STK_MOV,
                        ID_DOCUMENTO_TIPO = col.STK_ROMANEIO.ID_DOCUMENTO_TIPO,
                        ID_FILIAL_PFJ = col.STK_ROMANEIO.ID_FILIAL_PFJ,
                        ID_FILIAL_PFJ_DESTINO = col.STK_ROMANEIO.ID_FILIAL_PFJ_DESTINO,
                        ID_GPECON = col.STK_ROMANEIO.ID_GPECON,
                        ID_LINX = col.STK_ROMANEIO.ID_LINX,
                        ID_LOJA = col.STK_ROMANEIO.ID_LOJA,
                        ID_LOJA_DESTINO = col.STK_ROMANEIO.ID_LOJA_DESTINO,
                        ID_OPERACAO_FINALIDADE = col.STK_ROMANEIO.ID_OPERACAO_FINALIDADE,
                        ID_STK_ROMANEIO = col.STK_ROMANEIO.ID_STK_ROMANEIO,
                        ID_STK_ROMANEIO_ORIGEM = col.STK_ROMANEIO.ID_STK_ROMANEIO_ORIGEM,
                        ID_TAB_PRECO = col.STK_ROMANEIO.ID_TAB_PRECO,
                        LX_STATUS_ROMANEIO = col.STK_ROMANEIO.LX_STATUS_ROMANEIO,
                        NUMERO_ROMANEIO = col.STK_ROMANEIO.NUMERO_ROMANEIO,
                    };

                    coletaRet.Romaneio = romaneio;

                    if (col.STK_ROMANEIO_AJUSTE_ITEM_LISTA != null
                        && col.LX_TIPO_AJUSTE != Convert.ToByte(Domains.LX_TIPO_AJUSTE.COMPLETO.Value)
                        && col.STK_ROMANEIO_AJUSTE_ITEM_LISTA.Count() > 0)
                    {
                        List<RomaneioAjusteItem> lRomAjusteItem = new List<RomaneioAjusteItem>();

                        foreach (var romajusteitem in col.STK_ROMANEIO_AJUSTE_ITEM_LISTA)
                        {
                            RomaneioAjusteItem romAjusteItem = new RomaneioAjusteItem()
                            {
                                ID_STK_ROMANEIO = romajusteitem.ID_STK_ROMANEIO_AJUSTE,
                                ID_STK_ROMANEIO_AJUSTE_ITEM = romajusteitem.ID_STK_ROMANEIO_AJUSTE_ITEM,
                                ID_SKU = romajusteitem.ID_SKU,
                            };


                            lRomAjusteItem.Add(romAjusteItem);
                        }

                        coletaRet.RomaneioAjusteItem_LISTA = lRomAjusteItem;
                    }

                    List<Inventario> lInventario = new List<Inventario>();
                    Inventario inventario = null;

                    foreach (var invent in col.STK_INVENTARIO_SETOR_LISTA)
                    {
                        inventario = new Inventario()
                        {
                            DESC_SETOR = invent.DESC_SETOR,
                            ID_INVENTARIO_SETOR = invent.ID_INVENTARIO_SETOR,
                            ID_LINX = invent.ID_LINX,
                            ID_STK_ROMANEIO = invent.ID_STK_ROMANEIO,
                            LX_STATUS_INVENTARIO_SETOR = invent.LX_STATUS_INVENTARIO_SETOR,
                            NUMERO_CONTAGENS = invent.NUMERO_CONTAGENS,
                            NUMERO_SETOR = invent.NUMERO_SETOR,
                            OBS_SETOR = invent.OBS_SETOR,
                        };

                        lInventario.Add(inventario);
                    }
                    coletaRet.Inventario_LISTA = lInventario;

                    lColetasRet.Add(coletaRet);
                }
            }

            return lColetasRet;
        }

        public void AtualizarColetas(List<Coleta> coletas)
        {
            lock (syncLock)
            {
                if (coletas != null && coletas.Count() > 0)
                {
                    foreach (var coleta in coletas)
                    {
                        if (coleta.ID_INVENTARIO_SETOR > 0 && coleta.ID_STK_ROMANEIO > 0)
                        {
                            #region Verifica se ja tem o inventario setor com coleta em andamento - se houver todas as coletas do inventário são removidas
                            var inventarioSetorExistente = this.repositorioRomaneio.GetInventarioSetor((int)coleta.ID_INVENTARIO_SETOR, coleta.ID_STK_ROMANEIO);

                            if (inventarioSetorExistente != null)
                            {
                                if (inventarioSetorExistente.LX_STATUS_INVENTARIO_SETOR == Convert.ToByte(Domains.LX_STATUS_INVENTARIO_SETOR.AguardandoColeta.Value))
                                {
                                    if (inventarioSetorExistente.STK_COLETA_LISTA != null)
                                    {
                                        bool removeColetas = false;
                                        foreach (var coletaExistente in inventarioSetorExistente.STK_COLETA_LISTA)
                                        {
                                            if (coletaExistente.LX_STATUS_COLETA == Convert.ToByte(Domains.LX_STATUS_COLETA.EmAndamento.Value))
                                            {
                                                removeColetas = true;
                                                break;
                                            }
                                            else if (coletaExistente.LX_STATUS_COLETA != Convert.ToByte(Domains.LX_STATUS_COLETA.EmAndamento.Value)
                                                && coletaExistente.LX_STATUS_COLETA != Convert.ToByte(Domains.LX_STATUS_COLETA.ColetaFinalizada.Value))
                                                throw new BM.Exceptions.BusinessModelException("Não é possível receber coletas para o inventário " + inventarioSetorExistente.ID_INVENTARIO_SETOR + ", romaneio " + inventarioSetorExistente.ID_STK_ROMANEIO + ", verifique coleta(s) não finalizada(s).");
                                        }

                                        if (removeColetas)
                                        {
                                            int qtdeRemover = inventarioSetorExistente.STK_COLETA_LISTA.Count();
                                            for (int i = 1; i <= qtdeRemover; i++)
                                            {
                                                repositorioRomaneio.Remove(inventarioSetorExistente.STK_COLETA_LISTA.First());
                                            }
                                        }
                                    }
                                }
                                else
                                    throw new BM.Exceptions.BusinessModelException("Este inventário não está mais aguardando coleta!");
                            }
                            else
                                throw new BM.Exceptions.BusinessModelException("Não foi encontrado este Inventário e Setor.");
                            #endregion

                            #region AdicionaColeta
                            STK_COLETA coletaAdicionar = new STK_COLETA()
                            {
                                DATA_COLETA = coleta.DATA_COLETA,
                                DESC_COLETA = coleta.DESC_COLETA,
                                ID_DOCUMENTO_TIPO = coleta.ID_DOCUMENTO_TIPO,
                                ID_STK_ROMANEIO = coleta.ID_STK_ROMANEIO,
                                LX_STATUS_COLETA = Convert.ToByte(Domains.LX_STATUS_COLETA.EmAndamento.Value), //Inicia fixo o status da coleta                                
                                NOME_RESPONSAVEL = coleta.NOME_RESPONSAVEL,
                                NUMERO_COLETA = coleta.NUMERO_COLETA,
                                OBS_COLETA = coleta.OBS_COLETA,
                                ID_INVENTARIO_SETOR = coleta.ID_INVENTARIO_SETOR,
                            };

                            List<STK_COLETA_ITEM> lItensColeta = new List<STK_COLETA_ITEM>();
                            foreach (var item in coleta.ColetaItem_LISTA)
                            {
                                STK_COLETA_ITEM coletaItem = new STK_COLETA_ITEM()
                                {
                                    ID_SKU = item.ID_SKU,
                                    QTDE_COLETADA = item.QTDE_COLETADA,
                                    LX_STATUS_COLETA_ITEM = (item.LX_STATUS_COLETA_ITEM == 0 ? (short)1 : item.LX_STATUS_COLETA_ITEM),
                                };

                                STK_COLETA_ITEM_DETALHE coletaItemDetalhe = new STK_COLETA_ITEM_DETALHE()
                                {
                                    ID_STK_LOCALIZACAO = item.ID_STK_LOCALIZACAO,
                                    ID_STK_LOTE = item.ID_STK_LOTE,
                                    NUMERO_SERIE = item.NUMERO_SERIE,
                                    OBS_ITEM = item.OBS_ITEM,
                                };

                                coletaItem.STK_COLETA_ITEM_DETALHE_LISTA = coletaItemDetalhe;
                                lItensColeta.Add(coletaItem);
                            }

                            coletaAdicionar.STK_COLETA_ITEM_LISTA = lItensColeta;
                            #endregion

                            // teste idlinx
                            //coletaAdicionar.ID_LINX = 1;
                            //foreach (var item in coletaAdicionar.STK_COLETA_ITEM_LISTA)
                            //{
                            //    item.ID_LINX = 1;
                            //    item.STK_COLETA_ITEM_DETALHE_LISTA.ID_LINX = 1;
                            //}

                            //if(inventarioSetorExistente != null && coletaAdicionar != null)

                            // inventarioSetorExistente.STK_ROMANEIO_AJUSTE.LX_STATUS_INVENTARIO = Convert.ToByte(Domains.LX_STATUS_INVENTARIO.AguardandoAjuste.Value); 

                            this.repositorioRomaneio.Add(coletaAdicionar);
                            repositorioRomaneio.SaveChanges();


                            // Atualiza coleta para forçar Trigger de verificação que elege a coleta
                            coletaAdicionar.LX_STATUS_COLETA = Convert.ToByte(Domains.LX_STATUS_COLETA.ColetaEleita.Value);
                            repositorioRomaneio.SaveChanges();
                        }
                        else
                            throw new BM.Exceptions.BusinessModelException("Inventário/Romaneio não informado na coleta!");
                    }
                }
            }
        }
    }
}