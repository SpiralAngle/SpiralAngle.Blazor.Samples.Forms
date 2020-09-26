using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorFormSample.Server.Data;
using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Authorization;

namespace BlazorFormSample.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreaturesController : ControllerBase
    {
        private readonly CreatureDbContext _context;

        public CreaturesController(CreatureDbContext context)
        {
            _context = context;
        }

        // GET: api/Creatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreatures()
        {
            return await _context.Creatures.ToListAsync();
        }

        // GET: api/Creatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Creature>> GetCreature(Guid id)
        {
            var creature = await _context.Creatures.FindAsync(id);

            if (creature == null)
            {
                return NotFound();
            }

            return creature;
        }

        // PUT: api/Creatures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreature(Guid id, Creature creature)
        {
            if (id != creature.Id)
            {
                return BadRequest();
            }

            _context.Entry(creature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreatureExists(id))
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

        // POST: api/Creatures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Creature>> PostCreature(Creature creature)
        {
            _context.Creatures.Add(creature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreature", new { id = creature.Id }, creature);
        }

        // DELETE: api/Creatures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Creature>> DeleteCreature(Guid id)
        {
            var creature = await _context.Creatures.FindAsync(id);
            if (creature == null)
            {
                return NotFound();
            }

            _context.Creatures.Remove(creature);
            await _context.SaveChangesAsync();

            return creature;
        }

        private bool CreatureExists(Guid id)
        {
            return _context.Creatures.Any(e => e.Id == id);
        }
    }
}
