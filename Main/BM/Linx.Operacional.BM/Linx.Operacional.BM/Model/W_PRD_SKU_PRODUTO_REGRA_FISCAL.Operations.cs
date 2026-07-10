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
	public partial class W_PRD_SKU_PRODUTO_REGRA_FISCAL
	{
        public static List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> ValidaSku(List<int> listaIdSku)
        {
            LinxOperacional context = new LinxOperacional();
            List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> skuValida = new List<W_PRD_SKU_PRODUTO_REGRA_FISCAL>();

            if (listaIdSku.Count() > 0)
            {
                var skuAux = context.W_PRD_SKU_PRODUTO_REGRA_FISCAL.Where(r => listaIdSku.Contains(r.ID_SKU));

                var fornecedores = (from f in context.PRD_SKU_FORNECEDOR
                                    join r in skuAux
                                    on f.ID_SKU equals r.ID_SKU
                                    select new { f.ID_SKU, f.ID_FORNECEDOR }).ToList();

                skuValida = skuAux.ToList();

                skuValida = skuValida
                    .Select(entity0 => new W_PRD_SKU_PRODUTO_REGRA_FISCAL
                    {
                        COD_SKU = entity0.COD_SKU,
                        ID_AGRUPADOR_REGRA_PRD = entity0.ID_AGRUPADOR_REGRA_PRD,
                        ID_CLASSIF_FISCAL = entity0.ID_CLASSIF_FISCAL,
                        ID_FINALIDADE = entity0.ID_FINALIDADE,
                        ID_ORIGEM_MERCADORIA = entity0.ID_ORIGEM_MERCADORIA,
                        ID_SKU = entity0.ID_SKU,
                        COD_CLASSIF_FISCAL = entity0.COD_CLASSIF_FISCAL,
                        COD_EX_TIPI = entity0.COD_EX_TIPI,
                        COD_ORIGEM_MERCADORIA = entity0.COD_ORIGEM_MERCADORIA,
                        INDICA_ORIGEM_NACIONAL = entity0.INDICA_ORIGEM_NACIONAL,
                        COD_CEST = entity0.COD_CEST, 
                        COD_ITEM_FISCAL = entity0.COD_ITEM_FISCAL,
                        DESC_ITEM_FISCAL = entity0.DESC_ITEM_FISCAL,
                        EX_ID_ITEM_FISCAL = entity0.EX_ID_ITEM_FISCAL,
                        FORNECEDOR_LISTA = fornecedores.Where(f => f.ID_SKU == entity0.ID_SKU).Select(f => f.ID_FORNECEDOR).ToList()
                    }).ToList();
            }

            return skuValida;
        }

        public static List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> ValidaSku(int idBandeiraRede, int idLjvCanalVenda)
        {
            LinxOperacional context = new LinxOperacional();
            List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> skuValida = new List<W_PRD_SKU_PRODUTO_REGRA_FISCAL>();

            string bandeiraRede = "[[BANDEIRA{" + idBandeiraRede.ToString() + "}]]";
            string canalVenda = "[[CANALVENDA{" + idLjvCanalVenda + "}]]";

            var skuAux = Linx.Operacional.BM.W_PRD_SKU_PRODUTO_REGRA_FISCAL.GetQueryByParam(context, bandeiraRede + canalVenda, new System.Data.Entity.Core.Objects.ObjectParameter[] { });

            var fornecedores = (from f in context.PRD_SKU_FORNECEDOR
                                join r in skuAux
                                on f.ID_SKU equals r.ID_SKU
                                select new { f.ID_SKU, f.ID_FORNECEDOR }).ToList();

            skuValida = skuAux.ToList();


            skuValida = skuValida
                .Select(entity0 => new W_PRD_SKU_PRODUTO_REGRA_FISCAL
                {
                    COD_SKU = entity0.COD_SKU,
                    ID_AGRUPADOR_REGRA_PRD = entity0.ID_AGRUPADOR_REGRA_PRD,
                    ID_CLASSIF_FISCAL = entity0.ID_CLASSIF_FISCAL,
                    ID_FINALIDADE = entity0.ID_FINALIDADE,
                    ID_ORIGEM_MERCADORIA = entity0.ID_ORIGEM_MERCADORIA,
                    ID_SKU = entity0.ID_SKU,
                    COD_CLASSIF_FISCAL = entity0.COD_CLASSIF_FISCAL,
                    COD_EX_TIPI = entity0.COD_EX_TIPI,
                    COD_ORIGEM_MERCADORIA = entity0.COD_ORIGEM_MERCADORIA,
                    INDICA_ORIGEM_NACIONAL = entity0.INDICA_ORIGEM_NACIONAL,
                    COD_CEST = entity0.COD_CEST,
                    COD_ITEM_FISCAL = entity0.COD_ITEM_FISCAL,
                    DESC_ITEM_FISCAL = entity0.DESC_ITEM_FISCAL,
                    EX_ID_ITEM_FISCAL = entity0.EX_ID_ITEM_FISCAL,
                    FORNECEDOR_LISTA = fornecedores.Where(f => f.ID_SKU == entity0.ID_SKU).Select(f => f.ID_FORNECEDOR).ToList()
                }).ToList();

            return skuValida;
        }

        public static List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> ValidaFornecedores()
        {
            LinxOperacional context = new LinxOperacional();
            List<W_PRD_SKU_PRODUTO_REGRA_FISCAL> produtos = new List<W_PRD_SKU_PRODUTO_REGRA_FISCAL>();

            var skuAux = context.W_PRD_SKU_PRODUTO_REGRA_FISCAL;

            var fornecedores = (from f in context.PRD_SKU_FORNECEDOR
                                join r in skuAux
                                on f.ID_SKU equals r.ID_SKU
                                select new { f.ID_SKU, f.ID_FORNECEDOR }).ToList();

            produtos = skuAux.ToList();
            
            produtos = produtos
                .Select(entity0 => new W_PRD_SKU_PRODUTO_REGRA_FISCAL
                {
                    COD_SKU = entity0.COD_SKU,
                    ID_AGRUPADOR_REGRA_PRD = entity0.ID_AGRUPADOR_REGRA_PRD,
                    ID_CLASSIF_FISCAL = entity0.ID_CLASSIF_FISCAL,
                    ID_FINALIDADE = entity0.ID_FINALIDADE,
                    ID_ORIGEM_MERCADORIA = entity0.ID_ORIGEM_MERCADORIA,
                    ID_SKU = entity0.ID_SKU,
                    COD_CLASSIF_FISCAL = entity0.COD_CLASSIF_FISCAL,
                    COD_EX_TIPI = entity0.COD_EX_TIPI,
                    COD_ORIGEM_MERCADORIA = entity0.COD_ORIGEM_MERCADORIA,
                    INDICA_ORIGEM_NACIONAL = entity0.INDICA_ORIGEM_NACIONAL,
                    COD_CEST = entity0.COD_CEST,
                    COD_ITEM_FISCAL = entity0.COD_ITEM_FISCAL,
                    DESC_ITEM_FISCAL = entity0.DESC_ITEM_FISCAL,
                    EX_ID_ITEM_FISCAL = entity0.EX_ID_ITEM_FISCAL,
                    FORNECEDOR_LISTA = fornecedores.Where(f => f.ID_SKU == entity0.ID_SKU).Select(f => f.ID_FORNECEDOR).ToList()
                }).ToList();
            //}

            return produtos;
        }
    }

    public partial class W_PRD_SKU_PRODUTO_REGRA_FISCAL
    {
        private List<int> _FORNECEDOR_LISTA = null;

        public List<int> FORNECEDOR_LISTA
        {
            set
            {
                _FORNECEDOR_LISTA = value;
            }
            get
            {
                return _FORNECEDOR_LISTA;
            }
        }

    }
}
