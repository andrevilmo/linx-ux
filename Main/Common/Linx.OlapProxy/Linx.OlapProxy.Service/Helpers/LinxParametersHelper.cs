using Linx.OlapProxy.Service.Enums;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.OlapProxy.Service.Helpers
{
    internal class LinxParametersHelper
    {
        private static string _linxId;
        private static string _olapCatalog;
        private static string _dataSourceInfo;
        private static string _olapServiceUri;
        private static string _economicGroupId;

        internal static string LinxId
        {
            get
            {
                if (_linxId.IsNullOrEmpty())
                    _linxId = GetLinxParameter(ParameterType.LinxId);
                return _linxId;
            }
        }

        internal static string OlapCatalog
        {
            get
            {
                if (_olapCatalog.IsNullOrEmpty())
                    _olapCatalog = GetLinxParameter(ParameterType.OlapCatalog);
                return _olapCatalog;
            }
        }

        internal static string OlapServiceUri
        {
            get
            {
                if (string.IsNullOrEmpty(_olapServiceUri))
                    _olapServiceUri = GetLinxParameter(ParameterType.OlapServiceUri);
                return _olapServiceUri;
            }
        }

        internal static string DataSourceInfo
        {
            get
            {
                if (_dataSourceInfo.IsNullOrEmpty())
                {
                    var parameters = GetAllLinxParameters();
                    var catalog = parameters["OlapCatalog"];
                    var olapUri = parameters["OlapServiceUri"];
                    _dataSourceInfo = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", olapUri, catalog);
                }
                return _dataSourceInfo;
            }
        }

        internal static string EconomicGroupId
        {
            get
            {
                if (_economicGroupId.IsNullOrEmpty())
                    _economicGroupId = GetLinxParameter(ParameterType.EconomicGroupId);
                return _economicGroupId;
            }
        }

        internal static ParameterType GetParameterType(string levelUniqueName)
        {
            var parameterType = default(ParameterType);

            if (levelUniqueName.Contains("ID_LINX"))
                parameterType = ParameterType.LinxId;
            else if (levelUniqueName.Contains("ID_GPECON"))
                parameterType = ParameterType.EconomicGroupId;
            else if (levelUniqueName.Contains("ID_BANDEIRA_REDE"))
                parameterType = ParameterType.BrandsId;

            return parameterType;
        }

        private static Dictionary<string, string> GetAllLinxParameters()
        {
            var parameters = new Dictionary<string, string>();

            var userId = Linx.Business.Tools.UserServiceHelper.GetCurrentUserId();
            parameters.Add("TCS_USUARIO", userId.ToString());

            var linxId = Linx.Business.Tools.UserServiceHelper.GetCurrentIdLinx("ControleSistema");
            var olapCatalog = Linx.Business.Tools.LinxParameters.GetParameter<string>("OLAPDATABASENAME", parameters);
            var olapServiceUri = Linx.Business.Tools.LinxParameters.GetParameter<string>("OLAPSERVERURI", parameters);

            parameters.Add("OlapCatalog", olapCatalog);
            parameters.Add("OlapServiceUri", olapServiceUri);
            parameters.Add("LinxId", ((linxId.HasValue) ? linxId.Value.ToString() : string.Empty));

            return parameters;
        }

        internal static string GetLinxParameter(Enums.ParameterType parameterType)
        {
            var parameterValue = string.Empty;
            var parameters = new Dictionary<string, string>();

            var userId = Linx.Business.Tools.UserServiceHelper.GetCurrentUserId();
            parameters.Add("TCS_USUARIO", userId.ToString());

            switch (parameterType)
            {
                case Enums.ParameterType.OlapCatalog:
                    parameterValue = Linx.Business.Tools.LinxParameters.GetParameter<string>("OLAPDATABASENAME", parameters); break;
                case Enums.ParameterType.OlapServiceUri:
                    parameterValue = Linx.Business.Tools.LinxParameters.GetParameter<string>("OLAPSERVERURI", parameters); break;
                case Enums.ParameterType.LinxId:
                    var linxId = Linx.Business.Tools.UserServiceHelper.GetCurrentIdLinx("ControleSistema");
                    if (linxId.HasValue && linxId.Value > 0)
                        parameterValue = linxId.Value.ToString();
                    break;
                case Enums.ParameterType.EconomicGroupId:
                    var IdGpecon = Linx.Business.Tools.UserServiceHelper.GetCurrentIdGpecon();
                    if (IdGpecon.HasValue && IdGpecon.Value > 0)
                        parameterValue = IdGpecon.Value.ToString();
                    break;
                default:
                    throw new ArgumentNullException("ParameterType not found.");
            }

            return parameterValue;
        }
    }
}