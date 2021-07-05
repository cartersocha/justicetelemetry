using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenT2.DataAccess;
using OpenT2.Models;
using OpenT2.Repositories;
using Azure.Monitor.OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using System.Data.SqlClient;
using System;

namespace OpenT2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenTelemetryTracing((builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("Justice&CarterAPI-Country")
                .AddSource("AnotherAPI")
                .AddSource("Hello")
                .AddSqlClientInstrumentation(options =>
                {
                    options.SetDbStatementForText = true;
                    options.RecordException = true;
                    options.Enrich
                    = (activity, eventName, rawObject) =>
                    {
                        if (eventName.Equals("OnCustom"))
                        {
                            if (rawObject is SqlCommand cmd)
                            {
                                activity.SetTag("db.commandTimeout", cmd.CommandTimeout);
                            }
                        };
                    };
                   })
               //.AddAzureMonitorTraceExporter(o =>
                //{
                  // o.ConnectionString = $"InstrumentationKey=a95de56a-a39d-4fc9-9646-6d7c480ee9cf;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/";
                //}));
                //.AddJaegerExporter());
                .AddZipkinExporter(b =>
                {
                    var zipkinHostName = "localhost";
                    b.Endpoint = new Uri($"http://{zipkinHostName}:9411/api/v2/spans");
                }));
                //.AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:4317"));
            services.AddDbContext<postgresContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDataContext>(provider => provider.GetService<postgresContext>());
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenT2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenT2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
