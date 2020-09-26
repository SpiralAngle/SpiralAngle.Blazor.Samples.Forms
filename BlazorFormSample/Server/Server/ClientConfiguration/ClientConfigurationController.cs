using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorFormSample.Server.ClientConfiguration
{
    [ApiController]
    [Route("configuration")]
    public class ClientConfigurationController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClientConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ClientConfiguration clientConfiguration = new ClientConfiguration();
            _configuration.Bind("ClientConfigNoSecretsAllowed", clientConfiguration);
            var value = JsonConvert.SerializeObject(clientConfiguration);
            return new OkObjectResult(value);
        }
    }
}
