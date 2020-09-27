using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorFormSample.Server.Shared;
using BlazorFormSample.Shared.CreatureModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFormSample.Server.CreatureApi
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreaturesController : EntityController<Creature>
    {
        public CreaturesController(IEntityProvider<Creature> entityProvider) : base(entityProvider)
        {
        }        
    }
}
