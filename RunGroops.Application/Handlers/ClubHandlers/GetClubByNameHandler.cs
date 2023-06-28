using MediatR;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetClubByNameHandler : IRequestHandler<GetClubByNameQuery, Club>
    {
        private readonly IClubRepository _clubRepository;
        public GetClubByNameHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<Club> Handle(GetClubByNameQuery request, CancellationToken cancellationToken)
        {
            return await _clubRepository.GetClubByNameAsync(request.name);
        }
    }
}
