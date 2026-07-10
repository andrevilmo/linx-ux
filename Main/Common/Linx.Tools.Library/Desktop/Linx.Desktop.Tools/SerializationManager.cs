using System;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Runtime.Serialization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace Linx.Tools
{
    public static partial class SerializationManager<T>
    {

        public static string ObjectToJson(T sourceObject)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            ds.WriteObject(stream, sourceObject);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return jsonString;
        }

        public static T JsonToObject(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(stream);
            stream.Close();
            return obj;
        }

        public static string ObjectToString(T sourceObject)
        {
            string destinationString = "";

            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    serializer.WriteObject(stream, sourceObject);
                    stream.Position = 0;
                    destinationString = reader.ReadToEnd();
                }
            }

            return destinationString;
        }

        public static T StringToObject(string sourceString)
        {
            object destinationObject = null;

            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(sourceString);
                    writer.Flush();
                    stream.Position = 0;
                    destinationObject = serializer.ReadObject(stream);
                }
            }

            return (T)destinationObject;
        }


        public static bool Store(string fileName, T obj)
        {
            using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
            {
                //Creating directory if necessary.
                StorageManager.CreateDirectory(appStore, fileName);

                using (FileStream fileStream = appStore.OpenFile(fileName, FileMode.Create, FileAccess.Write))
                {
                    WriteObject(fileStream, obj);
                }

                return true;
            }
        }

        public static T Retrieve(string filename)
        {
            T obj = default(T);
            using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
            {
                if (appStore.FileExists(filename))
                {
                    using (FileStream fileStream = appStore.OpenFile(filename, FileMode.Open))
                    {
                        obj = ReadObject(fileStream);
                    }
                }
            }
            return obj;
        }

        public static void WriteObject(Stream stream, T obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public static T ReadObject(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

    }

    public static class SerializationManager
    {

        public static string ObjectToJson<T>(T sourceObject)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(sourceObject, typeof(T), Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static T JsonToObject<T>(string jsonString)
        {
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, typeof(T));
        }


        public static string ObjectToJson(object sourceObject)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(sourceObject, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        public static object JsonToObject(string jsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
        }


        public static string GetJsonConnectionString(string sourcePath, string connectionName)
        {
            string connectionString = "";
            if (File.Exists(sourcePath))
            {
                Newtonsoft.Json.Linq.JObject sourceJson = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(sourcePath));
                var sourceCnnStrings = sourceJson.Properties().FirstOrDefault(p => p.Name == "ConnectionStrings");
                if (sourceCnnStrings != null)
                {
                    var stringsSourceObj = sourceCnnStrings.Value as Newtonsoft.Json.Linq.JObject;

                    if (stringsSourceObj != null)
                    {
                        foreach (var prop in stringsSourceObj.Properties())
                        {
                            if (prop.Name == connectionName)
                            {
                                connectionString = prop.Value.ToString();
                                break;
                            }
                        }
                    }                    
                }
            }

            return connectionString;
        }

        public static void MergeJsonConnectionStrings(string sourcePath, string targetPath)
        {
            if (File.Exists(targetPath))
            {
                Newtonsoft.Json.Linq.JObject sourceJson = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(sourcePath));
                Newtonsoft.Json.Linq.JObject targetJson = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(targetPath));

                var sourceCnnStrings = sourceJson.Properties().FirstOrDefault(p => p.Name == "ConnectionStrings");
                var targetCnnStrings = targetJson.Properties().FirstOrDefault(p => p.Name == "ConnectionStrings");

                if (sourceCnnStrings != null && targetCnnStrings != null)
                {
                    var stringsSourceObj = sourceCnnStrings.Value as Newtonsoft.Json.Linq.JObject;
                    var stringsTargetObj = targetCnnStrings.Value as Newtonsoft.Json.Linq.JObject;

                    if (stringsSourceObj != null && stringsTargetObj != null)
                    {
                        foreach (var prop in stringsSourceObj.Properties())
                        {
                            var tProp = stringsTargetObj.Properties().FirstOrDefault(p => p.Name == prop.Name);
                            if (tProp != null)
                                tProp.Value = prop.Value;
                            else
                                stringsTargetObj.Add(new Newtonsoft.Json.Linq.JProperty(prop.Name, prop.Value));
                        }
                    }
                    string resultBody = targetJson.ToString();
                    File.WriteAllText(targetPath, resultBody);
                }
            }

        }

    }
}
