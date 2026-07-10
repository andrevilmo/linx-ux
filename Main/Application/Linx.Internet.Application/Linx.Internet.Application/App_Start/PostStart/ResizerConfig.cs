using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Helpers;
using NLog;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Linx.Internet.Application.App_Start.ResizerConfig), "PostStart", Order = 3)]

namespace Linx.Internet.Application.App_Start
{
    public static class ResizerConfig
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void PostStart()
        {
            Logger.Info("Inicio da configuração 'ImageResizer'");
            ImageResizer.Configuration.Config.Current.Pipeline.RewriteDefaults +=

              delegate(IHttpModule m, HttpContext c, ImageResizer.Configuration.IUrlEventArgs args)
              {
                  if (args.VirtualPath.IndexOf("no-image.png", StringComparison.OrdinalIgnoreCase) > -1)
                      return;

                  if (args.VirtualPath.IndexOf(".png", StringComparison.OrdinalIgnoreCase) > -1
                    || args.VirtualPath.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) > -1
                    || args.VirtualPath.IndexOf(".gif", StringComparison.OrdinalIgnoreCase) > -1)
                  {
                      args.QueryString["404"] = string.Concat("~/linx-internet-application", "/", BaseHelpers.NumeroVersaoURL, "/lib/linx/img/no-image.png");
                  }

              };
        }
    }
}