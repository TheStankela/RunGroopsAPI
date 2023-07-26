using MediatR;
using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRacesHandler : IRequestHandler<GetRacesQuery, PagedList<Race>>
    {
        private readonly IRaceRepository _raceRepository;

        public GetRacesHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<PagedList<Race>> Handle(GetRacesQuery request, CancellationToken cancellationToken)
        {
            var query = await _raceRepository.GetRacesAsync();

            return await PagedList<Race>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
