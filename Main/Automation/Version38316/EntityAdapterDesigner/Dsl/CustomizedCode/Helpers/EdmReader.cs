using System;
using System.Linq;
using System.Reflection;
using Linx.Tools;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Xml.Linq;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public abstract class EdmReader
    {
        #region Constants

        internal const string IsProperty = "IsProperty";
        internal const string IsEntityReference = "IsEntityReference";
        protected const decimal PrecisionDefault = 999999;
        protected const string RelationOne = "1 [One] ", RelationZeroOrOne = "0..1 [ZeroOrOne] ", RelationMany = "* [Many] ";

        #endregion

        #region EdmTypeEnum

        public enum EdmTypeEnum { ObjectContext, DbContext }

        #endregion

        #region Properties

        public string AssemblyPath { get; set; }

        public string ContextName { get; private set; }


        public string TargetNamespace { get; private set; }

        internal Type ContextType { get; private set; }

        public EdmTypeEnum EdmType
        {
            get { return this is ObjectContextReader ? EdmTypeEnum.ObjectContext : EdmTypeEnum.DbContext; }
        }

        protected Assembly Assembly { get; set; }

        public bool IsDbContext
        {
            get { return this.EdmType == EdmTypeEnum.DbContext; }
        }

        public string RelationReferences { get; protected set; }
        public string DetailReferences { get; protected set; }
        private Dictionary<string, string> _details = new Dictionary<string, string>();

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Verify if an entity set is a model view.
        /// </summary>
        /// <param name="entitySetName"></param>
        /// <returns></returns>
        public abstract bool IsModelView(string entitySetName);

        /// <summary>
        /// Gets all context's EntitySets 
        /// </summary>
        /// <returns></returns>
        public abstract Type[] GetEdmsSet();

        /// <summary>
        /// Get the precision from a data property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entityName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        internal abstract decimal GetPrecisionMetadata(IEntityProperty property, string entityName, string field);

        /// <summary>
        /// Gets a multiplicity from a relationship.
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="referenceAlias"></param>
        /// <param name="propFkReference"></param>
        /// <returns></returns>
        protected abstract string GetRelationshipMultiplicity(MemberInfo[] members, Type memberType, string referenceAlias, PropertyInfo propFkReference);

        /// <summary>
        /// Retuns if a property is a data property
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract bool IsPropertyDataMember(PropertyInfo pInfo, IEnumerable<Type> types);
        /// <summary>
        /// Returns if a property is a foreign key
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected abstract bool IsForeignKey(PropertyInfo pInfo, IEnumerable<Type> types);
        /// <summary>
        /// Gets a foreign key alias/name
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract string GetForeignKeyAlias(PropertyInfo propInfo);
        /// <summary>
        /// Get a type that a foreign key has referenced.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="propInfo"></param>
        /// <param name="referenceAlias"></param>
        /// <returns></returns>
        protected abstract Type GetForeignKeyReferenceType(MemberInfo[] members, PropertyInfo pInfo, string referenceAlias);
        /// <summary>
        /// Get literal (properties in text) for a property, example: PK, null
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract string GetLiteralProperties(PropertyInfo pInfo);
        /// <summary>
        /// Returns if a property is a navigation collection .
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract bool IsNavigationCollection(PropertyInfo pInfo);

        #endregion

        #region Virtual Methods

        #endregion

        #region Methods
        /// <summary>
        /// popules a TreeView with the entities relateds with entity select.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectedEntityName"></param>
        /// <param name="expandNodeAction"></param>
        /// <param name="edmTreeMaximumLevel"></param>
        internal void FillTree(TreeView treeView, string selectedEntityName, Action expandNodeAction, int edmTreeMaximumLevel)
        {
            FillTree(treeView: treeView, parentNode: null, selectedEntityName: selectedEntityName, parentTypeName: "",
                parentPath: "", expandNodeAction: expandNodeAction, innerLevel: 0, edmTreeMaximumLevel: edmTreeMaximumLevel);
        }

        private void FillTree(TreeView treeView, TreeNode parentNode, string selectedEntityName, string parentTypeName, string parentPath, Action expandNodeAction, int innerLevel, int edmTreeMaximumLevel)
        {
            if (parentNode == null && selectedEntityName == parentTypeName && treeView.Nodes.Count > 0)
                return;

            if (parentNode == null)
            {
                treeView.Nodes.Clear();
                DetailReferences = "";
                _details.Clear();
                RelationReferences = "";
            }

            if (!selectedEntityName.IsNullOrEmpty())
            {
                TreeNode entityNode, referecesNode, refNode;
                Type memberType, referenceType;
                string typeName = (parentNode == null ? selectedEntityName : parentTypeName), detailRef;
                Type[] types = GetTypes();

                string relation, memberPropType, referenceAlias;
                string pathWithoutTop = "";
                memberType = GetTypeByName(typeName);

                MemberInfo[] members = memberType.GetMembers();
                //Add entity
                entityNode = (parentNode == null ? treeView.Nodes.Add(typeName, typeName, 0, 0) : parentNode.Nodes.Add(parentNode.Name + "(" + typeName + ")", typeName, 0, 0));
                entityNode.Tag = "IsEntity";
                referecesNode = entityNode.Nodes.Add("Entity References", " Entity References", 1, 1);
                referecesNode.Tag = "IsReference";

                //Add References
                var entityProperties = members.Where(m => m.MemberType == MemberTypes.Property).Select(m => (PropertyInfo)m);
                foreach (PropertyInfo propInfo in entityProperties)
                {
                    #region Get references
                    if (IsForeignKey(propInfo, types))
                    {
                        referenceAlias = GetForeignKeyAlias(propInfo);
                        referenceType = GetForeignKeyReferenceType(members, propInfo, referenceAlias);
                        relation = GetRelationshipMultiplicity(members, memberType, referenceAlias, propInfo);

                        if (referenceAlias != "")
                        {
                            if (innerLevel <= edmTreeMaximumLevel && (parentPath.IsNullOrEmpty() || !("." + parentPath + ".").Contains("." + referenceAlias + ".")))
                            {
                                refNode = referecesNode.Nodes.Add(referenceAlias, relation + referenceAlias, 2, 2);
                                refNode.Tag = EdmReader.IsEntityReference;
                                FillTree(treeView, refNode, selectedEntityName, referenceType.Name, parentPath + (parentPath.IsNullOrEmpty() ? typeName : "") + "." + referenceAlias, expandNodeAction, innerLevel + 1, edmTreeMaximumLevel);

                                //Adjust relation references
                                if (parentNode == null)
                                    RelationReferences += (RelationReferences.IsNullOrEmpty() ? "" : "#") + referenceType.Name + "(" + referenceAlias + ")";
                            }
                        }
                    }
                    #endregion
                    #region Details
                    if (IsNavigationCollection(propInfo))
                    {
                        referenceType = propInfo.PropertyType.GenericTypeArguments[0];
                        referenceAlias = propInfo.Name;

                        if (referenceAlias != "")
                        {
                            //Adjust detail references
                            pathWithoutTop = (parentPath.IsNullOrEmpty() ? "" : ("." + parentPath).Right("." + selectedEntityName + "."));
                            detailRef = (pathWithoutTop.IsNullOrEmpty() ? "" : pathWithoutTop + ".") + referenceAlias;
                            if (!_details.ContainsKey(referenceType.Name))
                            {
                                _details[referenceType.Name] = detailRef;
                            }
                            else if (_details[referenceType.Name].Occurs(".") > detailRef.Occurs("."))
                                _details[referenceType.Name] = detailRef;
                        }
                    }
                    #endregion
                }

                #region Add members
                PropertyInfo[] propertiesDataMember =
                    entityProperties.Where(p => IsPropertyDataMember(p, types)).OrderBy(e => e.Name).ToArray();

                foreach (PropertyInfo propInfo in propertiesDataMember)
                {
                    if (propInfo.ToString().IndexOf("System.Nullable`1[") >= 0)
                        memberPropType = propInfo.ToString().Replace("System.Nullable`1[", "System.Nullable<").Replace("]", ">");
                    else
                        memberPropType = propInfo.ToString();

                    //Adjust easy presentation
                    memberPropType = propInfo.Name + " [" + (memberPropType + " ").Left(" " + propInfo.Name + " ") + "] ";

                    //Get Attributes of field
                    string literalProps = GetLiteralProperties(propInfo);
                    if (literalProps != "")
                        literalProps = " (:" + literalProps + ":)";


                    if (!referecesNode.Nodes.ContainsKey(propInfo.Name))
                    {
                        refNode = entityNode.Nodes.Add((parentPath.IsNullOrEmpty() ? typeName : parentPath) + "." + propInfo.Name, memberPropType + literalProps, 3, 3);
                        refNode.Tag = EdmReader.IsProperty;
                    }
                }
                #endregion

                if (parentNode == null)
                {
                    //Adjust Details References
                    foreach (var entr in _details)
                    {
                        DetailReferences += (DetailReferences.IsNullOrEmpty() ? "" : "#") + entr.Key + "(" + entr.Value + ")";
                    }
                }

                if (!expandNodeAction.IsNull() && parentNode.IsNull())
                {
                    expandNodeAction();

                    entityNode.Expand();
                    referecesNode.Expand();
                }
            }
        }

        internal Type[] GetTypes()
        {
            return Assembly.GetTypes();
        }

        internal Type GetTypeByName(string typeName)
        {
            return Assembly.GetType(this.TargetNamespace + "." + typeName);
        }

        internal Type GetType(string typeFullName)
        {
            return Assembly.GetType(typeFullName);
        }

        internal object[] GetCustomAttributes(bool inherit)
        {
            return this.Assembly.GetCustomAttributes(true);
        }

        protected Type[] GetInternalEdmsSet(params string[] entitySetTypeNames)
        {
            return ContextType.GetProperties()
                .Where(p => p.PropertyType.IsGenericType && entitySetTypeNames.Contains(p.PropertyType.Name) &&
                        p.PropertyType.GenericTypeArguments.Length > 0)
                .Select(p => p.PropertyType.GenericTypeArguments[0])
                .ToArray();
        }

        internal string GetDomainName(string entityName, string propertyName)
        {
            string domainName = String.Empty;
            var type = this.Assembly.GetTypes().FirstOrDefault(e => e.Name == entityName);
            if (type != null)
            {
                string fPoint = (Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(type.GetProperty(propertyName), typeof(FunctionalPoint), "FunctionName") as string);
                if (!fPoint.IsNullOrEmpty())
                    domainName = fPoint.Extract("DomainName[", "]");
            }
            return domainName;
        }

        internal decimal GetFieldPrecision(IEntityProperty property)
        {
            if (property.Datatype.ToLower().Contains("char"))
                return 1;

            if (property.Datatype.ToLower().Contains("datetime"))
                return 10;

            if (property.Datatype.ToLower().Contains("guid"))
                return 12;

            if (property.Datatype.ToLower().Contains("bool"))
                return 0;

            if (property.Datatype.ToLower().Contains("byte") || property.Datatype.ToLower().Contains("sbyte"))
                return 3;

            if (property.Datatype.ToLower().Contains("int16") || property.Datatype.ToLower().Contains("uint16"))
                return 6;

            if (property.Datatype.ToLower().Contains("int32") || property.Datatype.ToLower().Contains("uint32"))
                return 12;

            if (property.Datatype.ToLower().Contains("int64") || property.Datatype.ToLower().Contains("uint64"))
                return 24;

            string[] nameParts = property.EdmKey.Split(new char[] { '.' });

            if (nameParts.Length < 2)
                return 0;

            string entityName = nameParts[nameParts.Length - 2], field = nameParts[nameParts.Length - 1];

            //Get entity name
            if (nameParts.Length > 2)
                entityName = property.GetEntityNameByRelation(entityName);


            decimal precision = GetPrecisionMetadata(property, entityName, field);

            return (precision == PrecisionDefault ? 0 : precision);
        }

        public string ReadResourceContent()
        {
            if (this.AssemblyPath == null)
                return String.Empty;

            string body = String.Empty;
            //Read template file
            try
            {
                using (Stream stream = this.Assembly.GetManifestResourceStream(System.IO.Path.GetFileNameWithoutExtension(this.AssemblyPath) + ".ssdl"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        body = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                MessageBox.Show("The resource [" + System.IO.Path.GetFileNameWithoutExtension(this.AssemblyPath) + ".edmx] does not exists into the Entity Framework Assembly.",
                    "EDM alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return body;
        }

        #endregion

        #region Construct Factory

        public static EdmReader GetEdmInfo(string path)
        {
            if (path.IsNullOrEmpty())
                throw new ArgumentNullException("path");

            EdmReader edm = null;

            if (!File.Exists(path))
                throw new FileNotFoundException("File not exists: " + path);

            var assembly = AssemblyHelper.Load(path);



            Type[] types = assembly.GetTypes();

            Type typeContext = types.Where(t => t.BaseType.Name.InList("ObjectContext", "DbContext")).FirstOrDefault();

            if (typeContext == null)
                throw new Exception("Not Found ObjectContext|DbContext in Path='" + path + "'");


            if (typeContext.BaseType.Name == "ObjectContext")
                edm = new ObjectContextReader();
            else
                edm = new DbContextReader();

            edm.ContextType = typeContext;
            edm.Assembly = assembly;
            edm.AssemblyPath = path;
            edm.TargetNamespace = typeContext.Namespace;
            edm.ContextName = typeContext.Name;

            if (edm == null)
                return null;

            return edm;
        }

        #endregion
    }


    internal class ObjectContextReader : EdmReader
    {
        #region Override Methods
        
        public override Type[] GetEdmsSet()
        {
            return this.GetInternalEdmsSet("ObjectSet`1");
        }

        public override bool IsModelView(string entitySetName)
        {
            return false;
        }        

        internal override decimal GetPrecisionMetadata(IEntityProperty property, string entityName, string field)
        {
            string contentFile = this.ReadResourceContent();

            decimal precision = 0;

            //create xmlDoc
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(contentFile);

            //create namespace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns", "http://schemas.microsoft.com/ado/2009/02/edm/ssdl");


            XmlNode fieldNode =
                xmlDoc.SelectSingleNode(
                    string.Format("ns:Schema/ns:EntityType[@Name='{0}']/ns:Property[@Name='{1}']", entityName, field),
                    nsmgr);



            if (fieldNode.Attributes["MaxLength"] != null)
            {
                if (!decimal.TryParse(fieldNode.Attributes["MaxLength"].Value, out precision))
                    precision = PrecisionDefault;

                return precision;
            }

            if (fieldNode.Attributes["Precision"] != null)
            {
                try
                {
                    precision = decimal.Parse(fieldNode.Attributes["Precision"].Value);
                    if (fieldNode.Attributes["Scale"] != null)
                    {
                        precision = decimal.Parse(((int)precision).ToString() +
                            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator +
                            fieldNode.Attributes["Scale"].Value);
                    }
                }
                catch
                {
                    precision = PrecisionDefault;
                }

                return precision;
            }
            return precision;
        }

        protected override string GetRelationshipMultiplicity(MemberInfo[] members, Type memberType, string referenceAlias, PropertyInfo propFkReference)
        {
            var edmRelation = members.Where(item => item is PropertyInfo && item.Name == referenceAlias).First().GetCustomAttributes(true).Where(item => item.ToString() == "System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute").First() as System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute;
            var edmRelation1 = GetCustomAttributes(true).Where(item => item.ToString() == "System.Data.Objects.DataClasses.EdmRelationshipAttribute" && (item as System.Data.Objects.DataClasses.EdmRelationshipAttribute).RelationshipName == edmRelation.RelationshipName).First() as System.Data.Objects.DataClasses.EdmRelationshipAttribute;

            if (edmRelation1.Role1Name == edmRelation.TargetRoleName)
                return edmRelation1.Role1Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.Many ? RelationMany : edmRelation1.Role1Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.One ? EdmReader.RelationOne : EdmReader.RelationZeroOrOne;
            else
                return edmRelation1.Role2Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.Many ? RelationMany : edmRelation1.Role2Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.One ? EdmReader.RelationOne : EdmReader.RelationZeroOrOne;
        }

        protected override bool IsPropertyDataMember(PropertyInfo propInfo, IEnumerable<Type> types)
        {
            return !(propInfo.Name.InList("EntityKey", "EntityState"))
                && !propInfo.PropertyType.Name.InList("EntityCollection`1", "EntityReference`1")
                && !(propInfo.PropertyType.BaseType != null && propInfo.PropertyType.BaseType == typeof(EntityObject));
        }

        protected override bool IsForeignKey(PropertyInfo pInfo, IEnumerable<Type> types)
        {
            return pInfo.PropertyType.Name == "EntityReference`1";
        }

        protected override string GetForeignKeyAlias(PropertyInfo propInfo)
        {
            return propInfo.Name.Replace("Reference", "");
        }

        protected override Type GetForeignKeyReferenceType(MemberInfo[] members, PropertyInfo propInfo, string referenceAlias)
        {
            return propInfo.PropertyType.IsGenericType ? propInfo.PropertyType.GenericTypeArguments[0] : propInfo.PropertyType;
        }

        protected override string GetLiteralProperties(PropertyInfo propInfo)
        {
            string literalProps = "";

            var edmScalarAttr = propInfo.GetCustomAttribute<EdmScalarPropertyAttribute>(true);
            if (edmScalarAttr != null)
            {
                if (edmScalarAttr.EntityKeyProperty)
                    literalProps = "PK";

                if (edmScalarAttr.IsNullable)
                    literalProps = literalProps + (literalProps == "" ? "" : ",") + "Null";
            }
            return literalProps;
        }

        protected override bool IsNavigationCollection(PropertyInfo propInfo)
        {
            return propInfo.PropertyType.Name == "EntityCollection`1";
        }

        #endregion
    }

    internal class DbContextReader : EdmReader
    {
        #region Override Methods

        public override Type[] GetEdmsSet()
        {
            return this.GetInternalEdmsSet("DbSet`1", "ExtendedDbSet`1", "DbSetView`1");
        }

        public override bool IsModelView(string entitySetName)
        {
            var entitySetProperty = ContextType.GetProperty(entitySetName);
            if (entitySetProperty != null)
                return (entitySetProperty.PropertyType.IsGenericType && entitySetProperty.PropertyType.Name == "DbSetView`1" &&
                        entitySetProperty.PropertyType.GenericTypeArguments.Length > 0);
            else
                return false;
        }

        internal override decimal GetPrecisionMetadata(IEntityProperty property, string entityName, string field)
        {
            var type = Assembly.GetTypes().FirstOrDefault(e => e.Name == entityName);
            if (type.IsNull()) throw new NullReferenceException("entityName");
            var propertyInfo = type.GetProperty(field);

            if (propertyInfo == null) throw new NullReferenceException(string.Format("Property [{0}] not found"));


            if (propertyInfo.GetCustomAttribute<MaxLengthAttribute>() != null)
                return propertyInfo.GetCustomAttribute<MaxLengthAttribute>().Length;

            if (propertyInfo.GetCustomAttribute<StringLengthAttribute>() != null)
                return propertyInfo.GetCustomAttribute<StringLengthAttribute>().MaximumLength;

            if (propertyInfo.GetCustomAttribute<PrecisionAttribute>() != null)
            {
                decimal precision = propertyInfo.GetCustomAttribute<PrecisionAttribute>().Value;
                if (propertyInfo.GetCustomAttribute<ScaleAttribute>() != null)
                    precision = decimal.Parse(((int)precision).ToString() + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + propertyInfo.GetCustomAttribute<ScaleAttribute>().Value);

                return precision;
            }

            return PrecisionDefault;


        }

        protected override string GetRelationshipMultiplicity(MemberInfo[] members, Type memberType, string referenceAlias, PropertyInfo propFkReference)
        {
            return
                (
                members.Any(m => m is PropertyInfo && 
                    (((PropertyInfo)m).GetCustomAttribute<RequiredAttribute>() != null || ((PropertyInfo)m).GetCustomAttribute<KeyAttribute>() != null) &&
                    ((PropertyInfo)m).GetCustomAttribute<ForeignKeyAttribute>() != null && ((PropertyInfo)m).GetCustomAttribute<ForeignKeyAttribute>().Name == propFkReference.Name)
                ) ?
                EdmReader.RelationOne :
                EdmReader.RelationZeroOrOne;
        }

        protected override bool IsPropertyDataMember(PropertyInfo pInfo, IEnumerable<Type> types)
        {
            return !IsPrimitiveFK(pInfo) && !IsNavigationCollection(pInfo) && !IsForeignKey(pInfo, types) && !pInfo.IsILinx();
        }

        private bool IsPrimitiveFK(PropertyInfo pInfo)
        {
            return (pInfo.GetCustomAttribute<ForeignKeyAttribute>() != null && pInfo.GetCustomAttribute<KeyAttribute>() == null);
        }
        
        protected override Type GetForeignKeyReferenceType(MemberInfo[] members, PropertyInfo propInfo, string referenceAlias)
        {
            var _propInfo = members.FirstOrDefault(m => m.Name == referenceAlias && m is PropertyInfo) as PropertyInfo;
            return _propInfo.PropertyType;
        }

        protected override string GetLiteralProperties(PropertyInfo pInfo)
        {
            string literalProps = "";

            if (IsPrimaryKey(pInfo))
                literalProps = "PK";
            else
                if (!IsRequired(pInfo))
                    literalProps = literalProps + (literalProps == "" ? "" : ",") + "Null";

            return literalProps;
        }

        protected override bool IsNavigationCollection(PropertyInfo pInfo)
        {
            return pInfo.PropertyType.Name.InList("ICollection`1") &&
                IsVirtualMethod(pInfo);
        }

        private static bool IsVirtualMethod(PropertyInfo pInfo)
        {
            return (pInfo.GetMethod.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual;
        }

        protected override bool IsForeignKey(PropertyInfo pInfo, IEnumerable<Type> types)
        {
            return (IsVirtualMethod(pInfo) && types.Contains(pInfo.PropertyType));
        }
        
        protected override string GetForeignKeyAlias(PropertyInfo pInfo)
        {
            return pInfo.Name;
        }

        #endregion

        #region Private Methods

        private bool IsRequired(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<RequiredAttribute>(true) != null;
        }

        private bool IsPrimaryKey(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<KeyAttribute>(true) != null;
        }

        #endregion
    }
}
