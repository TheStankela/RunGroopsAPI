using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.RaceCommands
{
    public record AddRaceCommand(
        RaceRequest RaceRequest,
        IFormFile File) : IRequest<bool>;
}
