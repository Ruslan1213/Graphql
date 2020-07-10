using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Autofac;
using AutoMapper;
using GraphQL;
using GraphQlLibary.Middleware;
using GraphQlLibary.DAL.Context;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQlLibary.Models.ConfigModels;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GraphQlLibary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));

            services.AddControllersWithViews();
            string connection = Configuration.GetConnectionString("LibaryDbConnection");
            services.AddDbContext<LibaryContext>(options => options.UseSqlServer(connection));
            services.AddSingleton(ConfigireAutoMapper());
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddDataLoader();

            services.Configure<CloudDictionaryConfig>(Configuration.GetSection("CloudinarySettings"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddDistributedMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseGraphiQLServer(null);
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseMiddleware<GraphQLMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Infrastructure.DI.ApplicationModule());
        }

        private IMapper ConfigireAutoMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Infrastructure.Mapping.DomainProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
