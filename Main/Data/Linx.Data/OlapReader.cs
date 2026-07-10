using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Data
{
    public class OlapReader
    {
        public AdomdConnection GetConnection(string connString)
        {
            return new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString);
        }

        public IEnumerable<string> LoadCubes(AdomdConnection connection)
        {

            var command = connection.CreateCommand();
            command.CommandText = "SELECT CUBE_NAME FROM $system.MDSCHEMA_CUBES where CUBE_SOURCE = 1";

            using (var reader = command.ExecuteReader())
            {
                return reader.Select(r => r["CUBE_NAME"] as string).ToList();
            }

        }

        public IEnumerable<OlapItemInfo> LoadMeasures(AdomdConnection connection, string cubeName)
        {
            var items = new List<OlapItemInfo>();


            var command = connection.CreateCommand();
            command.CommandText = "SELECT MEASURE_NAME, MEASURE_UNIQUE_NAME, MEASURE_CAPTION, DATA_TYPE, MEASUREGROUP_NAME FROM $SYSTEM.MDSCHEMA_MEASURES " +
            " WHERE [CUBE_NAME]=@CUBE_NAME and MEASURE_IS_VISIBLE ORDER BY [MEASUREGROUP_NAME]";
            command.Parameters.Add(new AdomdParameter("CUBE_NAME", cubeName));

            using (var reader = command.ExecuteReader())
            {
                items = reader.Select(dr =>
                    new OlapItemInfo
                    {
                        Name = (string)dr["MEASURE_NAME"],
                        UniqueName = (string)dr["MEASURE_UNIQUE_NAME"],
                        DisplayName = (string)dr["MEASURE_CAPTION"],
                        DataTypeNumber = (ushort)dr["DATA_TYPE"],
                        OlapItemType = OlapItemEnum.Measure,
                        GroupName = (string)dr["MEASUREGROUP_NAME"]
                    }
                    ).ToList();
            }

            return items.ToArray();
        }

        public IEnumerable<OlapItemInfo> LoadDimensions(AdomdConnection connection, string cubeName)
        {
            if (string.IsNullOrEmpty(cubeName)) return new OlapItemInfo[] { };

            var items = new List<OlapItemInfo>();


            var command = connection.CreateCommand();
            command.CommandText = "SELECT DIMENSION_NAME, DIMENSION_CAPTION FROM $system.MDSchema_Dimensions " +
            " WHERE CUBE_NAME=@CUBE_NAME and DIMENSION_IS_VISIBLE AND DIMENSION_TYPE <> 2 ORDER BY DIMENSION_CAPTION";
            command.Parameters.Add(new AdomdParameter("CUBE_NAME", cubeName));


            using (var reader = command.ExecuteReader())
            {
                items = reader.Select(dr =>
                    new OlapItemInfo
                    {
                        Name = (string)dr["DIMENSION_NAME"],
                        DisplayName = (string)dr["DIMENSION_CAPTION"],
                        OlapItemType = OlapItemEnum.Dimension
                    }
                    ).ToList();
            }

            return items.ToArray();
        }

        public IEnumerable<OlapItemInfo> LoadDimensionProperties(AdomdConnection connection, string cubeName, string dimensionName)
        {
            var items = new List<OlapItemInfo>();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT LEVEL_CAPTION, LEVEL_NAME, [LEVEL_UNIQUE_NAME], LEVEL_DBTYPE FROM $system.MDSchema_levels " +
            " WHERE CUBE_NAME=@CUBE_NAME AND [DIMENSION_UNIQUE_NAME]=@DIMENSION_NAME AND LEVEL_ORIGIN=2 AND LEVEL_NAME <> '(All)' AND LEVEL_IS_VISIBLE";
            command.Parameters.Add(new AdomdParameter("CUBE_NAME", cubeName));
            command.Parameters.Add(new AdomdParameter("DIMENSION_NAME", string.Format("[{0}]", dimensionName)));


            using (var reader = command.ExecuteReader())
            {
                items = reader.Select(dr =>
                    new OlapItemInfo
                    {
                        Name = (string)dr["LEVEL_NAME"],
                        UniqueName = (string)dr["LEVEL_UNIQUE_NAME"],
                        DisplayName = (string)dr["LEVEL_CAPTION"],
                        DataTypeNumber = (ushort)(int)dr["LEVEL_DBTYPE"],
                        OlapItemType = OlapItemEnum.DimensionProperty
                    }
                    ).ToList();
            }



            return items.ToArray();
        }

        public IEnumerable<OlapItemInfo> LoadKpis(AdomdConnection connection, string cubeName)
        {
            var items = new List<OlapItemInfo>();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT KPI_NAME, KPI_CAPTION, MEASUREGROUP_NAME, KPI_VALUE, KPI_STATUS " +
                " FROM $system.MDSCHEMA_KPIs WHERE [CUBE_NAME]='Model' ORDER BY KPI_CAPTION  ";
            command.Parameters.Add(new AdomdParameter("CUBE_NAME", cubeName));

            using (var reader = command.ExecuteReader())
            {
                items = reader.Select(dr =>
                    new OlapItemInfo
                    {
                        Name = (string)dr["KPI_NAME"],
                        UniqueName = (string)dr["KPI_VALUE"],
                        DisplayName = (string)dr["KPI_CAPTION"],
                        GroupName = (string)dr["MEASUREGROUP_NAME"],
                        OlapItemType = OlapItemEnum.Kpi
                    }
                    ).ToList();
            }


            return items;
        }


        public IEnumerable<string> GetMeasureGroups(OlapItemInfo[] measures)
        {
            var measureGroups =
               from m in measures
               group m by m.GroupName into g
               select new
               {
                   Name = g.Key,
                   Items = g.ToArray()
               };

            return measures.Select(m => m.GroupName).Distinct();
        }

        public IEnumerable<OlapMetaDataProperty> GetOlapMetaDataProperty(string connString, string cubeName, string entityBase)
        {
            List<OlapMetaDataProperty> properties = new List<OlapMetaDataProperty>();
            using (AdomdConnection connection = GetConnection(connString))
            {
                connection.Open();


                var dimensions = LoadDimensions(connection, cubeName);
                dimensions.Foreach(dim =>
                {
                    properties.Add(new OlapMetaDataProperty
                    {
                        id = dim.Name,
                        entityName = entityBase,
                        enabled = false,
                        children = true,
                        text = dim.DisplayName,
                        parent = "#",
                        dataType = GetDataType(dim)
                    });
                });

                connection.Close();
            }

            properties.Insert(0, new OlapMetaDataProperty
            {
                id = "Measures",
                entityName = entityBase,
                enabled = false,
                children = true,
                text = "Measures",
                parent = "#",
                dataType = 'S'
            });

            return properties;
        }

        public IEnumerable<OlapMetaDataProperty> GetOlapMetaDataPropertyDetails(string connString, string cubeName, string parentName)
        {
            List<OlapMetaDataProperty> properties = new List<OlapMetaDataProperty>();
            using (AdomdConnection connection = GetConnection(connString))
            {
                connection.Open();
                IEnumerable<OlapItemInfo> details;
                if (parentName == "Measures")
                {
                    details = LoadMeasures(connection, cubeName);
                }
                else
                {
                    details = LoadDimensionProperties(connection, cubeName, parentName);
                }
                details.Foreach(dim =>
                {
                    properties.Add(new OlapMetaDataProperty
                    {
                        id = "(PEsp)"+dim.UniqueName,
                        entityName = "",
                        enabled = true,
                        children = false,
                        text = dim.OlapItemType == OlapItemEnum.Measure ? string.Format("{0}.{1}", dim.GroupName, dim.DisplayName) : dim.DisplayName,
                        parent = parentName,
                        dataType = GetDataType(dim)
                    });
                });

                connection.Close();
            }

            return properties;
        }

        private char GetDataType(OlapItemInfo dim)
        {
            if (dim.OlapItemType == OlapItemEnum.Measure)
                return EntitySearch.ParseJDataType(dim.DataType.Name);
            else
                return 'S';
        }

    }

}
