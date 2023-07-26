using MediatR;

namespace RunGroops.Application.Commands.FriendCommands
{
    public record SendFriendRequestCommand(string ToUserId) : IRequest<bool>;
}
