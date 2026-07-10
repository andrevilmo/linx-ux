using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Linx.Framework.BV.Filtro;
using Linx.Framework.BV.Domains;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkFiltroController
    {
        [Route("LoadPredefinedFilters"), System.Web.Http.HttpGet()]
        public List<PredefinedFilter> LoadPredefinedFilters()
        {
            //carregando somente os pré definidos de data.
            return Linx.Tools.PredefinedFilter.LoadPredefinedFilters("DateTime");
        }

        [Route("LoadParameters"), System.Web.Http.HttpGet()]
        public List<Linx.Framework.BV.Filtro.Parametro> LoadParameters()
        {
            ParametroAutorizacao.ParametroAutorizacaoDomainService ds = new ParametroAutorizacao.ParametroAutorizacaoDomainService();

            var parametros = (from result in ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.PermiteVariacaoPorEntidade == false)
                              select new
                              {
                                  tituloParametro = result.TituloParametro,
                                  dataType = result.LxDatatypeParametro
                              }).ToList();

            return (from result in parametros
                    select new Linx.Framework.BV.Filtro.Parametro
                    {
                        TituloParametro = result.tituloParametro,
                        DataType = result.dataType == 1 ? "I" : result.dataType == 2 ? "S" : result.dataType == 3 ? "T" : "B"
                    }).OrderBy(i => i.TituloParametro).ToList();

        }
    }
}
