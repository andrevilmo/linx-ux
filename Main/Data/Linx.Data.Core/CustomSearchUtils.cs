using Linx.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Data
{
    public class CustomSearchUtils
    {
        public static List<Table> GetEntities(string entityNames, Assembly assembly, DbContext context)
        {
            List<Table> tableList = new List<Table>();
            string[] entities = entityNames.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string entity in entities)
            {
                Type types = assembly.GetTypes().Where(i => i.Name == entity).FirstOrDefault();
                string edmEntityName = Linx.Tools.ObjectExtension.GetFunctionalPointOfType(types.GetTypeInfo().UnderlyingSystemType, "EdmEntityName");
                string relations = ObjectExtension.GetFunctionalPointOfType(types.GetTypeInfo().UnderlyingSystemType, "EntityRelations");
                string[] relatedEntities = relations.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

                Table table = CustomSearchUtils.GetEntity(edmEntityName, context);
                table.IsMaster = true;
                tableList.Add(table);

                foreach (PropertyDefinitions propertyDefinition in ObjectExtension.GetFunctionalPoints(types.GetTypeInfo().UnderlyingSystemType))
                {
                    string columnName = propertyDefinition.FilterDataKey.Right(".");
                    string tableName = propertyDefinition.FilterDataKey.Occurs(".") > 1 ? propertyDefinition.FilterDataKey.Left("." + columnName).Right(".") : propertyDefinition.FilterDataKey.Left(".");

                    Column column = CustomSearchUtils.GetColumn(columnName, table);
                    column.DisplayName = propertyDefinition.Caption;
                    column.IsBVColumn = true;

                    table.Columns.Remove(column);
                    table.EnabledColumns.Add(column);
                }
            }
            return tableList;
        }

        private static List<Table> GetEntities(DbContext context)
        {
            List<Table> tableList = new List<Table>();
            var type = context.GetType().GetProperties();
            var contextName = context.GetType().FullName;
            var cache = WebCacheHelper.GetWebCache(contextName);

            if (cache.IsNull())
            {
                foreach (var item in type)
                {
                    if (!item.PropertyType.FullName.StartsWith("System.Nullable`1[[System.Int32") && item.PropertyType.GenericTypeArguments.Count() > 0)
                    {
                        Type entity = Type.GetType(item.PropertyType.GenericTypeArguments.FirstOrDefault().AssemblyQualifiedName);

                        TableAttribute tableAttribute = entity.GetTypeInfo().GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
                        Table table = new Table() { EdmName = entity.Name, Name = tableAttribute.Name, Schema = tableAttribute.Schema, DisplayName = tableAttribute.Name }; //verificar DisplayName
                        var columns = entity.GetProperties().Where(i => i.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() != null);

                        foreach (var property in columns)
                        {
                            KeyAttribute keyAttribute = property.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() as KeyAttribute;
                            ColumnAttribute columnAttribute = property.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
                            RequiredAttribute requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() as RequiredAttribute;
                            ForeignKeyAttribute fkAttribute = property.GetCustomAttributes(typeof(ForeignKeyAttribute), true).FirstOrDefault() as ForeignKeyAttribute;

                            if (!fkAttribute.IsNull())
                            {
                                var fkInfo = entity.GetProperty(fkAttribute.Name).PropertyType.Name;
                                table.RelatedFks.Add(new ForeignKey() { RelationName = fkAttribute.Name, TableName = fkInfo });
                            }

                            Column column = new Column()
                            {
                                Name = property.Name,
                                DisplayName = property.Name, //será alterado para o DisplayName do BM
                                DataType = property.PropertyType.FullName.StartsWith("System.Nullable") ? System.Nullable.GetUnderlyingType(property.PropertyType).Name : property.PropertyType.Name,
                                IsKey = !keyAttribute.IsNull(),
                                IsRequired = !requiredAttribute.IsNull(),
                                IsNullable = requiredAttribute.IsNull(),
                                IsForeignKey = !fkAttribute.IsNull(),
                                SqlName = columnAttribute.Name,
                                SqlDataType = columnAttribute.TypeName
                            };
                            table.Columns.Add(column);
                        }

                        table.Columns = table.Columns.OrderBy(i => i.DisplayName).ToList();
                        table.EnabledColumns = table.EnabledColumns.OrderBy(i => i.DisplayName).ToList();
                        tableList.Add(table);
                    }
                }
                WebCacheHelper.AddWebCache(contextName, tableList);
            }
            else
            {
                tableList = cache as List<Table>;
            }

            return tableList;
        }

        private static Table GetEntity(string entityName, DbContext context)
        {
            Table table = new Table();
            Table origin = CustomSearchUtils.GetEntities(context).Where(i => i.EdmName == entityName).FirstOrDefault();
            table.CopyInstanceFrom(origin);

            origin.RelatedFks.ForEach(i =>
            {
                ForeignKey foreingKey = new ForeignKey() { RelationName = i.RelationName, TableName = i.TableName };
                Table fkTable = CustomSearchUtils.GetEntity(foreingKey.TableName, context);
                foreingKey.Table = fkTable;
                table.RelatedFks.Add(foreingKey);
            });

            origin.Columns.ForEach(i =>
            {
                Column column = new Column()
                {
                    DataType = i.DataType,
                    DisplayName = i.DisplayName,
                    Name = i.Name,
                    IsBVColumn = i.IsBVColumn,
                    IsForeignKey = i.IsForeignKey,
                    IsKey = i.IsKey,
                    IsNullable = i.IsNullable,
                    IsRequired = i.IsRequired,
                    SqlDataType = i.SqlDataType,
                    SqlName = i.SqlName
                };
                table.Columns.Add(column);
            });
            return table;
        }

        private static Column GetColumn(string columnName, Table table)
        {
            Column column = null;

            Action<Table> recurse = null;

            recurse = new Action<Table>(i =>
            {
                if (!column.IsNull())
                    return;

                column = i.Columns.Where(it => it.Name == columnName).FirstOrDefault();

                i.RelatedFks.ForEach(fk =>
                {
                    recurse(fk.Table);
                });
            });

            recurse(table);

            return column;
        }

        public class Table
        {
            public Table()
            {
                this.Columns = new List<Column>();
                this.RelatedFks = new List<ForeignKey>();
                this.EnabledColumns = new List<Column>();
            }

            public string EdmName { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Schema { get; set; }
            public bool IsMaster { get; set; }
            public List<Column> Columns { get; set; }
            public List<Column> EnabledColumns { get; set; }
            public List<ForeignKey> RelatedFks { get; set; }
        }

        public class Column
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string DataType { get; set; }
            public bool IsKey { get; set; }
            public bool IsRequired { get; set; }
            public bool IsNullable { get; set; }
            public bool IsForeignKey { get; set; }
            public bool IsBVColumn { get; set; }
            public string SqlName { get; set; }
            public string SqlDataType { get; set; }
        }

        public class ForeignKey
        {
            public string RelationName { get; set; }
            public string TableName { get; set; }
            public Table Table { get; set; }
        }

    }
}
