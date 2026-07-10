#region Usings
using Linx.ServiceBus.Starter.Areas.HelpPage.Models;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Xml.Serialization;
#endregion

namespace Linx.ServiceBus.Starter.Areas.HelpPage.Controllers
{
    public partial class HelpController
    {
        private MediaTypeHeaderValue JsonApplication
        {
            get { return new MediaTypeHeaderValue("application/json"); }
        }
        private MediaTypeHeaderValue JsonText
        {
            get { return new MediaTypeHeaderValue("text/json"); }
        }

        #region IndexByAssembly

        public ActionResult IndexByAssembly(string assemblyName)
        {
            if (assemblyName == null)
                throw new System.Web.HttpException("O AssemblyName não foi passado /HelpAssembly/{assemblyName}");

            string assemblyPath = Server.MapPath("~/Help_WebApi/" + assemblyName + ".xml");

            if (!System.IO.File.Exists(assemblyPath))
                throw new System.Web.HttpException("Não foi encontrado a documentação do assembly " + assemblyName);

            var assembly = Linx.Tools.AssemblyHelper.Load(Server.MapPath("~/Bin/" + assemblyName + ".dll"));

            string[] controllers = assembly.GetTypes().Where(t => TypeInherits(t, typeof(ApiController))).Select(t => t.Name.Substring(0, t.Name.Length - 10)).ToArray();


            var apiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions;

            return View("Index", FindByController(apiDescriptions, (a) => controllers.Contains((string)a.Route.Defaults["Controller"])));
        }

        #endregion

        #region FindByController

        private Collection<ApiDescription> FindByController(Collection<ApiDescription> apiDescriptions, Func<ApiDescription, bool> predicate)
        {
            var list = new Collection<System.Web.Http.Description.ApiDescription>();
            for (int i = 0; i < apiDescriptions.Count; i++)
            {
                if (predicate(apiDescriptions[i]))
                    list.Add(apiDescriptions[i]);
            }

            return list;
        }

        #endregion

        #region IndexByController

        public ActionResult IndexByController(string controllerName)
        {
            if (controllerName == null)
                throw new System.Web.HttpException("O controllerName não foi passado /HelpController/{controllerName}");

            if (controllerName.EndsWith("Controller"))
                controllerName = controllerName.Substring(0, controllerName.Length - 10);

            var apiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions;

            var controllers = FindByController(apiDescriptions, (a) => a.ActionDescriptor.ControllerDescriptor.ControllerName.Equals(controllerName));
            ViewData.Add("Controller", controllerName);
            ViewData.Add("ControllerDescription", controllers.GetControllerDescription());

            return View(controllers);
        }

        #endregion

        #region Authentication

        public ActionResult Authentication()
        {
            HelpPageApiModel auth = new HelpPageApiModel();

            #region Add Api Description
            auth.ApiDescription = new ApiDescription()
               {
                   HttpMethod = HttpMethod.Get,
                   RelativePath = "Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/json/AuthenticateJson?userName={userName}&password={password}&applicationId={applicationId}",
                   //RelativePath = "/Linx-TCS0101-BO-TcsAutorizacao-TcsAutorizacaoDomainService.svc/json/AuthenticateJson",
                   Documentation = "Documentation for authentication in LinxServiceBus"
               };
            #endregion

            #region Add Parameter Descriptions
            auth.ApiDescription.ParameterDescriptions.Add(new ApiParameterDescription() { Name = "userName", Source = ApiParameterSource.FromUri, ParameterDescriptor = HttpParameterDescriptorFake.StringValue(), Documentation = "String that stores a user name" });
            auth.ApiDescription.ParameterDescriptions.Add(new ApiParameterDescription() { Name = "password", Source = ApiParameterSource.FromUri, ParameterDescriptor = HttpParameterDescriptorFake.StringValue(), Documentation = "String that stores a user password" });
            auth.ApiDescription.ParameterDescriptions.Add(new ApiParameterDescription() { Name = "applicationId", Source = ApiParameterSource.FromUri, ParameterDescriptor = HttpParameterDescriptorFake.GuidValue(new Guid("ABCDEFAB-9012-ABCD-7890-CDEFABCDEFAB")), Documentation = "Guid that represents a application id" });
            #endregion

            #region Add Sample Response
            var script = @"{
    ""AuthenticateJsonResult"": [
        {
            ""Key"": 1,
            ""Value"": ""abcdefab-cdef-abcd-efab-cdefabcdefab""
        },
        {
            ""Key"": 2,
            ""Value"": ""12345678-cdef-3456-efab-123456789012""
        },
        {
            ""Key"": 3,
            ""Value"": ""abcdefab-9012-abcd-7890-cdefabcdefab""
        },
        {
            ""Key"": 4,
            ""Value"": ""12345678-9012-3456-7890-123456789012""
        },
        {
            ""Key"": 5,
            ""Value"": ""abcdefab-9012-3456-7890-123456789012""
        },
        {
            ""Key"": 6,
            ""Value"": ""0""
        }
    ]
}";

            var scritText = script.ToTextSample();
            auth.SampleResponses.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"), scritText);
            auth.SampleResponses.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/json"), scritText);
            #endregion

            #region Add Addictional Information
            auth.AdditionalInformation["Result Means"] = "Key1 = CurrentCompany, Key2 = AuthorizationToken, Key3 = CurrentUser, Key4 = AccessGroup, key5 = EconomicGroup, key6 = Environment".ToTextSample();
            auth.AdditionalInformation["Headers"] = string.Format("Content-Type:\t\t application/json; charset=utf-8{0}CurrentCompany:\t\t Key1{0}AuthorizationToken:\t Key2{0}CurrentUser:\t\t Key3{0}AccessGroup:\t\t Key4{0}EconomicGroup:\t\t Key5{0}Environment:\t\t Key6{0}Application:\t\t applicationId", Environment.NewLine).ToTextSample();
            #endregion

            return View("Api", auth);
        }

        #endregion

        #region GetQuery

        internal void GetQuery(HelpPageApiModel api)
        {
            int stringCount = 1;
            string condictionItem = string.Empty, query = string.Empty;

            if (api == null || api.ApiDescription == null || api.ApiDescription.ActionDescriptor == null ||
                api.ApiDescription.ActionDescriptor.ReturnType == null)
                return;

            try
            {

                Type typeReturned = GetReturnedType(api);

                if (typeReturned == null || typeReturned.GetProperties().Length == 0) return;
                var properties = typeReturned.GetProperties()
                    .Where(p => !IgnoreProperty(p))
                    .ToArray();

                #region filter
                //filter
                List<string> filterItems = new List<string>();
                foreach (var p in properties)
                {
                    string sample = GetJsonSample(p.PropertyType);
                    if (p.PropertyType == typeof(string))
                    {
                        switch (stringCount++)
                        {
                            case 1:
                                condictionItem = string.Format("startswith({0}, {1})", p.Name, sample);
                                break;
                            case 2:
                                condictionItem = string.Format("substringof({1}, {0})", p.Name, sample);
                                break;
                            case 3:
                                condictionItem = string.Format("endswith({0}, {1})", p.Name, sample);
                                break;

                            default:
                                condictionItem = string.Format("{0} {1} {2}", p.Name, "eq", sample);
                                break;
                        }
                    }
                    else
                        if (p.PropertyType == typeof(bool))
                            condictionItem = string.Format("{0}", p.Name);
                        else
                            condictionItem = string.Format("{0} {1} {2}", p.Name, "eq", sample);

                    filterItems.Add(condictionItem);

                }
                List<string> condictions = new List<string>();
                condictions.Add("$filter=" + string.Join(" and ", filterItems));
                #endregion

                #region orderby
                //orderby
                condictions.Add("\r\n$orderby=" + string.Format("{0} desc, {1} asc", properties[0].Name, properties.Length > 1 ? properties[1].Name : properties[0].Name));
                #endregion
                #region skip
                //skip
                condictions.Add("\r\n$skip=5");
                #endregion
                #region top
                //top
                condictions.Add("\r\n$top=10");
                #endregion
                #region select
                //select
                condictions.Add("\r\n$select=" + string.Join(", ", properties.Select(c => c.Name)));
                #endregion
                #region inlinecount
                //inlinecount
                condictions.Add("\r\n$inlinecount=allpages");
                #endregion

                query = "?" + string.Join("&", condictions) + "\r\n\r\nMore information:\r\n http://www.OData.org \r\n http://www.odata.org/documentation/odata-v3-documentation/odata-core/#1023_Querying_Collections";
            }
            catch (Exception ex)
            {
                query = ex.Message;
            }

            api.AdditionalInformation["OData Query"] = query.ToTextSample();
        }





        #endregion

        #region  Private Methods [GetJsonSample, GetJsonSampleSaveChanges, TypeInherits, GetReturnedType, GetTypeOrInnerType, SaveChangesDescription, IgnoreProperty, GetReturnedTypes, GetAutoGeneratedKey, GetSampleByType, IsIdentity, IsAutomaticSequencyProperty]

        private string GetJsonSample(Type type)
        {
            if (type.ContainsGenericParameters)
                type = type.GenericTypeArguments[0];

            string returned = string.Empty;

            switch (type.Name.ToLower())
            {
                case "char":
                    returned = "'a'";
                    break;
                case "string":
                    returned = "'abc'";
                    break;
                case "byte":
                case "int":
                case "int16":
                case "int32":
                case "int64":
                case "uint":
                case "short":
                case "ushort":
                case "long":
                case "ulong":
                    returned = "123";
                    break;
                case "decimal":
                    returned = "12.34";
                    break;
                case "guid":
                    returned = "guid'12345678-ABCD-EF12-3456-7890ABCDEF12'";
                    break;
                case "datetime":
                    returned = "datetime'2012-12-31T01:04:00.000'";
                    break;
            }

            return returned;
        }

        private string GetJsonSampleSaveChanges(Type type)
        {
            if (type.ContainsGenericParameters)
                type = type.GenericTypeArguments[0];

            string returned = string.Empty;

            switch (type.Name.ToLower())
            {
                case "char":
                    returned = "\"a\"";
                    break;
                case "string":
                    returned = "\"abc\"";
                    break;
                case "byte":
                case "int":
                case "int16":
                case "int32":
                case "int64":
                case "uint":
                case "short":
                case "ushort":
                case "long":
                case "ulong":
                    returned = "\"123\"";
                    break;
                case "decimal":
                    returned = "\"12.34\"";
                    break;
                case "guid":
                    returned = "\"12345678-ABCD-EF12-3456-7890ABCDEF12\"";
                    break;
                case "datetime":
                    returned = "\"2012-12-31T01:04:00.000\"";
                    break;
                case "bool":
                case "boolean":
                    returned = "\"true\"";
                    break;
            }

            return returned;
        }

        private bool TypeInherits(Type type, Type targetType)
        {
            if (type != null && type == targetType)
                return true;

            if (type.BaseType != null)
                return TypeInherits(type.BaseType, targetType);
            else
                return false;
        }

        private Type GetReturnedType(HelpPageApiModel api)
        {
            Type typeReturned = api.ApiDescription.ActionDescriptor.ReturnType;
            return GetTypeOrInnerType(typeReturned);
        }

        private bool IsGenericType(Type typeReturned)
        {
            return typeReturned.Name == "IQueryable`1" && typeReturned.GenericTypeArguments.Length > 0;
        }

        private Type GetTypeOrInnerType(Type typeReturned)
        {
            if (typeReturned.GenericTypeArguments.Length > 0)
                typeReturned = typeReturned.GenericTypeArguments[0];

            return typeReturned;
        }

        private void SaveChangesDescription(HelpPageApiModel apiModel)
        {
            var types = GetReturnedTypes(apiModel);
            if (types == null || types.Count() == 0)
                return;

            var type = types.First();

            int id = 1;

            #region sampleRequest

            string sampleRequest = @"{
    ""entities"":
    [
{classes}
    ],
    ""saveOptions"":{}
}";
            #endregion

            #region foreach in types

            string sampleClasses = @"        {
            {entitySampleUpd},
            ""entityAspect"":
            {
                {entityTypeName}
                ""entityState"":""Modified"",
                ""originalValuesMap"":{{entitySampleOld}},
                ""autoGeneratedKey"":{{identity}}
            }
        },
        {
            {entitySampleDel},
            ""entityAspect"":
            {
                {entityTypeName}
                ""entityState"":""Deleted"",
                ""originalValuesMap"":{},
                ""autoGeneratedKey"":{{identity}}
            }
        },
        {
            {entitySampleIns},
            ""entityAspect"":
            {
                {entityTypeName}
                ""entityState"":""Added"",
                ""originalValuesMap"":{},
                ""autoGeneratedKey"":{{identity}}
            }
        },
";
            sampleClasses = sampleClasses.Replace("{entitySampleOld}", GetSampleByType(type, id));
            sampleClasses = sampleClasses.Replace("{entitySampleUpd}", GetSampleByType(type, id++));
            sampleClasses = sampleClasses.Replace("{entitySampleDel}", GetSampleByType(type, id++));
            sampleClasses = sampleClasses.Replace("{entitySampleIns}", GetSampleByType(type, -1));
            sampleClasses = sampleClasses.Replace("{entityTypeName}", string.Format("\"entityTypeName\":\"{0}:#{1}\",", type.Name, type.Namespace));
            sampleClasses = sampleClasses.Replace("{identity}", GetAutoGeneratedKey(type));

            #endregion

            sampleRequest = sampleRequest.Replace("{classes}", sampleClasses);

            apiModel.SampleRequests[JsonApplication] = sampleRequest.ToTextSample();
            apiModel.SampleRequests[JsonText] = sampleRequest.ToTextSample();
            apiModel.SampleResponses[JsonApplication] = string.Empty;
            apiModel.SampleResponses[JsonText] = string.Empty;

            #region Add Type Information
            foreach (var t in types)
                apiModel.AdditionalInformation["Entity Name: " + t.Name] = GetInformationType(t);

            #endregion
        }

        private TextSample GetInformationType(Type t)
        {
            string properties = "Properties: \r\n";

            foreach (var property in t.GetProperties().Where(p => !IgnoreProperty(p)))
            {
                properties += string.Format("   {0} \t({1}) \r\n", property.Name, GetDescriptionType(property));
            }

            return properties.ToTextSample();
        }

        private string GetDescriptionType(PropertyInfo property)
        {
            return (property.PropertyType.GenericTypeArguments.Length > 0 ? 
                property.PropertyType.Name + "-" + property.PropertyType.GenericTypeArguments.First().Name : 
                property.PropertyType.Name);
        }

        private bool IgnoreProperty(PropertyInfo p)
        {
            return
                p.GetCustomAttribute<IgnoreDataMemberAttribute>() != null ||
                p.GetCustomAttribute<XmlIgnoreAttribute>() != null ||
                (p.GetCustomAttribute<DataMemberAttribute>() != null &&
                    p.GetCustomAttribute<DataMemberAttribute>().Name == "EntityKeyLocalRelation");
        }

        private IEnumerable<Type> GetReturnedTypes(HelpPageApiModel apiModel)
        {
            var types = apiModel.ApiDescription
                .ActionDescriptor
                .ControllerDescriptor
                .ControllerType.GetMethods()
                .Where(m => m.Name.StartsWith("Get") && m.Name.EndsWith("ByEntitySearchNoAssociations"))
                .Select(m => GetTypeOrInnerType(m.ReturnType));

            if (types.Count() > 0)
                return types;

            types = apiModel.ApiDescription
                .ActionDescriptor
                .ControllerDescriptor
                .ControllerType.GetMethods()
                .Where(m => IsGenericType(m.ReturnType))
                .Select(m => GetTypeOrInnerType(m.ReturnType));

            return types;
        }

        private string GetAutoGeneratedKey(Type type)
        {
            string autoGenerated = string.Empty;
            if (type == null) return autoGenerated;
            var properties = type.GetProperties();
            if (properties == null || properties.Length == 0) return autoGenerated;

            foreach (var p in properties)
            {
                if (IsIdentity(p))
                {
                    autoGenerated = "\"propertyName\":\"" + p.Name + "\",\"autoGeneratedKeyType\":\"Identity\"";
                    break;
                }
            }
            return autoGenerated;
        }

        private string GetSampleByType(Type type, int id)
        {
            List<string> propSample = new List<string>();

            if (type == null) return string.Empty;
            var properties = type.GetProperties()
                .Where(p => !IgnoreProperty(p))
                .ToArray();
            if (properties == null || properties.Length == 0) return string.Empty;

            foreach (var p in properties)
            {
                if (IsIdentity(p))
                {
                    propSample.Add(string.Format("\"{0}\":\"{1}\"", p.Name, id.ToString()));
                }
                else
                {
                    var s = GetJsonSampleSaveChanges(p.PropertyType);
                    if (s.IsNullOrEmpty()) continue;
                    propSample.Add(string.Format("\"{0}\":{1}", p.Name, s));
                }
            }

            return string.Join(", ", propSample);
        }

        private bool IsIdentity(PropertyInfo pInf)
        {
            var funcP = pInf.GetCustomAttribute<FunctionalPoint>();
            return funcP != null && IsAutomaticSequencyProperty(funcP.FunctionName);

        }

        private bool IsAutomaticSequencyProperty(string functionalPoint)
        {
            return !string.IsNullOrEmpty(functionalPoint) &&
                functionalPoint.ToLower().Contains("IsAutomaticSequency[true]".ToLower());

        }

        #endregion
    }
}