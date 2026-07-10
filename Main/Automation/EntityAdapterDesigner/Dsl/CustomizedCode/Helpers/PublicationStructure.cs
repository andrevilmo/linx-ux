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
        public bool HasError { get; set; }
        private DateTime _lastVersion = new DateTime();
        private string _metadataPath;
        public string BusinessAssemblyPath { get; set; }
        private List<PublicationEntity> _entities = new List<PublicationEntity>();
        public List<PublicationEntity> Entities { get { return _entities; } }

        private List<PublicationDomain> _domains = new List<PublicationDomain>();
        public List<PublicationDomain> Domains { get { return _domains; } }

        private List<PublicationKpi> _kpis = new List<PublicationKpi>();
        public List<PublicationKpi> Kpis { get { return _kpis; } }

        public PublicationStructure() { }

        public PublicationStructure(string metadataPath, string assemblyPath)
        {
            _metadataPath = metadataPath;
            BusinessAssemblyPath = assemblyPath;
            Update();
        }

        public void Update()
        {
            if (System.IO.File.Exists(_metadataPath))
            {
                if (System.IO.File.GetLastWriteTime(_metadataPath) <= _lastVersion)
                    return;
                else
                    _lastVersion = System.IO.File.GetLastWriteTime(_metadataPath);
            }
            else
                return;

            this.Entities.Clear();
            this.Domains.Clear();
            this.Kpis.Clear();


            CallBackPublisherStructures call = new CallBackPublisherStructures(_metadataPath);
            this.HasError = (call.Metadata == null || call.Metadata.Models == null || call.Metadata.KPIs == null || call.Metadata.Domains == null);

            if (!this.HasError)
            {
                PublicationStructure strucuture = call.LoadPublisherStructures();
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

        }

    }


    [Serializable]
    internal class CallBackPublisherStructures
    {

        public DomainServiceMetadata Metadata { get; set; }


        #region Public Implementation

        public CallBackPublisherStructures()
        {

        }
        public CallBackPublisherStructures(string metadataPath)
        {
            if (System.IO.Path.GetExtension(metadataPath).ToLower() == ".json" && System.IO.File.Exists(metadataPath))
            {
                try
                {
                    this.Metadata = SerializationManager<DomainServiceMetadata>.JsonToObject(System.IO.File.ReadAllText(metadataPath));
                }
                catch
                {
                    this.Metadata = null;
                }
            }

        }

        public PublicationStructure LoadPublisherStructures()
        {
            if (this.Metadata == null)
                return null;

            var result = new PublicationStructure();

            object value;
            PublicationEntity entity;
            PublicationDomain domain;
            PublicationKpi kpi;

            foreach (var innerDomain in this.Metadata.Domains)
            {
                domain = new PublicationDomain() { ClassName = innerDomain.Name, NameSpace = innerDomain.NameSpace };
                //Add values
                foreach (var property in innerDomain.Values.OrderBy(e => e.Name))
                {
                    domain.Values.Add(new PublicationDomainProperty() { Name = property.Name, Value = property.Value, DisplayName = property.DisplayName });
                }
                result.Domains.Add(domain);
            }

            foreach (var innerKpi in this.Metadata.KPIs)
            {
                kpi = new PublicationKpi() { ClassName = innerKpi.Name, NameSpace = innerKpi.NameSpace };
                result.Kpis.Add(kpi);
            }

            foreach (var model in this.Metadata.Models)
            {
                foreach (var innerEntity in model.Entities)
                {
                    var pubViewAtributes = innerEntity.GetCustomAttribute("LinxPublicationView");
                    var functionalPoint = innerEntity.GetCustomAttribute("FunctionalPoint");
                    if (!pubViewAtributes.IsNullOrEmpty() && !functionalPoint.IsNullOrEmpty())
                    {                        
                        entity = new PublicationEntity() { Name = innerEntity.Name, IsOlap = functionalPoint.Contains("IsOlap[true]"), IsAggregationView = functionalPoint.Contains("IsAggregationView[true]"), ForceAggregationPaging = functionalPoint.Contains("ForceAggregationPaging[true]"), HasLocalResultEntityAdapters = functionalPoint.Contains("HasLocalResultEntityAdapters[true]"), EntitiesDescription = functionalPoint.Extract("Entities[", "]"), DisplayName = functionalPoint.Extract("DisplayName[", "]"), TemporaryKeyName = functionalPoint.Extract("TemporaryKeyName[", "]"), CompositionHierarchy = functionalPoint.Extract("CompositionHierarchy[", "]"), Namespace = model.Namespace, EdmEntityName = functionalPoint.Extract("EdmEntityName[", "]"), IsIQueryable = functionalPoint.Contains("IsIQueryable[true]") };

                        var pksValue = DomainServiceModel.GetCustomAttributeValue(pubViewAtributes, "PrimaryKeys");
                        if (!pksValue.IsNullOrEmpty())
                            entity.PrimaryKeys.Add(new PublicationNamedClass() { Name = pksValue });

                        var edmValue = DomainServiceModel.GetCustomAttributeValue(pubViewAtributes, "EdmName");
                        if (!edmValue.IsNullOrEmpty())
                            entity.EdmName = edmValue.ToString();
                        else
                            entity.EdmName = "None";

                        var updatableValue = DomainServiceModel.GetCustomAttributeValue(pubViewAtributes, "IsUpdatable");
                        if (!updatableValue.IsNullOrEmpty())
                            entity.IsUpdatable = (updatableValue == "true");


                        var pubAttributes = innerEntity.GetCustomAttributes("LinxPublicationLookUp");
                        if (!pubAttributes.IsNull() && pubAttributes.Length > 0)
                        {
                            foreach (var attr in pubAttributes)
                            {
                                //NameSpace, ClassName, EntityName, AllowsMaintenance
                                var pubLookup = new PublicationLookUp();

                                var propValue = DomainServiceModel.GetCustomAttributeValue(attr, "NameSpace");
                                if (!propValue.IsNullOrEmpty())
                                    pubLookup.NameSpace = propValue;

                                propValue = DomainServiceModel.GetCustomAttributeValue(attr, "ClassName");
                                if (!propValue.IsNullOrEmpty())
                                    pubLookup.ClassName = propValue;

                                propValue = DomainServiceModel.GetCustomAttributeValue(attr, "EntityName");
                                if (!propValue.IsNullOrEmpty())
                                    pubLookup.EntityName = propValue;

                                propValue = DomainServiceModel.GetCustomAttributeValue(attr, "AllowsMaintenance");
                                if (!propValue.IsNullOrEmpty())
                                    pubLookup.AllowsMaintenance = (propValue == "true");

                                entity.LookUps.Add(pubLookup);
                            }
                        }


                        //Get properties
                        string displayName, edmKey, lookUpInfo, dataType, customMediaTable, domainName, kpiName, displayControl, defaultValue, precision, dataFormatString, fPoint, mask, maskType, aggregationFunction, connectedAttribute, description, measureFormula, orderByOrientation, range;
                        bool isSuggestion, isEditableData, isBrowsable, isNull, isAutomaticSequency, isMeasure, isPrimaryKey;
                        int displayOrder, orderBySequence;
                        foreach (var member in innerEntity.Properties.OrderBy(e => e.Name))
                        {
                            var displayAttr = member.GetCustomAttribute("Display");
                            if (displayAttr.IsNull())
                                continue;

                            isBrowsable = DomainServiceModel.GetCustomAttributeValue(displayAttr, "AutoGenerateField") == "true";

                            var pubField = member.GetCustomAttribute("LinxPublicationField");
                            if (pubField.IsNullOrEmpty())
                                continue;

                            isSuggestion = DomainServiceModel.GetCustomAttributeValue(pubField, "IsSuggestion") == "true";
                            edmKey = DomainServiceModel.GetCustomAttributeValue(pubField, "EdmKey");
                            lookUpInfo = DomainServiceModel.GetCustomAttributeValue(pubField, "LookUpInfo");

                            displayName = DomainServiceModel.GetCustomAttributeValue(displayAttr, "Name");
                            value = DomainServiceModel.GetCustomAttributeValue(displayAttr, "Order");
                            if (!value.IsNullOrEmpty())
                                displayOrder = int.Parse(value.ToString());
                            else
                                displayOrder = 0;

                            var rangeAttr = member.GetCustomAttribute("Range");
                            if (rangeAttr != null)
                            {
                                value = DomainServiceModel.GetCustomAttributeValue(displayAttr, "Minimum");
                                range = (!value.IsNullOrEmpty() ? value.ToString() : "");
                                value = DomainServiceModel.GetCustomAttributeValue(displayAttr, "Maximum");
                                range += (!value.IsNullOrEmpty() ? "," + value.ToString() : !range.IsNullOrEmpty() ? "," : "");
                            }
                            else
                                range = "";

                            isPrimaryKey = member.IsPrimaryKey();

                            dataType = member.DataType;

                            //Get functional point

                            value = member.GetCustomAttribute("FunctionalPoint");
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
                            var brandDecimalsControl = member.GetCustomAttribute("BrandDecimals");

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
                                Range = range,
                                BrandDecimalsControl = brandDecimalsControl != null
                            });
                        }


                        result.Entities.Add(entity);

                    }


                }
            }

            //Create composition hierarchy
            foreach (var parent in result.Entities.Where(e => !e.CompositionHierarchy.IsNullOrEmpty()))
            {
                var relations = parent.CompositionHierarchy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (relations.Length > 1)
                {
                    parent.Adddetails(relations, result.Entities.Where(e => e.CompositionHierarchy.IsNullOrEmpty()));
                }
            }


            return result;
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

        public List<string> GetAllLinkDefinitions(ContextMetadata contextMD)
        {
            List<string> result = new List<string>();
            var dataEntityType = contextMD.Entities.FirstOrDefault(e => e.Name == this.Name);

            if (dataEntityType != null)
            {
                //Links to Source
                foreach (var navPropInfo in dataEntityType.Properties.Where(p => p.IsNavigation))
                {
                    var navPropName = navPropInfo.Name;
                    var sourceType = contextMD.Entities.FirstOrDefault(e => e.Name == navPropInfo.DataType);
                    var lstFks = dataEntityType.Properties.Where(p => p.GetCustomAttribute("ForeignKey") == navPropName).ToArray();
                    var lstPks = sourceType.Properties.Where(p => p.IsPrimaryKey()).ToArray();
                    if (lstFks.Length > 0 && lstFks.Length == lstPks.Length)
                    {
                        var isNullable = lstFks[0].IsNullable;
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
                foreach (var navPropInfo in dataEntityType.Properties.Where(p => p.IsCollection))
                {
                    var inverseNavPropName = navPropInfo.GetCustomAttribute("InverseProperty");
                    var targetType = contextMD.Entities.FirstOrDefault(e => e.Name == navPropInfo.DataType.Extract("ICollection<", ">"));
                    var lstFks = targetType.Properties.Where(p => p.GetCustomAttribute("ForeignKey") == inverseNavPropName).ToArray();
                    var lstPks = dataEntityType.Properties.Where(p => p.IsPrimaryKey()).ToArray();
                    if (lstFks.Length > 0 && lstFks.Length == lstPks.Length)
                    {
                        var isNullable = lstFks[0].IsNullable;
                        string reference = "[" + (isNullable ? "0" : "1") + ".." + (navPropInfo.IsCollection ? "*" : "1") + "] " + targetType.Name + " (";
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
        public bool RemoveValidations { get; set; }
        public bool BrandDecimalsControl { get; set; }
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
        public string ValuesEndpointName { get; set; }
        private List<PublicationDomainProperty> values = new List<PublicationDomainProperty>();
        public List<PublicationDomainProperty> Values { get { return values; } }

        public string GetDomainLinqExpression()
        {
            string domainExpression = "";
            for (var idx = 0; idx < this.Values.Count; idx++)
            {
                var dmValue = this.Values[idx];
                if (domainExpression.IsNullOrEmpty())
                    domainExpression = "((#LxExpr#) == [-" + dmValue.Value.Replace("\"", "").Replace("'", "") + "-] ? \"" + dmValue.DisplayName + "\" : #NextValue#)";
                else
                    domainExpression = domainExpression.Replace("#NextValue#", "((#LxExpr#) == [-" + dmValue.Value.Replace("\"", "").Replace("'", "") + "-] ? \"" + dmValue.DisplayName + "\" : #NextValue#)");
            }

            if (!domainExpression.IsNullOrEmpty())
                domainExpression = domainExpression.Replace("#NextValue#", "\"\"");

            return "//<" + this.ClassName + ">" + domainExpression + "</" + this.ClassName + ">";

        }
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
