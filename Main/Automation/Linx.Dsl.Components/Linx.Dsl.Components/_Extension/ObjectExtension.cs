using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Linx.Dsl.Components
{
    public static class ObjectExtension
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            if (retval is Guid)
                retval = retval.ToString();

            return (T)Convert.ChangeType(retval, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
        }

        public static List<string> GetProps(this Object obj)
        {
            List<string> retorno = new List<string>();
            Type type = obj.GetType();

            var Props = type.GetProperties();

            foreach (var p in Props)
            {
                PropertyInfo info = type.GetProperty(p.Name);

                if ((typeof(ICollection).IsAssignableFrom(info.PropertyType) || typeof(ICollection<>).IsAssignableFrom(info.PropertyType)) == false)
                    retorno.Add(p.Name);
            }

            return retorno;
        }

        public static object GetPropertyValue(this object srcobj, string propertyName)
        {
            if (srcobj == null)
                return null;

            object obj = srcobj;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = propertyName.Split('.');

            foreach (string propertyNamePart in propertyNameParts)
            {
                if (obj == null) return null;

                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    PropertyInfo pi = obj.GetType().GetProperty(propertyNamePart);
                    if (pi == null) return null;
                    obj = pi.GetValue(obj, null);
                }
                else
                {   // propertyNamePart is areference to specific element 
                    // (by index) inside a collection
                    // like AggregatedCollection[123]
                    //   get collection name and element index
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object
                    PropertyInfo pi = obj.GetType().GetProperty(collectionPropertyName);
                    if (pi == null) return null;
                    object unknownCollection = pi.GetValue(obj, null);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as Array[];
                        obj = collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as IList
                        System.Collections.IList collectionAsList = unknownCollection as System.Collections.IList;
                        if (collectionAsList != null)
                        {
                            obj = collectionAsList[collectionElementIndex];
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static string ObjectToXml<U>(this object referencia)
        {
            var serializer = new DataContractSerializer(typeof(U));
            using (var backing = new System.IO.StringWriter())
            using (var writer = new System.Xml.XmlTextWriter(backing))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 3;

                serializer.WriteObject(writer, referencia);
                return backing.ToString();
            }
        }

        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static U StringToXml<U>(this string referencia)
        {
            if (referencia != null)
            {
                var serializer = new DataContractSerializer(typeof(U));
                using (var backing = new System.IO.StringReader(referencia))
                using (var reader = new System.Xml.XmlTextReader(backing))
                {
                    return (U)serializer.ReadObject(reader);
                }
            }

            return default(U);
        }
    }
}
