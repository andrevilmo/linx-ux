using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Linx.Internet.Application.Models;
using Linx.Internet.Application.Framework.Web;
using System.Linq;

namespace Linx.Internet.Application.Hubs
{
    public class VersionHub : Hub
    {
        public override Task OnConnected()
        {
            this.CheckVersion();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            this.CheckVersion();
            return base.OnReconnected();
        }

        private void CheckVersion()
        {
            //var clientVersion = Context.QueryString["clientVersion"];
            //var serverVersion = Linx.Internet.Application.Helpers.BaseHelpers.NumeroVersao;

            List<dynamic> routesversion = new List<dynamic>();

            foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
            {
                var moduleItem = new
                {
                    moduleUId = module.Value.ModuleUId.ToString(),
                    moduleId = string.Concat("pkg_", module.Value.ModuleName),
                    moduleName = module.Value.ModuleName,

                    assemblyName = module.Value.AssemblyName,
                    assemblyType = module.Value.AssemblyType,
                    assemblyVersion = module.Value.AssemblyVersion,
                    assemblyVersionFormated = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).ToLower(),
                    requireId = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).Replace(".", "-").ToLower(),
                    shellAssemblyVersion = module.Value.ShellAssemblyVersion,
                        
                    buildDate = module.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"),
                    CRC32 = module.Value.CRC32
                };
                
                routesversion.Add(moduleItem);
            }

            Clients.Caller.clientCheckVersion(routesversion);
        }
    }
}
