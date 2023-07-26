using MediatR;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class GetUserRacesHandler : IRequestHandler<GetUserRacesQuery, ICollection<Race>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserRacesHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<Race>> Handle(GetUserRacesQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserRacesAsync(request.UserId);
        }
    }
}
