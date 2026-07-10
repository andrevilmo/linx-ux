using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Linx.License.Client
{
    public static class WebResponseExt
    {
        private static WebResponse GetResponseNoException(this WebRequest req)
        {
            try
            {
                return req.GetResponse();
            }
            catch (WebException we)
            {
                var resp = we.Response as HttpWebResponse;
                if (resp == null)
                    throw;
                return resp;
            }
        }
        
        public static T Get<T>(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 5000;
            request.Proxy = null;
            request.ContentType = "application/json";
            WebResponse postResponse = request.GetResponseNoException();
            string responseBody = "";
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(postResponse.GetResponseStream(), encoding))
            {
                responseBody = reader.ReadToEnd();
            }
            //Check Error
            VerifyError(responseBody);
            //Return result
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public static T Post<T>(string url, object dataObject)
        {
            WebRequest request = WebRequest.Create(url);
            string data = JsonConvert.SerializeObject(dataObject);
            request.Method = "POST";
            request.Timeout = 5000;
            request.Proxy = null;
            request.ContentType = "application/json";
            
            // Set the content length of the string being posted.
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteContent = encoding.GetBytes(data);            
            request.ContentLength = byteContent.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(byteContent, 0, byteContent.Length);
            }

            WebResponse postResponse = request.GetResponseNoException();
            string responseBody = "";            
            using (var reader = new System.IO.StreamReader(postResponse.GetResponseStream(), encoding))
            {
                responseBody = reader.ReadToEnd();
            }
            //Check Error
            VerifyError(responseBody);
            //Return result
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public static string PostNoSerialize(string url, string data)
        {
            WebRequest request = WebRequest.Create(url);            
            request.Method = "POST";
            request.Timeout = 5000;
            request.Proxy = null;
            request.ContentType = "application/json";

            // Set the content length of the string being posted.
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteContent = encoding.GetBytes(data);
            request.ContentLength = byteContent.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(byteContent, 0, byteContent.Length);
            }

            WebResponse postResponse = request.GetResponseNoException();
            string responseBody = "";
            using (var reader = new System.IO.StreamReader(postResponse.GetResponseStream(), encoding))
            {
                responseBody = reader.ReadToEnd();
            }
            //Check Error
            VerifyError(responseBody);
            //Return result
            return responseBody;
        }

        public static string Post(string url, object dataObject)
        {
            WebRequest request = WebRequest.Create(url);
            string data = JsonConvert.SerializeObject(dataObject);
            request.Method = "POST";
            request.Timeout = 5000;
            request.Proxy = null;
            request.ContentType = "application/json";

            // Set the content length of the string being posted.
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteContent = encoding.GetBytes(data);
            request.ContentLength = byteContent.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(byteContent, 0, byteContent.Length);
            }

            WebResponse postResponse = request.GetResponseNoException();
            string responseBody = "";
            using (var reader = new System.IO.StreamReader(postResponse.GetResponseStream(), encoding))
            {
                responseBody = reader.ReadToEnd();
            }
            //Check Error
            VerifyError(responseBody);
            //Return result
            return responseBody;
        }

        private static void VerifyError(string responseBody)
        {
            if (!String.IsNullOrWhiteSpace(responseBody) && responseBody.Contains("System.Web.Http.HttpError, System.Web.Http"))
            {
                throw new Exception(responseBody.Extract("\"ExceptionMessage\":\"", "\""));
            }
        }

        private static string Extract(this string value, string searchBegin, string searchEnd)
        {
            int indexStart, indexEnd;

            indexStart = value.IndexOf(searchBegin, StringComparison.CurrentCultureIgnoreCase);

            if (indexStart < 0)
                return "";

            indexEnd = value.IndexOf(searchEnd, indexStart + searchBegin.Length, StringComparison.CurrentCultureIgnoreCase);

            if (indexEnd < 0)
                return "";

            if (!(indexStart >= 0 && indexEnd >= 0 && indexEnd > indexStart))
                return "";

            return value.Substring(indexStart + searchBegin.Length, indexEnd - indexStart - searchBegin.Length);

        }
    }

}
