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
    public class GameSystemsController : ControllerBase
    {
        private readonly IEntityProvider<GameSystem> _entityProvider;

        public GameSystemsController(IEntityProvider<GameSystem> entityProvider)
        {
            _entityProvider = entityProvider;
        }

        // GET: api/GameSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSystem>>> GetGameSystems()
        {
            return Ok(await _entityProvider.GetListAsync());
        }

        // GET: api/GameSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSystem>> GetGameSystem(Guid id)
        {
            var gameSystem = await _entityProvider.GetAsync(id);

            if (gameSystem == null)
            {
                return NotFound();
            }

            return gameSystem;
        }

        // PUT: api/GameSystems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameSystem(Guid id, GameSystem gameSystem)
        {            
            if (id != gameSystem.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _entityProvider.UpdateAsync(gameSystem);

            return NoContent();

        }

        // POST: api/GameSystems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameSystem>> PostGameSystem(GameSystem gameSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newId = await _entityProvider.AddAsync(gameSystem);
            gameSystem.Id = newId;
            return CreatedAtAction("GetGameSystem", new { id = newId }, await _entityProvider.GetAsync(newId));
        }

        // DELETE: api/GameSystems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameSystem(Guid id)
        {
            return await _entityProvider.DeleteAsync(id) ? new NoContentResult() : new ConflictResult() as ActionResult;
        }
    }
}
