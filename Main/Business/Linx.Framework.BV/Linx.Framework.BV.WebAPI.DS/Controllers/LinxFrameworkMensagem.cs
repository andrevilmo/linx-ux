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
using Linx.Framework.BV.Mensagem;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkMensagemController
    {
        [Route("GetMessages")]
        [HttpPost()]
        public List<MensagemInfo> GetMessages(Modulo.EnvironmentInfo[] environments)
        {
            List<MensagemInfo> mensagens = new List<MensagemInfo>();
            Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
            Guid? economicGroup = BusinessUserServiceHelper.GetCurrentEconomicGroupId();
            int currentIdLinx = 0;

            foreach (Modulo.EnvironmentInfo item in environments)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("CurrentUser", currentUser.ToString());
                headers.Add("EconomicGroup", economicGroup.ToString());
                headers.Add("Environment", item.EnvironmentId.ToString());
                headers.Add("CurrentCompany", item.CompanyUid.ToString());
                headers.Add("Application", item.ApplicationUid.ToString());
                headers.Add("LoginMode", BusinessUserServiceHelper.GetCurrentLoginMode());

                int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(headers).GetValueOrDefault();

                if (idLinx == currentIdLinx)
                    continue;

                Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId(headers).GetValueOrDefault();
                currentIdLinx = idLinx;

                List<TcsMensagemUsuario> lstTcsMensagem = repository.Context.GetTcsMensagemUsuarioNoAssociations().Where(i => i.IdLinx == idLinx && i.IdUsuario == idUsuario && i.Dispensada == null && i.Entregue != null).OrderBy(i => i.Entregue).ToList();

                foreach (TcsMensagemUsuario mensagem in lstTcsMensagem)
                {
                    mensagens.Add(new MensagemInfo() { IdTcsMensagemLog = mensagem.IdTcsMensagemLog, Titulo = mensagem.Titulo, Corpo = mensagem.Corpo, Lida = !mensagem.Lida.IsNullOrEmpty(), Entregue = mensagem.Entregue, TipoMensagem = GetMessageTypeName(mensagem.LxTipoMensagem) });
                }
            }
            return mensagens;
        }

        [Route("GetNewMessages")]
        [HttpPost()]
        public List<MensagemInfo> GetNewMessages(Modulo.EnvironmentInfo[] environments)
        {
            List<MensagemInfo> messages = new List<MensagemInfo>();

            Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
            Guid? economicGroup = BusinessUserServiceHelper.GetCurrentEconomicGroupId();
            int currentIdLinx = 0;

            foreach (Modulo.EnvironmentInfo item in environments)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("CurrentUser", currentUser.ToString());
                headers.Add("EconomicGroup", economicGroup.ToString());
                headers.Add("Environment", item.EnvironmentId.ToString());
                headers.Add("CurrentCompany", item.CompanyUid.ToString());
                headers.Add("Application", item.ApplicationUid.ToString());
                headers.Add("LoginMode", BusinessUserServiceHelper.GetCurrentLoginMode());

                int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(headers).GetValueOrDefault();
                Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId(headers).GetValueOrDefault();

                if (idLinx == currentIdLinx)
                    continue;
                
                currentIdLinx = idLinx;

                DateTime dataEnvio = DateTime.Now;
                List<TcsMensagemUsuario> mensagemUsuario = repository.Context.GetTcsMensagemUsuarioNoAssociations().Where(i => i.IdLinx == idLinx && i.IdUsuario == idUsuario && i.Entregue == null && i.Envio <= dataEnvio).ToList();

                foreach (TcsMensagemUsuario mensagem in mensagemUsuario)
                {
                    TcsMensagemUsuario mensagemU = new TcsMensagemUsuario();
                    mensagemU.CopyInstanceFrom(mensagem);
                    mensagemU.Entregue = DateTime.Now;
                    messages.Add(new MensagemInfo() { IdTcsMensagemLog = mensagem.IdTcsMensagemLog, Titulo = mensagem.Titulo, Corpo = mensagem.Corpo, Lida = false, Entregue= mensagemU.Entregue, TipoMensagem = GetMessageTypeName(mensagem.LxTipoMensagem) });
                    repository.Context.AddCustomChanges(mensagemU, mensagem, System.ServiceModel.DomainServices.Server.ChangeOperation.Update);
                }
                repository.Context.SaveCustomChanges();
            }
            return messages;
        }

        [Route("AddMessage")]
        [HttpPost()]
        public bool AddMessage(NewMessageInfo message)
        {
            string filtro = EntitySearch.ParseFromJEntitySearch(typeof(TcsPerfil), message.Filtro, false, false, false);
            return repository.Context.AddTcsMensagem(message.Titulo, message.Corpo, filtro, message.DataEnvio, message.IdLinx, message.LxTipoMensagem);
        }

        [Route("MarkMessageAsRead")]
        [HttpPost()]
        public void MarkMessageAsRead(long messageId)
        {
            TcsMensagemLog message = GetTcsMensagemLogNoAssociations().Where(i => i.IdTcsMensagemLog == messageId).FirstOrDefault();
            
            if (!message.IsNullOrEmpty())
            {
                TcsMensagemLog oldMessage = new TcsMensagemLog();
                oldMessage.CopyInstanceFrom(message);
                message.Lida = DateTime.Now;
                repository.Context.AddCustomChanges(message, oldMessage, System.ServiceModel.DomainServices.Server.ChangeOperation.Update);
                repository.Context.SaveCustomChanges();
            }
        }

        [Route("DismissMessage")]
        [HttpPost()]
        public void DismissMessage(long messageId)
        {
            TcsMensagemLog message = GetTcsMensagemLogNoAssociations().Where(i => i.IdTcsMensagemLog == messageId).FirstOrDefault();

            if (!message.IsNullOrEmpty())
            {
                TcsMensagemLog oldMessage = new TcsMensagemLog();
                oldMessage.CopyInstanceFrom(message);
                message.Dispensada = DateTime.Now;
                repository.Context.AddCustomChanges(message, oldMessage, System.ServiceModel.DomainServices.Server.ChangeOperation.Update);
                repository.Context.SaveCustomChanges();
            }
        }

        [Route("MarkMessageAsUnread")]
        [HttpPost()]
        public void MarkMessageAsUnread(long messageId)
        {
            TcsMensagemLog message = GetTcsMensagemLogNoAssociations().Where(i => i.IdTcsMensagemLog == messageId).FirstOrDefault();

            if (!message.IsNullOrEmpty())
            {
                TcsMensagemLog oldMessage = new TcsMensagemLog();
                oldMessage.CopyInstanceFrom(message);
                message.Lida = null;
                repository.Context.AddCustomChanges(message, oldMessage, System.ServiceModel.DomainServices.Server.ChangeOperation.Update);
                repository.Context.SaveCustomChanges();
            }
        }

        private string GetMessageTypeName(byte messageTypeId)
        {
            return (Domains.TipoMensagem.GetValues().ContainsKey(messageTypeId.ToString()) ? Domains.TipoMensagem.GetNames()[messageTypeId.ToString()] : "Info").ToLower();
        }
    }
}
