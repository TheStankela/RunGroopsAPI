using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Application.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> GetAllClubsAsync([FromQuery]int page)
        {
            var query = new GetAllClubsQuery(page);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserClubsAsync()
        {
            var query = new GetAllUserClubsQuery();
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddClubAsync([FromForm] ClubRequest clubRequest, IFormFile file)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddClubCommand(clubRequest, file);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Added successfully!" }) : BadRequest();
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateClubAsync([FromQuery] int clubId,[FromForm] UpdateClubRequest updateClubRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateClubCommand(clubId, updateClubRequest);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Updated successfully" }) : BadRequest();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteClubAsync([FromQuery]int clubId)
        {
            var command = new DeleteClubCommand(clubId);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Deleted successfully" }) : BadRequest();
        }
    }
}
