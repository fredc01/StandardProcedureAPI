using Microsoft.AspNetCore.Mvc;
using Supabase;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using StandardProcedureAPI.Models;

namespace StandardProcedureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HowToUseStepsController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public HowToUseStepsController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        // GET: api/howtousesteps
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var table = _supabase.From<how_to_use_steps>();
            var response = await table
                .Order(s => s.step_number, Supabase.Postgrest.Constants.Ordering.Ascending)
                .Get();

            var dto = response.Models.Select(s => new HowToUseStepDto
            {
                id = s.id,
                step_number = s.step_number,
                description = s.description,
                created_at = s.created_at
            });

            return Ok(dto);
        }

        // GET: api/howtousesteps/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var table = _supabase.From<how_to_use_steps>();
            var response = await table
                .Where(s => s.id == id)
                .Get();

            var step = response.Models.FirstOrDefault();
            if (step == null) return NotFound();

            var dto = new HowToUseStepDto
            {
                id = step.id,
                step_number = step.step_number,
                description = step.description,
                created_at = step.created_at
            };

            return Ok(dto);
        }

        // POST: api/howtousesteps
        [HttpPost]
        public async Task<IActionResult> Create(how_to_use_steps step)
        {
            var table = _supabase.From<how_to_use_steps>();
            await table.Insert(step);

            var dto = new HowToUseStepDto
            {
                id = step.id,
                step_number = step.step_number,
                description = step.description,
                created_at = step.created_at
            };

            return CreatedAtAction(nameof(Get), new { id = step.id }, dto);
        }

        // PUT: api/howtousesteps/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, how_to_use_steps step)
        {
            if (id != step.id) return BadRequest();

            var table = _supabase.From<how_to_use_steps>();
            await table
                .Where(s => s.id == id)
                .Update(step);

            return NoContent();
        }

        // DELETE: api/howtousesteps/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var table = _supabase.From<how_to_use_steps>();
            await table
                .Where(s => s.id == id)
                .Delete();


            return NoContent();
        }
    }
}
