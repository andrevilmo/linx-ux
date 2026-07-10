using Linx.Tools.Migration;
using Microsoft.VisualStudio.Modeling.Integration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner.CustomizedCode.DatabaseScriptGenerator
{

    public class ConceptualDataBase
    {
        public static Database GetDataBase(BusinessDataModelDesignerRoot rootDesigner)
        {
            Database database = new Database() { Name = rootDesigner.GetDataContextName() };

            //Generating DataBase Structure
            List<ModelBusAdapter> adapters = rootDesigner.GetModelAdapterss();
            List<BusinessDataModelDesignerRoot> models = new List<BusinessDataModelDesignerRoot>() { rootDesigner };
            models.AddRange(adapters.Select(e => e.GetModelRoot<BusinessDataModelDesignerRoot>()));

            try
            {
                List<string> entities = new List<string>();
                List<string> contexts = new List<string>();
                string contextEventsName = rootDesigner.GetOperationalEventsClassName();
                string contextStartEventsName = rootDesigner.GetStartEventsClassName();
                var modelClasses = rootDesigner.GetModelClasses(models).Where(e => !e.NotMapped && e.Kind == ClassKind.Table);


                foreach (var entity in modelClasses.Where(e => e.SuperclassSh == null))
                {
                    string tableName = entity.GetTableName(true);
                    var schema = database.Schemas.FirstOrDefault(e => e.Name == entity.Schema);
                    if (schema == null)
                    {
                        schema = new Schema() { Name = entity.Schema };
                        database.Schemas.Add(schema);
                    }

                    var table = schema.TablesBase.FirstOrDefault(e => e.Name == entity.Name) as Table;
                    if (table == null)
                    {
                        table = new Table()
                        {
                            Name = tableName,
                            Schema = schema,
                            PrimaryKey = new PrimaryKey() { Table = table, IsClustered = entity.IsClustered, Name = (entity.PrimaryKeyConstraintName.IsNullOrEmpty() ? "XPK_" + entity.GetTableName(true) : entity.PrimaryKeyConstraintName) + (rootDesigner.GetDefaultProvider() == Provider.SQLServer && !entity.IsClustered ? "__NC__" : String.Empty) }
                        };


                        var attributes = entity.GetAllAttributes().Where(e => !e.IsNotMapped()).ToList();
                        var indexes = entity.ModelIndexes.ToList();
                        foreach (var subEntity in entity.SubclassesSh)
                        {
                            attributes.AddRange(subEntity.Attributes.Where(e => !e.IsNotMapped()));
                            indexes.AddRange(subEntity.ModelIndexes);
                        }
                        
                        foreach (var attribute in attributes)
                        {
                            string columnName = attribute.GetColumnName();
                            var column = new Column()
                            {
                                Name = columnName,
                                Id = columnName,
                                DbDataType = attribute.GetColumnDataType(),
                                IsIdentity = attribute.IsIdentity,
                                IsPK = attribute.IsPrimaryKey,
                                IsNullable = attribute.IsNullable,
                                MaxLength = (short)attribute.MaxLength,
                                Precision = (byte)attribute.Precision,
                                Scale = (byte)attribute.Scale,
                                SqlDefault = attribute.SqlDefault,
                                TableBase = table
                            };

                            table.Columns.Add(column);

                            if (column.IsPK)
                            {
                                table.PrimaryKey.Columns.Add(column);
                                column.PrimaryKey = table.PrimaryKey;
                            }
                        }

                        foreach (var idx in indexes)
                        {
                            string idxName = idx.Name;
                            var index = new Index()
                            {
                                Id = idxName,
                                Name = idxName,
                                IsClustered = idx.IsClustered,
                                IsUnique = idx.IsUnique,
                                Table = table,
                                IsUniqueConstraint = idx.IsUnique,
                                CommandColumns = idx.Properties
                            };

                            foreach (var colRef in idx.Properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var column = table.Columns.FirstOrDefault(e => e.Name == (colRef + " ").Left(" "));
                                if (column != null)
                                    index.Columns.Add(column);
                            }

                            table.Indexes.Add(index);
                        }
                    }

                    schema.TablesBase.Add(table);
                    
                }


                //Add Associations
                foreach (var schema in database.Schemas)
                {
                    foreach (Linx.Tools.Migration.Table table in schema.TablesBase)
                    {
                        foreach (var targetClass in modelClasses.Where(e => e.GetTableName(true) == table.Name))
                        {
                            //Direct assiciations
                            foreach (var link in Association.GetLinksToSourceModelClasses(targetClass))
                            {
                                string fkName = link.GetFkName();

                                if (!table.ForeignKey.Any(e => e.Name == fkName))
                                {
                                    Linx.Tools.Migration.Table sourceTable = schema.TablesBase.First(e => e.Name == link.SourceModelClass.GetTableName(true)) as Linx.Tools.Migration.Table;

                                    ForeignKey fk = new ForeignKey()
                                    {
                                        DeleteAction = (link.WillCascadeOnDelete ? ForeignKey.ReferentialAction.Cascade : ForeignKey.ReferentialAction.NoAction),
                                        UpdateAction = ForeignKey.ReferentialAction.NoAction,
                                        Id = fkName,
                                        Name = fkName,
                                        Parent = table,
                                        Referenced = sourceTable
                                    };

                                    foreach (var attr in link.GetTargetAttributeElements())
                                    {
                                        var targetColumn = table.Columns.First(e => e.Name == attr.GetColumnName());
                                        var sourceAttr = link.SourceModelClass.GetAllAttributes().First(e => e.Name == attr.ForeignKey.Right("."));
                                        var sourceColumn = sourceTable.Columns.First(e => e.Name == sourceAttr.GetColumnName());
                                        fk.ForeignKeyColumns.Add(new ForeignKeyColumns(table, targetColumn, sourceTable, sourceColumn));
                                    }

                                    table.ForeignKey.Add(fk);
                                }
                            }

                            //Multiple associations
                            foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(targetClass))
                            {
                                string fkName = link.GetFkName();
                                if (!table.ForeignKey.Any(e => e.Name == fkName))
                                {
                                    Linx.Tools.Migration.Table sourceTable = schema.TablesBase.First(e => e.Name == link.OriginType.GetTableName(true)) as Linx.Tools.Migration.Table;

                                    ForeignKey fk = new ForeignKey()
                                    {
                                        DeleteAction = (link.WillCascadeOnDelete ? ForeignKey.ReferentialAction.Cascade : ForeignKey.ReferentialAction.NoAction),
                                        UpdateAction = ForeignKey.ReferentialAction.NoAction,
                                        Id = fkName,
                                        Name = fkName,
                                        Parent = table,
                                        Referenced = sourceTable
                                    };

                                    foreach (var attr in link.GetTargetAttributeElements())
                                    {
                                        var targetColumn = table.Columns.First(e => e.Name == attr.GetColumnName());
                                        var sourceAttr = link.OriginType.GetAllAttributes().First(e => e.Name == attr.ForeignKey.Right("."));
                                        var sourceColumn = sourceTable.Columns.First(e => e.Name == sourceAttr.GetColumnName());
                                        fk.ForeignKeyColumns.Add(new ForeignKeyColumns(table, targetColumn, sourceTable, sourceColumn));
                                    }

                                    table.ForeignKey.Add(fk);
                                }
                            }

                            //Inheritance associations
                            var supLink = Generalization.GetLinkToSuperclass(targetClass);
                            if (supLink != null)
                            {
                                string fkName = "FK_" + supLink.Superclass.Name;

                                if (!table.ForeignKey.Any(e => e.Name == fkName))
                                {
                                    Linx.Tools.Migration.Table sourceTable = schema.TablesBase.First(e => e.Name == supLink.Superclass.GetTableName(true)) as Linx.Tools.Migration.Table;

                                    ForeignKey fk = new ForeignKey()
                                    {
                                        DeleteAction = ForeignKey.ReferentialAction.Cascade,
                                        UpdateAction = ForeignKey.ReferentialAction.NoAction,
                                        Id = fkName,
                                        Name = fkName,
                                        Parent = table,
                                        Referenced = sourceTable
                                    };

                                    foreach (var attr in supLink.Subclass.GetPrimaryKeys())
                                    {
                                        var targetColumn = table.Columns.First(e => e.Name == attr.GetColumnName());
                                        var sourceColumn = sourceTable.Columns.First(e => e.Name == attr.GetColumnName());

                                        //Adjust 
                                        if (!supLink.Subclass.PrimaryKeyColumnMap.IsNullOrEmpty() && supLink.Subclass.PrimaryKeyColumnMap != targetColumn.Name)
                                            targetColumn.Name = supLink.Subclass.PrimaryKeyColumnMap;

                                        fk.ForeignKeyColumns.Add(new ForeignKeyColumns(table, targetColumn, sourceTable, sourceColumn));
                                    }

                                    table.ForeignKey.Add(fk);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception exep)
            {
                throw exep;
            }
            finally
            {
                //Release model bus adapters
                foreach (var modelBus in adapters)
                {
                    modelBus.Dispose();
                }
            }
            ////////////////////////////

            return database;
        }

    }


}

