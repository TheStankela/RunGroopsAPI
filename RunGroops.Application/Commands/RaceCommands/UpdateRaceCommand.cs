using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.RaceCommands
{
    public record UpdateRaceCommand(
        int RaceId,
        UpdateRaceRequest RaceRequest
        ) : IRequest<bool>;
}
