using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Data
{
    public class ReadUncommittedInterceptor : DbCommandInterceptor
    {
        public static readonly string SET_READ_UNCOMMITED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";
        //public static readonly string SET_READ_COMMITED = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";

        // Utilizado para : Select / Insert / Exec
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            ExecutingBase(command);
        }

        //public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        //{
        //    base.NonQueryExecuting(command, interceptionContext);
        //}

        //public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        //{
        //    base.ScalarExecuting(command, interceptionContext);
        //}

        private static void ExecutingBase(DbCommand command)
        {
            var text = command.CommandText;
            if (text.ToUpper().StartsWith("SELECT"))
            {
                command.CommandText = $"{SET_READ_UNCOMMITED} {Environment.NewLine} {text}";
            }
        }

    }
}
