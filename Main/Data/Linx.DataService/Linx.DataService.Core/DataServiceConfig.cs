using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;
using System.Collections.ObjectModel;


namespace Linx.DataService
{
    //ServiceConfig
    public class DataServiceConfig
    {
        public static DataServiceConfig Instance
        {
            get
            {
                lock (__lock)
                {
                    if (__instance == null)
                        __instance = new DataServiceConfig();
                    return __instance;
                }
            }
        }

        public JsonSerializerSettings GetJsonSerializerSettings()
        {
            lock (__lock)
            {
                if (_jsonSerializerSettings == null)
                {
                    _jsonSerializerSettings = CreateJsonSerializerSettings();
                }
                return _jsonSerializerSettings;
            }
        }
        
        /// <summary>
        /// Override to use a specialized JsonSerializer implementation.
        /// </summary>
        protected virtual JsonSerializerSettings CreateJsonSerializerSettings()
        {

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };

            // Default is DateTimeZoneHandling.RoundtripKind - you can change that here.
            // jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            // Hack is for the issue described in this post:
            // http://stackoverflow.com/questions/11789114/internet-explorer-json-net-javascript-date-and-milliseconds-issue
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd\\THH:mm:ss.fffK"
                // DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK"
            });
            // Needed because JSON.NET does not natively support I8601 Duration formats for TimeSpan
            jsonSerializerSettings.Converters.Add(new TimeSpanConverter());
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            return jsonSerializerSettings;
        }

        protected static readonly List<String> FrameworkProductNames = new List<String> {
          "Microsoft®",
          "Microsoft (R)",
          "Microsoft ASP.",
          "System.Net.Http",
          "Json.NET",
          "Antlr3.Runtime",
          "Iesi.Collections",
          "WebGrease",
          "Linx.DataService.ContextProvider"
        };


        private static Object __lock = new Object();
        private static DataServiceConfig __instance;

        private JsonSerializerSettings _jsonSerializerSettings = null;

    }

    // http://www.w3.org/TR/xmlschema-2/#duration
    public class TimeSpanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ts = (TimeSpan)value;
            var tsString = XmlConvert.ToString(ts);
            serializer.Serialize(writer, tsString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = serializer.Deserialize<String>(reader);
            return XmlConvert.ToTimeSpan(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }
    }
}
