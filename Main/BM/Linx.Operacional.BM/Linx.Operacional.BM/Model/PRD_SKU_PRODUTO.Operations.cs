using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Linx.Operacional.BM
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class PRD_SKU_PRODUTO
	{
        public static PRD_SKU_PRODUTO ValidaSku(int idSku)
        {
            LinxOperacional context = new LinxOperacional();
            PRD_SKU_PRODUTO skuValida = context.PRD_SKU_PRODUTO
                .Include("PRD_ARTIGO").Include("PRD_ARTIGO.TBC_UNIDADE_MEDIDA").Include("PRD_SKU_CODIGO_BARRA_LISTA").Include("PRD_SKU_NVE_LISTA")
                .Where(r => r.ID_SKU == idSku).FirstOrDefault();

            return skuValida;
        }

        public static PRD_SKU_PRODUTO ValidaSku(string codSku)
        {
            LinxOperacional context = new LinxOperacional();
            PRD_SKU_PRODUTO skuValida = new PRD_SKU_PRODUTO();

            if (!codSku.IsNullOrEmpty())
                skuValida = context.PRD_SKU_PRODUTO
                    .Include("PRD_ARTIGO").Include("PRD_ARTIGO.TBC_UNIDADE_MEDIDA").Include("PRD_SKU_CODIGO_BARRA_LISTA").Include("PRD_SKU_NVE_LISTA")
                    .Where(r => r.COD_SKU == codSku).FirstOrDefault();

            return skuValida;
        }

        public static List<PRD_SKU_PRODUTO> ValidaSku(string cnpj, string refFornecedor)
        {
            LinxOperacional context = new LinxOperacional();
            List<PRD_SKU_PRODUTO> skuValida = new List<PRD_SKU_PRODUTO>();

            if (!cnpj.IsNullOrEmpty() && !refFornecedor.IsNullOrEmpty())
                skuValida = context.PRD_SKU_PRODUTO
                    .Include("PRD_ARTIGO")
                    .Include("PRD_ARTIGO.TBC_UNIDADE_MEDIDA")
                    .Include("PRD_SKU_CODIGO_BARRA_LISTA")
                    .Include("PRD_SKU_NVE_LISTA")
                    .Include("PRD_SKU_FORNECEDOR_LISTA")
                    .Include("PRD_SKU_FORNECEDOR_LISTA.TBC_FORNECEDOR")
                    .Include("PRD_SKU_FORNECEDOR_LISTA.TBC_FORNECEDOR.TBC_PFJ")
                        .Where(r => r.PRD_SKU_FORNECEDOR_LISTA.Where(f=> 
                                f.REF_FORNECEDOR == refFornecedor && f.TBC_FORNECEDOR.TBC_PFJ.CNPJ_CPF == cnpj).Count() > 0)
                                    .ToList();

            return skuValida;
        }

        public static List<PRD_SKU_PRODUTO> ValidaSkuCodigoBarra(string codigoBarra)
        {
            LinxOperacional context = new LinxOperacional();
            List<PRD_SKU_PRODUTO> skusValida = new List<PRD_SKU_PRODUTO>();

            if (!codigoBarra.IsNullOrEmpty())
                skusValida = context.PRD_SKU_PRODUTO
                    .Include("PRD_ARTIGO")
                    .Include("PRD_ARTIGO.TBC_UNIDADE_MEDIDA")
                    .Include("PRD_SKU_CODIGO_BARRA_LISTA")
                    .Include("PRD_SKU_NVE_LISTA")
                    .Where(r => r.PRD_SKU_CODIGO_BARRA_LISTA.Where(f => f.CODIGO_BARRA == codigoBarra).Count() > 0).ToList();

            return skusValida;
        }

        public static List<PRD_SKU_PRODUTO> ValidaSku(List<int> listaIdSku)
        {

            LinxOperacional context = new LinxOperacional();
            List<PRD_SKU_PRODUTO> skuValida = new List<PRD_SKU_PRODUTO>();

            if (listaIdSku.Count() > 0)
                skuValida = context.PRD_SKU_PRODUTO
                    .Include("LCF_CLASSIFICACAO_FISCAL").Where(r => listaIdSku.Contains(r.ID_SKU)).ToList();

            return skuValida;
        }
    }
}
