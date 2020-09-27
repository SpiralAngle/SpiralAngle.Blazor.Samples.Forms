using BlazorFormSample.Client.SharedComponent;
using BlazorFormSample.Shared.CreatureModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.Creatures
{
    public class CreatureService : ServiceBase<Creature>
    {
        public CreatureService(IConfiguration configuration, HttpClient httpClient) : base(configuration, httpClient)
        {
        }


        protected override string ApiUrl
        {
            get
            {
                return $"{Configuration["ApiBaseUrl"]}/Creatures";
            }
        }
    }
}
