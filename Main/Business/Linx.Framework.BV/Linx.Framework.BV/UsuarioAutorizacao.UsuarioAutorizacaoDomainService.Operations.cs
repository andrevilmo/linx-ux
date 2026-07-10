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
using System.Web.Security;

namespace Linx.Framework.BV.UsuarioAutorizacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class UsuarioAutorizacaoDomainService
    {
        [Invoke(HasSideEffects = true)]
        public bool CheckLoginAvailability(string loginName)
        {
            bool aspNetExistence = (Membership.GetUser(loginName) != null);
            bool userExistence = GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == loginName).Count() > 0;

            return (!aspNetExistence && !userExistence);
        }

        [Invoke(HasSideEffects = true)]
        public string[] GetAvailableLogins(string userName, string companyName)
        {
            string[] availableLogins = new string[3];
            string[] splittedName = userName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //first name
            string loginName = splittedName[0];
            availableLogins[0] = getLoginName(loginName);

            //first + last name
            loginName = splittedName[0] + "." +  splittedName[splittedName.Count() - 1];
            availableLogins[1] = getLoginName(loginName);

            //first name + first company name
            string[] splittedCompanyName = companyName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            loginName = splittedName[0] + "." + splittedCompanyName[0];
            availableLogins[2] = getLoginName(loginName);

            return availableLogins;
        }

        private string getLoginName(string loginName)
        {
            if (!CheckLoginAvailability(loginName))
            {
                int counter = 1;
                while (true)
                {
                    loginName = loginName + counter.ToString();
                    if (CheckLoginAvailability(loginName))
                    {
                        break;
                    }
                }
            }
            return loginName;
        }

    }
}
