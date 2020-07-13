using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Autofac;
using AutoMapper;
using GraphQL;
using GraphQlLibary.DAL.Context;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQlLibary.Models.ConfigModels;
using GraphQlLibary.Web.Middleware;
using GraphQlLibary.Web.GraphQL;
using Microsoft.AspNetCore.Http;
using GraphQL.Validation;
using GraphQL.Server.Authorization.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using GraphQlLibary.Web.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQlLibary.Web
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
            string connection = Configuration.GetConnectionString("LibaryDbConnection");
            services.AddDbContext<LibaryContext>(options => options.UseSqlServer(connection));

            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddDataLoader();

            ConfigureDI(services);
            services.AddCors();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AuthSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDistributedMemoryCache();

            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(5);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseGraphiQLServer(null);
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseMiddleware<GraphQLMiddleware>();
            app.UseGraphQLUpload<LibarySchema>().UseGraphQL<LibarySchema>();

            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
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

        private void ConfigureDI(IServiceCollection services)
        {
            services.Configure<CloudDictionaryConfig>(Configuration.GetSection("CloudinarySettings"));
            services.AddSingleton<LibarySchema>().AddGraphQLUpload().AddGraphQL();
            services.AddSingleton(ConfigireAutoMapper());
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient<IValidationRule, AuthorizationValidationRule>()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("LoggedIn", p => p.RequireAuthenticatedUser());
                });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "graph-auth";
            });
        }
    }
}
