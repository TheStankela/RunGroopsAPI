using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.ClubCommands
{
    public record UpdateClubCommand(
       int clubId,UpdateClubRequest UpdateClubRequest) : IRequest<bool>;
}
