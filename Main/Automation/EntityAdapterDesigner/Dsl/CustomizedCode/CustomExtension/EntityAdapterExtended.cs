using EnvDTE;
using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;
using Linx.Tools;
using Microsoft.CSharp;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapter : IAditionalInformation
    {
        double totalReportWidth = 0;
        string[] parts, partsForCmp;
        string entityName, keyBase, targetBase, edmKey, keyName;
        private List<string> membersOrder;


        public string GetReplaceFieldToFilterDataKey(bool byParentComposition)
        {
            if (byParentComposition)
            {
                var topParent = this.GetTopParent();

                string detailTypes = "";
                var parentNames = this.GetAllParentNames();
                foreach (var detail in topParent.SourceEntityAdapters)
                {
                    if (detail.Name != this.Name && !parentNames.Contains("\"" + detail.Name + "\""))
                    {
                        detailTypes += ", typeof(" + detail.Name + ")";
                    }
                }

                return "EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, \"" + topParent.PrimaryEntity + "\", \"" + this.PrimaryEntity + "\", \"" + this.GetFullEntityRelation() + "\", typeof(" + this.Name + "ParentComposition)" + detailTypes + ")";
            }
            else
            {
                return "EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(" + this.Name + "))";
            }
        }

        public bool IsBufferSaving()
        {
            return (this.TargetEntityAdapter == null && (this.IsLargeDataMode || this.ClientLocalServices.Count > 0));
        }

        public List<string> GetPrimaryKeys()
        {
            List<string> keys = new List<string>();

            if (HasDynamicPrimaryKey())
                keys.Add("EntityUniqueKey");
            else
            {
                keys.AddRange(this.GetAllInheritanceProperties().Where(e => IsPrimaryKey(e)).Select(e => e.Name));
            }

            return keys;
        }

        public PublicationEntity ToPublicationEntity()
        {
            var lookUpsPropInfo = this.GetAllLookUpPropertiesInfo(true);
            var entity = new PublicationEntity() { Name = this.Name, IsAggregationView = this.IsAggregationView, ForceAggregationPaging = this.ForceAggregationPaging, HasLocalResultEntityAdapters = this.LocalResultEntityAdapters.Count > 0, EntitiesDescription = this.GetEntitiesDescription().Extract("[", "]"), DisplayName = this.DisplayName, TemporaryKeyName = this.GetTemporaryKeyName(), CompositionHierarchy = String.Empty, Namespace = String.Empty, EdmEntityName = String.Empty, IsOlap = this.IsOlap(), IsIQueryable = (this.QueryReturnType == EntityQueryReturnType.IQueryable), IsUpdatable = !this.IsReadOnly };
            //Get properties
            foreach (var attribute in this.GetAllInheritanceAttributes())
            {
                entity.Properties.Add(new PublicationProperty()
                {
                    DisplayName = attribute.DisplayName,
                    EdmKey = attribute.GetEdmPath(true),
                    IsSuggestion = attribute is EntityAdapterPublicationProperty,
                    Name = attribute.Name,
                    DataType = attribute.Datatype,
                    DefaultValue = (attribute is EntityAdapterProperty ? ((EntityAdapterProperty)attribute).DefaultValue : (attribute is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)attribute).DefaultValue : String.Empty)),
                    DataFormatString = attribute.DataFormatString,
                    DisplayControl = (attribute is EntityAdapterProperty ? ((EntityAdapterProperty)attribute).DisplayControl : (attribute is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)attribute).DisplayControl : DisplayControlType.TextBox)).ToString(),
                    DomainName = attribute.DomainName,
                    KpiName = attribute.KpiName,
                    IsBrowsable = attribute.IsBrowsable,
                    IsEditable = attribute.IsEditable,
                    NoUpdate = attribute.NoUpdatable,
                    IsNull = attribute.IsNull,
                    Precision = attribute.Precision,
                    IsAutomaticSequency = (attribute is EntityAdapterProperty && this.HasAutomaticSequency((EntityAdapterProperty)attribute)),
                    LookUpInfo = (lookUpsPropInfo.ContainsKey(attribute.Name) ? lookUpsPropInfo[attribute.Name] : String.Empty),
                    Mask = attribute.Mask,
                    MaskType = attribute.MaskType,
                    DisplayOrder = attribute.DisplayOrder,
                    IsMeasure = attribute.IsMeasure,
                    AggregationFunction = attribute.AggregationFunction.ToString(),
                    ConnectedAttribute = attribute.ConnectedAttribute,
                    Description = attribute.Description,
                    MeasureFormula = attribute.MeasureFormula,
                    OrderByOrientation = (attribute is EntityAdapterProperty ? ((EntityAdapterProperty)attribute).OrderByOrientation : OrderByOrientationType.Ascending).ToString(),
                    OrderBySequence = (attribute is EntityAdapterProperty ? ((EntityAdapterProperty)attribute).OrderBySequence : -1),
                    IsPrimaryKey = attribute is EntityAdapterProperty && this.IsPrimaryKey((EntityAdapterProperty)attribute),
                    CustomMediaTable = attribute.CustomMediaTable,
                    Range = attribute.Range,
                    RemoveValidations = attribute.RemoveValidations,
                    BrandDecimalsControl = attribute.BrandDecimalsControl
                });
            }

            //Generate details
            foreach (var detail in this.GetAllInheritanceSourceEntityAdapters())
            {
                var pubDetail = detail.ToPublicationEntity();
                if (pubDetail != null)
                {
                    entity.Details.Add(pubDetail);
                    pubDetail.Parent = entity;
                }
            }

            return entity;
        }

        public string GetDomainNameLinqExpression(EntityAdapterAttribute fieldDef, string bmExpr)
        {
            string domainExpression = "";
            if (!fieldDef.IgnoreForQuery && !fieldDef.DomainName.IsNullOrEmpty() && !this.IsOlap())
            {
                var outputFile = Path.Combine(Path.Combine(this.EntityAdapterDesignerRoot.DocumentPath, "Domains"), "DomainViews.shared.cs");
                if (File.Exists(outputFile))
                {
                    domainExpression = File.ReadAllText(outputFile).Extract("<" + fieldDef.DomainName + ">", "</" + fieldDef.DomainName + ">");
                    if (!domainExpression.IsNullOrEmpty())
                    {
                        string delimiter = (fieldDef.Datatype.ToLower().Contains("string") ? "\"" : "");
                        domainExpression = domainExpression.Replace("#LxExpr#", bmExpr).Replace("[-", delimiter).Replace("-]", delimiter);
                    }
                }
            }

            return domainExpression;
        }

        public void UpdateEntitySets()
        {
            EntityAdapter entity = this;
            entity.EntitySets = string.Empty;
            var model = entity.GetCurrentDataModel();
            if (model == null) return;

            List<string> entities = new List<string>();
            entities.Add(entity.PrimaryEntity + "(" + entity.PrimaryEntity + ")");
            entities.AddRange(entity.SecondaryEntities.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string entityKeys, typeName;

            ContextEntity memberType;
            ContextProperty[] members;

            foreach (string entityRef in entities)
            {
                typeName = entityRef.Extract("(", ")");
                memberType = model.EdmInfo.Metadata.Entities.FirstOrDefault(e => e.Name == typeName);
                members = memberType.Properties;
                entityKeys = string.Empty;

                foreach (var pKey in members.Where(m => m.IsPrimaryKey()))
                    entityKeys += (entityKeys.IsNullOrEmpty() ? string.Empty : ",") + pKey.Name;

                //Adjust EntitySets
                entity.EntitySets += (entity.EntitySets.IsNullOrEmpty() ? "" : " ") + entityRef + "[" + entityKeys + "]";
            }
        }

        #region Model View
        public string GetViewDbSets()
        {
            return (this.PrimaryEntity.IsNullOrEmpty() ? "" : " typeof(" + this.PrimaryEntity.Replace(",", "), typeof(") + ") ");
        }

        public void ConfigureBusinessView()
        {
            var builder = new Linx.EntityAdapterDesigner.CustomCode.frmBusinessViewBuilder() { Entity = this };
            builder.ShowDialog();
        }

        public void PreViewEntity()
        {
            var binPath = this.EntityAdapterDesignerRoot.GetFullPath("Linx.Web.Service.Bus");
            string outputAssembly = System.IO.Path.Combine(binPath, "bin\\" + this.EntityAdapterDesignerRoot.GetAssemblyName() + ".dll");
            if (!File.Exists(outputAssembly))
            {
                MessageBox.Show("The assembly [" + outputAssembly + "] does not exist. Compile the BM project before this action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var preview = new Linx.EntityAdapterDesigner.CustomCode.frmQueryPreview();
            preview.AssemblyName = outputAssembly;
            preview.ConfigName = System.IO.Path.Combine(binPath, "Web.config");
            preview.ContextName = this.EntityAdapterDesignerRoot.GetServiceNameSpace() + "." + this.EntityAdapterDesignerRoot.GetDomainServiceName();
            preview.EntityClass = this;
            preview.ShowDialog();
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
            if (this.IsModelView)
            {
                //Remove ivalid properties for business views
                foreach (var attr in this.EntityAdapterProperties.ToArray())
                {
                    if (attr.ModelViewSource.IsNullOrEmpty() && attr.ModelViewFormula.IsNullOrEmpty() && attr.ModelViewFormula.IsNullOrEmpty())
                        attr.Delete();
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
                this.PrimaryEntity = entityObject.Name;
                Action<CustomizedCode.Util.EntityQueryNode, bool> updateAttributes = null;
                updateAttributes = (eq, hasLeft) =>
                {
                    bool isBM = (this.GetCurrentDataModel() != null);
                    string sourceType = (isBM ? "" : (eq.ContextType.IsNullOrEmpty() ? this.EntityAdapterDesignerRoot.GetContextNamespace() + "." + this.EntityAdapterDesignerRoot.GetContextName() + "DomainService" : eq.ContextType));
                    if (eq.Updatable && !eq.BusinessModelType.IsNullOrEmpty())
                    {
                        if (!("," + this.ModelViewDbSets + ",").Contains("," + eq.Name + ","))
                            this.ModelViewDbSets += (this.ModelViewDbSets.IsNullOrEmpty() ? "" : ",") + (isBM ? "" : sourceType + "|" + (eq.BusinessModelType.IsNullOrEmpty() ? "" : eq.BusinessModelType) + "#") + eq.Name;
                    }

                    //Adjust Properties
                    if (eq.RelationType == CustomizedCode.Util.QueryNodeType.Entity)
                    {
                        foreach (var prop in eq.Properties.Where(e => e.Selected))
                        {
                            var targetProp = this.EntityAdapterProperties.FirstOrDefault(e => e.ModelViewSource == (eq.Name + "." + prop.SourceName + "(" + eq.Key.ToString() + ")") || (!e.ModelViewFormula.IsNullOrEmpty() && e.Name == prop.Name));
                            if (targetProp == null)
                            {
                                targetProp = this.EntityAdapterProperties.AddNew() as EntityAdapterProperty;
                                if (prop.Formula.IsNullOrEmpty())
                                {
                                    targetProp.ModelViewSource = (eq.Name + "." + prop.SourceName + "(" + eq.Key.ToString() + ")");
                                }
                                else
                                    targetProp.ModelViewSource = "";

                                targetProp.IsCustomized = false;
                                targetProp.IsEditable = true;
                                targetProp.IsBrowsable = true;
                            }

                            if (!targetProp.IsCustomized)
                            {
                                //Adjust edm key
                                if (isBM)
                                {
                                    targetProp.EdmKey = (eq.Name != this.PrimaryEntity ? this.PrimaryEntity + "." : "") + eq.Name + "." + prop.SourceName;
                                    targetProp.DataRelationKey = "";
                                }
                                else
                                {
                                    targetProp.EdmKey = "";
                                    targetProp.DataRelationKey = eq.Name + "#" + sourceType.Left(sourceType.Length - sourceType.Right(".").Length - 1) + "#" + eq.Name + "." + prop.SourceName;
                                }

                                targetProp.ModelViewFormula = prop.Formula;
                                targetProp.Name = prop.Name;
                                targetProp.DisplayName = prop.DisplayName;
                                if (this.IsAggregationView && (targetProp.AggregationFunction == UIAggregationFunctions.Count || targetProp.AggregationFunction == UIAggregationFunctions.CountDistinct))
                                    targetProp.Datatype = "System.Int32";
                                else
                                    targetProp.Datatype = prop.Type;
                                targetProp.IsPK = (entityObject == eq && prop.PrimaryKey);
                                targetProp.IsNull = prop.Nullable || hasLeft;
                                targetProp.DomainName = prop.DomainName;
                                targetProp.Precision = prop.Precision + ":" + prop.Scale;
                                targetProp.IsCustomized = false;
                                targetProp.LookUpSubscription = prop.LookupInfo;
                                if (!prop.DisplayControl.IsNullOrEmpty())
                                    targetProp.DisplayControl = (DisplayControlType)Enum.Parse(typeof(DisplayControlType), prop.DisplayControl);
                                else
                                    targetProp.DisplayControl = targetProp.GetDisplayControlClass();
                            }

                            validProperties.Add((prop.Formula.IsNullOrEmpty() ? targetProp.ModelViewSource : prop.Name));
                        }
                    }

                    eq.Joins.ForEach(e => updateAttributes(e, (hasLeft || eq.RelationType == CustomizedCode.Util.QueryNodeType.LeftJoin)));
                };

                updateAttributes(entityObject, false);
            }

            //Remove non selected attributes
            foreach (var attr in this.EntityAdapterProperties.Where(e => (!e.ModelViewSource.IsNullOrEmpty() && !validProperties.Any(p => p == e.ModelViewSource)) || (!e.ModelViewFormula.IsNullOrEmpty() && !validProperties.Any(p => p == e.Name))).ToArray())
            {
                attr.Delete();
            }
        }

        #endregion

        #region Model View Code Generation

        public string GetSuggestedModelViewOrder()
        {
            string order = GetModelViewOrderBy("");
            if (order.IsNullOrEmpty() && this.EntityAdapterProperties.Count > 0)
            {
                var propOrder = this.EntityAdapterProperties.FirstOrDefault(e => e.IsPK);
                if (propOrder == null)
                    propOrder = this.EntityAdapterProperties[0];

                return ".OrderBy(e => e." + propOrder.Name + ")";
            }

            return "";
        }

        private string GetModelViewOrderBy(string alias)
        {
            string order = "";

            foreach (var prop in this.EntityAdapterProperties.Where(e => e.OrderBySequence >= 0).OrderBy(o => o.OrderBySequence))
            {
                order += (order.IsNullOrEmpty() ? String.Empty : ", ") + alias + "." + prop.Name + " " + prop.OrderByOrientation.ToString().ToLower();
            }

            return order;
        }

        public void GetBusinessViewWrapperLinq(EntityQueryNode topEntityQuery, Linx.Tools.CodeBuilder builder, List<string> hasFilterDefinitions)
        {
            List<string> selectProperties = new List<string>();
            List<string> groupByProperties = new List<string>();
            string groupBy = "", outerWhere = "";
            List<string> outerExclWheres = new List<string>();

            if (!this.Filter.IsNullOrEmpty())
                outerWhere = "(" + this.Filter.Replace("this.", "f.") + ")";

            Action<EntityQueryNode> generateWrapperLinq = null;
            generateWrapperLinq = (eq) =>
            {
                if (eq.RelationType == QueryNodeType.Entity)
                {
                    foreach (var prop in eq.Properties.Where(e => e.Selected).OrderBy(e => e.Name))
                    {
                        var entityAttribute = this.EntityAdapterProperties.FirstOrDefault(e => e.Name == prop.Name);

                        if (entityAttribute == null)
                            continue;

                        if (this.IsAggregationView)
                        {
                            if (entityAttribute.AggregationFunction == UIAggregationFunctions.None)
                            {
                                groupByProperties.Add(prop.Name + " = q." + prop.Name);
                                selectProperties.Add(prop.Name + " = rg0.Key." + prop.Name);
                            }
                            else
                            {
                                if (entityAttribute.AggregationFunction == UIAggregationFunctions.Count)
                                    selectProperties.Add(prop.Name + " = rg0.Count()");
                                if (entityAttribute.AggregationFunction == UIAggregationFunctions.CountDistinct)
                                    selectProperties.Add(prop.Name + " = rg0" + (entityAttribute.CountDistinctFilter.IsNullOrEmpty() ? "" : ".Where(e => " + entityAttribute.CountDistinctFilter.Replace("this.", "e.q.").Replace("[ThisRef]", "e.q").Replace("[Value]", "e.q." + entityAttribute.Name) + ")") + ".Select(e => e.q." + entityAttribute.Name + ").Distinct().Count()");
                                else
                                    selectProperties.Add(prop.Name + " = rg0." + entityAttribute.AggregationFunction.ToString() + "(e => e.q." + prop.Name + ")");
                            }
                        }

                        if (!entityAttribute.Filter.IsNullOrEmpty())
                        {
                            outerWhere += (outerWhere.IsNullOrEmpty() ? "" : " && ") + "(" + ReplaceHasFilterDefinitions(entityAttribute.Filter.Replace("this.", "f.").Replace("[ThisRef]", "f").Replace("[Value]", "f." + entityAttribute.Name)) + ")";
                            AddHasFilterDefinitions(entityAttribute.Filter, hasFilterDefinitions);
                        }
                    }
                }

                eq.Joins.ForEach(e => generateWrapperLinq(e));

            };
            generateWrapperLinq(topEntityQuery);

            if (this.IsAggregationView)
            {
                builder.AddLine(";");
                builder.AddLine();
                builder.AddLine("var queryAggr = (from q in query");

                groupBy += "group new { q } by new { ";

                for (int idx = 0; idx < groupByProperties.Count; idx++)
                {
                    groupBy += groupByProperties[idx] + (idx == groupByProperties.Count - 1 ? "" : ", ");
                }

                groupBy += " } into rg0";

                builder.AddLine(groupBy);

                //Select properties
                builder.AddLine("select new " + this.Name + "()");
                builder.AddLine("{");
                builder.IncreaseIndent();
                for (int idx = 0; idx < selectProperties.Count; idx++)
                {
                    builder.AddLine(selectProperties[idx] + (idx == selectProperties.Count - 1 ? "" : ","));
                }
                builder.DecreaseIndent();
                builder.AddLine("})");
            }

            builder.AddLine((outerWhere.IsNullOrEmpty() ? "" : ".Where(f => " + outerWhere + ")") + ";");

            //Add exclusive filters
            foreach (var exclFilter in outerExclWheres)
            {
                builder.AddLine(exclFilter);
            }
        }

        public string GetBusinessViewLinqDefinition(string indent)
        {
            Linx.Tools.CodeBuilder builder = new Linx.Tools.CodeBuilder(indent), auxiliaryBuilder = new Linx.Tools.CodeBuilder(indent);
            var topEntityQueries = this.GetBusinessViewRootObjects();
            List<string> hasFilterDefinitions = new List<string>();
            Dictionary<string, string> contextInstances = new Dictionary<string, string>();

            if (topEntityQueries != null && topEntityQueries.Count > 0)
            {
                for (int idx = 0; idx < topEntityQueries.Count; idx++)
                {
                    GetBusinessViewLinqDefinition(builder, topEntityQueries[idx], (idx > 0), hasFilterDefinitions, contextInstances);
                }

                GetBusinessViewWrapperLinq(topEntityQueries[0], builder, hasFilterDefinitions);
            }

            if (contextInstances.Count > 0)
            {
                foreach (var ctxDef in contextInstances.Keys)
                {
                    if (ctxDef != "*")
                        auxiliaryBuilder.AddLine(ctxDef);
                }
                auxiliaryBuilder.AddLine();
            }

            if (hasFilterDefinitions.Count > 0)
            {
                foreach (var hasFilterDef in hasFilterDefinitions)
                {
                    auxiliaryBuilder.AddLine(hasFilterDef);
                }
                auxiliaryBuilder.AddLine();
            }

            string order = this.GetModelViewOrderBy("q");
            if (!order.IsNullOrEmpty())
            {
                string varQueryResult = "query" + (this.IsAggregationView ? "Aggr" : "");
                builder.AddLine(varQueryResult + " = (from q in " + varQueryResult + " orderby " + order + " select q);");
            }

            var result = auxiliaryBuilder.GetBody() + builder.GetBody();

            if (contextInstances.Values.Distinct().Count() > 1)
                result = result.Replace("NoAssociations()", "NoAssociations().ToArray()");

            if (contextInstances.Values.Distinct().Count() == 1)
            {
                var currentBM = this.EntityAdapterDesignerRoot.EntityDataModels.FirstOrDefault();
                string bmRef = (currentBM == null ? "" : currentBM.TargetNamespace + "." + currentBM.Name);

                var contextName = contextInstances.Keys.FirstOrDefault(e => e.Contains(" =")) ?? "";
                contextName = contextName.Left(" =").Right(" ");
                if (contextName.IsNullOrEmpty())
                {
                    result = result.Replace("#Params#", "");
                }
                else
                {
                    if (!bmRef.IsNullOrEmpty() && contextInstances.Values.First() == bmRef)
                    {
                        contextName = "context";
                    }
                    else
                    {
                        string firstContext = contextInstances.Keys.First(e => e.Contains(" ="));
                        result = result.Replace(firstContext, firstContext.Replace("#Params#", ""));
                    }
                    result = result.Replace("#Params#", contextName + ".GetEDM(), null");
                }
            }
            else
                result = result.Replace("#Params#", "");

            return result;
        }

        public void GetBusinessViewLinqDefinition(Linx.Tools.CodeBuilder builder, EntityQueryNode topEntityQuery, bool genUnion, List<string> hasFilterDefinitions, Dictionary<string, string> contextInstances)
        {
            if (topEntityQuery != null)
            {
                bool hasBM = this.GetCurrentDataModel() != null;
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

                            string whereClause = "";
                            if (!eq.WhereClause.IsNullOrEmpty())
                            {
                                AddHasFilterDefinitions(eq.WhereClause, hasFilterDefinitions);
                                whereClause = ReplaceHasFilterDefinitions(eq.WhereClause.Replace("this.", eq.Alias + "."));
                            }

                            if (!whereClause.IsNullOrEmpty() && (eq == topEntityQuery || (eq.Parent != null && eq.Parent.RelationType == QueryNodeType.InnerJoin)))
                            {
                                where += (where.IsNullOrEmpty() ? "" : " && ") + "(" + whereClause + ")";
                            }

                            if (!hasBM)
                            {
                                string keyCommand = (eq.ContextType.IsNullOrEmpty() ? "*" : eq.ContextType + " " + eq.ContextAlias + " = new " + eq.ContextType + "(#Params#);");
                                if (!contextInstances.ContainsKey(keyCommand))
                                {
                                    contextInstances.Add(keyCommand, eq.BusinessModelType);
                                }
                            }

                            if (eq == topEntityQuery)
                            {
                                builder.AddLine((genUnion ? ".Union" : "var query = ") + "(from " + eq.Alias + " in " + eq.ContextAlias + "." + (hasBM ? eq.Name : "Get" + eq.Name + "NoAssociations()"));
                            }
                            else
                            {
                                var eJoin = eq.Parent;
                                var eParent = eJoin.Parent;
                                if (eJoin.RelationType == QueryNodeType.InnerJoin)
                                {
                                    builder.AddLine("join " + eq.Alias + " in " + eq.ContextAlias + "." + (hasBM ? eq.Name : "Get" + eq.Name + "NoAssociations()") + " on " + eJoin.GetJoinLeftRelation() + " equals " + eJoin.GetJoinRightRelation());
                                }
                                else if (eJoin.RelationType == QueryNodeType.LeftJoin)
                                {
                                    builder.AddLine("join " + eq.Alias + "LF in " + eq.ContextAlias + "." + (hasBM ? eq.Name : "Get" + eq.Name + "NoAssociations()") + (whereClause.IsNullOrEmpty() ? "" : ".Where(e => " + whereClause.Replace(eq.Alias + ".", "e.") + ")") + " on " + eJoin.GetJoinLeftRelation() + " equals " + eJoin.GetJoinRightRelation() + " into " + eq.Alias + "Tmp");
                                    builder.AddLine("from " + eq.Alias + " in " + eq.Alias + "Tmp.DefaultIfEmpty()");
                                }
                            }

                            //Add Select
                            foreach (var prop in eq.Properties.Where(e => e.Selected).OrderBy(e => e.Name))
                            {
                                var entityAttribute = this.EntityAdapterProperties.FirstOrDefault(e => e.Name == prop.Name);

                                if (entityAttribute == null)
                                    continue;

                                string selectRef = "";
                                if (prop.Formula.IsNullOrEmpty())
                                {
                                    selectRef = eq.Alias + "." + prop.SourceName;
                                    selectProperties.Add(prop.Name + " = " + selectRef);
                                }
                                else
                                {
                                    selectRef = prop.Formula.Replace("this.", eq.Alias + ".");
                                    selectProperties.Add(prop.Name + " = " + selectRef);
                                }
                                if (!entityAttribute.DomainName.IsNullOrEmpty())
                                {
                                    selectProperties.Add(prop.Name + "Name = " + this.GetDomainNameLinqExpression(entityAttribute, selectRef));
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
                    builder.AddLine("where " + where);
                }

                //Select properties
                builder.AddLine("select new " + (this.IsAggregationView ? "" : this.Name + "()"));
                builder.AddLine("{");
                builder.IncreaseIndent();
                for (int idx = 0; idx < selectProperties.Count; idx++)
                {
                    builder.AddLine(selectProperties[idx] + (idx == selectProperties.Count - 1 ? "" : ","));
                }
                builder.DecreaseIndent();
                builder.AddLine("})");
            }
        }

        private void AddHasFilterDefinitions(string whereCaluse, List<string> hasFilterDefinitions)
        {
            var filters = whereCaluse.Split(new string[] { "HasFilter(" }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Left(")")).Where(e => !e.IsNullOrEmpty()).ToArray();
            foreach (var entityAttribute in filters)
            {
                if (whereCaluse.Contains("HasFilter(" + entityAttribute + ")"))
                {
                    string definition = "bool has" + entityAttribute + " = (predicate + \" \").Contains(\" " + entityAttribute + " \");";
                    if (!hasFilterDefinitions.Contains(definition))
                        hasFilterDefinitions.Add(definition);
                }
            }
        }

        private string ReplaceHasFilterDefinitions(string whereCaluse)
        {
            var filters = whereCaluse.Split(new string[] { "HasFilter(" }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Left(")")).Where(e => !e.IsNullOrEmpty()).ToArray();
            foreach (var entityAttribute in filters)
            {
                if (whereCaluse.Contains("HasFilter(" + entityAttribute + ")"))
                {
                    whereCaluse = whereCaluse.Replace("HasFilter(" + entityAttribute + ")", "has" + entityAttribute);
                }
            }

            return whereCaluse;
        }

        #endregion


        public void AdjustQueryReturnType()
        {
            if (this.IsDashboardFilter)
                this.QueryReturnType = EntityQueryReturnType.IEnumerable;
            else
            {
                if (this.QueryReturnType == EntityQueryReturnType.IEnumerable && this.EntityAdapterRepresentation == null && !this.ExistsEvent("OnSearchingReplacement") && this.LocalEntityAdapter == null && this.GetCurrentDataModel() != null)
                {
                    this.QueryReturnType = EntityQueryReturnType.IQueryable;
                }
            }
        }

        public List<LookUpAdapter> GetAllLookUpAdapters()
        {
            List<LookUpAdapter> list = new List<LookUpAdapter>();
            EntityAdapter baseEntity = this;
            while (baseEntity != null)
            {
                list.AddRange(baseEntity.LookUpAdapters);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return list;
        }

        public bool IsOlap()
        {
            return !this.GetCubeName().IsNullOrEmpty() && this.GetOlapCatalog() != null;
        }

        public OlapCatalog GetOlapCatalog()
        {
            var baseClass = this.GetTopBaseClass();
            if (baseClass.EntityDataModel == null)
            {
                if (baseClass.OlapCatalog != null)
                    return baseClass.OlapCatalog;
                else
                    return (baseClass.TargetEntityAdapter != null ? baseClass.TargetEntityAdapter.GetOlapCatalog() : null);
            }

            return null;
        }

        public string GetCubeName()
        {
            var baseClass = this.GetTopBaseClass();
            if (!baseClass.CubeName.IsNullOrEmpty())
                return baseClass.CubeName;
            else
                return (baseClass.TargetEntityAdapter != null ? baseClass.TargetEntityAdapter.GetCubeName() : null);
        }

        public EntityAdapterRepresentation GetEntityAdapterRepresentation()
        {
            var baseClass = this.GetTopBaseClass();
            if (baseClass.EntityAdapterRepresentation != null)
                return baseClass.EntityAdapterRepresentation;
            else
                return (baseClass.TargetEntityAdapter != null ? baseClass.TargetEntityAdapter.GetEntityAdapterRepresentation() : null);
        }

        public EntityAdapter GetLocalEntityAdapter()
        {
            var baseClass = this.GetTopBaseClass();
            if (baseClass.LocalEntityAdapter != null)
                return baseClass.LocalEntityAdapter;
            else
                return (baseClass.TargetEntityAdapter != null ? baseClass.TargetEntityAdapter.GetLocalEntityAdapter() : null);
        }

        public EntityDataModel GetCurrentDataModel()
        {
            var baseClass = this.GetTopBaseClass();
            if (baseClass.OlapCatalog == null)
            {
                if (baseClass.IsModelView)
                    return baseClass.EntityDataModel;

                if (baseClass.EntityDataModel != null)
                    return baseClass.EntityDataModel;
                else
                    return (baseClass.TargetEntityAdapter != null ? baseClass.TargetEntityAdapter.GetCurrentDataModel() : null);
            }

            return null;
        }

        public string GenerateOlapQuery(string indent)
        {
            bool firstControl;
            string result = String.Empty;
            if (this.IsOlap() && !this.IsDashboardFilter)
            {
                Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
                string measuresList = String.Empty, dimensionsList = String.Empty;
                Dictionary<string, string> measuresDict = new Dictionary<string, string>();

                //Get Measures and Dimensions

                foreach (var prop in this.GetAllInheritanceAttributes().Where(e => e.IsMeasure && !(e is EntityAdapterFormula)))
                {
                    measuresDict.Add(prop.Name, prop.DataRelationKey);
                }
                measuresList = string.Join(",", (measuresDict.Select(i => string.Format("\"{0}|{1}\"", i.Value, i.Key))));

                firstControl = true;
                foreach (var prop in this.GetAllInheritanceAttributes().Where(e => !e.IsMeasure && !(e is EntityAdapterFormula)))
                {
                    dimensionsList += (firstControl ? String.Empty : ", ") + "\"" + prop.DataRelationKey + "\"";
                    firstControl = false;
                }

                builder.AddLine("");
                builder.AddLine("[Ignore()]");
                builder.AddLine("public IEnumerable<" + this.Name + "> GetOlap" + this.Name + "(List<EntitySearch> entitySearchList)");
                builder.AddLine("{");

                //Generate Dictionary
                builder.AddLine("   List<MDXField> fieldsMap = new List<MDXField>();");
                var properties = this.GetAllInheritanceAttributes().Where(e => !(e is EntityAdapterFormula)).ToList();

                //Add properties from DashBoard
                if (this.TargetEntityAdapter != null && this.GetTopParent().IsDashboardFilter)
                {
                    var dbProperties = this.GetTopParent().GetAllInheritanceAttributes().Where(e => !(e is EntityAdapterFormula));
                    dbProperties = dbProperties.Where(e => !properties.Any(p => p.Name == e.Name)).ToArray();
                    properties.AddRange(dbProperties);
                }

                foreach (var prop in properties)
                {
                    builder.AddLine("   fieldsMap.Add(new MDXField(\"" + prop.Name + "\", \"" + prop.DataRelationKey + "\", " + prop.IsMeasure.ToString().ToLower() + "));");
                }
                //Especial Search Treat
                builder.AddLine("   var fields = entitySearchList.SelectMany(e => e.Expressions).Where(ex => ex.Name == \"Field\" && ((string)ex.Value ?? \"\").StartsWith(\"(PEsp)\")).Select(s => ((string)s.Value).Right(\"(PEsp)\"));");
                builder.AddLine("   fields.Foreach(i => { fieldsMap.Add(new MDXField(i, i, i.Contains(\"[Measures]\"))); });");
                builder.AddLine("   entitySearchList.ForEach(e => e.Expressions.ToList().ForEach(f => { if (f.Name == \"Field\" && ((string)f.Value).Contains(\"(PEsp)\")) f.Value = ((string)f.Value).Replace(\"(PEsp)\", \"\"); }));");
                //Metadata filter Analysis
                builder.AddLine("   //Verify valid properties for LINQ");
                builder.AddLine("   string[] validProperties = (entitySearchList == null ? new string[] { } : EntitySearch.GetValidProperties(entitySearchList));");
                builder.AddLine(this.GetValidMetaDataProperties("   ", "validProperties"));

                builder.AddLine("   validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));");

                builder.AddLine("   MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);");
                builder.AddLine("   builder.Conditions(entitySearchList);");

                builder.AddLine("   string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString(\"" + this.GetOlapCatalog().Name + "\");");
                builder.AddLine("   if (connString == \"name=" + this.GetOlapCatalog().Name + "\") connString = Linx.Tools.ConnectionManager.GetConnectionString(\"" + this.GetOlapCatalog().Name + "\");");
                builder.AddLine("   using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))");
                builder.AddLine("   {");
                builder.AddLine("       string mdxScript = (new MDXHelper(\"" + this.GetCubeName() + "\"))");
                builder.AddLine("          .Measures(" + measuresList + ")");
                builder.AddLine("          .Rows(" + dimensionsList + ")");
                builder.AddLine("          .SetIdLinxDimensions(\"" + this.GetOlapCatalog().IdLinxDimensions + "\")");
                builder.AddLine("          .SetIdGpeconDimensions(\"" + this.GetOlapCatalog().IdGpeconDimensions + "\")");
                builder.AddLine("          .SetIdBandeiraRedeDimensions(\"" + this.GetOlapCatalog().IdBandeiraRedeDimensions + "\")");
                builder.AddLine("          .SetIdFilialDimensions(\"" + this.GetOlapCatalog().IdFilialDimensions + "\")");
                builder.AddLine("          .SetMeasuresDimensions(\"" + this.GetOlapCatalog().MeasuresDimensions + "\")");
                builder.AddLine("          .Where(builder).FilterMetaData(validProperties)");
                builder.AddLine("          .SubqueryFilter(\"" + this.Filter + "\")");
                builder.AddLine("          .SetIdGpEcon(CurrentIdGpEcon())");
                builder.AddLine("          .SetIdLinx(CurrentIdLinx(\"{0}\"))", this.GetOlapCatalog().Name);
                builder.AddLine("          .SetIdFiliais(CurrentIdFiliais())");
                builder.AddLine("          .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});");
                builder.AddLine("       ");
                builder.AddLine("       var command = connection.CreateCommand();");
                builder.AddLine("       command.Properties.Add(\"DbpropMsmdFlattened2\", true);");
                builder.AddLine("       command.CommandText = mdxScript;");
                builder.AddLine("       connection.Open();");
                builder.AddLine("       IEnumerable<" + this.Name + "> result = null;");
                builder.AddLine("       using (var reader = command.ExecuteReader())");
                builder.AddLine("       {");
                builder.AddLine("           List<string> columnInReader = new List<string>();");
                builder.AddLine("           var dt = reader.GetSchemaTable();");
                builder.AddLine("           foreach (DataRow row in dt.Rows) columnInReader.Add(row[\"ColumnName\"].ToString());");
                builder.AddLine("           result = reader.Select(r => new " + this.Name + " {");

                //Get Projection CLass
                firstControl = true;
                foreach (var prop in this.GetAllInheritanceAttributes().Where(e => !(e is EntityAdapterFormula)))
                {
                    if (prop.IsMeasure)
                        builder.AddLine("           " + (firstControl ? String.Empty : ", ") + prop.Name + " = !columnInReader.Contains(\"[Measures].[" + prop.Name + "]\") || (validProperties.Length > 0 && !validProperties.Contains(\"" + prop.DataRelationKey + "\"))  || r[\"[Measures].[" + prop.Name + "]\"] is DBNull || r[\"[Measures].[" + prop.Name + "]\"] == null ? default(" + prop.Datatype + ") : System.Convert.To" + prop.Datatype + "(r[\"[Measures].[" + prop.Name + "]\"])" + (prop.Datatype.ToLower() == "double" ? ".GetValue()" : String.Empty));
                    else
                        builder.AddLine("           " + (firstControl ? String.Empty : ", ") + prop.Name + " = !columnInReader.Contains(\"" + prop.DataRelationKey + ".[MEMBER_CAPTION]\") || (validProperties.Length > 0 && !validProperties.Contains(\"" + prop.DataRelationKey + "\")) || r[\"" + prop.DataRelationKey + ".[MEMBER_CAPTION]\"] is DBNull || r[\"" + prop.DataRelationKey + ".[MEMBER_CAPTION]\"] == null ? default(" + prop.Datatype + ") : " + (prop.Datatype.ToLower().Contains("string") ? String.Empty : prop.Datatype + ".Parse") + "((string)r[\"" + prop.DataRelationKey + ".[MEMBER_CAPTION]\"])");
                    firstControl = false;
                }

                builder.AddLine("           }).ToList();");
                builder.AddLine("       }");
                builder.AddLine("       return result;");
                builder.AddLine("   }");
                builder.AddLine("}");
                result = builder.GetBody();
            }
            return result;
        }

        public bool HasEdmSource()
        {
            return this.EntityAdapterRepresentation == null && !this.PrimaryEntity.IsNullOrEmpty() && this.GetCurrentDataModel() != null;
        }

        public bool IsModelViewSource()
        {
            return (this.HasEdmSource() && this.GetCurrentDataModel().EdmInfo.IsModelView(this.PrimaryEntity));
        }

        private void LoadMembersOrder(bool isPoco, bool byParentComposition)
        {
            if (!this.GenerateDataMemberOrder)
                return;

            membersOrder = new List<string>();
            List<EntityAdapter> entitiesInheritance = new List<EntityAdapter>();

            //Fill entitiesInheritance
            entitiesInheritance.Add(this);
            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                entitiesInheritance.Add(baseEntity);
                //Base class
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            //Add members
            for (int idx = entitiesInheritance.Count - 1; idx >= 0; idx--)
            {
                AddMembersToList(entitiesInheritance[idx], membersOrder, idx == (entitiesInheritance.Count - 1), isPoco, byParentComposition);
            }
        }

        private void AddMembersToList(EntityAdapter entity, List<string> members, bool generateInternalProperties, bool isPoco, bool byParentComposition)
        {
            //Attributes
            members.AddRange(entity.GetAllAttributes().Select(e => (e.DataMemberName.IsNullOrEmpty() ? e.Name : e.DataMemberName)).ToList());

            if (generateInternalProperties)
            {
                //Add internal properties control
                if (entity.BaseEntityAdapter == null && entity.HasDynamicPrimaryKey())
                    membersOrder.Add("EntityUniqueKey");

                if ((entity.HasEntityKeyLocalRelation() || (entity.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == entity).Count() > 0 && entity.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == entity).First().HasEntityKeyLocalRelation())))
                    membersOrder.Add("EntityKeyLocalRelation");

                if (!isPoco && !byParentComposition)
                {
                    var temporaryKey = entity.GetTemporaryKey();
                    if (!temporaryKey.IsNull())
                        membersOrder.Add("Temporary" + temporaryKey.Name);
                }
                ////////////////////////////////////
            }

            //Instances
            members.AddRange(EntityInstanceReferencesEntityOwners.GetLinksToInstanceEntities(entity).Select(e => (e.DataMemberName.IsNullOrEmpty() ? e.Name : e.DataMemberName)).OrderBy(e => e).ToList());
            //Collections
            members.AddRange(EntityCollectionReferencesEntityOwners.GetLinksToCollectionEntities(entity).Select(e => (e.DataMemberName.IsNullOrEmpty() ? e.Name : e.DataMemberName)).OrderBy(e => e).ToList());
            //Parent
            if (entity.TargetEntityAdapter != null)
                members.Add((entity.TargetEntityAdapter.DataContractName.IsNullOrEmpty() ? entity.TargetEntityAdapter.Name : entity.TargetEntityAdapter.DataContractName));
            //Details
            members.AddRange(entity.SourceEntityAdapters.Select(e => e.GetAssociationDataMemberName()).OrderBy(e => e).ToList());
        }

        public int GetMemberOrder(string memberName)
        {
            if (membersOrder == null)
                return 1;

            int result = membersOrder.IndexOf(memberName);
            if (result == -1) //Not found
                result = membersOrder.Count;

            return result + 1;
        }

        public void AdjustColorShape()
        {
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as EntityAdapterShape;
            if (shape != null)
            {
                if (this.BaseEntityAdapter != null || !this.CustomBaseType.IsNullOrEmpty())
                {
                    if (shape.TextColor != System.Drawing.Color.Black || shape.OutlineColor != System.Drawing.Color.Black || shape.OutlineDashStyle != System.Drawing.Drawing2D.DashStyle.Dash)
                    {
                        shape.SetOutlineColor(System.Drawing.Color.Black);
                        shape.SetTextColor(System.Drawing.Color.Black);
                        shape.OutlineDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    }
                }
                else
                {
                    if (shape.TextColor != System.Drawing.Color.White || shape.OutlineColor != System.Drawing.Color.Transparent || shape.OutlineDashStyle != System.Drawing.Drawing2D.DashStyle.Solid)
                    {
                        shape.SetOutlineColor(System.Drawing.Color.Transparent);
                        shape.SetTextColor(System.Drawing.Color.White);
                        shape.OutlineDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }
                }
            }
        }

        public string GetEntityClassInfoValue()
        {
            return (this.BaseEntityAdapter != null ? "Base Type: " + this.BaseEntityAdapter.Name : (this.LocalEntityAdapter != null ? "Local View From " + this.LocalEntityAdapter.Name : (this.BusinessExtension == BusinessExtensions.None ? String.Empty : "Business: " + this.BusinessExtension.ToString())));
        }

        public void CheckEdmTreeMaximumLevel()
        {
            int maxLevel = this.EdmTreeMaximumLevel;
            foreach (EntityAdapterAttribute attrib in this.GetAllInheritanceAttributes().Where(e => !e.IsCustomized))
            {
                if (attrib is EntityAdapterProperty && !((EntityAdapterProperty)attrib).EdmKey.IsNullOrEmpty() && ((EntityAdapterProperty)attrib).EdmKey.Occurs(".") > maxLevel)
                    maxLevel = ((EntityAdapterProperty)attrib).EdmKey.Occurs(".");

                if (attrib is EntityAdapterPublicationProperty && !((EntityAdapterPublicationProperty)attrib).EdmKey.IsNullOrEmpty() && ((EntityAdapterPublicationProperty)attrib).EdmKey.Occurs(".") > maxLevel)
                    maxLevel = ((EntityAdapterPublicationProperty)attrib).EdmKey.Occurs(".");
            }

            foreach (var filter in this.EntityAdapterExtendedFilters)
            {
                foreach (var fProp in filter.EntityAdapterPropertyExtendedFilters)
                {
                    if (!fProp.EdmKey.IsNullOrEmpty() && fProp.EdmKey.Occurs(".") > maxLevel)
                        maxLevel = fProp.EdmKey.Occurs(".");
                }
            }

            if (maxLevel > this.EdmTreeMaximumLevel)
            {
                using (Transaction transaction = this.Store.TransactionManager.BeginTransaction("CheckEdmTreeMaximumLevel"))
                {
                    this.EdmTreeMaximumLevel = maxLevel;
                    transaction.Commit();
                }
            }
        }

        public void RemoveInheritanceConflicts()
        {
            if (this.BaseEntityAdapter != null)
            {
                var derivedAttributes = this.GetAllAttributes();
                if (derivedAttributes.Count > 0)
                {
                    var baseAttributes = this.BaseEntityAdapter.GetAllInheritanceAttributes().Select(e => e.Name).ToList();
                    if (baseAttributes.Count > 0)
                    {
                        var redundantProperties = derivedAttributes.Where(e => baseAttributes.Contains(e.Name)).ToList();
                        if (redundantProperties.Count > 0 && MessageBox.Show("Remove all duplicate properties in the derived class?", "Duplicate properties were detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (Transaction transaction =
                                    this.Store.TransactionManager.BeginTransaction("Remove duplicate properties."))
                            {
                                foreach (var attribute in redundantProperties)
                                {
                                    if (attribute is EntityAdapterProperty)
                                        this.EntityAdapterProperties.Remove((EntityAdapterProperty)attribute);
                                    else if (attribute is EntityAdapterFormula)
                                        this.EntityAdapterFormulas.Remove((EntityAdapterFormula)attribute);
                                    else if (attribute is EntityAdapterPublicationProperty)
                                        this.EntityAdapterPublicationProperties.Remove((EntityAdapterPublicationProperty)attribute);
                                }
                                transaction.Commit();
                            }
                        }
                    }
                }

            }
        }

        public void UpdateLocalEntityInfo()
        {
            if (this.LocalEntityAdapter != null)
            {
                this.PrimaryEntity = string.Empty;
                this.SecondaryEntities = string.Empty;
                this.EntitySets = string.Empty;
                this.EntityRelations = string.Empty;
                this.IsReadOnly = true;
            }
        }

        public void UpdateBaseClassInfo()
        {
            EntityAdapter baseEntity = this.BaseEntityAdapter;
            if (baseEntity != null)
            {
                if (this.PrimaryEntity != baseEntity.PrimaryEntity || this.QueryReturnType != baseEntity.QueryReturnType || this.IsReadOnly != baseEntity.IsReadOnly ||
                    this.ReferenceRelations != baseEntity.ReferenceRelations || this.RequeryDetailsAfterSave != baseEntity.RequeryDetailsAfterSave ||
                    this.SizeGridConfigurations != baseEntity.SizeGridConfigurations || this.CubeName != baseEntity.CubeName || this.IsPOCO != baseEntity.IsPOCO)
                {
                    using (Transaction transaction =
                      this.Store.TransactionManager.BeginTransaction("Adjust class base info."))
                    {
                        this.PrimaryEntity = baseEntity.PrimaryEntity;
                        this.QueryReturnType = baseEntity.QueryReturnType;
                        this.IsReadOnly = baseEntity.IsReadOnly;
                        this.ReferenceRelations = baseEntity.ReferenceRelations;
                        this.RequeryDetailsAfterSave = baseEntity.RequeryDetailsAfterSave;
                        this.SizeGridConfigurations = baseEntity.SizeGridConfigurations;
                        this.CubeName = baseEntity.CubeName;
                        this.IsPOCO = baseEntity.IsPOCO;

                        transaction.Commit();
                    }
                }
            }


        }

        public void ApplyPublication()
        {
            this.EntityAdapterPublicationProperties.Clear();

            //Verify external subscriptions
            foreach (var pub in this.EntityAdapterDesignerRoot.Subscriptions)
                ApplyPublisher(pub.Publisher);

            //Verify auto-subscription
            this.EntityAdapterDesignerRoot.VerifyPublisherAutoReference();
            if (this.EntityAdapterDesignerRoot.PublisherAutoReference != null)
                ApplyPublisher(this.EntityAdapterDesignerRoot.PublisherAutoReference);
        }

        public string GetAliasList(string alias)
        {
            string result = String.Empty;

            if (this.EntityAdapterRepresentation == null)
            {
                foreach (var element in GetEdmEntities(alias).OrderBy(e => e.Key.Length))
                {
                    result += (result.IsNullOrEmpty() ? "" : ", ") + element.Value;
                }
            }
            else
            {
                Action<EntityAdapterRepresentation> action = null;

                action = (rep) =>
                {
                    result += (result.IsNullOrEmpty() ? "" : ", ") + rep.Name;

                    rep.SourceEntityAdapterRepresentations.ForEach(e => action(e));
                };

                action(this.EntityAdapterRepresentation);

            }

            return result;
        }

        public bool HasEnabledMedias()
        {
            if (!this.PrimaryEntity.IsNullOrEmpty())
            {
                var entity = this.GetTopParent();
                var defUI = entity.EntityAdapterUserInterfaces.FirstOrDefault(e => e.IsDefault);
                if (defUI != null)
                {
                    if (!defUI.LayoutContent.IsNullOrEmpty())
                    {
                        var layout = defUI.LayoutDefinition;
                        if (layout != null)
                            return layout.EnableMedias && (this.GetAllInheritanceProperties().Count(e => (e.Datatype.ToLower().Contains("guid") || e.Datatype.ToLower().InList("int", "int32", "system.int32", "long", "int64", "system.int64")) && this.IsPrimaryKey(e)) == 1);
                    }
                }
            }
            return false;
        }

        public string GetHierarchyRepresentationEntitySearch(bool byEntitySearch, string indent)
        {
            string result = String.Empty;

            if (byEntitySearch && this.EntityAdapterRepresentation != null)
            {
                Action<EntityAdapter> hierarchySearchAction = null;
                hierarchySearchAction = (entity) =>
                {
                    if (entity.EntityAdapterRepresentation != null)
                    {
                        result += "\r\n " + indent + "repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,\"" + entity.Name + "\", \"" + entity.EntityAdapterRepresentation.TargetEntityAdapterName + "\", 0" + entity.GetFieldListByRepresentation(entity.EntityAdapterRepresentation.Name) + ");";
                    }
                    entity.SourceEntityAdapters.ForEach(e => hierarchySearchAction(e));
                };
                result += "\r\n " + indent + "string repSerializedEntitySearch = serializedEntitySearch;";
                hierarchySearchAction(this);

            }

            return result;
        }

        public bool HasNavigationFromParent()
        {
            return this.TargetEntityAdapter != null && !this.TargetEntityAdapter.GetDetailRelationBySetName(this).IsNullOrEmpty();
        }

        public string GetEntitiesSelectionForLinq(bool isDbContext, string alias, bool byEntitySearch, string indent, ref string letFilter, bool isDetail = false, string parentAlias = "")
        {
            string result = String.Empty;

            if (this.EntityAdapterRepresentation == null)
            {
                string businessFilter = "";
                bool hasMainBusinessFilter = this.EntityAdapterDesignerRoot.HasMainBusinessFilter(this.PrimaryEntity);
                foreach (var element in GetEdmEntities(alias).OrderBy(e => e.Key.Length))
                {
                    if (element.Key != this.PrimaryEntity)
                    {
                        result += "\r\n " + indent + " let " + element.Value + " = " + alias + "." + ("#" + element.Key).Right("#" + PrimaryEntity + ".");

                        //Check business filter
                        if (!hasMainBusinessFilter)
                        {
                            string bFilterPart = GetBusinessFilterByRelation(element.Value, isDbContext, element.Key);
                            if (!bFilterPart.IsNullOrEmpty())
                            {
                                businessFilter += (businessFilter.IsNullOrEmpty() ? "" : " && ") + bFilterPart;
                                hasMainBusinessFilter = true;
                            }
                        }
                    }
                }

                if (!businessFilter.IsNullOrEmpty())
                {
                    letFilter += (letFilter.IsNullOrEmpty() ? "" : " && ") + "(" + businessFilter + ")";
                }

                string denormLet = this.GetDenormalizedLet(alias, indent, isDbContext);
                if (!denormLet.IsNullOrEmpty())
                    result += denormLet;

                if (parentAlias.IsNullOrEmpty() || this.TargetEntityAdapter == null)
                    return "from " + alias + " in this." + (isDbContext ? "DbContext" : "ObjectContext") + "." + (this.PrimaryEntityBase.IsNullOrEmpty() || isDbContext ? this.PrimaryEntity : this.PrimaryEntityBase + ".OfType<" + this.PrimaryEntity + ">()") + (byEntitySearch ? ".Where" + (!this.PrimaryEntityBase.IsNullOrEmpty() && isDbContext ? "<" + this.PrimaryEntityBase + ">" : String.Empty) + "(dynQuery, parameters.ToArray())" : "") + result;
                else
                    return "from " + alias + " in " + parentAlias + "." + this.TargetEntityAdapter.GetDetailRelationBySetName(this) + result;
            }
            else
            {
                var domainServices = GetRepresentationDomainServices();
                bool isIEnumerableType = this.QueryReturnType == EntityQueryReturnType.IEnumerable || this.HasAnyRepresentationAsEnumerableType();
                Action<EntityAdapterRepresentation> action = null;

                int joinReference = 0;
                action = (rep) =>
                {
                    bool isLeftJoin;
                    var parentLink = EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation.GetLinkToTargetEntityAdapterRepresentation(rep);
                    joinReference++;
                    isLeftJoin = parentLink.JoinType == EntityAdapterJoinType.LeftJoin;
                    result += "\r\n " + indent + "join " + rep.Name + (isLeftJoin ? "LF" : String.Empty) + " in " + domainServices[rep.TargetNameSpace + "#" + rep.TargetEdmName] + ".Get" + rep.TargetEntityAdapterName + (byEntitySearch ? "ByEntitySearch" : String.Empty) + "NoAssociations(" + (byEntitySearch ? "EntitySearch.FilterExpressionFields(serializedEntitySearch,\"" + this.Name + "\", \"" + rep.TargetEntityAdapterName + "\", " + joinReference.ToString() + this.GetFieldListByRepresentation(rep.Name) + ")" : String.Empty) + ")" + (rep.Filter.IsNullOrEmpty() ? String.Empty : ".Where(rp => " + rep.Filter.Replace("[ThisRef]", "rp") + ")") + (isIEnumerableType ? ".ToArray()" : String.Empty) + " on " + this.GetJoinExpression(parentLink.TargetProperties, parentLink.SourceProperties, rep.TargetEntityAdapterRepresentation.Name, rep.Name + (isLeftJoin ? "LF" : String.Empty)) + (isLeftJoin ? " into " + rep.Name + "Tmp" : String.Empty);

                    if (isLeftJoin)
                        result += "\r\n " + indent + "from " + rep.Name + " in " + rep.Name + "Tmp.DefaultIfEmpty()";

                    rep.SourceEntityAdapterRepresentations.ForEach(e => action(e));
                };

                this.EntityAdapterRepresentation.SourceEntityAdapterRepresentations.ForEach(e => action(e));

                return "\r\n " + indent + "from " + this.EntityAdapterRepresentation.Name + " in " + domainServices[this.EntityAdapterRepresentation.TargetNameSpace + "#" + this.EntityAdapterRepresentation.TargetEdmName] + ".Get" + this.EntityAdapterRepresentation.TargetEntityAdapterName + (byEntitySearch ? "ByEntitySearch" : String.Empty) + "NoAssociations(" + (byEntitySearch ? "repSerializedEntitySearch" : String.Empty) + ")" + (this.EntityAdapterRepresentation.Filter.IsNullOrEmpty() ? String.Empty : ".Where(rp => " + this.EntityAdapterRepresentation.Filter.Replace("[ThisRef]", "rp") + ")") + (isIEnumerableType ? ".ToArray()" : String.Empty) + result;
            }
        }

        public string GetDetailRelationBySetName(EntityAdapter detail)
        {
            string setName = detail.PrimaryEntity;
            string listName = string.Empty;
            EntityAdapter entity = this;

            if (!entity.DetailRelationsSuggestion.IsNullOrEmpty())
                listName = ("#" + entity.DetailRelationsSuggestion).Extract("#" + detail.Name + "(", ")");

            if (listName.IsNullOrEmpty())
                listName = ("#" + entity.DetailRelations).Extract("#" + setName + "(", ")");

            while (listName.IsNullOrEmpty() && entity.BaseEntityAdapter != null)
            {
                entity = entity.BaseEntityAdapter;
                if (!entity.DetailRelationsSuggestion.IsNullOrEmpty())
                    listName = ("#" + entity.DetailRelationsSuggestion).Extract("#" + detail.Name + "(", ")");

                if (listName.IsNullOrEmpty())
                    listName = ("#" + entity.DetailRelations).Extract("#" + setName + "(", ")");
            }

            return listName;
        }

        public bool HasMainTenanceForEntityRepresentations()
        {
            return (this.IsModelView && this.GetCurrentDataModel() == null && !this.IsReadOnly && !this.ModelViewDbSets.IsNullOrEmpty()) || (this.EntityAdapterRepresentation != null && !this.IsReadOnly && this.CreateCRUD && GetRepresentationDomainServices(true).Count > 0);
        }

        public List<EntityAdapter> GetSourceEntityAdapters()
        {
            return this.SourceEntityAdapters.OrderBy(e => e.Name).ToList();
        }

        public string GetCollectionsDefinition(string indent, bool isPoco)
        {
            string result = String.Empty, dataType;
            List<EntityCollectionReferencesEntityOwners> links = EntityCollectionReferencesEntityOwners.GetLinksToCollectionEntities(this).OrderBy(e => e.Name).ToList();

            if (links.Count > 0)
            {
                result += "\r\n ";
                result += "\r\n " + indent + "//Entity Collections";
                foreach (EntityCollectionReferencesEntityOwners link in links)
                {
                    dataType = link.DataType.Replace("<T>", "<" + link.SourceEntityAdapter.Name + ">").Replace("T[]", link.SourceEntityAdapter.Name + "[]");
                    result += "\r\n ";
                    result += "\r\n " + indent + "private " + dataType + " _" + link.Name + ";";
                    result += "\r\n " + indent + "[DataMember(Name = \"" + (link.DataMemberName.IsNullOrEmpty() ? link.Name : link.DataMemberName) + "\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder((link.DataMemberName.IsNullOrEmpty() ? link.Name : link.DataMemberName))) + ")]";
                    result += "\r\n " + indent + "public " + dataType + " " + link.Name;
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "      get {";

                    if (link.CreateEmptyInstance)
                    {
                        result += "\r\n " + indent + "              if (_" + link.Name + " == null)";
                        result += "\r\n " + indent + "                  _" + link.Name + " = new " + (link.DataType == "IEnumerable<T>" ? "List<" + link.SourceEntityAdapter.Name + ">()" : dataType + (link.DataType == "T[]" ? " {}" : "()")) + ";";
                    }

                    result += "\r\n " + indent + "              return _" + link.Name + ";";
                    result += "\r\n " + indent + "          } ";

                    //link

                    if (isPoco)
                        result += "\r\n " + indent + "      set { _" + link.Name + " = value;}";
                    else
                    {
                        result += "\r\n " + indent + "      set";
                        result += "\r\n " + indent + "      {";
                        result += "\r\n " + indent + "          if (_" + link.Name + " != value)";
                        result += "\r\n " + indent + "          {";
                        result += "\r\n " + indent + "              _" + link.Name + " = value;";
                        result += "\r\n " + indent + "              this.RaisePropertyChanged(\"" + link.Name + "\");";
                        result += "\r\n " + indent + "          }";
                        result += "\r\n " + indent + "      }";
                    }
                    result += "\r\n " + indent + "}";
                }
            }

            return result;
        }

        public string GetFreeDetailsDefinition(string indent, bool isPoco)
        {
            string result = String.Empty, dataType;

            if (this.SourceEntityAdapters.Count > 0)
            {
                result += "\r\n ";
                result += "\r\n " + indent + "//Detail Collections";
                foreach (EntityAdapter entity in this.SourceEntityAdapters.OrderBy(e => e.Name))
                {
                    dataType = this.DetailsCollectionType.Replace("<T>", "<" + entity.Name + ">").Replace("T[]", entity.Name + "[]");
                    result += "\r\n ";
                    result += "\r\n " + indent + "private " + dataType + " _" + entity.Name + "List;";
                    result += "\r\n " + indent + "[DataMember(Name = \"" + entity.GetAssociationDataMemberName() + "\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder(entity.GetAssociationDataMemberName())) + ")]";
                    result += "\r\n " + indent + "public " + dataType + " " + entity.Name + "List";
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "      get { return _" + entity.Name + "List;}";
                    if (isPoco)
                        result += "\r\n " + indent + "      set { _" + entity.Name + "List = value;}";
                    else
                    {
                        result += "\r\n " + indent + "      set";
                        result += "\r\n " + indent + "      {";
                        result += "\r\n " + indent + "          if (_" + entity.Name + "List != value)";
                        result += "\r\n " + indent + "          {";
                        result += "\r\n " + indent + "              _" + entity.Name + "List = value;";
                        result += "\r\n " + indent + "              this.RaisePropertyChanged(\"" + entity.Name + "\");";
                        result += "\r\n " + indent + "          }";
                        result += "\r\n " + indent + "      }";
                    }
                    result += "\r\n " + indent + "}";
                }
            }

            return result;
        }

        public string GetInstancesDefinition(string indent, bool isPoco)
        {
            string result = String.Empty;

            List<EntityInstanceReferencesEntityOwners> links = EntityInstanceReferencesEntityOwners.GetLinksToInstanceEntities(this).OrderBy(e => e.Name).ToList();

            if (links.Count > 0)
            {
                result += "\r\n ";
                result += "\r\n " + indent + "//Entity Instances";
                foreach (EntityInstanceReferencesEntityOwners link in links)
                {
                    result += "\r\n ";
                    result += "\r\n " + indent + "private " + link.SourceEntityAdapter.Name + " _" + link.Name + ";";
                    result += "\r\n " + indent + "[DataMember(Name = \"" + (link.DataMemberName.IsNullOrEmpty() ? link.Name : link.DataMemberName) + "\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder((link.DataMemberName.IsNullOrEmpty() ? link.Name : link.DataMemberName))) + ")]";
                    result += "\r\n " + indent + "public " + link.SourceEntityAdapter.Name + " " + link.Name;
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "      get { return _" + link.Name + ";}";
                    if (isPoco)
                        result += "\r\n " + indent + "      set { _" + link.Name + " = value;}";
                    else
                    {
                        result += "\r\n " + indent + "      set";
                        result += "\r\n " + indent + "      {";
                        result += "\r\n " + indent + "          if (_" + link.Name + " != value)";
                        result += "\r\n " + indent + "          {";
                        result += "\r\n " + indent + "              _" + link.Name + " = value;";
                        result += "\r\n " + indent + "              this.RaisePropertyChanged(\"" + link.Name + "\");";
                        result += "\r\n " + indent + "          }";
                        result += "\r\n " + indent + "      }";
                    }
                    result += "\r\n " + indent + "}";
                }
            }

            return result;
        }

        public string GenerateMainTenanceForEntityRepresentations(string indent)
        {
            string result = String.Empty;
            bool isModelViewForBV = (this.IsModelView && this.GetCurrentDataModel() == null);

            if (this.EntityAdapterRepresentation != null || isModelViewForBV)
            {
                var domainServices = GetRepresentationDomainServices(true);
                if (domainServices.Count > 0)
                {
                    result += "\r\n " + indent + "//Save All Representations Of Entity " + this.Name;
                    result += "\r\n " + indent + "[Ignore]";
                    result += "\r\n " + indent + "private void SaveBufferRepresentationsOf" + this.Name + "(List<EntityChange> entityChanges)";
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "  foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is " + this.Name + " && e.Entity.GetType().Name == \"" + this.Name + "\"))";
                    result += "\r\n " + indent + "  {";
                    result += "\r\n " + indent + "      " + this.Name + " entity = (" + this.Name + ")entry.Entity;";
                    result += "\r\n " + indent + "      entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));";
                    result += "\r\n " + indent + "  }";
                    result += "\r\n " + indent + "}";

                    result += "\r\n ";
                    result += "\r\n " + indent + "//Get Representation Of " + this.Name;
                    result += "\r\n " + indent + "[Ignore]";
                    result += "\r\n " + indent + "private List<EntityChange> GetRepresentations(" + this.Name + " entity, " + this.Name + " original, ChangeOperation operation)";
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "      List<EntityChange> result = new List<EntityChange>();";
                    result += "\r\n " + indent + "      switch (operation)";
                    result += "\r\n " + indent + "      {";

                    result += "\r\n " + indent + "          case ChangeOperation.None:";
                    result += GetCustomChangesForEntityRepresentations(domainServices, indent + "              ", "ChangeOperation.None", "none");
                    result += "\r\n " + indent + "              break;";

                    result += "\r\n " + indent + "          case ChangeOperation.Delete:";
                    result += GetCustomChangesForEntityRepresentations(domainServices, indent + "              ", "ChangeOperation.Delete", "delete");
                    result += "\r\n " + indent + "              break;";

                    result += "\r\n " + indent + "          case ChangeOperation.Insert:";
                    result += GetCustomChangesForEntityRepresentations(domainServices, indent + "              ", "ChangeOperation.Insert", "insert");
                    result += "\r\n " + indent + "              break;";

                    result += "\r\n " + indent + "          case ChangeOperation.Update:";
                    result += GetCustomChangesForEntityRepresentations(domainServices, indent + "              ", "ChangeOperation.Update", "update");
                    result += "\r\n " + indent + "              break;";

                    result += "\r\n " + indent + "          default:";
                    result += "\r\n " + indent + "              break;";

                    result += "\r\n " + indent + "      }";
                    result += "\r\n " + indent + "      return result;";
                    result += "\r\n " + indent + "}";
                }
            }

            return result;
        }

        private string GetCustomChangesForEntityRepresentations(Dictionary<string, string> domainServices, string indent, string customOp, string prefix)
        {
            string result = String.Empty;

            Action<RepresentationStructure> action = null;
            action = (rep) =>
            {
                if (rep.IsPublisherUpdatable && !rep.IsReadOnly)
                {
                    var attributes = this.GetAllInheritanceAttributes().Where(e => e.DataRelationKey.Left("#") == rep.Name).ToArray();
                    string entityName = prefix + rep.TargetEntityAdapterName;
                    result += "\r\n " + indent + "//" + customOp.Right(".") + " " + rep.TargetEntityAdapterName;
                    result += "\r\n " + indent + rep.TargetNameSpace + "." + rep.TargetEntityAdapterName + " " + entityName + " = new " + rep.TargetNameSpace + "." + rep.TargetEntityAdapterName + "() {";
                    for (int idx = 0; idx < attributes.Length; idx++)
                    {
                        result += "\r\n " + indent + attributes[idx].DataRelationKey.Right(".") + " = entity." + attributes[idx].Name + (idx == (attributes.Length - 1) ? "" : ",");
                    }
                    result += "\r\n " + indent + "};";

                    if (customOp == "ChangeOperation.Update")
                    {
                        result += "\r\n " + indent + "//Original Definition";
                        result += "\r\n " + indent + rep.TargetNameSpace + "." + rep.TargetEntityAdapterName + " " + entityName + "Original = (original == null ? null : new " + rep.TargetNameSpace + "." + rep.TargetEntityAdapterName + "() {";
                        for (int idx = 0; idx < attributes.Length; idx++)
                        {
                            result += "\r\n " + indent + attributes[idx].DataRelationKey.Right(".") + " = original." + attributes[idx].Name + (idx == (attributes.Length - 1) ? "" : ",");
                        }
                        result += "\r\n " + indent + "});";
                    }

                    result += "\r\n " + indent + "result.Add(new EntityChange() { Entity = " + entityName + ", Original = " + (customOp == "ChangeOperation.Update" ? entityName + "Original" : (customOp == "ChangeOperation.None" ? entityName : "null")) + ", Operation = " + (customOp == "ChangeOperation.None" ? "ChangeOperation.Update" : customOp) + ", Representation = " + (customOp == "ChangeOperation.Insert" ? "entity" : "null") + ", Mark = \"" + (domainServices.Count > 0 ? domainServices[rep.TargetNameSpace + "#" + rep.TargetEdmName] : "") + "\" });";

                    //Refresh Keys
                    if (customOp == "ChangeOperation.Insert")
                    {
                        for (int idx = 0; idx < attributes.Length; idx++)
                        {
                            if (attributes[idx] is EntityAdapterProperty && ((EntityAdapterProperty)attributes[idx]).IsAutomaticSequency)
                            {
                                result += "\r\n " + indent + "foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add(\"" + attributes[idx].Name + "\", \"" + attributes[idx].DataRelationKey.Right(".") + "\");";
                            }
                        }
                    }
                }

            };


            List<RepresentationStructure> targets = new List<RepresentationStructure>();

            if (this.EntityAdapterRepresentation != null)
            {
                Action<EntityAdapterRepresentation> actionTransform = null;
                actionTransform = (repOriginal) =>
                {
                    targets.Add(new RepresentationStructure()
                    {
                        IsPublisherUpdatable = repOriginal.IsPublisherUpdatable,
                        IsReadOnly = repOriginal.IsReadOnly,
                        Name = repOriginal.Name,
                        TargetEntityAdapterName = repOriginal.TargetEntityAdapterName,
                        TargetNameSpace = repOriginal.TargetNameSpace,
                        TargetEdmName = repOriginal.TargetEdmName
                    });

                    repOriginal.SourceEntityAdapterRepresentations.ForEach(e => actionTransform(e));
                };
                actionTransform(this.EntityAdapterRepresentation);
            }
            else if (this.IsModelView && !this.ModelViewDbSets.IsNullOrEmpty())
            {
                foreach (var dbSet in this.ModelViewDbSets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string contextType = dbSet.Left("#");
                    var targetEdmName = contextType.Right("|");
                    contextType = contextType.Left("|");
                    string entityName = dbSet.Right("#");
                    var targetNameSpace = contextType.Left(contextType.Length - contextType.Right(".").Length - 1);

                    targets.Add(new RepresentationStructure()
                    {
                        IsPublisherUpdatable = true,
                        IsReadOnly = this.IsReadOnly,
                        Name = entityName,
                        TargetEntityAdapterName = entityName,
                        TargetNameSpace = targetNameSpace,
                        TargetEdmName = targetEdmName
                    });
                }
            }

            //Generate result
            targets.ForEach(e => action(e));

            return result;
        }

        public List<CommonExtendedFilterProperty> GetCommonExtendedFilters()
        {
            List<CommonExtendedFilterProperty> result = new List<CommonExtendedFilterProperty>();

            foreach (EntityAdapterExtendedFilter eFilter in this.EntityAdapterExtendedFilters)
            {
                foreach (EntityAdapterPropertyExtendedFilter efProp in eFilter.EntityAdapterPropertyExtendedFilters.Where(e => e.IsEnabled))
                {
                    result.Add(new CommonExtendedFilterProperty()
                    {
                        Name = efProp.EdmKey,
                        DisplayName = efProp.DisplayName,
                        DataType = efProp.DataType,
                        EdmKey = efProp.EdmKey
                    });
                }
            }

            //Add Representations
            result.AddRange(this.GetExtendedFiltersByRepresentation());

            return result;
        }

        private List<CommonExtendedFilterProperty> GetExtendedFiltersByRepresentation()
        {
            List<CommonExtendedFilterProperty> result = new List<CommonExtendedFilterProperty>();

            if (this.EntityAdapterRepresentation != null)
            {
                List<string> usedFields = new List<string>();

                foreach (string field in this.GetAllInheritanceAttributes().Where(e => e.DataRelationKey.Left("#") == this.EntityAdapterRepresentation.Name).Select(e => e.DataRelationKey.Right(".")))
                {
                    if (!usedFields.Contains(field))
                        usedFields.Add(field);
                }


                Action<EntityAdapterRepresentation> action = null;
                action = (currentEntityPresentation) =>
                {
                    if (currentEntityPresentation.EnableExtendedFilter)
                    {
                        var pubEntity = currentEntityPresentation.EntityAdapterDesignerRoot.GetPublishedEntityByRef(currentEntityPresentation.BusinessObject, currentEntityPresentation.TargetNameSpace, currentEntityPresentation.TargetEntityAdapterName);
                        if (pubEntity != null)
                        {
                            foreach (CustomizedCode.PublicationProperty member in pubEntity.Properties)
                            {
                                if (!usedFields.Contains(member.Name))
                                {
                                    usedFields.Add(member.Name);
                                    result.Add(new CommonExtendedFilterProperty()
                                    {
                                        Name = member.Name,
                                        DisplayName = member.DisplayName,
                                        DataType = member.DataType,
                                        EdmKey = MacroEngineHelper.ReplaceMacrosEntitySql(member.EdmKey, this)
                                    });
                                }
                            }
                        }
                    }

                    currentEntityPresentation.SourceEntityAdapterRepresentations.ForEach(e => action(e));
                };

                action(this.EntityAdapterRepresentation);

            }

            return result;
        }

        private string GetFieldListByRepresentation(string representationName)
        {
            string result = String.Empty;
            Dictionary<string, string> fields = new Dictionary<string, string>();

            foreach (var attrib in this.GetAllInheritanceAttributes().Where(e => e.DataRelationKey.Left("#") == representationName))
            {
                if (!fields.ContainsKey(attrib.Name))
                    fields.Add(attrib.Name, attrib.DataRelationKey.Right("."));
            }

            EntityAdapterRepresentation currentEntityRepresentation = GetRepresentationByName(representationName);
            if (currentEntityRepresentation != null)
            {
                //Get invalid fields
                Dictionary<string, string> invalidFields = new Dictionary<string, string>();
                this.AddFieldsFromAntecedentRepresentations(currentEntityRepresentation, invalidFields);

                var pubEntity = currentEntityRepresentation.EntityAdapterDesignerRoot.GetPublishedEntityByRef(currentEntityRepresentation.BusinessObject, currentEntityRepresentation.TargetNameSpace, currentEntityRepresentation.TargetEntityAdapterName);
                if (pubEntity != null)
                {
                    foreach (CustomizedCode.PublicationProperty member in pubEntity.Properties)
                    {
                        if (!fields.ContainsKey(member.Name) && !invalidFields.ContainsKey(member.Name) && !fields.Values.Contains(member.Name) && !invalidFields.Values.Contains(member.Name))
                            fields.Add(member.Name, member.Name);
                    }
                }
            }

            //Get field list
            foreach (var field in fields)
                result += (result.IsNullOrEmpty() ? String.Empty : ",") + ("\"" + field.Key + "#" + field.Value + "\"");

            return (result.IsNullOrEmpty() ? string.Empty : ", ") + result;
        }

        private void AddFieldsFromAntecedentRepresentations(EntityAdapterRepresentation currentEntityRepresentation, Dictionary<string, string> fields)
        {
            if (currentEntityRepresentation != null)
            {
                EntityAdapterRepresentation antecedentRepresentation = currentEntityRepresentation.TargetEntityAdapterRepresentation;
                while (antecedentRepresentation != null)
                {
                    var pubEntity = antecedentRepresentation.EntityAdapterDesignerRoot.GetPublishedEntityByRef(antecedentRepresentation.BusinessObject, antecedentRepresentation.TargetNameSpace, antecedentRepresentation.TargetEntityAdapterName);
                    if (pubEntity != null)
                    {
                        foreach (CustomizedCode.PublicationProperty member in pubEntity.Properties)
                        {
                            if (!fields.ContainsKey(member.Name))
                                fields.Add(member.Name, member.Name);
                        }
                    }

                    antecedentRepresentation = antecedentRepresentation.TargetEntityAdapterRepresentation;
                }
            }
        }

        private EntityAdapterRepresentation GetRepresentationByName(string name)
        {
            EntityAdapterRepresentation result = null;

            if (this.EntityAdapterRepresentation != null)
            {
                Action<EntityAdapterRepresentation> action = null;

                action = (rep) =>
                {
                    if (result == null)
                    {
                        if (rep.Name == name)
                            result = rep;
                        else rep.SourceEntityAdapterRepresentations.ForEach(e => action(e));
                    }
                };

                action(this.EntityAdapterRepresentation);
            }

            return result;
        }

        private string GetJoinExpression(string leftProperties, string rightProperties, string leftAlias, string rightAlias)
        {
            string leftResult = String.Empty, rightResult = String.Empty, propAlias = "Prop";
            string[] leftElements = leftProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] rightElements = rightProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (leftElements.Length == rightElements.Length)
            {
                if (leftElements.Length > 1)
                {
                    for (int idx = 0; idx < leftElements.Length; idx++)
                    {
                        leftResult += (leftResult.IsNullOrEmpty() ? String.Empty : ", ") + propAlias + idx.ToString() + " = " + leftAlias + "." + leftElements[idx];
                        rightResult += (rightResult.IsNullOrEmpty() ? String.Empty : ", ") + propAlias + idx.ToString() + " = " + rightAlias + "." + rightElements[idx];
                    }

                    leftResult = "new {" + leftResult + "}";
                    rightResult = "new {" + rightResult + "}";
                }
                else if (leftElements.Length == 1)
                {
                    leftResult = leftAlias + "." + leftElements[0];
                    rightResult = rightAlias + "." + rightElements[0];
                }
            }

            return leftResult + " equals " + rightResult;
        }

        public string GetRepresentationDomainServiceInstances(string indent)
        {
            return GetRepresentationDomainServiceInstances(indent, false);
        }

        public string GetRepresentationDomainServiceInstances(string indent, bool justUpdatable, string contentTester = "")
        {
            Dictionary<string, string> availableEDMs = new Dictionary<string, string>();
            var edm = this.EntityAdapterDesignerRoot.EntityDataModels.FirstOrDefault();
            if (edm != null)
                availableEDMs.Add(edm.TargetNamespace + "." + edm.Name, "this.GetEDM()");
            string result = String.Empty, domainService, edmName, commandLine;
            foreach (var ds in GetRepresentationDomainServices(justUpdatable))
            {
                domainService = ds.Key.Left("#");
                edmName = ds.Key.Right("#");
                commandLine = "\r\n " + indent + domainService + "." + domainService.Right(".") + "DomainService " + ds.Value + " = new " + domainService + "." + domainService.Right(".") + "DomainService(" + (!edmName.IsNullOrEmpty() && availableEDMs.ContainsKey(edmName) ? availableEDMs[edmName] + ", " : "") + "this.Headers) { IsSecure = this.IsSecure };";
                if (contentTester.IsNullOrEmpty() || !contentTester.Contains(commandLine))
                    result += commandLine;
                if (!edmName.IsNullOrEmpty() && !availableEDMs.ContainsKey(edmName))
                {
                    availableEDMs.Add(edmName, ds.Value + ".GetEDM()");
                }
            }
            return result;
        }

        public Dictionary<EntityAdapterProperty, List<string>> GetDenormalizedStructures()
        {
            List<string> innerList = new List<string>();
            Dictionary<EntityAdapterProperty, List<string>> result = new Dictionary<EntityAdapterProperty, List<string>>();

            foreach (EntityAdapterProperty denormalizedProp in this.EntityAdapterProperties.Where(e => !e.DenormalizedDataInfo.IsNullOrEmpty() && !e.EdmKey.IsNullOrEmpty()))
            {
                string startProp = denormalizedProp.DenormalizedDataInfo.Extract("[", "-"), endProp = denormalizedProp.DenormalizedDataInfo.Extract("-", "]");
                if (!startProp.IsNullOrEmpty() && startProp.IsNumeric() && !endProp.IsNullOrEmpty() && endProp.IsNumeric())
                {
                    for (int idx = int.Parse(startProp); idx < int.Parse(endProp); idx++)
                    {
                        innerList.Add(denormalizedProp.DenormalizedDataInfo.Replace("[" + startProp + "-" + endProp + "]", idx.ToString()));
                    }
                }
                else
                {
                    innerList.AddRange(denormalizedProp.DenormalizedDataInfo.Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }

                result.Add(denormalizedProp, innerList.ToList());
                innerList.Clear();
            }

            return result;
        }

        public string GetDenormalizedLet(string alias, string indent, bool isDbContext)
        {
            string result = String.Empty, denormalizedCommand, entityName, principalProperties, joinPrincipal, joinNormalized, propName, dataType, lastKeyAlias = String.Empty, normalizedAlias = String.Empty;

            foreach (var denormalizedInfo in GetDenormalizedStructures())
            {
                if (denormalizedInfo.Key != null)
                {
                    denormalizedCommand = String.Empty;
                    normalizedAlias = "normalizedEntity" + denormalizedInfo.Key.Name;
                    string macro = "(from dn in this." + (isDbContext ? "DbContext" : "ObjectContext") + ".{0} select new { {1}, Id = {2}, Value = dn.{3} })";
                    entityName = denormalizedInfo.Key.GetEdmEntityName();
                    dataType = denormalizedInfo.Key.Datatype.ToLower();
                    principalProperties = joinPrincipal = joinNormalized = String.Empty;

                    foreach (var keyProp in this.EntityAdapterProperties.Where(e => this.IsPrimaryKey(e)))
                    {
                        propName = keyProp.GetEdmPropertyName();
                        principalProperties += (principalProperties.IsNullOrEmpty() ? String.Empty : ", ") + this.ReplaceEdmPath(keyProp.GetEdmPath(), "dn");
                        joinPrincipal += (joinPrincipal.IsNullOrEmpty() ? String.Empty : ", ") + this.ReplaceEdmPath(keyProp.GetEdmPath(), alias);
                        joinNormalized += (joinNormalized.IsNullOrEmpty() ? String.Empty : ", ") + normalizedAlias + "." + propName;
                    }

                    for (int idx = 0; idx < denormalizedInfo.Value.Count; idx++)
                    {
                        denormalizedCommand += "\r\n " + indent + (denormalizedCommand.IsNullOrEmpty() ? String.Empty : ".Union") + macro.Replace("{0}", entityName).Replace("{1}", principalProperties).Replace("{2}", (idx + 1).ToString()).Replace("{3}", denormalizedInfo.Value[idx]);
                    }

                    if (!lastKeyAlias.IsNullOrEmpty())
                    {
                        joinPrincipal += ", " + lastKeyAlias + ".Id";
                        joinNormalized += ", " + normalizedAlias + ".Id";
                    }

                    if (!denormalizedCommand.IsNullOrEmpty())
                        denormalizedCommand = "\r\n " + indent + " join " + normalizedAlias + " in (" + denormalizedCommand + ")" + "\r\n " + indent + " on new { " + joinPrincipal + " } equals new { " + joinNormalized + " }";

                    lastKeyAlias = normalizedAlias;

                    result += denormalizedCommand;
                }
            }

            //Check Business for SKU
            if (this.BusinessExtension == BusinessExtensions.SKU && !result.IsNullOrEmpty() && !lastKeyAlias.IsNullOrEmpty())
            {
                result += "\r\n " + indent + " join skuBE in this." + (isDbContext ? "DbContext" : "ObjectContext") + ".PRD_SKU_CONVERSAO" + "\r\n " + indent + " on new { " + normalizedAlias + ".PRODUTO, " + normalizedAlias + ".COR_PRODUTO, ORDEM = " + normalizedAlias + ".Id } equals new { skuBE.PRODUTO, skuBE.COR_PRODUTO, skuBE.ORDEM }";
            }

            return result;
        }

        public bool HasDenormalizedLet()
        {
            return this.EntityAdapterProperties.Where(e => !e.DenormalizedDataInfo.IsNullOrEmpty() && !e.EdmKey.IsNullOrEmpty()).Count() > 0;
        }

        public string GetManualAuthorization(string type, string methodName, string indent)
        {
            string commandLine = String.Empty;

            if (this.EntityAdapterDesignerRoot.EnableAutomaticAuthorization)
            {
                commandLine = "\r\n " + indent + "//Check Authorization Manually";
                commandLine = "\r\n " + indent + "if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && " + (methodName.Contains(",") ? "(\"," + methodName + ",\").Contains(\",\" + this.ServiceContext.Operation.Name + \",\")" : "this.ServiceContext.Operation.Name == \"" + methodName + "\"") + ")))";
                commandLine += "\r\n " + indent + "{";
                commandLine += "\r\n " + indent + "     AuthorizationResult authorizationResult = (new " + this.Name + type + "CustomAuthorizationAutoAttribute()).Authorize(this);";
                commandLine += "\r\n " + indent + "     if (authorizationResult != AuthorizationResult.Allowed)";
                commandLine += "\r\n " + indent + "         throw new DomainException(authorizationResult.ErrorMessage);";
                commandLine += "\r\n " + indent + "     else";
                commandLine += "\r\n " + indent + "         this.IsSecure = true;";
                commandLine += "\r\n " + indent + "}";
            }

            return commandLine;
        }

        public string AdjustMasterFilter(string indent, string searchListName)
        {
            string body = "";
            var topEntity = this.GetTopParent();
            if (topEntity != this && topEntity.IsDashboardFilter)
            {
                body += "\r\n " + indent + "//Adjust EntityName for MasterFiltering";
                body += "\r\n " + indent + "var thisES = " + searchListName + ".FirstOrDefault(e => e.EntityName == \"" + this.Name + "\");";
                body += "\r\n " + indent + "if (thisES != null)";
                body += "\r\n " + indent + "{";
                body += "\r\n " + indent + "    foreach (var es in " + searchListName + ".Where(e => e.EntityName == \"" + topEntity.Name + "\").ToArray())";
                body += "\r\n " + indent + "    {";
                body += "\r\n " + indent + "      es.EntityName = thisES.EntityName;";
                body += "\r\n " + indent + "      es.SubQueryInfo = thisES.SubQueryInfo;";
                body += "\r\n " + indent + "      es.EdmEntityName = thisES.EdmEntityName;";
                body += "\r\n " + indent + "      es.EdmParentEntityName = thisES.EdmParentEntityName;";
                body += "\r\n " + indent + "      es.BaseEntityNames = thisES.BaseEntityNames;";
                body += "\r\n " + indent + "    }";
                body += "\r\n " + indent + "}";
                body += "\r\n ";
            }
            return body;
        }

        public string GetRepresentationDomainServiceCustomCommands(string indent, string contentTester = "")
        {
            string result = String.Empty, commandLine;
            foreach (var ds in GetRepresentationDomainServices(true))
            {
                commandLine = "\r\n " + indent + "foreach (var entityChange in entityChanges.Where(e => e.Mark == \"" + ds.Value + "\").ToList())";
                commandLine += "\r\n " + indent + "{";
                commandLine += "\r\n " + indent + "      " + ds.Value + ".AddCustomChanges(entityChange.Entity, entityChange.Original, operation);";
                commandLine += "\r\n " + indent + "      " + ds.Value + ".SaveCustomChanges();";
                if (this.EntityAdapterDesignerRoot.RefreshIdentityKeysAfterSave)
                    commandLine += "\r\n " + indent + "      if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();";
                commandLine += "\r\n " + indent + "}";
                if (contentTester.IsNullOrEmpty() || !contentTester.Contains(commandLine))
                    result += commandLine;
            }
            return result;
        }

        public string GetRepresentationDomainServiceSubmitCommands(string indent, string contentTester = "")
        {
            string result = String.Empty, commandLine;
            foreach (var ds in GetRepresentationDomainServices(true))
            {
                var varName = ds.Value + "Changes";
                commandLine = "\r\n " + indent + ds.Value + ".SubmitData(this.ServiceContext, " + varName + ");";
                if (contentTester.IsNullOrEmpty() || !contentTester.Contains(commandLine))
                {
                    result += "\r\n " + indent + "var " + varName + " = entityChanges.Where(e => e.Mark == \"" + ds.Value + "\").ToList();";
                    result += commandLine;
                    if (this.EntityAdapterDesignerRoot.RefreshIdentityKeysAfterSave)
                    {
                        result += "\r\n " + indent + "//Replace keys from source";
                        result += "\r\n " + indent + "foreach (var entityChange in " + varName + ") { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }";
                    }
                }
            }
            return result;
        }

        public void CreateSKU()
        {
            if (this.BaseEntityAdapter == null)
            {
                EntityAdapterProperty normalizedKey = this.EntityAdapterProperties.Where(e => e.Name == "IdSku").FirstOrDefault();
                if (normalizedKey == null)
                {
                    normalizedKey = this.EntityAdapterProperties.AddNew() as EntityAdapterProperty;
                    normalizedKey.Name = "IdSku";
                    normalizedKey.DenormalizedDataInfo = String.Empty;
                    normalizedKey.DisplayName = "SKU";
                }
                normalizedKey.IsEditable = false;
                normalizedKey.IsPK = true;
                normalizedKey.IsFK = false;
                normalizedKey.Datatype = "System.Int32";
                normalizedKey.DataFormatString = "N0";
                normalizedKey.IsCustomized = true;
                normalizedKey.EdmKey = "skuBE.ID_SKU";
            }
        }

        public void RemoveSKU()
        {
            if (this.BaseEntityAdapter == null)
            {
                EntityAdapterProperty normalizedKey = this.EntityAdapterProperties.Where(e => e.Name == "IdSku").FirstOrDefault();
                if (normalizedKey != null)
                    this.EntityAdapterProperties.Remove(normalizedKey);
            }
        }

        public bool HasAnyRepresentationAsEnumerableType()
        {
            if (this.EntityAdapterRepresentation != null)
            {
                //Check if has many EdmContexts
                if (this.GetRepresentationDomainServices().Keys.Select(e => e.Right("#")).Distinct().Count() > 1)
                    return true;

                bool hasAny = false;
                Action<EntityAdapterRepresentation> action = null;
                action = (rep) =>
                {
                    if (!hasAny)
                    {
                        if (!rep.IsIQueryable)
                            hasAny = true;
                        else
                            rep.SourceEntityAdapterRepresentations.ForEach(e => action(e));
                    }
                };

                action(this.EntityAdapterRepresentation);

                return hasAny;
            }

            return false;
        }

        private Dictionary<string, string> GetRepresentationDomainServices(bool justUpdatable = false)
        {
            bool isModelView = (justUpdatable && this.IsModelView && !this.IsReadOnly && !this.ModelViewDbSets.IsNullOrEmpty() && this.GetCurrentDataModel() == null);
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (this.EntityAdapterRepresentation != null || isModelView)
            {
                Dictionary<string, string> allDomainServices = this.EntityAdapterDesignerRoot.GetAllRepresentationDomainServices();
                Action<RepresentationStructure> action = null;
                action = (rep) =>
                {
                    if ((!justUpdatable || (rep.IsPublisherUpdatable && !rep.IsReadOnly)) && !result.ContainsKey(rep.TargetNameSpace + "#" + rep.TargetEdmName) && allDomainServices.ContainsKey(rep.TargetNameSpace + "#" + rep.TargetEdmName))
                    {
                        result.Add(rep.TargetNameSpace + "#" + rep.TargetEdmName, allDomainServices[rep.TargetNameSpace + "#" + rep.TargetEdmName]);
                    }
                };

                List<RepresentationStructure> targets = new List<RepresentationStructure>();

                if (this.EntityAdapterRepresentation != null)
                {
                    Action<EntityAdapterRepresentation> actionTransform = null;
                    actionTransform = (repOriginal) =>
                    {
                        targets.Add(new RepresentationStructure()
                        {
                            IsPublisherUpdatable = repOriginal.IsPublisherUpdatable,
                            IsReadOnly = repOriginal.IsReadOnly,
                            Name = repOriginal.Name,
                            TargetEntityAdapterName = repOriginal.TargetEntityAdapterName,
                            TargetNameSpace = repOriginal.TargetNameSpace,
                            TargetEdmName = repOriginal.TargetEdmName
                        });

                        repOriginal.SourceEntityAdapterRepresentations.ForEach(e => actionTransform(e));
                    };
                    actionTransform(this.EntityAdapterRepresentation);
                }
                else if (this.IsModelView && !this.ModelViewDbSets.IsNullOrEmpty())
                {
                    foreach (var dbSet in this.ModelViewDbSets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string contextType = dbSet.Left("#");
                        var targetEdmName = contextType.Right("|");
                        contextType = contextType.Left("|");
                        string entityName = dbSet.Right("#");
                        var targetNameSpace = contextType.Left(contextType.Length - contextType.Right(".").Length - 1);

                        targets.Add(new RepresentationStructure()
                        {
                            IsPublisherUpdatable = true,
                            IsReadOnly = this.IsReadOnly,
                            Name = entityName,
                            TargetEntityAdapterName = entityName,
                            TargetNameSpace = targetNameSpace,
                            TargetEdmName = targetEdmName
                        });
                    }
                }

                //Generate result
                targets.ForEach(e => action(e));
            }

            return result;
        }

        public List<EntityAdapter> GetCompleteHierarchy()
        {
            List<EntityAdapter> result = new List<EntityAdapter>();

            Action<EntityAdapter> action = null;
            action = (entity) =>
            {
                result.Add(entity);
                entity.SourceEntityAdapters.ForEach(e => action(e));
            };

            action(this);

            return result;
        }

        public string GetAllParentNames()
        {
            string parents = "";
            var parent = this.TargetEntityAdapter;
            while (parent != null)
            {
                parents += (parents == "" ? "" : ", ") + "\"" + parent.Name + "\"";
                parent = parent.TargetEntityAdapter;
            }
            return parents;
        }

        public string GetBindingPath()
        {
            if (this.TargetEntityAdapter == null)
                return "DataElement.DataView";
            else
            {
                string bindingPath = this.Name + "PagedList";
                var parent = this.TargetEntityAdapter;
                while (true)
                {
                    if (parent.TargetEntityAdapter == null)
                    {
                        bindingPath = "DataElement.DataView." + bindingPath;
                        break;
                    }
                    bindingPath = parent.Name + "PagedList" + "." + bindingPath;
                    parent = parent.TargetEntityAdapter;
                }

                return bindingPath;
            }
        }

        public Dictionary<string, string> GetEdmEntities(string alias)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string reference;
            int cntAlias = 0;
            dict.Add(this.PrimaryEntity, alias);


            foreach (var prop in this.GetAllInheritanceAttributes())
            {
                if (prop.IsCustomized)
                    continue;

                if (prop is EntityAdapterProperty)
                    reference = ((EntityAdapterProperty)prop).EdmKey;
                else if (prop is EntityAdapterPublicationProperty)
                    reference = ((EntityAdapterPublicationProperty)prop).EdmKey;
                else if (prop is EntityAdapterFormula && ((EntityAdapterFormula)prop).IsUpdatable)
                    reference = ((EntityAdapterFormula)prop).LinqDefinition;
                else continue;

                if (MacroEngineHelper.HasMacro(reference, this))
                    continue;

                reference = (reference + "#").Left("." + reference.Right(".") + "#");
                if (!reference.IsNullOrEmpty() && !dict.ContainsKey(reference))
                {
                    cntAlias++;
                    dict.Add(reference, alias + "Al" + cntAlias.ToString());
                }
            }

            foreach (var filter in this.GetAllInheritanceExtendedFilters())
            {
                var propFilter = filter.EntityAdapterPropertyExtendedFilters.FirstOrDefault();
                if (!propFilter.IsNull())
                {
                    reference = propFilter.EdmKey;
                    reference = (reference + "#").Left("." + reference.Right(".") + "#");
                    if (!reference.IsNullOrEmpty() && !dict.ContainsKey(reference))
                    {
                        cntAlias++;
                        dict.Add(reference, alias + "Al" + cntAlias.ToString());
                    }
                }
            }


            return dict;
        }


        public void AdjustLookupsInfoFromSubscription()
        {
            if (this.GetTopBaseClass().EntityAdapterRepresentation == null)
                return;

            var _designerRoot = this.EntityAdapterDesignerRoot;
            List<PublicationStructure> publishers = new List<PublicationStructure>();
            _designerRoot.VerifyPublisherAutoReference();
            if (_designerRoot.PublisherAutoReference != null)
                publishers.Add(_designerRoot.PublisherAutoReference);
            publishers.AddRange(_designerRoot.Subscriptions.Where(e => e.Publisher != null).Select(e => e.Publisher));

            foreach (var prop in this.GetAllInheritanceAttributes().Where(e => !e.LookUpSubscription.IsNullOrEmpty()).ToList())
            {
                string[] subscriptionParts = prop.DataRelationKey.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                if (subscriptionParts.Length == 3)
                {
                    var entity = publishers.Select(e => e.Entities.FirstOrDefault(p => p.Namespace == subscriptionParts[1] && p.Name == subscriptionParts[2].Left("."))).Where(e => e != null).FirstOrDefault();
                    if (entity != null)
                    {
                        var sourceProp = entity.Properties.FirstOrDefault(e => e.Name == subscriptionParts[2].Right("."));
                        if (sourceProp != null)
                            prop.LookUpSubscription = sourceProp.LookUpInfo;
                    }
                }
            }
        }

        public List<LookUpStruct> GetAllLookUpsInfo(bool byPropertyRelated)
        {
            return GetAllLookUpsInfo(true, byPropertyRelated);
        }

        public List<LookUpStruct> GetAllLookUpsInfo(bool loadProperties, bool byPropertyRelated)
        {
            return LookUpStruct.GetLookUpStructures(this.GetAllLookUpPropertiesInfo(byPropertyRelated), loadProperties);
        }

        public Dictionary<string, string> GetAllLookUpPropertiesInfo(bool byPropertyRelated)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            int newKey = 0;

            var baseEntity = this;
            while (baseEntity != null)
            {
                foreach (LookUpAdapter lookUp in baseEntity.LookUpAdapters)
                {
                    foreach (var element in lookUp.GetLookupInfoFromSource(byPropertyRelated))
                    {
                        if (byPropertyRelated)
                        {
                            if (!result.ContainsKey(element.Key))
                                result.Add(element.Key, element.Value);
                        }
                        else
                        {
                            if (!result.ContainsKey(element.Key))
                                result.Add(element.Key, element.Value);
                            else
                            {
                                newKey++;
                                result.Add("LookupKey" + newKey.ToString(), element.Value);
                            }
                        }
                    }
                }
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            if (this.EntityAdapterRepresentation != null || (this.IsModelView && this.GetCurrentDataModel() == null)) //Get by publication
            {
                foreach (var attribute in this.GetAllInheritanceAttributes().Where(e => !e.LookUpSubscription.IsNullOrEmpty()))
                {
                    if (byPropertyRelated)
                    {
                        if (!result.ContainsKey(attribute.Name))
                            result.Add(attribute.Name, attribute.LookUpSubscription);
                    }
                    else
                    {
                        if (!result.ContainsKey(attribute.Name))
                            result.Add(attribute.Name, attribute.LookUpSubscription);
                        else
                        {
                            newKey++;
                            result.Add("LookupKey" + newKey.ToString(), attribute.LookUpSubscription);
                        }
                    }
                }
            }

            return result;
        }

        public string ReplaceEdmPath(string edmPth, string alias, string baseAlias = "", bool byEntitySQL = false, bool isEdmAggregation = false)
        {
            string result = MacroEngineHelper.ReplaceMacros(edmPth, byEntitySQL ? MacroOutputType.EntitySQL : MacroOutputType.CSharp, this);
            if (isEdmAggregation)
            {
                //result = result.Replace(this.PrimaryEntity + ".", (!baseAlias.IsNullOrEmpty() ? baseAlias + "." : ""));

                result = System.Text.RegularExpressions.Regex.Replace(result, "(?<![a-zA-Z0-9_@])" + this.PrimaryEntity + "\\.", (!baseAlias.IsNullOrEmpty() ? baseAlias + "." : ""));
            }
            else
            {
                foreach (var element in GetEdmEntities(alias).OrderByDescending(e => e.Key.Length))
                {
                    //result = result.Replace(element.Key + ".", (!baseAlias.IsNullOrEmpty() ? baseAlias + "." : "") + element.Value + ".");
                    result = System.Text.RegularExpressions.Regex.Replace(result, "(?<![a-zA-Z0-9_@])" + element.Key + "\\.", (!baseAlias.IsNullOrEmpty() ? baseAlias + "." : "") + element.Value + ".");
                }
            }
            return result;
        }

        private void ApplyPublisher(Linx.EntityAdapterDesigner.CustomizedCode.PublicationStructure publisher)
        {
            bool isNullReference;
            string pubPkPropName, suffix;
            foreach (var entity in publisher.Entities)
            {
                isNullReference = false;
                suffix = String.Empty;
                foreach (var pk in entity.PrimaryKeys.Where(e => e.Name.Occurs(".") > 0))
                {
                    pubPkPropName = entity.Properties.Where(e => e.EdmKey == pk.Name).Select(e => e.Name).FirstOrDefault();
                    partsForCmp = pk.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var prop in this.EntityAdapterProperties.Where(e => !e.IsCustomized && e.IsFK && e.IsPK && e.EdmKey.Occurs(".") > 1))
                    {
                        parts = prop.EdmKey.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        entityName = this.GetEntityNameByRelation(parts[parts.Length - 2]);
                        if (entityName.IsNullOrEmpty())
                            entityName = parts[parts.Length - 2];

                        if ((prop.PublicationRelatedKey.IsNullOrEmpty() ? prop.Name : prop.PublicationRelatedKey) == pubPkPropName && entityName == partsForCmp[partsForCmp.Length - 2] && parts[parts.Length - 1] == partsForCmp[partsForCmp.Length - 1])
                        {
                            keyBase = pk.Name.Left("." + partsForCmp[partsForCmp.Length - 1]);
                            targetBase = prop.EdmKey.Left("." + parts[parts.Length - 1]);
                            keyName = prop.Name;
                            isNullReference = prop.IsNull;
                            if (!prop.PublicationSuffix.IsNullOrEmpty())
                                suffix = prop.PublicationSuffix;

                            //Create properties
                            EntityAdapterProperty entProperty;
                            foreach (var property in entity.Properties.Where(e => e.IsSuggestion && e.Name != keyName).OrderBy(e => e.Name))
                            {
                                edmKey = System.Text.RegularExpressions.Regex.Replace(property.EdmKey, "(?<![a-zA-Z0-9_@])" + keyBase + "\\.", targetBase + ".");

                                //If property exists, remove it
                                entProperty = this.EntityAdapterProperties.Where(e => !e.IsCustomized && e.EdmKey == edmKey).FirstOrDefault();
                                if (!entProperty.IsNull())
                                    this.EntityAdapterProperties.Remove(entProperty);

                                if (this.EntityAdapterPublicationProperties.Where(e => e.Name == (property.Name + suffix)).Count() == 0)
                                {
                                    this.EntityAdapterPublicationProperties.Add(new EntityAdapterPublicationProperty(this.Partition)
                                    {
                                        EdmKey = edmKey,
                                        Name = property.Name + suffix,
                                        Suffix = suffix,
                                        DisplayName = property.DisplayName,
                                        Datatype = (isNullReference && !property.DataType.ToLower().Contains("string") && !property.DataType.Contains("System.Nullable<") ? "System.Nullable<" + property.DataType + ">" : property.DataType),
                                        DisplayControl = (DisplayControlType)Enum.Parse(typeof(DisplayControlType), property.DisplayControl),
                                        IsNull = isNullReference || property.IsNull,
                                        IsBrowsable = property.IsBrowsable,
                                        IsEditable = property.IsEditable,
                                        DomainName = property.DomainName,
                                        KpiName = property.KpiName,
                                        Precision = property.Precision,
                                        DataFormatString = property.DataFormatString,
                                        DefaultValue = property.DefaultValue,
                                        IsCustomized = false,
                                        Mask = property.Mask,
                                        MaskType = property.MaskType
                                    });
                                }
                            }
                            ///////////////////

                        }
                    }

                }

            }
        }

        public void CheckDefaultUserInterface()
        {
            if (this.EntityAdapterUserInterfaces.Count > 0 && this.EntityAdapterUserInterfaces.Where(u => u.IsDefault).Count() == 0)
                this.EntityAdapterUserInterfaces[0].IsDefault = true;

            string nameSpace = this.EntityAdapterDesignerRoot.GetDirectContextNamespace();
            foreach (var ui in this.EntityAdapterUserInterfaces)
            {
                if (ui.SubscriptionEntityAdapterName != this.Name || ui.SubscriptionNameSpace != nameSpace)
                {
                    ui.SubscriptionNameSpace = nameSpace;
                    ui.SubscriptionEntityAdapterName = this.Name;
                }
            }
        }

        public List<EntityAdapter> GetAllSourceEntityAdapters()
        {
            List<EntityAdapter> result = new List<EntityAdapter>();

            foreach (EntityAdapter childEntity in this.SourceEntityAdapters)
            {
                result.Add(childEntity);
                result.AddRange(childEntity.GetAllSourceEntityAdapters());
            }

            return result;
        }

        public List<EntityAdapter> GetAllTargetEntityAdapters(string topPrimaryEntity)
        {
            List<EntityAdapter> result = new List<EntityAdapter>();

            EntityAdapter parentEntity = this;
            while (!parentEntity.TargetEntityAdapter.IsNull())
            {
                if (parentEntity.PrimaryEntity == topPrimaryEntity)
                    break;
                result.Add(parentEntity);
                parentEntity = parentEntity.TargetEntityAdapter;
            }

            return result;
        }

        public string GetFullPath()
        {
            string result = this.Name;
            EntityAdapter parent = this.TargetEntityAdapter;
            while (!parent.IsNull())
            {
                result = parent.Name + "." + result;
                parent = parent.TargetEntityAdapter;
            }

            return result;
        }

        public string GetCompositionMetaDataList(string contextName)
        {
            string composition = String.Empty;

            EntityAdapter topClass = this.GetTopParent();
            composition += "\"" + this.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName + "." + topClass.Name + "\"";

            if (topClass != this)
            {
                composition += ", \"" + this.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName + "." + this.Name + "\"";

                Action<EntityAdapter> action = null;
                action = (e) =>
                {
                    composition += ", \"" + this.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName + "." + e.Name + "\"";

                    foreach (var diEntity in e.DerivedEntityAdapters)
                        action(diEntity);
                };

                foreach (var dEntity in topClass.DerivedEntityAdapters)
                    action(dEntity);
            }

            return composition;
        }

        public EntityAdapter GetTopParent()
        {
            EntityAdapter parent = this;
            while (true)
            {
                if (parent.TargetEntityAdapter.IsNull())
                    break;
                parent = parent.TargetEntityAdapter;
            }
            return parent;
        }

        public EntityAdapter GetTopBaseClass()
        {
            EntityAdapter baseClass = this;
            while (true)
            {
                if (baseClass.BaseEntityAdapter.IsNull())
                    break;
                baseClass = baseClass.BaseEntityAdapter;
            }
            return baseClass;
        }

        public EntityAdapter GetTargetEntity()
        {
            EntityAdapter entity;

            entity = this.TargetEntityAdapter;
            if (entity != null)
                return entity;
            else
            {
                EntityAdapter baseType = this.BaseEntityAdapter;
                if (baseType == null)
                    return null;
                else
                    return baseType.GetTargetEntity();
            }
        }

        public LookUpAdapter GetInheritanceLookUpAdapter(string relationName)
        {
            LookUpAdapter result = null;

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result = (baseEntity.LookUpAdapters.Where(e => e.RelationName == relationName).FirstOrDefault());
                if (result != null)
                    break;
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public List<EntityAdapterProperty> GetInheritanceProperties()
        {
            List<EntityAdapterProperty> result = new List<EntityAdapterProperty>();

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.EntityAdapterProperties);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public bool HasQuickSearch()
        {
            var quickSearchProperties = this.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).ToArray();
            return quickSearchProperties.Where(e => e.Datatype.ToLower().Contains("string")).Count() > 0;
        }

        public List<EntityAdapterProperty> GetAllInheritanceProperties()
        {
            List<EntityAdapterProperty> result = new List<EntityAdapterProperty>(this.EntityAdapterProperties);

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.EntityAdapterProperties);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public List<EntityAdapterPublicationProperty> GetAllInheritancePublicationProperties()
        {
            List<EntityAdapterPublicationProperty> result = new List<EntityAdapterPublicationProperty>(this.EntityAdapterPublicationProperties);

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.EntityAdapterPublicationProperties);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public List<EntityAdapterPublicationProperty> GetInheritancePublicationProperties()
        {
            List<EntityAdapterPublicationProperty> result = new List<EntityAdapterPublicationProperty>();

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.EntityAdapterPublicationProperties);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public string GetInheritanceSecondaryEntities()
        {
            string result = String.Empty;

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                if (!baseEntity.SecondaryEntities.IsNullOrEmpty())
                    result += (result.IsNullOrEmpty() ? String.Empty : " ") + baseEntity.SecondaryEntities;
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public List<EntityAdapterProperty> GetDerivedProperties(bool addThisInstance = false)
        {
            List<EntityAdapterProperty> result = new List<EntityAdapterProperty>();

            if (addThisInstance)
                result.AddRange(this.EntityAdapterProperties);

            foreach (var dEntity in this.DerivedEntityAdapters)
            {
                result.AddRange(dEntity.GetDerivedProperties(true));
            }

            return result;
        }

        public string GetDerivedSecondaryEntities(bool addThisInstance = false)
        {
            string result = String.Empty, subResult = String.Empty;

            if (addThisInstance && !this.SecondaryEntities.IsNullOrEmpty())
                result = this.SecondaryEntities;

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            foreach (var dEntity in this.DerivedEntityAdapters)
            {
                subResult = dEntity.GetDerivedSecondaryEntities(true);
                if (!subResult.IsNullOrEmpty())
                    result += (result.IsNullOrEmpty() ? String.Empty : " ") + subResult;
            }

            return result;
        }

        public Project GetProject()
        {
            Project current = null;
            EntityAdapterDesignerDiagram diagram = this.Store.ElementDirectory.AllElements.OfType<EntityAdapterDesignerDiagram>().FirstOrDefault();
            if (diagram != null)
            {
                EnvDTE.DTE vs = diagram.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    if (project.ProjectItems.IsNullOrEmpty())
                        continue;

                    foreach (ProjectItem item in project.ProjectItems)
                    {
                        if (Path.GetExtension(item.Name).ToLower() == ".ead")
                        {
                            current = project;
                            break;
                        }
                    }

                    if (current != null)
                        break;

                }

            }

            return current;
        }

        public bool ExistsEvent(string eventName)
        {
            return (this.EntityAdapterEvents.Any(e => e.Name == eventName));
        }

        public bool ExistsClientEvent(string eventName)
        {
            return (this.EntityAdapterClientEvented.Any(e => e.Name == eventName));
        }

        public string GetEventCall(string eventName)
        {
            GenericOperation operation = this.EntityAdapterEvents.Where(e => e.Name == eventName).FirstOrDefault();
            if (operation.IsNull())
                return String.Empty;
            else
                return (operation.Workflow.IsNull() ? operation.Name : "Invoke" + operation.Workflow.Name);
        }

        public LookUpAdapter GetLookUpAdapter(string relationName)
        {
            return (this.LookUpAdapters.Where(e => e.RelationName == relationName).FirstOrDefault());
        }

        public bool ExistsOperation(string operationName)
        {
            return (this.EntityAdapterOperations.Where(e => e.Name == operationName).Count() > 0);
        }

        public bool ExistsProperty(string propertyName)
        {
            return (this.GetAllInheritanceAttributes().Where(e => e.Name == propertyName).Count() > 0);
        }

        public bool ExistsPropertyByEdmKey(string edmKey)
        {
            var attributes = this.GetAllInheritanceAttributes();

            return (attributes.Where(e => e is EntityAdapterProperty && this.IsTheSameEdmKey(((EntityAdapterProperty)e).EdmKey, edmKey)).Count() > 0) ||
                (attributes.Where(e => e is EntityAdapterPublicationProperty && this.IsTheSameEdmKey(((EntityAdapterPublicationProperty)e).EdmKey, edmKey)).Count() > 0);
        }

        public bool IsTheSameEdmKey(string edmKey, string edmKeyPart)
        {
            bool exists = false;

            exists = (edmKey == this.PrimaryEntity + "." + edmKeyPart);
            if (!exists)
            {
                Dictionary<string, string> entitySets = new Dictionary<string, string>();
                foreach (string entitySet in this.EntitySets.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (entitySet.Left("(") != this.PrimaryEntity)
                    {
                        string entitySetType = entitySet.Extract("(", ")");
                        if (entitySetType.Contains(":"))
                            entitySetType = entitySetType.Right(":");
                        entitySets.Add(entitySet.Left("("), entitySetType);
                    }
                }

                foreach (string relation in entitySets.Keys)
                {
                    exists = edmKey == (this.PrimaryEntity + "." + relation + "." + edmKeyPart);
                    if (exists)
                        break;
                }
            }

            return exists;
        }

        public bool ExistsFormula(string formulaName)
        {
            return (this.EntityAdapterFormulas.Where(e => e.Name == formulaName).Count() > 0);
        }

        public List<EntityAdapterAttribute> GetAllAttributes(bool byParentComposition = false)
        {
            List<EntityAdapterAttribute> attributes = new List<EntityAdapterAttribute>();
            attributes.AddRange(this.EntityAdapterProperties.Select(e => (EntityAdapterAttribute)e));
            attributes.AddRange(this.EntityAdapterFormulas.Select(e => (EntityAdapterAttribute)e));
            attributes.AddRange(this.EntityAdapterPublicationProperties.Select(e => (EntityAdapterAttribute)e));

            if (byParentComposition)
            {
                EntityAdapter parentEntity = this.TargetEntityAdapter;
                while (parentEntity != null)
                {
                    attributes.AddRange(parentEntity.GetAllAttributes().Where(e => !attributes.Any(r => r.Name == e.Name)));
                    parentEntity = parentEntity.TargetEntityAdapter;
                }
            }

            return (byParentComposition ? attributes.Where(e => !(e is EntityAdapterFormula)).ToList() : attributes);
        }

        public List<EntityAdapterAttribute> GetAllInheritanceAttributes(bool byParentComposition = false)
        {
            List<EntityAdapterAttribute> result = new List<EntityAdapterAttribute>(this.GetAllAttributes());

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.GetAllAttributes());
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            if (byParentComposition)
            {
                EntityAdapter parentEntity = this.TargetEntityAdapter;
                while (parentEntity != null)
                {
                    result.AddRange(parentEntity.GetAllInheritanceAttributes().Where(e => !result.Any(r => r.Name == e.Name)));
                    parentEntity = parentEntity.TargetEntityAdapter;
                }
            }

            return (byParentComposition ? result.Where(e => !(e is EntityAdapterFormula)).ToList() : result);

        }

        public List<EntityAdapterAttribute> GetNonExistentAttributes(EntityAdapter entity)
        {
            var allAttributes = entity.GetAllInheritanceAttributes();
            return this.GetAllInheritanceAttributes().Where(e => !allAttributes.Any(p => p.Name == e.Name)).ToList();
        }

        public string GetReplicationKey(string contextName)
        {
            if (!this.ReplicationKey.IsNullOrEmpty() && this.TargetEntityAdapter == null)
            {
                return contextName + "DomainService:" + this.ReplicationKey;
            }
            else
                return String.Empty;
        }

        public string GetCompositionHierarchy()
        {
            if (this.TargetEntityAdapter == null)
            {
                string listResult = String.Empty;
                Action<EntityAdapter, string> action = null;
                action = (entity, parentName) =>
                {
                    listResult += (listResult.IsNullOrEmpty() ? String.Empty : ",") + (parentName.IsNullOrEmpty() ? String.Empty : parentName + ".") + entity.Name;
                    entity.GetAllInheritanceSourceEntityAdapters().ForEach(e => action(e, entity.Name));
                };
                action(this, String.Empty);
                return listResult;
            }
            else
                return String.Empty;
        }

        public List<EntityAdapterExtendedFilter> GetAllInheritanceExtendedFilters()
        {
            List<EntityAdapterExtendedFilter> result = new List<EntityAdapterExtendedFilter>(this.EntityAdapterExtendedFilters);

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.EntityAdapterExtendedFilters);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public List<EntityAdapter> GetAllInheritanceSourceEntityAdapters()
        {
            List<EntityAdapter> result = new List<EntityAdapter>(this.SourceEntityAdapters);

            EntityAdapter baseEntity = this.BaseEntityAdapter;
            while (baseEntity != null)
            {
                result.AddRange(baseEntity.SourceEntityAdapters);
                baseEntity = baseEntity.BaseEntityAdapter;
            }

            return result;
        }

        public string GetAssociationDataMemberName()
        {
            string memeberName = this.Name + "List";
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null && !link.DataMemberName.IsNullOrEmpty())
            {
                memeberName = link.DataMemberName;
            }
            return memeberName;
        }

        public string GetAssociation(bool isForeignKey)
        {
            return GetAssociation(isForeignKey, false, false);
        }

        public string GetAssociation(bool isForeignKey, bool isJS)
        {
            return GetAssociation(isForeignKey, isJS, false);
        }

        public string GetAssociation(bool isForeignKey, bool isJS, bool isNetCore)
        {
            string association = "";
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                string parentKeyFields, detailKeyFields;

                if (this.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    parentKeyFields = "EntityUniqueKey";
                    detailKeyFields = "EntityParentUniqueKey";
                }
                else
                {
                    //Local view association 
                    if (this.HasEntityKeyLocalRelation() || (this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).Count() > 0 && this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).First().HasEntityKeyLocalRelation()))
                    {
                        parentKeyFields = "EntityKeyLocalRelation";
                        detailKeyFields = "EntityKeyLocalRelation";
                    }
                    else
                    {
                        parentKeyFields = link.ParentKeyFields;
                        detailKeyFields = link.DetailKeyFields;
                    }
                }

                //Adjust relation by parent key if the relation is empty.
                if (isJS && parentKeyFields.IsNullOrEmpty() && detailKeyFields.IsNullOrEmpty())
                {
                    foreach (var attribute in this.GetExtraParentRelationKey())
                    {
                        parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + attribute.Name;
                        detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + attribute.Name;
                    }
                }

                if (!parentKeyFields.IsNullOrEmpty() && !detailKeyFields.IsNullOrEmpty())
                {
                    if (isJS)
                    {
                        if (!this.TargetEntityAdapter.HasDynamicPrimaryKey())
                        {
                            string[] parentPrimaryKeys = this.TargetEntityAdapter.GetAllInheritanceProperties().Where(e => this.TargetEntityAdapter.IsPrimaryKey(e)).Select(e => e.Name).ToArray();
                            string[] parentFields = parentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] detailFields = detailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            parentKeyFields = detailKeyFields = "";
                            if (parentFields.Length == detailFields.Length)
                            {
                                for (int idx = 0; idx < parentFields.Length; idx++)
                                {
                                    if (parentPrimaryKeys.Contains(parentFields[idx].Trim()))
                                    {
                                        parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + parentFields[idx].Trim();
                                        detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + detailFields[idx].Trim();
                                    }
                                }
                            }

                            foreach (var pKey in parentPrimaryKeys)
                            {
                                if (!("," + parentKeyFields + ",").Contains("," + pKey + ","))
                                {
                                    parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + pKey;
                                    detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + pKey;
                                }
                            }
                        }

                        association = (isForeignKey ? this.TargetEntityAdapter.Name : this.Name + "List") + ": { entityTypeName: \"" + (isForeignKey ? this.TargetEntityAdapter.Name : this.Name) + ":#" + this.EntityAdapterDesignerRoot.GetContextNamespace() + "\", isScalar: " + isForeignKey.ToString().ToLower() + ", " + (isForeignKey ? "foreignKeyNames" : "invForeignKeyNames") + ": [" + this.EntityAdapterDesignerRoot.ToSeparatedStrList((isForeignKey ? detailKeyFields : parentKeyFields)) + "], associationName: \"FK_" + this.TargetEntityAdapter.Name + "_" + this.Name + "\" }";
                    }
                    else
                        association = @"[" + (isNetCore ? "Linx.DS.Core.Data." : "") + @"Association(""FK_" + this.TargetEntityAdapter.Name + @"_" + this.Name + @""", """ + (isForeignKey ? detailKeyFields : parentKeyFields) + @""", """ + (!isForeignKey ? detailKeyFields : parentKeyFields) + @""", IsForeignKey=" + isForeignKey.ToString().ToLower() + @")]";
                }
            }
            return association;
        }

        public Dictionary<string, string> GetAllParentKeystAssociation(bool isJS)
        {
            Dictionary<string, string> keysAssociation = new Dictionary<string, string>();
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {

                if (this.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    keysAssociation.Add("EntityParentUniqueKey", "EntityUniqueKey");
                }

                //Local view association 
                if (this.HasEntityKeyLocalRelation() || (this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).Count() > 0 && this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).First().HasEntityKeyLocalRelation()))
                {
                    keysAssociation.Add("EntityKeyLocalRelation", "EntityKeyLocalRelation");
                }

                if (!link.ParentKeyFields.IsNullOrEmpty() && !link.DetailKeyFields.IsNullOrEmpty())
                {
                    string[] parentKeys = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] detailKeys = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parentKeys.Length == detailKeys.Length)
                    {
                        for (int idx = 0; idx < parentKeys.Length; idx++)
                        {
                            string key = detailKeys[idx].Trim();
                            if (!keysAssociation.ContainsKey(key))
                                keysAssociation.Add(key, parentKeys[idx].Trim());
                        }
                    }
                }

                //Adjust relation by parent key if the relation is empty.
                if (isJS && keysAssociation.Count == 0)
                {
                    foreach (var attribute in this.GetExtraParentRelationKey())
                    {
                        keysAssociation.Add(attribute.Name, attribute.Name);
                    }
                }
            }

            return keysAssociation;
        }

        public string GetEnumDefinitions(string indent, bool byParentComposition)
        {
            string body = "";

            foreach (EntityAdapterAttribute fieldDef in this.GetAllAttributes(byParentComposition))
            {
                body += EntityAdapterAttribute.GetEnumDefinitions(indent, this.EntityAdapterDesignerRoot.TargetNamespace, fieldDef.Name, fieldDef.DomainName, fieldDef.KpiName, fieldDef.DisplayName);
            }

            return body;
        }

        public string GetChangeState(string indent)
        {
            string body = "";
            if (this.GetTopParent().IsBufferSaving() && this.BaseEntityAdapter == null)
            {
                body += "\r\n" + indent + "private string _changeState = \"N\";";
                body += "\r\n" + indent + "[DataMember()]";
                body += "\r\n" + indent + "public string ChangeState { get { return _changeState; } set { _changeState = value; } }";
            }

            return body;
        }

        public string GetTableMedia(string indent)
        {
            string body = "";
            if (this.HasEnabledMedias())
            {
                body += "\r\n" + indent + "[DataMember()]";
                body += "\r\n" + indent + "public string TableMedia { get; set; }";
            }

            return body;
        }

        public string GetSaveMedia(string indent)
        {
            string body = "";

            if (this.HasEnabledMedias())
            {
                var pk = this.GetAllInheritanceProperties().FirstOrDefault(e => (e.Datatype.ToLower().Contains("guid") || e.Datatype.ToLower().InList("int", "int32", "system.int32", "long", "int64", "system.int64")) && this.IsPrimaryKey(e));
                if (pk != null)
                {
                    body += "\r\n" + indent + "public void SaveMedia(DomainOperation operation)";
                    body += "\r\n" + indent + "{";
                    body += "\r\n" + indent + "     if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))";
                    body += "\r\n" + indent + "     {";
                    body += "\r\n" + indent + "         " + (this.EntityAdapterDesignerRoot.IsTCS() ? "Linx.Framework.BV.BusinessMediaHelper" : "Linx.Business.Tools.MediaHelper") + ".SyncMedia(\"" + this.PrimaryEntity + "\", " + (pk.Datatype.ToLower().Contains("int") ? "this." + pk.Name : "null") + ", " + (pk.Datatype.ToLower().Contains("guid") ? "this." + pk.Name : "null") + ", this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());";
                    body += "\r\n" + indent + "     }";
                    body += "\r\n" + indent + "     else if (operation == DomainOperation.Delete) {";
                    body += "\r\n" + indent + "         " + (this.EntityAdapterDesignerRoot.IsTCS() ? "Linx.Framework.BV.BusinessMediaHelper" : "Linx.Business.Tools.MediaHelper") + ".SyncMedia(\"" + this.PrimaryEntity + "\", " + (pk.Datatype.ToLower().Contains("int") ? "this." + pk.Name : "null") + ", " + (pk.Datatype.ToLower().Contains("guid") ? "this." + pk.Name : "null") + ", new List<Guid>() { Guid.Empty });";
                    body += "\r\n" + indent + "     }";
                    body += "\r\n" + indent + "}";
                }
            }

            return body;
        }

        public List<EntityAdapterProperty> GetExtraParentRelationKey()
        {
            List<EntityAdapterProperty> result = new List<EntityAdapterProperty>();
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                if (link.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    return result;
                }

                string parentKeyFields, detailKeyFields;

                parentKeyFields = link.ParentKeyFields;
                detailKeyFields = link.DetailKeyFields;

                var parentPrimaryKeys = this.TargetEntityAdapter.GetAllInheritanceProperties().Where(e => this.TargetEntityAdapter.IsPrimaryKey(e)).ToArray();
                string[] parentFields = parentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] detailFields = detailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                parentKeyFields = detailKeyFields = "";
                if (parentFields.Length == detailFields.Length)
                {
                    for (int idx = 0; idx < parentFields.Length; idx++)
                    {
                        if (parentPrimaryKeys.Any(e => e.Name == parentFields[idx].Trim()))
                        {
                            parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + parentFields[idx].Trim();
                            detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + detailFields[idx].Trim();
                        }
                    }
                }

                foreach (var pKey in parentPrimaryKeys)
                {
                    if (!("," + parentKeyFields + ",").Contains("," + pKey.Name + ","))
                    {
                        result.Add(pKey);
                    }
                }
            }
            return result;
        }


        public Dictionary<string, string> GetNoParentKeyRelations()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                string parentKeyFields, detailKeyFields;

                parentKeyFields = link.ParentKeyFields;
                detailKeyFields = link.DetailKeyFields;

                var parentPrimaryKeys = this.TargetEntityAdapter.GetAllInheritanceProperties().Where(e => this.TargetEntityAdapter.IsPrimaryKey(e)).ToArray();
                string[] parentFields = parentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] detailFields = detailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                parentKeyFields = detailKeyFields = "";
                if (parentFields.Length == detailFields.Length)
                {
                    for (int idx = 0; idx < parentFields.Length; idx++)
                    {
                        if (!parentPrimaryKeys.Any(e => e.Name == parentFields[idx].Trim()))
                        {
                            result[detailFields[idx].Trim()] = parentFields[idx].Trim();
                        }
                    }
                }
            }

            return result;
        }

        public bool IsRelationWithParent(string propertyName)
        {
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                return link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()).Contains(propertyName);
            }
            else return false;
        }

        public void GetJsWhereDetailRelation(Linx.Tools.CodeBuilder codeBuilder, string parentAlias, string concatVariable = "")
        {
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                codeBuilder.Add("                    " + (concatVariable.IsNullOrEmpty() ? ".withParameters({ jEntitySearch: " : concatVariable + " += '&jEntitySearch=' + ") + "ownerReference.GetJsWhereDetailRelationFor" + this.Name + "(customParentRelation)" + (concatVariable.IsNullOrEmpty() ? " })" : ";"));
            }
        }

        public void GenerateJsWhereDetailRelationMethod(Linx.Tools.CodeBuilder codeBuilder, string parentAlias, string commonObjectName = "", bool hasVisibleProperties = true)
        {
            codeBuilder.AddLine("   ownerReference.GetJsWhereDetailRelationFor" + this.Name + " = function(customParentRelation) {");

            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                var parentKeys = (link.ParentKeyFields.IsNullOrEmpty() ? new string[] { } : link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                var detailKeys = (link.DetailKeyFields.IsNullOrEmpty() ? new string[] { } : link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                var parentLink = this.GetParentLinkRelation();
                var topParent = this.GetTopParent();

                //Se existe evento OnDetailSearching gera código para validar cancelamento da query do detalhe
                if (this.TargetEntityAdapter.ExistsClientEvent("OnDetailSearching"))
                {
                    codeBuilder.Add("       var filter = ownerReference.OnDetailSearching('" + this.Name + "');");
                    codeBuilder.Add("       if (filter === 'Error'){");
                    codeBuilder.Add("           return filter;");
                    codeBuilder.Add("        }");
                }
                //

                codeBuilder.Add("       return " + (parentLink.IsDashboard || topParent.IsDashboardFilter || this.EnableQueryByParent ? "vm.lastJEntitySearch() + " : "") + "'" + (this.EnableQueryByParent ? topParent.Name : this.Name) + "{'");

                if (!this.TargetEntityAdapter.IsDashboardFilter && parentKeys.Length > 0 && parentKeys.Length == detailKeys.Length)
                {
                    codeBuilder.Add(" + (!" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "isNullOrEmpty(customParentRelation) ? customParentRelation : ");
                    for (int idx = 0; idx < detailKeys.Length; idx++)
                    {
                        var parentAttr = link.TargetEntityAdapter.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == parentKeys[idx]);
                        codeBuilder.Add((link.RemoveFieldIfEmpty ? "(" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "isNullOrEmpty(" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "getAbsoluteValue(" + parentAlias + "." + parentKeys[idx] + ")) ? '' : " : "") + "'" + (this.EnableQueryByParent ? parentKeys[idx] : detailKeys[idx]) + "#==#' + " + parentAlias + ".serverDataType['" + parentKeys[idx] + "'] + " + ((parentAttr != null && parentAttr.Datatype.ToLower().Contains("date")) ? "" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "convertDateToString(" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "getAbsoluteValue(" + parentAlias + "." + parentKeys[idx] + "))" : "" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "getAbsoluteValue(" + parentAlias + "." + parentKeys[idx] + ").toString()") + (link.RemoveFieldIfEmpty ? ")" : "") + (idx < detailKeys.Length - 1 ? " + ';' + " : ""));
                    }
                    codeBuilder.Add(")");
                }

                if (this.TargetEntityAdapter.ExistsClientEvent("OnDetailSearching"))
                {
                    codeBuilder.Add(" + function() { var filter = ownerReference.OnDetailSearching('" + this.Name + "');" + (this.EnableMetaDataFilter && !this.EnableQueryByParent ? " if (!" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "isNullOrEmpty(filter) && filter.indexOf('{LinqValidProperties#') === -1)" + (hasVisibleProperties ? " filter += vm.getVisibleProperties('" + this.Name + "List')" : "") + ";" : "") + " return (!" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "isNullOrEmpty(filter) && filter.indexOf('{') == -1 ? ';' + filter + '}' : '}' + filter); }()");
                }
                else
                {
                    codeBuilder.Add(" + '}'");

                    if (hasVisibleProperties && this.EnableMetaDataFilter && !this.EnableQueryByParent)
                        codeBuilder.Add(" + vm.getVisibleProperties('" + this.Name + "List')");
                }

                codeBuilder.Add(";");

            }
            codeBuilder.AddLine();
            codeBuilder.AddLine("   }");
        }

        public string GetJsTestDetailRelation(string parentAlias, string commonObjectName = "")
        {
            string associationTest = "";
            EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
            if (link != null)
            {
                if (!link.ParentKeyFields.IsNullOrEmpty() && !link.DetailKeyFields.IsNullOrEmpty())
                {
                    var parentKeys = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var detailKeys = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parentKeys.Length == detailKeys.Length)
                    {
                        for (int idx = 0; idx < detailKeys.Length; idx++)
                        {
                            associationTest += (associationTest.IsNullOrEmpty() ? "" : " && ") + "!" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "isNullOrEmpty(" + (commonObjectName.IsNullOrEmpty() ? "" : commonObjectName + ".") + "getAbsoluteValue(" + parentAlias + "." + parentKeys[idx] + "))";
                        }
                    }
                }
            }

            if (!associationTest.IsNullOrEmpty())
                associationTest = " && (" + associationTest + ")";

            return associationTest;
        }

        public string GetFixedFilter(string key)
        {
            string filterSelection = "", filter;

            if (this.EntityAdapterProperties != null)
            {
                foreach (EntityAdapterProperty property in this.GetAllInheritanceProperties())
                {
                    if (!property.Filter.IsNullOrEmpty())
                    {
                        filter = property.Filter;
                    }
                    else if (!this.SurrogateProperty.IsNullOrEmpty() && property.Name == this.SurrogateProperty && property.Datatype.ToLower().Contains("bool"))
                    {
                        filter = "[Value] == false";
                    }
                    else filter = String.Empty;

                    if (!filter.IsNullOrEmpty())
                    {
                        filterSelection += (filterSelection == "" ? "" : " && ") + (this.EntityAdapterRepresentation == null ? this.ReplaceEdmPath(filter.Replace("[Value]", property.EdmKey).Replace("[ThisRef]", this.PrimaryEntity), key) : filter.Replace("[Value]", (!property.DataRelationKey.Contains("#") ? property.DataRelationKey : property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right("."))).Replace("[ThisRef]", property.DataRelationKey.Left("#")));
                    }
                }

                foreach (EntityAdapterPublicationProperty property in this.GetAllInheritancePublicationProperties())
                {
                    if (!property.Filter.IsNullOrEmpty())
                    {
                        filter = property.Filter;
                    }
                    else if (!this.SurrogateProperty.IsNullOrEmpty() && property.Name == this.SurrogateProperty && property.Datatype.ToLower().Contains("bool"))
                    {
                        filter = "[Value] == false";
                    }
                    else filter = String.Empty;

                    if (!filter.IsNullOrEmpty())
                    {
                        filterSelection += (filterSelection == "" ? "" : " && ") + (this.EntityAdapterRepresentation == null ? this.ReplaceEdmPath(filter.Replace("[Value]", property.EdmKey).Replace("[ThisRef]", this.PrimaryEntity), key) : filter.Replace("[Value]", (!property.DataRelationKey.Contains("#") ? property.DataRelationKey : property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right("."))).Replace("[ThisRef]", property.DataRelationKey.Left("#")));
                    }
                }
            }

            //Get filter of entity
            var baseClass = this;
            while (baseClass != null)
            {
                filter = baseClass.Filter;
                if (!filter.IsNullOrEmpty())
                {
                    filterSelection += (filterSelection == "" ? "" : " && ") + (this.EntityAdapterRepresentation == null ? this.ReplaceEdmPath(filter.Replace("[ThisRef]", this.PrimaryEntity), key) : filter);
                }
                baseClass = baseClass.BaseEntityAdapter;
            }

            return (filterSelection.IsNullOrEmpty() ? "" : "(" + filterSelection + ")");
        }

        public string GetLocalFixedFilter(string key)
        {
            string filterSelection = "";

            if (this.EntityAdapterProperties != null && !this.LocalEntityAdapter.IsNull())
            {
                //Test Composition
                string path = key;
                EntityAdapter parent = this.LocalEntityAdapter.TargetEntityAdapter;
                while (parent != null)
                {
                    path += "." + parent.Name;
                    filterSelection += (filterSelection == "" ? "" : " && ") + path + " != null";
                    parent = parent.TargetEntityAdapter;
                }
                //Get filters
                foreach (EntityAdapterProperty property in this.EntityAdapterProperties.Where(e => !e.Filter.IsNullOrEmpty()))
                {
                    filterSelection += (filterSelection == "" ? "" : " && ") + property.Filter.Replace("[Value]", property.DataRelationKey.Replace(this.LocalEntityAdapter.Name + ".", key + ".")).Replace("[ThisRef]", key);
                }
            }

            return (filterSelection.IsNullOrEmpty() ? "" : "where " + filterSelection);
        }

        public string GetLocalOrderByDefinition(string key)
        {
            string result = String.Empty;
            List<string> orderNames = new List<string>();

            foreach (var prop in this.EntityAdapterProperties.Where(e => e.OrderBySequence >= 0).OrderBy(o => o.OrderBySequence))
            {
                result += (result.IsNullOrEmpty() ? String.Empty : ", ") + prop.DataRelationKey.Replace(this.LocalEntityAdapter.Name + ".", key + ".") + " " + prop.OrderByOrientation.ToString().ToLower();
            }

            return (result.IsNullOrEmpty() ? String.Empty : "orderby " + result);
        }

        public string GetEntityRelationByParent()
        {
            if (this.TargetEntityAdapter != null && !this.TargetEntityAdapter.DetailRelations.IsNullOrEmpty())
                return ("#" + this.TargetEntityAdapter.DetailRelations).Extract("#" + this.PrimaryEntity + "(", ")");
            else
                return "";
        }

        public string GetEntityReferenceByParent()
        {
            string entityRef = "";
            if (this.TargetEntityAdapter != null && !this.TargetEntityAdapter.ReferenceRelations.IsNullOrEmpty()
                && (this.TargetEntityAdapter.PrimaryEntity != this.PrimaryEntity))
            {
                entityRef = ("#" + this.TargetEntityAdapter.ReferenceRelations).Extract("#" + this.PrimaryEntity + "(", ")");
            }
            return entityRef;

        }

        public string GetParentRelationName()
        {
            //entity.TargetEntityAdapter 
            string relationName = String.Empty;

            EntityAdapterReferencesTargetEntityAdapter link = EntityAdapterReferencesTargetEntityAdapter.GetLinkToTargetEntityAdapter(this);
            if (link != null)
            {
                foreach (string field in link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var prop = this.EntityAdapterProperties.Where(e => e.Name == field).FirstOrDefault();
                    if (!prop.IsNull())
                    {
                        relationName = prop.EdmKey.Extract(this.PrimaryEntity + ".", ".");
                        if (!relationName.IsNullOrEmpty())
                            break;
                    }
                }
            }

            return relationName;
        }

        public string GetEntityNameByRelation(string fkRelation)
        {
            if (!this.EntityRelations.IsNullOrEmpty() && !fkRelation.IsNullOrEmpty())
                return ("#" + this.EntityRelations).Extract("#" + fkRelation + "(", ")");
            else
                return "";
        }

        public string GetBusinessFilterByRelation(string alias, bool isDbContext, string fkRelation)
        {
            var edmInfo = this.EntityAdapterDesignerRoot.GetEdm().EdmInfo;
            if (edmInfo.IsRequiredPath(fkRelation))
            {
                var tableName = this.GetEntityNameByRelation(fkRelation.Right("."));
                var type = edmInfo.GetTypeByName(tableName);
                if (type != null && type.Properties.FirstOrDefault(p => p.Name == "ID_GPECON") != null)
                {
                    return "(!this.HasGpeconControl || " + alias + ".ID_GPECON == this." + (isDbContext ? "DbContext" : "ObjectContext") + ".IdGpecon)";
                }
            }
            return "";
        }

        public string GetFullEntityRelation()
        {
            string relationPath = "";
            var entity = this;
            var parent = this.TargetEntityAdapter;
            while (parent != null)
            {
                relationPath += (relationPath.IsNullOrEmpty() ? "" : ".") + entity.GetEntityRelationByName(parent.PrimaryEntity);
                entity = parent;
                parent = entity.TargetEntityAdapter;
            }
            return relationPath;
        }

        public string GetEntityRelationByName(string entityName)
        {
            if (!this.EntityRelations.IsNullOrEmpty() && !entityName.IsNullOrEmpty())
                return ("#" + this.EntityRelations.Left("(" + entityName + ")")).Right("#");
            else
                return "";
        }

        public string GetFieldsRelatedWithParent()
        {
            string parentRelation = ",";
            EntityAdapterReferencesTargetEntityAdapter link = GetParentLinkRelation();
            if (link != null)
            {
                var childFiels = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int index = 0; index < childFiels.Length; index++)
                    parentRelation += childFiels[index] + ",";
            }
            return parentRelation;
        }

        public string GetParentEdmRelationToLinq(string alias, string parentAlias)
        {
            bool parentIsAggregated = (this.TargetEntityAdapter != null && this.TargetEntityAdapter.IsAggregationView);
            string parentRelation = "", parentField, childField;
            EntityAdapterReferencesTargetEntityAdapter link = GetParentLinkRelation();
            if (link != null)
            {
                var parentFiels = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var childFiels = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parentFiels.Length == childFiels.Length)
                {
                    for (int index = 0; index < childFiels.Length; index++)
                    {
                        childField = this.EntityAdapterDesignerRoot.GetEdmkeyByPropertyName(this, childFiels[index]).Right(this.PrimaryEntity + ".");
                        parentField = this.EntityAdapterDesignerRoot.GetEdmkeyByPropertyName(this.TargetEntityAdapter, parentFiels[index]).Right(this.TargetEntityAdapter.PrimaryEntity + ".");
                        if (!childField.IsNullOrEmpty() && !parentField.IsNullOrEmpty())
                            parentRelation += (parentRelation == "" ? " where " : " && ") + alias + "." + childField + " == " + parentAlias + "." + parentField;
                    }
                }
            }
            return parentRelation;
        }

        public string GetParentRelationToLinq(string alias, string parentAlias, string parentGroupAlias)
        {
            string parentRelation = "", childField, parentField;
            bool isRepresentation = this.EntityAdapterRepresentation != null;
            EntityAdapterReferencesTargetEntityAdapter link = GetParentLinkRelation();
            if (link != null)
            {
                var parentFiels = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var childFiels = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parentFiels.Length == childFiels.Length)
                {
                    for (int index = 0; index < childFiels.Length; index++)
                    {
                        if (isRepresentation)
                        {
                            childField = this.EntityAdapterDesignerRoot.GetDataRepresentationKeyBypropertyName(this, childFiels[index]);
                            parentField = (parentGroupAlias.IsNullOrEmpty() ? this.EntityAdapterDesignerRoot.GetDataRepresentationKeyBypropertyName(this.TargetEntityAdapter, parentFiels[index]) : parentGroupAlias + "." + parentFiels[index]);
                            if (!childField.IsNullOrEmpty() && !parentField.IsNullOrEmpty())
                                parentRelation += (parentRelation == "" ? " where " : " && ") + childField + " == " + parentField;
                        }
                        else
                        {
                            childField = this.ReplaceEdmPath(this.EntityAdapterDesignerRoot.GetEdmkeyByPropertyName(this, childFiels[index]), alias);
                            parentField = (parentGroupAlias.IsNullOrEmpty() ? this.TargetEntityAdapter.ReplaceEdmPath(this.EntityAdapterDesignerRoot.GetEdmkeyByPropertyName(this.TargetEntityAdapter, parentFiels[index]), parentAlias) : parentGroupAlias + "." + parentFiels[index]);
                            if (!childField.IsNullOrEmpty() && !parentField.IsNullOrEmpty())
                                parentRelation += (parentRelation == "" ? " where " : " && ") + childField + " == " + parentField;
                        }
                    }
                }
            }
            return parentRelation;
        }

        public string GetParentRelationToLinqForEntity(string alias, string parentAlias, bool byEntitySearchExpression = false, string indent = "", bool invert = false)
        {
            bool parentIsAggregated = (this.TargetEntityAdapter != null && this.TargetEntityAdapter.IsAggregationView);
            string parentRelation = "";
            EntityAdapterReferencesTargetEntityAdapter link = GetParentLinkRelation();
            if (link != null)
            {
                var parentFiels = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var childFiels = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parentFiels.Length == childFiels.Length)
                {
                    for (int index = 0; index < childFiels.Length; index++)
                    {
                        if (byEntitySearchExpression)
                        {
                            if (index == 0)
                                parentRelation += "\r\n" + indent + "EntitySearch " + alias + " = new EntitySearch(\"" + (invert && this.TargetEntityAdapter != null ? this.TargetEntityAdapter.Name : this.Name) + "\");";
                            else
                                parentRelation += "\r\n" + indent + alias + ".Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, \"&&\"));";

                            parentRelation += "\r\n" + indent + alias + ".Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, \"" + (invert ? parentFiels[index] : childFiels[index]) + "\"));";
                            parentRelation += "\r\n" + indent + alias + ".Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, \"==\"));";
                            parentRelation += "\r\n" + indent + alias + ".Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, " + (invert ? parentAlias + "." + childFiels[index] : parentAlias + "." + parentFiels[index]) + "));";
                        }
                        else
                        {
                            parentRelation += (parentRelation == "" ? " where " : " && ") + alias + "." + childFiels[index] + " == " + parentAlias + "." + parentFiels[index];
                        }
                    }
                }
            }

            return parentRelation;
        }

        public string GetParentRelationToLinqForEntitySearch(string indent)
        {
            bool parentIsAggregated = (this.TargetEntityAdapter != null && this.TargetEntityAdapter.IsAggregationView);
            string parentRelationES = "";
            EntityAdapterReferencesTargetEntityAdapter link = GetParentLinkRelation();
            if (link != null)
            {
                var parentFiels = link.ParentKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var childFiels = link.DetailKeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parentFiels.Length == childFiels.Length)
                {
                    for (int index = 0; index < childFiels.Length; index++)
                    {
                        if (!parentRelationES.IsNullOrEmpty())
                            parentRelationES += "\r\n" + indent + "             detailSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, \"&&\"));";

                        parentRelationES += "\r\n" + indent + "             detailSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, \"" + childFiels[index] + "\"));";
                        parentRelationES += "\r\n" + indent + "             detailSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, \"==\"));";
                        parentRelationES += "\r\n" + indent + "             detailSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this." + parentFiels[index] + "));";

                    }
                }
            }

            if (!parentRelationES.IsNullOrEmpty())
            {
                parentRelationES =
                    "\r\n" + indent + "             List<EntitySearch> searchList = new List<EntitySearch>() { new EntitySearch(\"" + this.Name + "\") } ;" +
                    "\r\n" + indent + "             EntitySearch detailSearch = searchList.First();" +
                    parentRelationES;
            }

            return parentRelationES;
        }

        public string GetRelationFieldsToLinq(string alias, string indent, bool byParentComposition, bool checkValidProps)
        {
            string relationFieldsToLinq = String.Empty;
            EntityAdapterProperty property;
            EntityAdapterFormula formula;
            EntityAdapterPublicationProperty pProperty;
            bool isRepresentation = this.EntityAdapterRepresentation != null;
            string attrExpr = "";

            foreach (EntityAdapterAttribute attribute in (byParentComposition ? this.GetAllInheritanceAttributes().Where(e => !(e is EntityAdapterFormula)) : this.GetAllInheritanceAttributes()))
            {
                if (attribute is EntityAdapterProperty)
                {
                    property = (EntityAdapterProperty)attribute;
                    string prefixLinqMethod = property.GetPrefixLinqMethod(), suffixLinqMethod = property.GetSuffixLinqMethod();
                    attrExpr = (isRepresentation ? (!property.DataRelationKey.Contains("#") ? property.DataRelationKey : property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right(".")) : (property.DenormalizedDataInfo.IsNullOrEmpty() ? ReplaceEdmPath(property.EdmKey, alias) : "normalizedEntity" + property.Name + ".Value"));
                    relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + (prefixLinqMethod.IsNullOrEmpty() ? "" : prefixLinqMethod + "(") + attrExpr + (prefixLinqMethod.IsNullOrEmpty() ? "" : ")") + (suffixLinqMethod.IsNullOrEmpty() ? "" : "." + suffixLinqMethod) + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                }
                else if (attribute is EntityAdapterPublicationProperty)
                {
                    pProperty = (EntityAdapterPublicationProperty)attribute;
                    attrExpr = (isRepresentation ? (!pProperty.DataRelationKey.Contains("#") ? pProperty.DataRelationKey : pProperty.DataRelationKey.Left("#") + "." + pProperty.DataRelationKey.Right(".")) : ReplaceEdmPath(pProperty.EdmKey, alias));
                    relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + pProperty.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + attrExpr + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                }
                else if (attribute is EntityAdapterFormula)
                {
                    formula = (EntityAdapterFormula)attribute;
                    if (!formula.LinqDefinition.IsNullOrEmpty() && !formula.IsUpdatable)
                    {
                        attrExpr = (isRepresentation ? (!formula.DataRelationKey.Contains("#") ? formula.DataRelationKey : formula.DataRelationKey.Left("#") + "." + formula.DataRelationKey.Right(".")) : ReplaceEdmPath(formula.LinqDefinition, alias));
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + formula.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + attrExpr + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                    }
                }

                if (!attribute.DomainName.IsNullOrEmpty() && !attrExpr.IsNullOrEmpty())
                {
                    string domainExp = GetDomainNameLinqExpression(attribute, attrExpr);
                    if (!domainExp.IsNullOrEmpty())
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + "Name = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + domainExp + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType("System.String") + ")" : "");
                }
            }

            if (byParentComposition)
                relationFieldsToLinq += this.GetLinqDefinitionsByParentComposition(alias, indent);

            return relationFieldsToLinq;
        }

        public string GetValidMetaDataProperties(string indent, string arrayName)
        {
            if (!this.EnableMetaDataFilter)
                return "";

            string body = "";
            var properties = this.GetAllInheritanceAttributes();

            var domains = properties.Where(e => !e.DomainName.IsNullOrEmpty()).Select(e => "\"" + e.Name + "Name\"").ToArray();
            if (domains.Length > 0)
            {
                body += "\r\n" + indent + "//Adjust Metadata for Domains";
                body += "\r\n" + indent + "string[] domains = new string[] { " + String.Join(",", domains) + " };";
                body += "\r\n" + indent + "for (int idx = 0; idx < " + arrayName + ".Length; idx++)";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + "    if (domains.Contains(" + arrayName + "[idx]))";
                body += "\r\n" + indent + "        " + arrayName + "[idx] = (" + arrayName + "[idx] + \"#\").Left(\"Name#\");";
                body += "\r\n" + indent + "}";
            }

            if (properties.Any(e => e.IgnoreMetaData) && properties.Any(e => !e.IgnoreMetaData))
            {
                string validProperties = "", invalidProperties = "";
                foreach (var prop in properties)
                {
                    if (prop.IgnoreMetaData)
                        invalidProperties += (invalidProperties == "" ? "" : ", ") + "\"" + prop.Name + "\"";
                    else
                        validProperties += (validProperties == "" ? "" : ", ") + "\"" + prop.Name + "\"";
                }

                body += "\r\n" + indent + "//Metadata filter Analysis";
                body += "\r\n" + indent + "if (" + arrayName + ".Length == 0)";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + "      " + arrayName + " = new string[] {";
                body += "\r\n" + indent + "           " + validProperties;
                body += "\r\n" + indent + "      };";
                body += "\r\n" + indent + "}";
                body += "\r\n" + indent + "else";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + "      var validMetaData = new List<string>();";
                body += "\r\n" + indent + "      var invalidMetaData = new string[] {";
                body += "\r\n" + indent + "          " + invalidProperties;
                body += "\r\n" + indent + "      };";
                body += "\r\n" + indent + "      foreach (string propName in " + arrayName + ")";
                body += "\r\n" + indent + "      {";
                body += "\r\n" + indent + "          if (!invalidMetaData.Contains(propName))";
                body += "\r\n" + indent + "              validMetaData.Add(propName);";
                body += "\r\n" + indent + "      }";
                body += "\r\n" + indent + "      " + arrayName + " = validMetaData.ToArray();";
                body += "\r\n" + indent + "}";

            }

            return body;
        }

        public string GetValidPropertiesForLinq(string indent)
        {
            string body = "";

            if (this.EnableMetaDataFilter)
            {
                body += "\r\n" + indent + "//Set valid properties for LINQ.";
                body += "\r\n" + indent + "//There are two opposite collections for logical control. The reason is just for generating the best command to database provider.";
                EntityAdapterReferencesTargetEntityAdapter link = this.GetParentLinkRelation();
                foreach (EntityAdapterAttribute attribute in this.GetAllInheritanceAttributes())
                {
                    if (attribute is EntityAdapterProperty || attribute is EntityAdapterPublicationProperty || (attribute is EntityAdapterFormula && !((EntityAdapterFormula)attribute).LinqDefinition.IsNullOrEmpty() && !((EntityAdapterFormula)attribute).IsUpdatable))
                    {
                        if (link == null || link.DetailKeyFields.IsNullOrEmpty() || !link.DetailKeyFields.Contains(attribute.Name))
                        {
                            body += "\r\n" + indent + "bool[] has" + attribute.Name + " = (validProperties.Length == 0 || validProperties.Contains(\"" + attribute.Name + "\") ? _trueMetaCondition : _falseMetaCondition);";
                            body += "\r\n" + indent + "bool[] hasNo" + attribute.Name + " = (validProperties.Length == 0 || validProperties.Contains(\"" + attribute.Name + "\") ? _falseMetaCondition : _trueMetaCondition);";
                        }
                        else
                        {
                            body += "\r\n" + indent + "bool[] has" + attribute.Name + " = _trueMetaCondition;";
                            body += "\r\n" + indent + "bool[] hasNo" + attribute.Name + " = _falseMetaCondition;";
                        }
                    }
                }
            }

            return body;
        }

        private string GetDefaultByDataType(string dataType)
        {
            return this.EntityAdapterDesignerRoot.GetDefaultByDataType(dataType);
        }

        public string GetGroupingFieldsToLinq(string alias, string indent, bool checkValidProps)
        {
            string relationFieldsToLinq = String.Empty;
            EntityAdapterProperty property;
            EntityAdapterPublicationProperty pProperty;
            EntityAdapterFormula formula;
            bool isRepresentation = this.EntityAdapterRepresentation != null;
            string attrExpr;

            foreach (EntityAdapterAttribute attribute in this.GetAllInheritanceAttributes().Where(e => e.AggregationFunction == UIAggregationFunctions.None))
            {
                attrExpr = "";
                if (attribute is EntityAdapterProperty)
                {
                    property = (EntityAdapterProperty)attribute;
                    string prefixLinqMethod = property.GetPrefixLinqMethod(), suffixLinqMethod = property.GetSuffixLinqMethod();
                    attrExpr = (prefixLinqMethod.IsNullOrEmpty() ? "" : prefixLinqMethod + "(") + (isRepresentation ? (!property.DataRelationKey.Contains("#") ? property.DataRelationKey : property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right(".")) : ReplaceEdmPath(property.EdmKey, alias)) + (prefixLinqMethod.IsNullOrEmpty() ? "" : ")") + (suffixLinqMethod.IsNullOrEmpty() ? "" : "." + suffixLinqMethod);
                    relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + attrExpr + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                }
                else if (attribute is EntityAdapterPublicationProperty)
                {
                    pProperty = (EntityAdapterPublicationProperty)attribute;
                    attrExpr = (isRepresentation ? (!pProperty.DataRelationKey.Contains("#") ? pProperty.DataRelationKey : pProperty.DataRelationKey.Left("#") + "." + pProperty.DataRelationKey.Right(".")) : ReplaceEdmPath(pProperty.EdmKey, alias));
                    relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + attrExpr + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                }
                else if (attribute is EntityAdapterFormula)
                {
                    formula = (EntityAdapterFormula)attribute;
                    if (!formula.LinqDefinition.IsNullOrEmpty() && !formula.IsUpdatable)
                    {
                        attrExpr = (isRepresentation ? (!formula.DataRelationKey.Contains("#") ? formula.DataRelationKey : formula.DataRelationKey.Left("#") + "." + formula.DataRelationKey.Right(".")) : ReplaceEdmPath(formula.LinqDefinition, alias));
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + " = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + attrExpr + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType(attribute.Datatype) + ")" : "");
                    }
                }

                if (!attribute.DomainName.IsNullOrEmpty() && !attrExpr.IsNullOrEmpty() && attribute.AggregationFunction == UIAggregationFunctions.None)
                {
                    string domainExp = GetDomainNameLinqExpression(attribute, attrExpr);
                    if (!domainExp.IsNullOrEmpty())
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + "Name = " + (this.EnableMetaDataFilter && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + domainExp + (this.EnableMetaDataFilter && checkValidProps ? " : " + this.GetDefaultByDataType("System.String") + ")" : "");
                }
            }

            return relationFieldsToLinq;
        }

        public string GetLocalGroupingFieldsToLinq(string alias, string ident)
        {
            string relationFieldsToLinq = String.Empty;

            foreach (EntityAdapterAttribute attribute in this.GetAllAttributes().Where(e => e.AggregationFunction == UIAggregationFunctions.None))
            {
                relationFieldsToLinq += "\r\n" + ident + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + " = " + attribute.DataRelationKey.Replace(this.LocalEntityAdapter.Name + ".", alias + ".");
            }

            return relationFieldsToLinq;
        }

        public string GetAggregationFieldsToLinq(string groupAlias, string alias, string indent, bool checkValidProps)
        {
            string relationFieldsToLinq = String.Empty;
            EntityAdapterProperty property;
            EntityAdapterPublicationProperty pProperty;
            EntityAdapterFormula formula;
            bool isRepresentation = this.EntityAdapterRepresentation != null;
            bool isGroupPart;

            foreach (EntityAdapterAttribute attribute in this.GetAllInheritanceAttributes())
            {
                isGroupPart = false;
                if (attribute is EntityAdapterProperty)
                {
                    property = (EntityAdapterProperty)attribute;
                    if (property.AggregationFunction == UIAggregationFunctions.None)
                    {
                        isGroupPart = true;
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + groupAlias + ".Key." + property.Name;
                    }
                    else
                    {
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + (property.AggregationFunction == UIAggregationFunctions.CountDistinct ? groupAlias + (attribute.CountDistinctFilter.IsNullOrEmpty() ? "" : ".Where(e => " + attribute.CountDistinctFilter.Replace("this.", "e.").Replace("[ThisRef]", "e").Replace("[Value]", "e." + attribute.Name) + ")") + ".Select(e => " + (isRepresentation ? (!property.DataRelationKey.Contains("#") ? property.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right(".")) : ReplaceEdmPath(property.EdmKey, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ").Distinct().Count()" : (property.AggregationFunction == UIAggregationFunctions.Count ? groupAlias + ".Count()" : groupAlias + "." + property.AggregationFunction.ToString().Replace("Avg", "Average") + "(e => " + (isRepresentation ? (!property.DataRelationKey.Contains("#") ? property.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + property.DataRelationKey.Left("#") + "." + property.DataRelationKey.Right(".")) : ReplaceEdmPath(property.EdmKey, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ")")) + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? " : " + GetDefaultByDataType(attribute.Datatype) + ")" : "");
                    }
                }
                else if (attribute is EntityAdapterPublicationProperty)
                {
                    pProperty = (EntityAdapterPublicationProperty)attribute;
                    if (pProperty.AggregationFunction == UIAggregationFunctions.None)
                    {
                        isGroupPart = true;
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + pProperty.Name + " = " + groupAlias + ".Key." + pProperty.Name;
                    }
                    else
                    {
                        relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + pProperty.Name + " = " + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + (pProperty.AggregationFunction == UIAggregationFunctions.CountDistinct ? groupAlias + (attribute.CountDistinctFilter.IsNullOrEmpty() ? "" : ".Where(e => " + attribute.CountDistinctFilter.Replace("this.", "e.").Replace("[ThisRef]", "e").Replace("[Value]", "e." + attribute.Name) + ")") + ".Select(e => " + (isRepresentation ? (!pProperty.DataRelationKey.Contains("#") ? pProperty.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + pProperty.DataRelationKey.Left("#") + "." + pProperty.DataRelationKey.Right(".")) : ReplaceEdmPath(pProperty.EdmKey, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ").Distinct().Count()" : (pProperty.AggregationFunction == UIAggregationFunctions.Count ? groupAlias + ".Count()" : groupAlias + "." + pProperty.AggregationFunction.ToString().Replace("Avg", "Average") + "(e => " + (isRepresentation ? (!pProperty.DataRelationKey.Contains("#") ? pProperty.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + pProperty.DataRelationKey.Left("#") + "." + pProperty.DataRelationKey.Right(".")) : ReplaceEdmPath(pProperty.EdmKey, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ")")) + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? " : " + GetDefaultByDataType(attribute.Datatype) + ")" : "");
                    }
                }
                else if (attribute is EntityAdapterFormula)
                {
                    formula = (EntityAdapterFormula)attribute;
                    if (((!isRepresentation && !formula.LinqDefinition.IsNullOrEmpty()) || (isRepresentation && !formula.DataRelationKey.IsNullOrEmpty())) && !formula.IsUpdatable)
                    {
                        if (formula.AggregationFunction == UIAggregationFunctions.None)
                        {
                            isGroupPart = true;
                            relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + formula.Name + " = " + groupAlias + ".Key." + formula.Name;
                        }
                        else
                        {
                            relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + formula.Name + " = " + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? "(has" + attribute.Name + ".Contains(true) || !hasNo" + attribute.Name + ".Contains(true) ? " : "") + (attribute.AggregationFunction == UIAggregationFunctions.CountDistinct ? groupAlias + (attribute.CountDistinctFilter.IsNullOrEmpty() ? "" : ".Where(e => " + attribute.CountDistinctFilter.Replace("this.", "e.").Replace("[ThisRef]", "e").Replace("[Value]", "e." + attribute.Name) + ")") + ".Select(e => " + (isRepresentation ? (!formula.DataRelationKey.Contains("#") ? formula.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + formula.DataRelationKey.Left("#") + "." + formula.DataRelationKey.Right(".")) : ReplaceEdmPath(formula.LinqDefinition, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ").Distinct().Count()​" : (formula.AggregationFunction == UIAggregationFunctions.Count ? groupAlias + ".Count()" : groupAlias + "." + formula.AggregationFunction.ToString().Replace("Avg", "Average") + "(e => " + (isRepresentation ? (!formula.DataRelationKey.Contains("#") ? formula.DataRelationKey.Replace(this.EntityAdapterRepresentation.Name + ".", "e.") : "e." + formula.DataRelationKey.Left("#") + "." + formula.DataRelationKey.Right(".")) : ReplaceEdmPath(formula.LinqDefinition, alias, "e", false, (this.EntityAdapterRepresentation == null && this.IsAggregationView))) + ")")) + (this.EnableMetaDataFilter && this.RemoveMeasureIfNotUsed && checkValidProps ? " : " + GetDefaultByDataType(attribute.Datatype) + ")" : "");
                        }
                    }
                }

                if (isGroupPart && !attribute.DomainName.IsNullOrEmpty())
                {
                    relationFieldsToLinq += "\r\n" + indent + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + attribute.Name + "Name = " + groupAlias + ".Key." + attribute.Name + "Name";
                }
            }

            return relationFieldsToLinq;
        }

        public EntityAdapterReferencesTargetEntityAdapter GetParentLinkRelation()
        {
            if (this.TargetEntityAdapter != null)
                return EntityAdapterReferencesTargetEntityAdapter.GetLinkToTargetEntityAdapter(this);
            else
                return null;
        }

        public EntityAdapterReferencesBaseEntityAdapter GetBaseLinkRelation()
        {
            if (this.BaseEntityAdapter != null)
                return EntityAdapterReferencesBaseEntityAdapter.GetLinkToBaseEntityAdapter(this);
            else
                return null;
        }

        public string GetTemporaryKeyName()
        {
            var temporaryKey = GetTemporaryKey();
            if (temporaryKey != null)
                return temporaryKey.Name;
            else
                return string.Empty;
        }

        public EntityAdapterProperty GetTemporaryKey()
        {
            if (!this.IsAggregationView && !this.HasDynamicPrimaryKey())
            {
                var keys = this.EntityAdapterProperties.Where(e => e.IsPK && !e.IsFK);
                if (keys.Count() == 1)
                {
                    //Verify Primary key            
                    List<EntityAdapterProperty> details = keys.Where(e => e.Datatype.InList("System.Int32", "Int32", "System.Int64", "Int64")).ToList();
                    if (details.Count == 1)
                        return details.First();
                    else
                    {
                        details = keys.Where(e => e.Datatype.InList("System.Guid", "Guid")).ToList();
                        if (details.Count == 1)
                            return details.First();
                    }
                }
            }
            return null;
        }

        public string GetEntitiesDescription()
        {
            string descriptionReturn = "", entityName;
            List<EntityAdapterProperty> details;

            //Primary key 
            var keys = this.GetAllInheritanceProperties().Where(e => e.IsPK && !e.IsFK);
            if (keys.Count() == 1)
            {
                details = keys.Where(e => e.Datatype.InList("System.Int32", "Int32", "System.Int64", "Int64")).ToList();
                if (details.Count == 1)
                    descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + this.PrimaryEntity + ":" + details[0].Name;
                else
                {
                    details = keys.Where(e => e.Datatype.InList("System.Guid", "Guid")).ToList();
                    if (details.Count == 1)
                        descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + this.PrimaryEntity + ":" + details[0].Name;
                }
            }

            //Foreign keys
            string[] fkRelations = this.GetAllInheritanceProperties().Where(e => e.IsFK && e.EdmKey.Occurs(".") == 2).Select(d => d.EdmKey.Extract(".", ".")).Distinct().ToArray();
            foreach (string fkRelation in fkRelations)
            {
                entityName = this.GetEntityNameByRelation(fkRelation);

                if (this.TargetEntityAdapter == null || this.TargetEntityAdapter.PrimaryEntity != entityName)
                {
                    details = this.GetAllInheritanceProperties().Where(e => e.IsFK && e.EdmKey.Occurs(".") == 2 && e.Datatype.IndexOf("Int32") >= 0 && e.EdmKey.Extract(".", ".") == fkRelation).ToList();
                    if (details.Count == 1)
                        descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + entityName + ":" + details[0].Name;
                    else
                    {
                        details = this.GetAllInheritanceProperties().Where(e => e.IsFK && e.EdmKey.Occurs(".") == 2 && e.Datatype.IndexOf("Guid") >= 0 && e.EdmKey.Extract(".", ".") == fkRelation).ToList();
                        if (details.Count == 1)
                            descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + entityName + ":" + details[0].Name;
                    }
                }
            }

            if (!descriptionReturn.IsNullOrEmpty())
                descriptionReturn += "]";


            descriptionReturn += ";SubQueryInfo[" + (this.TargetEntityAdapter == null ? "" : "Select 1 From #ParentAlias#." + this.TargetEntityAdapter.GetDetailRelationBySetName(this) + " as #Alias#") + "]";
            descriptionReturn += ";EdmEntityName[" + this.PrimaryEntity + "]";
            descriptionReturn += ";EntityRelations[" + this.EntityRelations + "]";
            descriptionReturn += ";EdmParentEntityName[" + (this.TargetEntityAdapter == null ? "" : this.TargetEntityAdapter.PrimaryEntity) + "]";
            descriptionReturn += ";IsIQueryable[" + (this.QueryReturnType == EntityQueryReturnType.IQueryable).ToString().ToLower() + "]";

            return descriptionReturn;
        }

        public void ConfigPropertyOrder()
        {
            CustomizedCode.FormPropertySort form = new CustomizedCode.FormPropertySort();
            form.Entity = this;
            form.ShowDialog();
        }

        public void SetPropertyOrder()
        {
            EntityAdapterProperty[] orderedList;

            switch (this.PropertyOrder)
            {
                case AttributeOrder.Name:
                    orderedList = this.EntityAdapterProperties.OrderBy(ex => ex.Name).ToArray();
                    break;
                case AttributeOrder.DisplayName:
                    orderedList = this.EntityAdapterProperties.OrderBy(ex => ex.DisplayName).ToArray();
                    break;
                case AttributeOrder.EdmKey:
                    orderedList = this.EntityAdapterProperties.OrderBy(ex => ex.EdmKey).ToArray();
                    break;
                default:
                    orderedList = this.EntityAdapterProperties.OrderBy(ex => ex.Name).ToArray();
                    break;
            }

            //Adjust order
            int order = 0;
            foreach (var element in orderedList)
            {
                this.EntityAdapterProperties.Move(element, order);
                order++;
            }
        }

        public bool HasBusinessFilters()
        {
            return this.EntityAdapterRepresentation == null && this.GetAllInheritanceAttributes().Where(e => !e.IgnoreForQuery && !e.GetEdmPath().IsNullOrEmpty() && this.ExcludeAsFilter(e)).Count() > 0;
        }

        public string GetMetaDataMaps(EntityDataModel edm, string indent)
        {
            string body = String.Empty;
            bool isKey, isDataPK, isDataFK;
            EntityAdapterProperty property;
            EntityAdapterFormula formula;
            string fkRelation, entityName, baseEntityName, field, propValid, entityBaseTypeName, entityTypeName, entityRelation;
            List<string> entityKeys = new List<string>();
            bool isAutoReference;
            List<string> keys = new List<string>();
            string keyName;

            //Inheritances
            Dictionary<string, string> derivedClasses = GetAllSourceDerivedClasses();
            var entitySets = this.EntitySets.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (entitySets.Count() > 0)
                body += "\r\n" + indent + "EdmEntityMetaData metaData;";

            body += "\r\n" + indent + "List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();";
            foreach (string entitySet in entitySets)
            {
                //Start key controllers
                entityKeys.Clear();
                keys.Clear();

                entityRelation = entitySet.Left("(");
                entityBaseTypeName = entityTypeName = entitySet.Extract("(", ")");
                if (derivedClasses.ContainsKey(entityTypeName))
                    entityBaseTypeName = derivedClasses[entityTypeName];

                body += "\r\n" + indent + @"metaData = dataMaps.Where(e => e.QualifiedEntitySetName == """ + edm.Name + "." + entityBaseTypeName + @""").FirstOrDefault();";
                body += "\r\n" + indent + "if (metaData == null)";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + "     metaData = new EdmEntityMetaData() { CheckExistence = " + this.CheckExistenceOnInserting.ToString().ToLower() + ", EdmEntityType = typeof(" + edm.TargetNamespace + "." + entityTypeName + @"), QualifiedEntitySetName = """ + edm.Name + "." + entityBaseTypeName + @""" };";
                body += "\r\n" + indent + "     dataMaps.Add(metaData);";
                body += "\r\n" + indent + "}\r\n";

                foreach (EntityAdapterAttribute attribute in this.GetAllAttributes().OrderBy(e => (e is EntityAdapterProperty ? ((EntityAdapterProperty)e).EdmKey.Length : 0)))
                {
                    if (attribute is EntityAdapterProperty)
                    {
                        property = ((EntityAdapterProperty)attribute);
                        propValid = ("." + property.EdmKey).Right("." + entityRelation + ".");
                        if (property.IsEdmKeyProperty() && !propValid.IsNullOrEmpty() && propValid.Occurs(".") <= 1)
                        {
                            if (!property.IsPK && !property.IsFK)
                            {
                                field = entityRelation + "." + property.EdmKey.Right(".");
                                if (("." + property.EdmKey).Right(field.Length) == field)
                                    body += "\r\n" + indent + @"metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey =""" + property.EdmKey + @""", Source = """ + property.Name + @""", Target = """ + property.EdmKey.Right(".") + @""", NoUpdatable = " + property.NoUpdatable.ToString().ToLower() + @", IsKey = false, IsFK = false, QualifiedEntitySetName = """ + edm.Name + "." + entityBaseTypeName + @""", RelationPropertyName = """ + entityRelation + @""" });";
                            }
                            else
                            {
                                fkRelation = ("." + (property.EdmKey + " ").Left("." + property.EdmKey.Right(".") + " ")).Right(".");
                                isKey = !property.TargetKeyName.IsNullOrEmpty() || entitySet.Extract("[", "]").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(property.EdmKey.Right("."));
                                entityName = this.GetEntityNameByRelation(fkRelation);
                                if (entityName.IsNullOrEmpty())
                                    entityName = fkRelation;

                                isAutoReference = false;
                                if (isKey)
                                {
                                    if (!entityKeys.Contains(entitySet + "|" + entityName + "." + property.EdmKey.Right(".")))
                                        entityKeys.Add(entitySet + "|" + entityName + "." + property.EdmKey.Right("."));
                                    else
                                    {
                                        isAutoReference = (entityName == entityTypeName);
                                    }
                                }

                                baseEntityName = entityName;
                                if (derivedClasses.ContainsKey(entityName))
                                    baseEntityName = derivedClasses[entityName];
                                keyName = (property.TargetKeyName.IsNullOrEmpty() ? property.EdmKey.Right(".") : property.TargetKeyName);
                                isDataPK = (isKey && !isAutoReference && !keys.Contains(keyName));
                                isDataFK = (isAutoReference || (entityName != entityTypeName));
                                if (isDataPK && isDataFK && (property.Datatype.ToLower().Contains("nullable") || property.Datatype.ToLower().Contains("?")))
                                    isDataPK = false;
                                body += "\r\n" + indent + @"metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey =""" + property.EdmKey + @""", Source = """ + property.Name + @""", Target = """ + property.EdmKey.Right(".") + @""", TargetKeyName = """ + (!property.TargetKeyName.IsNullOrEmpty() && isDataFK ? property.TargetKeyName : ((isKey && !isAutoReference) && (edm.Name + "." + entityName != edm.Name + "." + entityTypeName) ? property.EdmKey.Right(".") : String.Empty)) + @""", NoUpdatable = " + property.NoUpdatable.ToString().ToLower() + ", IsKey = " + (isDataPK).ToString().ToLower() + @", IsFK = " + isDataFK.ToString().ToLower() + @", QualifiedEntitySetName = """ + edm.Name + "." + baseEntityName + @""", RelationPropertyName = """ + fkRelation + @""" });";
                                if (isDataPK)
                                    keys.Add(keyName);
                            }
                        }
                    }
                    else if (attribute is EntityAdapterFormula)
                    {
                        formula = ((EntityAdapterFormula)attribute);
                        if (!formula.LinqDefinition.IsNullOrEmpty() && formula.IsUpdatable)
                        {
                            field = entityRelation + "." + formula.LinqDefinition.Right(".");
                            if (("." + formula.LinqDefinition).Right(field.Length) == field)
                            {
                                body += "\r\n" + indent + @"metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { Source = """ + formula.Name + @""", Target = """ + formula.LinqDefinition.Right(".") + @""", NoUpdatable = " + formula.NoUpdatable.ToString().ToLower() + @", IsKey = false, IsFK = false, QualifiedEntitySetName = """ + edm.Name + "." + entityBaseTypeName + @""", RelationPropertyName = """ + entityRelation + @""" });";
                            }
                        }
                    }
                }
            }

            if (this.ReverseInsertOrder || (!this.SecondaryEntities.IsNullOrEmpty() && this.BaseEntityAdapter != null))
                body += "\r\n\r\n" + indent + "dataMaps.Reverse();";

            body += "\r\n\r\n" + indent + "return dataMaps;";
            return body;
        }

        public bool IsInconsistentPrimaryKey(EntityAdapterProperty property)
        {
            return property.IsPK && (this.GetCurrentDataModel() == null || this.EntitySets.IsNullOrEmpty()) && property.IsNullable();
        }

        //It's a primary key
        public bool IsPrimaryKey(EntityAdapterProperty property)
        {
            if (this.HasDynamicPrimaryKey())
                return false;

            if (property.IsNullable() || !property.IsPK)
                return false;

            var entity = property.EntityAdapter;
            if (entity.IsAggregationView || entity.GetCurrentDataModel() == null || entity.EntitySets.IsNullOrEmpty())
                return property.IsPK;
            else
            {
                string keyFields = "," + entity.EntitySets.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Extract("[", "]") + ",";
                return keyFields.Contains("," + property.EdmKey.Right(".").Trim() + ",") || (!property.TargetKeyName.IsNullOrEmpty() && keyFields.Contains("," + property.TargetKeyName + ","));
            }
        }

        public EntityAdapterAttribute GetAttributeFromComposition(string propertyName)
        {
            EntityAdapterAttribute result = null;
            var entity = this;
            while (entity != null)
            {
                result = entity.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == propertyName);
                if (result != null)
                    break;
                else
                    entity = entity.TargetEntityAdapter;
            }

            return result;
        }

        public string GetOrderByCommand()
        {
            string orderField = String.Empty;

            var properties = this.GetAllInheritanceProperties();
            foreach (var propOrder in properties.Where(e => e.OrderBySequence >= 0).OrderBy(o => o.OrderBySequence))
            {
                orderField += (orderField.IsNullOrEmpty() ? String.Empty : ", ") + propOrder.Name + " " + (propOrder.OrderByOrientation == OrderByOrientationType.Ascending ? "asc" : "desc");
            }

            //Get primary keys if there is no order
            if (orderField.IsNullOrEmpty())
            {
                foreach (var propOrder in properties.Where(e => this.IsPrimaryKey(e)))
                {
                    orderField += (orderField.IsNullOrEmpty() ? String.Empty : ", ") + propOrder.Name + " " + (propOrder.OrderByOrientation == OrderByOrientationType.Ascending ? "asc" : "desc");
                }
            }

            //Get first property if there is no order
            if (orderField.IsNullOrEmpty())
            {
                var propOrder = properties.FirstOrDefault();
                if (propOrder != null)
                    orderField += (orderField.IsNullOrEmpty() ? String.Empty : ", ") + propOrder.Name + " " + (propOrder.OrderByOrientation == OrderByOrientationType.Ascending ? "asc" : "desc");
            }

            return orderField;
        }

        public bool HasPrimaryKey()
        {
            var baseClass = this.GetTopBaseClass();

            if (baseClass.CreateDynamicPrimaryKey)
                return false;

            foreach (var prop in baseClass.EntityAdapterProperties)
            {
                if (baseClass.IsPrimaryKey(prop))
                    return true;
            }
            return false;
        }

        public void AdjustDependentResources(string oldName, string newName)
        {
            this.CheckDefaultUserInterface();
            if (!oldName.IsNullOrEmpty() && !newName.IsNullOrEmpty() && oldName != newName && this.TargetEntityAdapter != null)
            {
                var parent = this.GetTopParent();
                if (parent != null)
                {
                    foreach (var ui in parent.EntityAdapterUserInterfaces)
                    {
                        bool hasChanges = false;
                        var lDef = ui.LayoutDefinition;
                        Action<LayoutElement> adjust = null;
                        adjust = (element) =>
                        {
                            if (!element.BindingPath.IsNullOrEmpty() && element.BindingPath.Contains(oldName + "PagedList"))
                            {
                                hasChanges = true;
                                element.BindingPath = element.BindingPath.Replace(oldName + "PagedList", newName + "PagedList");
                            }
                            if (element is LayoutContainer)
                                ((LayoutContainer)element).Controls.ForEach(e => adjust(e));
                        };

                        lDef.Containers.ForEach(e => adjust(e));
                        if (lDef.RemovedLayoutElements != null && lDef.RemovedLayoutElements.Count > 0)
                            lDef.RemovedLayoutElements.ForEach(e => adjust(e));

                        if (hasChanges)
                            ui.StoreCurrentlayout(lDef);
                    }
                }

            }
        }

        public bool HasIdentityPrimaryKey()
        {
            var baseClass = this.GetTopBaseClass();
            var keys = baseClass.EntityAdapterProperties.Where(prop => baseClass.IsPrimaryKey(prop)).ToArray();
            if (keys.Length == 0 || keys.Length > 1)
                return false;

            var propType = keys.First().Datatype.ToLower();

            return propType.Contains("int") || propType.Contains("long") || propType.Contains("short");
        }

        //It's a secondary key
        public bool IsSecondaryKey(EntityAdapterProperty property)
        {
            bool result = false;

            string keyFields;
            foreach (string secondary in this.SecondaryEntities.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                keyFields = "," + (" " + this.EntitySets).Extract(" " + secondary + "[", "]") + ",";
                result = keyFields.Contains("," + property.EdmKey.Right(".").Trim() + ",");
                if (result)
                    break;
            }

            return result;
        }

        public string GetPublicationAttributes(string indent)
        {
            string body = String.Empty;

            if (this.EnableForPublication)
            {
                string keys = String.Empty;

                foreach (var property in this.GetAllInheritanceProperties())
                {
                    if (this.IsPrimaryKey(property))
                        keys += (keys.IsNullOrEmpty() ? String.Empty : ",") + property.EdmKey;
                }

                if (keys.IsNullOrEmpty())
                    keys = this.Name + ".EntityUniqueKey";

                var edm = this.GetCurrentDataModel();
                body += "\r\n" + indent + @"[LinxPublicationView(PrimaryKeys=""" + keys + @""", IsUpdatable=" + this.IsUpdatableWhenPublished.ToString().ToLower() + @", EdmName=""" + (edm == null ? String.Empty : edm.TargetNamespace + "." + edm.Name) + @""")]";
            }


            foreach (var ui in this.EntityAdapterUserInterfaces.Where(e => e.SpecializedLayoutType == SpecializedLayout.IsSpecializedLookUp))
            {
                body += "\r\n" + indent + @"[LinxPublicationLookUp(NameSpace=""" + ui.NameSpace + @""", ClassName=""" + ui.Name + @""", EntityName=""" + this.PrimaryEntity + @"""" + (ui.SpecializedLayoutType == SpecializedLayout.IsSpecializedLookUp ? ", AllowsMaintenance=" + ui.IsMaintenanceLookUp.ToString().ToLower() : "") + ")]";
            }


            return body;
        }

        public string GetAttributeDefinitions(EntityAdapterAttribute fieldDef, string contextName, Dictionary<string, string> lookUpsPropInfo, string indent, int memberOrder, bool isPoco, bool hasDynamicPK, bool isFreeProperty, bool byParentComposition = false)
        {
            bool isRequired = (!isPoco && !isFreeProperty && !fieldDef.RemoveValidations && (!fieldDef.IsNull || fieldDef.IsCompulsory));
            string body = "\r\n\r\n" + indent + (fieldDef.IgnoreDataMember ? "[IgnoreDataMember()]" : "[DataMember(" + (isRequired ? "IsRequired = true, " : String.Empty) + "Name = \"" + (fieldDef.DataMemberName.IsNullOrEmpty() ? fieldDef.Name : fieldDef.DataMemberName) + "\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (memberOrder > 0 ? ", Order = " + memberOrder : "") + ")]");
            body += "\r\n" + indent + "[XmlAttribute()]";
            body += "\r\n" + indent + "[Editable(true)]";
            body += "\r\n" + indent + @"[Display(Name = """ + fieldDef.DisplayName.Replace("\t", "").Replace("\r", "").Replace("\n", "") + @""", Description=""" + fieldDef.Description.Replace("\r\n", "\\r\\n") + @""", Order = " + fieldDef.DisplayOrder.ToString() + @", AutoGenerateField = " + fieldDef.IsBrowsable.ToString().ToLower() + @", GroupName="""", ResourceType= null)]";

            //Verify PK
            if (!hasDynamicPK)
            {
                if (this.IsIndependentKey(fieldDef) || (!isFreeProperty && (fieldDef is EntityAdapterProperty) && this.IsPrimaryKey(((EntityAdapterProperty)fieldDef))))
                {
                    body += "\r\n" + indent + "[Key()]";
                }
            }

            //Automatic business attributes
            if (!isPoco)
            {
                string defaultValue = ((fieldDef is EntityAdapterProperty) ? ((EntityAdapterProperty)fieldDef).DefaultValue : "");
                string edmKey = fieldDef.GetEdmPath();

                if (byParentComposition && this.IsParentCompositionAllowed() && !edmKey.IsNullOrEmpty())
                {
                    if ((fieldDef is EntityAdapterProperty && ((EntityAdapterProperty)fieldDef).EntityAdapter != this))
                    {
                        string relation = "";
                        var parent = this;
                        while (!(parent == null || parent == ((EntityAdapterProperty)fieldDef).EntityAdapter))
                        {
                            relation += (relation.IsNullOrEmpty() ? "" : ".") + parent.GetParentRelationName();
                            parent = parent.TargetEntityAdapter;
                        }

                        if (!relation.IsNullOrEmpty())
                        {
                            int parentPrimaryEntityLength = edmKey.Left(".").Length;
                            if (parentPrimaryEntityLength > 0)
                                edmKey = this.PrimaryEntity + "." + relation + edmKey.Right(edmKey.Length - parentPrimaryEntityLength);
                        }
                    }
                }

                if (fieldDef.BrandDecimalsControl)
                    body += "\r\n" + indent + "[BrandDecimals()]";

                if (!fieldDef.Range.IsNullOrEmpty())
                    body += "\r\n" + indent + "[Range(" + fieldDef.Range + ")]";

                //Str Len
                if (!isFreeProperty && !fieldDef.RemoveValidations)
                {
                    int strlen = (fieldDef.Precision.Contains(":") ? int.Parse(fieldDef.Precision.Left(":")) : (int.Parse(fieldDef.Precision) / 10));
                    if (strlen > 0 && !fieldDef.Datatype.Contains("[]") && fieldDef.Datatype.ToLower().Contains("string"))
                        body += "\r\n" + indent + "[LinxStringLength(" + strlen.ToString() + ")]";
                }

                body += "\r\n" + indent + @"[FunctionalPoint(""Precision[" + fieldDef.Precision + "];IsEditable[" + fieldDef.IsEditable.ToString().ToLower() + "];CustomMediaTable[" + fieldDef.CustomMediaTable + "];IsAutomaticSequency[" + (fieldDef is EntityAdapterProperty && this.HasAutomaticSequency((EntityAdapterProperty)fieldDef)).ToString().ToLower() + "];IsNull[" + fieldDef.IsNull.ToString().ToLower() + "];DomainName[" + fieldDef.DomainName + "];KpiName[" + fieldDef.KpiName + "];KpiRelatedAttribute[" + fieldDef.KpiRelatedAttribute + "];DefaultValue[" + defaultValue.Replace(@"""", @"\""") + "];DataFormatString[" + fieldDef.DataFormatString + "];OrderByOrientation[" + (fieldDef is EntityAdapterProperty ? ((EntityAdapterProperty)fieldDef).OrderByOrientation : OrderByOrientationType.Ascending) + "];OrderBySequence[" + (fieldDef is EntityAdapterProperty ? ((EntityAdapterProperty)fieldDef).OrderBySequence : -1) + "];AggregationFunction[" + (this.IsAggregationView && fieldDef.AggregationFunction == UIAggregationFunctions.Count ? UIAggregationFunctions.Sum : fieldDef.AggregationFunction).ToString() + "];ObjectClass[" + fieldDef.DisplayControl + "];ConnectedField[" + fieldDef.ConnectedAttribute + @"];Mask[" + fieldDef.Mask + @"];MaskType[" + fieldDef.MaskType + @"];ExcludedAsFilter[" + ExcludeAsFilter(fieldDef).ToString().ToLower() + @"]" + this.GetLookUpClientBinding(fieldDef) + ";FilterDataKey[" + edmKey + @"];IsMeasure[" + fieldDef.IsMeasure.ToString().ToLower() + @"]"")]";

            }

            if (!isFreeProperty && this.EnableForPublication)
                body += "\r\n" + indent + "[LinxPublicationField(IsSuggestion=" + (!fieldDef.IsPK && !fieldDef.IsFK && fieldDef.IsPublicationSuggestion).ToString().ToLower() + @", LookUpInfo=""" + (lookUpsPropInfo.ContainsKey(fieldDef.Name) ? lookUpsPropInfo[fieldDef.Name] : String.Empty) + @""", EdmKey=""" + fieldDef.GetEdmPath(true).Replace(@"""", @"\""") + @""")]";

            if (!isFreeProperty && !fieldDef.CustomValidationMethod.IsNullOrEmpty())
                body += "\r\n" + indent + @"[CustomValidation(typeof(" + contextName + @"CustomValidation), """ + fieldDef.CustomValidationMethod + @""")]";

            body += Linx.EntityAdapterDesigner.EntityAdapterDesignerRoot.GetCustomAttributes(indent, fieldDef.CustomAttributes);

            return body;
        }


        private string GetLookUpClientBinding(EntityAdapterAttribute fieldDef)
        {
            string result = "";
            string lookupName = fieldDef.GetLookUpName();

            if (!lookupName.IsNullOrEmpty())
            {
                var lookup = this.GetAllLookUpsInfo(true).FirstOrDefault(e => e.Name == lookupName);
                if (lookup != null)
                {
                    result += ";LookUpName[" + lookup.Name + "]";
                    result += ";LookUpTitle[Seleção de (" + fieldDef.DisplayName + ")]";
                    result += ";LookUpQuery[execute" + lookup.Name + "]";
                    result += ";LookUpFinalize[finalize" + lookup.Name + "]";

                    var props = lookup.Properties.OrderBy(e => e.Order).ToArray();
                    result += ";LookUpDisplayColumns[{";
                    for (int idx = 0; idx < props.Length; idx++)
                    {
                        result += (idx == 0 ? "" : ", ") + "\\\"" + props[idx].Name + "\\\" : \\\"" + props[idx].DisplayName + "\\\"";
                    }
                    result += "}]";

                    result += ";LookUpColumns[{";
                    for (int idx = 0; idx < props.Length; idx++)
                    {
                        result += (idx == 0 ? "" : ", ") + "\\\"" + props[idx].Name + "\\\" : " + props[idx].IsBrowsable.ToString().ToLower();
                    }
                    result += "}]";
                }
            }

            return result;
        }

        //Get derived classes for KnownTypeAttribute
        public string GetDerivedAttributeDefinitions(string indent)
        {
            string body = String.Empty;

            foreach (var derivedClass in this.DerivedEntityAdapters)
            {
                body += "\r\n" + indent + "[KnownType(typeof(" + derivedClass.Name + "))]";
            }

            return body;
        }

        public bool HasEdmPath(EntityAdapterAttribute fieldDef)
        {
            if (fieldDef is EntityAdapterProperty)
                return System.Text.RegularExpressions.Regex.IsMatch(((EntityAdapterProperty)fieldDef).EdmKey, "(?<![a-zA-Z0-9_@])" + this.PrimaryEntity + ".");

            if (fieldDef is EntityAdapterPublicationProperty)
                return System.Text.RegularExpressions.Regex.IsMatch(((EntityAdapterPublicationProperty)fieldDef).EdmKey, "(?<![a-zA-Z0-9_@])" + this.PrimaryEntity + ".");

            if (fieldDef is EntityAdapterFormula && ((EntityAdapterFormula)fieldDef).IsUpdatable)
                return System.Text.RegularExpressions.Regex.IsMatch(((EntityAdapterFormula)fieldDef).LinqDefinition, "(?<![a-zA-Z0-9_@])" + this.PrimaryEntity + ".");

            return false;
        }

        public bool ExcludeAsFilter(EntityAdapterAttribute fieldDef)
        {
            if (fieldDef.IgnoreForQuery)
                return true;

            if (fieldDef.ForceAsFilter)
                return false;

            if (MacroEngineHelper.HasMacro(fieldDef.GetEdmPath(true), this))
                return false;

            if (fieldDef.IsCustomized)
                return true;

            if (!fieldDef.GetModelViewSource().IsNullOrEmpty())
                return false;

            if (!HasEdmPath(fieldDef))
                return true;

            return ((this.IsAggregationView && fieldDef.AggregationFunction != UIAggregationFunctions.None) || !(fieldDef is EntityAdapterProperty || fieldDef is EntityAdapterPublicationProperty || (fieldDef is EntityAdapterFormula && ((EntityAdapterFormula)fieldDef).IsUpdatable)));
        }

        public string GetDefinitionsForEntityInstanceByName(string indent)
        {
            string body = String.Empty;

            body += "\r\n" + indent + "System.ServiceModel.DomainServices.Client.Entity result = null;";
            body += "\r\n" + indent;
            body += "\r\n" + indent + @"switch (entityName)";
            body += "\r\n" + indent + @"{";

            GetAllCaseForComposition(ref body, indent, "");

            body += "\r\n" + indent + @"   default:";
            body += "\r\n" + indent + @"        break;";
            body += "\r\n" + indent + @"}";
            body += "\r\n" + indent + @"";
            body += "\r\n" + indent + @"return result;";

            return body;
        }

        public void GetAllCaseForComposition(ref string body, string indent, string parentRef)
        {
            string reference = (parentRef.IsNullOrEmpty() ? "this" : parentRef + "." + this.Name + "List.First()");

            body += "\r\n" + indent + @"    case """ + this.Name + @""":";
            body += "\r\n" + indent + @"        result = " + reference + ";";
            body += "\r\n" + indent + @"        break;";

            foreach (var detail in this.SourceEntityAdapters)
                detail.GetAllCaseForComposition(ref body, indent, reference);
        }

        private string GetLookUpRelatedFieldName(string entityName, string edmFieldName)
        {
            string field = String.Empty;
            var lookUp = this.LookUpAdapters.Where(e => e.RelationName == entityName && e.EntitySource == entityName).FirstOrDefault();
            if (!lookUp.IsNull())
            {
                var prop = lookUp.LookUpProperties.Where(e => e.EdmKey == edmFieldName).FirstOrDefault();
                if (!lookUp.LookUpProperties.Where(e => e.EdmKey == edmFieldName).IsNull())
                    field = prop.EntityPropertyRelated;
            }

            return field;
        }

        public string GetClientContextPropertyReference(string contextName, string indent)
        {
            string body = "";

            if (this.BaseEntityAdapter == null)
            {
                body += "\r\n" + indent + "#region Client Context Reference";
                body += "\r\n" + indent + "[IgnoreDataMember()]";
                body += "\r\n" + indent + "[XmlIgnore()]";
                if (this.TargetEntityAdapter == null)
                {
                    body += "\r\n" + indent + "public " + contextName + "DomainContext ClientContext { get; set; }";
                }
                else
                {
                    body += "\r\n" + indent + "public " + contextName + "DomainContext ClientContext { get { return (this." + this.TargetEntityAdapter.Name + " == null ? null : this." + this.TargetEntityAdapter.Name + ".ClientContext); } set { if (this." + this.TargetEntityAdapter.Name + " != null) { this." + this.TargetEntityAdapter.Name + ".ClientContext = value; } } }";
                }
                body += "\r\n" + indent + "#endregion Client Context Reference";
            }

            return body;
        }

        /// <summary>
        /// Get call for local view events.
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        public string GetLocalEventsCall(string indent)
        {
            string body = "";

            if (this.LocalResultEntityAdapters.Count > 0)
            {
                body += "\r\n" + indent + "_" + this.Name.Left(1).ToLower() + this.Name.Substring(1) + "PagedList.CollectionChanged += (sender, e) => { ";
                foreach (EntityAdapter entity in this.LocalResultEntityAdapters)
                {
                    body += " Load" + entity.Name + "LocalView();";
                }
                body += " };";
            }

            return body;
        }

        public string GetSurrogateSuport(string indent, bool isDbContext)
        {
            string body = "";

            if (!this.SurrogateProperty.IsNullOrEmpty())
            {
                EntityAdapterAttribute prop = this.GetAllInheritanceProperties().Where(e => e.Name == this.SurrogateProperty && e.Datatype.ToLower().Contains("bool")).FirstOrDefault();
                if (prop == null)
                    prop = this.GetAllInheritancePublicationProperties().Where(e => e.Name == this.SurrogateProperty && e.Datatype.ToLower().Contains("bool")).FirstOrDefault();

                if (prop != null)
                {
                    body += "\r\n" + indent + "//New copy of entity for surrogate support";
                    body += "\r\n" + indent + this.Name + " newEntity = new " + this.Name + "();";
                    body += "\r\n" + indent + "newEntity.CopyFrom(entity);";
                    body += "\r\n" + indent + "newEntity.OnAfterAdd();";
                    body += "\r\n" + indent + "newEntity." + this.SurrogateProperty + " = false;";
                    body += "\r\n" + indent + "newEntity.ApplyChanges(this." + (isDbContext ? "DbContext" : "ObjectContext") + ", null, ChangeOperation.Insert, null);";
                    body += "\r\n" + indent + "//Revert changes";
                    body += "\r\n" + indent + "entity.CopyInstanceFrom(this.GetChangeSet().GetOriginal<" + this.Name + ">(entity));";
                    body += "\r\n" + indent + "entity." + this.SurrogateProperty + " = true;";
                }
            }

            return body;
        }

        private bool ExistsInLocalView(string propName)
        {
            foreach (EntityAdapter entity in this.LocalResultEntityAdapters)
            {
                if (entity.EntityAdapterProperties.Where(e => e.DataRelationKey == this.Name + "." + propName).Count() > 0)
                    return true;
            }
            return false;
        }

        protected override ModelElement ChooseMergeTarget(ElementGroup elementGroup)
        {
            return base.ChooseMergeTarget(elementGroup);
        }

        public string GetFillDetails(string contextName, string indent)
        {
            string body = "";

            body += "\r\n" + indent + "public" + (this.BaseEntityAdapter == null ? " virtual" : " override") + " void FillDetails(" + contextName + "DomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)";
            body += "\r\n" + indent + "{";
            if (this.BaseEntityAdapter != null)
                body += "\r\n" + indent + "  base.FillDetails(context, serializedEntitySearch, jEntitySearch);";

            foreach (EntityAdapter entity in this.SourceEntityAdapters.Where(e => e.LocalEntityAdapter == null))
            {
                body += "\r\n" + indent + "  if (viewNames == null || viewNames.Contains(\"" + entity.Name + "\"))";
                body += "\r\n" + indent + "  {";
                body += "\r\n" + indent + "     List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));";
                string childES = entity.GetParentRelationToLinqForEntity("childES", "this", true, indent + "     ");
                if (!childES.IsNullOrEmpty())
                {
                    body += childES;
                    body += "\r\n" + indent + "     queryFilters.Add(childES);";
                }
                body += "\r\n" + indent + "     string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);";

                body += "\r\n" + indent + "     //Load " + entity.Name + " and all sub-details";

                body += "\r\n" + indent + "     if (this." + entity.Name + "List == null || this." + entity.Name + "List.Count() == 0)";
                body += "\r\n" + indent + "     {";
                body += "\r\n" + indent + "         if (take > 0)";
                body += "\r\n" + indent + "             this." + entity.Name + "List = context.GetPaged" + entity.Name + "(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();";
                body += "\r\n" + indent + "         else";
                body += "\r\n" + indent + "             this." + entity.Name + "List = (from r in context.Get" + entity.Name + "ByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();";
                body += "\r\n" + indent + "     }";
                if (entity.HasDetails())
                {
                    body += "\r\n" + indent + "     foreach(" + entity.Name + " detail in this." + entity.Name + "List)";
                    body += "\r\n" + indent + "     {";
                    body += "\r\n" + indent + "         detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);";
                    body += "\r\n" + indent + "     }";
                }
                body += "\r\n" + indent + "  }";
            }

            body += "\r\n" + indent + "}";

            return body;
        }


        public string GetFlatEntities(string indent)
        {
            string body = "";

            body += "\r\n" + indent + "public" + (this.BaseEntityAdapter == null ? " virtual" : " override") + " List<object> GetFlatEntities()";
            body += "\r\n" + indent + "{";
            if (this.BaseEntityAdapter != null)
                body += "\r\n" + indent + "  List<object> result = base.GetFlatEntities();";
            else
                body += "\r\n" + indent + "  List<object> result = new List<object>() { this };";

            foreach (EntityAdapter entity in this.SourceEntityAdapters.Where(e => e.LocalEntityAdapter == null))
            {
                body += "\r\n" + indent + "  if (this." + entity.Name + "List != null && this." + entity.Name + "List.Count() > 0)";
                body += "\r\n" + indent + "  {";
                body += "\r\n" + indent + "     foreach (var entity in this." + entity.Name + "List)";
                body += "\r\n" + indent + "     {";
                body += "\r\n" + indent + "         result.AddRange(entity.GetFlatEntities());";
                body += "\r\n" + indent + "     }";
                body += "\r\n" + indent + "  }";
            }
            body += "\r\n" + indent + "  return result;";
            body += "\r\n" + indent + "}";


            body += "\r\n\r\n" + indent + "public" + (this.BaseEntityAdapter == null ? " virtual" : " override") + " void ResetDetails()";
            body += "\r\n" + indent + "{";
            if (this.BaseEntityAdapter != null)
                body += "\r\n" + indent + "  base.ResetDetails();";

            foreach (EntityAdapter entity in this.SourceEntityAdapters.Where(e => e.LocalEntityAdapter == null))
            {
                body += "\r\n" + indent + "  if (this." + entity.Name + "List != null)";
                body += "\r\n" + indent + "  {";
                body += "\r\n" + indent + "     foreach (var detail in this." + entity.Name + "List)";
                body += "\r\n" + indent + "     {";
                body += "\r\n" + indent + "        detail.ResetDetails();";
                body += "\r\n" + indent + "     }";
                body += "\r\n" + indent + "     this." + entity.Name + "List = null;";
                body += "\r\n" + indent + "  }";
            }
            body += "\r\n" + indent + "}";

            if (this.GetTopParent().IsBufferSaving())
            {
                body += "\r\n\r\n" + indent + "public" + (this.BaseEntityAdapter == null ? " virtual" : " override") + " void ResetChangeState()";
                body += "\r\n" + indent + "{";
                if (this.BaseEntityAdapter != null)
                    body += "\r\n" + indent + "  base.ResetChangeState();";
                else
                    body += "\r\n" + indent + "  this.ChangeState = \"N\";";

                foreach (EntityAdapter entity in this.SourceEntityAdapters.Where(e => e.LocalEntityAdapter == null))
                {
                    body += "\r\n" + indent + "  if (this." + entity.Name + "List != null)";
                    body += "\r\n" + indent + "  {";
                    body += "\r\n" + indent + "     foreach (var detail in this." + entity.Name + "List.ToArray())";
                    body += "\r\n" + indent + "     {";
                    body += "\r\n" + indent + "        detail.ResetChangeState();";
                    body += "\r\n" + indent + "     }";
                    body += "\r\n" + indent + "  }";
                }
                body += "\r\n" + indent + "}";

            }


            return body;
        }

        public string GetLoadParent(string contextName, string indent)
        {
            string body = "";

            if (this.TargetEntityAdapter != null)
            {
                body += "\r\n" + indent + "public void LoadParent(" + contextName + "DomainService context)";
                body += "\r\n" + indent + "{";

                var entity = this.TargetEntityAdapter;
                body += "\r\n" + indent + "     List<EntitySearch> queryFilters = new List<EntitySearch>();";
                string parentSearch = this.GetParentRelationToLinqForEntity("parentSearch", "this", true, indent + "     ", true);
                if (!parentSearch.IsNullOrEmpty())
                {
                    body += parentSearch;
                    body += "\r\n" + indent + "     queryFilters.Add(parentSearch);";
                }
                body += "\r\n" + indent + "     string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);";

                body += "\r\n" + indent + "     //Load " + entity.Name;
                body += "\r\n" + indent + "     this." + entity.Name + " = (from r in context.Get" + entity.Name + "ByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();";

                body += "\r\n" + indent + "}";
            }

            return body;
        }

        public string GetGetEntityByKey(string contextName, string indent)
        {
            string body = "";

            var entity = this;
            var pKeys = entity.EntityAdapterProperties.Where(e => entity.IsPrimaryKey(e)).ToList();
            if (pKeys.Count() > 0)
            {
                body += "\r\n" + indent + "[Ignore]";
                body += "\r\n" + indent + "public " + entity.Name + " Get" + entity.Name + "ByKey(" + String.Join(", ", pKeys.Select(p => p.Datatype + " " + p.Name.ToCamelCase())) + ")";
                body += "\r\n" + indent + "{";

                body += "\r\n" + indent + "     List<EntitySearch> queryFilters = new List<EntitySearch>();";

                for (int index = 0; index < pKeys.Count; index++)
                {
                    if (index == 0)
                        body += "\r\n" + indent + "     EntitySearch search = new EntitySearch(\"" + this.Name + "\");";
                    else
                        body += "\r\n" + indent + "     search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, \"&&\"));";

                    body += "\r\n" + indent + "     search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, \"" + pKeys[index].Name + "\"));";
                    body += "\r\n" + indent + "     search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, \"==\"));";
                    body += "\r\n" + indent + "     search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, " + pKeys[index].Name.ToCamelCase() + "));";
                }

                body += "\r\n" + indent + "     queryFilters.Add(search);";
                body += "\r\n" + indent + "     string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);";

                body += "\r\n" + indent + "     return (from r in this.Get" + entity.Name + "ByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();";

                body += "\r\n" + indent + "}";
            }

            return body;
        }


        public string GetInjectionClientsideCodeAttribute(string indent)
        {
            string body = "", bodyForReturn = "";

            //Insert Code Into GetIdentity 
            foreach (EntityAdapterProperty fieldDef in this.EntityAdapterProperties.Where(e => this.IsPrimaryKey(e) && e.Datatype.ToLower().Contains("string")))
            {
                body += "\r\n" + indent + indent + "    if (_" + fieldDef.Name.Left(1).ToLower() + fieldDef.Name.Substring(1) + " == null) _" + fieldDef.Name.Left(1).ToLower() + fieldDef.Name.Substring(1) + " =  String.Empty;";
            }

            if (!body.IsNullOrEmpty())
                bodyForReturn += "\r\n" + indent + @"[InjectClientsideCode(""GetIdentity"", @""" + body + @""")]";


            return bodyForReturn;
        }

        public bool HasAutomaticSequency(EntityAdapterProperty property)
        {
            var parentRelation = this.GetParentLinkRelation();
            if (parentRelation != null && ("," + parentRelation.DetailKeyFields.Replace(" ", "") + ",").Contains("," + property.Name + ","))
                return false;

            return (property.IsAutomaticSequency || (!this.CreateDynamicPrimaryKey && !this.EnableMetaDataFilter && !this.IsAggregationView && this.LocalEntityAdapter == null && (((this.IsPrimaryKey(property) && !property.IsFK) || this.IsSecondaryKey(property)) && !this.GetFieldsRelatedWithParent().Contains("," + property.Name + ","))));
        }

        public bool HasDynamicPrimaryKey()
        {
            var baseClass = this.GetTopBaseClass();

            if (baseClass.CreateDynamicPrimaryKey)
                return true;
            else
                return false;
        }

        public string GetDefaultValuesDefinition(string indent)
        {
            string body = "", parameterName, pkDataType;

            if (this.BaseEntityAdapter == null && this.HasDynamicPrimaryKey())
                body += "\r\n" + indent + "this.EntityUniqueKey = Guid.NewGuid();";

            foreach (EntityAdapterAttribute fieldDef in this.GetAllAttributes())
            {
                if (fieldDef is EntityAdapterProperty)
                {
                    if (this.HasAutomaticSequency((EntityAdapterProperty)fieldDef))
                    {
                        pkDataType = ("." + fieldDef.Datatype.ToLower()).Right(".");
                        if (pkDataType.Contains("nullable<"))
                            pkDataType = pkDataType.Extract("nullable<", ">");
                        switch (pkDataType)
                        {
                            case "guid":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = Guid.NewGuid();";
                                break;
                            case "int16":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = Math.Abs(BitConverter.ToInt16(Guid.NewGuid().ToByteArray(), 0));";
                                break;
                            case "int32":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = Math.Abs(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));";
                                break;
                            case "int64":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));";
                                break;
                            case "uint16":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = BitConverter.ToUInt16(Guid.NewGuid().ToByteArray(), 0);";
                                break;
                            case "uint32":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);";
                                break;
                            case "uint64":
                                body += "\r\n" + indent + "this." + fieldDef.Name + " = BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 0);";
                                break;
                            default:
                                break;
                        }
                    }

                    if (!((EntityAdapterProperty)fieldDef).DefaultValue.IsNullOrEmpty())
                    {
                        parameterName = ((EntityAdapterProperty)fieldDef).DefaultValue.Extract("[", "]");
                        if (parameterName.IsNullOrEmpty())
                        {
                            body += "\r\n" + indent + "this." + fieldDef.Name + " = " + ((EntityAdapterProperty)fieldDef).DefaultValue + ";";
                        }
                    }
                }
            }

            return body;
        }



        public string GetLinqDefinitionsByParentComposition(string alias, string indent)
        {

            string body = "", entityParent = alias;
            EntityAdapter detail = this, parent = this.TargetEntityAdapter;
            List<string> attributes = new List<string>();
            attributes.AddRange(GetAllInheritanceAttributes().Select(e => e.Name));

            while (parent != null)
            {
                var fields = detail.GetParentLinkRelation().DetailKeyFields.Split(new char[] { ',' });
                if (fields.Length > 0)
                {
                    var propertyRelation = detail.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == fields[0].Trim());
                    if (propertyRelation != null)
                    {
                        var bmParentRelation = (propertyRelation is EntityAdapterProperty ? ((EntityAdapterProperty)propertyRelation).EdmKey : (propertyRelation is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)propertyRelation).EdmKey : ((EntityAdapterFormula)propertyRelation).LinqDefinition));
                        bmParentRelation = ("#" + bmParentRelation).Right("#" + detail.PrimaryEntity + ".").Left(".");
                        var bmParentName = ("#" + detail.EntityRelations).Extract("#" + bmParentRelation + "(", ")");

                        if (!bmParentRelation.IsNullOrEmpty() && !bmParentName.IsNullOrEmpty())
                        {
                            entityParent += "." + bmParentRelation;
                            var properties = parent.GetAllInheritanceAttributes();
                            body += "\r\n" + indent + "//" + parent.Name + " Properties.";
                            foreach (EntityAdapterAttribute fieldDef in properties.Where(e => !(e is EntityAdapterFormula) && !attributes.Contains(e.Name)))
                            {
                                attributes.Add(fieldDef.Name);
                                string edmPath = fieldDef.GetEdmPath(true);
                                if (!edmPath.IsNullOrEmpty())
                                {
                                    if (!fieldDef.IsCustomized && edmPath.StartsWith(parent.PrimaryEntity + "."))
                                    {
                                        if (edmPath.Left(".") == bmParentName)
                                            edmPath = ("#" + edmPath).Right("#" + bmParentName + ".");
                                        edmPath = entityParent + "." + edmPath;
                                    }
                                    else
                                    {
                                        string replaceKey = "";
                                        if (edmPath.Contains(parent.PrimaryEntity + "."))
                                        {
                                            replaceKey = parent.PrimaryEntity;
                                        }

                                        if (!replaceKey.IsNullOrEmpty())
                                        {
                                            string input = edmPath;
                                            string pattern = @"\b" + replaceKey + @"\b";
                                            string value = entityParent;

                                            if (parent.PrimaryEntity != bmParentName)
                                                value += "." + parent.PrimaryEntity;

                                            edmPath = System.Text.RegularExpressions.Regex.Replace(input, pattern, value);
                                        }
                                    }

                                    //Replace macros
                                    edmPath = ReplaceEdmPath(edmPath, alias);

                                    body += "\r\n" + indent + ", " + fieldDef.Name + " = " + edmPath;

                                    if (!fieldDef.DomainName.IsNullOrEmpty())
                                    {
                                        string domainExp = GetDomainNameLinqExpression(fieldDef, edmPath);
                                        if (!domainExp.IsNullOrEmpty())
                                            body += "\r\n" + indent + (body.IsNullOrEmpty() ? "" : ", ") + fieldDef.Name + "Name = " + domainExp;
                                    }

                                }
                            }

                            detail = parent;
                            parent = detail.TargetEntityAdapter;
                        }
                        else break;
                    }
                    else break;
                }
                else break;
            }

            return body;
        }

        public string GetPropertyDefinitions(string contextName, string indent)
        {
            return GetPropertyDefinitions(contextName, indent, false);
        }

        public string GetPropertyDefinitions(string contextName, string indent, bool byParentComposition)
        {
            return GetPropertyDefinitions(contextName, indent, byParentComposition, new List<string>());
        }

        public bool IsParentCompositionAllowed()
        {
            bool result = (this.ParentCompositionEnabled && !this.IsModelView && !this.IsDashboardFilter && !this.IsAggregationView && !this.IsOlap() && !this.ExistsEvent("OnSearchingReplacement") && !this.PrimaryEntity.IsNullOrEmpty() && this.EntityAdapterRepresentation == null && this.LocalEntityAdapter == null);
            if (result)
            {
                EntityAdapter parent = this.TargetEntityAdapter;
                while (!parent.IsNull())
                {
                    result = (parent.ParentCompositionEnabled && !parent.IsDashboardFilter && !parent.IsAggregationView && !parent.IsOlap() && !parent.ExistsEvent("OnSearchingReplacement") && !parent.PrimaryEntity.IsNullOrEmpty() && parent.EntityAdapterRepresentation == null && parent.LocalEntityAdapter == null);
                    if (!result)
                        break;
                    parent = parent.TargetEntityAdapter;
                }
            }

            return result;
        }

        public bool IsIndependentKey(EntityAdapterAttribute attribute)
        {
            return (this.PrimaryEntity.IsNullOrEmpty() && attribute is EntityAdapterProperty && ((EntityAdapterProperty)attribute).IsPK) || attribute.Name == "NormalizedKey";
        }

        private bool HasEntityKeyLocalRelation()
        {
            return (this.LocalEntityAdapter != null && this.LocalEntityAdapter == this.TargetEntityAdapter);
        }

        public bool HasDefault(bool analizeBaseClass = true, bool analizeDerivedClass = true)
        {
            bool hasDefault = this.EntityAdapterProperties.Where(e => !e.DefaultValue.IsNullOrEmpty()).FirstOrDefault() != null || this.EntityAdapterPublicationProperties.Where(e => !e.DefaultValue.IsNullOrEmpty()).FirstOrDefault() != null;

            if (analizeBaseClass)
            {
                //Verify base classes
                if (!hasDefault && this.BaseEntityAdapter != null)
                {
                    hasDefault = this.BaseEntityAdapter.HasDefault(true, false);
                }
            }

            if (analizeDerivedClass)
            {
                //Verify derived classes
                if (!hasDefault)
                {
                    foreach (var entity in this.DerivedEntityAdapters)
                    {
                        hasDefault = entity.HasDefault(false, true);
                        if (hasDefault)
                            break;
                    }
                }
            }

            return hasDefault;
        }

        public string GenerateDefaults(string indent)
        {
            string body = String.Empty;

            foreach (var prop in this.EntityAdapterProperties.Where(e => !e.DefaultValue.IsNullOrEmpty() && e.DefaultValue.Extract("[", "]").IsNullOrEmpty()))
            {
                body += "\r\n" + indent + indent + prop.Name + " = " + prop.DefaultValue + ";";
            }
            foreach (var prop in this.EntityAdapterPublicationProperties.Where(e => !e.DefaultValue.IsNullOrEmpty() && e.DefaultValue.Extract("[", "]").IsNullOrEmpty()))
            {
                body += "\r\n" + indent + indent + prop.Name + " = " + prop.DefaultValue + ";";
            }

            if (!body.IsNullOrEmpty())
            {
                body = "\r\n" + indent + "if (setDefaults)" +
                       "\r\n" + indent + "{" +
                       body +
                       "\r\n" + indent + "}";
            }

            return body;
        }

        public string GetPropertyDefinitions(string contextName, string indent, bool byParentComposition, List<string> attributes, bool isPoco = false, bool generateFreeProperty = false)
        {
            if (this.IsPOCO)
                isPoco = true;

            string body = String.Empty, formula, tmpName, dataType;
            bool isFreeProperty = (attributes.Count > 0), createPropertyField, hasDynamicPK = (this.BaseEntityAdapter == null && this.HasDynamicPrimaryKey());


            //Load orders
            this.LoadMembersOrder(isPoco, byParentComposition);

            var lookUpsPropInfo = this.GetAllLookUpPropertiesInfo(true);
            foreach (EntityAdapterAttribute fieldDef in (byParentComposition ? this.GetAllInheritanceAttributes(byParentComposition).Where(e => !attributes.Contains(e.Name)) : this.GetAllAttributes().Where(e => !attributes.Contains(e.Name))))
            {
                if (generateFreeProperty && fieldDef.IgnoreDataMember)
                    continue;

                if (fieldDef.KpiRelatedAttribute.IsNullOrEmpty() && fieldDef is EntityAdapterFormula && (!((EntityAdapterFormula)fieldDef).Formula.IsNullOrEmpty()))
                    formula = ((EntityAdapterFormula)fieldDef).GetFormulaDefinition();
                else
                    formula = String.Empty;

                createPropertyField = !(isPoco && !formula.IsNullOrEmpty());
                attributes.Add(fieldDef.Name);

                dataType = (fieldDef.Datatype.IsNullOrEmpty() ? fieldDef.Datatype : fieldDef.Datatype);
                if (!isPoco)
                {
                    body += "\r\n" + indent + "//Extensibility Partial Method Definitions For " + fieldDef.Name;
                    body += "\r\n" + indent + "partial void On" + fieldDef.Name + "Changing(" + dataType + " value);";
                    body += "\r\n" + indent + "partial void On" + fieldDef.Name + "Changed();";
                }

                if (createPropertyField)
                    body += "\r\n\r\n" + indent + "private " + dataType + " _" + fieldDef.Name + ";";

                //Getting attribute definitions
                body += GetAttributeDefinitions(fieldDef, contextName, lookUpsPropInfo, indent, (!this.GenerateDataMemberOrder ? -1 : this.GetMemberOrder((fieldDef.DataMemberName.IsNullOrEmpty() ? fieldDef.Name : fieldDef.DataMemberName))), isPoco, hasDynamicPK, isFreeProperty, byParentComposition);

                body += "\r\n" + indent + "public " + dataType + " " + fieldDef.Name;
                body += "\r\n" + indent + "{";

                if (generateFreeProperty && !formula.IsNullOrEmpty())
                {
                    body += "\r\n" + indent + indent + "get; set;";
                }
                else
                {
                    body += "\r\n" + indent + indent + "get";
                    body += "\r\n" + indent + indent + "{";

                    if (createPropertyField)
                    {
                        if (!formula.IsNullOrEmpty())
                        {
                            body += "\r\n" + indent + indent + "      if (_" + fieldDef.Name + " != (" + formula + "))";
                            body += "\r\n" + indent + indent + "         _" + fieldDef.Name + " =  " + formula + ";";
                        }

                        if (formula.IsNullOrEmpty() && dataType.ToLower().Contains("string") && (this.IsIndependentKey(fieldDef) || ((fieldDef is EntityAdapterProperty && this.IsPrimaryKey(((EntityAdapterProperty)fieldDef))) || (!fieldDef.IsNull && this.IsAggregationView && !isFreeProperty && fieldDef.AggregationFunction == UIAggregationFunctions.None))))
                        {
                            body += "\r\n" + indent + indent + "      if (_" + fieldDef.Name + ".IsNullOrEmpty())";
                            body += "\r\n" + indent + indent + "         _" + fieldDef.Name + " =  String.Empty;";
                        }

                        body += "\r\n" + indent + indent + "      return _" + fieldDef.Name + ";";
                    }
                    else
                        body += "\r\n" + indent + indent + "      return " + formula + ";";


                    body += "\r\n" + indent + indent + "}";

                    if (createPropertyField)
                    {
                        body += "\r\n" + indent + indent + "set";
                        body += "\r\n" + indent + indent + "{";

                        if (!isPoco)
                        {
                            body += "\r\n" + indent + indent + "      if (this._" + fieldDef.Name + " != value)";
                            body += "\r\n" + indent + indent + "      {";
                            body += "\r\n" + indent + indent + "          this.ValidateProperty(\"" + fieldDef.Name + "\", value);";
                            body += "\r\n" + indent + indent + "          this.On" + fieldDef.Name + "Changing(value);";
                            body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanging(\"" + fieldDef.Name + "\");";
                            body += "\r\n" + indent + indent + "          this._" + fieldDef.Name + " = value;";
                            body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanged(\"" + fieldDef.Name + "\");";
                            body += "\r\n" + indent + indent + "          this.On" + fieldDef.Name + "Changed();";
                            body += "\r\n" + indent + indent + "      }";
                        }
                        else
                            body += "\r\n" + indent + indent + "      this._" + fieldDef.Name + " = value;";

                        body += "\r\n" + indent + indent + "}";
                    }
                    else body += "\r\n" + indent + indent + "internal set { }";
                }

                body += "\r\n" + indent + "}";
            }

            if (this.TargetEntityAdapter != null && this.TargetEntityAdapter.HasDynamicPrimaryKey())
            {
                body += "\r\n" + indent + "[DataMember(Name = \"EntityParentUniqueKey\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder("EntityParentUniqueKey")) + ")]";
                body += "\r\n" + indent + "[XmlAttribute()]";

                body += "\r\n" + indent + "[Editable(true)]";
                body += "\r\n" + indent + "public System.Guid EntityParentUniqueKey { get; set; }";
            }

            //Dynamic properties
            if (hasDynamicPK)
            {
                body += "\r\n" + indent + "//Extensibility Partial Method Definitions For EntityUniqueKey";
                body += "\r\n" + indent + "partial void OnEntityUniqueKeyChanging(System.Guid value);";
                body += "\r\n" + indent + "partial void OnEntityUniqueKeyChanged();";
                body += "\r\n\r\n" + indent + "private System.Guid _entityUniqueKey;";
                body += "\r\n" + indent + "[DataMember(Name = \"EntityUniqueKey\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder("EntityUniqueKey")) + ")]";
                body += "\r\n" + indent + "[XmlAttribute()]";
                body += "\r\n" + indent + "[Editable(true)]";
                body += "\r\n" + indent + "[Key()]";
                body += "\r\n" + indent + "public System.Guid EntityUniqueKey";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "get";
                body += "\r\n" + indent + indent + "{";
                if (!generateFreeProperty)
                {
                    body += "\r\n" + indent + indent + "      if (_entityUniqueKey.IsNullOrEmpty())";
                    body += "\r\n" + indent + indent + "         _entityUniqueKey =  System.Guid.NewGuid();";
                }
                body += "\r\n" + indent + indent + "      return _entityUniqueKey; ";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + indent + "set";
                body += "\r\n" + indent + indent + "{";
                body += "\r\n" + indent + indent + "      if (this._entityUniqueKey != value)";
                body += "\r\n" + indent + indent + "      {";
                if (!generateFreeProperty)
                {
                    body += "\r\n" + indent + indent + "          this.ValidateProperty(\"EntityUniqueKey\", value);";
                    body += "\r\n" + indent + indent + "          this.OnEntityUniqueKeyChanging(value);";
                    body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanging(\"EntityUniqueKey\");";
                }
                body += "\r\n" + indent + indent + "          this._entityUniqueKey = value;";
                if (!generateFreeProperty)
                {
                    body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanged(\"EntityUniqueKey\");";
                    body += "\r\n" + indent + indent + "          this.OnEntityUniqueKeyChanged();";
                }
                body += "\r\n" + indent + indent + "      }";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + "}";

            }

            //Creating local relation key
            if (this.HasEntityKeyLocalRelation() || (this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).Count() > 0 && this.LocalResultEntityAdapters.Where(e => e.LocalEntityAdapter == this).First().HasEntityKeyLocalRelation()))
            {
                body += "\r\n" + indent + "//Local parent relation key";
                if (!isPoco)
                {
                    body += "\r\n" + indent + "partial void OnEntityKeyLocalRelationChanging(int value);";
                    body += "\r\n" + indent + "partial void OnEntityKeyLocalRelationChanged();";
                }
                body += "\r\n\r\n" + indent + "private int _entityKeyLocalRelation;";
                body += "\r\n" + indent + "[DataMember(Name = \"EntityKeyLocalRelation\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder("EntityKeyLocalRelation")) + ")]";
                body += "\r\n" + indent + "[XmlAttribute()]";
                body += "\r\n" + indent + "[Editable(true)]";
                body += "\r\n" + indent + "public int EntityKeyLocalRelation";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "get";
                body += "\r\n" + indent + indent + "{";
                body += "\r\n" + indent + indent + "      return _entityKeyLocalRelation; ";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + indent + "set";
                body += "\r\n" + indent + indent + "{";
                if (!isPoco)
                {
                    body += "\r\n" + indent + indent + "      if (this._entityKeyLocalRelation != value)";
                    body += "\r\n" + indent + indent + "      {";
                    body += "\r\n" + indent + indent + "          this.ValidateProperty(\"EntityKeyLocalRelation\", value);";
                    body += "\r\n" + indent + indent + "          this.OnEntityKeyLocalRelationChanging(value);";
                    body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanging(\"EntityKeyLocalRelation\");";
                }
                body += "\r\n" + indent + indent + "          this._entityKeyLocalRelation = value;";
                if (!isPoco)
                {
                    body += "\r\n" + indent + indent + "          this.RaiseDataMemberChanged(\"EntityKeyLocalRelation\");";
                    body += "\r\n" + indent + indent + "          this.OnEntityKeyLocalRelationChanged();";
                }
                body += "\r\n" + indent + indent + "      }";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + "}";
            }

            if (!isPoco)
            {
                //Get temporary key
                if (!byParentComposition)
                {
                    var temporaryKey = GetTemporaryKey();
                    if (!temporaryKey.IsNull())
                    {
                        tmpName = "Temporary" + temporaryKey.Name;
                        body += "\r\n\r\n" + indent + "private " + temporaryKey.Datatype + " _" + tmpName + ";";
                        body += "\r\n" + indent + "[DataMember(Name = \"" + tmpName + "\", EmitDefaultValue = " + this.DataMemberEmitDefaultValue.ToString().ToLower() + (!this.GenerateDataMemberOrder ? "" : ", Order = " + this.GetMemberOrder(tmpName)) + ")]";
                        body += "\r\n" + indent + "[XmlAttribute()]";
                        body += "\r\n" + indent + "[Editable(true)]";
                        body += "\r\n" + indent + @"[Display(Name = """ + temporaryKey.DisplayName + @" (Tmp)"", Description=""Temporary Key"", Order = " + temporaryKey.DisplayOrder.ToString() + @", AutoGenerateField = false, GroupName="""", ResourceType= null)]";
                        body += "\r\n" + indent + "public " + temporaryKey.Datatype + " " + tmpName;
                        body += "\r\n" + indent + "{";
                        body += "\r\n" + indent + indent + "get";
                        body += "\r\n" + indent + indent + "{";
                        body += "\r\n" + indent + indent + "      if (this._" + tmpName + ".IsNullOrEmpty())";
                        body += "\r\n" + indent + indent + "            this._" + tmpName + " = this._" + temporaryKey.Name + ";";
                        body += "\r\n" + indent + indent + "      return this._" + tmpName + ";";
                        body += "\r\n" + indent + indent + "}";
                        body += "\r\n" + indent + indent + "set";
                        body += "\r\n" + indent + indent + "{";
                        body += "\r\n" + indent + indent + "      if (this._" + tmpName + " != value)";
                        body += "\r\n" + indent + indent + "          this._" + tmpName + " = value;";
                        body += "\r\n" + indent + indent + "}";
                        body += "\r\n" + indent + "}";
                    }
                }
            }

            body += this.GetInstancesDefinition(indent, isPoco);
            body += this.GetCollectionsDefinition(indent, isPoco);
            if (generateFreeProperty)
                body += this.GetFreeDetailsDefinition(indent, isPoco);


            return body;
        }

        public string GetSizeGridCalculators(string indent)
        {
            string body = "", tagName, totalName;

            //Total for size grid configurations
            if (!this.SizeGridConfigurations.IsNullOrEmpty())
            {
                foreach (string gridRef in this.SizeGridConfigurations.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    tagName = (gridRef + "#").Left("#");
                    totalName = ("#" + gridRef).Right("#");

                    if (!totalName.IsNullOrEmpty() && !tagName.IsNullOrEmpty())
                    {
                        body += "\r\n";
                        body += "\r\n" + indent + "private void Calculate" + totalName + "()";
                        body += "\r\n" + indent + "{";
                        body += "\r\n" + indent + indent + "this." + totalName + " = ";
                        for (int idx = 1; idx < 49; idx++)
                            body += (idx == 1 ? "" : " + ") + "this." + tagName + idx.ToString();
                        body += ";";
                        body += "\r\n" + indent + "}";
                    }
                }
            }

            return body;
        }


        /// <summary>
        /// Update Telerik MasterDetail Report
        /// </summary>
        public void UpdateMasterDetailReport()
        {
            UpdateReport(true);
        }


        /// <summary>
        /// Update Telerik Report
        /// </summary>
        public void UpdateReport(bool isMasterDetail = false)
        {
            bool generateCrossTabReport = false;
            List<string> reportChildEntities = new List<string>(), reportChildNames = new List<string>();
            if (isMasterDetail)
            {
                CustomCode.FrmReportDetailsSelector reportDetailSelector = new CustomCode.FrmReportDetailsSelector();
                reportDetailSelector.Entity = this;
                reportDetailSelector.ShowDialog();
                reportChildEntities.AddRange(reportDetailSelector.SelectedDetails);
                if (reportChildEntities.Count == 0)
                    return;
            }

            //Getting Settings             
            FormReportSettings reportSetttings = new FormReportSettings();
            reportSetttings.ChildEntities = reportChildEntities;
            reportSetttings.MainEntity = this;
            reportSetttings.ShowDialog();
            generateCrossTabReport = reportSetttings.GenerateCrossTabReport;
            string title = reportSetttings.Title;
            Dictionary<string, List<string>> propertySelection = reportSetttings.PropertySelection;
            if (title.IsNullOrEmpty())
                return;

            this.EntityAdapterDesignerRoot.UpdateReportProject();

            Project diagramProject = this.EntityAdapterDesignerRoot.GetEadProject();
            string reportProjectName = diagramProject.Name + ".Reports";
            Project reportProject = this.EntityAdapterDesignerRoot.GetProjectByName(reportProjectName);

            if (reportProject.IsNull())
                return;

            string reportName = this.TelerikReportName(reportProject);
            //Get Add Report Child Names
            if (isMasterDetail)
            {
                foreach (var reportChildEntity in reportChildEntities)
                {
                    var childEntity = this.SourceEntityAdapters.FirstOrDefault(e => e.Name == reportChildEntity);
                    reportChildNames.Add(reportName + ".Detail" + reportChildEntity);
                }
            }

            //Generate Utils
            this.EntityAdapterDesignerRoot.UpdateReportUtilsFile(reportProject);

            if (generateCrossTabReport)
            {
                string body = GenReportCSFileForCrossTab(reportName.Replace(".", ""), reportChildEntities.ToArray(), title);
                this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportName + ".cs", body);
                body = GenReportDesignerFileForCrossTab(reportName.Replace(".", ""), title, reportChildEntities.ToArray(), reportChildNames.ToArray(), propertySelection);
                this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportName + ".Designer.cs", body);
            }
            else
            {
                string body = GenReportCSFile(reportName.Replace(".", ""), true, reportChildEntities.ToArray(), title);
                this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportName + ".cs", body);
                body = GenReportDesignerFile(reportName.Replace(".", ""), true, title, reportChildEntities.ToArray(), reportChildNames.ToArray(), propertySelection);
                this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportName + ".Designer.cs", body);

                if (isMasterDetail)
                {
                    for (int idx = 0; idx < reportChildEntities.Count; idx++)
                    {
                        var reportChildEntity = reportChildEntities[idx];
                        var reportChildname = reportChildNames[idx];
                        var childEntity = this.SourceEntityAdapters.FirstOrDefault(e => e.Name == reportChildEntity);

                        body = childEntity.GenReportCSFile(reportChildname.Replace(".", ""), false, null, "");
                        this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportChildname + ".cs", body);
                        body = childEntity.GenReportDesignerFile(reportChildname.Replace(".", ""), false, "", null, null, propertySelection);
                        this.EntityAdapterDesignerRoot.UpdateProjectItemFile(reportProject, "", reportChildname + ".Designer.cs", body);
                    }
                }
            }

        }


        private string TelerikReportName(Project current, string parentName = "", string suffix = "", bool checkFileCounter = true)
        {
            var webApi = this.EntityAdapterDesignerRoot.WebApiControllers.Where(i => i.ProjectSuffix == "DS").FirstOrDefault();

            string reportName = string.Format("{0}.{1}{2}", webApi.Name, (parentName.IsNullOrEmpty() ? this.Name : parentName), suffix);

            string fileLocation = Path.Combine(Path.Combine(Path.GetDirectoryName(current.FullName), string.Empty), reportName + ".cs");
            int counter = 1;

            if (checkFileCounter)
            {
                while (true)
                {
                    if (File.Exists(fileLocation))
                    {
                        reportName = string.Format("{0}.{1}{2}{3}", webApi.Name, (parentName.IsNullOrEmpty() ? this.Name : parentName), suffix, counter);
                        fileLocation = Path.Combine(Path.Combine(Path.GetDirectoryName(current.FullName), string.Empty), reportName + ".cs");
                        counter++;
                    }
                    else
                        break;
                }
            }

            return reportName;
        }


        private string GenReportCSFile(string reportName, bool isMaster, string[] reportChildEntities, string title)
        {
            string reportNamespace = this.EntityAdapterDesignerRoot.TargetNamespace.ToString() + ".Reports";
            string eadName = this.EntityAdapterDesignerRoot.GetContextName();
            string className = string.Format("{0}DataSource", eadName);

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = cscp.CreateGenerator(sw);
            CodeGeneratorOptions cgo = new CodeGeneratorOptions();

            System.CodeDom.CodeNamespace ns = new System.CodeDom.CodeNamespace(reportNamespace);
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
            ns.Imports.Add(new CodeNamespaceImport("System.Drawing"));
            ns.Imports.Add(new CodeNamespaceImport("Telerik.Reporting"));
            ns.Imports.Add(new CodeNamespaceImport("Telerik.Reporting.Drawing"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("Linx.Tools"));

            CodeTypeDeclaration partial = new CodeTypeDeclaration(reportName);
            partial.IsPartial = true;
            partial.IsClass = true;
            partial.BaseTypes.Add("Telerik.Reporting.Report");
            if (isMaster)
            {
                partial.Comments.Add(new CodeCommentStatement("Inform here the report title."));
                partial.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(title))));
            }
            ns.Types.Add(partial);

            //Constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            //Initialize
            CodeSnippetStatement code = new CodeSnippetStatement("            InitializeComponent();"); constructor.Statements.Add(code);

            if (isMaster)
            {
                constructor.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataMember = \"Get" + this.Name + "\";"));
                constructor.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataSource = new " + className + "();"));
                constructor.Statements.Add(new CodeSnippetStatement("            this.DataSource = this.ReportDS;"));

                //Delegate
                CodeDelegateCreateExpression delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), string.Format("{0}_ItemDataBinding", reportName));
                CodeAttachEventStatement statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "ItemDataBinding", delegateReport);
                constructor.Statements.Add(statement1);

                //_ItemDataBinding
                CodeMemberMethod method = new CodeMemberMethod();
                method.Name = string.Format("{0}_ItemDataBinding", reportName);
                method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
                method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
                method.Statements.Add(new CodeSnippetStatement("            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;"));
                method.Statements.Add(new CodeSnippetStatement("            //Adjust Image Reference"));
                method.Statements.Add(new CodeSnippetStatement("            Image logoImg = null;"));
                method.Statements.Add(new CodeSnippetStatement("            if (!String.IsNullOrWhiteSpace(parameters[\"CompanyLogo\"].Value.ToString()))"));
                method.Statements.Add(new CodeSnippetStatement("            {"));
                method.Statements.Add(new CodeSnippetStatement("                try"));
                method.Statements.Add(new CodeSnippetStatement("                {"));
                method.Statements.Add(new CodeSnippetStatement("                    System.Net.WebClient wc = new System.Net.WebClient();"));
                method.Statements.Add(new CodeSnippetStatement("                    byte[] originalData = wc.DownloadData(parameters[\"CompanyLogo\"].Value.ToString());"));
                method.Statements.Add(new CodeSnippetStatement("                    System.IO.MemoryStream stream = new System.IO.MemoryStream(originalData);"));
                method.Statements.Add(new CodeSnippetStatement("                    logoImg = Bitmap.FromStream(stream);"));
                method.Statements.Add(new CodeSnippetStatement("                }"));
                method.Statements.Add(new CodeSnippetStatement("                catch { }"));
                method.Statements.Add(new CodeSnippetStatement("            }"));
                method.Statements.Add(new CodeSnippetStatement("            if (logoImg == null)"));
                method.Statements.Add(new CodeSnippetStatement("            {"));
                method.Statements.Add(new CodeSnippetStatement("                var directory = String.Empty;"));
                method.Statements.Add(new CodeSnippetStatement("                try"));
                method.Statements.Add(new CodeSnippetStatement("                {"));
                method.Statements.Add(new CodeSnippetStatement("                    directory = System.Web.HttpRuntime.BinDirectory;"));
                method.Statements.Add(new CodeSnippetStatement("                    string logoFile = System.IO.Path.Combine(directory, \"..\\\\image\\\\Linx.PNG\");"));
                method.Statements.Add(new CodeSnippetStatement("                    if (System.IO.File.Exists(logoFile))"));
                method.Statements.Add(new CodeSnippetStatement("                        logoImg = Bitmap.FromFile(logoFile);"));
                method.Statements.Add(new CodeSnippetStatement("                }"));
                method.Statements.Add(new CodeSnippetStatement("                catch { }"));
                method.Statements.Add(new CodeSnippetStatement("            }"));
                method.Statements.Add(new CodeSnippetStatement("            PictureBox1.Value = logoImg;"));
                if (reportChildEntities != null && reportChildEntities.Length > 0)
                {
                    method.Statements.Add(new CodeSnippetStatement("            ((" + className + ")this.ReportDS.DataSource).DetailsForLoading = new string[] { \"" + String.Join("\",\"", reportChildEntities) + "\" };"));
                }
                partial.Members.Add(method);

                if (reportChildEntities != null && reportChildEntities.Length > 0)
                {
                    for (int idx = 0; idx < reportChildEntities.Length; idx++)
                    {
                        string reportChildEntity = reportChildEntities[idx];
                        //SubReport NeedDataSource
                        //Delegate
                        delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), "subReport" + reportChildEntity + "_NeedDataSource");
                        statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "subReport" + reportChildEntity + ".NeedDataSource", delegateReport);
                        constructor.Statements.Add(statement1);
                        //Method
                        method = new CodeMemberMethod();
                        method.Name = "subReport" + reportChildEntity + "_NeedDataSource";
                        method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
                        method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
                        method.Statements.Add(new CodeSnippetStatement("            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;"));
                        method.Statements.Add(new CodeSnippetStatement("            Telerik.Reporting.Processing.SubReport subReportItem = sender as Telerik.Reporting.Processing.SubReport;"));
                        method.Statements.Add(new CodeSnippetStatement(string.Format(@"            IList valores = item.DataObject[""{0}List""] as IList;", reportChildEntity)));
                        method.Statements.Add(new CodeSnippetStatement("            subReportItem.InnerReport.DataSource = valores;"));
                        method.Statements.Add(new CodeSnippetStatement("            subReport" + reportChildEntity + ".Height = Unit.Inch(0.01);"));
                        partial.Members.Add(method);
                        //SubReport ItemDataBound
                        delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), "subReport" + reportChildEntity + "_ItemDataBound");
                        statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "subReport" + reportChildEntity + ".ItemDataBound", delegateReport);
                        constructor.Statements.Add(statement1);
                        method = new CodeMemberMethod();
                        method.Name = "subReport" + reportChildEntity + "_ItemDataBound";
                        method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
                        method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
                        method.Statements.Add(new CodeSnippetStatement("            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;"));
                        method.Statements.Add(new CodeSnippetStatement(string.Format(@"            IList valores = item.DataObject[""{0}List""] as IList;", reportChildEntity)));
                        method.Statements.Add(new CodeSnippetStatement("            item.Visible = (valores == null || valores.Count == 0 ? false : true);"));
                        partial.Members.Add(method);
                    }
                }

            }
            else
            {
                constructor.Statements.Add(new CodeSnippetStatement("            this.DataSource = null;"));
            }

            partial.Members.Add(constructor);
            codeGenerator.GenerateCodeFromNamespace(ns, sw, cgo);
            sw.Flush();
            string result = this.EntityAdapterDesignerRoot.GetString(ms);
            sw.Close();
            return result;

        }

        //private string GenReportDesignerFile(string reportName, bool isMaster, string reportChildEntity = null, string reportChildName = null)
        private string GenReportDesignerFile(string reportName, bool isMaster, string title, string[] reportChildEntities = null, string[] reportChildNames = null, Dictionary<string, List<string>> propertySelection = null)
        {
            string eadName = this.EntityAdapterDesignerRoot.GetContextName();
            string className = string.Format("{0}DataSource", eadName);
            string reportNamespace = this.EntityAdapterDesignerRoot.TargetNamespace.ToString() + ".Reports";
            string reportEntity = reportName;
            string reportDomainService = String.Format("{0}.{1}.{1}DomainService", reportNamespace, System.IO.Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName));

            List<string> validProperties = new List<string>();
            if (propertySelection != null && propertySelection.ContainsKey(this.Name))
                validProperties = propertySelection[this.Name];

            List<EntityAdapterAttribute> reportFields = this.GetAllInheritanceAttributes().Where(e => e.IsBrowsable && (validProperties.Count == 0 || validProperties.Contains(e.Name))).OrderBy(e => e.DisplayOrder).ToList();

            string headerFields = string.Empty;
            string detailFields = string.Empty;
            string footerFields = string.Empty;

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = cscp.CreateGenerator(sw);
            CodeGeneratorOptions cgo = new CodeGeneratorOptions();


            System.CodeDom.CodeNamespace ns = new System.CodeDom.CodeNamespace(reportNamespace);
            CodeTypeDeclaration partial = new CodeTypeDeclaration(reportName);
            partial.IsPartial = true;
            partial.IsClass = true;
            partial.Attributes = MemberAttributes.Private;
            ns.Types.Add(partial);

            //InitializeComponent
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "InitializeComponent";

            method.Statements.Add(new CodeCommentStatement("Sections"));
            method.Statements.Add(new CodeSnippetStatement("this.detailSection1 = new Telerik.Reporting.DetailSection();"));
            CodeMemberField detail = new CodeMemberField("Telerik.Reporting.DetailSection", "detailSection1");
            partial.Members.Add(detail);

            //Cabeçalho
            if (isMaster)
            {
                method.Statements.Add(new CodeSnippetStatement("this.pageHeader = new Telerik.Reporting.PageHeaderSection();"));
                CodeMemberField header = new CodeMemberField("Telerik.Reporting.PageHeaderSection", "pageHeader");
                partial.Members.Add(header);

                method.Statements.Add(new CodeSnippetStatement("this.pageFooter = new Telerik.Reporting.PageFooterSection();"));
                CodeMemberField pageFooter = new CodeMemberField("Telerik.Reporting.PageFooterSection", "pageFooter");
                partial.Members.Add(pageFooter);

                //Header
                method.Statements.Add(new CodeCommentStatement("PageHeader"));
                method.Statements.Add(new CodeSnippetStatement("this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(2D, Telerik.Reporting.Drawing.UnitType.Cm);"));
                method.Statements.Add(new CodeSnippetStatement(@"this.pageHeader.Name = ""pageHeader"";"));
                headerFields = "this.TextBoxHeader, this.TextBoxDateTime, this.PictureBox1, this.TextBoxCompanyName, this.TextBoxFilter";
                method.Statements.Add(new CodeSnippetStatement("this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;"));
                method.Statements.Add(new CodeSnippetStatement("this.pageHeader.Style.Padding.Bottom = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);"));
                //Imagem
                method.Statements.Add(new CodeCommentStatement("PictureBox1"));
                CodeMemberField headerField = new CodeMemberField("Telerik.Reporting.PictureBox", "PictureBox1");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("this.PictureBox1 = new Telerik.Reporting.PictureBox();"));
                method.Statements.Add(new CodeSnippetStatement("this.PictureBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.PictureBox1.MimeType = """";"));
                method.Statements.Add(new CodeSnippetStatement(@"this.PictureBox1.Name = ""PictureBox1"";"));
                method.Statements.Add(new CodeSnippetStatement("this.PictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.PictureBox1.Value = ""= Parameters.CompanyLogo.Value"";"));
                //Company Name
                method.Statements.Add(new CodeCommentStatement("TextBoxCompanyName"));
                headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxCompanyName");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxCompanyName = new Telerik.Reporting.TextBox();"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxCompanyName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.5999999046325684D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxCompanyName.Name = ""TextBoxCompanyName"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.1000003814697266D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxCompanyName.Style.Font.Bold = true;"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxCompanyName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxCompanyName.Value = ""= Parameters.CompanyName.Value"";"));
                //Nome Report
                method.Statements.Add(new CodeCommentStatement("TextBoxHeader"));
                headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxHeader");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxHeader = new Telerik.Reporting.TextBox();"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxHeader.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.7D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.90D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxHeader.Name = ""TextBoxHeader"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxHeader.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.800000190734863D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxHeader.Style.Font.Bold = true;"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxHeader.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);"));
                method.Statements.Add(new CodeSnippetStatement(string.Format(@"this.TextBoxHeader.Value = ""{0}"";", title)));
                //Hora
                method.Statements.Add(new CodeCommentStatement("TextBoxDateTime"));
                headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxDateTime");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxDateTime = new Telerik.Reporting.TextBox();"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxDateTime.Name = ""TextBoxDateTime"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxDateTime.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxDateTime.Value = ""= Now()"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;"));
                //Filtro
                method.Statements.Add(new CodeCommentStatement("TextBoxFilter"));
                headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxFilter");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter = new Telerik.Reporting.TextBox();"));
                footerFields = "this.TextBoxPageCount";
                method.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxFilter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.799997329711914D), Telerik.Reporting.Drawing.Unit.Cm(0.90000033378601074D));"));
                method.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxFilter.Name = ""TextBoxFilter"";"));
                method.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));"));
                method.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Style.Font.Bold = false;"));
                method.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);"));
                method.Statements.Add(new CodeSnippetStatement(@"             this.TextBoxFilter.Value = ""=Parameters.TranslatedJqueryExpression.Value"";"));

                //Rodapé
                method.Statements.Add(new CodeCommentStatement("PageFooter"));
                method.Statements.Add(new CodeSnippetStatement("this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.70000004768371582D, Telerik.Reporting.Drawing.UnitType.Cm);"));
                method.Statements.Add(new CodeSnippetStatement(@"this.pageFooter.Name = ""pageFooter"";"));
                method.Statements.Add(new CodeSnippetStatement("this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;"));
                //Páginas
                method.Statements.Add(new CodeCommentStatement("TextBoxPageCount"));
                headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxPageCount");
                partial.Members.Add(headerField);
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxPageCount = new Telerik.Reporting.TextBox();"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxPageCount.Name = ""TextBoxPageCount"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxPageCount.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                method.Statements.Add(new CodeSnippetStatement(@"this.TextBoxPageCount.Value = ""= \""Pg :\"" + PageNumber.ToString() + \""/\"" + PageCount.ToString()"";"));
                method.Statements.Add(new CodeSnippetStatement("this.TextBoxPageCount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;"));

                //Parameters
                //EntitySearchId
                method.Statements.Add(new CodeCommentStatement("Parameters"));
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter1.Name = ""EntitySearchId"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter1.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter1);"));
                //CurrentUser
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter2.Name = ""CurrentUser"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter2.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter2);"));
                //CurrentCompany
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter3.Name = ""CurrentCompany"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter3.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter3);"));
                //AuthorizationToken
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter4.Name = ""AuthorizationToken"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter4.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter4);"));
                //TransactionInfo
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter5.Name = ""TransactionInfo"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter5.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter5);"));
                //CompanyLogo
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter6.Name = ""CompanyLogo"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter6.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter6);"));
                //Company Name
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter7.Name = ""CompanyName"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter7.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter7);"));
                //Access Group
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter8.Name = ""AccessGroup"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter8.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter8);"));
                //Application
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter9.Name = ""Application"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter9.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter9);"));
                //EconomicGroup
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter10.Name = ""EconomicGroup"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter10.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter10);"));
                //Environment
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter11.Name = ""Environment"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter11.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter11);"));
                //JqueryExpression
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter12.Name = ""JqueryExpression"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter12.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter12);"));
                //CurrentUserName
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter13.Name = ""CurrentUserName"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter13.Value = """";"));
                method.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter13);"));
                //Translated JqueryExpression
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter14.Name = ""TranslatedJqueryExpression"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter14.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter14);"));
                //Branch
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter15.Name = ""Branch"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter15.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter15);"));
                //LoginMode
                method.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter16 = new Telerik.Reporting.ReportParameter();"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter16.Name = ""LoginMode"";"));
                method.Statements.Add(new CodeSnippetStatement(@"           reportParameter16.Value = "" "";"));
                method.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter16);"));

                if (reportChildEntities != null && reportChildEntities.Length > 0)
                {
                    for (int idx = 0; idx < reportChildEntities.Length; idx++)
                    {
                        var reportChildEntity = reportChildEntities[idx];
                        var reportChildName = reportChildNames[idx].Replace(".", "");

                        //SubReport1
                        CodeMemberField subReport = new CodeMemberField("Telerik.Reporting.SubReport", "subReport" + reportChildEntity);
                        partial.Members.Add(subReport);
                        method.Statements.Add(new CodeSnippetStatement("this.subReport" + reportChildEntity + " = new Telerik.Reporting.SubReport();"));
                        method.Statements.Add(new CodeSnippetStatement("            this.subReport" + reportChildEntity + ".Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.1D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(" + ((idx + 1) * 1.1D).ToString().Replace(",", ".") + "D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                        method.Statements.Add(new CodeSnippetStatement("this.subReport" + reportChildEntity + ".Name = \"subReport" + reportChildEntity + "\";"));
                        method.Statements.Add(new CodeSnippetStatement(string.Format("this.subReport" + reportChildEntity + ".ReportSource = this.{0}Child1;", reportChildEntity)));

                        CodeMemberField childReport = new CodeMemberField(reportChildName, string.Format("{0}Child1", reportChildEntity));
                        partial.Members.Add(childReport);
                        method.Statements.Add(new CodeSnippetStatement(string.Format("this.{0}Child1 = new {2}.{1}();", reportChildEntity, reportChildName, reportNamespace)));
                        method.Statements.Add(new CodeSnippetStatement(string.Format("((System.ComponentModel.ISupportInitialize)(this.{0}Child1)).BeginInit();", reportChildEntity)));
                        detailFields += (detailFields != string.Empty ? "," : "") + "this.subReport" + reportChildEntity;
                    }
                }
            }
            else
            {
                CodeMemberField group = new CodeMemberField("Telerik.Reporting.Group", "group1");
                partial.Members.Add(group);
                group = new CodeMemberField("Telerik.Reporting.GroupFooterSection", "groupFooterSection1");
                partial.Members.Add(group);
                group = new CodeMemberField("Telerik.Reporting.GroupHeaderSection", "groupHeaderSection1");
                partial.Members.Add(group);

                //Group
                method.Statements.Add(new CodeSnippetStatement("this.group1 = new Telerik.Reporting.Group();"));
                method.Statements.Add(new CodeSnippetStatement("this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();"));
                method.Statements.Add(new CodeSnippetStatement("this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();"));
                method.Statements.Add(new CodeSnippetStatement("this.group1.GroupFooter = this.groupFooterSection1;"));
                method.Statements.Add(new CodeSnippetStatement("this.group1.GroupHeader = this.groupHeaderSection1;"));
                method.Statements.Add(new CodeSnippetStatement(@"this.group1.Name = ""group1"";"));
                method.Statements.Add(new CodeSnippetStatement("this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm);"));
                method.Statements.Add(new CodeSnippetStatement(@"this.groupHeaderSection1.Name = ""groupHeaderSection1"";"));
                method.Statements.Add(new CodeSnippetStatement("this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.13229165971279144D, Telerik.Reporting.Drawing.UnitType.Cm);"));
                method.Statements.Add(new CodeSnippetStatement(@"this.groupFooterSection1.Name = ""groupFooterSection1"";"));
                method.Statements.Add(new CodeSnippetStatement("this.Groups.AddRange(new Telerik.Reporting.Group[] { this.group1 });"));
            }

            partial.Members.Add(method);

            //Add Data Source
            partial.Members.Add(new CodeMemberField("Telerik.Reporting.ObjectDataSource", "ReportDS"));
            method.Statements.Add(new CodeSnippetStatement("this.ReportDS = new Telerik.Reporting.ObjectDataSource();"));
            method.Statements.Add(new CodeSnippetStatement("this.ReportDS.DataMember = \"Get" + this.Name + "\";"));
            method.Statements.Add(new CodeSnippetStatement("this.ReportDS.DataSource = typeof(" + reportNamespace + "." + className + ");"));
            method.Statements.Add(new CodeSnippetStatement("this.ReportDS.Name = \"ReportDS\";"));

            //Source Parameters:
            if (isMaster)
                method.Statements.Add(new CodeSnippetStatement("this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] { new Telerik.Reporting.ObjectDataSourceParameter(\"reportItem\", typeof(System.Object), \"= ReportItem\")});"));


            method.Statements.Add(new CodeSnippetStatement("this.DataSource = this.ReportDS;"));


            //Campos do detalhe
            for (int i = 0; i < reportFields.Count * 2; i++)
            {
                string textBoxName = string.Format("TextBox{0}", i + 1);
                CodeMemberField reportField = new CodeMemberField("Telerik.Reporting.TextBox", textBoxName);
                partial.Members.Add(reportField);

                method.Statements.Add(new CodeSnippetStatement(string.Format("this.{0} = new Telerik.Reporting.TextBox();", textBoxName)));
            }

            method.Statements.Add(new CodeSnippetStatement("this.Style.BackgroundColor = System.Drawing.Color.White;"));
            method.Statements.Add(new CodeSnippetStatement("this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {this.detailSection1" + (isMaster ? ", this.pageHeader, this.pageFooter" : ", this.groupHeaderSection1, this.groupFooterSection1") + "});"));
            method.Statements.Add(new CodeCommentStatement("BeginInit"));
            method.Statements.Add(new CodeSnippetStatement("((System.ComponentModel.ISupportInitialize)(this)).BeginInit();"));
            //detalhe
            method.Statements.Add(new CodeSnippetStatement("this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(" + (reportChildEntities == null || reportChildEntities.Length == 0 ? "0.67D" : (reportChildEntities.Length + 1).ToString() + ".00D") + ");"));
            method.Statements.Add(new CodeSnippetStatement("this.detailSection1.KeepTogether = true;"));
            method.Statements.Add(new CodeSnippetStatement(@"this.detailSection1.Name = ""detailSection1"";"));

            //Fields
            int textCounter = 1;
            double totalSize = 0;
            double pageSize = 20;

            foreach (EntityAdapterAttribute field in reportFields)
            {
                double fieldSize = (field.DisplayName.Length * 0.20);
                string precision = (field.Precision.Contains(":") ? field.Precision.Replace(":", ",") : (Double.Parse(field.Precision) / 10.00).ToString());
                if (fieldSize < ((Double.Parse(precision)) * 0.12))
                    fieldSize = ((Double.Parse(precision)) * 0.12);

                //Label
                string fieldName = string.Format("TextBox{0}", textCounter);
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit({2}, Telerik.Reporting.Drawing.UnitType.Cm));", fieldName, totalSize.ToString().Replace(",", "."), (isMaster ? "1.5D" : "0.01"))));
                method.Statements.Add(new CodeSnippetStatement(String.Format(@"this.{0}.Name = ""{0}"";", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));", fieldName, fieldSize.ToString().Replace(",", "."))));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Style.Font.Bold = true;", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format(@"this.{0}.Value = ""{1}"";", fieldName, field.DisplayName)));
                headerFields += (headerFields != string.Empty ? "," : "") + fieldName;
                textCounter++;

                //Field
                fieldName = string.Format("TextBox{0}", textCounter);
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));", fieldName, totalSize.ToString().Replace(",", "."))));
                method.Statements.Add(new CodeSnippetStatement(String.Format(@"this.{0}.Name = ""{0}"";", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));", fieldName, fieldSize.ToString().Replace(",", "."))));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Style.Font.Bold = false;", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format("this.{0}.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);", fieldName)));
                method.Statements.Add(new CodeSnippetStatement(String.Format(@"this.{0}.Value = ""=Fields.{1}"";", fieldName, field.Name)));
                detailFields += (detailFields != string.Empty ? "," : "") + fieldName;
                textCounter++;

                totalSize += fieldSize + 0.1;
            }

            //Vincula campos com a região.
            //Header
            if (isMaster)
                method.Statements.Add(new CodeSnippetStatement("this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + headerFields + " });"));
            else
                method.Statements.Add(new CodeSnippetStatement("this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + headerFields + " });"));
            //Detail
            if (detailFields != string.Empty)
                method.Statements.Add(new CodeSnippetStatement("this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + detailFields + " });"));
            //Footer
            if (footerFields != string.Empty)
                method.Statements.Add(new CodeSnippetStatement("this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + footerFields + " });"));

            method.Statements.Add(new CodeSnippetStatement("this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1" + (isMaster ? ", this.pageHeader, this.pageFooter" : "") + "});"));

            //PageSettings
            method.Statements.Add(new CodeCommentStatement("PageSettings"));

            method.Statements.Add(new CodeSnippetStatement(string.Format("this.PageSettings.Landscape = {0};", totalSize > pageSize ? "true" : "false")));
            method.Statements.Add(new CodeSnippetStatement(string.Format("this.Width = new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm);", totalSize > pageSize ? "31.299997329711914D" : "20D")));

            if (isMaster)
            {
                method.Statements.Add(new CodeSnippetStatement(string.Format("this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));", totalSize > pageSize ? "25D" : "16.5D")));
                method.Statements.Add(new CodeSnippetStatement(string.Format("this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));", totalSize > pageSize ? "25.5D" : "17D")));

                if (reportChildEntities != null && reportChildEntities.Length > 0)
                {
                    for (int idx = 0; idx < reportChildEntities.Length; idx++)
                    {
                        var reportChildEntity = reportChildEntities[idx];
                        method.Statements.Add(new CodeSnippetStatement(string.Format("            this.subReport" + reportChildEntity + ".Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.95D, Telerik.Reporting.Drawing.UnitType.Cm));", totalSize > pageSize ? "28.00D" : "19.50D")));
                    }
                }

            }

            method.Statements.Add(new CodeSnippetStatement("this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            method.Statements.Add(new CodeSnippetStatement("this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            method.Statements.Add(new CodeSnippetStatement("this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            method.Statements.Add(new CodeSnippetStatement("this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            method.Statements.Add(new CodeSnippetStatement("this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;"));
            method.Statements.Add(new CodeSnippetStatement("this.Style.BackgroundColor = System.Drawing.Color.White;"));

            //EndInit
            method.Statements.Add(new CodeCommentStatement("EndInit"));

            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                foreach (var reportChildEntity in reportChildEntities)
                    method.Statements.Add(new CodeSnippetStatement(string.Format("((System.ComponentModel.ISupportInitialize)(this.{0}Child1)).EndInit();", reportChildEntity)));
            }

            method.Statements.Add(new CodeSnippetStatement("((System.ComponentModel.ISupportInitialize)(this)).EndInit();"));

            codeGenerator.GenerateCodeFromNamespace(ns, sw, cgo);
            sw.Flush();
            string result = this.EntityAdapterDesignerRoot.GetString(ms);
            sw.Close();
            return result;
        }


        private string GenReportCSFileForCrossTab(string reportName, string[] reportChildEntities, string title)
        {
            string reportNamespace = this.EntityAdapterDesignerRoot.TargetNamespace.ToString() + ".Reports";
            string eadName = this.EntityAdapterDesignerRoot.GetContextName();
            string className = string.Format("{0}DataSource", eadName);

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = cscp.CreateGenerator(sw);
            CodeGeneratorOptions cgo = new CodeGeneratorOptions();

            System.CodeDom.CodeNamespace ns = new System.CodeDom.CodeNamespace(reportNamespace);
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
            ns.Imports.Add(new CodeNamespaceImport("System.Drawing"));
            ns.Imports.Add(new CodeNamespaceImport("Telerik.Reporting"));
            ns.Imports.Add(new CodeNamespaceImport("Telerik.Reporting.Drawing"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("Linx.Tools"));

            CodeTypeDeclaration partial = new CodeTypeDeclaration(reportName);
            partial.IsPartial = true;
            partial.IsClass = true;
            partial.BaseTypes.Add("Telerik.Reporting.Report");
            partial.Comments.Add(new CodeCommentStatement("Inform here the report title."));
            partial.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(title))));

            ns.Types.Add(partial);

            //Constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            //Initialize
            CodeSnippetStatement code = new CodeSnippetStatement("            InitializeComponent();");
            constructor.Statements.Add(code);

            constructor.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataMember = \"Get" + this.Name + "\";"));
            constructor.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataSource = new " + className + "();"));
            constructor.Statements.Add(new CodeSnippetStatement("            this.DataSource = null;"));
            constructor.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".DataSource = this.ReportDS;"));

            //Delegate
            CodeDelegateCreateExpression delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), string.Format("{0}_ItemDataBinding", reportName));
            CodeAttachEventStatement statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "ItemDataBinding", delegateReport);
            constructor.Statements.Add(statement1);

            //_ItemDataBinding
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = string.Format("{0}_ItemDataBinding", reportName);
            method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
            method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
            method.Statements.Add(new CodeSnippetStatement("            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;"));
            method.Statements.Add(new CodeSnippetStatement("            Image logoImg = null;"));
            method.Statements.Add(new CodeSnippetStatement("            if (!String.IsNullOrWhiteSpace(parameters[\"CompanyLogo\"].Value.ToString()))"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                try"));
            method.Statements.Add(new CodeSnippetStatement("                {"));
            method.Statements.Add(new CodeSnippetStatement("                    System.Net.WebClient wc = new System.Net.WebClient();"));
            method.Statements.Add(new CodeSnippetStatement("                    byte[] originalData = wc.DownloadData(parameters[\"CompanyLogo\"].Value.ToString());"));
            method.Statements.Add(new CodeSnippetStatement("                    System.IO.MemoryStream stream = new System.IO.MemoryStream(originalData);"));
            method.Statements.Add(new CodeSnippetStatement("                    logoImg = Bitmap.FromStream(stream);"));
            method.Statements.Add(new CodeSnippetStatement("                }"));
            method.Statements.Add(new CodeSnippetStatement("                catch { }"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            if (logoImg == null)"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                var directory = String.Empty;"));
            method.Statements.Add(new CodeSnippetStatement("                try"));
            method.Statements.Add(new CodeSnippetStatement("                {"));
            method.Statements.Add(new CodeSnippetStatement("                    directory = System.Web.HttpRuntime.BinDirectory;"));
            method.Statements.Add(new CodeSnippetStatement("                    string logoFile = System.IO.Path.Combine(directory, \"..\\\\image\\\\Linx.PNG\");"));
            method.Statements.Add(new CodeSnippetStatement("                    if (System.IO.File.Exists(logoFile))"));
            method.Statements.Add(new CodeSnippetStatement("                        logoImg = Bitmap.FromFile(logoFile);"));
            method.Statements.Add(new CodeSnippetStatement("                }"));
            method.Statements.Add(new CodeSnippetStatement("                catch { }"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            PictureBox1.Value = logoImg;"));
            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                method.Statements.Add(new CodeSnippetStatement("            ((" + className + ")this.ReportDS.DataSource).DetailsForLoading = new string[] { \"" + String.Join("\",\"", reportChildEntities) + "\" };"));
            }
            partial.Members.Add(method);

            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                for (int idx = 0; idx < reportChildEntities.Length; idx++)
                {
                    string reportChildEntity = reportChildEntities[idx];
                    //SubReport NeedDataSource
                    //Delegate
                    delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), "crosstab" + reportChildEntity + "_NeedDataSource");
                    statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "crosstab" + reportChildEntity + ".NeedDataSource", delegateReport);
                    constructor.Statements.Add(statement1);

                    constructor.Statements.Add(new CodeSnippetStatement("            this.crosstab" + reportChildEntity + ".DataSource = null;"));

                    //Method
                    method = new CodeMemberMethod();
                    method.Name = "crosstab" + reportChildEntity + "_NeedDataSource";
                    method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
                    method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
                    method.Statements.Add(new CodeSnippetStatement("            Telerik.Reporting.Processing.Table item = (Telerik.Reporting.Processing.Table)sender;"));
                    method.Statements.Add(new CodeSnippetStatement(string.Format(@"            IList valores = item.DataObject[""{0}List""] as IList;", reportChildEntity)));
                    method.Statements.Add(new CodeSnippetStatement("            item.DataSource = valores;"));
                    partial.Members.Add(method);
                    //CrossTab ItemDataBound
                    delegateReport = new CodeDelegateCreateExpression(new CodeTypeReference("System.EventHandler"), new CodeThisReferenceExpression(), "crosstab" + reportChildEntity + "_ItemDataBound");
                    statement1 = new CodeAttachEventStatement(new CodeThisReferenceExpression(), "crosstab" + reportChildEntity + ".ItemDataBound", delegateReport);
                    constructor.Statements.Add(statement1);
                    method = new CodeMemberMethod();
                    method.Name = "crosstab" + reportChildEntity + "_ItemDataBound";
                    method.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
                    method.Parameters.Add(new CodeParameterDeclarationExpression("EventArgs", "e"));
                    method.Statements.Add(new CodeSnippetStatement("            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;"));
                    method.Statements.Add(new CodeSnippetStatement(string.Format(@"            IList valores = item.DataObject[""{0}List""] as IList;", reportChildEntity)));
                    method.Statements.Add(new CodeSnippetStatement("            item.Visible = (valores == null || valores.Count == 0 ? false : true);"));
                    partial.Members.Add(method);
                }
            }

            partial.Members.Add(constructor);
            codeGenerator.GenerateCodeFromNamespace(ns, sw, cgo);
            sw.Flush();
            string result = this.EntityAdapterDesignerRoot.GetString(ms);
            sw.Close();
            return result;

        }


        private string GenReportDesignerFileForCrossTab(string reportName, string title, string[] reportChildEntities = null, string[] reportChildNames = null, Dictionary<string, List<string>> propertySelection = null)
        {
            string reportNamespace = this.EntityAdapterDesignerRoot.TargetNamespace.ToString() + ".Reports";
            string eadName = this.EntityAdapterDesignerRoot.GetContextName();
            string className = string.Format("{0}DataSource", eadName);
            string reportEntity = reportName;

            string headerFields = string.Empty;
            string detailFields = string.Empty;
            string footerFields = string.Empty;

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = cscp.CreateGenerator(sw);
            CodeGeneratorOptions cgo = new CodeGeneratorOptions();


            System.CodeDom.CodeNamespace ns = new System.CodeDom.CodeNamespace(reportNamespace);
            CodeTypeDeclaration classDeclaration = new CodeTypeDeclaration(reportName);
            classDeclaration.IsPartial = true;
            classDeclaration.IsClass = true;
            classDeclaration.Attributes = MemberAttributes.Private;
            ns.Types.Add(classDeclaration);

            //InitializeComponent
            CodeMemberMethod initMethod = new CodeMemberMethod();
            initMethod.Name = "InitializeComponent";

            initMethod.Statements.Add(new CodeCommentStatement("Sections"));

            CodeMemberField detail = new CodeMemberField("Telerik.Reporting.DetailSection", "detailSection1");
            classDeclaration.Members.Add(detail);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.detailSection1 = new Telerik.Reporting.DetailSection();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4.2D);"));

            //Crosstab    
            CodeMemberField crossTab = new CodeMemberField("Telerik.Reporting.Crosstab", "crosstab" + this.Name);
            crossTab.Attributes = MemberAttributes.Private;
            classDeclaration.Members.Add(crossTab);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + " = new Telerik.Reporting.Crosstab();"));

            //ControlGroup
            initMethod.Statements.Add(new CodeSnippetStatement("            var tableGroupControl = new Telerik.Reporting.TableGroup();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            tableGroupControl.Groupings.Add(new Telerik.Reporting.Grouping(null));"));
            initMethod.Statements.Add(new CodeSnippetStatement("            tableGroupControl.Name = \"Detail\";"));

            //Control Group
            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".RowGroups.Add(tableGroupControl);"));


            //Cabeçalho
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageHeader = new Telerik.Reporting.PageHeaderSection();"));
            CodeMemberField header = new CodeMemberField("Telerik.Reporting.PageHeaderSection", "pageHeader");
            classDeclaration.Members.Add(header);

            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageFooter = new Telerik.Reporting.PageFooterSection();"));
            CodeMemberField pageFooter = new CodeMemberField("Telerik.Reporting.PageFooterSection", "pageFooter");
            classDeclaration.Members.Add(pageFooter);

            //Header
            initMethod.Statements.Add(new CodeCommentStatement("PageHeader"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(2D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.pageHeader.Name = ""pageHeader"";"));
            headerFields = "this.TextBoxHeader, this.TextBoxDateTime, this.PictureBox1, this.TextBoxCompanyName, this.TextBoxFilter";
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageHeader.Style.Padding.Bottom = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            //Imagem
            initMethod.Statements.Add(new CodeCommentStatement("PictureBox1"));
            CodeMemberField headerField = new CodeMemberField("Telerik.Reporting.PictureBox", "PictureBox1");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PictureBox1 = new Telerik.Reporting.PictureBox();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PictureBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.PictureBox1.MimeType = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.PictureBox1.Name = ""PictureBox1"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.PictureBox1.Value = ""= Parameters.CompanyLogo.Value"";"));
            //Company Name
            initMethod.Statements.Add(new CodeCommentStatement("TextBoxCompanyName"));
            headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxCompanyName");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxCompanyName = new Telerik.Reporting.TextBox();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxCompanyName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.5999999046325684D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxCompanyName.Name = ""TextBoxCompanyName"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.1000003814697266D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxCompanyName.Style.Font.Bold = true;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxCompanyName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxCompanyName.Value = ""= Parameters.CompanyName.Value"";"));
            //Nome Report
            initMethod.Statements.Add(new CodeCommentStatement("TextBoxHeader"));
            headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxHeader");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxHeader = new Telerik.Reporting.TextBox();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxHeader.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.7D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.90D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxHeader.Name = ""TextBoxHeader"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxHeader.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.800000190734863D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxHeader.Style.Font.Bold = true;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxHeader.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);"));
            initMethod.Statements.Add(new CodeSnippetStatement(string.Format(@"            this.TextBoxHeader.Value = ""{0}"";", title)));
            //Hora
            initMethod.Statements.Add(new CodeCommentStatement("TextBoxDateTime"));
            headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxDateTime");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxDateTime = new Telerik.Reporting.TextBox();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxDateTime.Name = ""TextBoxDateTime"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxDateTime.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxDateTime.Value = ""= Now()"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;"));
            //Filtro
            initMethod.Statements.Add(new CodeCommentStatement("TextBoxFilter"));
            headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxFilter");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter = new Telerik.Reporting.TextBox();"));
            footerFields = "this.TextBoxPageCount";
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxFilter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.799997329711914D), Telerik.Reporting.Drawing.Unit.Cm(0.90000033378601074D));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxFilter.Name = ""TextBoxFilter"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Style.Font.Bold = false;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"             this.TextBoxFilter.Value = ""=Parameters.TranslatedJqueryExpression.Value"";"));

            //Rodapé
            initMethod.Statements.Add(new CodeCommentStatement("PageFooter"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.70000004768371582D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.pageFooter.Name = ""pageFooter"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;"));
            //Páginas
            initMethod.Statements.Add(new CodeCommentStatement("TextBoxPageCount"));
            headerField = new CodeMemberField("Telerik.Reporting.TextBox", "TextBoxPageCount");
            classDeclaration.Members.Add(headerField);
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxPageCount = new Telerik.Reporting.TextBox();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxPageCount.Name = ""TextBoxPageCount"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxPageCount.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.TextBoxPageCount.Value = ""= \""Pg :\"" + PageNumber.ToString() + \""/\"" + PageCount.ToString()"";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.TextBoxPageCount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;"));

            //Parameters
            //EntitySearchId
            initMethod.Statements.Add(new CodeCommentStatement("Parameters"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter1.Name = ""EntitySearchId"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter1.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter1);"));
            //CurrentUser
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter2.Name = ""CurrentUser"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter2.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter2);"));
            //CurrentCompany
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter3.Name = ""CurrentCompany"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter3.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter3);"));
            //AuthorizationToken
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter4.Name = ""AuthorizationToken"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter4.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter4);"));
            //TransactionInfo
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter5.Name = ""TransactionInfo"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter5.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter5);"));
            //CompanyLogo
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter6.Name = ""CompanyLogo"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter6.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter6);"));
            //Company Name
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter7.Name = ""CompanyName"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter7.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter7);"));
            //Access Group
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter8.Name = ""AccessGroup"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter8.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter8);"));
            //Application
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter9.Name = ""Application"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter9.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter9);"));
            //EconomicGroup
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter10.Name = ""EconomicGroup"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter10.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter10);"));
            //Environment
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter11.Name = ""Environment"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter11.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter11);"));
            //JqueryExpression
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter12.Name = ""JqueryExpression"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter12.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter12);"));
            //CurrentUserName
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter13.Name = ""CurrentUserName"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter13.Value = """";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportParameters.Add(reportParameter13);"));
            //Translated JqueryExpression
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter14.Name = ""TranslatedJqueryExpression"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter14.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter14);"));
            //Branch
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter15.Name = ""Branch"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter15.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter15);"));
            //LoginMode
            initMethod.Statements.Add(new CodeSnippetStatement(@"           Telerik.Reporting.ReportParameter reportParameter16 = new Telerik.Reporting.ReportParameter();"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter16.Name = ""LoginMode"";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           reportParameter16.Value = "" "";"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"           this.ReportParameters.Add(reportParameter16);"));


            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            var {0} = new Telerik.Reporting.TableGroup();", "tableGroupSubReport")));
            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            {0}.Name = \"{0}\";", "tableGroupSubReport")));
            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            tableGroupControl.ChildGroups.Add({0});", "tableGroupSubReport")));

            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                for (int idx = 0; idx < reportChildEntities.Length; idx++)
                {
                    var reportChildEntity = reportChildEntities[idx];
                    var reportChildName = reportChildNames[idx].Replace(".", "");

                    //subReport
                    CodeMemberField subReport = new CodeMemberField("Telerik.Reporting.Crosstab", "crosstab" + reportChildEntity);
                    classDeclaration.Members.Add(subReport);
                    initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + reportChildEntity + " = new Telerik.Reporting.Crosstab();"));
                    initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + reportChildEntity + ".Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(" + ((idx + 1) * 0.3D).ToString().Replace(",", ".") + "D, Telerik.Reporting.Drawing.UnitType.Cm));"));
                    initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + reportChildEntity + ".Name = \"crosstab" + reportChildEntity + "\";"));

                    detailFields += (detailFields != string.Empty ? "," : "") + "this.crosstab" + reportChildEntity;

                    string groupSubReportName;
                    groupSubReportName = "tableGroupSubReport" + idx;

                    //Adiciona os grupos do SubReport:

                    initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            var {0} = new Telerik.Reporting.TableGroup();", groupSubReportName)));
                    initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            {0}.Name = \"{0}\";", groupSubReportName)));

                    //Insere os grupos do Subreport ao controle do relatório
                    initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            tableGroupControl.ChildGroups.Add({0});", groupSubReportName)));

                    //ControlGroup
                    initMethod.Statements.Add(new CodeSnippetStatement("            var tableGroupControl" + reportChildEntity + " = new Telerik.Reporting.TableGroup();"));
                    initMethod.Statements.Add(new CodeSnippetStatement("            tableGroupControl" + reportChildEntity + ".Groupings.Add(new Telerik.Reporting.Grouping(null));"));
                    initMethod.Statements.Add(new CodeSnippetStatement("            tableGroupControl" + reportChildEntity + ".Name = \"tableGroupControl" + reportChildEntity + "\";"));         //Control Group
                    initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + reportChildEntity + ".RowGroups.Add(tableGroupControl" + reportChildEntity + ");"));

                }
            }


            classDeclaration.Members.Add(initMethod);
            //Add Data Source
            classDeclaration.Members.Add(new CodeMemberField("Telerik.Reporting.ObjectDataSource", "ReportDS"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.Style.BackgroundColor = System.Drawing.Color.White;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter });"));
            initMethod.Statements.Add(new CodeCommentStatement("BeginInit"));
            initMethod.Statements.Add(new CodeSnippetStatement("            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();"));
            //Detail
            initMethod.Statements.Add(new CodeSnippetStatement("            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(" + (reportChildEntities == null || reportChildEntities.Length == 0 ? "0.67D" : (reportChildEntities.Length + 1).ToString() + ".00D") + ");"));
            initMethod.Statements.Add(new CodeSnippetStatement(@"            this.detailSection1.Name = ""detailSection1"";"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + headerFields + " });"));


            //Generate all data structures for Master
            GenerateReportDataStructure(initMethod, classDeclaration, propertySelection, detailFields, null);
            //Generate all data structures for each Detail
            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                for (int idx = 0; idx < reportChildEntities.Length; idx++)
                {
                    var detailEntity = this.SourceEntityAdapters.FirstOrDefault(e => e.Name == reportChildEntities[idx]);
                    detailEntity.ColumnsCount = this.ColumnsCount;
                    detailEntity.GenerateReportDataStructure(initMethod, classDeclaration, propertySelection, "", reportChildEntities);
                }
            }

            //Report DataSource Definition
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportDS = new Telerik.Reporting.ObjectDataSource();"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataMember = \"Get" + this.Name + "\";"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportDS.DataSource = typeof(" + reportNamespace + "." + className + ");"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.ReportDS.Name = \"ReportDS\";"));

            //Source Parameters:
            initMethod.Statements.Add(new CodeSnippetStatement("this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] { new Telerik.Reporting.ObjectDataSourceParameter(\"reportItem\", typeof(System.Object), \"= ReportItem\")});"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.DataSource = this.ReportDS;"));

            //Footer
            if (footerFields != string.Empty)
                initMethod.Statements.Add(new CodeSnippetStatement("            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { " + footerFields + " });"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter });"));

            //PageSettings
            initMethod.Statements.Add(new CodeCommentStatement("PageSettings"));

            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.Width = new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm);", "29D")));

            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));", "25D")));
            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit({0}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));", "25.5D")));


            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;"));
            initMethod.Statements.Add(new CodeSnippetStatement("            this.Style.BackgroundColor = System.Drawing.Color.White;"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.PageSettings.Landscape = true;"));

            //EndInit
            initMethod.Statements.Add(new CodeCommentStatement("EndInit"));
            initMethod.Statements.Add(new CodeSnippetStatement("            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();"));

            codeGenerator.GenerateCodeFromNamespace(ns, sw, cgo);
            sw.Flush();
            string result = this.EntityAdapterDesignerRoot.GetString(ms);
            sw.Close();
            return result;
        }

        public string ColumnsCount { get; set; }

        public void GenerateReportDataStructure(CodeMemberMethod initMethod, CodeTypeDeclaration classDeclaration, Dictionary<string, List<string>> propertySelection, string detailFields, string[] reportChildEntities)
        {
            List<string> validProperties = new List<string>();
            if (propertySelection != null && propertySelection.ContainsKey(this.Name))
                validProperties = propertySelection[this.Name];

            List<EntityAdapterAttribute> reportFields = this.GetAllInheritanceAttributes().Where(e => e.IsBrowsable && (validProperties.Count == 0 || validProperties.Contains(e.Name))).OrderBy(e => e.DisplayOrder).ToList();

            //Add CrossTab into the correct row
            if (reportChildEntities != null && reportChildEntities.Length > 0)
            {
                initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.TargetEntityAdapter.Name + ".Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.2D)));"));
                initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.crosstab" + this.TargetEntityAdapter.Name + ".Body.SetCellContent({0}, 0, this.crosstab{1}, 1, {2});", reportChildEntities.ToList().IndexOf(this.Name) + 1, this.Name, ColumnsCount)));
                initMethod.Statements.Add(
                    new CodeSnippetStatement(
                        " this.crosstab" + this.Name + ".Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;"));
            }
            else
            {
                ColumnsCount = reportFields.Count.ToString();
                initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));"));
                initMethod.Statements.Add(new CodeSnippetStatement("            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.crosstab" + this.Name + " });"));
            }

            //Crosstab body row
            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.5D)));"));

            for (int headerIndex = 0; headerIndex < reportFields.Count; headerIndex++)
            {
                EntityAdapterAttribute field = reportFields[headerIndex];

                //Header
                double fieldSize = (field.DisplayName.Length * 0.20);
                string precision = (field.Precision.Contains(":") ? field.Precision.Replace(":", ",") : (Double.Parse(field.Precision) / 10.00).ToString());
                if (fieldSize < ((Double.Parse(precision)) * 0.12))
                    fieldSize = ((Double.Parse(precision)) * 0.12);

                string headerName = string.Format("TextBox{0}{1}", this.Name, headerIndex + 1);
                CodeMemberField reportField = new CodeMemberField("Telerik.Reporting.TextBox", headerName);
                classDeclaration.Members.Add(reportField);
                initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.{0} = new Telerik.Reporting.TextBox();", headerName)));

                initMethod.Statements.Add(new CodeSnippetStatement(String.Format(@"            this.{0}.Name = ""{0}"";", headerName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));", headerName, fieldSize.ToString().Replace(",", "."))));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Style.Font.Bold = true;", headerName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);", headerName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format(@"            this.{0}.Value = ""{1}"";", headerName, field.DisplayName)));
                detailFields += (detailFields != string.Empty ? "," : "") + headerName;

                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            var tableGroup{0}{1} = new Telerik.Reporting.TableGroup();", this.Name, headerIndex + 1)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            tableGroup{0}{1}.ReportItem = this.{2};", this.Name, headerIndex + 1, headerName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.crosstab" + this.Name + ".ColumnGroups.Add(tableGroup{0}{1});", this.Name, headerIndex + 1)));

                //Field
                int fieldIndex = reportFields.Count + headerIndex + 1;
                string fieldName = string.Format("TextBox{0}{1}", this.Name, fieldIndex + 1);
                reportField = new CodeMemberField("Telerik.Reporting.TextBox", fieldName);
                classDeclaration.Members.Add(reportField);
                initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.{0} = new Telerik.Reporting.TextBox();", fieldName)));

                initMethod.Statements.Add(new CodeSnippetStatement(String.Format(@"            this.{0}.Name = ""{0}"";", fieldName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit({1}, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));", fieldName, fieldSize.ToString().Replace(",", "."))));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Style.Font.Bold = false;", fieldName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format("            this.{0}.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);", fieldName)));
                initMethod.Statements.Add(new CodeSnippetStatement(String.Format(@"            this.{0}.Value = ""=Fields.{1}"";", fieldName, field.Name)));

                initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));"));
                initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            this.crosstab" + this.Name + ".Body.SetCellContent(0, {0}, this.{1});", headerIndex, fieldName)));


                detailFields += (detailFields != string.Empty ? "," : "") + fieldName;
            }

            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".DataSource = this.ReportDS;"));

            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Name = \"crosstab" + this.Name + "\";"));

            //Control Group
            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12D), Telerik.Reporting.Drawing.Unit.Cm(2D));"));

            //Detail
            initMethod.Statements.Add(new CodeSnippetStatement("            this.crosstab" + this.Name + ".Items.AddRange(new Telerik.Reporting.ReportItemBase[] {" + detailFields + " });"));


        }


        private void GenTextbox(XmlTextWriter writer, string textboxName, string textboxValue, string textAlign, int fontSize, double width, bool fontBold, bool includeZindex, bool isRight, double leftPosition = 0, double topPosition = 0)
        {
            writer.WriteStartElement("Textbox");
            writer.WriteAttributeString("Name", textboxName);
            writer.WriteElementString("CanGrow", "true");
            writer.WriteElementString("KeepTogether", "true");
            writer.WriteStartElement("Paragraphs");
            writer.WriteStartElement("Paragraph");
            writer.WriteStartElement("TextRuns");
            writer.WriteStartElement("TextRun");
            writer.WriteElementString("Value", textboxValue);
            writer.WriteStartElement("Style");
            writer.WriteElementString("FontFamily", "Tahoma");
            writer.WriteElementString("FontSize", string.Format("{0}pt", fontSize.ToString()));

            if (fontBold)
                writer.WriteElementString("FontWeight", "Bold");

            writer.WriteEndElement();//Style
            writer.WriteEndElement();//TextRun
            writer.WriteEndElement();//TextRuns
            writer.WriteStartElement("Style");
            writer.WriteElementString("TextAlign", textAlign);
            writer.WriteEndElement();//Style
            writer.WriteEndElement();//Paragraph
            writer.WriteEndElement();//Paragraphs
            writer.WriteElementString("rd:DefaultName", textboxName);

            if (isRight)
            {
                if (leftPosition > 0)
                    writer.WriteElementString("Left", string.Format("{0}in", leftPosition));
                else
                    writer.WriteElementString("Left", string.Format("{0}in", ((totalReportWidth) - 3).ToString().Replace(",", ".")));
            }
            else if (leftPosition > 0)
                writer.WriteElementString("Left", string.Format("{0}in", leftPosition));

            if (width > 0)
            {
                writer.WriteElementString("Height", "0.21in");
                writer.WriteElementString("Width", String.Format("{0}in", width.ToString().Replace(",", ".")));
            }

            if (topPosition > 0)
                writer.WriteElementString("Top", String.Format("{0}in", topPosition.ToString().Replace(",", ".")));

            if (includeZindex)
            {
                writer.WriteElementString("ZIndex", "1");
            }

            GenPadding(writer, 2);
            writer.WriteEndElement();//Textbox
        }

        private void GenPadding(XmlTextWriter writer, int size)
        {
            writer.WriteStartElement("Style");
            writer.WriteElementString("PaddingLeft", string.Format("{0}pt", size.ToString()));
            writer.WriteElementString("PaddingRight", string.Format("{0}pt", size.ToString()));
            writer.WriteElementString("PaddingTop", string.Format("{0}pt", size.ToString()));
            writer.WriteElementString("PaddingBottom", string.Format("{0}pt", size.ToString()));
            writer.WriteEndElement();//Style
        }

        public void UpdateSourceDerivedClasses(Dictionary<string, string> specializedClasses)
        {
            this.SourceDerivedClasses = String.Empty;
            if (specializedClasses != null && specializedClasses.Count > 0)
            {
                foreach (var derived in specializedClasses)
                {
                    this.SourceDerivedClasses += (this.SourceDerivedClasses.IsNullOrEmpty() ? String.Empty : ",") + derived.Key + ":" + derived.Value;
                }
            }
        }

        public Dictionary<string, string> GetAllSourceDerivedClasses()
        {
            Dictionary<string, string> derivedClasses = new Dictionary<string, string>();
            if (!this.SourceDerivedClasses.IsNullOrEmpty())
            {
                foreach (var derived in this.SourceDerivedClasses.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string key = derived.Left(":"), value = derived.Right(":");
                    if (!key.IsNullOrEmpty() && !value.IsNullOrEmpty() && !derivedClasses.ContainsKey(key))
                        derivedClasses.Add(key, value);
                }
            }

            return derivedClasses;
        }


        public void GenerateEntityLookUps(List<LookUpStruct> lookUpStructures)
        {
            if (!EnableAutomaticLookUps)
                return;

            LookUpAdapter lookUpAdapter;
            LookUpProperty lookUpProperty;

            //Get existent lookups for restoring orders
            Dictionary<string, List<LookUpProperty>> lookupsForOrdering = new Dictionary<string, List<LookUpProperty>>();
            foreach (var lookup in this.LookUpAdapters)
            {
                if (!lookup.IsCustomized)
                    lookupsForOrdering.Add(lookup.Name, lookup.LookUpProperties.ToList());
            }

            foreach (var lStruct in lookUpStructures)
            {

                //Remove parent relation
                if (this.TargetEntityAdapter != null && this.TargetEntityAdapter.PrimaryEntity == lStruct.Name)
                    continue;

                lookUpAdapter = this.GetLookUpAdapter(lStruct.Name);
                if (lookUpAdapter.IsNull())
                {
                    lookUpAdapter = new LookUpAdapter(this.Partition);
                    lookUpAdapter.Name = "LookUp" + lStruct.Name.PrepareName();
                    lookUpAdapter.RelationName = lStruct.Name;
                    lookUpAdapter.Description = "Look Up " + lStruct.Name;
                    lookUpAdapter.DisplayName = "Look Up " + lStruct.Name;
                    lookUpAdapter.EntitySource = lStruct.Name;
                    lookUpAdapter.EntitySourceBase = "";
                    lookUpAdapter.IsCustomized = false;
                    this.EntityAdapterDesignerRoot.LookUpAdapters.Add(lookUpAdapter);
                    this.LookUpAdapters.Add(lookUpAdapter);

                    //Verify base type
                    LookUpAdapter baseLoopUpAdapter = this.GetInheritanceLookUpAdapter(lStruct.Name);
                    if (baseLoopUpAdapter != null)
                    {
                        baseLoopUpAdapter.DerivedLookUpAdapters.Add(lookUpAdapter);
                        lookUpAdapter.UpdateBaseClassInfo(true);
                    }
                }
                else
                {
                    if (lookUpAdapter.IsCustomized)
                        continue;
                }

                foreach (var property in lStruct.Properties)
                {
                    if (lookUpAdapter.GetInheritanceProperties().Where(e => e.Name == property.Name).Count() > 0)
                        continue;

                    if (lookUpAdapter.GetDerivedProperties().Where(e => e.Name == property.Name).Count() > 0)
                        continue;

                    string propertyName = property.Name.PrepareName();
                    lookUpProperty = lookUpAdapter.GetLookUpProperty(propertyName);
                    if (lookUpProperty.IsNull())
                    {
                        lookUpProperty = new LookUpProperty(this.Partition);
                        lookUpProperty.Name = propertyName;
                        lookUpProperty.EntityPropertyRelated = property.EntityPropertyRelated;
                        var relatedProp = this.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == property.EntityPropertyRelated);
                        lookUpProperty.DisplayName = (relatedProp != null ? relatedProp.DisplayName : property.Name.Proper());
                        if (relatedProp != null)
                        {
                            relatedProp.IsFK = property.IsPrimaryKey;
                            relatedProp.DisplayControl = DisplayControlType.LookUpTextBox;
                        }
                        lookUpProperty.IsCustomized = false;
                        lookUpAdapter.LookUpProperties.Add(lookUpProperty);
                    }
                    else if (lookUpProperty.IsCustomized)
                        continue;

                    lookUpProperty.EdmKey = lStruct.Name + "." + property.Name;
                    lookUpProperty.Datatype = property.Datatype;
                    lookUpProperty.Precision = property.Precision;
                    lookUpProperty.DataFormatString = property.GetDataFormatString();
                    lookUpProperty.IsPrimaryKey = property.IsPrimaryKey;
                }

            }
            //Validating LookUp properties
            for (int idxLookUp = this.LookUpAdapters.Count - 1; idxLookUp >= 0; idxLookUp--)
            {
                if (!this.LookUpAdapters[idxLookUp].IsCustomized)
                {
                    if (!this.LookUpAdapters[idxLookUp].ValidProperties())
                        continue;

                    //Restore lookups ordering
                    if (lookupsForOrdering.ContainsKey(this.LookUpAdapters[idxLookUp].Name))
                    {
                        this.LookUpAdapters[idxLookUp].Reorder(lookupsForOrdering[this.LookUpAdapters[idxLookUp].Name]);
                    }
                }
            }

        }


        public void GenerateEntityLookUps(Dictionary<string, string> specializedClasses)
        {

            if (!EnableAutomaticLookUps)
                return;

            LookUpAdapter lookUpAdapter;
            LookUpProperty lookUpProperty;
            string entityName, relationName;
            string[] parts;
            string keys;


            //Generating entity sets dictionary
            Dictionary<string, string> entitySets = new Dictionary<string, string>();
            foreach (string entitySet in this.EntitySets.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (entitySet.Left("(") != this.PrimaryEntity)
                {
                    string entitySetType = entitySet.Extract("(", ")");
                    if (entitySetType.Contains(":"))
                        entitySetType = entitySetType.Right(":");
                    entitySets.Add(entitySet.Left("("), entitySetType);
                }
            }

            //Get existent lookups for restoring orders
            Dictionary<string, List<LookUpProperty>> lookupsForOrdering = new Dictionary<string, List<LookUpProperty>>();
            foreach (var lookup in this.LookUpAdapters)
            {
                if (!lookup.IsCustomized)
                    lookupsForOrdering.Add(lookup.Name, lookup.LookUpProperties.ToList());
            }


            foreach (EntityAdapterAttribute property in this.GetAllAttributes().Where(e => (e is EntityAdapterProperty && ((EntityAdapterProperty)e).EdmKey.Occurs(".") > 1) || (e is EntityAdapterPublicationProperty && ((EntityAdapterPublicationProperty)e).EdmKey.Occurs(".") > 1)).OrderBy(e => (e is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)e).EdmKey : ((EntityAdapterProperty)e).EdmKey)))
            {
                parts = (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).EdmKey : ((EntityAdapterPublicationProperty)property).EdmKey).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    relationName = parts[1];

                    if (!entitySets.ContainsKey(relationName))
                    {
                        entityName = this.GetEntityNameByRelation(relationName).Trim();

                        //Remove parent relation
                        if (this.TargetEntityAdapter != null && this.TargetEntityAdapter.PrimaryEntity == entityName)
                            continue;

                        if (!entityName.IsNullOrEmpty())
                        {
                            keys = this.EntityAdapterDesignerRoot.GetPrimaryKeyByEntity(entityName);
                            lookUpAdapter = this.GetLookUpAdapter(relationName);
                            if (lookUpAdapter.IsNull())
                            {
                                lookUpAdapter = new LookUpAdapter(this.Partition);
                                lookUpAdapter.Name = "LookUp" + relationName.PrepareName();
                                lookUpAdapter.RelationName = relationName;
                                lookUpAdapter.Description = "Look Up " + entityName;
                                lookUpAdapter.DisplayName = "Look Up " + entityName;
                                lookUpAdapter.EntitySource = entityName;
                                if (specializedClasses.ContainsKey(lookUpAdapter.EntitySource))
                                    lookUpAdapter.EntitySourceBase = specializedClasses[lookUpAdapter.EntitySource];
                                else
                                    lookUpAdapter.EntitySourceBase = "";
                                lookUpAdapter.IsCustomized = false;
                                this.EntityAdapterDesignerRoot.LookUpAdapters.Add(lookUpAdapter);
                                this.LookUpAdapters.Add(lookUpAdapter);

                                //Verify base type
                                LookUpAdapter baseLoopUpAdapter = this.GetInheritanceLookUpAdapter(relationName);
                                if (baseLoopUpAdapter != null)
                                {
                                    baseLoopUpAdapter.DerivedLookUpAdapters.Add(lookUpAdapter);
                                    lookUpAdapter.UpdateBaseClassInfo(true);
                                }
                            }
                            else
                            {
                                if (lookUpAdapter.IsCustomized)
                                    continue;
                            }

                            if (lookUpAdapter.GetInheritanceProperties().Where(e => e.Name == property.Name).Count() > 0)
                                continue;

                            if (lookUpAdapter.GetDerivedProperties().Where(e => e.Name == property.Name).Count() > 0)
                                continue;

                            lookUpProperty = lookUpAdapter.GetLookUpProperty(property.Name);
                            if (lookUpProperty.IsNull())
                            {
                                lookUpProperty = new LookUpProperty(this.Partition);
                                lookUpProperty.Name = property.Name;
                                lookUpProperty.DisplayName = property.DisplayName;
                                lookUpProperty.EntityPropertyRelated = property.Name;
                                lookUpProperty.IsCustomized = false;
                                lookUpAdapter.LookUpProperties.Add(lookUpProperty);
                            }
                            else if (lookUpProperty.IsCustomized)
                                continue;

                            lookUpProperty.EdmKey = entityName + "." + (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).EdmKey : ((EntityAdapterPublicationProperty)property).EdmKey).Right("." + relationName + ".");
                            lookUpProperty.Datatype = property.Datatype;
                            lookUpProperty.DataFormatString = property.DataFormatString;
                            lookUpProperty.Precision = property.Precision;
                            lookUpProperty.IsPrimaryKey = ("," + keys + ",").Contains(lookUpProperty.EdmKey.Right("."));

                        }
                    }
                }
            }


            //Generating secondaries lookups
            if (entitySets.Count > 0)
            {
                foreach (string entitySet in entitySets.Keys)
                {
                    foreach (EntityAdapterAttribute property in this.GetAllAttributes().Where(e => (e is EntityAdapterProperty && ((EntityAdapterProperty)e).EdmKey.Right(this.PrimaryEntity + "." + entitySet + ".").Occurs(".") > 0) || (e is EntityAdapterPublicationProperty && ((EntityAdapterPublicationProperty)e).EdmKey.Right(this.PrimaryEntity + "." + entitySet + ".").Occurs(".") > 0)).OrderBy(e => (e is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)e).EdmKey : ((EntityAdapterProperty)e).EdmKey)))
                    {
                        parts = (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).EdmKey : ((EntityAdapterPublicationProperty)property).EdmKey).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length >= 3)
                        {
                            relationName = parts[2];
                            entityName = this.GetEntityNameByRelation(relationName).Trim();

                            if (!entityName.IsNullOrEmpty())
                            {
                                keys = this.EntityAdapterDesignerRoot.GetPrimaryKeyByEntity(entityName);
                                lookUpAdapter = this.GetLookUpAdapter(relationName);
                                if (lookUpAdapter.IsNull())
                                {
                                    lookUpAdapter = new LookUpAdapter(this.Partition);
                                    lookUpAdapter.Name = "LookUp" + relationName.PrepareName();
                                    lookUpAdapter.RelationName = relationName;
                                    lookUpAdapter.Description = "Look Up " + entityName;
                                    lookUpAdapter.DisplayName = "Look Up " + entityName;
                                    lookUpAdapter.EntitySource = entityName;
                                    if (specializedClasses.ContainsKey(lookUpAdapter.EntitySource))
                                        lookUpAdapter.EntitySourceBase = specializedClasses[lookUpAdapter.EntitySource];
                                    else
                                        lookUpAdapter.EntitySourceBase = "";
                                    lookUpAdapter.IsCustomized = false;
                                    this.EntityAdapterDesignerRoot.LookUpAdapters.Add(lookUpAdapter);
                                    this.LookUpAdapters.Add(lookUpAdapter);

                                    //Verify base type
                                    LookUpAdapter baseLoopUpAdapter = this.GetInheritanceLookUpAdapter(relationName);
                                    if (baseLoopUpAdapter != null)
                                    {
                                        baseLoopUpAdapter.DerivedLookUpAdapters.Add(lookUpAdapter);
                                        lookUpAdapter.UpdateBaseClassInfo(true);
                                    }
                                }
                                else
                                {
                                    if (lookUpAdapter.IsCustomized)
                                        continue;
                                }

                                if (lookUpAdapter.GetInheritanceProperties().Where(e => e.Name == property.Name).Count() > 0)
                                    continue;

                                if (lookUpAdapter.GetDerivedProperties().Where(e => e.Name == property.Name).Count() > 0)
                                    continue;

                                lookUpProperty = lookUpAdapter.GetLookUpProperty(property.Name);
                                if (lookUpProperty.IsNull())
                                {
                                    lookUpProperty = new LookUpProperty(this.Partition);
                                    lookUpProperty.Name = property.Name;
                                    lookUpProperty.DisplayName = property.DisplayName;
                                    lookUpProperty.EntityPropertyRelated = property.Name;
                                    lookUpProperty.IsCustomized = false;
                                    lookUpAdapter.LookUpProperties.Add(lookUpProperty);
                                }
                                else if (lookUpProperty.IsCustomized)
                                    continue;

                                lookUpProperty.EdmKey = entityName + "." + (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).EdmKey : ((EntityAdapterPublicationProperty)property).EdmKey).Right("." + relationName + ".");
                                lookUpProperty.Datatype = property.Datatype;
                                lookUpProperty.DataFormatString = property.DataFormatString;
                                lookUpProperty.Precision = property.Precision;
                                lookUpProperty.IsPrimaryKey = ("," + keys + ",").Contains(lookUpProperty.EdmKey.Right("."));
                            }
                        }
                    }
                }
            }

            //Validating LookUp properties
            for (int idxLookUp = this.LookUpAdapters.Count - 1; idxLookUp >= 0; idxLookUp--)
            {
                if (!this.LookUpAdapters[idxLookUp].IsCustomized)
                {
                    if (!this.LookUpAdapters[idxLookUp].ValidProperties())
                        continue;

                    //Restore lookups ordering
                    if (lookupsForOrdering.ContainsKey(this.LookUpAdapters[idxLookUp].Name))
                    {
                        this.LookUpAdapters[idxLookUp].Reorder(lookupsForOrdering[this.LookUpAdapters[idxLookUp].Name]);
                    }
                }
            }

        }

        public bool HasBrand(bool verifyDetails = false)
        {
            bool result = this.GetAllInheritanceAttributes().Any(p => p.Name == "IdBandeiraRede");

            if (!result && verifyDetails)
            {
                foreach (var detail in this.SourceEntityAdapters)
                {
                    result = detail.HasBrand(verifyDetails);
                    if (result)
                        break;
                }
            }

            return result;
        }

        public bool HasGpecon(bool verifyDetails = false)
        {
            bool result = this.GetAllInheritanceAttributes().Any(p => p.Name == "IdGpecon");

            if (!result && verifyDetails)
            {
                foreach (var detail in this.SourceEntityAdapters)
                {
                    result = detail.HasGpecon(verifyDetails);
                    if (result)
                        break;
                }
            }

            return result;
        }

        public void GenerateEntityOlapLookUps()
        {
            if (!EnableAutomaticLookUps)
                return;

            Func<EntityAdapterAttribute, bool> IsDimension = e => (e is EntityAdapterProperty && !((EntityAdapterProperty)e).IsMeasure) || (e is EntityAdapterPublicationProperty && !((EntityAdapterPublicationProperty)e).IsMeasure);
            LookUpAdapter lookUpAdapter;
            LookUpProperty lookUpProperty;
            string relationName;

            foreach (EntityAdapterAttribute property in
                    this.GetAllAttributes().Where(IsDimension)
                    .OrderBy(e => (e is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)e).DataRelationKey : ((EntityAdapterProperty)e).EdmKey)))
            {

                if (property is EntityAdapterProperty)
                {
                    ((EntityAdapterProperty)property).DisplayControl = DisplayControlType.LookUpTextBox;
                }
                var mdxKey = (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).DataRelationKey : ((EntityAdapterPublicationProperty)property).DataRelationKey);
                relationName = this.Name + "_" + property.Name;

                lookUpAdapter = this.GetLookUpAdapter(relationName);
                if (lookUpAdapter.IsNull())
                {
                    lookUpAdapter = new LookUpAdapter(this.Partition);
                    lookUpAdapter.Name = "LookUp" + relationName.PrepareName();
                    lookUpAdapter.RelationName = relationName;
                    lookUpAdapter.Description = "Look Up " + property.Name;
                    lookUpAdapter.DisplayName = "Look Up " + property.Name;
                    lookUpAdapter.EntitySource = relationName;
                    lookUpAdapter.EntitySourceBase = "";
                    lookUpAdapter.IsCustomized = false;
                    this.EntityAdapterDesignerRoot.LookUpAdapters.Add(lookUpAdapter);
                    this.LookUpAdapters.Add(lookUpAdapter);

                    //Verify base type
                    LookUpAdapter baseLoopUpAdapter = this.GetInheritanceLookUpAdapter(relationName);
                    if (baseLoopUpAdapter != null)
                    {
                        baseLoopUpAdapter.DerivedLookUpAdapters.Add(lookUpAdapter);
                        lookUpAdapter.UpdateBaseClassInfo(true);
                    }
                }
                else
                {
                    if (lookUpAdapter.IsCustomized)
                        continue;
                }


                lookUpProperty = lookUpAdapter.GetLookUpProperty(property.Name);
                if (lookUpProperty.IsNull())
                {
                    lookUpProperty = new LookUpProperty(this.Partition);
                    lookUpProperty.Name = property.Name;
                    lookUpProperty.EntityPropertyRelated = property.Name;
                    lookUpProperty.IsCustomized = false;
                    lookUpAdapter.LookUpProperties.Add(lookUpProperty);
                }
                else if (lookUpProperty.IsCustomized)
                    continue;

                lookUpProperty.EdmKey = mdxKey;
                lookUpProperty.DisplayName = property.DisplayName;
                lookUpProperty.Datatype = property.Datatype;
                lookUpProperty.DataFormatString = property.DataFormatString;
                lookUpProperty.Precision = property.Precision;
                lookUpProperty.IsPrimaryKey = true;
            }

        }

        public string GetComments()
        {
            string result = String.Empty;

            foreach (var doc in this.Comments.Where(e => !e.Text.IsNullOrEmpty()))
            {
                result += (result.IsNullOrEmpty() ? String.Empty : "\r\n") + doc.Text;
            }

            return result;
        }

        public string GetJsonPairValues()
        {
            string result = String.Empty;

            foreach (var attrib in this.GetAllInheritanceAttributes().OrderBy(e => e.Name))
            {
                result += (result.IsNullOrEmpty() ? " " : ", ") + "\"" + attrib.Name + "\":" + (attrib.Datatype.ToLower().Contains("string") ? "\"Abc\"" : (attrib.Datatype.ToLower().Contains("nullable") || attrib.Datatype.ToLower().Contains("?") ? "null" : "0"));
            }

            return result;
        }

        public bool HasDetailsAsSurrogate()
        {
            return this.SourceEntityAdapters.Where(e => !e.SurrogateProperty.IsNullOrEmpty()).Count() > 0;
        }

        public bool HasDetails()
        {
            bool hasDet = this.HasDetailsUp();

            if (!hasDet)
                hasDet = this.HasDetailsDown();

            return hasDet;
        }

        private bool HasDetailsUp()
        {
            bool hasDet = this.SourceEntityAdapters.Count() > 0;

            if (!hasDet && this.BaseEntityAdapter != null)
                hasDet = this.BaseEntityAdapter.HasDetailsUp();

            return hasDet;
        }

        private bool HasDetailsDown()
        {
            bool hasDet = this.SourceEntityAdapters.Count() > 0;

            if (!hasDet && this.DerivedEntityAdapters.Count > 0)
            {
                foreach (var deriv in this.DerivedEntityAdapters)
                {
                    hasDet = deriv.HasDetailsDown();
                    if (hasDet)
                        break;
                }
            }

            return hasDet;
        }

        public string GenerateHierarchyForSaving(string indent)
        {
            string result = String.Empty;


            result += "\r\n " + indent + "public" + (this.BaseEntityAdapter == null ? " virtual" : " override") + " bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)";
            result += "\r\n " + indent + "{";

            result += "\r\n" + indent + "  bool hasChanges = false;";
            if (this.BaseEntityAdapter != null)
                result += "\r\n" + indent + "  hasChanges = base.AdjustHierarchyForSaving(entity, changeSet);";

            foreach (EntityAdapter entity in this.SourceEntityAdapters)
            {
                result += "\r\n ";
                string parentRelation = entity.GetParentRelationToLinqForEntity("((" + entity.Name + ")e.Entity)", "this").Right("where ");
                result += "\r\n " + indent + "  var _" + entity.Name + "Elements = changeSet.ChangeSetEntries.Where(e => e.Entity is " + entity.Name + " && ((" + entity.Name + ")e.Entity)." + this.Name + " == null && e.Associations == null && e.OriginalAssociations == null" + (parentRelation.IsNullOrEmpty() ? "" : " && " + parentRelation) + ").ToList();";
                result += "\r\n " + indent + "  if (_" + entity.Name + "Elements.Count > 0 && this." + entity.Name + "List.Count() == 0)";
                result += "\r\n " + indent + "  {";
                result += "\r\n " + indent + "      this." + entity.Name + "List = _" + entity.Name + "Elements.Select(e => (" + entity.Name + ")e.Entity).ToList();";
                result += "\r\n " + indent + "      List<int> indexDetails = new List<int>();";
                result += "\r\n " + indent + "      int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);";
                result += "\r\n " + indent + "      foreach (var detail in _" + entity.Name + "Elements)";
                result += "\r\n " + indent + "      {";
                result += "\r\n " + indent + "          indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));";
                result += "\r\n " + indent + "          ((" + entity.Name + ")detail.Entity)." + this.Name + " = this;";
                result += "\r\n " + indent + "          detail.Associations = new Dictionary<string, int[]>();";
                result += "\r\n " + indent + "          ((Dictionary<string, int[]>)detail.Associations).Add(\"" + this.Name + "\", new int[] { masterIndex });";

                if (entity.HasDetails())
                    result += "\r\n " + indent + "          ((" + entity.Name + ")detail.Entity).AdjustHierarchyForSaving(detail, changeSet);";

                result += "\r\n " + indent + "      }";
                result += "\r\n " + indent + "      hasChanges = true;";
                result += "\r\n " + indent + "      if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();";
                result += "\r\n " + indent + "      ((Dictionary<string, int[]>)entity.Associations).Add(\"" + entity.Name + "List\", indexDetails.ToArray());";
                result += "\r\n " + indent + "  }";
            }

            result += "\r\n ";
            result += "\r\n" + indent + "  return hasChanges;";
            result += "\r\n " + indent + "}";


            return result;
        }

        public void AddPropertyChangeEvent(EntityAdapterAttribute attribute, bool isChangingEvent)
        {
            string name = "On" + attribute.Name + (isChangingEvent ? "Changing" : "Changed");

            if (!this.ExistsEvent(name))
            {
                EntityAdapterEvent customEvent = new EntityAdapterEvent(this.Partition);
                customEvent.Name = name;
                customEvent.IsPartial = true;
                customEvent.IsUniqueOverload = true;
                customEvent.IsShared = true;
                customEvent.Access = OperationAccess.Default;
                customEvent.ReturnType = "void";
                customEvent.Parameters = (isChangingEvent ? attribute.Datatype + " value" : "");
                customEvent.DocComment = "Occurs " + (isChangingEvent ? "before" : "after") + " the value changed.";
                this.EntityAdapterEvents.Add(customEvent);
            }
            else
                MessageBox.Show("This event already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string GetQueryByParent(string indent, bool byEntitySearch, bool byPaging, bool byParentComposition, bool noAssociations)
        {
            var result = String.Empty;
            if (this.EnableQueryByParent && this.TargetEntityAdapter != null)
            {
                string methodName = (byPaging ? "Paged" : "") + this.TargetEntityAdapter.Name + (byParentComposition ? "ParentComposition" : "") + (!byPaging && byEntitySearch ? "ByEntitySearch" : "") + (!byPaging && noAssociations ? "NoAssociations" : "");

                result += "\r\n" + indent + this.QueryReturnType + "<" + this.Name + "> result = null;";
                result += "\r\n" + indent + "var entity = this.Get" + methodName + "(" + (byEntitySearch ? "serializedEntitySearch" : "") + (byPaging ? (byEntitySearch ? ", " : "") + "skip, take" : "") + (byEntitySearch ? ", jEntitySearch" : "") + ").FirstOrDefault();";
                result += "\r\n" + indent + "if (entity != null)";
                result += "\r\n" + indent + "{";
                result += "\r\n" + indent + "   result = entity." + this.Name + "List.As" + this.QueryReturnType.ToString().Right("I") + "<" + this.Name + ">();";
                result += "\r\n" + indent + "}";
                result += "\r\n" + indent + "return result;";
            }
            return result;
        }

        public string GetOrderByDefinition(string entityRef, bool byPaging, string linqEntityRef, string indent)
        {
            string result = String.Empty;
            List<string> orderNames = new List<string>();

            if (byPaging)
            {
                if (this.EntityAdapterRepresentation != null)
                {
                    foreach (var prop in this.GetAllInheritanceProperties().Where(e => e.IsPK && !e.DataRelationKey.IsNullOrEmpty() && !e.IsNullable()))
                    {
                        if (!orderNames.Contains(prop.DataRelationKey.Right(".")))
                        {
                            orderNames.Add(prop.DataRelationKey.Right("."));
                            result += (result.IsNullOrEmpty() ? String.Empty : ", ") + (!prop.DataRelationKey.Contains("#") ? prop.DataRelationKey : prop.DataRelationKey.Left("#") + "." + prop.DataRelationKey.Right(".")) + " " + prop.OrderByOrientation.ToString().ToLower();
                        }
                    }

                    //If has no order, get one property
                    if (result.IsNullOrEmpty())
                    {
                        var prop = this.GetAllInheritanceProperties().Where(e => !e.IsCustomized && e.AggregationFunction == UIAggregationFunctions.None && !e.DataRelationKey.IsNullOrEmpty()).FirstOrDefault();
                        if (!prop.IsNullOrEmpty())
                            result = (!prop.DataRelationKey.Contains("#") ? prop.DataRelationKey : prop.DataRelationKey.Left("#") + "." + prop.DataRelationKey.Right(".")) + " " + prop.OrderByOrientation.ToString().ToLower();
                    }
                }
                else
                {
                    foreach (var prop in this.GetAllInheritanceProperties().Where(e => this.IsPrimaryKey(e)).OrderBy(o => o.EdmKey.Occurs(".")))
                    {
                        if (!orderNames.Contains(prop.EdmKey.Right(".")))
                        {
                            orderNames.Add(prop.EdmKey.Right("."));
                            result += (result.IsNullOrEmpty() ? String.Empty : ", ") + ReplaceEdmPath(prop.EdmKey, entityRef) + " " + prop.OrderByOrientation.ToString().ToLower();
                        }
                    }

                    //If has no order, get one property
                    if (result.IsNullOrEmpty())
                    {
                        var prop = this.GetAllInheritanceProperties().Where(e => !e.IsCustomized && e.AggregationFunction == UIAggregationFunctions.None && !e.EdmKey.IsNullOrEmpty()).FirstOrDefault();
                        if (!prop.IsNullOrEmpty())
                            result = ReplaceEdmPath(prop.EdmKey, entityRef) + " " + prop.OrderByOrientation.ToString().ToLower();
                    }
                }
            }
            else
            {
                if (this.EntityAdapterRepresentation != null)
                {
                    foreach (var prop in this.GetAllInheritanceProperties().Where(e => e.OrderBySequence >= 0 && !e.DataRelationKey.IsNullOrEmpty()).OrderBy(o => o.OrderBySequence))
                    {
                        if (!orderNames.Contains(prop.DataRelationKey.Right(".")))
                        {
                            orderNames.Add(prop.DataRelationKey.Right("."));
                            result += (result.IsNullOrEmpty() ? String.Empty : ", ") + (!prop.DataRelationKey.Contains("#") ? prop.DataRelationKey : prop.DataRelationKey.Left("#") + "." + prop.DataRelationKey.Right(".")) + " " + prop.OrderByOrientation.ToString().ToLower();
                        }
                    }
                }
                else
                {
                    foreach (var prop in this.GetAllInheritanceProperties().Where(e => e.OrderBySequence >= 0 && !e.EdmKey.IsNullOrEmpty()).OrderBy(o => o.OrderBySequence))
                    {
                        if (!orderNames.Contains(prop.EdmKey.Right(".")))
                        {
                            orderNames.Add(prop.EdmKey.Right("."));
                            result += (result.IsNullOrEmpty() ? String.Empty : ", ") + ReplaceEdmPath(prop.EdmKey, entityRef) + " " + prop.OrderByOrientation.ToString().ToLower();
                        }
                    }
                }
            }

            return linqEntityRef + (result.IsNullOrEmpty() ? String.Empty : "\r\n" + indent + "orderby " + result);
        }

        public void AddServerEvent(string eventName)
        {
            EntityAdapterEvent customEvent = new EntityAdapterEvent(this.Partition);
            customEvent.Name = eventName;
            customEvent.OverloadName = eventName;
            customEvent.IsUniqueOverload = true;
            customEvent.Access = OperationAccess.Public;
            customEvent.ReturnType = "void";

            switch (eventName)
            {
                case "OnValidatingChanges":
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute for validating data changes.";
                    customEvent.ReturnType = "bool";
                    break;
                case "OnSavingChanges":
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute before save changes.";
                    break;
                case "OnSavingContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute before save context changes.";
                    break;
                case "OnSavedChanges":
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute after save changes.";
                    break;
                case "OnSavedContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute after save context changes.";
                    break;
                case "OnTransactingChanges":
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute on transaction starting.";
                    break;
                case "OnTransactingContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute on transaction context starting.";
                    break;
                case "OnTransactedChanges":
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeOperation changeOperation";
                    customEvent.DocComment = "Execute on transaction ending.";
                    break;
                case "OnTransactedContextChanges":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = Path.GetFileNameWithoutExtension(this.EntityAdapterDesignerRoot.DocumentName) + "DomainService context#ChangeSetEntry[] entities";
                    customEvent.DocComment = "Execute on transaction context ending.";
                    break;
                case "OnPrepareForSearching":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "ref string dynQuery#List<EntitySearch> searchList";
                    customEvent.DocComment = "Prepare filter for searching.";
                    break;
                case "OnSearching":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "ref " + (!this.PrimaryEntity.IsNullOrEmpty() ? this.QueryReturnType.ToString() : "IEnumerable") + "<" + this.Name + "> searchDefinition#bool noAssociations#List<EntitySearch> searchList";
                    customEvent.DocComment = "Execute before search data.";
                    break;
                case "OnFiltering":
                    customEvent.IsStatic = true;
                    customEvent.Parameters = "List<EntitySearch> searchList";
                    customEvent.DocComment = "Execute before apply filter.";
                    break;
                case "OnSearchingReplacement":
                    var edm = this.EntityAdapterDesignerRoot.EntityDataModels.FirstOrDefault();
                    customEvent.IsStatic = true;
                    if (!edm.IsNull() && !this.PrimaryEntity.IsNullOrEmpty())
                        customEvent.Parameters = edm.TargetNamespace + "." + edm.Name + " context#string dynQuery#List<ObjectParameter> parameters#List<EntitySearch> entitySearchList";
                    else
                        customEvent.Parameters = "List<EntitySearch> entitySearchList";
                    customEvent.ReturnType = (!this.PrimaryEntity.IsNullOrEmpty() ? this.QueryReturnType.ToString() : "IEnumerable") + "<" + this.Name + ">";
                    customEvent.DocComment = "Replace the automatic search method.";
                    break;
                default:
                    foreach (LookUpAdapter lookUpAdapter in this.LookUpAdapters)
                    {
                        if (lookUpAdapter.IsOnLookingUp(eventName))
                        {
                            customEvent.IsStatic = true;
                            customEvent.Parameters = "ref " + lookUpAdapter.QueryReturnType.ToString() + "<" + lookUpAdapter.Name + "> searchDefinition#System.String propertyName#EntitySearch entitySearch";
                            customEvent.DocComment = "Execute before lookup on server side.";
                        }
                    }
                    break;
            }

            //Add event
            this.EntityAdapterEvents.Add(customEvent);
        }

        public void AddClientEvent(string eventName)
        {
            EntityAdapterClientEvent customEvent = new EntityAdapterClientEvent(this.Partition);
            customEvent.Name = eventName;
            customEvent.OverloadName = eventName;
            customEvent.IsUniqueOverload = true;
            customEvent.Access = OperationAccess.Public;
            string eventConfig = GetClientEventDefinition(eventName);
            customEvent.ReturnType = eventConfig.Left(" | ");
            customEvent.Parameters = eventConfig.Right(" | ");

            //Add event
            this.EntityAdapterClientEvented.Add(customEvent);
        }

        public string GetClientEventDefinition(string eventName)
        {
            string returnType = "void", parameters = "";

            switch (eventName)
            {
                case "OnDeleting":
                case "OnAdding":
                case "OnSaving":
                case "OnDataRefreshing":
                    returnType = "bool";
                    break;
                case "OnDetailSearching":
                    returnType = "string";
                    parameters = "string detailName";
                    break;
                case "OnAllDetailsSearched":
                    break;
                case "OnDetailSearched":
                    parameters = "string detailName";
                    break;
                case "OnPropertyChanged":
                    parameters = "string propertyName#object oldValue#object newValue";
                    break;
                default:
                    if (LookUpAdapter.IsOnLookedUp(eventName))
                    {
                        parameters = "object lookUpElement";
                    }
                    else if (LookUpAdapter.IsBeforeQuery(eventName))
                    {
                        returnType = "string";
                        parameters = "string fieldToSearch#object lookupInfo";
                    }
                    else if (LookUpAdapter.IsOnLoadingQuery(eventName))
                    {
                        parameters = "object data";
                    }
                    break;
            }

            return returnType + " | " + parameters;
        }

        public List<string> GetClientEventNames()
        {
            List<string> result = new List<string>();

            result.Add("OnDetailSearching");
            result.Add("OnDetailSearched");
            result.Add("OnAllDetailsSearched");
            result.Add("OnSelected");
            result.Add("OnPropertyChanged");
            result.Add("OnSaving");
            result.Add("OnSaved");
            result.Add("OnDeleting");
            result.Add("OnAdding");
            result.Add("OnDeleted");
            result.Add("OnAdded");
            result.Add("OnDataRefreshing");
            result.Add("OnDataRefreshed");

            foreach (LookUpAdapter lookUpAdapter in this.LookUpAdapters)
            {
                result.Add(lookUpAdapter.GetBeforeQueryName());
                result.Add(lookUpAdapter.GetOnLookedUpName());
                result.Add(lookUpAdapter.GetOnLoadingQueryName());
            }

            foreach (var attr in this.GetAllInheritanceAttributes())
            {
                if (!attr.LookUpSubscription.IsNullOrEmpty())
                {
                    string lookupName = attr.GetLookUpName();
                    if (!lookupName.IsNullOrEmpty())
                    {
                        result.Add(LookUpAdapter.GetBeforeQueryName(lookupName));
                        result.Add(LookUpAdapter.GetOnLookedUpName(lookupName));
                        result.Add(LookUpAdapter.GetOnLoadingQueryName(lookupName));
                    }
                }
            }

            return result;
        }


        public string GenerateWorkflowInvokers(string indent)
        {
            string body = String.Empty;

            foreach (GenericOperation operation in this.EntityAdapterOperations)
            {
                if (operation.Workflow != null)
                    body += "\r\n" + Linx.Builder.Resources.CodeGen.GetWorkflowInvoker(operation.Workflow.Name, operation.IsStatic, operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries), indent) + "\r\n";
            }

            foreach (GenericOperation operation in this.EntityAdapterEvents)
            {
                if (operation.Workflow != null)
                    body += "\r\n" + Linx.Builder.Resources.CodeGen.GetWorkflowInvoker(operation.Workflow.Name, operation.IsStatic, operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries), indent) + "\r\n";
            }

            if (!body.IsNullOrEmpty())
            {
                body = "\r\n" + indent + "#region Workflow Invoke Definitions" + body + "\r\n" + indent + "#endregion Workflow Invoke Definitions";
            }

            return body;
        }


        public string GetEnvPart()
        {
            return this.EntityAdapterDesignerRoot.GetDirectorySourcePart();
        }
    }

}
