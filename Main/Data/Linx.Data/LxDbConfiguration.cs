using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Data
{
    public class OneLineFormatter : DatabaseLogFormatter
    {
        ISecurityHelper securityHelper;
        public OneLineFormatter(DbContext context, Action<string> writeAction)
            : base(context, writeAction)
        {
            securityHelper = ImplementationHelper<ISecurityHelper>.GetInstance("SecurityHelper", "Linx.Business.Tools");
        }

        public override void LogCommand<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            try
            {
                string callerName = Context.GetPropertyValue("CallerName") as string;

                var edmType = Context.GetType();
                if (!edmType.FullName.Contains(".Framework."))
                {
                    command.CommandText = Environment.NewLine + "/*" +
                                           Environment.NewLine + "Linx Business Information: " +
                                           (securityHelper == null ? "" : Environment.NewLine + "  User Name: " + securityHelper.GetCurrentUserName() +
                                           Environment.NewLine + "  User Id: " + securityHelper.GetCurrentUserId() +
                                           Environment.NewLine + "  Environment: " + securityHelper.GetCurrentEnvironmentId() +
                                           Environment.NewLine + "  Form Menu: " + securityHelper.GetTransactionInfo()) +
                                           Environment.NewLine + string.Format("  BM Name: {0}", edmType.Name + "(" + edmType.Assembly.FullName + ")") +
                                           (callerName.IsNullOrEmpty() ? "" : Environment.NewLine + string.Format("  BV Name: {0}", callerName)) +
                                           Linx.Tools.HttpHelper.GetInfo() +
                                           Environment.NewLine + "*/" +
                                          Environment.NewLine + command.CommandText;
                }
            }
            catch
            {
                //Nothing to do
            }

            base.LogCommand<TResult>(command, interceptionContext);
            //Write(string.Format(
            //    "Context '{0}' is executing command '{1}'{2}",
            //    Context.GetType().Name,
            //    command.CommandText.Replace(Environment.NewLine, ""),
            //    Environment.NewLine));
        }
        
        public override void LogResult<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
        }
    }


    //<entityFramework codeConfigurationType="Linx.Data.LxDbConfiguration, Linx.Data">
    public class LxDbConfiguration : DbConfiguration
    {
        public LxDbConfiguration()
        {
            SetDatabaseLogFormatter(
                (context, writeAction) => new OneLineFormatter(context, writeAction));
        }
    }
}
