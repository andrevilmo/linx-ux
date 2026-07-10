//using System;
//using System.Web;
//using System.Linq;
//using Microsoft.Web.Infrastructure;
//using System.Web.Security;
//using NLog;
//using System.Configuration;
//using WebActivatorEx;
//using ImageResizer.Plugins.SqlReader;
//using System.Collections.Specialized;

//[assembly: PostApplicationStartMethod(typeof(Linx.ImageService.Web.PostStart_AzureReader2Plugin), "PostStart", Order = 12)]


//namespace Linx.ImageService.Web  
//{
//    public static class PostStart_AzureReader2Plugin
//    {
//        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

//        public static void PostStart()
//        {
//            Logger.Info("Configurando o plugin 'AzureReader2Plugin'");

//            NameValueCollection args1 = new NameValueCollection();
//            args1.Add("connectionstring", "DefaultEndpointsProtocol=http;AccountName=linxb2c001site001;AccountKey=VeSgXEVNmg3gY88xPpBSfBYqnN/J7+1BW8/EKxNEbiB5EDZPIxt10/ZK/GStZ2p4VPYLbQFB58EBo0REoJGabQ==");
//            args1.Add("endpoint", "http://linxb2c001site001.blob.core.windows.net/");
//            args1.Add("prefix", "blob001");
//            args1.Add("lazyExistenceCheck", "true");
//            args1.Add("vpp", "true");
//            ImageResizer.Plugins.AzureReader2.AzureReader2Plugin pluginAzureReader = new ImageResizer.Plugins.AzureReader2.AzureReader2Plugin(args1);
//            pluginAzureReader.Install(ImageResizer.Configuration.Config.Current);

//            NameValueCollection args2 = new NameValueCollection();
//            args2.Add("connectionstring", "DefaultEndpointsProtocol=http;AccountName=linxb2c109;AccountKey=ztgVlXVURgky9KresPHCFRp1k/nfe288rkzDXHvGrcGcWVE4YQp5hgkI+kVsXgrOrNrlTCxDcgLTgKQdBNf19Q==");
//            args2.Add("endpoint", "http://http://linxb2c109.blob.core.windows.net/");
//            args2.Add("prefix", "blob002");
//            args2.Add("lazyExistenceCheck", "true");
//            args2.Add("vpp", "true");
//            ImageResizer.Plugins.AzureReader2.AzureReader2Plugin pluginAzureReader2 = new ImageResizer.Plugins.AzureReader2.AzureReader2Plugin(args2);
//            pluginAzureReader2.Install(ImageResizer.Configuration.Config.Current);
//        }

//    }   
//}

