using Microsoft.AspNetCore.Mvc;
using WorkWell.Api.Models;
using WorkWell.Api.Repositories;

namespace WorkWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentRepository _repo;

        public AssessmentsController(IAssessmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(decimal userId)
        {
            return Ok(await _repo.GetAllByUserAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assessment assessment)
        {
            var id = await _repo.CreateAsync(assessment);
            assessment.Id = id;

            return CreatedAtAction(nameof(GetById), new { id }, assessment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
