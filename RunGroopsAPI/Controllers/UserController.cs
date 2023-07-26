using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Commands.UserCommands;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;
using RunGroops.Infrastructure.Context;

namespace RunGroopsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext applicationDbContext;
        public UserController(IMediator mediator, ApplicationDbContext applicationDbContext)
        {
            _mediator = mediator;
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]int page, [FromQuery]int pageSize)
        {
            var query = new GetUsersQuery(page, pageSize);
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
        [HttpGet("userName={userName}")]
        public async Task<IActionResult> GetUsersByName(string userName, [FromQuery]int page, [FromQuery]int pageSize)
        {
            var query = new GetUsersByNameQuery(userName, page, pageSize);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("clubs")]
        public async Task<IActionResult> GetUserClubs([FromQuery]string userId)
        {
            var query = new GetUserClubsQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("races")]
        public async Task<IActionResult> GetUserRaces([FromQuery] string userId)
        {
            var query = new GetUserRacesQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string userId, [FromForm] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateUserCommand(userId, request);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Updated successfully!" }) : BadRequest(ModelState);
        }
    }
}
