using System;
using System.Web;
using System.Linq;
using Microsoft.Web.Infrastructure;
using System.Web.Security;
using NLog;
using System.Configuration;
using WebActivatorEx;
using ImageResizer.Plugins.SqlReader;

[assembly: PostApplicationStartMethod(typeof(Linx.ImageService.Web.PostStart_Config404), "PostStart", Order = 98)]


namespace Linx.ImageService.Web  
{
    public static class PostStart_Config404
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void PostStart()
        {
            Logger.Info("Configurando o plugin 'Config404'");
            ImageResizer.Configuration.Config.Current.Pipeline.RewriteDefaults +=

              delegate(IHttpModule m, HttpContext c, ImageResizer.Configuration.IUrlEventArgs args)
              {
                  if (args.VirtualPath.IndexOf("no-image.png", StringComparison.OrdinalIgnoreCase) > -1)
                      return;

                  if (args.VirtualPath.IndexOf(".png", StringComparison.OrdinalIgnoreCase) > -1
                    || args.VirtualPath.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) > -1
                    || args.VirtualPath.IndexOf(".gif", StringComparison.OrdinalIgnoreCase) > -1)
                  {
                      args.QueryString["404"] = string.Concat("~/images/no-image.png");
                  }

              };
        }

    }   
}

