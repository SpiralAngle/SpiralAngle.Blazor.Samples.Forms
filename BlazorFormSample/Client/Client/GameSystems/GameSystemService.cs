using BlazorFormSample.Client.SharedComponent;
using Models = BlazorFormSample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BlazorFormSample.Client.GameSystems
{
    public class GameSystemService : ServiceBase<Models.GameModels.GameSystem>
    {
        public GameSystemService(IConfiguration configuration, HttpClient httpClient) : base(configuration, httpClient)
        {
        }

        protected override string ApiUrl {
            get
            {
                return $"{Configuration["ApiBaseUrl"]}/GameSystems";
            }
        }
    }
}
