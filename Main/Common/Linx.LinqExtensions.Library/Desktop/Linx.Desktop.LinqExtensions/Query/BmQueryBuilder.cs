using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Linx.LinqExtensions.Dynamic;
using Linx.Tools;

namespace Linx.LinqExtensions.BM
{
    [Serializable]
    public class BmQueryBuilder
    {
        #region Members Variables
        private string assemblyFile;
        private string contextName;
        private string entityName;
        private AppDomain domain;
        private int take = 100;
        private string filter;
        public string DataRows { get; set; }
        public string SqlOutput { get; set; }
        #endregion

        #region Public Implementation

        public BmQueryBuilder()
        {
        }

        public static BmQueryBuilder ExecuteQuery(string assemblyPath, string configPath, string contextName, string entityName, int take, string filter)
        {

            AppDomainSetup setup = new AppDomainSetup();
            setup.ConfigurationFile = (configPath.IsNullOrEmpty() ? assemblyPath + ".config" : configPath);

            // Create the new appdomain with the new config.
            AppDomain appDomain = AppDomain.CreateDomain("DomainTmp", AppDomain.CurrentDomain.Evidence, setup);
            BmQueryBuilder instance = new BmQueryBuilder(appDomain, assemblyPath, contextName, entityName, take, filter);
            try
            {                
                appDomain.DoCallBack(new CrossAppDomainDelegate(instance.RunQuery));
                instance.DataRows = appDomain.GetData("DataRows") as string;
                instance.SqlOutput = appDomain.GetData("SqlOutput") as string;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Problems with the assembly [" + assemblyPath + "]. Details: " + exp.GetCompleteMessage(), "Alert", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
            AppDomain.Unload(appDomain);
            appDomain = null;

            return instance;
        }

        public BmQueryBuilder(AppDomain domain, string assemblyFile, string contextName, string entityName, int take, string filter)
        {
            this.assemblyFile = assemblyFile;
            this.contextName = contextName;
            this.entityName = entityName;
            this.domain = domain;
            this.take = take;
            this.filter = filter;
        }

        protected void RunQuery()
        {
            Assembly assembly = AssemblyHelper.LoadWithDependencies(this.assemblyFile);

            var dbType = assembly.GetType(this.contextName);

            if (assembly == null || dbType == null)
                return;

            object dbInstance = Activator.CreateInstance(dbType);
            object dbConfig = dbInstance.GetPropertyValue("Configuration");
            if (dbConfig != null)
                dbConfig.SetPropertyValue("ProxyCreationEnabled", false);

            string sqlOutput = "";
            var dataBase = dbInstance.GetPropertyValue("Database");
            if (dataBase != null)
                dataBase.SetPropertyValue("Log", (Action<string>)((s) => sqlOutput += s));
            
            var dbMemberInfo = dbType.GetMember(this.entityName).FirstOrDefault();

            object[] queryResult = null;
            if (dbMemberInfo is PropertyInfo)
            {
                var query = ((IQueryable<object>)((PropertyInfo)dbMemberInfo).GetValue(dbInstance, null));
                if (!filter.IsNullOrEmpty())
                    query = query.Where(filter);
                queryResult = query.Take(this.take).ToArray();
            }
            else if (dbMemberInfo is MethodInfo)
            {
                var method = ((MethodInfo)dbMemberInfo);
                var query = ((IQueryable<object>)method.Invoke(dbInstance, new object[] { }));
                if (!filter.IsNullOrEmpty())
                    query = query.Where(filter);
                queryResult = query.Take(this.take).ToArray();
            }

            var dataRows = (queryResult == null ? "" : Newtonsoft.Json.JsonConvert.SerializeObject(queryResult));

            this.domain.SetData("SqlOutput", sqlOutput);
            this.domain.SetData("DataRows", dataRows);
        }

        #endregion
    }
}
