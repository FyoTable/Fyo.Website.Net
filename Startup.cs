using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Fyo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Fyo.IoC;
using Fyo.Interfaces;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Fyo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            ServiceProvider = serviceProvider;
        }

        public IConfiguration Configuration { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment { get; }
        public IServiceProvider ServiceProvider { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
            
            services.Configure<RazorViewEngineOptions>(o => {  
                o.ViewLocationExpanders.Add(new MyViewLocationExpander());  
            });  

            services.AddMvc(o => {
                o.EnableEndpointRouting = false;
            })
                .AddJsonOptions(options => {
                    // options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // options.SerializerSettings.Converters.Add(new StringEnumConverter {
                    //     CamelCaseText = true
                    // });
                });

            var connections = Configuration.GetSection("ConnectionStrings");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connections["DefaultConnection"]));

            // IoC
            ServicesModule.AddServices(services);

            var serviceProvider = services.BuildServiceProvider();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                Configuration.GetSection("Auth0").Bind(options);
                options.Events = GetJwtBearerEvents(serviceProvider);
            });
            
            Console.WriteLine("Env: " + HostingEnvironment.EnvironmentName);
            if(!HostingEnvironment.IsDevelopment() && !HostingEnvironment.IsEnvironment("local")){
                // services.Configure<MvcOptions>(options => {
                //     options.Filters.Add(new RequireHttpsAttribute());
                // });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

            if (env.IsDevelopment() || env.IsEnvironment("local"))
            {

                app.UseDeveloperExceptionPage();
                // app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                // {
                //     HotModuleReplacement = true,
                //     ReactHotModuleReplacement = true
                // });

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    if (!serviceScope.ServiceProvider.GetService<DataContext>().AllMigrationsApplied())
                    {
                        serviceScope.ServiceProvider.GetService<DataContext>().Database.Migrate();
                        serviceScope.ServiceProvider.GetService<DataContext>().EnsureSeeded(Configuration);
                    }                    
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                var options = new RewriteOptions();//.AddRedirectToHttps();

                app.UseRewriter(options);
            }
        
            app.UseRouting();

            app.UseCors(builder =>
                builder.WithOrigins(new string[] { "http://localhost:5000" , "http://localhost:8000" })
                    .AllowAnyHeader()
            );

            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider( 
                    Path.Combine(Directory.GetCurrentDirectory(), @"Web", @"wwwroot")), 
                    RequestPath = new PathString("")
            });

            app.UseAuthentication();
            
            app.UseEndpoints(endpoints =>  {
                endpoints.MapHub<SignalR>("/signalr");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // routes.MapSpaFallbackRoute(
                //     name: "spa-fallback",
                //     defaults: new { controller = "Home", action = "Index" });
            });
        }

        private JwtBearerEvents GetJwtBearerEvents(ServiceProvider serviceProvider)
        {
            var events = new JwtBearerEvents();

            events.OnTokenValidated = async(context) =>
            {
                // If you need the user's information for any reason at this point, you can get it by looking at the Claims property
                // of context.Ticket.Principal.Identity
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    // Get the user's auth0 ID
                    string userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                    Console.WriteLine("USERID: ", userId);

                    var userService = serviceProvider.GetService<IUserService>();
                    User user = await userService.GetByThirdPartyId(userId);

                    if(user != null){
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole.ToString()));
                        //todo: also add tenant id as a claim once we decide how to do that
                    }
                }                
            };

            return events;
        }
    }

    public class MyViewLocationExpander : IViewLocationExpander  
    {  
  
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,   
            IEnumerable<string> viewLocations)  
        {  
  
            //replace the Views to MyViews..  
            viewLocations = viewLocations.Select(s => s.Replace("Views", "Web/Views"));  
  
            return viewLocations;  
        }  
  
        public void PopulateValues(ViewLocationExpanderContext context)  
        {  
            //nothing to do here.  
        }  
    } 
}
