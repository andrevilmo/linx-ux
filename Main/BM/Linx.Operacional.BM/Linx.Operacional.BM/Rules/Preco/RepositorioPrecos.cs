using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace Linx.Operacional.BM.Rules.Preco
{
    public class RepositorioPrecos
    {
         private LinxOperacional contexto = null;

         public RepositorioPrecos(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        // public decimal GetPrecoCompra(int idTabPreco, int idFilial, int idSku, DateTime? data)
        //{

        //    DateTime dataPreco = data == null ? DateTime.Now : (DateTime)data;
        //    DateTime dataMax = DateTime.MaxValue;

        //    var q = (from preco in contexto.PRD_SKU_PRECO
                    
        //             join remarcacao in contexto.PRD_SKU_REMARCACAO on preco.ID_PRD_SKU_REMARCACAO equals remarcacao.ID_PRD_SKU_REMARCACAO

        //             join filial in contexto.TBC_PFJ on idFilial equals filial.ID_PFJ
                     
        //             where
        //               preco.ID_TAB_PRECO == idTabPreco &&
        //               preco.ID_SKU == idSku &&
        //               preco.DATA_INI_VIGENCIA <= dataPreco &&
        //               (preco.DATA_FIM_VIGENCIA == null || preco.DATA_FIM_VIGENCIA >= dataPreco) &&
        //               preco.INATIVO == false &&
        //               (remarcacao.ID_REGIME_TRIBUTARIO_DESTINO == null || remarcacao.ID_REGIME_TRIBUTARIO_DESTINO == filial.ID_REGIME_TRIBUTARIO)

        //             select new
        //             {
        //                 preco = preco.PRECO,
        //                 pesoData = EntityFunctions.DiffDays(dataPreco,preco.DATA_INI_VIGENCIA) + EntityFunctions.DiffDays(preco.DATA_FIM_VIGENCIA != null ? preco.DATA_FIM_VIGENCIA : dataMax,dataPreco),
        //                 regime = (remarcacao.ID_REGIME_TRIBUTARIO_DESTINO == null ? false : remarcacao.ID_REGIME_TRIBUTARIO_DESTINO == filial.ID_REGIME_TRIBUTARIO)
        //             });

        //    //pesoData = dataPreco.Subtract(preco.DATA_INI_VIGENCIA).TotalDays + preco.DATA_FIM_VIGENCIA.GetValueOrDefault(DateTime.MaxValue).Subtract(dataPreco).TotalDays,

        //    if (q.Count() > 0)
        //        if (q.Where(r => r.regime).Count() > 0)
        //            return q.Where(r => r.regime).OrderByDescending(r => r.pesoData).FirstOrDefault().preco;
        //        else
        //            return q.OrderByDescending(r => r.pesoData).FirstOrDefault().preco;
        //    else
        //        return 0M;

        //}
    }
}
