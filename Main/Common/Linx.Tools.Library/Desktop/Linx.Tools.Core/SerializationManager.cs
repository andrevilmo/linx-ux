using System;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

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
            stream.Dispose();
            return jsonString;
        }

        public static T JsonToObject(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(stream);
            stream.Dispose();
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
            return true;
        }

        public static T Retrieve(string filename)
        {
            T obj = default(T);

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

    }
}
