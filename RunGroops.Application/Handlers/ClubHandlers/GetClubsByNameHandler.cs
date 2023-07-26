using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetClubsByNameHandler : IRequestHandler<GetClubsByNameQuery, PagedList<Club>>
    {
        private readonly IClubRepository _clubRepository;
        public GetClubsByNameHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<PagedList<Club>> Handle(GetClubsByNameQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Club> query = await _clubRepository.GetClubsByNameAsync(request.Name);

            return await PagedList<Club>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
