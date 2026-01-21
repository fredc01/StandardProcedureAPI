using Microsoft.AspNetCore.Mvc;
using Supabase;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using StandardProcedureAPI.Models;

namespace StandardProcedureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public FeaturesController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        // GET: api/features
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var table = _supabase.From<features>();
            var response = await table
                .Order(f => f.CreatedAt, Supabase.Postgrest.Constants.Ordering.Ascending)
                .Get();

            // Map to DTO
            var dto = response.Models.Select(f => new FeatureDto
            {
                id = f.id,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt
            });

            return Ok(dto);
        }

        // GET: api/features/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var table = _supabase.From<features>();
            var response = await table
                .Where(f => f.id == id)
                .Get();

            var feature = response.Models.FirstOrDefault();
            if (feature == null) return NotFound();

            // Map to DTO
            var dto = new FeatureDto
            {
                id = feature.id,
                Title = feature.Title,
                Description = feature.Description,
                CreatedAt = feature.CreatedAt
            };

            return Ok(dto);
        }

        // POST: api/features
        [HttpPost]
        public async Task<IActionResult> Create(features feature)
        {
            var table = _supabase.From<features>();
            await table.Insert(feature);

            // Map to DTO for response
            var dto = new FeatureDto
            {
                id = feature.id,
                Title = feature.Title,
                Description = feature.Description,
                CreatedAt = feature.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = feature.id }, dto);
        }

        // PUT: api/features/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, features feature)
        {
            if (id != feature.id)
                return BadRequest();

            var table = _supabase.From<features>();
            await table
                .Where(f => f.id == id)
                .Update(feature);

            return NoContent();
        }

        // DELETE: api/features/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var table = _supabase.From<features>();
            await table
                .Where(f => f.id == id)
                .Delete();

            return NoContent();
        }
    }
}
