using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using Linx.Framework.BV;
using System.ComponentModel.Composition;


namespace Linx.Business.Tools
{
    //var parametersHelper = Linx.Tools.ImplementationHelper<IParametersHelper>.GetInstance("ParametersHelper", "Linx.Business.Tools");
    [Export(typeof(IParametersHelper))]
    [ExportMetadata("ImplementationName", "ParametersHelper")]
    public class InstanceParametersHelper : IParametersHelper
    {
        public T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues)
        {
            return LinxBusinessParameters.GetParameter<T>(parameterName, variationValues);
        }

        public List<string> GetParametersList()
        {
            return LinxBusinessParameters.GetParametersList();
        }

        public object GetParameterValueAndType(string parameterName, Dictionary<string, string> variationValues, out Type definedType)
        {
            return LinxBusinessParameters.GetParameterValueAndType(parameterName, variationValues, out definedType);
        }

        public List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList)
        {
            return LinxBusinessParameters.GetParameterbyList(parameterList);
        }
    }



    public static class LinxParameters
    {
        public static T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues)
        {
            return LinxBusinessParameters.GetParameter<T>(parameterName, variationValues);
        }

        public static T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues, int applicativeId)
        {
            return LinxBusinessParameters.GetParameter<T>(parameterName, variationValues, applicativeId);
        }

        public static List<string> GetParametersList()
        {
            return LinxBusinessParameters.GetParametersList();
        }

        public static object GetParameterValueAndType(string parameterName, Dictionary<string, string> variationValues, out Type definedType)
        {
            return LinxBusinessParameters.GetParameterValueAndType(parameterName, variationValues, out definedType);
        }

        public static List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList)
        {
            return LinxBusinessParameters.GetParameterbyList(parameterList);
        }

        public static List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList, int applicativeId)
        {
            return LinxBusinessParameters.GetParameterbyList(parameterList, applicativeId);
        }
    }
}
