using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkWell.Api.Models;
using WorkWell.Api.Repositories;

namespace WorkWell.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UseresController : ControllerBase
    {
        private readonly IUseresRepository _repo;

        public UseresController(IUseresRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _repo.GetPagedAsync(page, pageSize);
            var total = await _repo.CountAsync();

            var result = new
            {
                data = items,
                pagination = new
                {
                    page,
                    pageSize,
                    totalItems = total,
                    totalPages = (int)Math.Ceiling(total / (double)pageSize)
                },
                links = new
                {
                    self = Url.Action(null, new { page, pageSize }),
                    next = page * pageSize < total ? Url.Action(null, new { page = page + 1, pageSize }) : null,
                    prev = page > 1 ? Url.Action(null, new { page = page - 1, pageSize }) : null
                }
            };

            return Ok(result);
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
