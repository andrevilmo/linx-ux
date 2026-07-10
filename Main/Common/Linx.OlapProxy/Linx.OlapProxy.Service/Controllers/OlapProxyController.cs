using Linx.OlapProxy.Service.Helpers;
using Linx.OlapProxy.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Web.Http;
using System.Xml;

namespace Linx.OlapProxy.Service.Controllers
{
    public class OlapProxyController : ApiController
    {
        public HttpResponseMessage Post(string currentBrands, string allowedBrands, string jEntitySearch)
        {
            var soapEnvelopeHelper = new SOAPEnvelopeHelper();
            var olapProxyRequest = new OlapProxyRequest(currentBrands, allowedBrands, jEntitySearch);

            var soapRequest = soapEnvelopeHelper.GetSOAPRequest(olapProxyRequest);

            using (var client = new WebClient())
            {
                var soapResponse = client.UploadData(
                    LinxParametersHelper.OlapServiceUri,
                    WebRequestMethods.Http.Post,
                    Encoding.ASCII.GetBytes(soapRequest)
                );

                return new HttpResponseMessage()
                {
                    Content = new StringContent(
                        Encoding.ASCII.GetString(soapResponse),
                        Encoding.UTF8,
                        MediaTypeNames.Text.Plain
                    )
                };
            }
        }
    }
}
