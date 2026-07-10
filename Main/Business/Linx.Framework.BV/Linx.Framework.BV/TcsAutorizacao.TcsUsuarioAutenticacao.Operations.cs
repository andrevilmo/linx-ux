using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.Autorizacao.BM;
using System.Web.Security;
using Linx.Resources.Localization.Security;

namespace Linx.TCS0101.BO.TcsAutorizacao
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsUsuarioAutenticacao
	{
        private void AddAspNetUser()
        {
            try
            {
                //Asp Net Security
                MembershipCreateStatus createStatus;
                string password = this.CriaUsuario ? Membership.GeneratePassword(new Random().Next(10, 30), 5) : this.ConfirmacaoUsuario;
                MembershipUser newUser = Membership.CreateUser(this.NomeAutenticacao, password, this.Email, SecurityQuestions.PetNameQuestion, "Dog", true, out createStatus);
                if (createStatus != MembershipCreateStatus.Success)
                    throw new DomainException(ErrorCodeToString(createStatus));

                //E-mail
                if (this.CriaUsuario)
                    Linx.Tools.LinxMail.Send(this.Email, "Geração automática de senha de usuário.".Translate(), true,  TcsAutorizacaoDomainService.EmailBody(this.NomeUsuario, this.NomeAutenticacao, password));
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes and add appropriate error handling.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return Linx.Resources.Localization.Security.ErrorResources.MembershipCreateStatusDuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return Linx.Resources.Localization.Security.ErrorResources.MembershipCreateStatusDuplicateEmail;

                case MembershipCreateStatus.ProviderError:
                    return Linx.Resources.Localization.Security.ErrorResources.MembershipCreateStatusProviderError;

                case MembershipCreateStatus.UserRejected:
                    return Linx.Resources.Localization.Security.ErrorResources.MembershipCreateStatusUserRejected;

                case MembershipCreateStatus.InvalidPassword:
                case MembershipCreateStatus.InvalidEmail:
                case MembershipCreateStatus.InvalidAnswer:
                case MembershipCreateStatus.InvalidQuestion:
                case MembershipCreateStatus.InvalidUserName:
                    // All this errors should have been handled by the UI validation so theoretically
                    // we should never get to this point
                    return "Validation Error: " + createStatus.ToString();

                default:
                    return "Could not register the user, please verify the provided information and try again.";
            }
        }
    }
}
