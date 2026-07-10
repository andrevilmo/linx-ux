using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Channels;
using Linx.Tools;
using System.ServiceModel.DomainServices.Server;
using System.Web.Http.Controllers;
using Linx.Framework.BV;

namespace Linx.Business.Tools
{
    
    public class LinxAutorization
    {
        public static AuthorizationResult ValidateAuthorization(string authenticatedUser, AuthorizationType type, string boName)
        {    
            return ValidateAuthorization(type, boName, null);
        }

        public static AuthorizationResult ValidateAuthorization(AuthorizationType type, string boName, Dictionary<string, string> headers)
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return AuthorizationResult.Allowed;

            var applicationId = UserServiceHelper.GetCurrentApplicationId();
            //DE02E733-A636-4C41-907F-55C8721FCCDF --> MID-e Client
            //1DFA2D1D-D907-4E06-A53B-766E26D46F23 --> Serviço do MID
            if (applicationId != null && (applicationId.Value == System.Guid.Parse("DE02E733-A636-4C41-907F-55C8721FCCDF") || applicationId.Value == System.Guid.Parse("1DFA2D1D-D907-4E06-A53B-766E26D46F23")))
                return AuthorizationResult.Allowed;

            return LinxBusinessAutorization.ValidateAuthorization(type, boName, headers);
        }

        public static bool CheckAuthorization(HttpActionContext actionContext, string boName)
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return true;

            var applicationId = UserServiceHelper.GetCurrentApplicationId();
            //DE02E733-A636-4C41-907F-55C8721FCCDF --> MID-e Client
            //1DFA2D1D-D907-4E06-A53B-766E26D46F23 --> Serviço do MID
            if (applicationId != null && (applicationId.Value == System.Guid.Parse("DE02E733-A636-4C41-907F-55C8721FCCDF") || applicationId.Value == System.Guid.Parse("1DFA2D1D-D907-4E06-A53B-766E26D46F23")))
                return true;

            return ValidateAuthorization(
                actionContext.Request.Method.GetAuth(), 
                boName,
                actionContext.Request.Headers.ToDictionary()) == AuthorizationResult.Allowed;
        }
    }
}
