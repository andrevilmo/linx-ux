using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using Linx.Framework.BV.Parametro;

namespace Linx.Framework.BV
{
    public static class LinxBusinessParameters
    {
        public static T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues, Dictionary<string, string> headers = null)
        {
            var parameterValue = GetParameterValue(parameterName, variationValues, headers);

            if (parameterValue.IsNull())
                return default(T);

            if (typeof(T).FullName == "System.Guid")
                return (T)((object)new Guid(parameterValue));
            else
                return (T)Convert.ChangeType(parameterValue, typeof(T));
        }

        public static T GetParameter<T>(string parameterName, Dictionary<string, string> variationValues, int applicativeId)
        {
            Dictionary<string, string> appHeaders = BusinessUserServiceHelper.GetEnvironmentInfo(applicativeId);
            return GetParameter<T>(parameterName, variationValues, appHeaders);
        }

        public static List<string> GetParametersList()
        {
            ParametroDomainService context = new ParametroDomainService();
            return context.GetTcsParametro().Select(item => item.TituloParametro).OrderBy(item => item).ToList();
        }

        /// <summary>
        /// Author: Denys Rodrigues
        /// Data: 17/04/2009
        /// This method returns the parameter value and suggested type.
        /// Alterado em 27/12/2012 -> Serginho
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="contactId"></param>
        /// <param name="definedType"></param>
        /// <returns></returns>
        public static object GetParameterValueAndType(string parameterName, Dictionary<string, string> variationValues, out Type definedType)
        {
            return Convert.ChangeType(GetParameterValue(parameterName, variationValues, out definedType), definedType);
        }

        private static string GetParameterValue(string parameterName, Dictionary<string, string> variationValues, Dictionary<string, string> headers = null)
        {
            Type definedType = typeof(Nullable);
            return GetParameterValue(parameterName, variationValues, out definedType, headers);
        }

        private static string GetParameterValue(string parameterName, Dictionary<string, string> variationValues, out Type definedType, Dictionary<string, string> headers = null)
        {
            ParameterRequestValue parametroValor = GetParametersValue(new List<ParameterRequestInfo>() { new ParameterRequestInfo() { Title = parameterName, VariationValues = variationValues } }, headers).FirstOrDefault();

            if (parametroValor != null)
            {
                definedType = parametroValor.DataType;
                return parametroValor.Value;
            }
            else
            {
                definedType = null;
                return null;
            }
        }

        public static List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList, Dictionary<string, string> headers = null)
        {
            return GetParametersValue(parameterList, headers);
        }

        public static List<ParameterRequestValue> GetParameterbyList(List<ParameterRequestInfo> parameterList, int applicativeId)
        {
            Dictionary<string, string> appHeaders = BusinessUserServiceHelper.GetEnvironmentInfo(applicativeId);
            return GetParametersValue(parameterList, appHeaders);
        }

        private static List<ParameterRequestValue> GetParametersValue(List<ParameterRequestInfo> parameterList, Dictionary<string, string> headers = null)
        {
            ParametroDomainService context = new ParametroDomainService(headers);
            return context.GetParameterValue(parameterList, headers).ToList();
        }
    }
}
