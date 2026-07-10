using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Linx.Tools;
using System.Collections.Generic;
using System.Linq;
using Linx.Business.Tools;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;


namespace Linx.Business.Tools
{   
    public sealed class LinxCustomSearchUtils
    {
        public List<TableNode> Tables = new List<TableNode>();
        private DateTime currentDate = DateTime.Now;

        public void EvaluateUserParameter(object parameterValue, List<EntitySearch> evaluatedSearch, Dictionary<string, string> parameters)
        {
            foreach (EntitySearch search in evaluatedSearch)
            {
                string field = string.Empty;
                foreach (EntitySearchExpression item in search.Expressions)
                {
                    switch (item.Name)
                    {
                        case "Field":
                            field = item.Value.ToString();
                            break;

                        case "Value":
                            if (item.Value.ToString().Contains("@"))
                            {
                                Type dataType = GetFieldtype(search.EntityName, field);

                                if (!dataType.IsNull())
                                {
                                    item.Value = ConvertFieldValue(parameterValue.ToString(), dataType);
                                    parameters.Add(item.Value.ToString(), parameterValue.ToString());
                                    return;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public Type GetFieldtype(string entity, string field)
        {
            Type fieldType = null;
            TableNode table = GetTableNode(entity);
            List<NodePropertyInfo> fields = table.ElementProp.NodeProperties.Where(i => i.Name == field).ToList();

            if (fields.Count > 0)
                fieldType = fields.First().PropertyType;

            return fieldType;
        }

        public object ConvertFieldValue(string value, Type type)
        {
            object fieldValue = null;

            if (type.FullName.Contains("DateTime"))
                fieldValue = DateTime.ParseExact(value, "dd/MM/yyyy", null);
            else if (type.FullName.Contains("Byte"))
            {
                fieldValue = byte.Parse(value);
            }
            else
            {
                if (type.FullName.Contains("Nullable"))
                    fieldValue = Convert.ChangeType(value, Type.GetType(GetDataType(type, true)), CultureInfo.InvariantCulture);
                else
                    fieldValue = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }

            return fieldValue;
        }

        public TableNode GetTableNode(string tableName)
        {
            TableNode currentNode = null;
            List<TableNode> nodes = Tables.Where(i => i.FullName == tableName).ToList();
            if (nodes.Count > 0)
                currentNode = nodes.First();

            return currentNode;
        }

        public List<TableNode> LoadEntities(object dataAnalysis, Guid uidObject)
        {
            if (dataAnalysis is List<TableNode>)
                Tables = (List<TableNode>)dataAnalysis;
            else if (dataAnalysis is List<LinxEntityReferenceInfo>)
            {
                foreach (LinxEntityReferenceInfo info in (List<LinxEntityReferenceInfo>)dataAnalysis)
                {

                    ElementNode elementNode = new ElementNode();
                    elementNode.NodeProperties = new List<NodePropertyInfo>();

                    foreach (PropertyDefinitions item in info.Properties)
                    {
                        elementNode.NodeProperties.Add(new NodePropertyInfo(item.FilterDataKey, Type.GetType(item.FullDataType), item.DataType, item.Caption, false));
                    }

                    Tables.Add(new TableNode(info.ClassName, elementNode, info.ClassName, Guid.Empty, info.SubQueryInfo, info.EdmEntityName, info.EdmParentEntityName));
                }
            }
            else
            {
                Type type = (dataAnalysis is Type ? (Type)dataAnalysis : dataAnalysis.GetType());
                string nodeDescription = ObjectExtension.GetFunctionalPointOfType(type, "DisplayName");
                string subQueryInfo = ObjectExtension.GetFunctionalPointOfType(type, "SubQueryInfo");
                string edmEntityName = ObjectExtension.GetFunctionalPointOfType(type, "EdmEntityName");
                string edmParentEntityName = ObjectExtension.GetFunctionalPointOfType(type, "EdmParentEntityName");

                if (nodeDescription.Trim().IsNullOrEmpty())
                    nodeDescription = type.Name;

                Tables.Clear();
                Tables.Add(new TableNode(nodeDescription, GetElementNode(type), type.FullName.Replace(type.Namespace + ".", "").Replace("+", "."), uidObject, subQueryInfo, edmEntityName, edmParentEntityName));
                GetEntityList(Tables[0], uidObject);
            }
            return Tables;
        }

        public List<TableNode> LoadEntities(Dictionary<Guid, object> lstUidObject)
        {
            Tables.Clear();

            foreach (KeyValuePair<Guid, object> item in lstUidObject)
            {
                Type type = (item.Value is Type ? (Type)item.Value : item.Value.GetType());
                string nodeDescription = ObjectExtension.GetFunctionalPointOfType(type, "DisplayName");
                string subQueryInfo = ObjectExtension.GetFunctionalPointOfType(type, "SubQueryInfo");
                string edmEntityName = ObjectExtension.GetFunctionalPointOfType(type, "EdmEntityName");
                string edmParentEntityName = ObjectExtension.GetFunctionalPointOfType(type, "EdmParentEntityName");

                if (nodeDescription.Trim().IsNullOrEmpty())
                    nodeDescription = type.Name;

                TableNode currentNode = new TableNode(nodeDescription, GetElementNode(type), type.FullName.Replace(type.Namespace + ".", "").Replace("+", "."), item.Key, subQueryInfo, edmEntityName, edmParentEntityName);
                Tables.Add(currentNode);
                GetEntityList(currentNode, item.Key);
            }
            return Tables;
        }

        private void GetEntityList(TableNode parent, Guid uidObject)
        {
            System.Reflection.PropertyInfo[] propsInfo = parent.ElementProp.Entity.GetProperties().Where(p => p.PropertyType.Name == "EntityCollection`1").ToArray();
            if (propsInfo.Length > 0)
            {
                foreach (var prop in propsInfo)
                {
                    Type detailType = prop.PropertyType.GetElement();
                    string nodeFullName = detailType.FullName.Replace(detailType.Namespace + ".", "").Replace("+", ".");
                    string nodeDescription = ObjectExtension.GetFunctionalPointOfType(detailType, "DisplayName");
                    string subQueryInfo = ObjectExtension.GetFunctionalPointOfType(detailType, "SubQueryInfo");
                    string edmEntityName = ObjectExtension.GetFunctionalPointOfType(detailType, "EdmEntityName");
                    string edmParentEntityName = ObjectExtension.GetFunctionalPointOfType(detailType, "EdmParentEntityName");
                    if (nodeDescription.Trim().IsNullOrEmpty())
                        nodeDescription = detailType.Name;

                    if (Tables.Where(i => i.FullName == nodeFullName).Count() == 0)
                    {
                        TableNode item = new TableNode(nodeDescription, GetElementNode(detailType), nodeFullName, uidObject, subQueryInfo, edmEntityName, edmParentEntityName);
                        GetEntityList(item, uidObject);
                        Tables.Add(item);
                    }
                }
            }
        }

        private ElementNode GetElementNode(Type type)
        {
            ElementNode elementNode = new ElementNode(type);

            List<NodePropertyInfo> nodeProperties = new List<NodePropertyInfo>();
            nodeProperties = new List<NodePropertyInfo>();
            List<PropertyDefinitions> properties = ObjectExtension.GetFunctionalPoints(type, true);

            foreach (var item in properties)
            {
                Type dataType = Type.GetType(item.FullDataType);
                System.Reflection.MethodInfo method = type.GetMethod("Get" + item.Name + "Values");
                string typeName = GetDataType(dataType);
                List<EnumValidationValues> enumValues = null;
                bool excludedField = false;
                System.Reflection.PropertyInfo member = type.GetProperty(item.Name);
                string fPoint = null;
                if (!member.IsNullOrEmpty())
                    fPoint = ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string;

                if (fPoint != null)
                    excludedField = fPoint.Extract("ExcludedAsFilter[", "]") == "true";

                if (method != null)
                {
                    object dataItem = Activator.CreateInstance(type);
                    Dictionary<string, string> domainValues = method.Invoke(dataItem, new object[] { }) as Dictionary<string, string>;
                    enumValues = EnumValidationValues.LoadValues(domainValues, typeName);
                }
                nodeProperties.Add(new NodePropertyInfo((item.FilterDataKey.IsNullOrEmpty() ? item.Name : item.FilterDataKey), dataType, item.DataType, item.Caption, excludedField, enumValues));
            }

            elementNode.NodeProperties = nodeProperties;

            return elementNode;
        }

        private string GetDataType(Type type, bool fullName = false)
        {
            string dataType = string.Empty;
            if (type.FullName.StartsWith("System.Nullable"))
            {
                dataType = type.FullName.Extract("[[", ",");
                if (!fullName)
                    dataType = dataType.Replace("System.", string.Empty);
            }
            else
                dataType = type.Name;

            return dataType;
        }
    }

    public sealed class TableNode
    {
        public TableNode()
        {
        }
        public TableNode(string nodeDescription, ElementNode element, string fullName, Guid uidObject, string subQueryInfo, string edmEntityName, string edmParentEntityName)
        {

            this.NodeDescription = nodeDescription;
            this.ElementProp = element;
            this.FullName = fullName;
            this.UidObject = uidObject;
            this.SubQueryInfo = subQueryInfo;
            this.EdmEntityName = edmEntityName;
            this.EdmParentEntityName = edmParentEntityName;
        }

        public string NodeDescription { get; set; }
        public string FullName { get; set; }
        public string SubQueryInfo { get; set; }
        public string EdmEntityName { get; set; }
        public string EdmParentEntityName { get; set; }
        public ElementNode ElementProp { get; set; }
        public Guid UidObject { get; set; }
    }

    public sealed class ElementNode
    {
        public ElementNode(Type entity)
        {
            Entity = entity;
        }

        public ElementNode()
        {

        }

        public Type Entity { get; set; }
        public List<NodePropertyInfo> NodeProperties { get; set; }
    }

    public class NodePropertyInfo
    {
        public NodePropertyInfo(string name, Type propertyType, string propertyTypeName, string description, bool excluded, List<EnumValidationValues> validationValues = null)
        {
            if (validationValues == null)
                validationValues = new List<EnumValidationValues>();

            this.Name = name;
            this.PropertyType = propertyType;
            this.PropertyTypeName = propertyTypeName;
            this.Description = description;
            this.ValidationValues = validationValues;
            this.Excluded = excluded;
        }
        public string Name { get; set; }
        public Type PropertyType { get; set; }
        public string PropertyTypeName { get; set; }
        public string Description { get; set; }
        public List<EnumValidationValues> ValidationValues { get; set; }
        public bool Excluded { get; set; }
    }

    public class FilterItem : INotifyPropertyChanged
    {
        public FilterItem()
        {
        }

        public FilterItem(int idFilter, string description, Guid uidObject)
        {
            this.IdFilter = idFilter;
            this.Description = description;
            this.OperatorAnd = true;
            this.OperatorOr = false;
            this.UidObject = uidObject;
        }

        public FilterItem(int idFilter, string description, bool operatorAnd, bool operatorOr, Guid uidObject)
        {
            this.IdFilter = idFilter;
            this.Description = description;
            this.OperatorAnd = operatorAnd;
            this.OperatorOr = operatorOr;
            this.UidObject = uidObject;
        }

        public FilterItem(int idFilter, string description, string xmlFilter, Guid uidObject)
        {
            this.IdFilter = idFilter;
            this.Description = description;
            this.OperatorAnd = true;
            this.OperatorOr = false;
            this.XmlFilter = xmlFilter;
            this.UidObject = uidObject;
        }

        private int _IdFilter;
        private string _Description, _ParameterValue;
        bool _OperatorAnd, _OperatorOr;
        private Guid _UidObject;

        public int IdFilter
        {
            get
            {
                return _IdFilter;
            }
            set
            {
                _IdFilter = value;
                OnPropertyChanged("IdFilter");
            }
        }
        [ReadOnly(true)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        public bool OperatorAnd
        {
            get
            {
                return _OperatorAnd;
            }
            set
            {
                _OperatorAnd = value;
                OnPropertyChanged("OperatorAnd");
            }
        }
        public bool OperatorOr
        {
            get
            {
                return _OperatorOr;
            }
            set
            {
                _OperatorOr = value;
                OnPropertyChanged("OperatorOr");
            }
        }
        public string XmlFilter { get; set; }
        public string ParameterValue
        {
            get { return _ParameterValue; }
            set
            {
                _ParameterValue = value;
                OnPropertyChanged("ParameterValue");
            }
        }
        public Dictionary<string, string> ParameterList { get; set; }
        public Guid UidObject
        {
            get { return _UidObject; }
            set
            {
                _UidObject = value;
                this.OnPropertyChanged("UidObject");
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == "OperatorAnd" && OperatorAnd && OperatorOr)
                OperatorOr = false;
            else if (propertyName == "OperatorOr" && OperatorOr && OperatorAnd)
                OperatorAnd = false;
        }

        #endregion
    }

    public sealed class ParameterInfo
    {
        public ParameterInfo()
        {
        }

        public ParameterInfo(string parameterName, Type parameterType)
        {
            ParameterName = parameterName;
            ParameterType = parameterType;
            Expressions = new List<EntitySearchExpression>();
        }

        public String ParameterName { get; set; }
        public Type ParameterType { get; set; }
        public List<EntitySearchExpression> Expressions { get; set; }
    }
}
