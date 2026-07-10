using Linx.Internet.Application.Framework.Web;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMarkupMin.Core;
using WebMarkupMin.Core.Minifiers;
using Linx.Internet.Application;
using System.Configuration;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Linx.Internet.Application.App_Start.MinifyConfig), "PostStart", Order = 2)]

namespace Linx.Internet.Application.App_Start
{
    public static class MinifyConfig
    {
        //private static PluginConfig plugins = null;
        public static void PostStart()
        {
            return;

            //if (ConfigurationManager.AppSettings.GetValue<bool>("Shell.CombineAndMinifyCssJs.Enabled", false) == false)
            //    return; 
            
            //IJsMinifier jsMinifier = WebMarkupMinContext.Current.Code.CreateDefaultJsMinifierInstance();
            //var htmlMinifier = new HtmlMinifier();

            //foreach (var item in Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources)
            //{
            //    // verifique se o arquivo e |JS e nao tem MIN no nome
            //    if (item.Value.Extension == ".js" && item.Value.FullPath.Contains(".min.") == false)
            //    {
            //        using (var ms = new MemoryStream(item.Value.Bytes, false))
            //        {
            //            using (var sw = new StreamReader(ms, System.Text.Encoding.UTF8))
            //            {
            //                // minifica JS o arquivo em memoria
            //                var result = jsMinifier.Minify(sw.ReadToEnd(), false, System.Text.Encoding.UTF8);

            //                if (result.Errors.Count == 0)
            //                {
            //                    // copiar o arquivo minificado para o repositorio de arquivos "embedados"
            //                    item.Value.Bytes = System.Text.Encoding.UTF8.GetBytes(result.MinifiedContent);
            //                }
            //            }
            //        }
            //    }
            //    //else if (item.Value.Extension == ".html")
            //    //{
            //    //    using (var ms = new MemoryStream(item.Value.Bytes, false))
            //    //    {
            //    //        using (var sw = new StreamReader(ms, System.Text.Encoding.UTF8))
            //    //        {
            //    //            var result = htmlMinifier.Minify(sw.ReadToEnd(), generateStatistics: false);

            //    //            if (result.Errors.Count == 0)
            //    //            {
            //    //                item.Value.Bytes = System.Text.Encoding.UTF8.GetBytes(result.MinifiedContent);
            //    //            }
            //    //        }
            //    //    }

            //    //}

            //}
        }
    }
}