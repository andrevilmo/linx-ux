// -----------------------------------------------------------------------
// <copyright file="JsonSupport.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class JsonScriptSupport
    {

        public static string GetSubmitChangesPostCommand(object entity)
        {
            if (entity.IsNull())
                return String.Empty;

            Type entityType = entity.GetType();
            return String.Format("/{0}-{1}DomainService.svc/json/SubmitChanges", entityType.Namespace.Replace(".", "-"), entityType.Namespace.Right(".")); 
        }

        public static string GetSubmitChangesPostPayLoad<T>(IEnumerable<T> entities)
        {
            string body = "\r\n       {\"changeSet\": [";

            int operationIndex = -1;
            foreach (T entity in entities)
            {
                operationIndex++;
                body += GetInsertsForSubmittingChanges(entity, ref operationIndex);                
            }
            
            body += "\r\n       ]}";

            return body;
        }


        /// <summary>
        /// Generate serialized datetime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToSerializedString(this DateTime dateTime)
        {
            string result = null;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DateTime));
            using (MemoryStream stream = new MemoryStream())
            {
                dateTime = dateTime.Add(TimeZone.CurrentTimeZone.GetUtcOffset(dateTime)).ToUniversalTime();
                serializer.WriteObject(stream, dateTime);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }


        public static string FormatValue(object enteredValue)
        {
            if (enteredValue.IsNull())
                return "null";

            string dataType = enteredValue.GetType().FullName;
            
            if (dataType.ToLower().Contains("string"))
                return "\"" + enteredValue.ToString().Replace(@"\", @"\\").Replace(@"/", @"\/").Replace(@"""", @"\""").Replace("\n", @"\n").Replace("\t", @"\t").Replace("\r", @"\r").Replace("\b", @"\b").Replace("\f", @"\f") + "\"";
            else if (dataType.ToLower().Contains("guid"))
                return "\"" + enteredValue.ToString() + "\"";
            else if (dataType.ToLower().Contains("bool"))
                return enteredValue.ToString().ToLower();
            else if (dataType.ToLower().Contains("datetime"))
                return ((DateTime)enteredValue).ToSerializedString();
            else if (dataType.ToLower().Contains("double"))
                return ((double)enteredValue).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (dataType.ToLower().Contains("decimal"))
                return ((decimal)enteredValue).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (dataType.ToLower().Contains("float"))
                return ((float)enteredValue).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else 
                return enteredValue.ToString();
            
        }

        /// <summary>
        /// Read object to get POST Payload.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string GetInsertsForSubmittingChanges(object entity, ref int operationIndex)
        {
            string body = String.Empty;
            System.Collections.IEnumerable details;
            Type entityType = entity.GetType();
            object[] customAttributes;
            object enteredValue;

            //Insert Entity
            if (operationIndex > 0)
                body += "\r\n" + "       ,";
            body += "\r\n" + "       {\"Id\":\"" + operationIndex.ToString() + "\",\"Operation\":2," + "";
            body += "\r\n" + "       \"Entity\":{\"__type\":\"" + entityType.Name + ":#" + entityType.Namespace + "\"";

            var properties = entity.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo member in properties)
            {
                if (!member.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1"))                
                {                   
                    //Pair Values
                    customAttributes = member.GetCustomAttributes(typeof(DataMemberAttribute), false);
                    if (customAttributes != null && customAttributes.Length > 0)
                    {
                        //Check Functional Point
                        if ((ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string) != null)
                        {                            
                            enteredValue = entity.GetPropertyValue(member.Name);
                            body += (body.IsNullOrEmpty() ? " " : ", ") + "\"" + member.Name + "\":" + FormatValue(enteredValue);
                        }
                    }
                    
                }
            }

            body += "}}";

            //Get Details
            foreach (System.Reflection.PropertyInfo member in properties)
            {
                if (member.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1"))
                {
                    customAttributes = member.GetCustomAttributes(typeof(CompositionAttribute), false);
                    if (customAttributes != null && customAttributes.Length > 0)
                    {
                        details = entity.GetPropertyValue(member.Name) as System.Collections.IEnumerable;
                        if (details != null)
                        {
                            foreach (var element in details)
                            {
                                operationIndex++;
                                body += GetInsertsForSubmittingChanges(element, ref operationIndex);
                            }
                        }
                    }
                }                
            }


            return body;
        }

       

    }
}
