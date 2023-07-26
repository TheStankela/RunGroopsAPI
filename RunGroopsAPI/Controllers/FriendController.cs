using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Application.Commands.FriendCommands;
using RunGroops.Application.Queries.FriendQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetUserFriendsCount([FromQuery]string userId)
        {
            var query = new GetUserFriendsCountQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("Requests")]
        public async Task<IActionResult> GetUserPendingFriendRequests()
        {
            var query = new GetUserPendingFriendRequestsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [Authorize]
        [HttpPost("Request/Accept")]
        public async Task<IActionResult> AcceptFriendRequest([FromQuery] string fromUserId)
        {
            var command = new AcceptFriendRequestCommand(fromUserId);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new {Message = "Accepted successfully!"}) : BadRequest(ModelState);
        }
        [Authorize]
        [HttpPost("Request/Send")]
        public async Task<IActionResult> SendFriendRequest([FromQuery]string toUserId)
        {
            var command = new SendFriendRequestCommand(toUserId);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Sent successfully!" }) : BadRequest(ModelState);
        }
    }
}
