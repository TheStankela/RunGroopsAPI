using MediatR;

namespace RunGroops.Application.Commands.RaceCommands
{
    public record DeleteRaceCommand(int raceId) : IRequest<bool>;
}
