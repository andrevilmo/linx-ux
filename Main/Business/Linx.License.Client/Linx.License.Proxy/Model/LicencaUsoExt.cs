using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.License.Client
{
    public partial class LicencaUso
    {
        #region Crypt

        public void EncryptData()
        {
            this.IdLicencaUso = Licensecing.Encrypt(this.IdLicencaUso);
            this.LxStatusChave = Licensecing.Encrypt(this.LxStatusChave);
            this.Periodicidade = Licensecing.Encrypt(this.Periodicidade);
            this.DiasOffline = Licensecing.Encrypt(this.DiasOffline);
            this.Mensagem = Licensecing.Encrypt(this.Mensagem);
            this.TemporaryIdLicencaUso = Licensecing.Encrypt(this.TemporaryIdLicencaUso);
        }

        public void DecryptData()
        {
            this.IdLicencaUso = Licensecing.Decrypt(this.IdLicencaUso);
            this.LxStatusChave = Licensecing.Decrypt(this.LxStatusChave);
            this.Periodicidade = Licensecing.Decrypt(this.Periodicidade);
            this.DiasOffline = Licensecing.Decrypt(this.DiasOffline);
            this.Mensagem = Licensecing.Decrypt(this.Mensagem);
            this.TemporaryIdLicencaUso = Licensecing.Decrypt(this.TemporaryIdLicencaUso);
        }

        #endregion
    }

    [DbConfigurationType(typeof(SQLiteDbConfiguration))]
    public partial class LicenseContext
    {
        public LicenseContext(SQLiteConnection connection)
            : base(connection, true)
        {
            
        }
    }

    public class SQLiteDbConfiguration : DbConfiguration
    {
        public SQLiteDbConfiguration()
        {
            SetDefaultConnectionFactory(new System.Data.Entity.Infrastructure.LocalDbConnectionFactory("v11.0"));
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}
