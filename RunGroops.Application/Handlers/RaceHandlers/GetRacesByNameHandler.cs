using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRacesByNameHandler : IRequestHandler<GetRacesByNameQuery, PagedList<Race>>
    {
        private readonly IRaceRepository _raceRepository;
        public GetRacesByNameHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<PagedList<Race>> Handle(GetRacesByNameQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Race> query = await _raceRepository.GetRacesByNameAsync(request.RaceName);

            return await PagedList<Race>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
