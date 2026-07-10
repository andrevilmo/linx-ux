using System;
using System.Linq;
using System.Reflection;
using Linx.Tools;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

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

        #region Properties

        public ContextMetadata Metadata { get; set; }

        public string AssemblyPath { get; set; }

        public string ContextName { get; private set; }

        public string TargetNamespace { get; private set; }

        public bool IsDbContext
        {
            get { return this is DbContextReader; }
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
        protected abstract string GetRelationshipMultiplicity(ContextProperty[] members, ContextProperty propFkReference);

        /// <summary>
        /// Retuns if a property is a data property
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract bool IsPropertyDataMember(ContextProperty pInfo, IEnumerable<ContextEntity> types);
        /// <summary>
        /// Returns if a property is a foreign key
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected abstract bool IsForeignKey(ContextProperty pInfo, IEnumerable<ContextEntity> types);
        /// <summary>
        /// Gets a foreign key alias/name
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract string GetForeignKeyAlias(ContextProperty propInfo);
        /// <summary>
        /// Get a type that a foreign key has referenced.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="propInfo"></param>
        /// <param name="referenceAlias"></param>
        /// <returns></returns>
        protected abstract ContextEntity GetForeignKeyReferenceType(ContextProperty[] members, string referenceAlias);
        /// <summary>
        /// Get literal (properties in text) for a property, example: PK, null
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract string GetLiteralProperties(ContextProperty pInfo);
        /// <summary>
        /// Returns if a property is a navigation collection .
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        protected abstract bool IsNavigationCollection(ContextProperty pInfo);


        internal abstract bool IsBrandDecimalsControl(IEntityProperty property);


        #endregion

        #region Virtual Methods

        #endregion

        #region Methods

        private bool IsRequiredFK(ContextEntity entity, string relationPropertyName)
        {
            return entity.Properties.Any(e => e.Decorators.Contains("ForeignKeyAttribute") && e.Decorators.Contains(relationPropertyName) && !e.IsNullable);
        }

        public bool IsRequiredPath(string path)
        {
            if (!path.IsNullOrEmpty() && path.Occurs(".") > 0)
            {
                var partsPath = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var curType = Metadata.Entities.FirstOrDefault(e => e.Name == partsPath[0]);

                if (curType != null)
                {
                    for (var idx = 1; idx < partsPath.Length; idx++)
                    {
                        var relationPropertyName = partsPath[idx];
                        var relationProperty = curType.Properties.FirstOrDefault(e => e.Name == relationPropertyName);
                        if (!relationProperty.IsNullOrEmpty())
                        {
                            if (!IsRequiredFK(curType, relationPropertyName))
                            {
                                return false;
                            }

                            curType = Metadata.Entities.FirstOrDefault(e => e.Name == relationProperty.DataType);
                        }
                    }
                    return true;
                }
            }

            return false;
        }


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
                ContextEntity memberType, referenceType;
                string typeName = (parentNode == null ? selectedEntityName : parentTypeName), detailRef;
                ContextEntity[] types = GetTypes();

                string relation, memberPropType, referenceAlias;
                string pathWithoutTop = "";
                memberType = GetTypeByName(typeName);

                ContextProperty[] entityProperties = memberType.Properties;
                //Add entity
                entityNode = (parentNode == null ? treeView.Nodes.Add(typeName, typeName, 0, 0) : parentNode.Nodes.Add(parentNode.Name + "(" + typeName + ")", typeName, 0, 0));
                entityNode.Tag = "IsEntity";
                referecesNode = entityNode.Nodes.Add("Entity References", " Entity References", 1, 1);
                referecesNode.Tag = "IsReference";

                //Add References
                foreach (var propInfo in entityProperties)
                {
                    #region Get references
                    if (IsForeignKey(propInfo, types))
                    {
                        referenceAlias = GetForeignKeyAlias(propInfo);
                        referenceType = GetForeignKeyReferenceType(entityProperties, referenceAlias);
                        relation = GetRelationshipMultiplicity(entityProperties, propInfo);

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
                        var rTypeName = propInfo.DataType.Extract("ICollection<", ">");
                        referenceType = Metadata.Entities.FirstOrDefault(p => p.Name == rTypeName);
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
                ContextProperty[] propertiesDataMember =
                    entityProperties.Where(p => IsPropertyDataMember(p, types)).OrderBy(e => e.Name).ToArray();

                foreach (var propInfo in propertiesDataMember)
                {
                    memberPropType = propInfo.DataType;

                    //Adjust easy presentation
                    memberPropType = propInfo.Name + " [" + memberPropType + "] ";

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

        internal ContextEntity[] GetTypes()
        {
            return Metadata.Entities;
        }

        protected ContextEntity[] GetInternalEdmsSet()
        {
            return Metadata.Entities;
        }

        internal ContextEntity GetTypeByName(string typeName)
        {
            return Metadata.Entities.FirstOrDefault(e => e.Name == typeName);
        }

        internal ContextEntity GetType(string typeFullName)
        {
            return Metadata.Entities.FirstOrDefault(e => (Metadata.Namespace + "." + e.Name) == typeFullName);
        }

        internal string GetDomainName(string entityName, string propertyName)
        {
            string domainName = String.Empty;
            var type = Metadata.Entities.FirstOrDefault(e => e.Name == entityName);
            if (type != null)
            {
                var property = type.Properties.FirstOrDefault(p => p.Name == propertyName);
                if (property != null)
                {
                    string fPoint = property.Decorators.FirstOrDefault(e => e.Contains("FunctionalPoint"));
                    if (!fPoint.IsNullOrEmpty())
                        domainName = fPoint.Extract("DomainName[", "]");
                }
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
                return 36;

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

        #endregion

        #region Construct Factory

        public static EdmReader GetEdmInfo(string path)
        {
            if (path.IsNullOrEmpty())
                throw new Exception("EMPTY");

            EdmReader edm = null;

            if (!File.Exists(path))
                throw new Exception("DLL");

            string metaPath = path + ".meta.json";

            if (!File.Exists(metaPath))
                throw new Exception("JSON");


            edm = new DbContextReader(metaPath);
            if (edm.Metadata == null || edm.Metadata.Entities == null || edm.Metadata.Domains == null)            
            {
                throw new Exception("STRUCT");
            }

            edm.TargetNamespace = edm.Metadata.Namespace;
            edm.ContextName = edm.Metadata.Name;
            return edm;
        }

        #endregion
    }


    internal class DbContextReader : EdmReader
    {
        public DbContextReader(string metadataPath)
        {
            if (System.IO.Path.GetExtension(metadataPath).ToLower() == ".json" && System.IO.File.Exists(metadataPath))
            {
                try
                {
                    this.Metadata = SerializationManager<ContextMetadata>.JsonToObject(System.IO.File.ReadAllText(metadataPath));
                }
                catch
                {
                    this.Metadata = null;
                }
            }
        }
        
        #region Override Methods

        public override bool IsModelView(string entitySetName)
        {
            var entitySetProperty = Metadata.Entities.FirstOrDefault(e => e.Name == entitySetName);

            if (entitySetProperty != null && entitySetProperty.StructureType.IsNullOrEmpty())
            {
                MessageBox.Show(String.Format("Propriedade StructureType não encontrada.\nPor favor gere novamente o arquivo de Metadata do BM {0}.", Metadata.Namespace), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception(String.Format("Propriedade StructureType não encontrada.\nPor favor gere novamente o arquivo de Metadata do BM {0}.", Metadata.Namespace));
            }

            return (entitySetProperty != null && entitySetProperty.StructureType.ToLower() == "modelview");
        }

        internal override decimal GetPrecisionMetadata(IEntityProperty property, string entityName, string field)
        {
            var type = Metadata.Entities.FirstOrDefault(e => e.Name == entityName);
            if (type.IsNull()) throw new NullReferenceException("entityName");
            var propertyInfo = type.Properties.FirstOrDefault(p => p.Name == field);

            if (propertyInfo == null) throw new NullReferenceException(string.Format("Property [{0}] not found", property));

            var maxLength = propertyInfo.Decorators.FirstOrDefault(e => e.Contains("MaxLength"));
            if (!maxLength.IsNullOrEmpty())
                return int.Parse(maxLength.Extract("MaxLength(", ")"));

            var precisionAttr = propertyInfo.Decorators.FirstOrDefault(e => e.Contains("Precision"));
            if (!precisionAttr.IsNullOrEmpty())
            {
                decimal precision = decimal.Parse(precisionAttr.Extract("Precision(", ")"));
                var scaleAttr = propertyInfo.Decorators.FirstOrDefault(e => e.Contains("Scale"));
                if (!scaleAttr.IsNullOrEmpty())
                {
                    int scale = int.Parse(scaleAttr.Extract("Scale(", ")"));
                    precision = decimal.Parse(((int)precision).ToString() + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + scale.ToString());
                }

                return precision;
            }

            return PrecisionDefault;
        }


        protected override string GetRelationshipMultiplicity(ContextProperty[] members, ContextProperty propFkReference)
        {
            return
                (
                members.Any(m =>
                    (!m.IsNullable || m.IsPrimaryKey()) &&
                    (m.GetCustomAttribute("ForeignKey") == propFkReference.Name))
                ) ?
                EdmReader.RelationOne :
                EdmReader.RelationZeroOrOne;
        }


        protected override bool IsPropertyDataMember(ContextProperty pInfo, IEnumerable<ContextEntity> types)
        {
            return !IsPrimitiveFK(pInfo) && !IsNavigationCollection(pInfo) && !IsForeignKey(pInfo, types);
        }

        private bool IsPrimitiveFK(ContextProperty pInfo)
        {
            return (pInfo.IsForeignKey() && !pInfo.IsPrimaryKey());
        }

        protected override ContextEntity GetForeignKeyReferenceType(ContextProperty[] members, string referenceAlias)
        {
            var _propInfo = members.FirstOrDefault(m => m.Name == referenceAlias);
            return Metadata.Entities.FirstOrDefault(e => e.Name == _propInfo.DataType);
        }

        protected override string GetLiteralProperties(ContextProperty pInfo)
        {
            string literalProps = "";

            if (pInfo.IsPrimaryKey())
                literalProps = "PK";
            else
            {
                if (!pInfo.IsRequired())
                    literalProps = literalProps + (literalProps == "" ? "" : ",") + "Null";
            }

            return literalProps;
        }

        protected override bool IsNavigationCollection(ContextProperty pInfo)
        {
            return pInfo.DataType.Contains("ICollection<");
        }

        protected override bool IsForeignKey(ContextProperty pInfo, IEnumerable<ContextEntity> types)
        {
            return (types.Any(e => e.Name == pInfo.DataType));
        }

        protected override string GetForeignKeyAlias(ContextProperty pInfo)
        {
            return pInfo.Name;
        }

        internal override bool IsBrandDecimalsControl(IEntityProperty property)
        {
            string[] nameParts = property.EdmKey.Split(new char[] { '.' });
            if (nameParts.Length < 2)
                return false;
            string entityName = nameParts[nameParts.Length - 2], field = nameParts[nameParts.Length - 1];
            //Get entity name
            if (nameParts.Length > 2)
                entityName = property.GetEntityNameByRelation(entityName);
            var type = Metadata.Entities.FirstOrDefault(e => e.Name == entityName);
            if (type.IsNull()) throw new NullReferenceException("entityName");
            var propertyInfo = type.Properties.FirstOrDefault(p => p.Name == field);

            bool isBrandDecimalsControl = false;
            if (!propertyInfo.IsNull())
            {
                isBrandDecimalsControl = propertyInfo.Decorators.Any(d => d.Contains("BrandDecimals"));
            }
            return isBrandDecimalsControl;
        }
        #endregion

    }
}
