using MayStats_Infra.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MayStats_Infra.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<TId, TTable> : ControllerBase where TTable : class
    {
        private readonly IEFRepository<TId, TTable> _repository;

        public GenericController(IEFRepository<TId, TTable> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TTable>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TTable>> GetById(TId id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TTable entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = typeof(TTable).GetProperty("Id").GetValue(entity) }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(TId id, TTable entity)
        {
            if (!id.Equals(typeof(TTable).GetProperty("Id").GetValue(entity)))
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(TId id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
