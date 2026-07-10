using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using Linx.Tools;
using Breeze.ContextProvider;

namespace Linx.DataService
{

    public class DataServiceRepository<T> : ContextProvider where T : IDataServiceContext
    {
        private T _context;
        public T Context { get { return _context; } }
        private Type[] _types = null;

        public DataServiceRepository(params Type[] types) : this()
        {
            _types = types;
        }

        public DataServiceRepository()
        {
            _context = Activator.CreateInstance<T>();
        }

        // No metadata from this provider but must implement abstract method.
        protected override string BuildJsonMetadata() { return null; }

        #region Connection Elements
        // No DbConnections needed
        public override System.Data.IDbConnection GetDbConnection()
        {
            return null;
        }

        protected override void OpenDbConnection()
        {
            // do nothing
        }

        protected override void CloseDbConnection()
        {
            // do nothing 
        }
        #endregion

        #region Save processing

        // Todo: delegate to helper classes when it gets more complicated
        protected override bool BeforeSaveEntity(EntityInfo entityInfo)
        {
            return true;
        }

        protected override void SaveChangesCore(SaveWorkState saveWorkState)
        {
            this.SaveInnerChanges(saveWorkState);
        }
        
        #endregion

        #region Saving Core

        private readonly List<KeyMapping> _keyMappings = new List<KeyMapping>();
        private void SaveInnerChanges(SaveWorkState saveWorkState)
        {
            if (_types == null || _types.Length == 0)
                return;

            List<EntityInfo> infoEntities = new List<EntityInfo>();
            List<EntityInfo> mapEntities;
            foreach (Type type in _types)
            {
                if (saveWorkState.SaveMap.TryGetValue(type, out mapEntities))
                {
                    infoEntities.AddRange(mapEntities);
                }
            }

            if (infoEntities.Count == 0)
                return;

            object originalEntity, entity;
            List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
            for (int changeIndex = 0; changeIndex < infoEntities.Count; changeIndex++)
            {
                entity = infoEntities[changeIndex].Entity;
                if (infoEntities[changeIndex].EntityState != EntityState.Added && infoEntities[changeIndex].OriginalValuesMap != null && infoEntities[changeIndex].OriginalValuesMap.Count > 0)
                {
                    originalEntity = Activator.CreateInstance(entity.GetType());
                    originalEntity.CopyInstanceFrom(entity);
                    foreach (var propName in infoEntities[changeIndex].OriginalValuesMap.Keys)
                    {
                        originalEntity.SetPropertyValue(propName, infoEntities[changeIndex].OriginalValuesMap[propName]);
                    }
                }
                else
                    originalEntity = null;

                changeSetEntries.Add(new ChangeSetEntry(changeIndex, entity, originalEntity, infoEntities[changeIndex].EntityState.ToDomainOperation()));
            }

            saveWorkState.KeyMappings = _context.SaveEntities(changeSetEntries).Select(e => new KeyMapping() { EntityTypeName = e.EntityTypeName, TempValue = e.TempValue, RealValue = e.RealValue }).ToList();
        }

        #endregion

    }

    public static class DataServiceExtension
    {
        public static DomainOperation ToDomainOperation(this EntityState value)
        {
            DomainOperation result = DomainOperation.None;
            switch (value)
            {
                case EntityState.Added:
                    result = DomainOperation.Insert;
                    break;
                case EntityState.Deleted:
                    result = DomainOperation.Delete;
                    break;
                case EntityState.Modified:
                    result = DomainOperation.Update;
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
