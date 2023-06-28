using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.ClubCommands
{
    public record AddClubCommand(
        ClubRequest ClubRequest) : IRequest<bool>;
}
