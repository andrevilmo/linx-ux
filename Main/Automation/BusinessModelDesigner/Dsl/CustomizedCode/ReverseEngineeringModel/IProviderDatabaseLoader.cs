using Linx.Tools.Migration;
using System;
using System.Collections.Generic;

namespace Linx.BusinessModelDesigner.CustomizedCode.ReverseEngineeringModel
{
    public interface IProviderDatabaseLoader
    {
        Database GetDatabaseObjects(Action<string, int> status);

        void GetProcedureColumns(Procedure procedure, Dictionary<string, string> values);

        List<Column> GetScriptColumns(string sqlScript);
    }
}
