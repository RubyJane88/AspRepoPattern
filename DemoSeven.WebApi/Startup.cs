using System;
using System.Net;
using DemoSeven.WebApi.Extensions;
using DemoSeven.WebApi.Helpers;
using DemoSeven.WebApi.Models;
using DemoSeven.WebApi.Repositories;
using DemoSeven.WebApi.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DemoSeven.WebApi
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
            services.AddAuthorization();
            services.AddControllers();
            
            /* EF Core DbContext */
            var connectionString = Configuration["MSSQLServer:ConnectionString"];
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
            
            /* Redis Cache */
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisServer:ConnectionString"];
            });
            
            /* AutoMapper for mapping Entities to Dtos */
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            /* CORS */
            services.AddCors();
            
            /* API versioning */
            services.AddApiVersioningExtension();
            services.AddVersionedApiExplorerExtension();
            
            /* For Open API documentation */
            services.AddSwaggerGenExtension();

            /* Hangfire is a timer or chron jobs or scheduled jobs */
            services.AddHangfire(x => x.UseInMemoryStorage());
            services.AddHangfireServer();
            
            /* auth */
            services.Configure<AuthSettings>(Configuration.GetSection(nameof(AuthSettings)));

            /* health checks */
            services.AddHealthChecks();

            /* scrutor */
            services.Scan(scan => 
                scan.FromCallingAssembly()                    
                    .AddClasses()
                    .AsMatchingInterface());

            /* register your contracts and repositories/services here through the built-in dependency injections */
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Scrutor maps automatically from interface to service. So as a result,  e.g. 
            //services.AddScoped<IPerson, Person>  => services.AddScoped<> 
            services.AddScoped<TodoRepository>();
            services.AddScoped<JobService>();
            services.AddScoped<UserService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerExtensions(provider);
              
            }
            
            /* CORS Policy */
            app.UseCors(b =>
            {
                b.AllowAnyOrigin();
                b.AllowAnyHeader();
                b.AllowAnyMethod();
            });
            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();

            /* hangfire dashboard */
            app.UseHangfireDashboard("/chron-jobs-dashboard");
            
            // /* health check */
             app.UseHealthChecks("/api/health");
             
            /* Basic Global Exception Handler*/
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.ContentType = "text/html";
                            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                            if (null != exceptionObject)
                            {
                                var errorMessage = $"{exceptionObject.Error.Message}";
                                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                            }
                        });
                }
            );

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}