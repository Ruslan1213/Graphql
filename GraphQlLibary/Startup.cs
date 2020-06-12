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
            services.AddControllersWithViews();
            string connection = Configuration.GetConnectionString("LibaryDbConnection");
            services.AddDbContext<LibaryContext>(options => options.UseSqlServer(connection));
            services.AddSingleton(ConfigireAutoMapper());
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphiQLServer(null);
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
