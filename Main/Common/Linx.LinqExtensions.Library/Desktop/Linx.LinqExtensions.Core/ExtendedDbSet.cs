using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Dynamic.Core;
using Linx.LinqExtensions.Dynamic;

namespace Linx.LinqExtensions
{
    public class ExtendedDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IAsyncEnumerableAccessor<TEntity>, IInfrastructure<IServiceProvider>, IContext
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
        
        public void ThrowIfEntityDoesNotMatchFilter(TEntity entity)
        {
            //if (!MatchesFilter(entity))
            //    throw new ArgumentOutOfRangeException();
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            DoInitializeEntity(entity);
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Add(entity);
        }

        public void AddRange(params TEntity[] entities)
        {
            _set.AddRange(entities);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public EntityEntry<TEntity> Attach(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Attach(entity);
        }

        public void AttachRange(params TEntity[] entities)
        {
            _set.AttachRange(entities);
        }
        public void AttachRange(IEnumerable<TEntity> entities)
        {
            _set.AttachRange(entities);
        }
        
        public EntityEntry<TEntity> Remove(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Remove(entity);
        }

        public void RemoveRange(params TEntity[] entities)
        {
            _set.RemoveRange(entities);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Update(entity);
        }
        public void UpdateRange(params TEntity[] entities)
        {
            _set.UpdateRange(entities);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _set.UpdateRange(entities);
        }


        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        Type IQueryable.ElementType { get { return typeof(TEntity); } }

        Expression IQueryable.Expression { get { return _filteredSet.Expression; } }

        IQueryProvider IQueryable.Provider { get { return _filteredSet.Provider; } }
        
        public IAsyncEnumerable<TEntity> AsyncEnumerable
        {
            get
            {
                return _set.ToAsyncEnumerable();
            }
        }

        public IServiceProvider Instance
        {
            get
            {
                return _context.GetService<IServiceProvider>();
            }
        }

        void DoInitializeEntity(TEntity entity)
        {
            if (_initializeEntity != null)
                _initializeEntity(entity);
        }

        public IQueryable<TEntity> Where(string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
        {
            IQueryable<TEntity> query = _set;

            if (!String.IsNullOrWhiteSpace(predicate))
            {
                List<Object> parameterValues = new List<object>();
                predicate = predicate.ToDynamicLinqExpression(typeof(TEntity), parameters, parameterValues);

                if (!String.IsNullOrWhiteSpace(predicate))
                {                    
                    query = query.Where(predicate, parameterValues.ToArray());
                }
            }

            return query;
        }
    }

}
