using Linx.Tools.Migration;
using Microsoft.VisualStudio.Modeling.Integration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Linx.Tools;


namespace Linx.BusinessModelDesigner.CustomizedCode.DatabaseScriptGenerator
{

    public class ConceptualDataBase
    {
        public static Database GetDataBase(BusinessModelDesignerRoot rootDesigner)
        {
            Database database = new Database() { Name = rootDesigner.GetDataContextName() };

            //Generating DataBase Structure
            List<ModelBusAdapter> adapters = rootDesigner.GetModelAdapters();
            List<BusinessModelDesignerRoot> models = new List<BusinessModelDesignerRoot>() { rootDesigner };
            models.AddRange(adapters.Select(e => e.GetModelRoot<BusinessModelDesignerRoot>()));

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

                    var table = database.FindTable(entity.Name, entity.Schema) as Table;
                    if (table == null)
                    {
                        table = new Table()
                        {
                            Name = tableName,
                            Schema = schema,
                            PrimaryKey = new PrimaryKey() { Table = table, IsClustered = entity.IsClustered, Name = (entity.PrimaryKeyConstraintName.IsNullOrEmpty() ? "XPK_" + entity.GetTableName(true) : entity.PrimaryKeyConstraintName) + (rootDesigner.GetDefaultProvider() == Provider.SQLServer && !entity.IsClustered ? "__NC__" : String.Empty) }
                        };


                        var attributes = entity.Attributes.Where(e => !e.IsNotMapped()).ToList();
                        var indexes = entity.ModelIndexes.ToList();
                        foreach (var subEntity in entity.SubclassesSh)
                        {
                            attributes.AddRange(subEntity.Attributes.Where(e => !e.IsNotMapped()));
                            indexes.AddRange(subEntity.ModelIndexes);
                        }
                        //add PK's Generalization
                        if (entity.Superclass != null)
                        {
                            attributes.AddRange(entity.Superclass.Attributes.Where(p => p.IsPrimaryKey && !p.IsNotMapped()));
                        }
                        #region Columns Info
                        foreach (var attribute in attributes)
                        {
                            if (attribute.InStudy) continue;

                            string columnName = attribute.GetColumnName();
                            var column = new Column()
                            {
                                Name = columnName,
                                Id = columnName,
                                DbDataType = attribute.GetColumnDataType(),
                                IsIdentity = attribute.IsIdentity && entity.Superclass == null,
                                IsPK = attribute.IsPrimaryKey,
                                IsNullable = attribute.IsNullable,
                                MaxLength = attribute.DataType.In(ModelDataType.StringText, ModelDataType.ByteArray) ? (short)-1 : (short)attribute.MaxLength,
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
                        //add discriminator
                        //getLink and Add the column if not exists
                        foreach (var link in GeneralizationSh.GetLinksToSubclassesSh(entity))
                        {
                            var columnName = link.Discriminator.Left("=").Trim();
                            if (!table.Columns.Any(c => c.Name == columnName))
                            {
                                table.Columns.Add(new Column()
                                {
                                    Name = columnName,
                                    IsNullable = false,
                                    DbDataType = DataTypeEnum.TINYINT
                                });
                            }
                        }


                        #endregion

                        #region Indexes Info
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
                            //add properties
                            foreach (var colRef in idx.Properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var column = table.Columns.FirstOrDefault(e => e.Name == colRef.Trim());
                                if (column != null)
                                    index.Columns.Add(column);
                            }
                            //add INCLUDE Properties
                            foreach (var colRef in idx.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var column = table.Columns.FirstOrDefault(e => e.Name == colRef.Trim());
                                if (column != null)
                                    index.Include.Add(column);
                            }

                            table.Indexes.Add(index);
                        }
                        #endregion
                    }

                    schema.TablesBase.Add(table);

                }


                //Add Associations
                foreach (var schema in database.Schemas)
                {
                    foreach (Table table in schema.TablesBase)
                    {
                        foreach (var targetClass in modelClasses.Where(e => e.GetTableName(true) == table.Name))
                        {
                            //Direct assiciations
                            foreach (var link in Association.GetLinksToSourceModelClasses(targetClass))
                            {
                                string fkName = link.GetFkName();

                                if (!link.SourceModelClass.InStudy && !table.ForeignKey.Any(e => e.Name == fkName))
                                {
                                    Table sourceTable = database.FindTable(link.SourceModelClass.GetTableName(true)) as Linx.Tools.Migration.Table;

                                    ForeignKey fk = new ForeignKey()
                                    {
                                        DeleteAction = (link.WillCascadeOnDelete ? ForeignKey.ReferentialAction.Cascade : ForeignKey.ReferentialAction.NoAction),
                                        UpdateAction = ForeignKey.ReferentialAction.NoAction,
                                        Id = fkName,
                                        Name = fkName,
                                        Parent = table,
                                        Referenced = sourceTable,
                                        RemoveAutomaticIndex = link.RemoveAutomaticIndex
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
                            if (targetClass.MultipleAssociation != null)
                            {
                                foreach (var multipleOriginType in targetClass.MultipleAssociation.OriginTypes)
                                {
                                    foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(multipleOriginType))
                                    {
                                        string fkName = link.GetFkName();
                                        if (!link.OriginType.InStudy && !table.ForeignKey.Any(e => e.Name == fkName))
                                        {
                                            Linx.Tools.Migration.Table sourceTable = database.FindTable(link.OriginType.GetTableName(false)) as Linx.Tools.Migration.Table;

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
                                }
                            }

                            //Inheritance associations
                            var supLink = Generalization.GetLinkToSuperclass(targetClass);
                            if (supLink != null)
                            {
                                string fkName = "FK_" + table.Name + "__" + supLink.Superclass.Name;

                                if (!supLink.Superclass.InStudy && !table.ForeignKey.Any(e => e.Name == fkName))
                                {
                                    Table sourceTable = database.FindTable(supLink.Superclass.GetTableName(false)) as Table;

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

        private static void CreateIndexByFK(ForeignKey fk)
        {

        }
    }


}

