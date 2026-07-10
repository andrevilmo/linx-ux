using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Expressions;
using Linx.LinqExtensions.Functional;


namespace Linx.LinqExtensions
{
    public class ExtendedDbSet<TEntity> : IDbSet<TEntity>, IOrderedQueryable<TEntity>, IOrderedQueryable, IQueryable<TEntity>, IQueryable, IEnumerable<TEntity>, IEnumerable, IListSource, IDbAsyncEnumerable<TEntity>, IDbAsyncEnumerable
        where TEntity : class
    {
        private readonly DbSet<TEntity> _set;
        private DbContext _context;
        public DbContext Context { get { return _context; } }
        private readonly Action<TEntity> _initializeEntity;
        public IQueryable<TEntity> FilteredSet { get { return _filteredSet; } }
        private IQueryable<TEntity> _filteredSet = null;
        private List<Expression<Func<TEntity, bool>>> _filters = new List<Expression<Func<TEntity, bool>>>();

        public ExtendedDbSet(DbContext context)
            : this(context, null, null)
        {
        }

        public ExtendedDbSet(DbContext context, Expression<Func<TEntity, bool>> filter)
            : this(context, filter, null)
        {
        }

        public ExtendedDbSet(DbContext context, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
            : this(context.Set<TEntity>(), filter, initializeEntity)
        {
            _context = context;
        }

        private ExtendedDbSet(DbSet<TEntity> set, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
        {
            _set = set;
            _filteredSet = set;
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
            return _set.Include(path);            
        }

        public IQueryable<TEntity> Where(string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
        {
            return this.Where<TEntity>(predicate, parameters);
        }

        public IQueryable<TEntity> Where<TEntityBase>(string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
            where TEntityBase : class
        {
            IQueryable<TEntity> query = ((IObjectContextAdapter)this._context).ObjectContext.CreateObjectSet<TEntityBase>().OfType<TEntity>().Where(predicate, parameters);

            if (_filters.Count > 0)
            {
                foreach (var filter in _filters)
                    query = query.Where(filter);
            }

            return query;
        }


        public void ThrowIfEntityDoesNotMatchFilter(TEntity entity)
        {
            //if (!MatchesFilter(entity))
            //    throw new ArgumentOutOfRangeException();
        }

        public TEntity Add(TEntity entity)
        {
            DoInitializeEntity(entity);
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var entity = _set.Create<TDerivedEntity>();
            DoInitializeEntity(entity);
            return (TDerivedEntity)entity;
        }

        public TEntity Create()
        {
            var entity = _set.Create();
            DoInitializeEntity(entity);
            return entity;
        }

        public TEntity Find(params object[] keyValues)
        {
            var entity = _set.Find(keyValues);
            if (entity == null)
                return null;

            // If the user queried an item outside the filter, then we throw an error.
            // If IDbSet had a Detach method we would use it...sadly, we have to be ok with the item being in the Set.
            ThrowIfEntityDoesNotMatchFilter(entity);
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Remove(entity);
        }

        /// <summary>
        /// Returns the items in the local cache
        /// </summary>
        /// <remarks>
        /// It is possible to add/remove entities via this property that do NOT match the filter.
        /// Use the <see cref="ThrowIfEntityDoesNotMatchFilter"/> method before adding/removing an item from this collection.
        /// </remarks>
        public ObservableCollection<TEntity> Local { get { return _set.Local; } }

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
