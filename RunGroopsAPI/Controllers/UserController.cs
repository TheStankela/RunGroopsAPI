using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Infrastructure.Context;

namespace RunGroopsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]int page)
        {
            var query = new GetUsersQuery(page);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpGet("clubs")]
        public async Task<IActionResult> GetUserClubs([FromQuery]string userId)
        {
            var query = new GetUserClubsQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
