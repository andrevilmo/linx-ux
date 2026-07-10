using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using System;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Hosting;

namespace Linx.License.Client.WebAPICore.App_Start
{

    public class ModuleInitializer_Licenciamento : IModuleInitializer
    {
        
        private static IHostingEnvironment _hostingEnvironment;
        public static string MapPath(string complement)
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            return System.IO.Path.Combine(webRoot, complement);
        }
        
        public void Init(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            services.AddOData();
        }
        
        public void Init(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices;
            _model = GetEdmModel(_serviceProvider.GetRequiredService<IAssemblyProvider>());
            app.UseMvc(builder => {
                builder.MapODataRoute("Licenciamento", _model);
            });
        }
        
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider { get { return _serviceProvider; } }
        private static IEdmModel _model;
        public static IEdmModel Model
        {
            get
            {
                return _model;
            }
        }
        
        private static IEdmModel GetEdmModel(IAssemblyProvider assemblyProvider)
        {
            var modelBuilder = new ODataConventionModelBuilder(assemblyProvider);
            var model = modelBuilder.GetEdmModel();
            return model;
        }
    }
}
