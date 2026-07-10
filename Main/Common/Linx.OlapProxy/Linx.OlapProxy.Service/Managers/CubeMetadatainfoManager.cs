using Linx.OlapProxy.Service.Enums;
using Linx.OlapProxy.Service.Helpers;
using Linx.OlapProxy.Service.Models;
using Linx.OlapProxy.Service.Resources;
using Microsoft.AnalysisServices.AdomdClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Linx.OlapProxy.Service.Managers
{
    public class CubeMetadatainfoManager
    {
        private static Dictionary<string, CubeMetadatainfo> _cubeMetadataInfo = new Dictionary<string, CubeMetadatainfo>();

        internal static CubeMetadatainfo GetCubeMetadatainfo()
        {
            CubeMetadatainfo currentCubeMetadatainfo = null;

            if (_cubeMetadataInfo.ContainsKey(LinxParametersHelper.DataSourceInfo))
                currentCubeMetadatainfo = _cubeMetadataInfo[LinxParametersHelper.DataSourceInfo];
            else
                currentCubeMetadatainfo = LoadMetadatainfo();

            return currentCubeMetadatainfo;
        }

        private static void FillDimensionsInfo(CubeMetadatainfo metadataInfo, AdomdConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = OlapMDXQueryResource.DimensionsCommand;

            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var dimensionName = dataReader["DIMENSION_UNIQUE_NAME"].ToString();

                var currentDimension = metadataInfo.DimensionsInfo
                    .FirstOrDefault(x => x.DimensionName == dimensionName);

                if (currentDimension == null)
                {
                    currentDimension = new DimensionInfo(dimensionName);
                    metadataInfo.DimensionsInfo.Add(currentDimension);
                }

                currentDimension.Fields.Add(new FieldDimension()
                {
                    Name = dataReader["LEVEL_UNIQUE_NAME"].ToString(),
                    HierarchyName = dataReader["HIERARCHY_UNIQUE_NAME"].ToString(),
                    KeyType = LinxParametersHelper.GetParameterType(dataReader["LEVEL_UNIQUE_NAME"].ToString())
                });
            }

            dataReader.Close();
        }

        private static void FillMeasuresInfo(CubeMetadatainfo metadataInfo, AdomdConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = Resources.OlapMDXQueryResource.MeasuresCommand;

            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                metadataInfo.MeasuresInfo.Add(new MeasureInfo()
                {
                    Name = dataReader["MEASURE_UNIQUE_NAME"].ToString(),
                    GroupName = string.Format("[{0}]", dataReader["MEASUREGROUP_NAME"])
                });
            }

            dataReader.Close();
        }

        private static CubeMetadatainfo LoadMetadatainfo()
        {
            AdomdConnection connection = null;

            try
            {
                var cubeMetadatainfo = new CubeMetadatainfo();

                connection = new AdomdConnection(LinxParametersHelper.DataSourceInfo);
                connection.Open();

                CubeMetadatainfoManager.FillMeasuresInfo(cubeMetadatainfo, connection);
                CubeMetadatainfoManager.FillDimensionsInfo(cubeMetadatainfo, connection);

                _cubeMetadataInfo.Add(LinxParametersHelper.DataSourceInfo, cubeMetadatainfo);

                return cubeMetadatainfo;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
    }
}