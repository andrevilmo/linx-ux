using Linx.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Data
{

    public interface EntityQuery<T> : IQueryable<T> { }

    [DataContract()]
    [Serializable()]
    public class Entity : INotifyPropertyChanged
    {
        // Summary:
        //     Event raised whenever an System.Windows.Ria.Entity property has changed
        public event PropertyChangedEventHandler PropertyChanged;

        //
        // Summary:
        //     Called when an System.Windows.Ria.Entity property has changed.
        //
        // Parameters:
        //   e:
        //     The event arguments
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (!this.PropertyChanged.IsNull())
                this.PropertyChanged(this, e);
        }

        //
        // Summary:
        //     Called from a property setter to notify the framework that an System.Windows.Ria.Entity
        //     data member has changed. This method performs any required change tracking
        //     and state transitions.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property that has changed
        protected void RaiseDataMemberChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }


        //
        // Summary:
        //     Validate whether the specified value is valid for the specified property
        //     of the current Entity.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property on this entity to set. This name cannot be null
        //     or empty.
        //
        //   value:
        //     The value to test. It may be null if null is valid for the given property
        //
        // Exceptions:
        //   System.ComponentModel.DataAnnotations.ValidationException:
        //     is thrown if this value is not valid for the specified property.
        //
        //   System.InvalidOperationException:
        //     is thrown if this property is marked with System.ComponentModel.DataAnnotations.EditableAttribute
        //     configured to prevent editing.
        //
        // Remarks:
        //     This method evaluates all the System.ComponentModel.DataAnnotations.ValidationAttributes
        //     associated with the specified property and throws a System.ComponentModel.DataAnnotations.ValidationException
        //     for the first one that signals a validation error. It also verifies the property
        //     is not read-only.
        //     All validation logic is bypassed if this entity is currently being deserialized.
        protected void ValidateProperty(string propertyName, object value) { }

        //
        // Summary:
        //     Called from a property setter to notify the framework that an System.Windows.Ria.Entity
        //     data member is about to be changed. This method performs any required change
        //     tracking and state transition operations.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property that is changing
        protected void RaiseDataMemberChanging(string propertyName) { }

        #region Property MetaDataMaps

        private List<EdmEntityMetaData> _MetaDataMaps;

        /// <summary>
        /// Metadata Map between this entity and other edm entity objects.
        /// </summary>
        internal List<EdmEntityMetaData> MetaDataMaps
        {
            get
            {
                return _MetaDataMaps;
            }
        }

        #endregion

        /// <summary>
        /// Method to override by derivated business object.
        /// </summary>
        /// <returns></returns>
        public virtual List<EdmEntityMetaData> CreateMetaDataMaps()
        {
            return new List<EdmEntityMetaData>();
        }

        public void VerifyMetaDataMaps()
        {
            if (_MetaDataMaps.IsNull())
                _MetaDataMaps = this.CreateMetaDataMaps();
        }


        /// <summary>
        /// Refresh all keys. This is and important thing to update identity fields.
        /// </summary>
        public void RefreshKeys()
        {
            string sourceKey;

            if (!this.MetaDataMaps.IsNull())
            {
                foreach (EdmEntityMetaData metaData in this.MetaDataMaps)
                {
                    if (!metaData.IsNull() && !metaData.EdmEntity.IsNull() && !metaData.EdmEntity.EntityKey.IsNull() && !metaData.EdmEntity.EntityKey.EntityKeyValues.IsNull())
                    {
                        foreach (var key in metaData.EdmEntity.EntityKey.EntityKeyValues)
                        {
                            if (!key.IsNull())
                            {
                                sourceKey = metaData.PropertiesMap.Where(e => e.IsKey && e.Target == key.Key).Select(e => e.Source).FirstOrDefault();
                                if (!sourceKey.IsNullOrEmpty())
                                    this.SetPropertyValue(sourceKey, metaData.EdmEntity.GetPropertyValue(key.Key));
                            }
                        }
                    }
                    else if (!metaData.IsNull() && !metaData.DbEntity.IsNull())
                    {
                        bool isBusinessView = (metaData.EdmEntityType != null && metaData.EdmEntityType.GetMethod("RefreshComposedKeys") != null);
                        var properties = (isBusinessView ? metaData.PropertiesMap.ToArray() : metaData.PropertiesMap.Where(e => e.IsKey).ToArray());
                        foreach (var key in properties)
                        {
                            this.SetPropertyValue(key.Source, metaData.DbEntity.GetPropertyValue(key.Target));
                        }
                    }
                }
            }

            this.AdjustParentAssociation();

        }


        private void AdjustParentAssociation()
        {
            object isForeignKey, parentRef;
            string[] thisKeyMembers = null, otherKeyMembers = null;

            foreach (var property in this.GetType().GetProperties())
            {
                isForeignKey = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(property, typeof(AssociationAttribute), "IsForeignKey");
                if (!isForeignKey.IsNull() && ((bool)isForeignKey))
                {
                    thisKeyMembers = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(property, typeof(AssociationAttribute), "ThisKeyMembers") as string[];
                    otherKeyMembers = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(property, typeof(AssociationAttribute), "OtherKeyMembers") as string[];

                    if (!thisKeyMembers.IsNull() && !otherKeyMembers.IsNull())
                    {
                        for (int idx = 0; idx < thisKeyMembers.Length; idx++)
                        {
                            parentRef = this.GetPropertyValue(property.Name);
                            if (!parentRef.IsNull())
                                this.SetPropertyValue(thisKeyMembers[idx], parentRef.GetPropertyValue(otherKeyMembers[idx]));
                        }
                    }
                }
            }

        }

        private IEnumerable<EdmEntityPropertydMap> GetKeysMap(EdmEntityMetaData metaData, string relationPropertyName)
        {
            //Get key members
            if (relationPropertyName.IsNullOrEmpty())
                return metaData.PropertiesMap.Where(e => e.IsKey);
            else
                return metaData.PropertiesMap.Where(e => (e.IsFK || (e.IsKey && e.EdmKey.Occurs(".") == 2)) && e.RelationPropertyName == relationPropertyName);
        }

        #region GetEntityKey

        /// <summary>
        /// Get Entity Key By Meta Data.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public EntityKey GetEntityKey(EdmEntityMetaData metaData)
        {
            return GetEntityKey(metaData, String.Empty);
        }

        /// <summary>
        /// Get Entity Key By Meta Data.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public EntityKey GetEntityKey(EdmEntityMetaData metaData, string relationPropertyName)
        {
            List<EntityKeyMember> keyMembers = new List<EntityKeyMember>();
            object keyValue;
            IEnumerable<EdmEntityPropertydMap> keysMap = this.GetKeysMap(metaData, relationPropertyName);

            if (keysMap.Count() <= 0)
                return null;



            foreach (EdmEntityPropertydMap key in keysMap)
            {
                keyValue = this.GetPropertyValue(key.Source);
                if (!keyValue.IsNull())
                    keyMembers.Add(new EntityKeyMember((key.TargetKeyName.IsNullOrEmpty() || !relationPropertyName.IsNullOrEmpty() ? key.Target : key.TargetKeyName), keyValue));
            }

            //If is not possible create the EntityKey object, abort the operation.
            if (keyMembers.Count == 0)
                return null;

            return new EntityKey((relationPropertyName.IsNullOrEmpty() ? metaData.QualifiedEntitySetName : keysMap.First().QualifiedEntitySetName), keyMembers);
        }

        #endregion
        #region DbContext Methods

        /// <summary>
        /// Apply all changes against the current entity.
        /// </summary>
        /// <param name="context">The DbContext object.</param>
        /// <param name="originalEntity">The original entity.</param>
        /// <param name="operation">The change operation.</param>
        /// <param name="parent">The parent entity.</param>
        public virtual void ApplyChanges(DbContext context, Entity originalEntity, ChangeOperation operation, Entity parent)
        {
            if (operation == ChangeOperation.None)
                return;

            object currentEntity;
            object entityElement;
            EntityKey entityKey;
            List<Entity> parents = new List<Entity>();

            this.VerifyMetaDataMaps();

            if (parent != null)
                parents.Add(parent);

            for (int mIndex = this.MetaDataMaps.Count - 1; mIndex >= 0; mIndex--)
            {
                if (mIndex == 0 && this.MetaDataMaps.Count > 1)
                    parents.Add(this);

                if (this.MetaDataMaps[mIndex].EdmEntityType.IsNull())
                    continue;

                //Check Operation		
                if (operation == ChangeOperation.Insert || operation == ChangeOperation.Update)
                {
                    currentEntity = this.CreateEntityObject(context, originalEntity, this.MetaDataMaps[mIndex], parents, operation);
                    if (currentEntity.IsNull())
                        continue;
                    this.MetaDataMaps[mIndex].DbEntity = currentEntity;
                }
                else if (operation == ChangeOperation.Delete)
                {
                    entityKey = this.GetEntityKey(this.MetaDataMaps[mIndex]);
                    if (!entityKey.IsNull() && context.TryGetObjectByKey(MetaDataMaps[mIndex].EdmEntityType, entityKey, out entityElement))
                        context.DeleteObject(entityElement);
                }
            }
        }


        /// <summary>
        /// Create an EDM entity object based in this entity.  
        /// </summary>
        /// <param name="context">The DbContext object.</param>
        /// <param name="metaData">Map between the current entity and one EDM entity object.</param>
        /// <param name="createEntityKey">Alert that the EntityKey should be created.</param>
        /// <param name="parents"></param>
        /// <returns></returns>
        public object CreateEntityObject(DbContext context, Entity originalEntity, EdmEntityMetaData metaData, List<Entity> parents, ChangeOperation operation)
        {
            object entityObject = null;
            EntityKey entityKey = null;
            object entityElement = null;
            bool replaceBusinessKeys = true;
            string ePropertyName;

            //Verify if entity has keys.
            if (metaData.PropertiesMap.Where(e => e.IsKey).Count() <= 0)
                return null;

            //Verify Update
            if (operation == ChangeOperation.Update)
            {
                entityKey = this.GetEntityKey(metaData);
                if (entityKey == null)
                    return null;
            }
            else if (operation == ChangeOperation.Insert && metaData.CheckExistence)
            {
                entityKey = this.GetEntityKey(metaData);
                if (entityKey != null)
                {
                    if (context.TryGetObjectByKey(metaData.EdmEntityType, entityKey, out entityElement))
                    {
                        replaceBusinessKeys = false;
                        operation = ChangeOperation.Update;
                    }
                }
            }

            //Creating Current Entity Instance
            entityObject = (entityElement == null ? Activator.CreateInstance(metaData.EdmEntityType) : entityElement);
            if (entityObject == null)
                return null;

            //Get original entityElement for updating
            if (operation == ChangeOperation.Update)
            {
                if (entityElement == null)
                {
                    if (originalEntity != null)
                    {
                        entityElement = Activator.CreateInstance(metaData.EdmEntityType);
                        if (entityElement.IsNull())
                            return null;

                        DbContextExtensions.SetEntityKeyValues(entityElement, entityKey);

                        //Set all properties by map
                        foreach (EdmEntityPropertydMap key in metaData.PropertiesMap.Where(e => (replaceBusinessKeys || !e.IsKey) && (!e.IsFK || e.IsKey)))
                        {
                            ePropertyName = (key.TargetKeyName.IsNullOrEmpty() ? key.Target : key.TargetKeyName);
                            if (!(operation == ChangeOperation.Update && (entityElement.GetType().IsIGpecon(ePropertyName) || entityElement.GetType().IsILinx(ePropertyName))))
                            {
                                entityElement.SetPropertyValue(ePropertyName, originalEntity.GetPropertyValue(key.Source));
                            }
                        }
                    }
                    else
                    {
                        //Get from database
                        if (!context.TryGetObjectByKey(metaData.EdmEntityType, entityKey, out entityElement))
                            return null;
                    }

                    if (entityElement != null)
                        entityObject.CopyFrom(entityElement);
                }
            }

            //Set all properties by map            
            foreach (EdmEntityPropertydMap key in metaData.PropertiesMap.Where(e => (replaceBusinessKeys || !e.IsKey) && (!e.IsFK || e.IsKey)))
            {
                ePropertyName = (key.TargetKeyName.IsNullOrEmpty() ? key.Target : key.TargetKeyName);

                if (operation == ChangeOperation.Update && key.NoUpdatable)
                {
                    this.SetPropertyValue(key.Source, entityObject.GetPropertyValue(ePropertyName));
                }
                else if (!(operation == ChangeOperation.Update && (entityElement.GetType().IsIGpecon(ePropertyName) || entityElement.GetType().IsILinx(ePropertyName))))
                {
                    object bvValue = GetBVPropertyValue(entityObject, key, ePropertyName);

                    entityObject.SetPropertyValue(ePropertyName, bvValue);
                }
            }

            //Update all foreign keys
            UpdateForeignKeys(metaData, entityObject, operation);

            //Apply changes
            if (operation == ChangeOperation.Update && entityElement != null)
            {
                if (entityObject != entityElement)
                    context.AttachUpdated(entityObject, entityElement);
                else
                    context.DetectChanges(entityObject);
            }
            else if (operation == ChangeOperation.Insert)
            {
                //Adjust temporary parent relationship
                if (parents != null && parents.Count > 0 && this._MetaDataMaps.Count > 1)
                {
                    List<string> inheritanceFKeys = metaData.PropertiesMap.Where(e => !e.IsFK && e.IsKey && e.EdmKey.Occurs(".") == 2).Select(e => e.RelationPropertyName).Distinct().ToList();
                    foreach (string relationPropertyName in metaData.PropertiesMap.Where(e => e.IsFK || (e.IsKey && e.EdmKey.Occurs(".") == 2)).Select(e => e.RelationPropertyName).Distinct())
                    {
                        var keysMap = this.GetKeysMap(metaData, relationPropertyName);
                        if (keysMap.Count() > 0)
                        {
                            string qualifiedEntitySetName = keysMap.First().QualifiedEntitySetName;
                            foreach (var parent in parents)
                            {
                                foreach (var parentMetaData in parent.MetaDataMaps.Where(e => e != metaData && e.DbEntity != null).ToList())
                                {
                                    if (context.ChangeTracker.Entries().Any(c => c.Entity == parentMetaData.DbEntity && c.State == System.Data.Entity.EntityState.Added))
                                    {
                                        if (parentMetaData.QualifiedEntitySetName == qualifiedEntitySetName)
                                            entityObject.SetPropertyValue(relationPropertyName, parentMetaData.DbEntity);
                                        else if (inheritanceFKeys.Contains(relationPropertyName))
                                        {
                                            string inverseRelationPropertyName = this.GetInverseNavigationName(parentMetaData.DbEntity, relationPropertyName);
                                            if (!inverseRelationPropertyName.IsNullOrEmpty())
                                                entityObject.SetPropertyValue(inverseRelationPropertyName, parentMetaData.DbEntity);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Add entity

                context.AddObject(entityObject);
            }

            return entityObject;
        }

        #endregion


        private object GetBVPropertyValue(object entityObject, EdmEntityPropertydMap key, string bmPropName)
        {
            object bvValue = this.GetPropertyValue(key.Source);
            if (!bvValue.IsNull() && bvValue.GetType().Name.ToLower().Contains("string"))
            {
                var bmProp = entityObject.GetType().GetProperty(bmPropName);
                if (bmProp != null)
                {
                    var columnAttr = bmProp.GetCustomAttribute<ColumnAttribute>();
                    if (columnAttr != null && columnAttr.TypeName == "char")
                    {
                        var maxLen = bmProp.GetCustomAttribute<MaxLengthAttribute>();
                        if (bvValue.ToString().Length < maxLen.Length)
                        {
                            bvValue = bvValue.ToString().PadRight(maxLen.Length, ' ');
                        }
                    }
                }
            }
            return bvValue;
        }


        private void UpdateForeignKeys(EdmEntityMetaData metaData, object entityObject, ChangeOperation operation)
        {
            var members = metaData.EdmEntityType.GetProperties();
            var navigationNames = members.Where(p => p.GetCustomAttribute<ForeignKeyAttribute>() != null && p.GetCustomAttribute<KeyAttribute>() == null).Select(p => p.GetCustomAttribute<ForeignKeyAttribute>().Name).Distinct().OrderBy(f => f).ToArray();
            foreach (var navigationName in navigationNames)
            {
                var navigationFK = members.FirstOrDefault(f => f.Name == navigationName);
                if (!navigationFK.IsNull())
                {
                    var propFKs = members.Where(f => f.GetCustomAttribute<ForeignKeyAttribute>() != null && f.GetCustomAttribute<ColumnAttribute>() != null && f.GetCustomAttribute<ForeignKeyAttribute>().Name == navigationName).OrderBy(f => f.GetCustomAttribute<ColumnAttribute>().Order).ToArray();
                    var propKeys = navigationFK.PropertyType.GetProperties().Where(p => p.GetCustomAttribute<KeyAttribute>() != null && p.GetCustomAttribute<ColumnAttribute>() != null).OrderBy(p => p.GetCustomAttribute<ColumnAttribute>().Order).ToArray();
                    if (propFKs.Length == propKeys.Length)
                    {
                        for (var idx = 0; idx < propFKs.Length; idx++)
                        {
                            var mapProp = metaData.PropertiesMap.FirstOrDefault(e => GetNavigationProperty(e.EdmKey) == navigationName + "." + propKeys[idx].Name);
                            if (!mapProp.IsNull() && !mapProp.Source.IsNullOrEmpty())
                            {
                                if (operation == ChangeOperation.Update && mapProp.NoUpdatable)
                                    this.SetPropertyValue(mapProp.Source, entityObject.GetPropertyValue(propFKs[idx].Name));
                                else
                                    entityObject.SetPropertyValue(propFKs[idx].Name, this.GetPropertyValue(mapProp.Source));
                            }
                        }
                    }
                }
            }
        }

        private string GetNavigationName(object entityObject, string propertyName)
        {
            var member = entityObject.GetType().GetProperty(propertyName);
            if (member != null)
            {
                var fk = member.GetCustomAttribute<ForeignKeyAttribute>();
                if (fk != null)
                    return fk.Name;
            }
            return String.Empty;
        }

        private string GetInverseNavigationName(object entityObject, string navigationName)
        {
            var member = entityObject.GetType().GetProperty(navigationName);
            if (member != null)
            {
                var inv = member.GetCustomAttribute<InversePropertyAttribute>();
                if (inv != null)
                    return inv.Property;
            }
            return String.Empty;
        }

        private string GetNavigationProperty(string edmKey)
        {
            var parts = edmKey.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3 || parts.Length == 4)
                return parts[parts.Length - 2] + "." + parts[parts.Length - 1];
            else
                return "#$%$";
        }

    }

    /// <summary>
    /// Data Key Mapping
    /// </summary>
    /// 
    [DataContract(IsReference = true)]
    [Serializable()]
    public class DataKeyMapping
    {
        public string EntityTypeName;
        public object RealValue;
        public object TempValue;
    }

    /// <summary>
    /// Data Service Context
    /// </summary>
    public interface IDataServiceContext
    {
        List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries);
    }

    /// <summary>
    /// Entity Change Representation
    /// </summary>
    /// 
    [DataContract(IsReference = true)]
    [Serializable()]
    public class EntityChange
    {
        public Entity Entity { get; set; }
        public Entity Original { get; set; }
        public ChangeOperation Operation { get; set; }
        public Entity Representation { get; set; }
        public string Mark { get; set; }
        private Dictionary<string, string> _keysForRefresh;
        public Dictionary<string, string> KeysForRefresh
        {
            get
            {
                if (_keysForRefresh == null)
                    _keysForRefresh = new Dictionary<string, string>();
                return _keysForRefresh;
            }
        }

        /// <summary>
        /// Replace all keys
        /// </summary>
        public void RefreshKeys()
        {
            if (this.Entity != null && this.Representation != null && this.KeysForRefresh.Count > 0)
            {
                foreach (var key in KeysForRefresh)
                {
                    this.Representation.SetPropertyValue(key.Key, this.Entity.GetPropertyValue(key.Value));
                }
            }
        }

    }

    /// <summary>
    ///   EDM Entity's Metadata.
    /// </summary>
    /// 
    [DataContract(IsReference = true)]
    [Serializable()]
    public class EdmEntityMetaData
    {
        private Type edmEntityType;
        public Type EdmEntityType { get { return edmEntityType; } set { edmEntityType = value; } }
        private List<EdmEntityPropertydMap> propertiesMap;
        public List<EdmEntityPropertydMap> PropertiesMap
        {
            get
            {
                if (propertiesMap.IsNull())
                    propertiesMap = new List<EdmEntityPropertydMap>();
                return propertiesMap;
            }
            set { propertiesMap = value; }
        }
        public string QualifiedEntitySetName { get; set; }
        public EntityObject EdmEntity { get; set; }
        public object DbEntity { get; set; }
        public bool CheckExistence { get; set; }
    }

    /// <summary>
    /// Properties map between POCO and EDM Entity.
    /// </summary>
    /// 
    [DataContract(IsReference = true)]
    [Serializable()]
    public struct EdmEntityPropertydMap
    {
        public string EdmKey { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string TargetKeyName { get; set; }
        public bool IsKey { get; set; }
        public bool IsFK { get; set; }
        public string QualifiedEntitySetName { get; set; }
        public string RelationPropertyName { get; set; }
        public bool NoUpdatable { get; set; }
    }

    public static class DbContextExtensions
    {
        public static void GetEntityQueryExpression(this List<EntitySearch> entitySearchList, ref string query, List<ObjectParameter> queryParameters, string edmParentEntityName = "", string alias = "it", int level = 0)
        {
            GetQueryExpression(entitySearchList, ref query, queryParameters, edmParentEntityName, alias, level, new List<EntitySearch>(), new List<string>());
        }

        private static void GetQueryExpression(List<EntitySearch> entitySearchList, ref string query, List<ObjectParameter> queryParameters, string edmParentEntityName, string alias, int level, List<EntitySearch> analyzedList, List<string> closeParenthesesControl = null, int queryGroup = 0)
        {
            string condition = "And", subAlias = alias;
            int cntElement = 0;
            bool hasStartedExpression;

            if (entitySearchList != null && entitySearchList.Count > 0)
            {

                if (entitySearchList.Where(e => e.EntityName == "TestFalseCondition").Count() > 0)
                {
                    query = " 1=0";
                    return;
                }

                List<EntitySearch> itens = entitySearchList.Where(e => !analyzedList.Contains(e) && ((edmParentEntityName.IsNullOrEmpty() && e.EdmParentEntityName.IsNullOrEmpty()) || (e.EdmParentEntityName == edmParentEntityName && e.QueryGroup == queryGroup && e.EdmParentEntityName != e.EdmEntityName))).ToList();

                foreach (var searchElement in itens)
                {
                    //Add recursive control
                    analyzedList.Add(searchElement);
                    //Set connection condition
                    condition = (searchElement.ConnectionCondition == "||" ? "Or" : "And");

                    if (searchElement.Parentheses == "(")
                    {
                        if (!query.IsNullOrEmpty())
                            query += " " + condition;
                        query += " (";
                    }

                    if (searchElement.HasFilters(entitySearchList))
                    {
                        cntElement++;
                        if (!edmParentEntityName.IsNullOrEmpty())
                            subAlias = alias + "_" + level.ToString() + "_" + cntElement.ToString();

                        if (searchElement.Parentheses != "(" && !query.IsNullOrEmpty() && query.Right(" ").Trim().ToLower() != "where" && (searchElement.Expressions.Where(e => !e.Excluded).Count() > 0 || !searchElement.SubQueryInfo.IsNullOrEmpty()))
                            query += " " + condition;

                        if (searchElement.Expressions.Where(e => !e.Excluded).Count() > 0 || !searchElement.SubQueryInfo.IsNullOrEmpty())
                        {
                            hasStartedExpression = true;
                            query += " " + (searchElement.SubQueryInfo.IsNullOrEmpty() ? "" : "Exists") + "(";
                        }
                        else hasStartedExpression = false;

                        if (!searchElement.SubQueryInfo.IsNullOrEmpty())
                            query += searchElement.SubQueryInfo.Replace("#Alias#", subAlias).Replace("#ParentAlias#", alias) + (searchElement.SubQueryInfo.ToLower().Contains(" where ") ? "" : " where") + (searchElement.Expressions.Where(e => !e.Excluded).Count() > 0 && searchElement.SubQueryInfo.ToLower().Contains(" where ") ? " And" : "");

                        if (searchElement.Expressions.Where(e => !e.Excluded).Count() > 0)
                        {
                            Dictionary<string, object> tmpParams = new Dictionary<string, object>();
                            searchElement.GetFullExpression(ref query, tmpParams, subAlias, queryParameters.Count, searchElement.ParamSuffix);
                            if (tmpParams.Count > 0)
                            {
                                foreach (var param in tmpParams)
                                    queryParameters.Add(new ObjectParameter(param.Key, param.Value));
                            }
                        }

                        if (!searchElement.EdmEntityName.IsNullOrEmpty())
                            GetQueryExpression(entitySearchList, ref query, queryParameters, searchElement.EdmEntityName, subAlias, level + 1, analyzedList, closeParenthesesControl, searchElement.QueryGroup);

                        if (hasStartedExpression)
                            query += ") ";
                    }
                    else if (!query.IsNullOrEmpty() && searchElement.Parentheses.IsNullOrEmpty() && !closeParenthesesControl.Contains(searchElement.EntityName))
                    {
                        string parentheses = searchElement.GetInnerCloseParentheses(entitySearchList);
                        if (!parentheses.IsNullOrEmpty())
                        {
                            closeParenthesesControl.Add(searchElement.EntityName);
                            query += ") ";
                        }
                    }

                    if (searchElement.Parentheses == ")")
                        query += ") ";
                }
            }
        }


        /// <summary>
        /// Retrieves a BM Object by EntityKey
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <param name="entityKey"></param>
        /// <param name="objectReference"></param>
        /// <returns></returns>
        public static bool TryGetObjectByKey(this DbContext context, Type type, EntityKey entityKey, out object objectReference)
        {
            var dbSet = context.GetPropertyValue(type.Name);
            if (dbSet == null) dbSet = context.GetPropertyValue(type.BaseType.Name);
            var dbSetType = dbSet.GetType();
            if (dbSetType.Name == "DbSetView`1")
            {
                var method = dbSetType.GetMethod("GetObjectByKey");
                objectReference = (method == null ? null : method.Invoke(dbSet, new object[] { entityKey }));
                return (objectReference != null);
            }
            else
            {
                IEnumerable<object> values =
                    entityKey.EntityKeyValues.Length > 1
                        ? ReorderValuesByColumnOrder(entityKey.EntityKeyValues, type)
                        : entityKey.EntityKeyValues.Select(e => e.Value);

                return TryGetObjectByKey(context, type, values, out objectReference);
            }
        }

        /// <summary>
        /// Remove an object in BM Context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityObject"></param>
        public static void DeleteObject(this DbContext context, object entityObject)
        {
            var dbSet = context.GetPropertyValue(entityObject.GetType().Name);
            if (dbSet == null) dbSet = context.GetPropertyValue(entityObject.GetType().BaseType.Name);
            var dbSetType = dbSet.GetType();
            if (dbSetType.Name == "DbSetView`1")
            {
                var method = dbSetType.GetMethod("Remove");
                method.Invoke(dbSet, new object[] { entityObject });
            }
            else
            {
                var entry = context.Entry(entityObject);
                if (entry == null) throw new NullReferenceException("entityObject");
                entry.State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Add an object in BM Context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityObject"></param>
        public static void AddObject(this DbContext context, object entityObject)
        {
            var dbSet = context.GetPropertyValue(entityObject.GetType().Name);
            if (dbSet == null) dbSet = context.GetPropertyValue(entityObject.GetType().BaseType.Name);
            var dbSetType = dbSet.GetType();
            if (dbSetType.Name == "DbSetView`1")
            {
                var method = dbSetType.GetMethod("Add");
                method.Invoke(dbSet, new object[] { entityObject });
            }
            else
            {

                var entry = context.Entry(entityObject);
                if (entry == null) throw new NullReferenceException("entityObject");
                entry.State = EntityState.Added;
            }
        }

        public static void DetectChanges(this DbContext context, object entityObject)
        {
            var dbSet = context.GetPropertyValue(entityObject.GetType().Name);
            if (dbSet == null) dbSet = context.GetPropertyValue(entityObject.GetType().BaseType.Name);
            var dbSetType = dbSet.GetType();
            if (dbSetType.Name == "DbSetView`1")
            {
                var method = dbSetType.GetMethod("DetectChanges");
                method.Invoke(dbSet, new object[] { entityObject });
            }
            else
            {
                context.ChangeTracker.DetectChanges();
            }
        }


        private static string[] GetPrimaryKeys(object entity)
        {
            var keys = entity.GetType().GetProperties().Where(e => e.GetCustomAttribute<KeyAttribute>() != null).OrderBy(e => e.GetCustomAttribute<ColumnAttribute>().Order).ToArray();
            return keys.Select(e => e.Name).ToArray();
        }

        public static EntityKey GetEntityKey(this DbContext context, object entity)
        {
            List<EntityKeyMember> keyMembers = new List<EntityKeyMember>();
            var keyNames = GetPrimaryKeys(entity);

            for (int idx = 0; idx < keyNames.Length; idx++)
            {
                keyMembers.Add(new EntityKeyMember(keyNames[idx], entity.GetPropertyValue(keyNames[idx])));
            }

            //If is not possible create the EntityKey object, abort the operation.
            if (keyMembers.Count == 0)
                return null;

            return new EntityKey(context.GetType().Name + "." + entity.GetType().Name, keyMembers);
        }

        /// <summary>
        /// AttachUpdated
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        public static object GetOriginal(this DbContext context, object entity)
        {
            object originalEntity = null;
            context.TryGetObjectByKey(entity.GetType(), context.GetEntityKey(entity), out originalEntity);
            return originalEntity;
        }

        /// <summary>
        /// Attach and update an object in BM Context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <param name="originalEntity"></param>
        public static void AttachUpdated(this DbContext context, object entity)
        {
            object originalEntity = context.GetOriginal(entity);

            if (originalEntity != null)
            {
                if (entity != originalEntity)
                    AttachUpdated(context, entity, originalEntity);
                else
                    context.DetectChanges(entity);
            }
        }

        /// <summary>
        /// Attach and update an object in BM Context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <param name="originalEntity"></param>
        public static void AttachUpdated(this DbContext context, object entity, object originalEntity)
        {
            if (entity != null && originalEntity != null)
            {
                var dbSet = context.GetPropertyValue(entity.GetType().Name);
                if (dbSet == null)
                    dbSet = context.GetPropertyValue(entity.GetType().BaseType.Name);
                var dbSetType = dbSet.GetType();
                if (dbSetType.Name == "DbSetView`1")
                {
                    var method = dbSetType.GetMethod("AttachUpdated");
                    method.Invoke(dbSet, new object[] { entity, originalEntity });
                }
                else
                {

                    //Original entity must first be reunited with a Context 
                    if (context.Entry(originalEntity).State == EntityState.Detached)
                        context.Entry(originalEntity).State = EntityState.Unchanged;

                    //Apply entity properties changes to the context 
                    context.Entry(originalEntity).CurrentValues.SetValues(entity);
                }
            }
        }

        /// <summary>
        /// Cancel pending changes
        /// </summary>
        /// <param name="context"></param>
        public static void CancelChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
                entry.State = EntityState.Unchanged;
        }

        /// <summary>
        /// Set entity key values in an object
        /// </summary>
        /// <param name="originalRef"></param>
        /// <param name="newkey"></param>
        internal static void SetEntityKeyValues(object originalRef, EntityKey newkey)
        {
            foreach (var dic in newkey.EntityKeyValues)
            {
                originalRef.SetPropertyValue(dic.Key, dic.Value);
            }
        }

        #region Private Methods
        private static bool AreEqualsEntityKeyValues(object originalRef, EntityKey newkey)
        {
            foreach (var dic in newkey.EntityKeyValues)
                if (originalRef.GetPropertyValue(dic.Key) != dic.Value) return false;

            return true;
        }

        private static IEnumerable<object> ReorderValuesByColumnOrder(EntityKeyMember[] keys, Type type)
        {
            var entityJoin = keys.Join(type.GetProperties(), k => k.Key, p => p.Name, (e1, e2) => new { km = e1, order = GetColumnOrderAttribute(e2) });
            return entityJoin.OrderBy(e => e.order).Select(e => e.km.Value);
        }

        private static int GetColumnOrderAttribute(PropertyInfo propertyInfo)
        {
            var column = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            return column == null ? 0 : column.Order;
        }

        private static bool TryGetObjectByKey(this DbContext context, Type type, IEnumerable<object> values, out object objectReference)
        {
            objectReference = context.Set(type).Find(values.ToArray());
            return objectReference != null;
        }
        #endregion
    }



}
