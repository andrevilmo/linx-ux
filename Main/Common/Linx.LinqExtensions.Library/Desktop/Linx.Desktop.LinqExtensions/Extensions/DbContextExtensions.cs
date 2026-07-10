using Linx.LinqExtensions;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Linx.LinqExtensions
{
    public static class DbContextExtensions
    {
        private const string ControllerFake = "Auditoria";
        private const string ActionFake = "AdicionarAuditoria";
        #region Extensions Methods
        static AuditAttribute GetAuditAttribute(this object obj)
        {
            return obj.GetType().GetCustomAttribute<AuditAttribute>();
        }

        static TableAttribute GetTableAttribute(this object obj)
        {
            return obj.GetType().GetCustomAttribute<TableAttribute>();
        }
        #endregion


        #region Changes Verification Methods
        static bool HasChangedEntity(DbPropertyValues oldE, DbPropertyValues newE)
        {
            if (oldE == null || newE == null) return true;
            foreach (var n in oldE.PropertyNames)
            {
                if (oldE[n] != newE[n])
                    return true;
            }
            return false;
        }
        static bool HasChangedProperty(DbPropertyValues oldE, DbPropertyValues newE, string propertyName)
        {
            return oldE == null || newE == null || (oldE[propertyName] ?? "").ToString() != (newE[propertyName] ?? "").ToString();
        }
        #endregion


        #region Public Method

        public static void AuditChangedEntities(this DbContext ctx, Func<string, bool> fnHasAuditEntity, DbEntityEntryCloned[] entries)
        {
            var auditsEntitiesType = entries.Select(e => e.TypeFullName).Distinct().Where(e => fnHasAuditEntity(e));

            if (!auditsEntitiesType.Any()) return;

            IAuditHelper AuditHelper = CreateAuditHelper();
            long auditId = 0L;

            foreach (var oldAuditEntry in entries)
            {
                if (oldAuditEntry == null || oldAuditEntry.State == EntityState.Unchanged) continue;

                AuditAttribute auditAttr = oldAuditEntry.Entity.GetAuditAttribute();

                if (auditAttr.AuditType == AuditType.None || !auditsEntitiesType.Contains(oldAuditEntry.TypeFullName)) continue;

                try
                {
                    if (auditId == 0L) auditId = AuditHelper.Audit(Assembly.GetCallingAssembly().FullName);

                    DbPropertyValues originalValues = oldAuditEntry.State == EntityState.Added ? null : oldAuditEntry.OriginalValues;
                    DbPropertyValues currentValues = oldAuditEntry.State == EntityState.Deleted ? null : oldAuditEntry.CurrentDbEntityEntry.CurrentValues;

                    if (auditAttr.AuditType == AuditType.Entity && HasChangedEntity(originalValues, currentValues))
                        AuditItemDetails(AuditHelper, auditId, oldAuditEntry, originalValues, currentValues, null);
                    else if (auditAttr.AuditType.In(AuditType.AllColumns, AuditType.SelectedColumns))
                        AuditItemDetails(AuditHelper, auditId, oldAuditEntry, originalValues, currentValues, auditAttr.Columns);

                }
                catch (Exception ex)
                {
                    ExceptionLogger.Instance.LogError(new Exception("Não foi possível adicionar a auditoria. Verifique o Inner Exception para mais detalhes." + ex.Message, ex), ControllerFake, ActionFake);
                }
            }
        }

        //todo: implementar o customAudit
        //public static void CustomAudit<TEntity>(int AuditId, TEntity oldEntity, TEntity newEntity)
        //{
        //    //Todo: implement code here
        //}
        //public static void CustomAudit<TEntity>(TEntity oldEntity, TEntity newEntity)
        //{
        //    CustomAudit(0, oldEntity, newEntity);
        //}
        #endregion


        #region Private Methods

        static string SerializeToJson(Dictionary<string, string> entity)
        {
            return entity == null ? null : SerializationManager.ObjectToJson(entity);
        }

        static void AuditItemDetails(IAuditHelper AuditHelper, long auditId, DbEntityEntryCloned auditEntry, DbPropertyValues originalValues, DbPropertyValues currentValues, string[] columnsForAudit)
        {
            var table = auditEntry.Entity.GetTableAttribute();
            var auditItem = AuditHelper.AuditItem(auditId, table.Schema, table.Name ?? auditEntry.Entity.GetType().Name, auditEntry.State.GetStringValue());
            if (columnsForAudit == null || columnsForAudit.Length == 0)
            {
                AuditHelper.AuditItemDetalhe(auditItem,
                    null,
                    SerializeToJson(originalValues.GetDictionaryValues()),
                    SerializeToJson(currentValues.GetDictionaryValues()));
            }
            else
            {
                foreach (var propName in columnsForAudit.Where(n => HasChangedProperty(originalValues, currentValues, n)))
                {
                    AuditHelper.AuditItemDetalhe(auditItem,
                        propName,
                        originalValues.GetStringValue(propName),
                        currentValues.GetStringValue(propName));
                }
            }

        }

        static IAuditHelper CreateAuditHelper()
        {
            IAuditHelper _auditHelper = null;
            try { _auditHelper = ImplementationHelper<IAuditHelper>.GetInstance("AuditHelper", "Linx.Framework.ControleSistema.BM"); } catch { }
            return _auditHelper;
        }
        #endregion

    }
}
