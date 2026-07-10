using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Model
{
    public class LinxAuditoriaNoContextEvents : LinxOperacional
    {
        public override int SaveChanges()
        {
            this.AdjustIdLinxForAdding();
            this.AdjustIdGpeconForAdding();

            try
            {
                int result = base.SaveChanges();
                return result;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.InnerException is System.Data.SqlClient.SqlException)
                    throw Linx.Tools.LinxSqlErrors.SqlException((System.Data.SqlClient.SqlException)dbUpdateException.InnerException.InnerException);
                else
                    throw new Exception(dbUpdateException.InnerException.InnerException.Message);
            }
        }

        private void AdjustIdLinxForAdding()
        {
            if (!this.IdLinx.IsNullOrEmpty())
            {
                foreach (var entity in this.ChangeTracker.Entries().Where(c => c.State == EntityState.Added && c.Entity is ILinx).ToArray())
                {
                    ((ILinx)entity.Entity).ID_LINX = this.IdLinx;
                }
            }
        }

        private void AdjustIdGpeconForAdding()
        {
            if (!this.IdGpecon.IsNullOrEmpty())
            {
                foreach (var entity in this.ChangeTracker.Entries().Where(c => c.State == EntityState.Added && c.Entity is IGpecon).ToArray())
                {
                    if (this.IdApp == 1 || ((IGpecon)entity.Entity).ID_GPECON.IsNullOrEmpty())
                    {
                        ((IGpecon)entity.Entity).ID_GPECON = this.IdGpecon;
                    }
                }
            }
        }
    }
}
