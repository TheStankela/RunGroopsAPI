using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetAllUserRacesHandler : IRequestHandler<GetAllUserRacesQuery, ICollection<Race>>
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetAllUserRacesHandler(IRaceRepository raceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ICollection<Race>> Handle(GetAllUserRacesQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return null;
            }
            return await _raceRepository.GetAllUserRacesAsync(userId);
        }
    }
}
