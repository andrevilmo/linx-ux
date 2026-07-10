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
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Linx.License.Client;
using Linx.LinqExtensions;

namespace Linx.License.Client.WebAPICore.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LicenciamentoController
    {
        /// <summary>
        /// Validar uma licença.
        /// Exemplo do comando POST:
        /// Url: http://localhost:1710/Licenciamento/Validar
        /// Body:
        /// {
        ///     "IdLicenca" : 4,
        ///     "IdCliente": "65161419000170",
        ///     "Usuario" : "usuarioTeste1"
        /// }     
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("Validar")]
        [HttpPost()]
        public LicencaRetorno Validar([FromBody] LicencaInfo info)
        {
            var result = this.Context.ValidarLicenca(info);

            return result.Result;
        }

        /// <summary>
        /// Enviar log para o servidor remoto de licenças.
        /// Exemplo do comando POST:
        /// Url: http://localhost:1710/Licenciamento/SalvarLog
        /// Body:
        /// {
        ///    "IdProduto" : 3,
        ///    "IdCliente": "65161419000170",
        ///    "Usuario" : "usuarioTeste1",
        ///    "IdUsuario" : null,
        ///    "NomeAutenticacao" : null,    
        ///    "CodigoFilial" : "",
        ///    "NomeFilial" : "",
        ///    "IndicaLoja" : false,
        ///    "IdLinx" : null,
        ///     "Detalhes" : [ "Teste1", "Teste2" ]
        /// }     
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>
        [Route("SalvarLog")]
        [HttpPost()]
        public LicencaRetorno SalvarLog([FromBody] LogInfo logContent)
        {
            var result = this.Context.SalvarLog(logContent);

            return result.Result;
        }

        /// <summary>
        /// Remover uma licença.
        /// Exemplo do comando POST:
        /// Url: http://localhost:1710/Licenciamento/Remover
        /// Body:
        /// {
        ///     "IdLicenca" : 4,
        ///     "IdCliente": "65161419000170",
        ///     "Usuario" : "usuarioTeste1"
        /// }     
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("Remover")]
        [HttpPost()]
        public LicencaRetorno Remover([FromBody] LicencaInfo info)
        {
            var result = this.Context.RemoverLicenca(info);

            return result.Result;
        }
    }
}
