using Linx.OlapProxy.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Linx.OlapProxy.Service.Helpers
{
    internal class SOAPEnvelopeHelper
    {
        public string GetSOAPRequest(OlapProxyRequest olapProxyRequest)
        {
            var inputStream = HttpContext.Current.Request.InputStream;
            inputStream.Seek(0, SeekOrigin.Begin);

            using (var streamReader = new StreamReader(inputStream))
                return this.ParseRequest(olapProxyRequest, streamReader.ReadToEnd());
        }

        public string GetSOAPResponse(string response)
        {
            return response;
        }

        private string ParseRequest(OlapProxyRequest olapProxyRequest, string request)
        {
            var requestParsed = string.Empty;

            if (!string.IsNullOrEmpty(request))
            {                
                var soapEnvelope = new XmlDocument();
                var mdxCommandHelper = new MDXCommandHelper();

                soapEnvelope.LoadXml(request);

                this.SetCatalog(soapEnvelope);
                this.SetDataSourceInfo(soapEnvelope);

                mdxCommandHelper.SetMDXConditions(soapEnvelope, olapProxyRequest);

                requestParsed = soapEnvelope.OuterXml;
            }

            return requestParsed;
        }

        private void SetCatalog(XmlDocument document)
        {
            var catalogNodes = document.GetElementsByTagName("Catalog");
            var catalogNamerNodes = document.GetElementsByTagName("CATALOG_NAME");

            foreach (XmlElement item in catalogNodes)
                item.InnerText = LinxParametersHelper.OlapCatalog;

            foreach (XmlElement item in catalogNamerNodes)
                item.InnerText = LinxParametersHelper.OlapCatalog;
        }

        private void SetDataSourceInfo(XmlDocument document)
        {
            var dataSourceInfoNodes = document.GetElementsByTagName("DataSourceInfo");

            foreach (XmlElement item in dataSourceInfoNodes)
                item.InnerText = LinxParametersHelper.DataSourceInfo;
        }
    }
}