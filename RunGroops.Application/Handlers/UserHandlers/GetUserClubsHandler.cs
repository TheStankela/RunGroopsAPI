using MediatR;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class GetUserClubsHandler : IRequestHandler<GetUserClubsQuery, ICollection<Club>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserClubsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<Club>> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserClubsAsync(request.UserId);
        }
    }
}
