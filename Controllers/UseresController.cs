using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkWell.Api.Models;
using WorkWell.Api.Repositories;

namespace WorkWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UseresController : ControllerBase
    {
        private readonly IUseresRepository _repo;

        public UseresController(IUseresRepository repo)
        {
            _repo = repo;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Useres user)
        {
            var id = await _repo.CreateAsync(user);
            user.Id = id;

            return CreatedAtAction(nameof(GetById), new { id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
