using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorFormSample.Client.GameSystems;
using BlazorFormSample.Client.SharedComponent;
using Models = BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using BlazorFormSample.Client.Creatures;

namespace BlazorFormSample.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            await GetConfigurationFromServer(builder);

            var scope = builder.Configuration["ScopeUri"];

            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("SpiralAngle.BlazorFormSample.Server", client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("SpiralAngle.BlazorFormSample.Server"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
            });

            builder.Services.AddScoped<IService<Models.GameModels.GameSystem>, GameSystemService>();
            builder.Services.AddScoped<IService<Models.CreatureModels.Creature>, CreatureService>();
            await builder.Build().RunAsync();
        }

        private static async Task GetConfigurationFromServer(WebAssemblyHostBuilder builder)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            using var response = await client.GetAsync("configuration");
            using var stream = await response.Content.ReadAsStreamAsync();
            builder.Configuration.AddJsonStream(stream);
        }
    }
}
