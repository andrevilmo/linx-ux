using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.ComponentModel.Composition;
using System.Web.Http;
using Linx.Demo.BV.Graf_dash_KPI;
using Linx.Demo.BM;


namespace Linx.Demo.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxDemoGraf_dash_KPIController
    {


        [Route("BuscaProduto")]
        [HttpGet()]
        [LinxDemoGraf_dash_KPIControllerAuthorize()]
        public List<ESTOQUE> BuscaProduto(int idproduto)
        {
            BMDTesteFrame bm = new BMDTesteFrame();

            // var produtoQuery = this.IdProduto;
            // int idProduto = this.IdProduto.GetValueOrDefault();
        //    var produto = new BM.PRODUTO();
            List<Linx.Demo.BM.ESTOQUE> lista = (from estoque in bm.ESTOQUE
                                                where estoque.ID_PRODUTO == idproduto
                                                select estoque).ToList();
            return lista;

        }

        [Route("BuscaEstoquePorProduto")]
        [HttpGet()]
        [LinxDemoGraf_dash_KPIControllerAuthorize()]
        public List<ESTOQUE> BuscaEstoquePorProduto(int idproduto)
        {
            BMDTesteFrame bm = new BMDTesteFrame();
            var lista = (from estoque in bm.ESTOQUE
                                                where estoque.ID_PRODUTO == idproduto
                                                select new
                                                {
                                                    idestoque = estoque.ID_ESTOQUE,
                                                    idproduto = estoque.ID_ESTOQUE,
                                                    descricao = estoque.DESCRICAO,
                                                    dtentrada = estoque.DATA_ENTRADA
                                                }).ToList();
            List<ESTOQUE> a = new List<ESTOQUE>();
            return a;
        }

        [Route("BuscaEstoquePorProduto2")]
        [HttpGet()]
        [LinxDemoGraf_dash_KPIControllerAuthorize()]
        public object BuscaEstoquePorProduto2(int idproduto)
        {
            BMDTesteFrame bm = new BMDTesteFrame();
            var lista = (from estoque in bm.ESTOQUE
                         where estoque.ID_PRODUTO == idproduto
                         select new
                         {
                             idestoque = estoque.ID_ESTOQUE,
                             idproduto = estoque.ID_ESTOQUE,
                             descricao = estoque.DESCRICAO,
                             dtentrada = estoque.DATA_ENTRADA,
                             qtda=estoque.QTDE_PRODUTO
                         }).ToList();
            return lista;

        }
    }
}
