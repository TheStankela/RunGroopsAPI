using MediatR;

namespace RunGroops.Application.Commands.ClubCommands
{
    public record DeleteClubCommand(
        int id) : IRequest<bool>;
}
