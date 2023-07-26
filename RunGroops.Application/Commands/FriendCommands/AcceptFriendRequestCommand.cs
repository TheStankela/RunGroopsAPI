using MediatR;

namespace RunGroops.Application.Commands.FriendCommands
{
    public record AcceptFriendRequestCommand(string FromUserId) : IRequest<bool>;
}
