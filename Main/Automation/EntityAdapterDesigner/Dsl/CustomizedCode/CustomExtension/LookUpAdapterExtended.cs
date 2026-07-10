using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;
using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomizedCode;

namespace Linx.EntityAdapterDesigner
{
    public partial class LookUpAdapter : IAditionalInformation
    {
        public static string GetLookUpName(string lookUpInfo)
        {
            return (lookUpInfo.IsNullOrEmpty() ? String.Empty : lookUpInfo.Extract("::", "#"));
        }

        public string GetEnumDefinitions(string indent)
        {
            string body = "";

            foreach (var fieldDef in this.LookUpProperties)
            {
                body += EntityAdapterAttribute.GetEnumDefinitions(indent, this.EntityAdapterDesignerRoot.TargetNamespace, fieldDef.Name, fieldDef.DomainName, fieldDef.KpiName, fieldDef.DisplayName);
            }

            return body;
        }

        public void Reorder(List<LookUpProperty> propertiesOrderList)
        {
            //Adjust last order
            for (int propIndex = 0; propIndex < propertiesOrderList.Count; propIndex++)
            {
                var prop = this.LookUpProperties.FirstOrDefault(e => e.Name == propertiesOrderList[propIndex].Name);
                if (prop != null) this.LookUpProperties.Move(prop, (propIndex < this.LookUpProperties.Count ? propIndex : this.LookUpProperties.Count - 1));
            }
        }

        public static bool GetMultiSelectionValue(string lookUpInfo)
        {
            bool result = false;
            if (!lookUpInfo.IsNullOrEmpty())
            {
                string[] parts = lookUpInfo.Right("::").Split(new char[] { '#' });
                if (parts.Length > 2)
                    result = parts[2] == "true";
            }
            return result;
        }

        public static string GetSubstituteProperties(string lookUpInfo)
        {
            string result = String.Empty;
            if (!lookUpInfo.IsNullOrEmpty())
            {
                string[] parts = lookUpInfo.Left("::").Split(new char[] { '#' });
                return (parts.Length > 9 ? parts[9] : "");
            }
            return result;
        }

        public EntityDataModel GetCurrentDataModel()
        {
            var baseClass = this.GetTopBaseClass();
            if (baseClass.EntityDataModel != null)
                return baseClass.EntityDataModel;
            else
                return (baseClass.EntityAdapter != null ? baseClass.EntityAdapter.GetCurrentDataModel() : null);
        }

        public LookUpAdapter GetTopBaseClass()
        {
            LookUpAdapter baseClass = this;
            while (true)
            {
                if (baseClass.BaseLookUpAdapter.IsNull())
                    break;
                baseClass = baseClass.BaseLookUpAdapter;
            }
            return baseClass;
        }

        public void AdjustColorShape()
        {
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as LookUpAdapterShape;
            if (shape != null)
            {
                if (this.BaseLookUpAdapter != null)
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

        public string GetEntityNameByRelation(string fkRelation)
        {
            if (!this.EntityRelations.IsNullOrEmpty() && !fkRelation.IsNullOrEmpty())
                return ("#" + this.EntityRelations).Extract("#" + fkRelation + "(", ")");
            else
                return "";
        }

        public static string GetBeforeQueryName(string name)
        {
            return "BeforeGet" + name + "Query";
        }

        public static string GetOnLookingUpName(string name)
        {
            return "OnLookingUp" + name;
        }

        public static string GetOnLookedUpName(string name)
        {
            return "OnLookedUp" + name;
        }

        public static string GetOnLoadingQueryName(string name)
        {
            return "OnLoading" + name + "Query";
        }

        public string GetBeforeQueryName()
        {
            return GetBeforeQueryName(this.Name);
        }

        public string GetOnLookingUpName()
        {
            return GetOnLookingUpName(this.Name);
        }

        public string GetOnLookedUpName()
        {
            return GetOnLookedUpName(this.Name);
        }

        public string GetOnLoadingQueryName()
        {
            return GetOnLoadingQueryName(this.Name);
        }

        public bool IsOnLookingUp(string eventName)
        {
            return (eventName.InList("OnLookingUp" + this.Name, "OnLookingUp" + this.RelationName, "OnLookingUp" + this.EntitySource)
                    //This is for compatibility with previous version
                    || eventName.InList("OnLookUping" + this.Name, "OnLookUping" + this.RelationName, "OnLookUping" + this.EntitySource));
        }

        public static bool IsOnLookedUp(string eventName)
        {
            return (eventName.Length > 10 && (eventName.Left(10) == "OnLookedUp"
                    //This is for compatibility with previous version
                    || eventName.Left(10) == "OnLookUped"));
        }

        public static bool IsBeforeQuery(string eventName)
        {
            return (eventName.Length > 9 && eventName.Left(9) == "BeforeGet");
        }

        private string GetDefaultByDataType(string dataType)
        {
            return this.EntityAdapterDesignerRoot.GetDefaultByDataType(dataType);
        }

        public static bool IsOnLoadingQuery(string eventName)
        {
            return (eventName.Length > 14 && eventName.Left(9) == "OnLoading" && eventName.Right(5) == "Query");
        }

        public string GetEntityOnLookingUp()
        {
            string result = String.Empty;
            if (this.EntityAdapter != null)
            {
                if (this.EntityAdapter.ExistsEvent("OnLookingUp" + this.Name))
                    result = "OnLookingUp" + this.Name;
                else if (!this.RelationName.IsNullOrEmpty() && this.EntityAdapter.ExistsEvent("OnLookingUp" + this.RelationName))
                    result = "OnLookingUp" + this.RelationName;
                else if (!this.EntitySource.IsNullOrEmpty() && this.EntityAdapter.ExistsEvent("OnLookingUp" + this.EntitySource))
                    result = "OnLookingUp" + this.EntitySource;
                //This is for compatibility with previous version    
                else if (this.EntityAdapter.ExistsEvent("OnLookUping" + this.Name))
                    result = "OnLookUping" + this.Name;
                else if (!this.RelationName.IsNullOrEmpty() && this.EntityAdapter.ExistsEvent("OnLookUping" + this.RelationName))
                    result = "OnLookUping" + this.RelationName;
                else if (!this.EntitySource.IsNullOrEmpty() && this.EntityAdapter.ExistsEvent("OnLookUping" + this.EntitySource))
                    result = "OnLookUping" + this.EntitySource;
            }

            return result;
        }

        public string GetEntityOnLookedUp()
        {
            string result = String.Empty;
            if (this.EntityAdapter != null)
            {
                if (this.EntityAdapter.ExistsClientEvent("OnLookedUp" + this.Name))
                    result = "OnLookedUp" + this.Name;
                else if (!this.RelationName.IsNullOrEmpty() && this.EntityAdapter.ExistsClientEvent("OnLookedUp" + this.RelationName))
                    result = "OnLookedUp" + this.RelationName;
                else if (!this.EntitySource.IsNullOrEmpty() && this.EntityAdapter.ExistsClientEvent("OnLookedUp" + this.EntitySource))
                    result = "OnLookedUp" + this.EntitySource;
            }

            return result;
        }

        public string GetSpecializedLookUp()
        {
            return (this.DisableSpecializedUI ? String.Empty : (!String.IsNullOrEmpty(this.SpecializedUI) ? this.SpecializedUI : this.EntityAdapterDesignerRoot.GetSpecializedLookUp(this.EntitySource)));
        }

        public void RemoveInheritanceConflicts()
        {
            if (this.BaseLookUpAdapter != null)
            {
                var derivedAttributes = this.LookUpProperties;
                if (derivedAttributes.Count > 0)
                {
                    var baseAttributes = this.BaseLookUpAdapter.GetAllInheritanceAttributes().Select(e => e.Name).ToList(); ;
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
                                    this.LookUpProperties.Remove(attribute);
                                }
                                transaction.Commit();
                            }
                        }
                    }
                }

            }
        }

        public void UpdateBaseClassInfo(bool force = false)
        {
            if (this.BaseLookUpAdapter != null && (force || this.RelationName != this.BaseLookUpAdapter.RelationName))
            {
                using (Transaction transaction =
                          this.Store.TransactionManager.BeginTransaction("Adjust class base info."))
                {
                    this.RelationName = this.BaseLookUpAdapter.RelationName;
                    transaction.Commit();
                }
            }
        }

        public List<LookUpProperty> GetInheritanceProperties()
        {
            List<LookUpProperty> result = new List<LookUpProperty>();

            LookUpAdapter baseLP = this.BaseLookUpAdapter;
            while (baseLP != null)
            {
                result.AddRange(baseLP.LookUpProperties);
                baseLP = baseLP.BaseLookUpAdapter;
            }

            return result;
        }

        public List<LookUpProperty> GetAllInheritanceAttributes()
        {
            List<LookUpProperty> result = new List<LookUpProperty>();

            //Add this instance properties
            result.AddRange(this.LookUpProperties);
            //Add inheritance properties
            result.AddRange(this.GetInheritanceProperties());

            return result;
        }


        public List<LookUpProperty> GetDerivedProperties(bool addThisInstance = false)
        {
            List<LookUpProperty> result = new List<LookUpProperty>();

            if (addThisInstance)
                result.AddRange(this.LookUpProperties);

            foreach (var dLP in this.DerivedLookUpAdapters)
            {
                result.AddRange(dLP.GetDerivedProperties(true));
            }

            return result;
        }

        public string GetLookUpClassInfoValue()
        {
            return (this.BaseLookUpAdapter != null ? "Base Type: " + this.BaseLookUpAdapter.Name : "");
        }

        public LookUpProperty GetLookUpProperty(string propertyName)
        {
            return (this.LookUpProperties.Where(e => e.Name == propertyName).FirstOrDefault());
        }

        public bool HasBrand()
        {
            return this.GetAllInheritanceAttributes().Any(p => p.Name == "IdBandeiraRede");
        }

        public string GetBusinessFilterByRelation(string alias, bool isDbContext, string fkRelation)
        {
            var edmInfo = this.EntityAdapterDesignerRoot.GetEdm().EdmInfo;
            if (edmInfo.IsRequiredPath(fkRelation))
            {
                var tableName = this.GetEntityNameByRelation(fkRelation.Right("."));
                var type = edmInfo.GetTypeByName(tableName);
                if (type != null && type.Properties.Any(p => p.Name == "ID_GPECON"))
                {
                    return "(!this.HasGpeconControl || " + alias + ".ID_GPECON == this." + (isDbContext ? "DbContext" : "ObjectContext") + ".IdGpecon)";
                }
            }
            return "";
        }

        public string GetEntitiesSelectionForLinq(bool isDbContext, string alias, string indent, ref string letFilter)
        {
            string result = String.Empty, businessFilter = String.Empty;
            bool hasMainBusinessFilter = this.EntityAdapterDesignerRoot.HasMainBusinessFilter(this.EntitySource);
            foreach (var element in GetEdmEntities(alias).OrderBy(e => e.Key.Length))
            {
                if (element.Key != this.EntitySource)
                {
                    result += "\r\n " + indent + " let " + element.Value + " = " + alias + "." + ("#" + element.Key).Right("#" + EntitySource + ".");

                    //Check business filter
                    if (!hasMainBusinessFilter)
                    {
                        string bFilterPart = this.GetBusinessFilterByRelation(element.Value, isDbContext, element.Key);
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

            return "from " + alias + " in this." + (isDbContext ? "DbContext" : "ObjectContext") + "." + (this.EntitySourceBase.IsNullOrEmpty() || isDbContext ? this.EntitySource : this.EntitySourceBase + ".OfType<" + this.EntitySource + ">()") + ".Where" + (!this.EntitySourceBase.IsNullOrEmpty() && isDbContext ? "<" + this.EntitySourceBase + ">" : String.Empty) + "(dynQuery, parameters.ToArray())" + result;
        }

        public Dictionary<string, string> GetEdmEntities(string alias)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string reference;
            int cntAlias = 0;
            dict.Add(this.EntitySource, alias);

            foreach (var prop in this.GetAllInheritanceAttributes())
            {
                if (prop.IsCustomized)
                    continue;

                if (MacroEngineHelper.HasMacro(prop.EdmKey, this))
                    continue;

                reference = (prop.EdmKey + "#").Left("." + prop.EdmKey.Right(".") + "#");
                if (!reference.IsNullOrEmpty() && !dict.ContainsKey(reference))
                {
                    cntAlias++;
                    dict.Add(reference, alias + "Al" + cntAlias.ToString());
                }
            }

            return dict;
        }

        public string ReplaceEdmPath(string edmPth, string alias, bool byEntitySQL = false)
        {
            string result = MacroEngineHelper.ReplaceMacros(edmPth, byEntitySQL ? MacroOutputType.EntitySQL : MacroOutputType.CSharp, this);
            foreach (var element in GetEdmEntities(alias).OrderByDescending(e => e.Key.Length))
                result = result.Replace(element.Key + ".", element.Value + ".");
            return result;
        }

        public string GetEntitiesDescription()
        {
            string descriptionReturn = "";
            List<LookUpProperty> details;

            //Primary key 
            var keys = this.GetAllInheritanceAttributes().Where(e => e.IsPrimaryKey);
            if (keys.Count() == 1)
            {
                details = keys.Where(e => e.Datatype.InList("System.Int32", "Int32")).ToList();
                if (details.Count == 1)
                    descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + this.EntitySource + ":" + details[0].Name;
                else
                {
                    details = keys.Where(e => e.Datatype.InList("System.Guid", "Guid")).ToList();
                    if (details.Count == 1)
                        descriptionReturn += (descriptionReturn.IsNullOrEmpty() ? ";Entities[" : "|") + this.EntitySource + ":" + details[0].Name;
                }
            }

            if (!descriptionReturn.IsNullOrEmpty())
                descriptionReturn += "]";

            descriptionReturn += ";EdmEntityName[" + this.EntitySource + "]";

            return descriptionReturn;
        }


        public bool ValidProperties()
        {
            string edmKey;
            //Remove the property that not exists into EntityAdapter properties.
            for (int idxProp = this.LookUpProperties.Count - 1; idxProp >= 0; idxProp--)
            {
                edmKey = this.RelationName + "." + ("#" + this.LookUpProperties[idxProp].EdmKey).Right("#" + this.EntitySource + ".");
                if (!this.LookUpProperties[idxProp].IsCustomized && !this.EntityAdapter.ExistsPropertyByEdmKey(edmKey) && !this.EntityAdapter.GetAllInheritanceAttributes().Any(e => e.Name == this.LookUpProperties[idxProp].EntityPropertyRelated))
                {
                    this.LookUpProperties[idxProp].Delete();
                }
            }

            //Remove me if not exists any property.
            if (this.LookUpProperties.Count == 0)
            {
                this.Delete();
                return false;
            }
            else
                return true;
        }

        private string GetAttributeDefinitions(LookUpProperty propertyDef, string indent, int order)
        {
            string body = "\r\n" + indent + "[DataMember()]", edmKey = propertyDef.EdmKey;

            if (propertyDef.IsPrimaryKey) //&& !(propertyDef.Datatype.ToLower().Contains("?") || propertyDef.Datatype.ToLower().Contains("nullable<"))
                body += "\r\n" + indent + "[Key()]";

            body += "\r\n" + indent + "[XmlAttribute()]";
            body += "\r\n" + indent + "[Editable(true)]";
            body += "\r\n" + indent + @"[Display(Name = """ + propertyDef.DisplayName.Replace("\t", "").Replace("\r", "").Replace("\n", "") + @""", Description="""", Order = " + order.ToString() + @", AutoGenerateField = " + propertyDef.IsBrowsable.ToString().ToLower() + @", GroupName="""", ResourceType= null)]";

            string precision = (propertyDef.Precision.IsNullOrEmpty() ? "0" : propertyDef.Precision);
            int strlen = (precision.Contains(":") ? int.Parse(precision.Left(":")) : (int.Parse(precision) / 10));
            if (strlen > 0 && !propertyDef.Datatype.Contains("[]") && propertyDef.Datatype.ToLower().Contains("string"))
                body += "\r\n" + indent + "[StringLength(" + strlen.ToString() + ")]";

            body += "\r\n" + indent + @"[FunctionalPoint(""IsEditable[false];ObjectClass[" + GetClassName(propertyDef) + @"];FilterDataKey[" + MacroEngineHelper.ReplaceMacrosEntitySql(edmKey, this) + @"]"")]";

            return body;
        }


        //Get derived classes for KnownTypeAttribute
        public string GetDerivedAttributeDefinitions(string indent)
        {
            string body = String.Empty;

            foreach (var derivedClass in this.DerivedLookUpAdapters)
            {
                body += "\r\n" + indent + "[KnownType(typeof(" + derivedClass.Name + "))]";
            }

            return body;
        }


        private string GetClassName(LookUpProperty propertyDef)
        {
            if (!propertyDef.DomainName.IsNullOrEmpty())
                return DisplayControlType.ComboBox.ToString();
            else if (propertyDef.Datatype.ToLower().InList("bool", "boolean", "system.boolean", "system.nullable<bool>", "system.nullable<boolean>", "system.nullable<system.boolean>"))
                return DisplayControlType.CheckBox.ToString();
            else
                return DisplayControlType.TextBox.ToString();
        }

        public string GetPropertyDefinitions(string indent)
        {
            string body = "", dataType;
            LookUpProperty propertyDef;

            for (int idxOrder = 0; idxOrder < this.LookUpProperties.Count; idxOrder++)
            {
                propertyDef = this.LookUpProperties[idxOrder];

                dataType = propertyDef.Datatype;
                body += "\r\n\r\n" + indent + "private " + dataType + " _" + propertyDef.Name + ";";

                //Getting attribute definitions
                body += GetAttributeDefinitions(propertyDef, indent, idxOrder);

                body += "\r\n" + indent + "public " + dataType + " " + propertyDef.Name;
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "get";
                body += "\r\n" + indent + indent + "{";
                body += "\r\n" + indent + indent + "      return _" + propertyDef.Name + ";";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + indent + "set";
                body += "\r\n" + indent + indent + "{";
                body += "\r\n" + indent + indent + "      if (this._" + propertyDef.Name + " != value)";
                body += "\r\n" + indent + indent + "      {";
                body += "\r\n" + indent + indent + "          this._" + propertyDef.Name + " = value;";
                body += "\r\n" + indent + indent + "      }";
                body += "\r\n" + indent + indent + "}";
                body += "\r\n" + indent + "}";
            }

            return body;
        }


        public string GetRelationFieldsToLinq(string alias, string ident)
        {
            string relationFieldsToLinq = String.Empty;

            foreach (LookUpProperty property in this.GetAllInheritanceAttributes().Where(e => !e.IgnoreMetaData))
            {
                relationFieldsToLinq += "\r\n" + ident + (relationFieldsToLinq.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + ReplaceEdmPath(property.EdmKey, alias);
            }

            return relationFieldsToLinq;
        }

        public string GetSubQueryDefinitions(string ident)
        {
            if (!this.EnableSubLookups || this.EntityAdapter == null || !this.EntityAdapter.HasEdmSource())
                return String.Empty;

            string relationFieldsToLinq = String.Empty;
            if (HasSubQueryFields())
            {
                var properties = this.GetAllInheritanceAttributes().Where(e => !e.IgnoreMetaData);
                bool isCustom = properties.Any(e => !e.GetCustomHierarchy().IsNullOrEmpty());
                string rightPart;
                string[] pathDefinition;
                if (isCustom)
                {
                    var maxNPoint = properties.Max(e => e.GetCustomHierarchy().Occurs("."));
                    pathDefinition = properties.Where(e => e.GetCustomHierarchy().Occurs(".") == maxNPoint).Select(e => e.GetCustomHierarchy()).First().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    var maxNPoint = properties.Max(e => e.EdmKey.Occurs("."));
                    pathDefinition = properties.Where(e => e.EdmKey.Occurs(".") == maxNPoint).Select(e => e.EdmKey).First().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                }

                var pathGroup = pathDefinition[0];
                string linqFields = String.Empty;
                for (int idx = 1; idx < pathDefinition.Length - 1; idx++)
                {
                    pathGroup += "." + pathDefinition[idx];
                    string pathGroupInverseToOne = (isCustom ? "" : GetHasInverseOneRelation(pathGroup));
                    relationFieldsToLinq += "\r\n" + ident + (relationFieldsToLinq.IsNullOrEmpty() ? "" : "else ") + "if (propertyName.InList(";
                    linqFields = String.Empty;
                    foreach (LookUpProperty property in properties)
                    {
                        if (isCustom)
                            rightPart = (property.GetCustomHierarchy().Contains(pathGroup + ".") ? property.GetCustomHierarchy().Right(pathGroup + ".") : "");
                        else
                            rightPart = (property.EdmKey.Contains(pathGroup + ".") ? property.EdmKey.Right(pathGroup + ".") : "");

                        if (!rightPart.IsNullOrEmpty() && !rightPart.Contains("."))
                            linqFields += (linqFields.IsNullOrEmpty() ? "" : ", ") + "\"" + property.Name + "\"";
                    }
                    relationFieldsToLinq += linqFields + "))";
                    relationFieldsToLinq += "\r\n" + ident + "{";
                    relationFieldsToLinq += "\r\n   " + ident + "query = (from r in query select new " + this.Name + "() {";
                    linqFields = String.Empty;
                    foreach (LookUpProperty property in properties)
                    {
                        if (isCustom)
                        {
                            if (property.GetCustomHierarchy().Contains(pathGroup + "."))
                                linqFields += "\r\n   " + ident + (linqFields.IsNullOrEmpty() ? "" : ", ") + property.Name + " = r." + property.Name;
                            else
                                linqFields += "\r\n   " + ident + (linqFields.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + this.GetDefaultByDataType(property.Datatype);
                        }
                        else
                        {
                            if ((pathGroupInverseToOne.IsNullOrEmpty() ? property.EdmKey.Contains(pathGroup + ".") : property.EdmKey.Contains(pathGroupInverseToOne + ".")))
                                linqFields += "\r\n   " + ident + (linqFields.IsNullOrEmpty() ? "" : ", ") + property.Name + " = r." + property.Name;
                            else
                                linqFields += "\r\n   " + ident + (linqFields.IsNullOrEmpty() ? "" : ", ") + property.Name + " = " + this.GetDefaultByDataType(property.Datatype);
                        }
                    }
                    relationFieldsToLinq += linqFields + "\r\n    " + ident + "}).Distinct();";
                    relationFieldsToLinq += "\r\n" + ident + "}";
                }
            }

            return (relationFieldsToLinq.IsNullOrEmpty() ? "" : "\r\n" + ident + "//Inner Group Definition") + relationFieldsToLinq;
        }

        public string GetHasInverseOneRelation(string pathGroup)
        {
            var model = this.EntityAdapter.GetCurrentDataModel();
            if (model != null && model.EdmInfo != null)
            {
                var parts = pathGroup.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var topEntityName = parts[0];
                var currentEntity = model.EdmInfo.GetTypeByName(topEntityName);
                if (currentEntity.IsNull())
                    throw new ArgumentOutOfRangeException(topEntityName, "The Object not found in BM");
                ContextEntity previousLevelType = null;
                for (int idx = 1; idx < parts.Length; idx++)
                {
                    var propertyNavigationName = parts[idx];
                    var propertyNavigation = currentEntity.Properties.FirstOrDefault(p => p.Name == propertyNavigationName);
                    if (propertyNavigation.IsNull())
                        throw new ArgumentOutOfRangeException(propertyNavigationName, string.Format("The Property Navigation not found in Object BM [{0}].", topEntityName));
                    previousLevelType = currentEntity;
                    currentEntity = model.EdmInfo.Metadata.Entities.FirstOrDefault(e => e.Name == propertyNavigation.DataType);
                    if (idx == parts.Length - 1)
                    {
                        if (propertyNavigation.Decorators.Any(d => d.Contains("InverseProperty")))
                        {
                            return pathGroup.Left(pathGroup.Length - propertyNavigationName.Length - 1);
                        }
                        else if (IsInverseToOne(currentEntity, propertyNavigationName, previousLevelType.Name))
                        {
                            return pathGroup.Left(pathGroup.Length - propertyNavigationName.Length - 1);
                        }
                    }
                }
            }

            return "";
        }

        private bool IsInverseToOne(ContextEntity entityType, string navigationName, string inverseTypeName)
        {
            foreach (var member in entityType.Properties)
            {
                var inv = member.Decorators.FirstOrDefault(m => m.Contains("InverseProperty"));
                if (!inv.IsNullOrEmpty() && inv.Extract("InverseProperty('", "')") == navigationName && member.DataType == inverseTypeName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSubQueryFields()
        {
            var properties = this.GetAllInheritanceAttributes().Where(e => !e.IgnoreMetaData);
            if (properties.Any(e => !e.CustomHierarchy.IsNullOrEmpty()))
                return true;
            else
            {
                var maxNPoint = properties.Max(e => e.EdmKey.Occurs("."));
                return (maxNPoint > 1);
            }
        }

        public bool HasAnyClientFilter()
        {
            return !this.ClientFilterExpression.IsNullOrEmpty() || (this.EntityAdapter != null && this.EntityAdapter.ExistsClientEvent(this.GetBeforeQueryName())) || this.HasSubQueryFields();
        }

        public string GetSubQueryFields()
        {
            if (!this.EnableSubLookups || this.EntityAdapter == null || !this.EntityAdapter.HasEdmSource())
                return String.Empty;

            string result = String.Empty;
            if (HasSubQueryFields())
            {
                var properties = this.GetAllInheritanceAttributes().Where(e => !e.IgnoreMetaData);
                bool isCustom = properties.Any(e => !e.GetCustomHierarchy().IsNullOrEmpty());
                string rightPart;
                string[] pathDefinition;
                if (isCustom)
                {
                    var maxNPoint = properties.Max(e => e.GetCustomHierarchy().Occurs("."));
                    pathDefinition = properties.Where(e => e.GetCustomHierarchy().Occurs(".") == maxNPoint).Select(e => e.GetCustomHierarchy()).First().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    var maxNPoint = properties.Max(e => e.EdmKey.Occurs("."));
                    pathDefinition = properties.Where(e => e.EdmKey.Occurs(".") == maxNPoint).Select(e => e.EdmKey).First().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                }

                var pathGroup = pathDefinition[0];
                for (int idx = 1; idx < pathDefinition.Length - 1; idx++)
                {
                    pathGroup += "." + pathDefinition[idx];
                    string pathGroupInverseToOne = (isCustom ? "" : GetHasInverseOneRelation(pathGroup));

                    var linqKeys = String.Empty;
                    foreach (LookUpProperty property in properties)
                    {
                        if (isCustom)
                            rightPart = (property.GetCustomHierarchy().Contains(pathGroup + ".") ? property.GetCustomHierarchy().Right(pathGroup + ".") : "");
                        else
                            rightPart = (property.EdmKey.Contains(pathGroup + ".") ? property.EdmKey.Right(pathGroup + ".") : "");
                        if (!rightPart.IsNullOrEmpty() && !rightPart.Contains("."))
                            linqKeys += (linqKeys.IsNullOrEmpty() ? "" : ",") + property.Name;
                    }

                    var linqFields = String.Empty;
                    foreach (LookUpProperty property in properties)
                    {
                        if (isCustom)
                        {
                            if (property.GetCustomHierarchy().Contains(pathGroup + "."))
                                linqFields += (linqFields.IsNullOrEmpty() ? "" : ",") + property.Name;
                        }
                        else
                        {
                            if ((pathGroupInverseToOne.IsNullOrEmpty() ? property.EdmKey.Contains(pathGroup + ".") : property.EdmKey.Contains(pathGroupInverseToOne + ".")))
                                linqFields += (linqFields.IsNullOrEmpty() ? "" : ",") + property.Name;
                        }
                    }

                    result += (result.IsNullOrEmpty() ? "" : ";") + linqKeys + "[" + linqFields + "]";
                }
            }

            return result;
        }

        public string GetFixedFilter(string key)
        {
            string filterSelection = "", filter;

            if (this.LookUpProperties != null)
            {
                foreach (var property in this.GetAllInheritanceAttributes())
                {
                    if (!property.Filter.IsNullOrEmpty())
                    {
                        filter = property.Filter;
                    }
                    else filter = String.Empty;

                    if (!filter.IsNullOrEmpty())
                    {
                        filterSelection += (filterSelection == "" ? "" : " && ") + this.ReplaceEdmPath(filter.Replace("[Value]", property.EdmKey).Replace("[ThisRef]", this.EntitySource), key);
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
                    filterSelection += (filterSelection == "" ? "" : " && ") + this.ReplaceEdmPath(filter.Replace("[ThisRef]", this.EntitySource), key);
                }
                baseClass = baseClass.BaseLookUpAdapter;
            }

            return (filterSelection.IsNullOrEmpty() ? "" : "(" + filterSelection + ")");
        }

        public string GetSubQueryClientFilters()
        {
            if (!this.EnableSubLookups || this.EntityAdapter == null || !this.EntityAdapter.HasEdmSource())
                return String.Empty;

            string result = String.Empty;

            if (HasSubQueryFields())
            {
                var properties = this.GetAllInheritanceAttributes().Where(e => !e.IgnoreMetaData);
                bool isCustom = properties.Any(e => !e.GetCustomHierarchy().IsNullOrEmpty());

                foreach (LookUpProperty defProperty in properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                {
                    var linqFields = String.Empty;

                    if (isCustom)
                    {
                        foreach (LookUpProperty property in properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty() && e.GetCustomHierarchy().Occurs(".") > defProperty.GetCustomHierarchy().Occurs(".")))
                        {
                            linqFields += (linqFields.IsNullOrEmpty() ? "" : ",") + property.Name + "=" + property.EntityPropertyRelated;
                        }
                    }
                    else
                    {
                        foreach (LookUpProperty property in properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty() && e.EdmKey.Occurs(".") > defProperty.EdmKey.Occurs(".")))
                        {
                            linqFields += (linqFields.IsNullOrEmpty() ? "" : ",") + property.Name + "=" + property.EntityPropertyRelated;
                        }
                    }

                    if (!linqFields.IsNullOrEmpty())
                        result += (result.IsNullOrEmpty() ? "" : ";") + defProperty.EntityPropertyRelated + "[" + linqFields + "]";
                }
            }

            return result;
        }

        private string GetSuffix(LookUpProperty prop)
        {
            string suffix = String.Empty;

            if (this.EntityAdapter != null)
            {
                var entityPubProp = this.EntityAdapter.GetAllInheritancePublicationProperties().Where(e => e.Name == prop.EntityPropertyRelated).FirstOrDefault();
                if (entityPubProp != null)
                    suffix = entityPubProp.Suffix;
                else
                {
                    var entityProp = this.EntityAdapter.GetAllInheritanceProperties().Where(e => e.Name == prop.EntityPropertyRelated).FirstOrDefault();
                    if (entityProp != null)
                        suffix = entityProp.PublicationSuffix;
                }
            }

            return suffix;
        }

        //Get lookup informations from 
        public Dictionary<string, string> GetLookupInfoFromSource(bool byPropertyRelated)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var properties = this.GetAllInheritanceAttributes();
            int order = 0;
            string key;
            foreach (LookUpProperty property in properties)
            {
                key = (byPropertyRelated ? property.EntityPropertyRelated : property.Name + (property.EntityPropertyRelated.IsNullOrEmpty() ? "" : "=" + property.EntityPropertyRelated));
                if (!key.IsNullOrEmpty() && !result.ContainsKey(key))
                    result.Add(key, property.Datatype + "#" + property.Name + "#" + property.IsPrimaryKey.ToString().ToLower() + "#" + this.GetSuffix(property) + "#" + property.Precision + "#" + property.DomainName + "#" + property.DisplayName + "#" + order.ToString() + "#" + property.IsBrowsable.ToString().ToLower() + "#" + property.GetSubstituteProperties() + "#" + property.DependencyProperty + "::" + this.Name + "#" + this.GetSpecializedLookUp() + "#" + this.IsMultiSelection.ToString().ToLower() + "#" + this.ReplaceAllOnClearState.ToString().ToLower() + "#" + this.RelationName + "#" + this.EntitySource + "#" + this.EntityAdapterDesignerRoot.GetDirectContextNamespace() + "#" + this.QueryReturnType.ToString() + "#" + this.GetSubQueryFields() + "#" + this.GetSubQueryClientFilters() + "#" + this.ApplyClientFilterOnClear.ToString().ToLower() + "#" + this.CheckExistence.ToString().ToLower());
                order++;
            }

            return result;
        }

        public bool IsOlap()
        {
            return !this.EntityAdapter.GetCubeName().IsNullOrEmpty() && this.EntityAdapter.GetOlapCatalog() != null;
        }

        public string GenerateOlapQuery(string indent)
        {
            bool firstControl;
            string result = String.Empty;
            if (this.IsOlap())
            {
                Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
                string dimensionsList =
                    string.Join(", ", this.GetAllInheritanceAttributes().Where(e => e.GetType() != typeof(EntityAdapterFormula)).Select(p => "\"" + p.EdmKey + "\""));

                //Get Measures and Dimensions

                builder.AddLine("");
                builder.AddLine("[Ignore()]");
                builder.AddLine("public IEnumerable<" + this.Name + "> GetOlap" + this.Name + "(List<EntitySearch> entitySearchList)");
                builder.AddLine("{");

                //Generate Dictionary
                builder.AddLine("   List<MDXField> fieldsMap = new List<MDXField>();");
                foreach (var prop in this.GetAllInheritanceAttributes().Where(e => e.GetType() != typeof(EntityAdapterFormula)))
                {
                    builder.AddLine("   fieldsMap.Add(new MDXField(\"" + prop.Name + "\", \"" + prop.EdmKey + "\", false));");
                }

                builder.AddLine("   string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));");
                builder.AddLine("   validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));");

                builder.AddLine("   MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);");
                builder.AddLine("   builder.Conditions(entitySearchList);");

                builder.AddLine("   string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString(\"" + this.EntityAdapter.GetOlapCatalog().Name + "\");");
                builder.AddLine("   if (connString == \"name=" + this.EntityAdapter.GetOlapCatalog().Name + "\") connString = Linx.Tools.ConnectionManager.GetConnectionString(\"" + this.EntityAdapter.GetOlapCatalog().Name + "\");");
                builder.AddLine("   using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))");
                builder.AddLine("   {");
                builder.AddLine("       string mdxScript = (new MDXHelper(\"" + this.EntityAdapter.GetCubeName() + "\"))");
                builder.AddLine("          .SetIdLinxDimensions(\"" + this.EntityAdapter.GetOlapCatalog().IdLinxDimensions + "\")");
                builder.AddLine("          .SetIdGpeconDimensions(\"" + this.EntityAdapter.GetOlapCatalog().IdGpeconDimensions + "\")");
                builder.AddLine("          .SetIdBandeiraRedeDimensions(\"" + this.EntityAdapter.GetOlapCatalog().IdBandeiraRedeDimensions + "\")");
                builder.AddLine("          .SetIdFilialDimensions(\"" + this.EntityAdapter.GetOlapCatalog().IdFilialDimensions + "\")");
                builder.AddLine("          .Rows(" + dimensionsList + ")");
                builder.AddLine("          .Where(builder).FilterMetaData(validProperties)");
                builder.AddLine("          .SubqueryFilter(\"" + this.Filter + "\")");
                builder.AddLine("          .SetIdGpEcon(CurrentIdGpEcon())");
                builder.AddLine("          .SetIdLinx(CurrentIdLinx(\"{0}\"))", this.EntityAdapter.GetOlapCatalog().Name);
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
                foreach (var prop in this.GetAllInheritanceAttributes().Where(e => e.GetType() != typeof(EntityAdapterFormula)))
                {
                    builder.AddLine("           " + (firstControl ? String.Empty : ", ") + prop.Name + " = !columnInReader.Contains(\"" + prop.EdmKey + ".[MEMBER_CAPTION]\") || (validProperties.Length > 0 && !validProperties.Contains(\"" + prop.EdmKey + "\")) || r[\"" + prop.EdmKey + ".[MEMBER_CAPTION]\"] is DBNull || r[\"" + prop.EdmKey + ".[MEMBER_CAPTION]\"] == null ? default(" + prop.Datatype + ") : " + (prop.Datatype.ToLower().Contains("string") ? String.Empty : prop.Datatype + ".Parse") + "((string)r[\"" + prop.EdmKey + ".[MEMBER_CAPTION]\"])");
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


        public string GetLinqDefinitions(string contextName, string indent)
        {
            string body = String.Empty;
            body += "\r\n" + indent + @"string strValue = String.Empty;";

            body += "\r\n\r\n" + indent + @"switch (propertyName)";
            body += "\r\n" + indent + @"{";

            foreach (LookUpProperty property in this.GetAllInheritanceAttributes().Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
            {
                body += "\r\n" + indent + @"	case """ + property.EntityPropertyRelated + @""":";

                if (property.Datatype.ToLower().InList("string", "system.string"))
                {
                    body += "\r\n" + indent + indent + "strValue = (string)propertyValue;";
                    body += "\r\n" + indent + indent + @"if (strValue == ""%"")";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "query = from c in query select c;";
                    body += "\r\n" + indent + indent + @"}";
                    body += "\r\n" + indent + indent + @"else if (strValue.Right(1) == ""%"" && strValue.Left(1) == ""%"")";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "strValue = strValue.Left(strValue.Length - 1);";
                    body += "\r\n" + indent + indent + indent + "strValue = strValue.Substring(1);";
                    body += "\r\n" + indent + indent + indent + "query = from c in query";
                    body += "\r\n" + indent + indent + indent + "		 where c." + property.Name + @".Contains(strValue) select c;";
                    body += "\r\n" + indent + indent + @"}";
                    body += "\r\n" + indent + indent + @"else if (strValue.Right(1) == ""%"")";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "strValue = strValue.Left(strValue.Length - 1);";
                    body += "\r\n" + indent + indent + indent + "query = from c in query";
                    body += "\r\n" + indent + indent + indent + "		 where c." + property.Name + @".StartsWith(strValue) select c;";
                    body += "\r\n" + indent + indent + @"}";
                    body += "\r\n" + indent + indent + @"else if (strValue.Left(1) == ""%"")";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "strValue = strValue.Substring(1);";
                    body += "\r\n" + indent + indent + indent + "query = from c in query";
                    body += "\r\n" + indent + indent + indent + "		 where c." + property.Name + @".EndsWith(strValue) select c;";
                    body += "\r\n" + indent + indent + @"}";
                    body += "\r\n" + indent + indent + @"else";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "query = from c in query";
                    body += "\r\n" + indent + indent + indent + "		 where c." + property.Name + @" == strValue select c;";
                    body += "\r\n" + indent + indent + @"}";
                }
                else
                {
                    body += "\r\n" + indent + indent + property.Datatype + " lu" + property.Name + " = (" + property.Datatype + ")propertyValue;";
                    body += "\r\n" + indent + indent + " if (lu" + property.Name + ".IsNullOrEmpty())";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "query = from c in query select c;";
                    body += "\r\n" + indent + indent + @"}";
                    body += "\r\n" + indent + indent + @"else";
                    body += "\r\n" + indent + indent + @"{";
                    body += "\r\n" + indent + indent + indent + "query = from c in query";
                    body += "\r\n" + indent + indent + indent + "		 where c." + property.Name + @" == lu" + property.Name + " select c;";
                    body += "\r\n" + indent + indent + @"}";
                }

                body += "\r\n" + indent + @"		break;";
            }

            body += "\r\n" + indent + @"	default:";
            body += "\r\n" + indent + @"		break;";
            body += "\r\n" + indent + @"}";

            return body;
        }


        public string GetEnvPart()
        {
            return this.EntityAdapterDesignerRoot.GetDirectorySourcePart();
        }
    }
}
