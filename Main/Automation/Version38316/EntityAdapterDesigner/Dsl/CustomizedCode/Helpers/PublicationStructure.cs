using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Linx.Tools;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;
using System.ComponentModel.DataAnnotations.Schema;


namespace Linx.EntityAdapterDesigner.CustomizedCode
{

    [Serializable]
    public class PublicationStructure
    {
        private DateTime _lastVersion = new DateTime();
        private string _assemblyPath;
        public string BusinessObjectPath { get { return _assemblyPath; } }
        private List<PublicationEntity> _entities = new List<PublicationEntity>();
        public List<PublicationEntity> Entities { get { return _entities; } }

        private List<PublicationDomain> _domains = new List<PublicationDomain>();
        public List<PublicationDomain> Domains { get { return _domains; } }

        private List<PublicationKpi> _kpis = new List<PublicationKpi>();
        public List<PublicationKpi> Kpis { get { return _kpis; } }

        public PublicationStructure() { }

        public PublicationStructure(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
            Update();
        }

        public void Update()
        {
            if (System.IO.File.Exists(_assemblyPath))
            {
                if (System.IO.File.GetLastWriteTime(_assemblyPath) <= _lastVersion)
                    return;
                else
                    _lastVersion = System.IO.File.GetLastWriteTime(_assemblyPath);
            }
            else
                return;

            this.Entities.Clear();
            this.Domains.Clear();
            this.Kpis.Clear();
            AppDomain appDomain = AppDomain.CreateDomain("DomainTmp");
            try
            {
                CallBackPublisherStructures call = new CallBackPublisherStructures(appDomain, _assemblyPath);
                appDomain.DoCallBack(new CrossAppDomainDelegate(call.LoadPublisherStructures));
                PublicationStructure strucuture = appDomain.GetData("PublicationStructure") as PublicationStructure;
                if (!strucuture.IsNull())
                {
                    if (strucuture.Entities.Count > 0)
                        this.Entities.AddRange(strucuture.Entities);
                    if (strucuture.Domains.Count > 0)
                        this.Domains.AddRange(strucuture.Domains);
                    if (strucuture.Kpis.Count > 0)
                        this.Kpis.AddRange(strucuture.Kpis);
                }
            }
            catch
            {
                MessageBox.Show("Unable to load [" + _assemblyPath + "] when checking public structures. Verify the dependences of this assembly.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            AppDomain.Unload(appDomain);
            appDomain = null;
        }

    }


    [Serializable]
    internal class CallBackPublisherStructures
    {
        #region Members Variables
        private string assemblyFile;
        private AppDomain domain;
        private PublicationStructure pubStructure;
        #endregion

        #region Public Implementation

        public CallBackPublisherStructures()
        {

        }
        public CallBackPublisherStructures(AppDomain domain, string assemblyFile)
        {
            this.assemblyFile = assemblyFile;
            this.domain = domain;
            this.pubStructure = new PublicationStructure();
        }

        public void LoadPublisherStructures()
        {
            Assembly assembly;

            assembly = AssemblyHelper.LoadWithDependencies(this.assemblyFile);

            var types = assembly.GetTypes().Where(e => !e.IsNull() && e.IsClass &&
                 ((!e.Namespace.IsNullOrEmpty() && e.Namespace.Right(".") == "Domains") || (!e.Namespace.IsNullOrEmpty() && e.Namespace.Right(".") == "KPIs") || (e.GetCustomAttributes(typeof(LinxPublicationViewAttribute), false).Count() > 0)))
                 .OrderBy(e => e.Name).ToArray();


            if (assembly == null || types == null)
                return;


            object[] attributes;
            object value;
            PublicationEntity entity;
            PublicationDomain domain;
            PublicationKpi kpi;

            if (types.IsNull())
                return;

            foreach (var type in types)
            {
                if (type.Namespace.Right(".") == "Domains")
                {
                    if (type.Name != "DomainHelper")
                    {
                        domain = new PublicationDomain() { ClassName = type.Name, NameSpace = type.Namespace };
                        string fPoint;
                        //Add properties
                        foreach (var property in type.GetProperties().OrderBy(e => e.Name))
                        {
                            fPoint = (Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(property, typeof(FunctionalPoint), "FunctionName") as string);
                            if (!fPoint.IsNullOrEmpty())
                                domain.Values.Add(new PublicationDomainProperty() { Name = property.Name, Value = (!fPoint.IsNullOrEmpty() ? fPoint.Extract("Value[", "]") : property.Name), DisplayName = (!fPoint.IsNullOrEmpty() ? fPoint.Extract("DisplayName[", "]") : property.Name) });
                        }
                        this.pubStructure.Domains.Add(domain);
                    }
                }
                else if (type.Namespace.Right(".") == "KPIs")
                {
                    kpi = new PublicationKpi() { ClassName = type.Name, NameSpace = type.Namespace };
                    this.pubStructure.Kpis.Add(kpi);
                }
                else
                {
                    entity = null;
                    attributes = type.GetCustomAttributes(typeof(LinxPublicationViewAttribute), false);
                    if (!attributes.IsNull() && attributes.Count() > 0)
                    {
                        entity = new PublicationEntity() { Name = type.Name, IsOlap = (ObjectExtension.GetFunctionalPointOfType(type, "IsOlap") == "true"), IsAggregationView = (ObjectExtension.GetFunctionalPointOfType(type, "IsAggregationView") == "true"), ForceAggregationPaging = (ObjectExtension.GetFunctionalPointOfType(type, "ForceAggregationPaging") == "true"), HasLocalResultEntityAdapters = (ObjectExtension.GetFunctionalPointOfType(type, "HasLocalResultEntityAdapters") == "true"), EntitiesDescription = ObjectExtension.GetFunctionalPointOfType(type, "Entities"), DisplayName = ObjectExtension.GetFunctionalPointOfType(type, "DisplayName"), TemporaryKeyName = ObjectExtension.GetFunctionalPointOfType(type, "TemporaryKeyName"), CompositionHierarchy = ObjectExtension.GetFunctionalPointOfType(type, "CompositionHierarchy"), SizeGridConfigurations = ObjectExtension.GetFunctionalPointOfType(type, "SizeGridConfigurations"), Namespace = type.Namespace, EdmEntityName = ObjectExtension.GetFunctionalPointOfType(type, "EdmEntityName"), IsIQueryable = (ObjectExtension.GetFunctionalPointOfType(type, "IsIQueryable") != "false") };
                        foreach (var attribute in attributes)
                        {
                            value = attribute.GetPropertyValue("PrimaryKeys");
                            if (value is string && !value.IsNullOrEmpty())
                                entity.PrimaryKeys.Add(new PublicationNamedClass() { Name = value.ToString() });

                            value = attribute.GetPropertyValue("EdmName");
                            if (value is string && !value.IsNullOrEmpty())
                                entity.EdmName = value.ToString();
                            else
                                entity.EdmName = "None";

                            value = attribute.GetPropertyValue("IsUpdatable");
                            if (value is bool)
                                entity.IsUpdatable = (bool)value;

                        }


                        if (!entity.IsNull())
                        {

                            attributes = type.GetCustomAttributes(typeof(LinxPublicationLookUpAttribute), false);
                            if (!attributes.IsNull() && attributes.Count() > 0)
                            {
                                foreach (LinxPublicationLookUpAttribute attribute in attributes)
                                {
                                    entity.LookUps.Add(new PublicationLookUp() { NameSpace = attribute.NameSpace, ClassName = attribute.ClassName, EntityName = attribute.EntityName, AllowsMaintenance = attribute.AllowsMaintenance });
                                }
                            }


                            //Get properties
                            string displayName, edmKey, lookUpInfo, dataType, customMediaTable, domainName, kpiName, displayControl, defaultValue, precision, dataFormatString, fPoint, mask, maskType, aggregationFunction, connectedAttribute, description, measureFormula, orderByOrientation, range;
                            bool isSuggestion, isEditableData, isBrowsable, isNull, isAutomaticSequency, isMeasure, isPrimaryKey;
                            int displayOrder, orderBySequence;
                            foreach (var member in type.GetProperties().OrderBy(e => e.Name))
                            {
                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "AutoGenerateField");
                                if (value.IsNull())
                                    continue;

                                isBrowsable = (bool)value;

                                attributes = member.GetCustomAttributes(typeof(LinxPublicationFieldAttribute), false);
                                if (attributes.IsNull() || attributes.Count() == 0)
                                    continue;

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(LinxPublicationFieldAttribute), "IsSuggestion");
                                isSuggestion = (value.IsNull() ? false : (bool)value);

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(LinxPublicationFieldAttribute), "EdmKey");
                                edmKey = (!value.IsNullOrEmpty() ? (string)value : String.Empty);

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(LinxPublicationFieldAttribute), "LookUpInfo");
                                lookUpInfo = (!value.IsNullOrEmpty() ? (string)value : String.Empty);

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "Name");
                                displayName = (!value.IsNullOrEmpty() ? (string)value : String.Empty);

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "Order");
                                displayOrder = (!value.IsNullOrEmpty() ? (int)value : 0);

                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(RangeAttribute), "Minimum");
                                range = (!value.IsNullOrEmpty() ? value.ToString() : "");
                                value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(RangeAttribute), "Maximum");
                                range += (!value.IsNullOrEmpty() ? "," + value.ToString() : !range.IsNullOrEmpty() ? "," : "");

                                isPrimaryKey = Linx.Tools.ObjectExtension.ExistsAttributeOnProperty(type, member.Name, typeof(KeyAttribute));

                                if (member.PropertyType.Name == "Nullable`1")
                                    dataType = "System.Nullable<" + member.PropertyType.FullName.Extract("System.Nullable`1[[System.", ",") + ">";
                                else
                                    dataType = member.PropertyType.Name;

                                //Get functional point
                                value = (Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string);
                                dataFormatString = customMediaTable = precision = displayControl = domainName = kpiName = defaultValue = mask = maskType = aggregationFunction = connectedAttribute = description = measureFormula = orderByOrientation = String.Empty;
                                isNull = isEditableData = isAutomaticSequency = isMeasure = false;
                                orderBySequence = -1;
                                if (!value.IsNullOrEmpty())
                                {
                                    fPoint = (string)value;
                                    defaultValue = fPoint.Extract("DefaultValue[", "]");
                                    customMediaTable = fPoint.Extract("CustomMediaTable[", "]");
                                    domainName = fPoint.Extract("DomainName[", "]");
                                    kpiName = fPoint.Extract("KpiName[", "]");
                                    displayControl = fPoint.Extract("ObjectClass[", "]");
                                    precision = fPoint.Extract("Precision[", "]");
                                    dataFormatString = fPoint.Extract("DataFormatString[", "]");
                                    if (!fPoint.Extract("IsEditable[", "]").IsNullOrEmpty())
                                        isEditableData = bool.Parse(fPoint.Extract("IsEditable[", "]"));
                                    if (!fPoint.Extract("IsNull[", "]").IsNullOrEmpty())
                                        isNull = bool.Parse(fPoint.Extract("IsNull[", "]"));
                                    isAutomaticSequency = fPoint.Extract("IsAutomaticSequency[", "]") == "true";
                                    mask = fPoint.Extract("Mask[", "]");
                                    maskType = fPoint.Extract("MaskType[", "]");
                                    isMeasure = fPoint.Extract("IsMeasure[", "]") == "true";
                                    aggregationFunction = fPoint.Extract("AggregationFunction[", "]");
                                    connectedAttribute = fPoint.Extract("ConnectedAttribute[", "]");
                                    description = fPoint.Extract("Description[", "]");
                                    measureFormula = fPoint.Extract("MeasureFormula[", "]");
                                    orderByOrientation = (fPoint.Extract("OrderByOrientation[", "]").IsNullOrEmpty() ? "Ascending" : fPoint.Extract("OrderByOrientation[", "]"));
                                    orderBySequence = (fPoint.Extract("OrderBySequence[", "]").IsNullOrEmpty() ? -1 : int.Parse(fPoint.Extract("OrderBySequence[", "]")));
                                }


                                entity.Properties.Add(new PublicationProperty()
                                {
                                    DisplayName = displayName,
                                    EdmKey = edmKey,
                                    IsSuggestion = isSuggestion,
                                    Name = member.Name,
                                    DataType = dataType,
                                    DefaultValue = defaultValue,
                                    DataFormatString = dataFormatString,
                                    DisplayControl = displayControl,
                                    DomainName = domainName,
                                    KpiName = kpiName,
                                    IsBrowsable = isBrowsable,
                                    IsEditable = isEditableData,
                                    IsNull = isNull,
                                    Precision = precision,
                                    IsAutomaticSequency = isAutomaticSequency,
                                    LookUpInfo = lookUpInfo,
                                    Mask = mask,
                                    MaskType = maskType,
                                    DisplayOrder = displayOrder,
                                    AggregationFunction = aggregationFunction,
                                    ConnectedAttribute = connectedAttribute,
                                    Description = description,
                                    IsMeasure = isMeasure,
                                    MeasureFormula = measureFormula,
                                    OrderByOrientation = orderByOrientation,
                                    OrderBySequence = orderBySequence,
                                    IsPrimaryKey = isPrimaryKey,
                                    CustomMediaTable = customMediaTable,
                                    Range = range
                                });
                            }


                            this.pubStructure.Entities.Add(entity);
                        }
                    }
                }
            }

            //Create composition hierarchy
            foreach (var parent in this.pubStructure.Entities.Where(e => !e.CompositionHierarchy.IsNullOrEmpty()))
            {
                var relations = parent.CompositionHierarchy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (relations.Length > 1)
                {
                    parent.Adddetails(relations, this.pubStructure.Entities.Where(e => e.CompositionHierarchy.IsNullOrEmpty()));
                }
            }

            this.domain.SetData("PublicationStructure", this.pubStructure);
        }

        #endregion
    }




    [Serializable]
    public class PublicationEntity
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string EdmName { get; set; }
        public string EdmEntityName { get; set; }
        public bool IsIQueryable { get; set; }
        public bool IsUpdatable { get; set; }
        public string CompositionHierarchy { get; set; }
        public string SizeGridConfigurations { get; set; }
        public string DisplayName { get; set; }
        public string EntitiesDescription { get; set; }
        public bool IsAggregationView { get; set; }
        public bool HasLocalResultEntityAdapters { get; set; }
        public string TemporaryKeyName { get; set; }
        public bool ForceAggregationPaging { get; set; }
        public bool IsOlap { get; set; }

        private List<PublicationNamedClass> primaryKeys = new List<PublicationNamedClass>();
        public List<PublicationNamedClass> PrimaryKeys { get { return primaryKeys; } }

        private List<PublicationProperty> properties = new List<PublicationProperty>();
        public List<PublicationProperty> Properties { get { return properties; } }

        private List<PublicationLookUp> lookUps = new List<PublicationLookUp>();
        public List<PublicationLookUp> LookUps { get { return lookUps; } }

        private List<PublicationEntity> _details = new List<PublicationEntity>();
        public List<PublicationEntity> Details { get { return _details; } }
        public PublicationEntity Parent { get; set; }

        public string[] GetMetadataKeys()
        {
            List<string> metaDataKeys = new List<string>();
            var keyProp = this.Properties.LastOrDefault(e => e.IsPrimaryKey);
            if (keyProp != null)
                metaDataKeys.Add(this.Name + ":" + keyProp.Name + ":" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType(keyProp.DataType, keyProp.DomainName));
            else
                metaDataKeys.Add(this.Name + ":EntityUniqueKey:" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("Guid", ""));

            foreach (var entity in this.Details)
            {
                metaDataKeys.AddRange(entity.GetMetadataKeys());
            }

            return metaDataKeys.ToArray();
        }
        
        public List<string> GetAllLinkDefinitions(Assembly assembly)
        {
            List<string> result = new List<string>();
            var dataEntityType = assembly.GetType(this.Namespace + "." + this.Name);

            if (dataEntityType != null)
            {                
                //Links to Source
                foreach (var navPropInfo in dataEntityType.GetProperties().Where(p => p.GetMethod.IsVirtual && p.GetCustomAttribute<InversePropertyAttribute>() == null))
                {
                    var navPropName = navPropInfo.Name;
                    var sourceType = navPropInfo.PropertyType;
                    var lstFks = dataEntityType.GetProperties().Where(p => !p.GetMethod.IsVirtual && p.GetCustomAttribute<ForeignKeyAttribute>() != null && p.GetCustomAttribute<ForeignKeyAttribute>().Name == navPropName).ToArray();
                    var lstPks = sourceType.GetProperties().Where(p => !p.GetMethod.IsVirtual && p.GetCustomAttribute<KeyAttribute>() != null).ToArray();
                    if (lstFks.Length > 0 && lstFks.Length == lstPks.Length)
                    {
                        var isNullable = lstFks[0].PropertyType.FullName.Contains("Nullable`1");
                        string reference = "[" + (isNullable ? "0..1" : "1..1") + "] " + sourceType.Name + " (";
                        string joinRelation = String.Empty;
                        for (int idx = 0; idx < lstFks.Length; idx++)
                        {
                            var fkProp = lstFks[idx];
                            reference += fkProp.Name + (idx < lstFks.Length - 1 ? ", " : "");
                            joinRelation += fkProp.Name + "=" + lstPks[idx].Name + (idx < lstFks.Length - 1 ? "," : "");
                        }
                        reference += ")#" + joinRelation;
                        result.Add(reference);
                    }
                }

                //Links to Target
                foreach (var navPropInfo in dataEntityType.GetProperties().Where(p => p.GetMethod.IsVirtual && p.GetCustomAttribute<InversePropertyAttribute>() != null))
                {
                    var inverseNavPropName = navPropInfo.GetCustomAttribute<InversePropertyAttribute>().Property;
                    var targetType = (navPropInfo.PropertyType.FullName.Contains("ICollection") ? navPropInfo.PropertyType.GetElement() : navPropInfo.PropertyType);
                    var lstFks = targetType.GetProperties().Where(p => !p.GetMethod.IsVirtual && p.GetCustomAttribute<ForeignKeyAttribute>() != null && p.GetCustomAttribute<ForeignKeyAttribute>().Name == inverseNavPropName).ToArray();
                    var lstPks = dataEntityType.GetProperties().Where(p => !p.GetMethod.IsVirtual && p.GetCustomAttribute<KeyAttribute>() != null).ToArray();
                    if (lstFks.Length > 0 && lstFks.Length == lstPks.Length)
                    {
                        var isNullable = lstFks[0].PropertyType.FullName.Contains("Nullable`1");
                        string reference = "[" + (isNullable ? "0" : "1") + ".." + (navPropInfo.PropertyType.FullName.Contains("ICollection") ? "*" : "1") + "] " + targetType.Name + " (";
                        string joinRelation = String.Empty;
                        for (int idx = 0; idx < lstFks.Length; idx++)
                        {
                            var fkProp = lstFks[idx];
                            reference += lstPks[idx].Name + (idx < lstFks.Length - 1 ? ", " : "");
                            joinRelation += lstPks[idx].Name + "=" + fkProp.Name + (idx < lstFks.Length - 1 ? "," : "");
                        }
                        reference += ")#" + joinRelation;
                        result.Add(reference);
                    }
                }
            }

            return result;
        }

        public string GetOrderByCommand()
        {

            string orderField = String.Empty;

            var properties = this.Properties;
            foreach (PublicationProperty propOrder in properties.Where(e => e.OrderBySequence >= 0).OrderBy(o => o.OrderBySequence))
            {
                orderField += (orderField.IsNullOrEmpty() ? String.Empty : ", ") + propOrder.Name + " " + propOrder.OrderByOrientation.ToString();
            }

            if (orderField.IsNullOrEmpty())
            {
                foreach (PublicationProperty propOrder in properties.Where(p => p.IsPrimaryKey))
                {
                    orderField += (orderField.IsNullOrEmpty() ? String.Empty : ", ") + propOrder.Name + " " + propOrder.OrderByOrientation.ToString();
                }
            }

            if (orderField.IsNullOrEmpty() && properties.Where(e => e.IsBrowsable).Count() > 0)
                orderField = properties.Where(e => e.IsBrowsable).First().Name + " Ascending";


            return orderField;
        }

        public string[] GetMediaKeys()
        {
            return this.Properties.Where(e => !e.CustomMediaTable.IsNullOrEmpty()).Select(e => e.CustomMediaTable + ":" + e.Name).Union(this.EntitiesDescription.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
        }

        public PublicationEntity GetDetailByName(string name)
        {
            PublicationEntity entity = null;

            Action<PublicationEntity> action = null;
            action = (parent) =>
                {
                    if (entity == null)
                    {
                        foreach (var detail in parent.Details)
                        {
                            if (detail.Name == name)
                                entity = detail;
                            else
                                action(detail);

                            if (entity != null)
                                break;
                        }
                    }
                };

            action(this);

            return entity;
        }

        public void Adddetails(string[] relations, IEnumerable<PublicationEntity> entities)
        {
            foreach (string entityName in relations.Where(e => e.StartsWith(this.Name + ".")).Select(e => e.Right(".")))
            {
                foreach (var detail in entities.Where(e => e.Name == entityName && e.Namespace == this.Namespace))
                {
                    this.Details.Add(detail);
                    detail.Parent = this;
                    detail.Adddetails(relations, entities);
                }
            }
        }
    }

    [Serializable]
    public class PublicationLookUp
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public string EntityName { get; set; }
        public bool AllowsMaintenance { get; set; }
    }


    [Serializable]
    public class PublicationProperty
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsSuggestion { get; set; }
        public string EdmKey { get; set; }
        public string DataType { get; set; }
        public string DisplayControl { get; set; }
        public bool IsNull { get; set; }
        public bool IsBrowsable { get; set; }
        public bool IsEditable { get; set; }
        public bool NoUpdate { get; set; }
        public string DomainName { get; set; }
        public string KpiName { get; set; }
        public string Precision { get; set; }
        public string DataFormatString { get; set; }
        public string DefaultValue { get; set; }
        public bool IsAutomaticSequency { get; set; }
        public string LookUpInfo { get; set; }
        public string Mask { get; set; }
        public string MaskType { get; set; }
        public int DisplayOrder { get; set; }
        public string AggregationFunction { get; set; }
        public string ConnectedAttribute { get; set; }
        public string Description { get; set; }
        public bool IsMeasure { get; set; }
        public string MeasureFormula { get; set; }
        public string OrderByOrientation { get; set; }
        public int OrderBySequence { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string CustomMediaTable { get; set; }
        public string Range { get; set; }
        public string ModelViewFormula { get; set; }
        public string ModelViewSource { get; set; }

        public bool IsNullable()
        {
            return (this.IsNull || this.DataType.Contains("Nullable<") || this.DataType.Contains("?"));
        }
    }

    [Serializable]
    public class PublicationKpi
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public KpiShowType ShowType { get; set; }
        private List<KpiRangeItem> _kpiRangeItems;
        public List<KpiRangeItem> KpiRangeItems
        {
            get
            {
                if (_kpiRangeItems == null)
                    _kpiRangeItems = new List<KpiRangeItem>();
                return _kpiRangeItems;
            }
        }
    }

    [Serializable]
    public class PublicationDomain
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        private List<PublicationDomainProperty> values = new List<PublicationDomainProperty>();
        public List<PublicationDomainProperty> Values { get { return values; } }
    }

    [Serializable]
    public class PublicationDomainProperty
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class PublicationNamedClass
    {
        public string Name { get; set; }
    }

    public class RepresentationStructure
    {
        public bool IsPublisherUpdatable { get; set; }
        public bool IsReadOnly { get; set; }
        public string Name { get; set; }
        public string TargetEntityAdapterName { get; set; }
        public string TargetNameSpace { get; set; }
        public string TargetEdmName { get; set; }        
    }


}
