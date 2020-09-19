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
    public class SkillGroupsController : ControllerBase
    {
        private readonly CreatureDbContext _context;

        public SkillGroupsController(CreatureDbContext context)
        {
            _context = context;
        }

        // GET: api/SkillGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillGroup>>> GetSkillGroups()
        {
            return await _context.SkillGroups.ToListAsync();
        }

        // GET: api/SkillGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillGroup>> GetSkillGroup(Guid id)
        {
            var skillGroup = await _context.SkillGroups.FindAsync(id);

            if (skillGroup == null)
            {
                return NotFound();
            }

            return skillGroup;
        }

        // PUT: api/SkillGroups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillGroup(Guid id, SkillGroup skillGroup)
        {
            if (id != skillGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(skillGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillGroupExists(id))
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

        // POST: api/SkillGroups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SkillGroup>> PostSkillGroup(SkillGroup skillGroup)
        {
            _context.SkillGroups.Add(skillGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkillGroup", new { id = skillGroup.Id }, skillGroup);
        }

        // DELETE: api/SkillGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillGroup>> DeleteSkillGroup(Guid id)
        {
            var skillGroup = await _context.SkillGroups.FindAsync(id);
            if (skillGroup == null)
            {
                return NotFound();
            }

            _context.SkillGroups.Remove(skillGroup);
            await _context.SaveChangesAsync();

            return skillGroup;
        }

        private bool SkillGroupExists(Guid id)
        {
            return _context.SkillGroups.Any(e => e.Id == id);
        }
    }
}
