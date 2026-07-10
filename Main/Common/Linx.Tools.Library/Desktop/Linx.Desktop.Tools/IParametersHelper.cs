using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public interface IParametersHelper
    {
        T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues);
        List<string> GetParametersList();
        object GetParameterValueAndType(string parameterName, Dictionary<string, string> variationValues, out Type definedType);
        List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList);
    }
}
