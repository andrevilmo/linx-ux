using Linx.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;


namespace Linx.DataService
{
    
    public class QueueTransaction
    {
        public static QueueTransaction GetTransaction(Guid transactionID)
        {
            var data = (string)WebCacheHelper.GetWebCache(transactionID.ToString());
            return data.IsNullOrEmpty() ? null : SerializationManager.JsonToObject<QueueTransaction>(data);
        }
        public static QueueTransaction SaveTransaction<T>(SaveInformation<T> saveInfo, string assemblyName, string controllerName, string actionName)
        {
            var obj = QueueTransaction.GetTransaction(saveInfo.TransactionID);
            if (obj.IsNull())
            {
                obj = new QueueTransaction(saveInfo.TransactionID);
            }

            obj.AddChange(new ChangeTracker
            {
                RootType = saveInfo.GetRootType(),
                ListRootType = saveInfo.DataList.GetType(),
                AssemblyName = assemblyName,
                Controller = controllerName,
                ActionName = actionName,
                ComponentName = saveInfo.ComponentName,
                DataObjects = saveInfo.GetDataListToString(),
                RelationInfo = saveInfo.RelationInfo
            });

            obj.SaveInCache();

            return obj;
        }

        public QueueTransaction(Guid id)
        {
            this.TransactionID = id;
            this.Changes = new Queue<ChangeTracker>();
        }

        public Guid TransactionID { get; set; }
        public Queue<ChangeTracker> Changes;

        public void AddChange(ChangeTracker change)
        {
            change.Parent = this;
            this.Changes.Enqueue(change);
        }

        /// <summary>
        /// Don't add Item's, for add use AddChange Method
        /// </summary>
        /// <returns></returns>
        public Queue<ChangeTracker> GetChanges()
        {
            return Changes;
        }

        public void SaveInCache()
        {
            if (GetTransaction(TransactionID).IsNull())
                WebCacheHelper.AddWebCache(TransactionID.ToString(), SerializationManager.ObjectToJson<QueueTransaction>(this));
            else
                WebCacheHelper.UpdateWebCache(TransactionID.ToString(), SerializationManager.ObjectToJson<QueueTransaction>(this));
        }

        public void DeleteCache()
        {
            WebCacheHelper.RemoveWebCache(TransactionID.ToString());
        }

        public ChangeTracker[] SubmitTansaction()
        {
            var executedChanges = new List<ChangeTracker>();
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            //{
                while (this.Changes.Count > 0)
                {
                    var change = this.Changes.Dequeue();
                    var assem = AssemblyLoadContext.Default.LoadFromAssemblyPath(change.AssemblyName);
                    var controllerInstance = assem.CreateInstance(change.Controller);

                    dynamic retListObject = controllerInstance.GetType().GetMethod(change.ActionName + "__ForMEF", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(controllerInstance, new object[] { change.DataObjects, change.RelationInfo, executedChanges });
                    change.ReturnedObjects = SerializationManager.ObjectToJson(retListObject);
                    change.SetListReturnedObjects(retListObject as IEnumerable);
                    executedChanges.Add(change);
                }
            //    scope.Complete();
            //}
            this.DeleteCache();

            return executedChanges.ToArray();
        }
    }


    public class ChangeTracker
    {
        public QueueTransaction Parent { get; set; }
        public Type RootType { get; set; }
        public Type ListRootType { get; set; }
        public string AssemblyName { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public string ComponentName { get; set; }
        public string RelationInfo { get; set; }
        public string DataObjects { get; set; }
        public string ReturnedObjects { get; set; }
        public List<object> ListReturnedObjects { get; set; }
        public void SetListReturnedObjects(IEnumerable list)
        {
            ListReturnedObjects = list.Cast<object>().ToList();
        }
    }

    public static class SaveInformationExtensions
    {
        internal static string GetDataListToString<T>(this SaveInformation<T> saveInfo)
        {
            return SerializationManager<List<T>>.ObjectToJson(saveInfo.DataList);
        }
        internal static Type GetRootType<T>(this SaveInformation<T> saveInfo)
        {
            return typeof(T);
        }

        /// <summary>
        /// performs the verification on consistency of information, wrong information throw exception
        /// </summary>
        public static void Validate<T>(this SaveInformation<T> saveInfo)
        {
            if (saveInfo.IsNull())
                throw new ArgumentException("The object SaveInfo is Null");
            if (saveInfo.ComponentName.IsNull())
                throw new ArgumentException("ComponentName is empty");
            //if (saveInfo.DataList.IsNull() || saveInfo.DataList.Count == 0)
            //    throw new ArgumentException("DataList is null or empty");
            if (saveInfo.TransactionID == Guid.Empty)
                throw new ArgumentException("TransactionID is null or empty");

        }
    }

    [Serializable]
    public class SaveInformation<T>
    {
        public SaveInformation() { }
        public SaveInformation(Guid transactionId, string componentName, string relationInfo, List<T> dataList)
        {
            this.TransactionID = transactionId;
            this.ComponentName = componentName;
            this.RelationInfo = relationInfo;
            this.DataList = dataList;
        }

        public Guid TransactionID { get; set; }
        public string ComponentName { get; set; }
        public string RelationInfo { get; set; }
        public List<T> DataList { get; set; }


    }


    //Criar help para desmembrar o ViewMapInfo
    public class ViewMapHelper
    {
        public static ViewMapHelper Parse(string viewMapInfo)
        {
            var _this = new ViewMapHelper();
            var parts = viewMapInfo.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _this.ParentUIView = parts[0].Right(":");
            _this.EntityName = parts[1].Right(":");
            _this.ParentFiels = parts[2].Right(":").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _this.Fiels = parts[3].Right(":").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return _this;
        }
        public string ParentUIView { get; set; }
        public string EntityName { get; set; }
        public string[] ParentFiels { get; set; }
        public string[] Fiels { get; set; }

        public List<T> ReplaceEntities<T>(List<T> rootList, List<object> sourceRootForReplace)
        {
            if (sourceRootForReplace == null || sourceRootForReplace.Count == 0)
                return rootList;

            if (ParentFiels.Length != Fiels.Length)
                throw new InvalidOperationException("The Detail Key don't match with Parent Key relationship.");

            var sourceForReplace = GetEntitiesInListByTypeName(sourceRootForReplace);
            if (sourceForReplace.Count == 0) throw new IndexOutOfRangeException("Não foi encontrado o objeto pai (\"" + EntityName + "\")");
            foreach (var entity in sourceForReplace)
            {
                foreach (var root in rootList)
                {
                    for (var i = 0; i < ParentFiels.Length; i++)
                    {
                        if (root.GetPropertyValue(Fiels[i]).Equals(entity.GetPropertyValue("Temporary" + ParentFiels[i])))
                            root.SetPropertyValue(Fiels[i], entity.GetPropertyValue(ParentFiels[i]));
                    }
                }
            }
            return rootList;
        }
        private List<object> GetEntitiesInListByTypeName(List<object> parent)
        {
            if (parent.First().GetType().Name == EntityName)
                return parent;
            List<object> returnedObjects = new List<object>();
            Action<object> ac = null;
            ac = (obj) =>
            {
                var t = obj.GetType();
                if (t.Name == EntityName)
                    returnedObjects.Add(obj);

                foreach (var nav in t.GetTypeInfo().GetProperties().Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType.GetTypeInfo().IsGenericType))
                    ((IEnumerable<object>)nav.GetValue(obj)).ToList().ForEach(ac);
            };

            parent.ForEach(ac);
            return returnedObjects;
        }

    }
}
