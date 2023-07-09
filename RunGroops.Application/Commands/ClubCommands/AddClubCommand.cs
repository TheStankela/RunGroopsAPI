using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Models;

namespace RunGroops.Application.Commands.ClubCommands
{
    public record AddClubCommand(
        ClubRequest ClubRequest,
        IFormFile file) : IRequest<bool>;
}
