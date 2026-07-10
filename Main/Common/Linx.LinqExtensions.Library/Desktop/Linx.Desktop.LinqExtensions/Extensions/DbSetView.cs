using Linx.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Linx.LinqExtensions
{
    public class DbSetView<TEntity> : IDbSet<TEntity>, IOrderedQueryable<TEntity>, IOrderedQueryable, IQueryable<TEntity>, IQueryable, IEnumerable<TEntity>, IEnumerable, IListSource, IDbAsyncEnumerable<TEntity>, IDbAsyncEnumerable
        where TEntity : class
    {
        private readonly IQueryable<TEntity> _set;
        private DbContext _context;
        public DbContext Context { get { return _context; } }
        private readonly Action<TEntity> _initializeEntity;
        public IQueryable<TEntity> FilteredSet { get { return _filteredSet; } }
        private IQueryable<TEntity> _filteredSet = null;
        private List<Expression<Func<TEntity, bool>>> _filters = new List<Expression<Func<TEntity, bool>>>();
        private object[] _innerDataSets = null;
        private Dictionary<TEntity, List<object>> _changedEntities = new Dictionary<TEntity, List<object>>();
        private Func<DbContext, string, System.Data.Entity.Core.Objects.ObjectParameter[], IQueryable<TEntity>> _getQueryByParam = null;
        private Dictionary<string, string> updateMap = null;
        private List<TEntity> _addedEntities = new List<TEntity>();
        private List<TEntity> _updatedEntities = new List<TEntity>();
        private List<TEntity> _deletedEntities = new List<TEntity>();

        public DbSetView(DbContext context, IQueryable<TEntity> query, Func<DbContext, string, System.Data.Entity.Core.Objects.ObjectParameter[], IQueryable<TEntity>> getQueryByParam)
            : this(context, query, getQueryByParam, null, null)
        {
        }

        public DbSetView(DbContext context, IQueryable<TEntity> query, Func<DbContext, string, System.Data.Entity.Core.Objects.ObjectParameter[], IQueryable<TEntity>> getQueryByParam, object[] innerDataSets)
            : this(context, query, getQueryByParam, innerDataSets, null)
        {
        }

        public DbSetView(DbContext context, IQueryable<TEntity> query, Func<DbContext, string, System.Data.Entity.Core.Objects.ObjectParameter[], IQueryable<TEntity>> getQueryByParam, object[] innerDataSets, Expression<Func<TEntity, bool>> filter)
            : this(context, query, getQueryByParam, innerDataSets, filter, null)
        {

        }

        public DbSetView(DbContext context, IQueryable<TEntity> query, Func<DbContext, string, System.Data.Entity.Core.Objects.ObjectParameter[], IQueryable<TEntity>> getQueryByParam, object[] innerDataSets, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
            : this(query, filter, initializeEntity)
        {
            _context = context;
            _innerDataSets = innerDataSets;
            _getQueryByParam = getQueryByParam;

            //Generate update maps 
            if (_innerDataSets != null && _innerDataSets.Count() > 0)
            {
                updateMap = new Dictionary<string, string>();
                foreach (var prop in typeof(TEntity).GetProperties().Where(e => e.GetCustomAttribute<ColumnAttribute>() != null))
                {
                    if (!updateMap.ContainsKey(prop.Name))
                        updateMap.Add(prop.Name, prop.GetCustomAttribute<ColumnAttribute>().Name);
                }
            }

        }

        private DbSetView(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
        {
            _set = query;
            _filteredSet = query;
            SetFilter(filter);
            _initializeEntity = initializeEntity;
        }

        public void ClearFilters()
        {
            _filters.Clear();
            _filteredSet = _set;
        }

        public void SetFilter(Expression<Func<TEntity, bool>> filter, bool append = false)
        {
            if (filter != null)
            {
                if (!append)
                    ClearFilters();

                _filters.Add(filter);
                _filteredSet = _filteredSet.Where(filter);
            }
        }

        public DbQuery<TEntity> Include(string path)
        {
            return null;
        }

        public IQueryable<TEntity> Where(string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
        {
            return this.Where<TEntity>(predicate, parameters);
        }

        public IQueryable<TEntity> Where<TEntityBase>(string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
            where TEntityBase : class
        {
            IQueryable<TEntity> query = _set;
            if (_getQueryByParam != null && !String.IsNullOrWhiteSpace(predicate) && parameters != null)
                query = _getQueryByParam(_context, predicate, parameters);

            if (_filters.Count > 0)
            {
                foreach (var filter in _filters)
                    query = query.Where(filter);
            }

            return query;
        }


        public void ThrowIfEntityDoesNotMatchFilter(TEntity entity)
        {

        }

        private void copyPropertiesToView(TEntity dataView, object innerEntity)
        {
            string typeName = innerEntity.GetType().Name;
            if (updateMap != null && updateMap.Count > 0)
            {
                foreach (var map in updateMap.Where(e => e.Value.Left(".") == typeName))
                {
                    dataView.SetPropertyValue(map.Key, innerEntity.GetPropertyValue(map.Value.Right(".")));
                }
            }
        }

        private void copyPropertiesFromView(TEntity dataView, object innerEntity)
        {
            string typeName = innerEntity.GetType().Name;
            if (updateMap != null && updateMap.Count > 0)
            {
                foreach (var map in updateMap.Where(e => e.Value.Left(".") == typeName))
                {
                    innerEntity.SetPropertyValue(map.Value.Right("."), dataView.GetPropertyValue(map.Key));
                }
            }
        }

        public void RefreshKeys()
        {
            foreach (var dataView in _changedEntities)
            {
                var view = dataView.Key;
                foreach (var innerEntity in dataView.Value)
                {                    
                    copyPropertiesToView(view, innerEntity);                    
                }
                var method = view.GetType().GetMethod("RefreshComposedKeys");
                if (method != null)
                {
                    method.Invoke(view, new object[] { });
                }
            }
        }
        
        public T GetUpdatableEntity<T>(TEntity entity)
        {
            if (_changedEntities.ContainsKey(entity))
                return (T)_changedEntities[entity].FirstOrDefault(e => e is T);
            else
                return default(T);
        }

        public TEntity[] GetAddedViews()
        {
            return _addedEntities.ToArray();
        }

        public TEntity[] GetUpdatedViews()
        {
            return _updatedEntities.ToArray();
        }

        public TEntity[] GetDeletedViews()
        {
            return _deletedEntities.ToArray();
        }

        private void ExecuteAction(TEntity entity, string actionName, TEntity original = null)
        {
            DbEntityEntry entry;
            object innerInstance;
            if (_innerDataSets != null && _innerDataSets.Length > 0)
            {
                if (!_changedEntities.ContainsKey(entity))
                    _changedEntities.Add(entity, new List<object>());

                foreach (var innerType in _innerDataSets.OfType<Type>())
                {
                    innerInstance = _changedEntities[entity].FirstOrDefault(e => e.GetType().FullName == innerType.FullName);
                    if (innerInstance == null)
                    {
                        innerInstance = Activator.CreateInstance(innerType);
                    }
                    copyPropertiesFromView(entity, innerInstance);

                    switch (actionName)
                    {
                        case "Add":
                            entry = _context.Entry(innerInstance);
                            if (entry == null) throw new NullReferenceException("entityObject");
                            entry.State = EntityState.Added;
                            if (!_changedEntities[entity].Contains(innerInstance))
                                _changedEntities[entity].Add(innerInstance);
                            //Add cache CRUD function
                            if (!_addedEntities.Contains(entity))
                                _addedEntities.Add(entity);
                            break;
                        case "Remove":
                            entry = _context.Entry(innerInstance);
                            if (entry == null) throw new NullReferenceException("entityObject");
                            entry.State = EntityState.Deleted;
                            if (!_changedEntities[entity].Contains(innerInstance))
                                _changedEntities[entity].Add(innerInstance);
                            //Add cache CRUD function
                            if (!_deletedEntities.Contains(entity))
                                _deletedEntities.Add(entity);
                            break;
                        case "DetectChanges":
                            entry = _context.Entry(innerInstance);
                            if (entry == null) throw new NullReferenceException("entityObject");
                            entry.State = EntityState.Unchanged;
                            _context.ChangeTracker.DetectChanges();
                            if (!_changedEntities[entity].Contains(innerInstance))
                                _changedEntities[entity].Add(innerInstance);
                            //Add cache CRUD function
                            if (!_updatedEntities.Contains(entity))
                                _updatedEntities.Add(entity);
                            break;
                        case "Attach":
                            entry = _context.Entry(innerInstance);
                            if (entry == null) throw new NullReferenceException("entityObject");
                            entry.State = EntityState.Unchanged;
                            if (!_changedEntities[entity].Contains(innerInstance))
                                _changedEntities[entity].Add(innerInstance);
                            break;
                        case "Create":
                            break;
                        case "AttachUpdated":
                            var originalInnerInstance = Activator.CreateInstance(innerType);
                            copyPropertiesFromView(original, originalInnerInstance);

                            //Original entity must first be reunited with a Context 
                            var originalEntry = _context.Entry(originalInnerInstance);
                            if (originalEntry == null) throw new NullReferenceException("entityObject");
                            originalEntry.State = EntityState.Unchanged;

                            //Apply entity properties changes to the context 
                            _context.Entry(originalInnerInstance).CurrentValues.SetValues(innerInstance);
                            if (!_changedEntities[entity].Contains(originalInnerInstance))
                                _changedEntities[entity].Add(originalInnerInstance);
                            //Add cache CRUD function
                            if (!_updatedEntities.Contains(entity))
                                _updatedEntities.Add(entity);

                            break;
                        default:
                            break;
                    }

                }
                
                //Call Validation:
                var method = entity.GetType().GetMethod("ValidateEntity");
                if (method != null)
                {
                    foreach (var ent in _changedEntities[entity])
                    {
                        var entryCtx = _context.Entry(ent);
                        if (entryCtx != null && (entryCtx.State == EntityState.Added || entryCtx.State == EntityState.Deleted || entryCtx.State == EntityState.Modified))
                        {
                            IEnumerable<string> result = method.Invoke(entity, new object[] { _context, entryCtx.State }) as IEnumerable<string>;
                            if (result != null && result.Count() > 0)
                            {
                                string errorMessage = "";
                                foreach (string error in result)
                                {
                                    errorMessage += (errorMessage.IsNullOrEmpty() ? "" : "\r\n") + error;
                                }
                                if (!errorMessage.IsNullOrEmpty())
                                {
                                    throw new Exception(errorMessage);
                                }
                            }
                            break;
                        }
                    }
                }

            }
        }

        #region Key Manipulation
        private string[] GetPrimaryKeys()
        {
            var keys = typeof(TEntity).GetProperties().Where(e => e.GetCustomAttribute<KeyAttribute>() != null).OrderBy(e => e.GetCustomAttribute<ColumnAttribute>().Order).ToArray();
            return keys.Select(e => e.Name).ToArray();
        }

        private EntityKey GetEntityKey(object[] keyValues)
        {
            List<EntityKeyMember> keyMembers = new List<EntityKeyMember>();
            var keyNames = GetPrimaryKeys();

            if (keyValues.Length != keyNames.Length)
                return null;

            for (int idx = 0; idx < keyNames.Length; idx++)
            {
                keyMembers.Add(new EntityKeyMember(keyNames[idx], keyValues[idx]));
            }

            //If is not possible create the EntityKey object, abort the operation.
            if (keyMembers.Count == 0)
                return null;

            return new EntityKey(_context.GetType().Name + "." + typeof(TEntity).Name, keyMembers);
        }
        
        public TEntity Find(params object[] keyValues)
        {
            return GetObjectByKey(GetEntityKey(keyValues));
        }
        
        public TEntity GetObjectByKey(EntityKey entityKey)
        {
            if (entityKey == null || entityKey.EntityKeyValues.Count() == 0)
                return null;

            string predicate = "";
            List<System.Data.Entity.Core.Objects.ObjectParameter> parameters = new List<System.Data.Entity.Core.Objects.ObjectParameter>();

            foreach (var eKey in entityKey.EntityKeyValues)
            {
                predicate += (predicate == "" ? "" : " && ") + eKey.Key + " == @p" + eKey.Key;
                parameters.Add(new System.Data.Entity.Core.Objects.ObjectParameter("p" + eKey.Key, eKey.Value));
            }

            return this.Where(predicate, parameters.ToArray()).FirstOrDefault();
        }
        #endregion
                

        public TEntity DetectChanges(TEntity entity)
        {
            DoInitializeEntity(entity);
            ThrowIfEntityDoesNotMatchFilter(entity);

            ExecuteAction(entity, "DetectChanges");

            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            DoInitializeEntity(entity);
            ThrowIfEntityDoesNotMatchFilter(entity);

            ExecuteAction(entity, "Add");

            return entity;
        }

        public TEntity AttachUpdated(TEntity entity, TEntity original)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);

            ExecuteAction(entity, "AttachUpdated", original);

            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);

            ExecuteAction(entity, "Attach");

            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var entity = this.Create();
            return (TDerivedEntity)entity;
        }

        public TEntity Create()
        {
            var entity = Activator.CreateInstance<TEntity>();

            DoInitializeEntity(entity);

            ExecuteAction(entity, "Create");

            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);

            ExecuteAction(entity, "Remove");

            return entity;
        }

        /// <summary>
        /// Returns the items in the local cache
        /// </summary>
        /// <remarks>
        /// It is possible to add/remove entities via this property that do NOT match the filter.
        /// Use the <see cref="ThrowIfEntityDoesNotMatchFilter"/> method before adding/removing an item from this collection.
        /// </remarks>
        public ObservableCollection<TEntity> Local { get { return null; } }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        Type IQueryable.ElementType { get { return typeof(TEntity); } }

        Expression IQueryable.Expression { get { return _filteredSet.Expression; } }

        IQueryProvider IQueryable.Provider { get { return _filteredSet.Provider; } }

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList() { throw new InvalidOperationException(); }

        void DoInitializeEntity(TEntity entity)
        {
            if (_initializeEntity != null)
                _initializeEntity(entity);
        }

        public IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
        {
            return ((IDbAsyncEnumerable<TEntity>)_set).GetAsyncEnumerator();
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }
    }

}
