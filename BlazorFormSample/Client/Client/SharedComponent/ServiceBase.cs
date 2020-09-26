using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using BlazorFormSample.Shared;
using System.Data;

namespace BlazorFormSample.Client.SharedComponent
{
    public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        protected abstract string ApiUrl { get; }
        protected IConfiguration Configuration { get; }

        private readonly HttpClient _httpClient;

        public ServiceBase(IConfiguration configuration, HttpClient httpClient)
        {
            Configuration = configuration;
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            var response = await _httpClient.GetStringAsync(ApiUrl);
            return JsonConvert.DeserializeObject<TEntity[]>(response);
        }
        public async Task<TEntity> GetAsync(Guid id)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}/{id}");
            return JsonConvert.DeserializeObject<TEntity>(response);
        }

        public async Task<Guid> AddItemAsync(TEntity item)
        {
            var response = await _httpClient.PostAsJsonAsync<TEntity>(ApiUrl, item);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            TEntity result = Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(responseContent);
            
            return result.Id;
        }

        public async Task<Guid> UpdateItemAsync(TEntity item)
        {
            var response = await _httpClient.PutAsJsonAsync<TEntity>($"{ApiUrl}/{item.Id}", item);
            response.EnsureSuccessStatusCode();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<Guid>(responseContent);
            return item.Id;
        }

        public async Task DeleteItemAsync(TEntity item)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{item.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
