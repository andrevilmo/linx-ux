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


using Linx.Framework.BV.Transacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkTransacaoController
    {
        [Route("GetTransactionAccess"), System.Web.Http.HttpGet()]
        public TcsTransacaoSecurity GetTransactionAccess(string transaction)
        {
            TcsTransacaoSecurity result = new TcsTransacaoSecurity();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode() == "POSUX")
            {
                result.Alterar = true;
                result.Excluir = true;
                result.Imprimir = true;
                result.Incluir = true;
                result.Pesquisar = true;
            }
            else
            {

                Guid uidUsuario = Linx.Business.Tools.UserServiceHelper.GetCurrentUserUid().GetValueOrDefault();
                TransacaoDomainService ds = new TransacaoDomainService();
                result = ds.GetBoAccess(uidUsuario, null, transaction).FirstOrDefault();

                if (result.IsNull())
                    result = new TcsTransacaoSecurity();
            }

            return result;
        }
    }
}
