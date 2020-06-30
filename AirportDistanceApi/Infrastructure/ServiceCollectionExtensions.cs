using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClientConfiguration = configuration.GetSection("HttpClientFactory");
            foreach (var clientConfiguration in httpClientConfiguration.GetChildren())
            {
                var apiName = clientConfiguration.Key;
                services.AddHttpClient(apiName, client =>
                {
                    client.BaseAddress = new Uri(clientConfiguration["BaseAddress"]);
                    var authenticationSection = clientConfiguration.GetSection("Authentication");
                    if (authenticationSection.GetChildren().Count() > 0)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            authenticationSection["Scheme"],
                            authenticationSection["Parameter"]);
                    }
                });
            }

            services.AddCors();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                

            if (configuration.GetValue<bool>("EnableSwagger"))
            {
                services.AddSwaggerGen();
            }
            
        }
    }
}
