using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.Mensagem
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class MensagemDomainService
    {
        private List<Int64> getUserList(string filtro)
        {
            List<Int64> lstUsuario = null;
            bool hasUserSearch = false;

            List<EntitySearch> search = SerializationManager<List<EntitySearch>>.StringToObject(filtro);

            EntitySearch userSearch = search.Where(i => i.EntityName == "TcsUsuario").FirstOrDefault();
            if (!userSearch.IsNull())
            {
                lstUsuario = this.GetTcsUsuarioByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { userSearch })).Select(i => i.IdUsuario).ToList();
                hasUserSearch = true;
            }


            //TcsPerfil
            EntitySearch perfilSearch = search.Where(i => i.EntityName == "TcsPerfil").FirstOrDefault();
            if (!perfilSearch.IsNull() && perfilSearch.Expressions.Count > 0)
            {
                List<Int64> perfil = this.GetTcsPerfilByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(search)).Select(i => i.IdUsuario).ToList();

                if (!hasUserSearch)
                    lstUsuario = perfil;
                else
                {
                    lstUsuario = lstUsuario.Where(i => perfil.Contains(i)).ToList();
                }
            }
            return lstUsuario;
        }

        [Invoke(HasSideEffects = true)]
        public bool AddTcsMensagem(string titulo, string corpo, string filtro, DateTime? dataEnvio, int idLinx, byte lxTipoMensagem)
        {
            Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            List<long> lstUsuario;
            try
            {
                lstUsuario = getUserList(filtro);
            }
            catch (Exception oException)
            {
                throw new Exception(String.Format("Erro ao processar o filtro enviado : {0}", oException.Message), oException.InnerException);
            }

            //Tratamento para prevenir erro de Usuário não existente
            UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
            lstUsuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => lstUsuario.Contains(i.IdUsuario)).Select(i => i.IdUsuario).ToList();

            if (lxTipoMensagem == 0 || lxTipoMensagem > 4)
            {
                lxTipoMensagem = 1;
            }

            if (dataEnvio.IsNullOrEmpty())
            {
                dataEnvio = DateTime.Now;
            }

            TcsMensagem mensagem = new TcsMensagem() { Titulo = titulo, Corpo = corpo, Filtro = filtro, Envio = dataEnvio.Value, IdLinx = idLinx, Criacao = DateTime.Now, IdUsuario = idUsuario, LxTipoMensagem = lxTipoMensagem };
            this.AddCustomChanges(mensagem, null, ChangeOperation.Insert);

            //Destinatários
            foreach (long userId in lstUsuario)
            {
                this.AddCustomChanges(new TcsMensagemLogDetail() { IdTcsMensagem = mensagem.IdTcsMensagem, IdUsuario = userId }, null, ChangeOperation.Insert);
            }

            this.SaveCustomChanges();
            return true;
        }
    }
}
