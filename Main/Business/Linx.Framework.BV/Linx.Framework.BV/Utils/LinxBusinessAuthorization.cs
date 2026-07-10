using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Channels;
using Linx.Tools;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.BV.Transacao;

namespace Linx.Framework.BV
{
    
    public class LinxBusinessAutorization
    {
        public static AuthorizationResult ValidateAuthorization(string authenticatedUser, AuthorizationType type, string boName)
        {    
            return ValidateAuthorization(type, boName, null);
        }

        public static AuthorizationResult ValidateAuthorization(AuthorizationType type, string boName, Dictionary<string, string> headers)
        {
            try
            {
                bool isAuthorized = false;
                string transaction = BusinessUserServiceHelper.GetTransactionInfo(headers);
                Guid currentUser = BusinessUserServiceHelper.GetCurrentUserUid(headers).GetValueOrDefault();

                if (currentUser.IsNullOrEmpty())
                    throw new Exception(String.Format("{0} - {1}\n\nNão foi possível recuperar a informação do usuário dos cabeçalhos.", ErrorConstants._AccessDenied.Code, ErrorConstants._AccessDenied.Message));

                //Token
                Autorizacao.AutorizacaoDomainService authorization = new Autorizacao.AutorizacaoDomainService();
                authorization.ValidateToken(currentUser, BusinessUserServiceHelper.GetAuthorizationToken(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentApplicationId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentCompanyId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentAccessGroupId(headers).GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentEnvironmentId(headers).GetValueOrDefault());

                TransacaoDomainService domain = new TransacaoDomainService(headers);
                IEnumerable<TcsTransacaoSecurity> boAccess = domain.GetBoAccess(currentUser, boName, transaction);

                if (boAccess.Count() > 0)
                {
                    TcsTransacaoSecurity permissions = boAccess.First();

                    switch (type)
                    {
                        case AuthorizationType.Query:
                            isAuthorized = (permissions.AcessoTotal || permissions.Pesquisar);
                            break;

                        case AuthorizationType.Update:
                            isAuthorized = (permissions.AcessoTotal || permissions.Alterar);
                            break;

                        case AuthorizationType.Insert:
                            isAuthorized = (permissions.AcessoTotal || permissions.Incluir);
                            break;

                        case AuthorizationType.Delete:
                            isAuthorized = (permissions.AcessoTotal || permissions.Excluir);
                            break;

                        default:
                            break;
                    }
                }

                if (isAuthorized)
                    return AuthorizationResult.Allowed;
                else
                {
                    string exceptionMessage = String.Format("{0} - {1}", ErrorConstants._AccessDenied.Code, ErrorConstants._AccessDenied.Message);

                    Dictionary<string, string> variation = new Dictionary<string, string>();
                    variation.Add("TCS_USUARIO", BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault().ToString());

                    try
                    {
                        if (LinxBusinessParameters.GetParameter<bool>("DETALHA_ERROS_AUTORIZACAO", variation))
                        {
                            List<string> objectList = Utils.GetObjectClassName(boName);
                            var authorizationConnection = authorization.GetEDM().Database.Connection as System.Data.SqlClient.SqlConnection;
                            var transactionConnection = domain.GetEDM().Database.Connection as System.Data.SqlClient.SqlConnection;
                            string environmentName = BusinessUserServiceHelper.GetCurrentEnvironmentName();
                            string companyName = BusinessUserServiceHelper.GetCurrentCompanyName();
                            string economicGroupName = BusinessUserServiceHelper.GetCurrentEconomicGroupName();

                            string objectName = string.Empty;
                            foreach (string objectClassName in objectList)
                            {
                                objectName = string.Format("{0}\n- {1}", objectName, objectClassName);
                            }
                            //objetos
                            exceptionMessage = String.Format("{0}\n\nO usuário não possui acesso a nenhuma transação relacionada com algum dos seguintes objetos :\n{1}", exceptionMessage, objectName);

                            //Databases
                            exceptionMessage = string.Format("{0}\n\nInformações verificadas :\n\nBancos : \n - Portal : Server : {1}  #  Database : {2}. \n - Cliente : Server : {3}  #  Database : {4}.", exceptionMessage, authorizationConnection.DataSource, authorizationConnection.Database, transactionConnection.DataSource, transactionConnection.Database);

                            //Ambiente
                            exceptionMessage = string.Format("{0}\n\nAmbiente : \n - {1}", exceptionMessage, environmentName);

                            //Id Linx
                            exceptionMessage = string.Format("{0}\n\nEmpresa : \n - {1}", exceptionMessage, companyName);

                            //GpeCon
                            exceptionMessage = string.Format("{0}\n\nGrupo Econômico : \n - {1}", exceptionMessage, economicGroupName);

                            //Informações gerais
                            exceptionMessage = string.Format("{0}\n\n Verifique as informações e acessos nos cadastros de Objeto / Transação / Perfil / Usuário.", exceptionMessage);
                        }
                    }
                    catch { }

                    throw new Exception(exceptionMessage);
                }
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }
    }
}
