using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WebAPI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var AllowedOriginsStr = configuration["AllowedOrigins"];
        if (!string.IsNullOrWhiteSpace(AllowedOriginsStr))
        {
            string[] origins = AllowedOriginsStr.Split(";");
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });
        }
        //
        //services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddControllers(options =>
        {
            options.Filters.Add(new ApiExceptionFilterAttribute());
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        }).AddNewtonsoftJson((MvcNewtonsoftJsonOptions options) =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        });
        return services;
    }
}