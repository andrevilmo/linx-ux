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
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Linx.BusinessDataModelDesigner.CustomizedCode.Util;

namespace Linx.BusinessDataModelDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public partial class ModelClass
    {

        public void AddServerEvent(string eventName)
        {
            ClassOperation customEvent = new ClassOperation(this.Partition);
            customEvent.Name = eventName;
            customEvent.OverloadName = eventName;
            customEvent.IsUniqueOverload = true;
            customEvent.Access = OperationAccess.Public;
            customEvent.ReturnType = "void";
            customEvent.IsEvent = true;

            switch (eventName)
            {
                case "OnValidatingChanges":
                    customEvent.Parameters = "Object db#Object entity#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute for validating data changes.";
                    customEvent.ReturnType = "bool";
                    break;
                case "OnSavingChanges":
                    customEvent.Parameters = "Object db#Object entity#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute before save changes.";
                    break;
                case "OnSavingContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "Object db#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute before save context changes.";
                    break;
                case "OnSavedChanges":
                    customEvent.Parameters = "Object db#Object entity#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute after save changes.";
                    break;
                case "OnSavedContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "Object db#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute after save context changes.";
                    break;
                case "OnTransactingChanges":
                    customEvent.Parameters = "Object db#Object entity#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute on transaction starting.";
                    break;
                case "OnTransactingContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "Object db#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute on transaction context starting.";
                    break;
                case "OnTransactedChanges":
                    customEvent.Parameters = "Object db#Object entity#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute on transaction ending.";
                    break;
                case "OnTransactedContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "Object db#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute on transaction context ending.";
                    break;
                case "OnPrepareForSearching":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "ref string dynQuery#List<EntitySearch> searchList";
                    customEvent.DocComment = "Prepare filter for searching.";
                    break;
                case "OnSearching":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "string searchDefinition#bool noAssociations#List<EntitySearch> searchList";
                    customEvent.DocComment = "Execute before search data.";
                    break;
                case "OnFiltering":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "List<EntitySearch> searchList";
                    customEvent.DocComment = "Execute before apply filter.";
                    break;
                case "OnSearchingReplacement":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "object context#string dynQuery#List<ObjectParameter> parameters#List<EntitySearch> entitySearchList";
                    customEvent.ReturnType = "List<" + this.Name + ">";
                    customEvent.DocComment = "Replace the automatic search method.";
                    break;
                default:
                    //foreach (LookUpAdapter lookUpAdapter in this.LookUpAdapters)
                    //{
                    //    if (lookUpAdapter.IsOnLookingUp(eventName))
                    //    {
                    //        customEvent.IsStatic = true;
                    //        customEvent.Parameters = "ref " + lookUpAdapter.QueryReturnType.ToString() + "<" + lookUpAdapter.Name + "> searchDefinition#System.String propertyName#EntitySearch entitySearch";
                    //        customEvent.DocComment = "Execute before lookup on server side.";
                    //    }
                    //}
                    break;
            }

            //Add event
            this.Operations.Add(customEvent);
        }

        public string GetSqlQuery(int top, string filter)
        {
            string result = String.Empty;
            string startCommand = String.Empty, endCommand = String.Empty;

            var defaultProvider = this.BusinessDataModelDesignerRoot.GetDefaultProvider();
            switch (defaultProvider)
            {
                case Provider.SQLServer:
                    startCommand = "TOP " + top.ToString();
                    break;
                case Provider.MySQL:
                case Provider.SQLite:
                case Provider.PostgreSQL:
                    endCommand = "LIMIT " + top.ToString();
                    break;
                default:
                    break;
            }

            if (this.Kind == ClassKind.Table || this.Kind == ClassKind.DatabaseView || this.Kind == ClassKind.ModelView)
            {
                string delimit = (defaultProvider == Provider.PostgreSQL ? "\"" : "");
                foreach (var prop in this.GetAllInheritanceAttributes())
                {
                    result += (result.IsNullOrEmpty() ? "SELECT " + startCommand + " " : ", ") + delimit + prop.GetColumnName() + delimit;
                }

                if (this.Kind == ClassKind.ModelView)
                {
                    string partialScript = "";
                    string jsScript = this.GetBusinessViewLinqDefinition("		        ", this.BusinessDataModelDesignerRoot, (this.BusinessDataModelDesignerRoot.GetDefaultProvider() == Provider.PostgreSQL ? "\"" : ""));
                    using (ScriptEngine engine = new ScriptEngine("jscript"))
                    {
                        ParsedScript parsed = engine.Parse("function getSqlScript() { " + jsScript + " }");
                        partialScript = parsed.CallMethod("getSqlScript").ToString();
                    }

                    result += " FROM " + partialScript + " AS QR";
                }
                else
                {
                    bool removeSchema = defaultProvider.In(Provider.MySQL, Provider.SQLite);
                    if (!delimit.IsNullOrEmpty())
                        result += " FROM " + delimit + this.GetTableName(removeSchema).Replace(".", delimit + "." + delimit) + delimit;
                    else
                        result += " FROM " + this.GetTableName(removeSchema);
                }

                if (!filter.IsNullOrEmpty())
                {
                    result += " WHERE " + filter;
                }

                result += " " + endCommand;

            }

            return result;
        }

        public bool IsInheritanceClass()
        {
            bool isIC = false;

            try
            {
                isIC = this.Superclass == null && this.SuperclassSh == null && (this.Subclasses.Count > 0 || this.SubclassesSh.Count > 0);
            }
            catch { }

            return isIC;
        }

        public bool HasRedundantInheritanceProperties()
        {
            if (this.Superclass != null || this.SuperclassSh != null)
            {
                var thisAttributes = this.Attributes.ToArray();

                var super = this.Superclass ?? this.SuperclassSh;
                while (super != null)
                {
                    if (super.Attributes.Any(e => thisAttributes.Any(at => at.Name == e.Name)))
                        return true;
                    super = super.Superclass ?? super.SuperclassSh;
                }
            }

            return false;
        }

        public void AdjustInheritedSchema()
        {
            ModelClass baseClass = this.SuperclassSh;
            if (baseClass != null && this.Schema != baseClass.Schema)
            {
                using (Transaction transaction =
                                              this.Store.TransactionManager.BeginTransaction("Adjust Discriminator."))
                {
                    this.Schema = baseClass.Schema;
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<ModelAttribute> GetAllInheritanceAttributes()
        {
            List<ModelAttribute> attributes = new List<ModelAttribute>(this.Attributes);

            var baseClass = this.SuperclassSh ?? this.Superclass;
            while (baseClass != null)
            {
                attributes.AddRange(baseClass.Attributes);
                baseClass = baseClass.SuperclassSh ?? baseClass.Superclass;
            }

            return attributes;
        }

        public string GetRoleName(string primaryKeyName)
        {
            return (this.PrimaryKeyColumnMap.IsNullOrEmpty() ? primaryKeyName : this.PrimaryKeyColumnMap);
        }

        public string GetTableName(bool removeSchema = false)
        {
            var theClass = this;
            while (theClass.SuperclassSh != null)
            {
                theClass = theClass.SuperclassSh;
            }
            return (removeSchema ? String.Empty : (theClass.Schema.IsNullOrEmpty() ? "dbo" : theClass.Schema) + ".") + (theClass.Table.IsNullOrEmpty() ? theClass.Name : theClass.Table);
        }

        public void FindMe()
        {
            this.BusinessDataModelDesignerRoot.SelectShape(this.Name);
        }

        public string GetFkBaseName()
        {
            string originalTableName = this.GetTableName(true);
            string hashValue = HashNames.CalculateMD5Hash(originalTableName + "|" + String.Join("|", this.GetPrimaryKeys().Select(e => e.GetColumnName()).OrderBy(e => e)));
            if (this.Superclass != null)
            {
                var link = Generalization.GetLink(this.Superclass, this);
                string tableName = originalTableName;
                if (tableName.Length > 18)
                    tableName = tableName.Left(18);
                return "FK_" + tableName + "_" + hashValue.Right(8);
            }
            else return "FK_" + hashValue.Right(8);
        }

        public void HideElementAssociations()
        {
            foreach (var link in Association.GetLinksToSourceModelClasses(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Hide();
            }

            foreach (var link in Association.GetLinksToTargetModelClasses(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Hide();
            }

            foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Hide();
                var mult = link.MultipleAssociation.GetPresentation<ShapeElement>();
                if (mult != null)
                    mult.Hide();
            }

            foreach (var link in new MultipleAssociationTarget[] { MultipleAssociationTarget.GetLinkToMultipleAssociation(this) })
            {
                if (link != null)
                {
                    var connector = link.GetPresentation<ShapeElement>();
                    if (connector != null)
                        connector.Hide();
                    var mult = link.MultipleAssociation.GetPresentation<ShapeElement>();
                    if (mult != null)
                        mult.Hide();
                }
            }
        }

        public void ShowElementAssociations()
        {
            foreach (var link in Association.GetLinksToSourceModelClasses(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Show();
            }

            foreach (var link in Association.GetLinksToTargetModelClasses(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Show();
            }

            foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(this))
            {
                var connector = link.GetPresentation<ShapeElement>();
                if (connector != null)
                    connector.Show();
                var mult = link.MultipleAssociation.GetPresentation<ShapeElement>();
                if (mult != null)
                    mult.Show();
            }

            foreach (var link in new MultipleAssociationTarget[] { MultipleAssociationTarget.GetLinkToMultipleAssociation(this) })
            {
                if (link != null)
                {
                    var connector = link.GetPresentation<ShapeElement>();
                    if (connector != null)
                        connector.Show();
                    var mult = link.MultipleAssociation.GetPresentation<ShapeElement>();
                    if (mult != null)
                        mult.Show();
                }
            }
        }

        public string GetViewDbSets()
        {
            return (this.ModelViewDbSets.IsNullOrEmpty() ? "" : " typeof(" + this.ModelViewDbSets.Replace(",", "), typeof(") + ") ");
        }

        public void ConfigureBusinessView()
        {
            var builder = new Linx.BusinessDataModelDesigner.CustomCode.frmBusinessViewBuilder() { Entity = this };
            builder.ShowDialog();
        }

        public void PreViewEntity()
        {
            var preview = new Linx.BusinessDataModelDesigner.CustomizedCode.Forms.frmQueryPreview();
            preview.ContextName = this.BusinessDataModelDesignerRoot.GetNamespace() + "." + this.BusinessDataModelDesignerRoot.GetDataContextName();
            preview.EntityClass = this;
            preview.ShowDialog();
        }


        public void AdjustValidatableMethod()
        {
            if (this is ReferenceModelClass)
                return;

            string valitationMethod = "ValidateEntity";
            if (this.IsValidatable)
            {
                var op = this.Operations.FirstOrDefault(e => e.Name == valitationMethod);
                if (op == null)
                {
                    op = this.Operations.AddNew() as ClassOperation;
                    op.Comment = "Execute business tasks or return an error list for cancelling the process.";
                    if (this.Kind == ClassKind.ModelView)
                    {
                        op.Comment += "\r\nFor accessing the updatable entities of this view, use the following syntax: var updatableEntity = context." + this.Name + ".GetUpdatableEntity<UpdatableEntityName>(this);";
                    }
                    op.Name = valitationMethod;
                    op.OverloadName = valitationMethod;
                    op.Parameters = this.BusinessDataModelDesignerRoot.GetDataContextName() + " context#System.Data.Entity.EntityState state";
                    op.ReturnType = "IEnumerable<string>";
                    op.Access = (this.Kind == ClassKind.ModelView ? OperationAccess.Public : OperationAccess.Private);
                }
            }
            else
            {
                var op = this.Operations.FirstOrDefault(e => e.Name == valitationMethod);
                if (op != null) op.Delete();
            }
        }

        public string GetInheritanceDefinitions()
        {
            string result = String.Empty;

            if (this.Superclass != null)
            {
                result += (result.IsNullOrEmpty() ? String.Empty : ", ") + this.Superclass.Name;
            }
            else if (this.SuperclassSh != null)
            {
                result += (result.IsNullOrEmpty() ? String.Empty : ", ") + this.SuperclassSh.Name;
            }
            else
            {
                if (this.IsILinx())
                    result += (result.IsNullOrEmpty() ? String.Empty : ", ") + "ILinx";
                if (this.IsIGpecon())
                    result += (result.IsNullOrEmpty() ? String.Empty : ", ") + "IGpecon";

                if (this.IsValidatable)
                    result += (result.IsNullOrEmpty() ? String.Empty : ", ") + "IValidatableObject";
            }

            return (result.IsNullOrEmpty() ? String.Empty : ": ") + result;
        }

        public bool IsILinx()
        {
            return this.EnableIdLinxForInserting && HasIdLinx();
        }

        public bool HasIdLinx(bool checkInheritance = false)
        {
            if (checkInheritance)
                return this.GetAllAttributes().Any(e => e.Name == "ID_LINX");
            else
                return this.Attributes.Any(e => e.Name == "ID_LINX");
        }

        public bool IsIGpecon()
        {
            return this.EnableIdGpeconForInserting && HasIdGpecon();
        }

        public bool HasIdGpecon(bool checkInheritance = false)
        {
            if (checkInheritance)
                return this.GetAllAttributes().Any(e => e.Name == "ID_GPECON");
            else
                return this.Attributes.Any(e => e.Name == "ID_GPECON");
        }

        public bool IsIdFilialPfj()
        {
            return this.EnableIdFilialPfjControl && HasIdFilialPfj();
        }

        public bool HasIdFilialPfj(bool checkInheritance = false)
        {
            if (checkInheritance)
                return this.GetAllAttributes().Any(e => e.Name == "ID_FILIAL_PFJ");
            else
                return this.Attributes.Any(e => e.Name == "ID_FILIAL_PFJ");
        }

        public string GetRelatedIdGpecon(List<BusinessDataModelDesignerRoot> models)
        {
            if (HasIdGpecon(true))
                return "ID_GPECON";
            else
            {
                string navigation = String.Empty;
                foreach (var attribute in this.GetAllAttributes().Where(e => !e.ForeignKey.IsNullOrEmpty() && !e.IsNullable))
                {
                    navigation = this.GetNavigationByForeignKeyPropertyName(models, attribute.Name, true);
                    if (!navigation.IsNullOrEmpty())
                        break;
                }
                return (navigation.IsNullOrEmpty() ? String.Empty : navigation + ".ID_GPECON");
            }
        }

        public string GetRelatedIdFilialPfj(List<BusinessDataModelDesignerRoot> models)
        {
            if (HasIdFilialPfj(true))
                return "ID_FILIAL_PFJ";
            else
            {
                string navigation = String.Empty;
                foreach (var attribute in this.GetAllAttributes().Where(e => !e.ForeignKey.IsNullOrEmpty() && !e.IsNullable))
                {
                    navigation = this.GetNavigationByForeignKeyPropertyName(models, attribute.Name, false, true);
                    if (!navigation.IsNullOrEmpty())
                        break;
                }
                return (navigation.IsNullOrEmpty() ? String.Empty : navigation + ".ID_FILIAL_PFJ");
            }
        }

        public string GetUpdateRelations(List<BusinessDataModelDesignerRoot> models, string indent)
        {
            Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);

            var autoPkeys = this.GetUniqueValues();
            if (autoPkeys.Count() > 0)
            {

                var attr = autoPkeys.First();
                string dataType = attr.GetDataType();

                builder.AddLine("public void UpdateRelations(IEnumerable<System.Data.Entity.Infrastructure.DbEntityEntry> entries, " + dataType + " oldKey, " + dataType + " newKey)");
                builder.AddLine("{");
                builder.IncreaseIndent();

                if (attr.IsPrimaryKey)
                {

                    List<ModelClass> myOwnReferences = new List<ModelClass>();
                    myOwnReferences.Add(this);
                    foreach (var model in models)
                    {
                        myOwnReferences.AddRange(
                           (from cls in model.Types
                            where cls is ModelClass && cls.Name == this.Name && cls != this
                            select (ModelClass)cls));
                    }

                    List<string> addedList = new List<string>();
                    foreach (var myRef in myOwnReferences)
                    {
                        foreach (var link in myRef.GetLinksToTargetModelClasses())
                        {
                            var relatedProps = link.GetTargetAttributes();
                            if (relatedProps.Count() == 1)
                            {
                                var modelClass = link.TargetModelClass;
                                if (!addedList.Contains(modelClass.Name))
                                {
                                    addedList.Add(modelClass.Name);
                                    var targetPropName = relatedProps.First();

                                    builder.AddLine("var added" + modelClass.Name + "List = entries.Where(e => e.State == EntityState.Added && e.Entity is " + modelClass.Name + " && ((" + modelClass.Name + ")e.Entity)." + targetPropName + " == oldKey).Select(e => (" + modelClass.Name + ")e.Entity).ToArray();");
                                    builder.AddLine("for (int idx = 0; idx < added" + modelClass.Name + "List.Length; idx++)");
                                    builder.AddLine("{");
                                    builder.AddLine("    added" + modelClass.Name + "List[idx]." + targetPropName + " = newKey;");
                                    builder.AddLine("}");
                                }
                            }
                        }

                        foreach (var link in myRef.GetLinksToMultipleAssociations())
                        {

                            var relatedProps = link.GetTargetAttributeElements();
                            if (relatedProps.Count() == 1)
                            {
                                var modelClass = link.MultipleAssociation.TargetType;
                                if (!addedList.Contains(modelClass.Name))
                                {
                                    addedList.Add(modelClass.Name);
                                    var targetPropName = relatedProps.First().Name;

                                    builder.AddLine("var added" + modelClass.Name + "List = entries.Where(e => e.State == EntityState.Added && e.Entity is " + modelClass.Name + " && ((" + modelClass.Name + ")e.Entity)." + targetPropName + " == oldKey).Select(e => (" + modelClass.Name + ")e.Entity).ToArray();");
                                    builder.AddLine("for (int idx = 0; idx < added" + modelClass.Name + "List.Length; idx++)");
                                    builder.AddLine("{");
                                    builder.AddLine("    added" + modelClass.Name + "List[idx]." + targetPropName + " = newKey;");
                                    builder.AddLine("}");
                                }
                            }
                        }
                    }

                    builder.DecreaseIndent();
                    builder.AddLine("}");
                }
            }

            return builder.GetBody();
        }

        public bool HasUniqueValues()
        {
            return this.Attributes.Any(e => !e.GetUniqueValue().IsNullOrEmpty());
        }

        public List<ModelAttribute> GetUniqueValues()
        {
            return this.Attributes.Where(e => !e.GetUniqueValue().IsNullOrEmpty()).ToList();
        }

        public bool HasDefaults()
        {
            return this.Attributes.Any(e => !e.GetDefaultValue().IsNullOrEmpty());
        }

        public List<ModelAttribute> GetDefaults()
        {
            return this.Attributes.Where(e => !e.GetDefaultValue().IsNullOrEmpty()).ToList();
        }

        public ModelClass GetTopSuperClass()
        {
            ModelClass baseType = this;

            if (baseType.Superclass != null)
            {
                while (baseType.Superclass != null)
                {
                    baseType = baseType.Superclass;
                }
            }
            else if (baseType.SuperclassSh != null)
            {
                while (baseType.SuperclassSh != null)
                {
                    baseType = baseType.SuperclassSh;
                }
            }

            return baseType;
        }

        public string GetTableNameMap()
        {
            if (this.SuperclassSh != null)
                return this.SuperclassSh.GetTableNameMap();
            else return (this.Table.IsNullOrEmpty() ? this.Name : this.Table);
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

        public void DeleteForeignKeys()
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return;

            foreach (var link in Association.GetLinksToTargetModelClasses(this))
            {
                link.DeletePropertyRelations();
            }

            foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(this))
            {
                link.DeletePropertyRelations();
            }

            foreach (var link in Generalization.GetLinksToSubclasses(this))
            {
                link.DeletePropertyRelations();
            }

            foreach (var link in GeneralizationSh.GetLinksToSubclassesSh(this))
            {
                link.DeletePropertyRelations();
            }
        }

        public void DeleteLink(string refLink)
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return;

            var link = Association.GetLinksToSourceModelClasses(this).Where(e => e.IsKey(refLink)).FirstOrDefault();
            if (link != null)
            {
                link.Delete();
                return;
            }

            var link2 = MultipleAssociationTarget.GetLinkToMultipleAssociation(this);
            if (link2 != null && link2.Id.ToString() == refLink)
            {
                link2.Delete();
                return;
            }

            var link3 = Generalization.GetLinkToSuperclass(this);
            if (link3 != null && link3.Id.ToString() == refLink)
            {
                link3.Delete();
                return;
            }

            var link4 = GeneralizationSh.GetLinkToSuperclassSh(this);
            if (link4 != null && link4.Id.ToString() == refLink)
            {
                link4.Delete();
                return;
            }
        }

        public Dictionary<ModelAttribute, List<ModelElement>> GetRedundantAssociations()
        {
            Dictionary<ModelAttribute, List<ModelElement>> result = new Dictionary<ModelAttribute, List<ModelElement>>();
            foreach (var attr in this.Attributes.Where(e => !e.ForeignKey.IsNullOrEmpty()))
            {
                var links = GetLinks(attr.ForeignKey.Left("."));
                if (links.Count > 1)
                    result[attr] = links;
            }
            return result;
        }


        public List<ModelElement> GetLinks(string refLink)
        {
            List<ModelElement> result = new List<ModelElement>();
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return result;

            result.AddRange(Association.GetLinksToSourceModelClasses(this).Where(e => e.IsKey(refLink)));

            if (this.MultipleAssociation != null)
            {
                result.AddRange(MultipleAssociationOrigin.GetLinksToOriginTypes(this.MultipleAssociation).Where(e => e.IsKey(refLink)));
            }

            return result;
        }

        public List<string> GetAllLinkDefinitions()
        {
            List<string> result = new List<string>();
            try
            {
                if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                    return result;
            }
            catch
            {
                return result;
            }

            foreach (var link in Association.GetLinksToSourceModelClasses(this))
            {
                string reference = "[" + (link.TargetMultiplicity == Multiplicity.ZeroMany || link.TargetMultiplicity == Multiplicity.ZeroOne ? "0..1" : "1..1") + "] " + link.SourceModelClass.Name + " (";
                string joinRelation = String.Empty;
                var lstFks = this.Attributes.Where(e => !e.ForeignKey.IsNullOrEmpty() && link.IsKey(e.ForeignKey.Left("."))).ToArray();
                for (int idx = 0; idx < lstFks.Length; idx++)
                {
                    reference += lstFks[idx].Name + (idx < lstFks.Length - 1 ? ", " : "");
                    joinRelation += lstFks[idx].Name + "=" + lstFks[idx].ForeignKey.Right(".") + (idx < lstFks.Length - 1 ? "," : "");
                }
                reference += ")#" + joinRelation;
                result.Add(reference);
            }

            if (this.MultipleAssociation != null)
            {
                foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(this.MultipleAssociation))
                {
                    string reference = "[" + (link.Multiplicity == Multiplicity.ZeroMany || link.Multiplicity == Multiplicity.ZeroOne ? "0..1" : "1..1") + "] " + link.OriginType.Name + " (";
                    string joinRelation = String.Empty;
                    var lstFks = this.Attributes.Where(e => !e.ForeignKey.IsNullOrEmpty() && link.IsKey(e.ForeignKey.Left("."))).ToArray();
                    for (int idx = 0; idx < lstFks.Length; idx++)
                    {
                        reference += lstFks[idx].Name + (idx < lstFks.Length - 1 ? ", " : "");
                        joinRelation += lstFks[idx].Name + "=" + lstFks[idx].ForeignKey.Right(".") + (idx < lstFks.Length - 1 ? "," : "");
                    }
                    reference += ")#" + joinRelation;
                    result.Add(reference);
                }
            }

            foreach (var link in Association.GetLinksToTargetModelClasses(this))
            {
                string reference = "[" + (link.TargetMultiplicity == Multiplicity.ZeroMany || link.TargetMultiplicity == Multiplicity.ZeroOne ? "0" : "1") + ".." + (link.TargetMultiplicity == Multiplicity.ZeroMany || link.TargetMultiplicity == Multiplicity.Many ? "*" : "1") + "] " + link.TargetModelClass.Name + " (";
                string joinRelation = String.Empty;
                var lstFks = link.TargetModelClass.Attributes.Where(e => !e.ForeignKey.IsNullOrEmpty() && link.IsKey(e.ForeignKey.Left("."))).ToArray();
                for (int idx = 0; idx < lstFks.Length; idx++)
                {
                    reference += lstFks[idx].ForeignKey.Right(".") + (idx < lstFks.Length - 1 ? ", " : "");
                    joinRelation += lstFks[idx].ForeignKey.Right(".") + "=" + lstFks[idx].Name + (idx < lstFks.Length - 1 ? "," : "");
                }
                reference += ")#" + joinRelation;
                result.Add(reference);
            }


            if (this.MultipleAssociations.Count > 0)
            {
                foreach (var ma in this.MultipleAssociations)
                {
                    foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(ma))
                    {
                        string reference = "[" + (link.Multiplicity == Multiplicity.ZeroMany || link.Multiplicity == Multiplicity.ZeroOne ? "0" : "1") + ".." + (link.Multiplicity == Multiplicity.ZeroMany || link.Multiplicity == Multiplicity.Many ? "*" : "1") + "] " + ma.TargetType.Name + " (";
                        string joinRelation = String.Empty;
                        var lstFks = ma.TargetType.Attributes.Where(e => !e.ForeignKey.IsNullOrEmpty() && link.IsKey(e.ForeignKey.Left("."))).ToArray();
                        for (int idx = 0; idx < lstFks.Length; idx++)
                        {
                            reference += lstFks[idx].ForeignKey.Right(".") + (idx < lstFks.Length - 1 ? ", " : "");
                            joinRelation += lstFks[idx].ForeignKey.Right(".") + "=" + lstFks[idx].Name + (idx < lstFks.Length - 1 ? "," : "");
                        }
                        reference += ")#" + joinRelation;
                        result.Add(reference);
                    }
                }
            }

            return result;
        }

        public string GetPrimaryKeyName()
        {
            string pkName = String.Empty;
            var modelClass = this.GetTopSuperClass();
            if (modelClass != null)
            {
                var property = modelClass.Attributes.FirstOrDefault(e => e.IsPrimaryKey && !e.IsNullable);
                if (property != null)
                    pkName = property.Name;
            }

            return pkName;
        }


        public List<ModelAttribute> GetPrimaryKeys()
        {
            List<ModelAttribute> result = new List<ModelAttribute>();
            var modelClass = this.GetTopSuperClass();
            if (modelClass != null)
            {
                result.AddRange(modelClass.Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable));
            }
            return result;
        }

        public Generalization GetLinkToSuperclass()
        {
            Generalization result = null;

            try
            {
                result = Generalization.GetLinkToSuperclass(this);
            }
            catch { }

            return result;
        }

        public GeneralizationSh GetLinkToSuperclassSh()
        {
            GeneralizationSh result = null;

            try
            {
                result = GeneralizationSh.GetLinkToSuperclassSh(this);
            }
            catch { }

            return result;
        }

        public List<Association> GetLinksToTargetModelClasses()
        {
            List<Association> result = new List<Association>();

            try
            {
                result = Association.GetLinksToTargetModelClasses(this).ToList();
            }
            catch { }

            return result;
        }

        public List<Association> GetLinksToSourceModelClasses()
        {
            List<Association> result = new List<Association>();

            try
            {
                result = Association.GetLinksToSourceModelClasses(this).ToList();
            }
            catch { }

            return result;
        }

        public MultipleAssociationTarget GetLinkToMultipleAssociation()
        {
            MultipleAssociationTarget result = null;

            try
            {
                result = MultipleAssociationTarget.GetLinkToMultipleAssociation(this);
            }
            catch { }

            return result;
        }

        public List<MultipleAssociationOrigin> GetLinksToMultipleAssociations()
        {
            List<MultipleAssociationOrigin> result = new List<MultipleAssociationOrigin>();

            try
            {
                result = MultipleAssociationOrigin.GetLinksToMultipleAssociations(this).ToList();
            }
            catch { }

            return result;
        }

        public void UpdateName(string oldName)
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (!(this is ReferenceModelClass) && oldName != this.Name)
            {
                var attr = this.Attributes.Where(e => e.IsPrimaryKey && e.Name == "ID_" + oldName).FirstOrDefault();
                if (attr != null)
                {
                    attr.Name = "ID_" + this.Name;
                    if (attr.DisplayName == ("ID_" + oldName).Replace("_", " ").Proper())
                        attr.DisplayName = ("ID_" + this.Name).Replace("_", " ").Proper();
                }
            }
        }

        public void AddPrimaryKey()
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return;

            if (this.Kind != ClassKind.ModelView && !(this is ReferenceModelClass) && this.Superclass == null && this.SuperclassSh == null && !this.Attributes.Any(e => e.IsPrimaryKey))
            {
                ModelAttribute attr = this.Attributes.AddNew() as ModelAttribute;
                if (attr != null)
                {
                    attr.Name = "ID_" + this.Name;
                    attr.DisplayName = attr.Name.Replace("_", " ").Proper();
                    attr.DataType = ModelDataType.Int;
                    attr.IsPrimaryKey = true;
                    attr.IsIdentity = true;
                    attr.IsNullable = false;
                    this.Attributes.Move(attr, 0);
                }
            }
        }

        public string GetModifiers()
        {
            string modif = String.Empty;

            if (this.Modifier != InheritanceModifier.None)
            {
                modif += " " + this.Modifier.ToString().ToLower();
            }

            return modif;
        }

        public string GetDiscriminators(string indent)
        {
            CodeBuilder builder = new CodeBuilder(indent);

            var links = GeneralizationSh.GetLinksToSubclassesSh(this);
            if (links.Count > 0)
            {
                builder.AddLine("");
                builder.AddLine("modelBuilder.Entity<" + this.Name + ">()");
                builder.IncreaseIndent();
                foreach (var link in links)
                {
                    builder.AddLine(".Map<" + link.SubclassSh.Name + ">(m => m.Requires(\"" + link.Discriminator.Left("=") + "\").HasValue(" + link.Discriminator.Right("=") + "))");
                }
                builder.AddLine(";");
                builder.AddLine("");
            }

            return builder.GetBody();
        }

        public string GetDiscriminator(string indent)
        {
            CodeBuilder builder = new CodeBuilder(indent);

            if (this.SuperclassSh != null)
            {
                var link = GeneralizationSh.GetLinkToSuperclassSh(this);
                if (link != null && !link.Discriminator.IsNullOrEmpty())
                {
                    builder.Add("Map(m => m.Requires(\"" + link.Discriminator.Left("=") + "\").HasValue(" + link.Discriminator.Right("=") + "));");
                }
            }

            return builder.GetBody();
        }

        public string GetNotMappedAttributes(string indent)
        {
            if (this.NotMapped)
                return String.Empty;

            CodeBuilder builder = new CodeBuilder(indent);

            foreach (var attr in this.Attributes.Where(e => !e.InStudy && e.IsNotMapped()))
            {
                builder.AddLine("Ignore(i => i." + attr.Name + ");");
            }

            return builder.GetBody();
        }

        #region Associations and compositions properties

        public string GetForeignKeyProperties(List<BusinessDataModelDesignerRoot> models, CodeBuilder builder)
        {
            if (builder == null)
                builder = new CodeBuilder();

            List<string> uniques = new List<string>();
            //Get my own references
            List<ModelClass> myOwnReferences = new List<ModelClass>();
            myOwnReferences.Add(this);
            foreach (var model in models)
            {
                myOwnReferences.AddRange(
                   (from cls in model.Types
                    where cls is ModelClass && cls.Name == this.Name && cls != this
                    select (ModelClass)cls));
            }

            //Associations
            List<Association> sourceLinks = new List<Association>(); ;
            foreach (var myRef in myOwnReferences)
            {
                foreach (var link in Association.GetLinksToSourceModelClasses(myRef))
                {
                    if (!sourceLinks.Any(e => e.SourceModelClass.Name == link.SourceModelClass.Name && e.GetTargetProperties() == link.GetTargetProperties()))
                        sourceLinks.Add(link);
                }

            }
            if (sourceLinks.Count() > 0)
            {
                foreach (Association link in sourceLinks)
                {
                    //Name and conflict detection
                    string propName = (link.TargetPropertyNameToSource.IsNullOrEmpty() ? link.SourceModelClass.Name : link.TargetPropertyNameToSource);
                    if (link.TargetPropertyNameToSource.IsNullOrEmpty())
                    {
                        var sourcePropLinks = sourceLinks.Where(e => e.TargetPropertyNameToSource.IsNullOrEmpty() && e.SourceModelClass.Name == propName).ToList();
                        if (propName == link.TargetModelClass.Name || (sourcePropLinks.Count > 1 && sourcePropLinks.IndexOf(link) > 0))
                            propName += (sourcePropLinks.IndexOf(link) + (propName == link.TargetModelClass.Name ? 1 : 0)).ToString();
                    }

                    if (!uniques.Contains(propName) && !link.SourceModelClass.InStudy)
                    {

                        string fkeys = String.Join(",", link.GetTargetColumns());
                        builder.AddLine("db." + this.Name + ".belongsTo(db." + link.SourceModelClass.Name + ", { as: '" + propName + "', foreignKey: '" + fkeys + "', onDelete: '" + (link.WillCascadeOnDelete ? "CASCADE" : "NO ACTION") + "' });");

                        uniques.Add(propName);
                    }
                }
            }


            //MultipleAssociations
            List<MultipleAssociation> multAssociations = new List<BusinessDataModelDesigner.MultipleAssociation>();
            foreach (var myRef in myOwnReferences)
            {
                if (myRef.MultipleAssociation != null)
                    multAssociations.Add(myRef.MultipleAssociation);
            }
            foreach (var multiple in multAssociations)
            {
                foreach (MultipleAssociationOrigin link in MultipleAssociationOrigin.GetLinksToOriginTypes(multiple))
                {
                    if (!uniques.Contains(link.OriginType.Name) && !link.OriginType.InStudy)
                    {

                        string fkeys = String.Join(",", link.GetTargetColumns());
                        builder.AddLine("db." + this.Name + ".belongsTo(db." + link.OriginType.Name + ", { as: '" + link.OriginType.Name + "', foreignKey: '" + fkeys + "', onDelete: '" + (link.WillCascadeOnDelete ? "CASCADE" : "NO ACTION") + "' });");

                        uniques.Add(link.OriginType.Name);
                    }
                }
            }

            return builder.GetBody();
        }


        public List<ModelAttribute> GetAllAttributes()
        {
            List<ModelAttribute> result = new List<ModelAttribute>();

            var entity = this;
            while (entity != null)
            {
                result.AddRange(entity.Attributes);
                if (entity.Superclass != null)
                    entity = entity.Superclass;
                else if (entity.SuperclassSh != null)
                    entity = entity.SuperclassSh;
                else
                    entity = null;
            }

            return result;
        }

        public List<ModelIndex> GetInconsistentIndexes()
        {
            List<ModelIndex> result = new List<ModelIndex>();
            var attributes = this.GetAllAttributes();

            foreach (var idx in this.ModelIndexes)
            {
                if (idx.Properties.IsNullOrEmpty())
                    result.Add(idx);
                else
                {
                    foreach (string colName in (idx.Properties + ",").ToUpper().Replace(" DESC,", ",").Replace(" ASC,", ",").Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!attributes.Any(attr => (attr.ColumnName.IsNullOrEmpty() ? attr.Name : attr.ColumnName).ToUpper() == colName))
                            result.Add(idx);
                    }
                }
            }

            return result;
        }

        public string GetNavigationByForeignKeyPropertyName(List<BusinessDataModelDesignerRoot> models, string propertyName, bool onlyIfIsGpecon = false, bool onlyIfIsFilial = false)
        {
            //Get my own references
            List<ModelClass> myOwnReferences = new List<ModelClass>();
            myOwnReferences.Add(this);
            foreach (var model in models)
            {
                myOwnReferences.AddRange(
                   (from cls in model.Types
                    where cls is ModelClass && cls.Name == this.Name && cls != this
                    select (ModelClass)cls));
            }

            //Associations
            List<Association> sourceLinks = new List<Association>(); ;
            foreach (var myRef in myOwnReferences)
            {
                foreach (var link in Association.GetLinksToSourceModelClasses(myRef))
                {
                    var existentLink = sourceLinks.FirstOrDefault(e => e.SourceModelClass.Name == link.SourceModelClass.Name && e.GetTargetProperties() == link.GetTargetProperties());
                    if (existentLink == null)
                        sourceLinks.Add(link);
                    else if (((link.SourcePropertyNameToTarget.IsNullOrEmpty() ? 0 : 1) + (link.TargetPropertyNameToSource.IsNullOrEmpty() ? 0 : 1)) > ((existentLink.SourcePropertyNameToTarget.IsNullOrEmpty() ? 0 : 1) + (existentLink.TargetPropertyNameToSource.IsNullOrEmpty() ? 0 : 1)))
                    {
                        sourceLinks.Remove(existentLink);
                        sourceLinks.Add(link);
                    }
                }
            }
            if (sourceLinks.Count() > 0)
            {
                foreach (Association link in sourceLinks)
                {
                    var fkProperty = link.GetTargetAttributeElements().FirstOrDefault(e => e.Name == propertyName);
                    if (fkProperty != null)
                    {
                        //Name and conflict detection
                        string propName = (link.TargetPropertyNameToSource.IsNullOrEmpty() ? link.SourceModelClass.Name : link.TargetPropertyNameToSource);
                        if (link.TargetPropertyNameToSource.IsNullOrEmpty())
                        {
                            var sourcePropLinks = sourceLinks.Where(e => e.TargetPropertyNameToSource.IsNullOrEmpty() && e.SourceModelClass.Name == propName).ToList();
                            if (propName == link.TargetModelClass.Name || (sourcePropLinks.Count > 1 && sourcePropLinks.IndexOf(link) > 0))
                                propName += (sourcePropLinks.IndexOf(link) + (propName == link.TargetModelClass.Name ? 1 : 0)).ToString();
                        }

                        if (onlyIfIsGpecon)
                        {
                            if (link.SourceModelClass.HasIdGpecon(true))
                                return propName;
                            else
                                return String.Empty;
                        }
                        else if (onlyIfIsFilial)
                        {
                            if (link.SourceModelClass.HasIdFilialPfj(true))
                                return propName;
                            else
                                return String.Empty;
                        }
                        else
                            return propName;
                    }
                }
            }


            //MultipleAssociations
            List<MultipleAssociation> multAssociations = new List<BusinessDataModelDesigner.MultipleAssociation>();
            foreach (var myRef in myOwnReferences)
            {
                if (myRef.MultipleAssociation != null)
                    multAssociations.Add(myRef.MultipleAssociation);
            }
            foreach (var multiple in multAssociations)
            {
                var fkProperty = multiple.TargetType.Attributes.FirstOrDefault(e => e.Name == propertyName);
                if (fkProperty != null)
                {

                    foreach (MultipleAssociationOrigin link in MultipleAssociationOrigin.GetLinksToOriginTypes(multiple).Where(e => e.IsFkAttrinute(fkProperty)))
                    {
                        if (onlyIfIsGpecon)
                        {
                            if (link.OriginType.HasIdGpecon(true))
                                return link.OriginType.Name;
                            else
                                return String.Empty;
                        }
                        else if (onlyIfIsFilial)
                        {
                            if (link.OriginType.HasIdFilialPfj(true))
                                return link.OriginType.Name;
                            else
                                return String.Empty;
                        }
                        else
                            return link.OriginType.Name;
                    }
                }
            }

            return String.Empty;
        }


        public string GetForeignKeyCollecions(List<BusinessDataModelDesignerRoot> models, CodeBuilder builder)
        {
            List<string> uniques = new List<string>();
            string propNameOnTarget;
            if (builder == null)
                builder = new CodeBuilder();

            //Get my own references
            List<ModelClass> myOwnReferences = new List<ModelClass>();
            myOwnReferences.Add(this);
            foreach (var model in models)
            {
                myOwnReferences.AddRange(
                   (from cls in model.Types
                    where cls is ModelClass && cls.Name == this.Name && cls != this
                    select (ModelClass)cls));
            }

            //Associations
            List<Association> targetLinks = new List<Association>();
            foreach (var myRef in myOwnReferences)
            {
                foreach (var link in Association.GetLinksToTargetModelClasses(myRef))
                {
                    var existentLink = targetLinks.FirstOrDefault(e => e.TargetModelClass.Name == link.TargetModelClass.Name && e.GetTargetProperties() == link.GetTargetProperties());
                    if (existentLink == null)
                        targetLinks.Add(link);
                    else if (((link.SourcePropertyNameToTarget.IsNullOrEmpty() ? 0 : 1) + (link.TargetPropertyNameToSource.IsNullOrEmpty() ? 0 : 1)) > ((existentLink.SourcePropertyNameToTarget.IsNullOrEmpty() ? 0 : 1) + (existentLink.TargetPropertyNameToSource.IsNullOrEmpty() ? 0 : 1)))
                    {
                        targetLinks.Remove(existentLink);
                        targetLinks.Add(link);
                    }
                }
            }
            if (targetLinks.Count() > 0)
            {
                foreach (Association link in targetLinks.OrderBy(e => e.TargetModelClass.Name + "_" + (e.SourcePropertyNameToTarget.IsNullOrEmpty() ? e.TargetModelClass.Name : e.SourcePropertyNameToTarget)))
                {
                    //Name and conflict detection on source
                    string propNameOnSource = (link.SourcePropertyNameToTarget.IsNullOrEmpty() ? link.TargetModelClass.Name : link.SourcePropertyNameToTarget);
                    if (link.SourcePropertyNameToTarget.IsNullOrEmpty())
                    {
                        string suffix = "_LISTA";
                        var targetPropLinks = targetLinks.Where(e => e.SourcePropertyNameToTarget.IsNullOrEmpty() && e.TargetModelClass.Name == propNameOnSource).ToList();
                        if (targetPropLinks.Count > 1 && targetPropLinks.IndexOf(link) > 0)
                            suffix += targetPropLinks.IndexOf(link).ToString();

                        propNameOnSource += suffix;
                    }
                    if (!uniques.Contains(propNameOnSource) && !link.TargetModelClass.InStudy)
                    {

                        string fkeys = String.Join(",", link.GetTargetColumns());
                        builder.AddLine("//Origin Document: " + link.TargetModelClass.BusinessDataModelDesignerRoot.DocumentName);
                        builder.AddLine("db." + this.Name + "." + (link.TargetMultiplicity == Multiplicity.Many || link.TargetMultiplicity == Multiplicity.ZeroMany ? "hasMany" : "hasOne") + "(db." + link.TargetModelClass.Name + ", { as: '" + propNameOnSource + "', foreignKey: '" + fkeys + "', onDelete: '" + (link.WillCascadeOnDelete ? "CASCADE" : "NO ACTION") + "' });");

                        uniques.Add(propNameOnSource);
                    }
                }
            }

            //MultipleAssociations
            foreach (var myRef in myOwnReferences)
            {
                foreach (var mult in myRef.MultipleAssociations)
                {
                    MultipleAssociationTarget link = MultipleAssociationTarget.GetLinkToTargetType(mult);
                    if (link != null)
                    {
                        var originLink = MultipleAssociationOrigin.GetLink(mult, myRef);
                        if (originLink != null)
                        {
                            string propertyName = (originLink == null || originLink.CollectionName.IsNullOrEmpty() ? link.TargetType.Name + "_LISTA" : originLink.CollectionName);
                            if (!uniques.Contains(propertyName) && !link.TargetType.InStudy)
                            {

                                string fkeys = String.Join(",", originLink.GetTargetColumns());
                                builder.AddLine("//Origin Document: " + link.TargetType.BusinessDataModelDesignerRoot.DocumentName);
                                builder.AddLine("db." + this.Name + ".hasMany(db." + link.TargetType.Name + ", { as: '" + propertyName + "', foreignKey: '" + fkeys + "', onDelete: '" + (originLink.WillCascadeOnDelete ? "CASCADE" : "NO ACTION") + "' });");

                                uniques.Add(propertyName);
                            }
                        }
                    }
                }
            }

            return builder.GetBody();
        }


        public string GetModelViewCollecions(List<BusinessDataModelDesignerRoot> models, string indent)
        {
            List<string> uniques = new List<string>();
            CodeBuilder builder = new CodeBuilder(indent);

            //Get my own references
            List<ModelClass> myOwnReferences = new List<ModelClass>();
            myOwnReferences.Add(this);
            foreach (var model in models)
            {
                myOwnReferences.AddRange(
                   (from cls in model.Types
                    where cls is ModelClass && cls.Name == this.Name && cls != this
                    select (ModelClass)cls));
            }

            //Associations
            List<ModelViewAssociation> targetLinks = new List<ModelViewAssociation>();
            foreach (var myRef in myOwnReferences)
            {
                foreach (var link in ModelViewAssociation.GetLinksToModelViews(myRef))
                {
                    targetLinks.Add(link);
                }
            }
            if (targetLinks.Count() > 0)
            {
                foreach (ModelViewAssociation link in targetLinks.OrderBy(e => e.TargetModelClass.Name + "_" + (e.CollectionName.IsNullOrEmpty() ? e.TargetModelClass.Name : e.CollectionName)))
                {
                    //Name and conflict detection on source
                    string propNameOnSource = (link.CollectionName.IsNullOrEmpty() ? link.TargetModelClass.Name : link.CollectionName);
                    if (link.CollectionName.IsNullOrEmpty())
                    {
                        string suffix = "_LISTA";
                        var targetPropLinks = targetLinks.Where(e => e.CollectionName.IsNullOrEmpty() && e.TargetModelClass.Name == propNameOnSource).ToList();
                        if (targetPropLinks.Count > 1 && targetPropLinks.IndexOf(link) > 0)
                            suffix += targetPropLinks.IndexOf(link).ToString();

                        propNameOnSource += suffix;
                    }
                    if (!uniques.Contains(propNameOnSource) && !link.TargetModelClass.InStudy)
                    {
                        builder.AddLine();
                        builder.AddLine("//Origin Document: " + link.TargetModelClass.BusinessDataModelDesignerRoot.DocumentName);
                        builder.AddLine("public virtual ICollection<" + link.TargetModelClass.Name + "> " + propNameOnSource + " { get; set; }");
                        uniques.Add(propNameOnSource);
                    }
                }
            }

            return builder.GetBody();
        }

        #endregion



        public void GetBusinessViewWrapperLinq(EntityQueryNode topEntityQuery, CodeBuilder builder, string aliasWrapper)
        {
            List<string> selectProperties = new List<string>();
            List<string> groupByProperties = new List<string>();
            string groupBy = "", outerWhere = "";
            List<string> outerExclWheres = new List<string>();

            if (!this.Filter.IsNullOrEmpty())
                outerWhere = "(" + this.Filter.Replace("this.", "") + ")";

            Action<EntityQueryNode> generateWrapperLinq = null;
            generateWrapperLinq = (eq) =>
            {
                if (eq.RelationType == QueryNodeType.Entity)
                {
                    foreach (var prop in eq.Properties.Where(e => e.Selected).OrderBy(e => e.Name))
                    {
                        var entityAttribute = this.Attributes.FirstOrDefault(e => e.Name == prop.Name);

                        if (entityAttribute == null)
                            continue;

                        if (this.ModelViewAggregation)
                        {
                            if (entityAttribute.AggregationFunction == AggregationFunctions.None)
                            {
                                groupByProperties.Add(aliasWrapper + "GRP" + aliasWrapper + "." + aliasWrapper + prop.Name + aliasWrapper);
                                selectProperties.Add(aliasWrapper + "GRP" + aliasWrapper + "." + aliasWrapper + prop.Name + aliasWrapper);
                            }
                            else
                            {
                                if (entityAttribute.AggregationFunction == AggregationFunctions.Count)
                                    selectProperties.Add("COUNT(1) AS " + aliasWrapper + prop.Name + aliasWrapper);
                                //else if (entityAttribute.AggregationFunction == AggregationFunctions.CountDistinct)
                                //    selectProperties.Add(prop.Name + " = rg0" + (entityAttribute.ModelViewCountDistinctFilter.IsNullOrEmpty() ? "" : ".Where(e => " + entityAttribute.ModelViewCountDistinctFilter.Replace("this.", "e.q.").Replace("[Value]", "e.q." + entityAttribute.Name) + ")") + ".Select(e => e.q." + entityAttribute.Name + ").Distinct().Count()");
                                else
                                    selectProperties.Add(entityAttribute.GetAggregationFunction() + "(" + aliasWrapper + "GRP" + aliasWrapper + "." + aliasWrapper + prop.Name + aliasWrapper + ") AS " + aliasWrapper + prop.Name + aliasWrapper);
                            }
                        }

                        if (!entityAttribute.Filter.IsNullOrEmpty())
                        {
                            outerWhere += (outerWhere.IsNullOrEmpty() ? "" : " AND ") + "(" + (entityAttribute.Filter.Replace("this.", "").Replace("[Value]", aliasWrapper + entityAttribute.Name + aliasWrapper)) + ")";
                        }
                    }
                }

                eq.Joins.ForEach(e => generateWrapperLinq(e));

            };
            generateWrapperLinq(topEntityQuery);

            if (this.ModelViewAggregation)
            {
                builder.AddLine(";");

                //Select properties
                builder.AddLine("var queryAggr = 'SELECT '");
                for (int idx = 0; idx < selectProperties.Count; idx++)
                {
                    builder.AddLine("+ '" + selectProperties[idx] + (idx == selectProperties.Count - 1 ? "" : ",") + " '");
                }

                builder.AddLine("+ 'FROM (' + query + ') AS " + aliasWrapper + "GRP" + aliasWrapper + " '");

                groupBy += "GROUP BY ";

                for (int idx = 0; idx < groupByProperties.Count; idx++)
                {
                    groupBy += groupByProperties[idx] + (idx == groupByProperties.Count - 1 ? "" : ", ");
                }

                builder.AddLine("+ '" + groupBy + " '");
            }

            builder.AddLine((outerWhere.IsNullOrEmpty() ? "" : "+ '" + outerWhere + "'"));

            //Add exclusive filters
            foreach (var exclFilter in outerExclWheres)
            {
                builder.AddLine("+ '" + exclFilter + "'");
            }

            builder.AddLine(";");
        }

        private string GetModelViewOrderBy(string alias, string aliasWrapper)
        {
            string order = "";

            foreach (var prop in this.Attributes.Where(e => e.ModelViewOrderBySequence >= 0).OrderBy(o => o.ModelViewOrderBySequence))
            {
                order += (order.IsNullOrEmpty() ? String.Empty : ", ") + aliasWrapper + alias + aliasWrapper + "." + aliasWrapper + prop.Name + aliasWrapper + " " + prop.ModelViewOrderByOrientation.ToString().ToLower();
            }

            return order;
        }

        public string GetCodePreQuery(string indent)
        {
            if (!this.ModelViewCodePreQuery.IsNullOrEmpty())
            {
                CodeBuilder builder = new CodeBuilder(indent);

                builder.AddLine("//Pre initialization code");
                foreach (var comamnd in this.ModelViewCodePreQuery.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    builder.AddLine(comamnd.Replace("\n", "").Replace("\r", ""));
                }

                return builder.GetBody();
            }
            else
                return "";
        }

        public string GetAlteranativeKeyFilter(string indent)
        {
            CodeBuilder builder = new CodeBuilder(indent);

            var distKeys = this.Attributes.Where(e => !e.ModelViewFormula.IsNullOrEmpty() && e.ModelViewFormula.StartsWith("KEY(")).ToArray();
            if (distKeys.Length > 0)
            {
                builder.AddLine("//Adjust Alteranative Keys");
                builder.AddLine("string paramKeyName;");
                foreach (var distKey in distKeys)
                {
                    var properties = distKey.ModelViewFormula.Extract("KEY(", ")").Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (properties.Length > 0)
                    {
                        builder.AddLine("//Adjust Alteranative Key: " + distKey.Name);
                        builder.AddLine("paramKeyName = predicate.Extract(\"it." + distKey.Name + " = @\", \" \");");
                        builder.AddLine("if (paramKeyName.IsNullOrEmpty())");
                        builder.AddLine("    paramKeyName = predicate.Extract(\"it." + distKey.Name + " = @\", \")\");");
                        builder.AddLine("");
                        builder.AddLine("if (!paramKeyName.IsNullOrEmpty())");
                        builder.AddLine("{");
                        builder.AddLine("    var param = parameters.FirstOrDefault(e => e.Name == paramKeyName);");
                        builder.AddLine("    if (param != null)");
                        builder.AddLine("    {");
                        builder.AddLine("        var newParameters = parameters.ToList();");
                        builder.AddLine("        string keyFilter = \"\";");
                        builder.AddLine("        string pValue = param.Value.ToString();");
                        builder.AddLine("        var valueParts = pValue.Split(new string[] { \"||\" }, StringSplitOptions.RemoveEmptyEntries);");
                        builder.AddLine("        if (valueParts.Length == " + properties.Length.ToString() + ")");
                        builder.AddLine("        {");
                        builder.AddLine("            //Creating new parameters");

                        for (int idx = 0; idx < properties.Length; idx++)
                        {
                            var prop = this.Attributes.FirstOrDefault(e => e.Name == properties[idx]);
                            if (prop != null)
                            {
                                string dataType = prop.GetDataType();
                                string valueName = "value" + idx.ToString();
                                builder.AddLine("            " + dataType + " " + valueName + (dataType.ToLower().Contains("string") ? " = valueParts[" + idx.ToString() + "].ToString()" : "") + ";");
                                if (dataType.ToLower().Contains("string"))
                                    builder.AddLine("            if (!" + valueName + ".IsNullOrEmpty())");
                                else
                                    builder.AddLine("            if (" + prop.GetDataType() + ".TryParse(valueParts[" + idx.ToString() + "], out " + valueName + "))");
                                builder.AddLine("            {");
                                builder.AddLine("                string keyProp = paramKeyName.Left(paramKeyName.Length - 1) + \"" + idx.ToString() + "\";");
                                builder.AddLine("                newParameters.Add(new System.Data.Entity.Core.Objects.ObjectParameter(keyProp, " + valueName + "));");
                                builder.AddLine("                keyFilter += (keyFilter.IsNullOrEmpty() ? \"\" : \" && \") + \"it." + prop.Name + " == @\" + keyProp;");
                                builder.AddLine("            }");
                            }
                            else
                                return "";
                        }

                        builder.AddLine("        }");
                        builder.AddLine("        newParameters.Remove(param);");
                        builder.AddLine("        parameters = newParameters.ToArray();");
                        builder.AddLine("        predicate = predicate.Replace(\"it." + distKey.Name + " = @\" + paramKeyName, keyFilter);");
                        builder.AddLine("    }");
                        builder.AddLine("}");
                    }
                }
            }

            return builder.GetBody();
        }

        public string GetBusinessViewLinqDefinition(string indent, BusinessDataModelDesignerRoot rootDesigner, string aliasWrapper)
        {
            CodeBuilder builder = new CodeBuilder(indent);
            builder.IncreaseIndent();
            var topEntityQueries = this.GetBusinessViewRootObjects();

            if (topEntityQueries != null && topEntityQueries.Count > 0)
            {
                for (int idx = 0; idx < topEntityQueries.Count; idx++)
                {
                    GetBusinessViewLinqDefinition(indent, builder, topEntityQueries[idx], (idx > 0), rootDesigner, aliasWrapper);
                }

                GetBusinessViewWrapperLinq(topEntityQueries[0], builder, aliasWrapper);
            }

            string varQueryResult = "query" + (this.ModelViewAggregation ? "Aggr" : "");
            string order = this.GetModelViewOrderBy("Ord", aliasWrapper);
            if (!order.IsNullOrEmpty())
            {
                builder.AddLine(varQueryResult + " = 'SELECT * FROM (' + " + varQueryResult + " + ') AS Ord  ORDER BY " + order + "';");
            }

            if (this.ModelViewTop > 0)
            {
                builder.AddLine(varQueryResult + " = 'SELECT TOP " + this.ModelViewTop.ToString() + " * FROM (' + " + varQueryResult + " + ') AS Tk';");
            }

            builder.AddLine("return '(' + " + varQueryResult + " + ')';");

            return builder.GetBody();
        }

        public string GetFormulaDefinition(EntityQueryNode eq, EntityQueryProperty prop, string aliasWrapper)
        {
            string formula = "";

            if (!prop.Formula.IsNullOrEmpty() & prop.Formula.StartsWith("KEY("))
            {
                var properties = prop.Formula.Extract("KEY(", ")").Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (properties.Length > 0)
                {
                    foreach (var propName in properties)
                    {
                        var eProp = this.Attributes.FirstOrDefault(e => e.Name == propName);
                        if (eProp != null)
                        {
                            formula += (formula.IsNullOrEmpty() ? "" : " + '||' + ") + aliasWrapper + eq.Alias + aliasWrapper + "." + aliasWrapper + eProp.ModelViewSource.Left("(").Right(".") + aliasWrapper;
                        }
                    }
                }
            }
            else
            {
                formula = eq.ReplaceExpression(prop.Formula, aliasWrapper);
            }

            return formula;
        }

        private string GetComposedKeyDefinition(ModelAttribute prop)
        {
            string composedKey = "";

            if (!prop.ModelViewFormula.IsNullOrEmpty() & prop.ModelViewFormula.StartsWith("KEY("))
            {
                var properties = prop.ModelViewFormula.Extract("KEY(", ")").Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (properties.Length > 0)
                {
                    foreach (var propName in properties)
                    {
                        var eProp = this.Attributes.FirstOrDefault(e => e.Name == propName);
                        if (eProp != null)
                        {
                            composedKey += (composedKey.IsNullOrEmpty() ? "" : " + \"||\" + ") + "this." + eProp.Name + (eProp.GetDataType().ToLower().Contains("string") ? "" : ".ToString()");
                        }
                    }
                }
            }

            return composedKey;
        }

        public string GetRefreshComposedKeys(string indent)
        {
            CodeBuilder builder = new CodeBuilder(indent);
            if (this.Kind == ClassKind.ModelView)
            {
                foreach (var attr in this.GetAllAttributes().Where(prop => !prop.ModelViewFormula.IsNullOrEmpty() & prop.ModelViewFormula.StartsWith("KEY(")).ToArray())
                {
                    var composedKey = GetComposedKeyDefinition(attr);
                    if (!composedKey.IsNullOrEmpty())
                    {
                        builder.AddLine(attr.Name + " = " + composedKey + ";");
                    }
                }
            }
            return builder.GetBody();
        }

        public bool HasFilteringDisabled()
        {
            return this.Attributes.Any(e => e.FilteringDisabled);
        }

        public string GetFilteringDisabledList()
        {
            string disabledList = "";

            foreach (var attr in this.Attributes.Where(e => e.FilteringDisabled))
            {
                disabledList += (disabledList.IsNullOrEmpty() ? "" : ",") + " \"" + this.Name + "." + attr.Name + "\"";
            }

            return "new string[] {" + disabledList + " }";
        }

        public void GetBusinessViewLinqDefinition(string indent, CodeBuilder mainBuilder, EntityQueryNode topEntityQuery, bool genUnion, BusinessDataModelDesignerRoot rootDesigner, string aliasWrapper)
        {
            if (topEntityQuery != null)
            {
                List<string> preCommands = new List<string>();
                var defaultProvider = rootDesigner.GetDefaultProvider();
                bool removeSchema = defaultProvider.In(Provider.MySQL, Provider.SQLite);
                int aliasCount = 0;
                string where = "";
                List<string> selectProperties = new List<string>();
                Action<EntityQueryNode, EntityQueryNode> restoreLayout = null;
                restoreLayout = (eq, parent) =>
                {
                    eq.Parent = parent;

                    switch (eq.RelationType)
                    {
                        case QueryNodeType.Entity:
                            if (eq.Alias.IsNullOrEmpty())
                            {
                                eq.Alias = "entity" + aliasCount.ToString();
                                aliasCount++;
                            }
                            //Adjust selected properties
                            if (!genUnion)
                                eq.SyncPropertiesWithView(this, eq == topEntityQuery, eq == topEntityQuery, true);

                            EntityQueryNode eJoin = null;
                            string whereClause = "";
                            if (!eq.WhereClause.IsNullOrEmpty())
                            {
                                whereClause = eq.ReplaceWhereClause(aliasWrapper);
                            }

                            if (!whereClause.IsNullOrEmpty() && (eq == topEntityQuery || (eq.Parent != null && eq.Parent.RelationType == QueryNodeType.InnerJoin)))
                            {
                                where += (where.IsNullOrEmpty() ? "" : " AND ") + "(" + whereClause + ")";
                            }

                            if (eq == topEntityQuery)
                            {
                                preCommands.Add("+ 'FROM " + eq.GetTableName(removeSchema, aliasWrapper) + " AS " + aliasWrapper + eq.Alias + aliasWrapper + " '");
                            }
                            else
                            {
                                eJoin = eq.Parent;
                                var eParent = eJoin.Parent;
                                if (eJoin.RelationType == QueryNodeType.InnerJoin)
                                {
                                    preCommands.Add("+ 'INNER JOIN " + eq.GetTableName(removeSchema, aliasWrapper) + " AS " + aliasWrapper + eq.Alias + aliasWrapper + " ON " + eJoin.GetJoinRelation(aliasWrapper, eq) + " '");
                                }
                                else if (eJoin.RelationType == QueryNodeType.LeftJoin)
                                {
                                    preCommands.Add("+ 'LEFT JOIN " + eq.GetTableName(removeSchema, aliasWrapper) + " AS " + aliasWrapper + eq.Alias + aliasWrapper + " ON " + eJoin.GetJoinRelation(aliasWrapper, eq) + (whereClause.IsNullOrEmpty() ? "" : " AND (" + whereClause + ")") + " '");
                                }
                            }

                            //Add Select

                            if (defaultProvider == Provider.SQLite && eJoin != null && eJoin.RelationType == QueryNodeType.LeftJoin && eJoin.JustFirstRightRelation)
                                selectProperties.Add("*" + eq.Alias + " = " + eq.Alias);

                            foreach (var prop in eq.Properties.Where(e => e.Selected).OrderBy(e => e.Name))
                            {
                                var entityAttribute = this.Attributes.FirstOrDefault(e => e.Name == prop.Name);

                                if (entityAttribute == null)
                                    continue;

                                if (prop.Formula.IsNullOrEmpty())
                                {
                                    selectProperties.Add(aliasWrapper + eq.Alias + aliasWrapper + "." + aliasWrapper + prop.SourceName + aliasWrapper + " AS " + aliasWrapper + prop.Name + aliasWrapper);
                                }
                                else
                                {
                                    selectProperties.Add(GetFormulaDefinition(eq, prop, aliasWrapper) + " AS " + aliasWrapper + prop.Name + aliasWrapper);
                                }
                            }

                            break;
                        case QueryNodeType.InnerJoin:
                        case QueryNodeType.LeftJoin:
                        default:
                            break;
                    }

                    eq.Joins.ForEach(e => restoreLayout(e, eq));

                };
                restoreLayout(topEntityQuery, null);

                //Where clause
                if (!where.IsNullOrEmpty())
                {
                    preCommands.Add("+ 'WHERE " + where + " '");
                }

                //Select properties
                mainBuilder.AddLine(genUnion ? "+ ' UNION ' +" : "var query = ");

                mainBuilder.AddLine("'SELECT '");
                for (int idx = 0; idx < selectProperties.Count; idx++)
                {
                    string expr = selectProperties[idx];
                    if (expr[0] == '*')
                        expr = expr.Right(expr.Length - 1);
                    mainBuilder.AddLine("+ '" + expr + (idx == selectProperties.Count - 1 ? "" : ",") + " '");
                }

                foreach (var command in preCommands)
                {
                    mainBuilder.AddLine(command);
                }
            }
        }

        internal List<EntityQueryNode> GetBusinessViewRootObjects()
        {
            List<EntityQueryNode> result = null;

            try
            {
                result = (this.ModelViewDefinition.IsNullOrEmpty() ? null : SerializationManager<List<EntityQueryNode>>.JsonToObject(this.ModelViewDefinition));
            }
            catch { }

            return result;
        }

        internal void AdjustAttributesForModelViews()
        {
            if (this.Kind == ClassKind.ModelView)
            {
                if (this.InStudy)
                    this.InStudy = false;

                //Remove ivalid properties for business views
                foreach (var attr in this.Attributes.ToArray())
                {
                    if (attr.ModelViewSource.IsNullOrEmpty() && attr.Formula.IsNullOrEmpty() && attr.ModelViewFormula.IsNullOrEmpty())
                        attr.Delete();
                    else if (attr.InStudy)
                        attr.InStudy = false;
                }
            }
        }

        internal void GenerateBusinessViewAttributes(CustomizedCode.Util.EntityQueryNode entityObject)
        {
            this.AdjustAttributesForModelViews();
            this.ModelViewDbSets = "";

            List<string> validProperties = new List<string>();
            if (entityObject != null)
            {
                Action<CustomizedCode.Util.EntityQueryNode, bool> updateAttributes = null;
                updateAttributes = (eq, hasLeft) =>
                {
                    if (eq.Updatable)
                    {
                        if (!("," + this.ModelViewDbSets + ",").Contains("," + eq.Name + ","))
                            this.ModelViewDbSets += (this.ModelViewDbSets.IsNullOrEmpty() ? "" : ",") + eq.Name;
                    }

                    //Adjust Properties
                    if (eq.RelationType == CustomizedCode.Util.QueryNodeType.Entity)
                    {
                        foreach (var prop in eq.Properties.Where(e => e.Selected))
                        {
                            var targetProp = this.Attributes.FirstOrDefault(e => e.ModelViewSource == (eq.Name + "." + prop.SourceName + "(" + eq.Key.ToString() + ")") || (!e.ModelViewFormula.IsNullOrEmpty() && e.Name == prop.Name));
                            if (targetProp == null)
                            {
                                targetProp = this.Attributes.AddNew() as ModelAttribute;
                                if (prop.Formula.IsNullOrEmpty())
                                    targetProp.ModelViewSource = (eq.Name + "." + prop.SourceName + "(" + eq.Key.ToString() + ")");
                                else
                                    targetProp.ModelViewSource = "";
                                targetProp.IsCustomized = false;
                            }

                            //Adjust properties
                            if (!targetProp.IsCustomized)
                            {
                                targetProp.ModelViewFormula = prop.Formula;
                                targetProp.Name = prop.Name;
                                targetProp.DisplayName = prop.DisplayName;
                                if (this.ModelViewAggregation && (targetProp.AggregationFunction == AggregationFunctions.Count || targetProp.AggregationFunction == AggregationFunctions.CountDistinct))
                                    targetProp.DataType = ModelDataType.Int;
                                else
                                    targetProp.DataType = prop.Type;
                                targetProp.IsPrimaryKey = (entityObject == eq && prop.PrimaryKey);
                                targetProp.IsNullable = prop.Nullable || hasLeft;
                                targetProp.DomainName = prop.DomainName;
                                targetProp.Precision = prop.Precision;
                                targetProp.Scale = prop.Scale;
                                targetProp.MaxLength = prop.MaxLength;
                            }
                            validProperties.Add((prop.Formula.IsNullOrEmpty() ? targetProp.ModelViewSource : prop.Name));
                        }
                    }

                    eq.Joins.ForEach(e => updateAttributes(e, (hasLeft || eq.RelationType == CustomizedCode.Util.QueryNodeType.LeftJoin)));
                };
                this.ModelViewMainEntity = entityObject.Name;
                updateAttributes(entityObject, false);
            }

            //Remove non selected attributes
            foreach (var attr in this.Attributes.Where(e => (!e.ModelViewSource.IsNullOrEmpty() && !validProperties.Any(p => p == e.ModelViewSource)) || (!e.ModelViewFormula.IsNullOrEmpty() && !validProperties.Any(p => p == e.Name))).ToArray())
            {
                attr.Delete();
            }
        }

    }
}
