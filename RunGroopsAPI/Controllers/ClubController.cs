using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.Interfaces;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Application.Models;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClubController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClubsAsync()
        {
            var query = new GetAllClubsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubByIdAsync(int id)
        {
            var query = new GetClubByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpGet("city/{cityName}")]
        public async Task<IActionResult> GetClubsByCityAsync(string cityName)
        {
            var query = new GetClubsByCityQuery(cityName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("name={clubName}")]
        public async Task<IActionResult> GetClubByNameAsync(string clubName)
        {
            var query = new GetClubByNameQuery(clubName);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddClubAsync(ClubRequest clubRequest)
        {
            var command = new AddClubCommand(clubRequest);
            var result = await _mediator.Send(command);
            return result is true ? Ok("Added successfully!") : BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClubAsync(int clubId, UpdateClubRequest updateClubRequest)
        {
            var command = new UpdateClubCommand(clubId, updateClubRequest);
            var result = await _mediator.Send(command);

            return result is true ? Ok("Updated successfully") : BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> UpdateClubAsync(int clubId)
        {
            var command = new DeleteClubCommand(clubId);
            var result = await _mediator.Send(command);

            return result is true ? Ok("Deleted successfully") : BadRequest();
        }
    }
}
