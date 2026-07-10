//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Immutability;
using Linx.Tools.Migration;

namespace Linx.BusinessDataModelDesigner
{
    public partial class ModelAttribute
    {
        public string GetColumnName()
        {
            return (this.ColumnName.IsNullOrEmpty() ? this.Name : this.ColumnName);
        }

        public string GetAggregationFunction()
        {
            string result = "";

            switch (this.AggregationFunction)
            {
                case AggregationFunctions.Average:
                    result = "AVG";
                    break;
                case AggregationFunctions.Count:
                case AggregationFunctions.Max:
                case AggregationFunctions.Min:
                case AggregationFunctions.Sum:
                    result = this.AggregationFunction.ToString().ToUpper();
                    break;
                case AggregationFunctions.CountDistinct:
                    result = "COUNT";
                    break;
                default:
                    break;
            }

            return result;
        }
        
        public string GetUniqueValue()
        {
            if (this.IsUniqueValue)
            {
                if (this.DataType == ModelDataType.Long)
                    return "Convert.ToInt64(Guid.NewGuid().ToString().Right(\"-\"), 16)";
                else if (this.DataType == ModelDataType.String)
                    return "Convert.ToInt64(Guid.NewGuid().ToString().Right(\"-\"), 16).ToString()";
                else if (this.DataType == ModelDataType.Guid)
                    return "System.Guid.NewGuid()";
            }

            return String.Empty;
        }

        public string GetDefaultValue()
        {   
            return this.DefaultValue;
        }

        public string GetAllLookUpInfo(List<ModelClass> modelClasses)
        {
            if (!this.ModelViewSource.IsNullOrEmpty() && this.ModelViewSource.Left(".") != this.ModelClass.ModelViewMainEntity)
            {
                string lookupEntityName = this.ModelViewSource.Left(".");
                string sourcePropName = this.ModelViewSource.Extract(".", "(");

                var lookupEntity = modelClasses.FirstOrDefault(e => e.Name == lookupEntityName);
                if (lookupEntity != null)
                {
                    var lookupProp = lookupEntity.Attributes.FirstOrDefault(e => e.Name == sourcePropName);
                    if (lookupProp != null)
                    {
                        var precision = GetPrecisionMetadata().ToString();
                        precision = (precision + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).Left(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator) + ":" + (precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).IsNullOrEmpty() ? "0" : precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
                        return @"[LinxPublicationField(LookUpInfo=""" + this.GetDataType() + "#" + sourcePropName + "#" + lookupProp.IsPrimaryKey.ToString().ToLower() + "##" + precision + "#" + this.DomainName + "#" + this.DisplayName + "#" + lookupEntity.Attributes.IndexOf(lookupProp).ToString() + "#true#::" + lookupEntityName + "##false#false##" + lookupEntityName + "#" + this.ModelClass.BusinessDataModelDesignerRoot.TargetNamespace + "#IQueryable#" + @""")]";
                    }
                }
            }
            
            return String.Empty;
        }

        public bool IsSelectedByAssociation()
        {
            bool isSelected = false;
            if (this.ModelClass != null && this.ModelClass.BusinessDataModelDesignerRoot != null && this.ModelClass.BusinessDataModelDesignerRoot.SelectedProperties.Count > 0)
                isSelected = this.ModelClass.BusinessDataModelDesignerRoot.SelectedProperties.Contains(this.ModelClass.Name + "." + this.Name);
            return isSelected;
        }

        public string GetDisplay()
        {
            return (this.IsSelectedByAssociation() ? ((char)9679).ToString() : string.Empty) + (this.HasIndex() ? ((char)9636).ToString() : string.Empty) + this.Name + ": " + this.GetDataType() + this.GetPrecision() + (this.IsNullable ? " Null" : string.Empty);
        }

        private bool HasIndex()
        {
            return this.ModelClass.ModelIndexes.Count > 0 && this.ModelClass.ModelIndexes.Any(e => ("," + e.Properties + ",").ToUpper().Replace(" DESC,", ",").Replace(" ASC,", ",").Replace(" ", "").Contains("," + (this.ColumnName.IsNullOrEmpty() ? this.Name : this.ColumnName).ToUpper() + ","));
        }

        internal decimal GetPrecisionMetadata()
        {
            if (this.MaxLength > 0 && this.GetDataType() == "string" && this.DataType != ModelDataType.StringText)
                return this.MaxLength;
            
            if (!this.Precision.IsNullOrEmpty())
            {
                decimal precision = this.Precision;
                if (!this.Scale.IsNullOrEmpty())
                    precision = decimal.Parse(((int)precision).ToString() + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + this.Scale);

                return precision;
            }

            return 0;
        }

        private string GetPrecision()
        {
            if(this.MaxLength > 0 && this.GetDataType() == "string" && this.DataType != ModelDataType.StringText)
            {
                return "(" + this.MaxLength.ToString() + ")";
            }
            else if (this.DataType == ModelDataType.Decimal)
                return "(" + this.Precision.ToString() + "," + this.Scale.ToString() + ")";

            return String.Empty;
        }

        public string GetDataType()
        {
            if (!this.CustomDataType.IsNullOrEmpty())
                return this.CustomDataType;
            else
                return GetAttribueDataType(this.DataType, this.IsNullable);
        }

        public bool IsNotMapped()
        {
            return this.NotMapped || !this.CustomDataType.IsNullOrEmpty();
        }

        public static string GetAttribueDataType(ModelDataType dataType, bool isNullable)
        {
            string result = String.Empty;

            switch (dataType)
            {
                case ModelDataType.Byte:
                    result = "byte";
                    break;
                case ModelDataType.SignedByte:
                    result = "sbyte";
                    break;
                case ModelDataType.UnsignedShort:
                    result = "ushort";
                    break;
                case ModelDataType.Int:
                    result = "int";
                    break;
                case ModelDataType.UnsignedInt:
                    result = "uint";
                    break;
                case ModelDataType.Long:
                    result = "long";
                    break;
                case ModelDataType.UnsignedLong:
                    result = "ulong";
                    break;
                case ModelDataType.Float:
                    result = "float";
                    break;
                case ModelDataType.Double:
                    result = "double";
                    break;
                case ModelDataType.Short:
                    result = "short";
                    break;
                case ModelDataType.Decimal:
                    result = "decimal";
                    break;
                case ModelDataType.Boolean:
                    result = "bool";
                    break;
                case ModelDataType.Date:
                case ModelDataType.DateTime:
                    result = "DateTime";
                    break;
                case ModelDataType.Guid:
                    result = "Guid";
                    break;
                case ModelDataType.ByteArray:
                case ModelDataType.Timestamp:
                    result = "Byte[]";
                    break;
                case ModelDataType.DateTimeOffset:
                    result = "DateTimeOffset";
                    break;
                default:
                    result = "string";
                    break;
            }

            return (isNullable && result != "string" && dataType != ModelDataType.ByteArray && dataType != ModelDataType.Timestamp ? String.Format("System.Nullable<{0}>", result) : result);
        }

        public Type GetDataType2()
        {
            return GetAttribueDataType2(this.DataType, this.IsNullable);
        }

        public bool IsIdentityDB()
        {
            if (!this.NotMapped && !this.ModelClass.NotMapped && this.IsIdentity && !this.IsNullable && this.ForeignKey.IsNullOrEmpty())
            {
                if (!this.IsPrimaryKey)
                {
                    //Verify PK
                    if (this.ModelClass.GetPrimaryKeys().Any(e => e.IsIdentity))
                        return false;

                    //Check other attributes as Identity
                    if (this.ModelClass.GetAllAttributes().Any(e => e != this && (!e.NotMapped && !e.ModelClass.NotMapped && e.IsIdentity && !e.IsNullable && e.ForeignKey.IsNullOrEmpty())))
                        return false;
                }

                string dataType = this.GetDataType().ToLower();
                return (dataType.Contains("int") || dataType.Contains("long") || dataType.Contains("short"));
            }
            else 
                return false;
        }

        public static Type GetAttribueDataType2(ModelDataType dataType, bool isNullable)
        {
            Type result;

            switch (dataType)
            {
                case ModelDataType.Byte:
                    result = (isNullable ? typeof(byte?) : typeof(byte));
                    break;
                case ModelDataType.SignedByte:
                    result = (isNullable ? typeof(sbyte?) : typeof(sbyte));
                    break;
                case ModelDataType.UnsignedShort:
                    result = (isNullable ? typeof(ushort?) : typeof(ushort));
                    break;
                case ModelDataType.Int:
                    result = (isNullable ? typeof(int?) : typeof(int));
                    break;
                case ModelDataType.UnsignedInt:
                    result = (isNullable ? typeof(uint?) : typeof(uint));
                    break;
                case ModelDataType.Long:
                    result = (isNullable ? typeof(long?) : typeof(long));
                    break;
                case ModelDataType.UnsignedLong:
                    result = (isNullable ? typeof(ulong?) : typeof(ulong));
                    break;
                case ModelDataType.Float:
                    result = (isNullable ? typeof(float?) : typeof(float));
                    break;
                case ModelDataType.Double:
                    result = (isNullable ? typeof(double?) : typeof(double));
                    break;
                case ModelDataType.Short:
                    result = (isNullable ? typeof(short?) : typeof(short));
                    break;
                case ModelDataType.Decimal:
                    result = (isNullable ? typeof(decimal?) : typeof(decimal));
                    break;
                case ModelDataType.Boolean:
                    result = (isNullable ? typeof(bool?) : typeof(bool));
                    break;
                case ModelDataType.Date:
                case ModelDataType.DateTime:
                    result = (isNullable ? typeof(DateTime?) : typeof(DateTime));
                    break;
                case ModelDataType.Guid:
                    result = (isNullable ? typeof(Guid?) : typeof(Guid));
                    break;
                case ModelDataType.ByteArray:
                case ModelDataType.Timestamp:
                    result = typeof(Byte[]);
                    break;
                case ModelDataType.DateTimeOffset:
                    result = (isNullable ? typeof(DateTimeOffset?) : typeof(DateTimeOffset));
                    break;
                default:
                    result = typeof(string);
                    break;
            }


            
            return result;
        }


        public string GetCustomAttributes(string indent)
        {
            CodeBuilder builder = new CodeBuilder(indent);
            foreach (string attr in this.CustomAttributes.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
            {
                builder.AddLine(attr);
            }
            return builder.GetBody();
        }
        
        #region Column from default default provider
        
        public string GetColumnTypeValue()
        {    
            return GetSqlColumnTypeValue();
        }

        public string GetDomainValues()
        {
            string values = "";
            if (!this.DomainName.IsNullOrEmpty())
            {
                var domain = this.ModelClass.BusinessDataModelDesignerRoot.GetAllDomains().FirstOrDefault(e => e.Name == this.DomainName);
                if (domain != null && domain.DomainValues.Count > 0)
                {
                    foreach (var value in domain.DomainValues)
                    {
                        values += (values.IsNullOrEmpty() ? "" : ", ") + value.Value;
                    }
                }
            }
            return values;
        }

        public DataTypeEnum GetColumnDataType()
        {
            DataTypeEnum result = DataTypeEnum.VARCHAR;

            switch (this.DataType)
            {
                case ModelDataType.Byte:
                case ModelDataType.SignedByte:
                case ModelDataType.Int:
                case ModelDataType.UnsignedInt:
                case ModelDataType.UnsignedShort:
                case ModelDataType.Short:
                    result = DataTypeEnum.INT;
                    break;
                case ModelDataType.Long:
                case ModelDataType.UnsignedLong:
                    result = DataTypeEnum.BIGINT;
                    break;
                case ModelDataType.Decimal:
                case ModelDataType.Double:
                    result = DataTypeEnum.DECIMAL;
                    break;                
                case ModelDataType.Float:
                    result = DataTypeEnum.FLOAT;
                    break;
                case ModelDataType.Boolean:
                    result = DataTypeEnum.BIT;
                    break;
                case ModelDataType.Date:
                    result = DataTypeEnum.DATE;
                    break;
                case ModelDataType.Timestamp:
                case ModelDataType.DateTimeOffset:
                case ModelDataType.DateTime:
                    result = DataTypeEnum.DATETIME;
                    break;
                case ModelDataType.StringChar:
                    result = DataTypeEnum.CHAR;
                    break;
                case ModelDataType.Guid:
                    result = DataTypeEnum.UNIQUEIDENTIFIER;
                    break;
                case ModelDataType.ByteArray:
                    result = DataTypeEnum.BINARY;
                    break;
                case ModelDataType.StringText:
                    result = DataTypeEnum.TEXT;
                    break;
                default:
                    result = DataTypeEnum.VARCHAR;
                    break;
            }

            return result;
        }

        public string GetSqlColumnTypeValue()
        {
            string result = String.Empty;

            switch (this.DataType)
            {
                case ModelDataType.Byte:
                case ModelDataType.SignedByte:
                case ModelDataType.Int:
                case ModelDataType.UnsignedInt:
                case ModelDataType.UnsignedShort:
                case ModelDataType.Short:
                    result = "INTEGER";
                    break;
                case ModelDataType.Long:
                case ModelDataType.UnsignedLong:
                    result = "BIGINT";
                    break;
                case ModelDataType.Decimal:
                    result = "DECIMAL(" + this.Precision.ToString() + "," + this.Scale.ToString() + ")";
                    break;
                case ModelDataType.Double:
                    result = "DOUBLE(" + this.Precision.ToString() + "," + this.Scale.ToString() + ")";
                    break;
                case ModelDataType.Float:
                    result = "FLOAT(" + this.Precision.ToString() + "," + this.Scale.ToString() + ")";
                    break;                
                case ModelDataType.Boolean:
                    result = "BOOLEAN";
                    break;
                case ModelDataType.Date:
                    result = "DATEONLY";
                    break;
                case ModelDataType.Timestamp:
                case ModelDataType.DateTimeOffset:
                case ModelDataType.DateTime:
                    result = "DATE";
                    break;
                case ModelDataType.StringChar:
                    result = "CHAR(" + this.MaxLength.ToString() + ")";
                    break;
                case ModelDataType.Guid:
                    result = "UUID";
                    break;
                case ModelDataType.ByteArray:
                    result = "ARRAY(DataTypes.INTEGER)";
                    break;
                case ModelDataType.StringText:
                    result = "TEXT";
                    break;                
                default:
                    result = "STRING(" + this.MaxLength.ToString() + ")";
                    break;
            }

            return result;
        }
        
        #endregion

        public int GetOrder()
        {
            int parentAttrCount = 0;
            var superClass = this.ModelClass.GetTopSuperClass();
            if (superClass != this.ModelClass)
            {
                bool endReached = false;
                Action<ModelClass> orderIncrement = null;
                if (this.ModelClass.Superclass != null)
                {
                    orderIncrement = (s) =>
                        {
                            parentAttrCount += s.Attributes.Count;
                            foreach (var b in s.Subclasses)
                            {
                                if (!endReached && b == this.ModelClass)
                                    endReached = true;

                                if (endReached)
                                    break;

                                orderIncrement(b);
                            }
                        };
                }
                else
                {
                    orderIncrement = (s) =>
                    {
                        parentAttrCount += s.Attributes.Count;
                        foreach (var b in s.SubclassesSh)
                        {
                            if (!endReached && b == this.ModelClass)
                                endReached = true;

                            if (endReached)
                                break;

                            orderIncrement(b);
                        }
                    };
                }
                orderIncrement(superClass);
            }

            return parentAttrCount + this.ModelClass.Attributes.IndexOf(this);
        }

        public string GetForeignKeyNavigation(List<BusinessDataModelDesignerRoot> models)
        {
            if (this.ForeignKey.IsNullOrEmpty())
                return String.Empty;
            else
                return this.ModelClass.GetNavigationByForeignKeyPropertyName(models, this.Name);
        }
        
        public void UpdateRelations(string oldName, bool force=false)
        {
            if (this.ModelClass == null || this.ModelClass.BusinessDataModelDesignerRoot == null || (!force && this.ModelClass.BusinessDataModelDesignerRoot.IsLocked))
                return;

            if (this.ModelClass == null)
                return;

            if (oldName.IsNullOrEmpty())
                oldName = this.Name;
            
            foreach (var target in this.ModelClass.TargetModelClasses.Where(e => !(e is ReferenceModelClass)))
            {
                var attr = target.Attributes.Where(e => ("." + e.ForeignKey).Right(".") == oldName).FirstOrDefault();
                if (attr != null)
                {
                    attr.DataType = this.DataType;
                    attr.ForeignKey = attr.ForeignKey.Left(".") + "." + this.Name;
                    attr.InStudy = this.InStudy || this.ModelClass.InStudy;
                    if (attr.Name == oldName || attr.Name == (oldName + "_FK"))
                    {
                        string newName = this.Name + (attr.Name == oldName ? String.Empty : "_FK");
                        if (!attr.ColumnName.IsNullOrEmpty() && (attr.ColumnName == attr.Name || attr.ColumnName == (attr.Name + "#").Left("_FK#")))
                        {
                            attr.ColumnName = (attr.ColumnName == attr.Name ? newName : (newName + "#").Left("_FK#"));
                        }
                        attr.Name = newName;
                        attr.DisplayName = this.DisplayName;
                    }
                }
            }

            foreach (var multi in this.ModelClass.MultipleAssociations.Where(e => e.TargetType != null))
            {
                var attr = multi.TargetType.Attributes.Where(e => ("." + e.ForeignKey).Right(".") == oldName).FirstOrDefault();
                if (attr != null)
                {
                    attr.DataType = this.DataType;
                    attr.InStudy = this.InStudy || this.ModelClass.InStudy;
                    attr.ForeignKey = attr.ForeignKey.Left(".") + "." + this.Name;
                    if (attr.Name == oldName || attr.Name == (oldName + "_FK"))
                    {
                        string newName = this.Name + (attr.Name == oldName ? String.Empty : "_FK");
                        if (!attr.ColumnName.IsNullOrEmpty() && (attr.ColumnName == attr.Name || attr.ColumnName == (attr.Name + "#").Left("_FK#")))
                        {
                            attr.ColumnName = (attr.ColumnName == attr.Name ? newName : (newName + "#").Left("_FK#"));
                        }
                        attr.Name = newName;
                        attr.DisplayName = this.DisplayName;
                    }
                }
            }

        }
    }
}
