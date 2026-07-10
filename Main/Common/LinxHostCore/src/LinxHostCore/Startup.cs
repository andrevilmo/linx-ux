using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Modular.Core;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.PlatformAbstractions;
using System.Runtime.Loader;
using Swashbuckle.AspNetCore.Swagger;
using Linx.Security.Core.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace LinxHostCore
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IList<ModuleInfo> modules = new List<ModuleInfo>();
        internal static IConfigurationRoot Configuration { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            services.AddAuthorization();
            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IConfiguration>(Configuration);

            // Add Custom Authentication 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CustomAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = CustomAuthenticationOptions.DefaultScheme;
            })
            // Call custom authentication extension method
            .AddCustomAuth(options =>
            {
                // Configure single or multiple passwords for authentication
                //options.AuthKey = "custom auth key";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IAuthorizationHandler, Linx.Security.Core.Authorization.ApiAuthorizationHandler>();

            // Add framework services.
            IMvcBuilder mvcBuilder = services.AddMvc();

            //Load api modules
            this.LoadModules(mvcBuilder, services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "LinxHostCore APIs",
                    Description = "RESTful API for LinxHostCore",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Alessandro Araujo", Email = "alessandro.fa@linx.com.br", Url = "" }
                });
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        private string GetRight(string value, string search)
        {
            int length = -1;
            while (value.IndexOf(search, length + 1) >= 0)
                length = value.IndexOf(search, length + 1);


            if ((length < 0) || ((length + search.Length) >= value.Length))
                return "";
            else
                return value.Substring(length + search.Length);
        }

        private void LoadBusinessModules(BusinessModuleType type)
        {
            string businessPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "BusinessModules");
            if (Directory.Exists(businessPath))
            {
                var modulesDirCollection = System.IO.Directory.GetDirectories(businessPath, Enum.GetName(typeof(BusinessModuleType), type), SearchOption.AllDirectories);
                if (modulesDirCollection.Length > 0)
                {
                    string modulesDir = GetRight(modulesDirCollection[0].ToLower(), System.IO.Directory.GetCurrentDirectory().ToLower()).Replace("\\", "/");

                    var moduleRootFolder = _hostingEnvironment.ContentRootFileProvider.GetDirectoryContents(modulesDir);

                    foreach (var moduleFolder in moduleRootFolder.Where(x => x.IsDirectory))
                    {
                        var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.PhysicalPath, "bin"));
                        if (!binFolder.Exists)
                        {
                            continue;
                        }

                        foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                        {
                            Assembly assembly;
                            try
                            {
                                assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                            }
                            catch (FileLoadException ex)
                            {
                                if (ex.Message == "Assembly with same name is already loaded")
                                {
                                    continue;
                                }
                                throw;
                            }

                            if (type == BusinessModuleType.API && assembly.FullName.Contains(moduleFolder.Name))
                            {
                                Console.WriteLine("Loaded " + moduleFolder.Name + ".");
                                modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.PhysicalPath });
                            }
                        }
                    }
                }
            }
        }

        private void LoadModules(IMvcBuilder mvcBuilder, IServiceCollection services)
        {
            //Loading Business Modules
            this.LoadBusinessModules(BusinessModuleType.BM);
            this.LoadBusinessModules(BusinessModuleType.BV);
            this.LoadBusinessModules(BusinessModuleType.API);

            //Initiaslizing modules
            if (modules.Count > 0)
            {
                foreach (var module in modules)
                {
                    // Register controller from modules
                    mvcBuilder.AddApplicationPart(module.Assembly);

                    // Register dependency in modules
                    var initTypes = module.Assembly.GetTypes().Where(x => typeof(IModuleInitializer).IsAssignableFrom(x)).ToArray();
                    foreach (var moduleInitializerType in initTypes)
                    {
                        if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
                        {
                            var instance = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                            module.Initializers.Add(instance);
                            instance.Init(services, _hostingEnvironment);
                        }
                    }
                }
            }

            services.AddMvc(o =>
            {
                for (int i = o.OutputFormatters.Count - 1; i >= 0; i--)
                {
                    var of = o.OutputFormatters[i] as OutputFormatter;
                    if (of != null && of.SupportedMediaTypes.Count == 0)
                        o.OutputFormatters.RemoveAt(i);
                }

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseSwagger(c =>
                c.PreSerializeFilters.Add((item, req) => item.Produces = new List<string> { "application/json" }));


            // Shows UseCors with named policy.
            app.UseCors("AllowAllOrigins");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStatusCodePages(async context =>
            {
                //context.HttpContext.Response.ContentType = "text/plain";
                //await context.HttpContext.Response.WriteAsync(
                //    "Status code page, status code: " +
                //   context.HttpContext.Response.StatusCode);

                await new Linx.Security.Core.JsonExceptionMiddleware().GetError(context.HttpContext);

            });

            //Json Error Serialization
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new Linx.Security.Core.JsonExceptionMiddleware().Invoke
            });

            //Enables Authentication
            app.UseAuthentication();

            app.UseMvc();

            //Initialize configuration
            foreach (var module in modules)
            {
                if (module.Initializers.Count > 0)
                {
                    foreach (var mi in module.Initializers)
                    {
                        mi.Init(app);
                    }
                }
            }


        }

    }

    enum BusinessModuleType
    {
        BM,
        BV,
        API
    }

}
