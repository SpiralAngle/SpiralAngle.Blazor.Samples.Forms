using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorFormSample.Shared;

namespace BlazorFormSample.Server.Shared
{
    public class EntityController<TEntity> : ControllerBase where TEntity : class, IEntity, new()
    {
        private readonly IEntityProvider<TEntity> _entityProvider;

        public EntityController(IEntityProvider<TEntity> entityProvider)
        {
            _entityProvider = entityProvider;
        }

        // GET: api/TEntitys
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return Ok(await _entityProvider.GetListAsync());
        }

        // GET: api/TEntitys/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var TEntity = await _entityProvider.GetAsync(id);

            if (TEntity == null)
            {
                return NotFound();
            }

            return TEntity;
        }

        // PUT: api/TEntitys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid id, TEntity TEntity)
        {
            if (id != TEntity.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _entityProvider.UpdateAsync(TEntity);

            return NoContent();

        }

        // POST: api/TEntitys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post(TEntity TEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newId = await _entityProvider.AddAsync(TEntity);
            TEntity.Id = newId;
            return CreatedAtAction("Post", new { id = newId }, await _entityProvider.GetAsync(newId));
        }

        // DELETE: api/TEntitys/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            return await _entityProvider.DeleteAsync(id) ? new NoContentResult() : new ConflictResult() as ActionResult;
        }
    }
}
