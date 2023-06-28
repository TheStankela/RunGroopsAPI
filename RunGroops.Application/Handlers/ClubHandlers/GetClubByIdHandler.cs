using MediatR;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetClubByIdHandler : IRequestHandler<GetClubByIdQuery, Club>
    {
        private readonly IClubRepository _clubRepository;
        public GetClubByIdHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<Club> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
        {
            return await _clubRepository.GetClubByIdAsync(request.id);
        }
    }
}
