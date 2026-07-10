



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBusinessCleaner
{
    public class DataFilesHelper
    {		
        public static string[] BusinessModelFiles { get {


                return new string[] {
					
                "linx.framework.autorizacao.bm.dll"
                , "linx.framework.autorizacao.bm.dll.config"
                , "linx.framework.autorizacao.bm.dll.meta.json"
                , "linx.framework.controlesistema.bm.dll"
                , "linx.framework.controlesistema.bm.dll.config"
                , "linx.framework.controlesistema.bm.dll.meta.json"
                , "linx.framework.domains.bm.dll"
                , "linx.framework.domains.bm.dll.config"
                , "linx.framework.domains.bm.dll.meta.json"
                , "linx.framework.loja.bm.dll"
                , "linx.framework.loja.bm.dll.config"
                , "linx.framework.loja.bm.dll.meta.json"
                };

         } }


        public static string[] BusinessViewFiles
        {
            get
            {

                return new string[] {
					
                "linx.business.common.dll"
                , "linx.business.tools.dll"
                , "linx.framework.bv.dll"
                , "linx.framework.bv.dll.meta.json"
                , "linx.report.access.bv.dll"
                , "linx.framework.custom.bv.dll"
                , "linx.framework.custom.bv.dll.meta.json"

                };

            }
        }



        public static string[] ServiceBusFiles
        {
            get
            {

                return new string[] {
					
                "breeze.contextprovider.dll"
                , "breeze.webapi2.dll"
                , "castle.core.dll"
                , "documentformat.openxml.dll"
                , "entityframework.dll"
                , "entityframework.sqlserver.dll"
                , "grc.core.dll"
                , "icsharpcode.sharpziplib.dll"
                , "imageresizer.dll"
                , "interactivepregeneratedviews.dll"
                , "ionic.zip.dll"
                , "linx.business.common.dll"
                , "linx.business.tools.dll"
                , "linx.data.dll"
                , "linx.dataservice.dll"
                , "linx.framework.autorizacao.bm.dll"
                , "linx.framework.autorizacao.bm.dll.config"
                , "linx.framework.bv.dll"
                , "linx.framework.bv.implementations.dll"
                , "linx.framework.bv.reports.dll"
                , "linx.framework.bv.webapi.dll"
                , "linx.framework.bv.webapi.ds.dll"
                , "linx.framework.bv.dll.meta.json"
                , "linx.framework.controlesistema.bm.dll"
                , "linx.framework.controlesistema.bm.dll.config"
                , "linx.framework.custom.bv.dll"
                , "linx.framework.custom.bv.reports.dll"
                , "linx.framework.custom.bv.webapi.ds.dll"
                , "linx.framework.custom.bv.dll.meta.json"
                , "linx.framework.domains.bm.dll"
                , "linx.framework.domains.bm.dll.config"
                , "linx.framework.loja.bm.dll"
                , "linx.linqextensions.dll"
                , "linx.olapproxy.service.dll"
                , "linx.report.access.bv.dll"
                , "linx.report.access.bv.reports.dll"
                , "linx.report.access.bv.telerikreport.webapi.ds.dll"
                , "linx.report.access.bv.webapi.ds.dll"
                , "linx.resources.localization.dll"
                , "linx.servicebus.starter.dll"
                , "linx.tools.dll"
                , "linxhttpcontext.dll"
                , "messagingtoolkit.qrcode.dll"
                , "microsoft.analysisservices.adomdclient.dll"
                , "microsoft.applicationserver.caching.client.dll"
                , "microsoft.applicationserver.caching.core.dll"
                , "microsoft.data.edm.dll"
                , "microsoft.data.edm.xml"
                , "microsoft.data.odata.dll"
                , "microsoft.data.odata.xml"
                , "microsoft.practices.servicelocation.dll"
                , "microsoft.practices.servicelocation.xml"
                , "microsoft.servicemodel.domainservices.hosting.dll"
                , "microsoft.web.infrastructure.dll"
                , "microsoft.windowsfabric.common.dll"
                , "microsoft.windowsfabric.data.common.dll"
                , "miniprofiler.dll"
                , "miniprofiler.entityframework6.dll"
                , "monagentlistener.dll"
                , "mysql.data.dll"
                , "newtonsoft.json.dll"
                , "newtonsoft.json.xml"
                , "restsharp.dll"
                , "stackexchange.redis.strongname.dll"
                , "structuremap.dll"
                , "structuremap.net4.dll"
                , "system.data.sqlite.dll"
                , "system.data.sqlite.ef6.dll"
                , "system.data.sqlite.linq.dll"
                , "system.net.http.formatting.dll"
                , "system.net.http.formatting.xml"
                , "system.servicemodel.dll"
                , "system.servicemodel.domainservices.hosting.dll"
                , "system.servicemodel.domainservices.server.dll"
                , "system.spatial.dll"
                , "system.spatial.xml"
                , "system.web.helpers.dll"
                , "system.web.http.dll"
                , "system.web.http.odata.dll"
                , "system.web.http.odata.xml"
                , "system.web.http.webhost.dll"
                , "system.web.http.webhost.xml"
                , "system.web.http.xml"
                , "system.web.mvc.dll"
                , "system.web.razor.dll"
                , "system.web.webpages.deployment.dll"
                , "system.web.webpages.dll"
                , "system.web.webpages.razor.dll"
                , "telerik.openaccess.35.extensions.dll"
                , "telerik.openaccess.dll"
                , "telerik.openaccess.runtime.dll"
                , "telerik.reporting.cache.database.dll"
                , "telerik.reporting.dll"
                , "telerik.reporting.service.dll"
                , "telerik.reporting.services.webapi.dll"
                , "webactivatorex.dll"

                };

            }
        }



		public static string[] UserInterfaceFiles
        {
            get
            {

                return new string[] {
					
                "linx.framework.bv.spa.dll"
                , "linx.framework.custom.bv.spa.dll"
                };

            }
        }

		public static string[] WebApiFiles
        {
            get
            {

                return new string[] {
					
                };

            }
        }

		public static string[] WebApiClientFiles
        {
            get
            {

                return new string[] {
					

                };

            }
        }

		public static string[] NugetBMFiles
        {
            get
            {

                return new string[] {
					

                };

            }
        }

		public static string[] NugetBvFiles
        {
            get
            {

                return new string[] {
					

                };

            }
        }

		public static CleanerPaths GetDirectories() {
			
			return new CleanerPaths()
            {
                BusinessModelPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Business Model",
                BusinessViewPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Business View",
                ServiceBusPath = @"c:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Service\bin",

				UserInterfacePath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\User Interface",
				WebApiPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Web API",
				WebApiClientPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Web API Client",
				NugetBMPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Common\Linx\Nuget\BM",
				NugetBvPath = @"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Common\Linx\Nuget\BV"

            };
		
		}
    }

	public class CleanerPaths
    {
        public string BusinessModelPath { get; set; }
        public string BusinessViewPath { get; set; }
        public string ServiceBusPath { get; set; }

		public string UserInterfacePath { get; set; }
		public string WebApiPath { get; set; }
		public string WebApiClientPath { get; set; }
		public string NugetBMPath { get; set; }
		public string NugetBvPath { get; set; }
    }
}

