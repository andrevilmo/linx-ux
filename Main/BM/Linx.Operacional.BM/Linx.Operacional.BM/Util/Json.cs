//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Linx.Operacional.BM.Util
//{
//    public static class Json
//    {

//        public static string AsJson(this object instance)
//        {
//            return Newtonsoft.Json.JsonConvert.SerializeObject(instance);
//        }

//        public static string AsJsonComplex(this object instance)
//        {
//            return Newtonsoft.Json.JsonConvert.SerializeObject(instance,
//                        Formatting.Indented,
//                        new JsonSerializerSettings()
//                        {
//                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//                        });
//        }
//    }
//}
