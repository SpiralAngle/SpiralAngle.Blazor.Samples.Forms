using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorFormSample.Server.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using BlazorFormSample.Server.Shared;
using BlazorFormSample.Shared.GameModels;

namespace BlazorFormSample.Server.GameSystemApi
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameSystemsController : EntityController<GameSystem>
    {
        public GameSystemsController(IEntityProvider<GameSystem> entityProvider) : base(entityProvider)
        {
        }
    }    
}
