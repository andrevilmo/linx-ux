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
    public partial class MultipleAssociationOrigin
    {
        public string GetFkName(bool noIdPart = false, bool automaticNames = false)
        {
            if (this.ForeignKeyConstraintName.IsNullOrEmpty() || automaticNames)
            {
                string originalTableName = this.MultipleAssociation.TargetType.GetTableName(true);
                string tableName = originalTableName;
                if (tableName.Length > 18)
                    tableName = tableName.Left(18);
                return "FK_" + (noIdPart ? originalTableName : tableName + "_" + HashNames.CalculateMD5Hash(originalTableName + "|" + String.Join("|", this.GetTargetColumns().OrderBy(e => e))).Right(8));
            }
            else
                return this.ForeignKeyConstraintName;
        }

        public bool HasRelationError()
        {
            var sourceAttrs = GetSourceAttributeElements();
            var targetAttrs = GetTargetAttributeElements();

            if (sourceAttrs.Length != targetAttrs.Length)
                return true;

            for (int idx = 0; idx < sourceAttrs.Length; idx++)
            {
                if (this.OriginType.GetRoleName(sourceAttrs[idx].Name) != targetAttrs[idx].ForeignKey.Right(".") || targetAttrs[idx].DataType != sourceAttrs[idx].DataType)
                    return true;
            }

            return false;
        }

        public void CorrectRelationInfo()
        {
            if (this.MultipleAssociation.TargetType is ReferenceModelClass)
                return;

            var sourceAttrs = GetSourceAttributeElements();
            var targetAttrs = GetTargetAttributeElements();

            if (sourceAttrs.Length == targetAttrs.Length)
            {
                bool hasChanges = false;
                using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Adjust Relation."))
                {
                    for (int idx = 0; idx < sourceAttrs.Length; idx++)
                    {
                        if (sourceAttrs.Length == 1 && this.OriginType.GetRoleName(sourceAttrs[idx].Name) != targetAttrs[idx].ForeignKey.Right("."))
                        {
                            targetAttrs[idx].ForeignKey = targetAttrs[idx].ForeignKey.Left(".") + "." + this.OriginType.GetRoleName(sourceAttrs[idx].Name);
                            hasChanges = true;
                        }

                        if (sourceAttrs.Length == 1 && this.OriginType.GetRoleName(sourceAttrs[idx].Name) == targetAttrs[idx].ForeignKey.Right(".") && sourceAttrs[idx].DataType != targetAttrs[idx].DataType)
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

        public ModelAttribute[] GetTargetAttributeElements()
        {
            return this.MultipleAssociation.TargetType.Attributes.Where(e => this.IsFkAttrinute(e)).ToArray();
        }

        public ModelAttribute[] GetSourceAttributeElements()
        {
            return this.OriginType.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey).ToArray();
        }

        public string[] GetTargetColumns()
        {
            return this.MultipleAssociation.TargetType.Attributes.Where(e => this.IsFkAttrinute(e)).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray();
        }

        public string GetFkId()
        {
            return (String.IsNullOrWhiteSpace(this.ForeignKeyConstraintName) ? this.Id.ToString() : this.ForeignKeyConstraintName);
        }

        public bool IsFkAttrinute(ModelAttribute attribute)
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && this.IsKey(attribute.ForeignKey.Left("."));
        }

        public bool IsFkAttrinute(ModelAttribute attribute, string sourceName, string oldForeignKeyConstraintName = "")
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && ((!String.IsNullOrWhiteSpace(this.ForeignKeyConstraintName) && attribute.ForeignKey == this.ForeignKeyConstraintName + "." + sourceName) || (attribute.ForeignKey == this.Id.ToString() + "." + sourceName) || (!String.IsNullOrWhiteSpace(oldForeignKeyConstraintName) && attribute.ForeignKey == oldForeignKeyConstraintName + "." + sourceName));
        }
        public bool IsKey(string refKey)
        {
            return refKey.InList(this.ForeignKeyConstraintName, this.Id.ToString());
        }

        public void SelectProperties()
        {
            if (this.MultipleAssociation.TargetType != null)
            {
                this.MultipleAssociation.TargetType.BusinessDataModelDesignerRoot.SelectedProperties.Clear();
                foreach (var attr in this.GetTargetAttributeElements())
                {
                    this.MultipleAssociation.TargetType.BusinessDataModelDesignerRoot.SelectedProperties.Add(attr.ModelClass.Name + "." + attr.Name);
                }
            }
        }

        public bool IsFkAttrinuteByName(ModelAttribute attribute, string sourceName)
        {
            return !attribute.ForeignKey.IsNullOrEmpty() && (attribute.ForeignKey.Right(".") == sourceName);
        }

        public void UpdatePropertyRelations(string oldForeignKeyConstraintName)
        {
            if (this.OriginType == null || this.OriginType.BusinessDataModelDesignerRoot == null || this.OriginType.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (this.MultipleAssociation != null && this.OriginType != null && this.MultipleAssociation.TargetType != null && !(this.MultipleAssociation.TargetType is ReferenceModelClass))
            {
                //Check if should remove old primary keys
                foreach (var attrPK in this.MultipleAssociation.TargetType.Attributes.Where(p => p.IsPrimaryKey && p.ForeignKey.IsNullOrEmpty()).ToList())
                {
                    this.MultipleAssociation.TargetType.Attributes.Remove(attrPK);
                }

                foreach (var source in this.OriginType.GetTopSuperClass().Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.MultipleAssociation.TargetType.Attributes.Where(p => this.IsFkAttrinute(p, this.OriginType.GetRoleName(source.Name), oldForeignKeyConstraintName)).FirstOrDefault();
                    if (attr == null)
                    {
                        attr = this.MultipleAssociation.TargetType.Attributes.Where(p => this.IsFkAttrinuteByName(p, this.OriginType.GetRoleName(source.Name))).FirstOrDefault();
                        if (attr != null && !MultipleAssociationOrigin.GetLinksToOriginTypes(this.MultipleAssociation).Any(e => e.GetFkId() == attr.ForeignKey.Left("."))) //Adjust Link
                            attr.ForeignKey = this.GetFkId() + "." + this.OriginType.GetRoleName(source.Name);
                        else
                            attr = null;

                    }
                    if (attr == null)
                    {
                        attr = (ModelAttribute)this.MultipleAssociation.TargetType.Attributes.AddNew();
                        attr.CopyInstanceFrom(source);
                        attr.IsIdentity = false;
                        attr.SqlDefault = String.Empty;
                        attr.DataType = source.DataType;
                        attr.ForeignKey = this.GetFkId() + "." + this.OriginType.GetRoleName(source.Name);
                        attr.Name = this.OriginType.GetRoleName(source.Name);
                        this.MultipleAssociation.TargetType.Attributes.Move(attr, 0);
                    }
                    else if (attr.ForeignKey.Left(".") != this.GetFkId()) //Adjust Link
                        attr.ForeignKey = this.GetFkId() + "." + this.OriginType.GetRoleName(source.Name);

                    attr.IsPrimaryKey = true;
                    attr.IsNullable = false;
                }
            }
        }

        public void DeletePropertyRelations()
        {
            if (this.OriginType == null || this.OriginType.BusinessDataModelDesignerRoot == null || this.OriginType.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (this.MultipleAssociation != null && this.OriginType != null && this.MultipleAssociation.TargetType != null && !(this.MultipleAssociation.TargetType is ReferenceModelClass))
            {
                foreach (var source in this.OriginType.GetTopSuperClass().Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.MultipleAssociation.TargetType.Attributes.Where(p => this.IsFkAttrinute(p, this.OriginType.GetRoleName(source.Name))).FirstOrDefault();
                    if (attr != null)
                        this.MultipleAssociation.TargetType.Attributes.Remove(attr);
                }
            }
        }

    }
}
