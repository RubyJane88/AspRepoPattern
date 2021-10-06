using System.Collections.Generic;
using DemoSeven.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DemoSeven.WebApi.Extensions
{
    public static class ServicesExtensions
    {
        //Custom service collection for the Startup.cs 

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ReportApiVersions = true;
                config.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("version"),
                    new HeaderApiVersionReader("version"));
            });
        }

        public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
        }

        /* Custom Service Collection */

        public static void AddSwaggerGenExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

            });
        }





    }
}