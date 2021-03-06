using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppInsightsDemoApp.Models;
using AppInsightsDemoApp.Repositories;
using AppInsightsDemoApp.RedisClients;
using Microsoft.ApplicationInsights;

namespace AppInsightsDemoApp
{
    public class Startup
    {
        private const string DevServerUrl = "http://localhost:8080";
        private const string ClientAppPath = "client-app";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(); // add
            services.AddSnapshotCollector();
            services.AddSpaStaticFiles(options => options.RootPath = $"{ClientAppPath}/dist");
            services.AddControllers();
            services.AddDbContext<AppInsightsDemoDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString(nameof(AppInsightsDemoDbContext))));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSingleton<IRedisClient, RedisClient>(p => 
                new RedisClient(Configuration["REDIS_CONNECTION_STRING"], p.GetService<TelemetryClient>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(builder =>
            {
                builder.Options.SourcePath = ClientAppPath;
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer(DevServerUrl);
                }
            });
        }
    }
}
