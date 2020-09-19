using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorFormSample.Server.Data;
using BlazorFormSample.Shared;

namespace BlazorFormSample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSystemsController : ControllerBase
    {
        private readonly CreatureDbContext _context;

        public GameSystemsController(CreatureDbContext context)
        {
            _context = context;
        }

        // GET: api/GameSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSystem>>> GetGameSystems()
        {
            return await _context.GameSystems.ToListAsync();
        }

        // GET: api/GameSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSystem>> GetGameSystem(Guid id)
        {
            var gameSystem = await _context.GameSystems.FindAsync(id);

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

            _context.Entry(gameSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSystemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameSystems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameSystem>> PostGameSystem(GameSystem gameSystem)
        {
            _context.GameSystems.Add(gameSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameSystem", new { id = gameSystem.Id }, gameSystem);
        }

        // DELETE: api/GameSystems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameSystem>> DeleteGameSystem(Guid id)
        {
            var gameSystem = await _context.GameSystems.FindAsync(id);
            if (gameSystem == null)
            {
                return NotFound();
            }

            _context.GameSystems.Remove(gameSystem);
            await _context.SaveChangesAsync();

            return gameSystem;
        }

        private bool GameSystemExists(Guid id)
        {
            return _context.GameSystems.Any(e => e.Id == id);
        }
    }
}
