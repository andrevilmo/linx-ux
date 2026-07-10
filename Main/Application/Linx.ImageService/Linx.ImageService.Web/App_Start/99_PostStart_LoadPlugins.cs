using System;
using System.Web;
using System.Linq;
using Microsoft.Web.Infrastructure;
using System.Web.Security;
using NLog;
using System.Configuration;
using WebActivatorEx;
using ImageResizer.Plugins.SqlReader;

[assembly: PostApplicationStartMethod(typeof(Linx.ImageService.Web.PostStart_LoadPlugins), "PostStart", Order = 99)]


namespace Linx.ImageService.Web  
{
    public static class PostStart_LoadPlugins
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void PostStart()
        {
            Logger.Info("Finalizando a configuração dos plugins");

            //Add access control
            ImageResizer.Configuration.Config.Current.Plugins.LoadPlugins();
            
            //ImageResizer.Configuration.Config.Current.Plugins.Get<SqlReaderPlugin>().Settings.BeforeAccess += delegate(string id)
            //{
            //    bool allowed = true;
            //    //INSERT HERE: execute query or whatever to check authorization to view this files
            //    //  SqlParameter pId = Config.Current.Plugins.Get<SqlReaderPlugin>().CreateIdParameter(id);
            //    // 
            //    if (HttpContext.Current.Request.QueryString["denyme"] != null) allowed = false;
            //    //END pretend code

            //    if (!allowed) throw new HttpException(403, "Access denied to this resource.");
            //};
        }

    }   
}

