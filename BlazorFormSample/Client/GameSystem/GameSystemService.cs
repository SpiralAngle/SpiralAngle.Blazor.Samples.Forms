using BlazorFormSample.Client.Shared;
using Models = BlazorFormSample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BlazorFormSample.Client.GameSystem
{
    public class GameSystemService : ServiceBase<Models.GameSystem>
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
