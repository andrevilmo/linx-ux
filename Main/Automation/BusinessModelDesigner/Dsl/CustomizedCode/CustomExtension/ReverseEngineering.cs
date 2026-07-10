using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Xml;
using System.Reflection;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Linx.BusinessModelDesigner.CustomCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ComponentModel.Design;
using Linx.Tools.Migration;

namespace Linx.BusinessModelDesigner
{
    public partial class BusinessModelDesignerRoot
    {
        public ModelDataType DbToModelDataType(DataTypeEnum dataType, short maxLength)
        {
            ModelDataType result;

            switch (dataType)
            {
                case DataTypeEnum.BIGINT:
                    result = ModelDataType.Long;
                    break;
                case DataTypeEnum.BINARY:
                    result = ModelDataType.Byte;
                    break;
                case DataTypeEnum.BIT:
                    result = ModelDataType.Boolean;
                    break;
                case DataTypeEnum.CHAR:
                case DataTypeEnum.NCHAR:
                    result = ModelDataType.StringChar;
                    break;
                case DataTypeEnum.DATE:
                    result = ModelDataType.Date;
                    break;
                case DataTypeEnum.DATETIME:
                case DataTypeEnum.DATETIME2:
                case DataTypeEnum.SMALLDATETIME:
                case DataTypeEnum.TIME:
                    result = ModelDataType.DateTime;
                    break;
                case DataTypeEnum.DATETIMEOFFSET:
                    result = ModelDataType.DateTimeOffset;
                    break;
                case DataTypeEnum.NUMERIC:
                case DataTypeEnum.DECIMAL:
                case DataTypeEnum.REAL:
                case DataTypeEnum.SMALLMONEY:
                case DataTypeEnum.MONEY:
                     result = ModelDataType.Decimal;
                    break;
                case DataTypeEnum.FLOAT:
                    result = ModelDataType.Float;
                    break;
                case DataTypeEnum.GEOGRAPHY:
                case DataTypeEnum.GEOMETRY:
                case DataTypeEnum.HIERARCHYID:
                case DataTypeEnum.TIMESTAMP:
                case DataTypeEnum.IMAGE:
                case DataTypeEnum.VARBINARY:
                    result = ModelDataType.ByteArray;
                    break;
                case DataTypeEnum.INT:
                    result = ModelDataType.Int;
                    break;
                case DataTypeEnum.NTEXT:                
                case DataTypeEnum.TEXT:
                case DataTypeEnum.XML:
                    result = ModelDataType.StringText;
                    break;
                case DataTypeEnum.NVARCHAR:
                case DataTypeEnum.VARCHAR:
                case DataTypeEnum.SYSNAME:
                case DataTypeEnum.SQL_VARIANT:
                    result = ModelDataType.String;
                    break;
                case DataTypeEnum.SMALLINT:
                    result = ModelDataType.Short;
                    break;
                case DataTypeEnum.TINYINT:
                    result = ModelDataType.Byte;
                    break;
                case DataTypeEnum.UNIQUEIDENTIFIER:
                    result = ModelDataType.Guid;
                    break;                
                default:
                    result = ModelDataType.String;
                    break;
            }

            if (maxLength <= 0 && (result == ModelDataType.String || result == ModelDataType.StringChar))
            {
                result = ModelDataType.StringText;
            }
            
            return result;
        }

        public void CheckColumns(string name, string[] columns)
        {
            ModelClass newClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == name) as ModelClass;
            if (newClass != null)
            {
                foreach (var attrib in newClass.Attributes.Where(e => !e.IsCustomized && !columns.Contains(e.GetColumnName())).ToList())
                {
                    attrib.Delete();
                }
            }
        }

        public void CheckIndexes(string name, string[] indexes)
        {
            ModelClass newClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == name) as ModelClass;
            if (newClass != null)
            {
                foreach (var index in newClass.ModelIndexes.Where(e => !indexes.Contains(e.Name)).ToList())
                {
                    index.Delete();
                }
            }
        }

        public void CheckAssociations(string name, string[] fks)
        {            
            ModelClass targetClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == name) as ModelClass;
            if (targetClass != null)
            {
                foreach (var fk in Association.GetLinksToSourceModelClasses(targetClass).Where(e => !fks.Contains(e.ForeignKeyConstraintName)).ToList())
                {
                    fk.Delete();
                }
            }
        }
        

        public ModelClass AddTableClass(string name, ClassKind kind, string schema, string primaryKeyConstraintName, bool isClustered)
        {
            ModelClass newClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == name) as ModelClass;
            if (newClass == null)
            {
                newClass = new ModelClass(this.Partition)
                   {
                       Name = name,
                       Kind = kind,
                       Schema = schema,
                       PrimaryKeyConstraintName = primaryKeyConstraintName,
                       Table = "",
                       IsClustered = isClustered
                   };
                this.Types.Add(newClass);
            }
            else if (!(newClass is ReferenceModelClass))
            {
                newClass.Kind = kind;
                newClass.Schema = schema;
                newClass.PrimaryKeyConstraintName = primaryKeyConstraintName;
                newClass.IsClustered = isClustered;
                if (kind == ClassKind.DatabaseScript && newClass.Table.IsNullOrEmpty())
                    newClass.Table = name;
            }

            return newClass;
        }

        public void AddColumnAttribute(string parentTable, string name,
                            ModelDataType dataType,
                            bool isPrimaryKey,
                            bool isIdentity,
                            bool isNullable,
                            Int16 precision,
                            Int16 scale, Int16 maxLength, int indexOrder, string sqlDefault)
        {

            ModelClass modelClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == parentTable) as ModelClass;
            if (modelClass != null && !(modelClass is ReferenceModelClass))
            {
                ModelAttribute newAttr = modelClass.Attributes.FirstOrDefault(e => e.GetColumnName() == name);
                if (newAttr == null)
                {
                    newAttr = new ModelAttribute(modelClass.Partition)
                        {
                            Name = name,
                            DataType = dataType,
                            IsPrimaryKey = isPrimaryKey,
                            IsIdentity = isIdentity,
                            IsNullable = isNullable,
                            Precision = precision,
                            Scale = scale,
                            ColumnName = "",
                            MaxLength = maxLength,
                            SqlDefault = sqlDefault
                        };

                    modelClass.Attributes.Add(newAttr);
                }
                else
                {
                    newAttr.DataType = dataType;
                    newAttr.IsPrimaryKey = isPrimaryKey;
                    newAttr.IsIdentity = isIdentity;
                    newAttr.IsNullable = isNullable;
                    newAttr.Precision = precision;
                    newAttr.Scale = scale;
                    newAttr.MaxLength = maxLength;
                    newAttr.SqlDefault = sqlDefault;
                }
                if (modelClass.Kind == ClassKind.DatabaseScript && newAttr.ColumnName.IsNullOrEmpty())
                    newAttr.ColumnName = name;
                modelClass.Attributes.Move(newAttr, indexOrder);
            }
        }

        public void AddTableIndex(string parentTable, string name,
                            string properties,
                            bool isUnique,
                            int indexOrder, bool isClustered)
        {

            ModelClass modelClass = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == parentTable) as ModelClass;
            if (modelClass != null && !(modelClass is ReferenceModelClass))
            {
                ModelIndex newIndex = modelClass.ModelIndexes.FirstOrDefault(e => e.Name == name);
                if (newIndex == null)
                {
                    newIndex = new ModelIndex(modelClass.Partition)
                    {
                        Name = name,
                        Properties = properties,
                        IsUnique = isUnique,
                        IsClustered = isClustered
                    };

                    modelClass.ModelIndexes.Add(newIndex);
                }
                else
                {
                    newIndex.Name = name;
                    newIndex.Properties = properties;
                    newIndex.IsUnique = isUnique;
                    newIndex.IsClustered = isClustered;
                }
                modelClass.ModelIndexes.Move(newIndex, indexOrder);
            }
        }

        public void AddTableAssociation(string name,
                            string tableFrom,
                            string tableTo,
                            Multiplicity multiplicity,
                            bool deleteCascade, string[] columns)
        {

            ModelClass source = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == tableFrom) as ModelClass;
            ModelClass target = this.Types.FirstOrDefault(e => e is ModelClass && ((ModelClass)e).GetTableName(true) == tableTo) as ModelClass;
            if (source != null && target != null && !(target is ReferenceModelClass))
            {
                Association link = Association.GetLinks(source, target).FirstOrDefault(e => e.ForeignKeyConstraintName == name);
                if (link == null)
                {
                    link = new Association(source, target)
                    {
                        ForeignKeyConstraintName = name,
                        SourceMultiplicity = SourceMultiplicity.One,
                        TargetMultiplicity = multiplicity,
                        WillCascadeOnDelete = deleteCascade
                    };
                }
                else
                {
                    link.TargetMultiplicity = multiplicity;
                    link.WillCascadeOnDelete = deleteCascade;
                }

                if (link != null)
                {
                    foreach (var columnRef in columns)
                    {
                        ModelAttribute attrTarget = target.Attributes.FirstOrDefault(e => e.GetColumnName() == columnRef.Left(":"));
                        if (attrTarget != null)
                        {
                            ModelAttribute ettrSource = source.Attributes.FirstOrDefault(e => e.GetColumnName() == columnRef.Right(":"));
                            if (ettrSource != null)
                            {
                                attrTarget.ForeignKey = link.GetFkId() + "." + ettrSource.Name;
                            }
                        }
                    }
                }
            }
        }

    }
}
