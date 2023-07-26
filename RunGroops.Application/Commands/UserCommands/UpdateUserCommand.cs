using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.UserCommands
{
    public record UpdateUserCommand(
        string UserId,
        UpdateUserRequest Request) : IRequest<bool>;
}
