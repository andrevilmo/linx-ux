using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.ComponentModel;
using Telerik.Reporting;

namespace Linx.Business.Tools
{
    public class LinxReportHelper
    {
        [Telerik.Reporting.Expressions.Function(Category = "Linx", Namespace = "Medias", Name = "GetMediaPath")]
        public static string GetMediaPath(string tableName, string primaryKey, object sender, object rawData)
        {
            if (tableName.IsNullOrEmpty()) throw new ArgumentNullException("tableName");
            if (primaryKey.IsNullOrEmpty()) throw new ArgumentNullException("primaryKey");
            if (sender.IsNull()) throw new ArgumentNullException("sender");
            if (rawData.IsNull()) throw new ArgumentNullException("rawData");

            var mediaPath = string.Empty;

            if (sender != null)
            {
                var report = ((Telerik.Reporting.Processing.ProcessingElement)sender).Report;
                if (report.IsNull()) throw new InvalidCastException("Cast is not valid Telerik.Reporting.Processing.ProcessingElement");


                var headers = LinxReportHelper.GetRequestHeaders(report);
                if (headers.IsNull()) throw new NullReferenceException("Not found Headers in Report");


                if (report.Parameters["ServiceBusUrl"].IsNull() || report.Parameters["ServiceBusUrl"].Value.IsNull()) throw new ArgumentOutOfRangeException("Not found [ServiceBusUrl] in Parameters");
                var baseAddresMultimidia = string.Format("{0}/LinxFrameworkMultimidia/",
                                report.Parameters["ServiceBusUrl"].Value.ToString().TrimEnd('/'));


                if (rawData.GetPropertyValue(primaryKey) == null) throw new ArgumentOutOfRangeException("Not found property [" + primaryKey + "] in the report");
                var currentKey = rawData.GetPropertyValue(primaryKey).ToString();


                var intKey = (currentKey.IsNumeric()) ? currentKey : string.Empty;
                var guidKey = (!currentKey.IsNumeric()) ? currentKey : string.Empty;

                var action = string.Format("getMediaThumbnailByKey?nomeTabela={0}" +
                                            "&idChave={1}" +
                                            "&uidChave={2} " +
                                            "&uidGrupoAcesso={3}" +
                                            "&uidEmpresa={4}" +
                                            "&uidGrupoEconomico={5}" +
                                            "&idAmbiente={6}" +
                                            "&uidUsuario={7}",
                                            tableName,
                                            intKey,
                                            guidKey,
                                            headers["uidGrupoAcesso"], headers["uidEmpresa"], headers["uidGrupoEconomico"], headers["idAmbiente"], headers["uidUsuario"]);

                mediaPath = string.Concat(baseAddresMultimidia, action);
            }

            return mediaPath;
        }

        public static Dictionary<string, string> GetRequestHeaders(Telerik.Reporting.Processing.Report report)
        {
            var headers = default(Dictionary<string, string>);

            if (report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                headers = LinxReportHelper.GetMediatHeaders(report);
            }
            else
            {
                if ((!report.Parameters.ContainsKey("Username") || report.Parameters["Username"].Value.IsNull()) ||
                   (!report.Parameters.ContainsKey("Password") || report.Parameters["Password"].Value.IsNull()) ||
                   (!report.Parameters.ContainsKey("ServiceBusUrl") || report.Parameters["ServiceBusUrl"].Value.IsNull()))
                    throw new Exception("Telerik Report parameters not found.");

                var userName = report.Parameters["Username"].Value.ToString();
                var password = report.Parameters["Password"].Value.ToString();

                var serviceBusUrl = string.Format("{0}/LinxFrameworkHelpers/",
                    report.Parameters["ServiceBusUrl"].Value.ToString().TrimEnd('/'));

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serviceBusUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password))));

                    using (var response = client.GetAsync("GetMediaHeaders").Result)
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        headers = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                    }
                }
            }

            return headers;
        }

        public static Dictionary<string, string> GetMediatHeaders(Telerik.Reporting.Processing.Report report)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            if (!report.Parameters.IsNull() && report.Parameters.Count() > 0)
            {
                headers.Add("uidGrupoAcesso", report.Parameters["AccessGroup"].Value as string);
                headers.Add("uidEmpresa", report.Parameters["CurrentCompany"].Value as string);
                headers.Add("uidGrupoEconomico", report.Parameters["EconomicGroup"].Value as string);
                headers.Add("idAmbiente", report.Parameters["Environment"].Value as string);
                headers.Add("uidUsuario", report.Parameters["CurrentUser"].Value as string);

            }

            return headers;
        }

        public static Dictionary<string, string> GetReportHeaders(IDictionary<string, Telerik.Reporting.Processing.Parameter> reportParameters)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            if (!reportParameters.IsNull() && reportParameters.Count() > 0)
            {
                headers.Add("CurrentUser", reportParameters["CurrentUser"].Value as string);
                headers.Add("CurrentCompany", reportParameters["CurrentCompany"].Value as string);
                headers.Add("AuthorizationToken", reportParameters["AuthorizationToken"].Value as string);
                headers.Add("TransactionInfo", reportParameters["TransactionInfo"].Value as string);
                headers.Add("Application", reportParameters["Application"].Value as string);
                headers.Add("AccessGroup", reportParameters["AccessGroup"].Value as string);
                headers.Add("EconomicGroup", reportParameters["EconomicGroup"].Value as string);
                headers.Add("Environment", reportParameters["Environment"].Value as string);

                if (reportParameters.ContainsKey("Branch"))
                    headers.Add("Branch", reportParameters["Branch"].Value as string);

                if (reportParameters.ContainsKey("LoginMode"))
                    headers.Add("LoginMode", reportParameters["LoginMode"].Value as string);

            }

            return headers;
        }

        public static List<Filter> GetReportFilters(Telerik.Reporting.Processing.Report report)
        {
            var filters = new List<Filter>();

            var detailsSectionCollection = report.ItemDefinition.Items.Find(typeof(DetailSection));

            foreach (var itemDetail in detailsSectionCollection.SelectMany(detailSectionItem => detailSectionItem.Items))
            {
                if (itemDetail is Graph)
                    filters.AddRange(((Graph)(itemDetail)).Filters);
                else if (itemDetail is Table)
                    filters.AddRange(((Table)(itemDetail)).Filters);
                else if (itemDetail is Map)
                    filters.AddRange(((Map)(itemDetail)).Filters);
            }

            return filters;
        }

        public static string ConvertFilterToJExpression(List<Filter> filters, Type entity)
        {
            var jqueryExpression = String.Empty;
            string entityName = entity.Name.Replace("ParentComposition", "");

            //Inserting Entity Name
            jqueryExpression += " " + entityName + "{";
            foreach (var filter in filters)
            {
                //Inserting Field
                var field = filter.Expression.Replace("Fields.", "").Replace("=", "");
                jqueryExpression += field + "#";
                //Inserting Operator
                jqueryExpression += GetEnumDescription<FilterOperator>(filter.Operator.ToString()) + "#";
                //InsertingValue
                var JExpressionDataType = EntitySearch.ParseJDataType(entity.GetProperty(field.Trim()).PropertyType.FullName);
                if (!Equals(filter, filters.Last()))
                    jqueryExpression += JExpressionDataType + filter.Value.Replace("=", "").Replace("\"", "") + ";&&";
                else
                    jqueryExpression += JExpressionDataType + filter.Value.Replace("=", "");
            }

            jqueryExpression += "}";

            return jqueryExpression;
        }

        private static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (((DescriptionAttribute)customAttribute[0]).Description == "=")
                return "==";
            else
                return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}
