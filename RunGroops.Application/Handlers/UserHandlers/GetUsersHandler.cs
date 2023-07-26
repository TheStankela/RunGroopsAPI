using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, PagedList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = await _userRepository.GetUsersAsync();

            var selectUserQuery = usersQuery.Select(u => new UserResponse(
                u.Id,
                u.UserName,
                u.Mileage,
                u.Pace,
                u.ImageURL
                ));

            var result = await PagedList<UserResponse>.CreateAsync(selectUserQuery, request.PageNumber, request.pageSize);

            return result;
        }
    }
}
