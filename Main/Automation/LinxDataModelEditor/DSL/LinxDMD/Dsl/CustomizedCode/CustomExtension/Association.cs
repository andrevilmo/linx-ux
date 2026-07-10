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
using Linx.BusinessDataModelDesigner.CustomizedCode.Util;

namespace Linx.BusinessDataModelDesigner
{

    public partial class Association
    {
        public string GetTargetProperties()
        {
            string properties = String.Empty;
            if (this.TargetModelClass != null)
            {
                var attrs = GetTargetAttributes();
                if (attrs.Length > 0)
                {
                    properties = ",";
                    properties += String.Join(",", attrs);
                    properties += ",";
                }
            }
            return properties;
        }

        public void Remove()
        {
            using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Delete Reference."))
            {
                var root = this.TargetModelClass.BusinessDataModelDesignerRoot;
                bool isLocked = root.IsLocked;
                root.IsLocked = true;
                this.Delete();
                root.IsLocked = isLocked;
                transaction.Commit();
            }
        }


        public void SelectProperties()
        {
            this.TargetModelClass.BusinessDataModelDesignerRoot.SelectedProperties.Clear();
            foreach (var attr in this.GetTargetAttributeElements())
            {
                this.TargetModelClass.BusinessDataModelDesignerRoot.SelectedProperties.Add(attr.ModelClass.Name + "." + attr.Name);
            }
        }

        public string[] GetTargetAttributes()
        {
            return this.TargetModelClass.Attributes.Where(e => this.IsFkAttrinute(e)).Select(e => e.Name).OrderBy(e => e).ToArray();
        }

        public string[] GetTargetColumns()
        {
            return this.TargetModelClass.Attributes.Where(e => this.IsFkAttrinute(e)).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray();
        }

        public ModelAttribute[] GetTargetAttributeElements()
        {
            return this.TargetModelClass.Attributes.Where(e => this.IsFkAttrinute(e)).ToArray();
        }

        public ModelAttribute[] GetSourceAttributeElements()
        {
            return this.SourceModelClass.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey).ToArray();
        }

        public bool HasRelationError()
        {
            var sourceAttrs = GetSourceAttributeElements();
            var targetAttrs = GetTargetAttributeElements();

            if (sourceAttrs.Length != targetAttrs.Length)
                return true;

            for (int idx = 0; idx < sourceAttrs.Length; idx++)
            {
                if (this.SourceModelClass.GetRoleName(sourceAttrs[idx].Name) != targetAttrs[idx].ForeignKey.Right(".") || targetAttrs[idx].DataType != sourceAttrs[idx].DataType)
                    return true;
            }

            return false;
        }

        public void CorrectRelationInfo()
        {
            if (this.TargetModelClass is ReferenceModelClass)
                return;

            var sourceAttrs = GetSourceAttributeElements();
            var targetAttrs = GetTargetAttributeElements();

            if (sourceAttrs.Length == targetAttrs.Length)
            {
                bool hasChanges = false;
                using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Adjust Relation."))
                {
                    //Adjust id reference for entities that are not references.
                    if (!this.IdReference.IsNullOrEmpty() && !(this.SourceModelClass is ReferenceModelClass) && !(this.TargetModelClass is ReferenceModelClass))
                    {
                        //Adjust properties
                        foreach (var prop in this.GetTargetAttributeElements().ToArray())
                        {
                            if (prop.ForeignKey.Contains(this.IdReference.ToString()))
                                prop.ForeignKey = prop.ForeignKey.Replace(this.IdReference.ToString(), this.Id.ToString());
                        }
                        //Adjust reference
                        this.IdReference = Guid.Empty;

                        hasChanges = true;
                    }

                    for (int idx = 0; idx < sourceAttrs.Length; idx++)
                    {
                        if (sourceAttrs.Length == 1 && this.SourceModelClass.GetRoleName(sourceAttrs[idx].Name) != targetAttrs[idx].ForeignKey.Right("."))
                        {
                            targetAttrs[idx].ForeignKey = targetAttrs[idx].ForeignKey.Left(".") + "." + this.SourceModelClass.GetRoleName(sourceAttrs[idx].Name);
                            hasChanges = true;
                        }

                        if (sourceAttrs.Length == 1 && this.SourceModelClass.GetRoleName(sourceAttrs[idx].Name) == targetAttrs[idx].ForeignKey.Right(".") && sourceAttrs[idx].DataType != targetAttrs[idx].DataType)
                        {
                            targetAttrs[idx].DataType = sourceAttrs[idx].DataType;
                            hasChanges = true;
                        }
                    }
                    if (hasChanges)
                        transaction.Commit();
                    else
                        transaction.Rollback();
                }
            }
        }

        public string GetFkName(bool noIdPart = false, bool automaticNames = false)
        {
            if (this.ForeignKeyConstraintName.IsNullOrEmpty() || automaticNames)
            {
                string originalTableName = this.TargetModelClass.GetTableName(true);
                string tableName = originalTableName;
                if (tableName.Length > 18)
                    tableName = tableName.Left(18);
                return "FK_" + (noIdPart ? originalTableName : tableName + "_" + HashNames.CalculateMD5Hash(originalTableName + "|" + String.Join("|", this.GetTargetColumns().OrderBy(e => e))).Right(8));
            }
            else
                return this.ForeignKeyConstraintName;
        }

        public string GetSourcePropertyNameToTargetInfoValue()
        {
            return (this.SourcePropertyNameToTarget ?? "") + (this.WillCascadeOnDelete ? "(DC)" : "");
        }

        public string GetFkId()
        {
            return (String.IsNullOrWhiteSpace(this.ForeignKeyConstraintName) ? this.Id.ToString() : this.ForeignKeyConstraintName);
        }

        public bool IsFkAttrinute(ModelAttribute attribute)
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && this.IsKey(attribute.ForeignKey.Left("."));
        }

        public bool IsKey(string refKey)
        {
            return refKey.InList(this.ForeignKeyConstraintName, this.Id.ToString(), this.IdReference.ToString());
        }

        public bool IsFkAttrinute(ModelAttribute attribute, string sourceName, string oldForeignKeyConstraintName = "")
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && ((!String.IsNullOrWhiteSpace(this.ForeignKeyConstraintName) && attribute.ForeignKey == this.ForeignKeyConstraintName + "." + sourceName) || (attribute.ForeignKey == this.Id.ToString() + "." + sourceName) || (attribute.ForeignKey == this.IdReference.ToString() + "." + sourceName) || (!String.IsNullOrWhiteSpace(oldForeignKeyConstraintName) && attribute.ForeignKey == oldForeignKeyConstraintName + "." + sourceName));
        }

        public bool IsFkAttrinuteByName(ModelAttribute attribute, string sourceName)
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && (attribute.ForeignKey.Right(".") == sourceName);
        }

        public void UpdatePropertyRelations(string oldForeignKeyConstraintName)
        {
            if (this.SourceModelClass == null || this.SourceModelClass.BusinessDataModelDesignerRoot == null || this.SourceModelClass.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (this.SourceModelClass != null && this.TargetModelClass != null && !(this.TargetModelClass is ReferenceModelClass))
            {
                //Check if should remove old primary keys
                if (this.TargetMultiplicity == Multiplicity.One)
                {
                    var keys = this.SourceModelClass.GetTopSuperClass().Attributes.Where(p => p.IsPrimaryKey).Select(p => this.SourceModelClass.GetRoleName(p.Name)).ToList();
                    foreach (var attrPK in this.TargetModelClass.Attributes.Where(p => p.IsPrimaryKey && !keys.Contains(("." + p.ForeignKey).Right("."))).ToList())
                    {
                        this.TargetModelClass.Attributes.Remove(attrPK);
                    }
                }

                int orderIndex = 0;
                foreach (var source in this.SourceModelClass.GetTopSuperClass().Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.TargetModelClass.Attributes.Where(p => this.IsFkAttrinute(p, this.SourceModelClass.GetRoleName(source.Name), oldForeignKeyConstraintName)).FirstOrDefault();
                    if (attr == null)
                    {
                        attr = this.TargetModelClass.Attributes.Where(p => this.IsFkAttrinuteByName(p, this.SourceModelClass.GetRoleName(source.Name))).FirstOrDefault();
                        if (attr != null && !Association.GetLinksToSourceModelClasses(this.TargetModelClass).Any(e => e.GetFkId() == attr.ForeignKey.Left("."))) //Adjust Link
                            attr.ForeignKey = this.GetFkId() + "." + this.SourceModelClass.GetRoleName(source.Name);
                        else
                            attr = null;
                    }
                    if (attr == null)
                    {
                        attr = (ModelAttribute)this.TargetModelClass.Attributes.AddNew();
                        attr.CopyInstanceFrom(source);
                        attr.IsPrimaryKey = false;
                        attr.IsIdentity = false;
                        attr.SqlDefault = String.Empty;
                        attr.ForeignKey = this.GetFkId() + "." + this.SourceModelClass.GetRoleName(source.Name);
                        attr.Name = this.SourceModelClass.GetRoleName(source.Name);
                        if (this.TargetMultiplicity == Multiplicity.One)
                        {
                            this.TargetModelClass.Attributes.Move(attr, orderIndex);
                            orderIndex++;
                        }
                    }
                    else if (attr.ForeignKey.Left(".") != this.GetFkId()) //Adjust Link
                        attr.ForeignKey = this.GetFkId() + "." + this.SourceModelClass.GetRoleName(source.Name);

                    if (!attr.IsPrimaryKey && this.TargetMultiplicity == Multiplicity.One)
                        attr.IsPrimaryKey = true;
                    attr.IsNullable = this.TargetMultiplicity == Multiplicity.ZeroOne || this.TargetMultiplicity == Multiplicity.ZeroMany;
                }
            }
        }

        public void DeletePropertyRelations()
        {
            if (this.SourceModelClass == null || this.SourceModelClass.BusinessDataModelDesignerRoot == null || this.SourceModelClass.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (this.SourceModelClass != null && this.TargetModelClass != null && !(this.TargetModelClass is ReferenceModelClass))
            {
                foreach (var source in this.SourceModelClass.GetTopSuperClass().Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.TargetModelClass.Attributes.Where(p => this.IsFkAttrinute(p, this.SourceModelClass.GetRoleName(source.Name))).FirstOrDefault();
                    if (attr != null)
                    {
                        this.TargetModelClass.Attributes.Remove(attr);
                    }
                    this.TargetModelClass.AddPrimaryKey();
                }
            }
        }

    }
}
