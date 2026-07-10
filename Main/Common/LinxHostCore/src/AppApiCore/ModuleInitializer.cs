using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Hosting;
using Linx.Security.Core.Authorization;

namespace AppApiCore.Modules
{
    public class ModuleInitializer : IModuleInitializer
    {
        private static IHostingEnvironment _hostingEnvironment;

        public static string MapPath(string complement)
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            return System.IO.Path.Combine(webRoot, complement);
        }

        public void Init(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            //services.AddTransient<IAnotherTestService, AnotherTestService>();
            //services.AddTransient<ITestService, TestService>();

            _hostingEnvironment = hostingEnvironment;

            services.AddOData(options =>
            {
                options.RoutingConventions.Insert(0, new CustomRoutingConvention());
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("UpdateAuth", policy => policy.Requirements.Add(new ApiAuthorizationRequirement(new string[] { "controller1", "controller2", "controller3" })));
            });

            Console.WriteLine("AppApiCore initialized!");
        }

        public void Init(IApplicationBuilder app)
        {
            //services.AddTransient<IAnotherTestService, AnotherTestService>();
            //services.AddTransient<ITestService, TestService>();

            _serviceProvider = app.ApplicationServices;

            _model = GetEdmModel(_serviceProvider.GetRequiredService<IAssemblyProvider>());
            app.UseMvc(builder =>
            {
                builder.MapODataRoute("api/VendasContext", _model);
            });

            Console.WriteLine("AppApiCore initialized!");
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
            modelBuilder.EntitySet<CLIENTE>("CLIENTE");
           
            // Functions       
            var function = modelBuilder.Function("GetClienteNoAssociations");
            function.Parameter<string>("p1");
            function.ReturnsCollectionFromEntitySet<CLIENTE>("CLIENTE");


            var model = modelBuilder.GetEdmModel();

            return model;
        }
    }

    public class CustomRoutingConvention : IODataRoutingConvention
    {
        public ActionDescriptor SelectAction(RouteContext routeContext)
        {
            Console.WriteLine("In CustomRoutingConvention !");
            return null;
        }
    }
}
