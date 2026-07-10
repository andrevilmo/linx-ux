using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class RepositorioRomaneio
    {
        private LinxOperacional contexto = null;

        public RepositorioRomaneio(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        public void Delete(STK_ROMANEIO romaneio)
        {
            this.contexto.Entry(romaneio).State = EntityState.Deleted;
        }

        public void Delete(STK_ROMANEIO_ITEM romaneioItem)
        {
            this.contexto.Entry(romaneioItem).State = EntityState.Deleted;
        }

        public void Delete(STK_ROMANEIO_NF romaneioNF)
        {
            this.contexto.Entry(romaneioNF).State = EntityState.Deleted;
        }

        public STK_ROMANEIO GetRomaneio(long IdRomaneio)
        {
            var query = this.contexto.STK_ROMANEIO.FirstOrDefault(p => p.ID_STK_ROMANEIO == IdRomaneio);

            return query;
        }

        public STK_ROMANEIO_NF GetRomaneioNf(long IdNotaFiscal, bool indicaSaida)
        {
            STK_ROMANEIO_NF romaneioNF = null;
            if (indicaSaida)
            {
                romaneioNF = this.contexto.STK_ROMANEIO_NF
                  .Include("STK_ROMANEIO")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.STK_ROMANEIO_NF_RELACIONADA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.NTS_ROMANEIO_ITEM_RELACIONADO")
                  .FirstOrDefault(p => p.EX_ID_NOTA_FISCAL_SAIDA == IdNotaFiscal);
            }
            else
            {
                romaneioNF = this.contexto.STK_ROMANEIO_NF
                  .Include("STK_ROMANEIO")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.STK_ROMANEIO_NF_RELACIONADA")
                  .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.NTS_ROMANEIO_ITEM_RELACIONADO")
                  .FirstOrDefault(p => p.EX_ID_NOTA_FISCAL_ENTRADA == IdNotaFiscal);
            }
            return romaneioNF;
        }

        public STK_ROMANEIO_NF GetRomaneioNf(long IdRomaneio)
        {
            var query = this.contexto.STK_ROMANEIO_NF
                .Include("STK_ROMANEIO")
                .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA")
                .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA")
                .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.STK_ROMANEIO_NF_RELACIONADA")
                .Include("STK_ROMANEIO.STK_ROMANEIO_ITEM_LISTA.STK_ROMANEIO_ITEM_RELACAO_LISTA.NTS_ROMANEIO_ITEM_RELACIONADO")
                .FirstOrDefault(p => p.ID_STK_ROMANEIO == IdRomaneio);
            
            return query;
        }

        public List<STK_ROMANEIO_ITEM> GetRomaneioItens(List<Int64> idsRomaneioItem)
        {
            return this.contexto.STK_ROMANEIO_ITEM.Where(f => idsRomaneioItem.Contains(f.ID_STK_ROMANEIO_ITEM)).ToList();
        }

        public List<STK_ROMANEIO_ITEM> GetItensRomaneio(long IdRomaneio)
        {
            var query = this.contexto.STK_ROMANEIO_ITEM.Where(p => p.ID_STK_ROMANEIO == IdRomaneio);

            return query.ToList();
        }

        public List<STK_ROMANEIO_AJUSTE> GetColetas(int idLoja)
        {
            var romaneios = contexto.STK_ROMANEIO
                .Include("STK_ROMANEIO_AJUSTE_LISTA")
                .Include("STK_ROMANEIO_AJUSTE_LISTA.STK_ROMANEIO_AJUSTE_ITEM_LISTA")
                .Include("STK_ROMANEIO_AJUSTE_LISTA.STK_INVENTARIO_SETOR_LISTA")
                .Where(w => w.ID_LOJA == idLoja
                        && w.STK_ROMANEIO_AJUSTE_LISTA != null)
                .ToList();

            List<STK_ROMANEIO_AJUSTE> coletas = new List<STK_ROMANEIO_AJUSTE>();

            foreach (var romaneio in romaneios)
            {
                if (romaneio.STK_ROMANEIO_AJUSTE_LISTA.LX_STATUS_INVENTARIO == Convert.ToByte(Domains.LX_STATUS_INVENTARIO.AguardandoColeta.Value))
                {
                    STK_ROMANEIO_AJUSTE coleta = romaneio.STK_ROMANEIO_AJUSTE_LISTA;

                    if (coleta != null && coleta.STK_INVENTARIO_SETOR_LISTA.Count() > 0)
                    {
                        List<STK_INVENTARIO_SETOR> lInventarios = coleta.STK_INVENTARIO_SETOR_LISTA.Where(w => w.LX_STATUS_INVENTARIO_SETOR == Convert.ToByte(Domains.LX_STATUS_INVENTARIO_SETOR.AguardandoColeta.Value)).ToList();

                        if (lInventarios != null && lInventarios.Count() > 0)
                        {
                            coleta.STK_INVENTARIO_SETOR_LISTA = lInventarios;
                            coletas.Add(coleta);
                        }
                    }
                }
            }

            return coletas;                  
        }

        public List<STK_ROMANEIO_AJUSTE> GetRomaneioAjuste(int idRomaneio)
        {
            return contexto.STK_ROMANEIO_AJUSTE
                .Include("STK_INVENTARIO_SETOR_LISTA.STK_COLETA_LISTA")
                .Where(w => w.STK_ROMANEIO.ID_STK_ROMANEIO == idRomaneio)
                .ToList();
        }

        public List<STK_COLETA> BuscaColetas(int idInventarioSetor)
        {
            var coletas = contexto.STK_COLETA
                .Include("STK_COLETA_ITEM_LISTA.STK_COLETA_ITEM_DETALHE_LISTA")
                .Where(w => w.ID_INVENTARIO_SETOR == idInventarioSetor)
                .ToList();

            return coletas;
        }

        public STK_INVENTARIO_SETOR GetInventarioSetor(int idInventarioSetor, long idStkRomaneio)
        {
            return this.contexto.STK_INVENTARIO_SETOR
                .Include("STK_ROMANEIO_AJUSTE")
                .Include("STK_COLETA_LISTA")
                .Where(w => w.ID_INVENTARIO_SETOR == idInventarioSetor
                && w.ID_STK_ROMANEIO == idStkRomaneio)
                .ToList().FirstOrDefault();
        }

        public STK_INVENTARIO_SETOR GetInventarioSetor(int idInventarioSetor)
        {
            return contexto.STK_INVENTARIO_SETOR
                .Where(w => w.ID_INVENTARIO_SETOR == idInventarioSetor)
                .ToList().FirstOrDefault();
        }

        public STK_ROMANEIO_NF_RELACIONADA GetRomaneioNFRelacionada(Int64 nfNumero, string nfSerie, string cnpj, int? idModeloFiscal, string chaveAcessoNF)
        {
            var romaneioNf = contexto.STK_ROMANEIO_NF_RELACIONADA.Where(f => f.NUMERO_NF == nfNumero &&
                f.SERIE_NF == nfSerie &&
                f.CNPJ_EMITENTE_NF == cnpj &&
                f.ID_MODELO_FISCAL == idModeloFiscal).FirstOrDefault();

            if (romaneioNf == null && !chaveAcessoNF.IsNullOrEmpty())
                romaneioNf = contexto.STK_ROMANEIO_NF_RELACIONADA.Where(f => f.CHAVE_ACESSO_NF == chaveAcessoNF && f.ID_MODELO_FISCAL == idModeloFiscal).FirstOrDefault();

            return romaneioNf;
        }

        public GEO_UNIDADE_FEDERACAO GetUF(string siglaUF)
        {
            return contexto.GEO_UNIDADE_FEDERACAO.Where(w => w.SIGLA_UF == siglaUF).FirstOrDefault();
        }

        public int GetIdPfj(string cnpj)
        {
            return contexto.TBC_PFJ.Where(f => f.CNPJ_CPF == cnpj).Select(f => f.ID_PFJ).FirstOrDefault();
        }

        public void Add(STK_COLETA coleta)
        {
            this.contexto.STK_COLETA.Add(coleta);
        }

        public void Remove(STK_COLETA coleta)
        {
            this.contexto.STK_COLETA.Remove(coleta);
        }

        public void SaveChanges()
        {
            try
            {
                if (contexto != null)
                    this.contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (contexto != null)
                contexto.Dispose();
        }

        public void Add(ADT_LOG_OCORRENCIA logOcorrencia)
        {
            this.contexto.ADT_LOG_OCORRENCIA.Add(logOcorrencia);
        }
    }
}
