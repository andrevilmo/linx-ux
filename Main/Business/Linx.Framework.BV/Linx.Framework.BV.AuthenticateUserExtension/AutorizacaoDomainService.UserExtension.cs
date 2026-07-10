using Linx.Framework.BV.Autorizacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Web.Security;

namespace Linx.Framework.BV.AuthenticateUserExtension
{
    [Export(typeof(IAuthenticateUserExtension))]
    public class AutorizacaoDomainServiceUserExtension : IAuthenticateUserExtension
    {
        public override bool ValidateUserExtension(string userName, string userPassword)
        {

            bool authenticated = false;

            UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
            var usuario = (from result in ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == userName)
                           select new { UidUsuario = result.UidUsuario, AutenticacaoWindows = result.AutenticacaoWindows }
                           ).FirstOrDefault();

            if (usuario.IsNullOrEmpty())
                return false;

            if (usuario.AutenticacaoWindows)
            {
                //using (DirectoryEntry entry = new DirectoryEntry("LDAP://extranet.boticario/DC=extranet,DC=boticario"))

                using (DirectoryEntry entry = new DirectoryEntry("LDAP://linxsaas.com.br/DC=linxsaas,DC=com,DC=br"))
                {
                    entry.Username = userName;
                    entry.Password = userPassword;

                    DirectorySearcher searcher = new DirectorySearcher(entry);

                    searcher.Filter = "(objectclass=user)";

                    try
                    {
                        searcher.FindOne();
                        authenticated = true;
                    }
                    catch (COMException ex)
                    {
                        //if (ex.ErrorCode == -2147023570)
                        //{
                        //    // Login or password is incorrect
                        //}

                        throw ex;
                    }
                }
            }

            else
            {
                authenticated = Membership.ValidateUser(userName, userPassword);
            }

            return authenticated;
        }
    }
}
