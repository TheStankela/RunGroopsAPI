using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, ICollection<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICollection<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUsersAsync(request.PageNumber);

            List<UserResponse> users = new();
            foreach (var user in result)
            {
                var userToAdd = new UserResponse
                {
                    Id= user.Id,
                    Mileage = user.Mileage,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    ImageURL = "https://img.freepik.com/free-vector/businessman-character-avatar-isolated_24877-60111.jpg?w=2000"
                };
                users.Add(userToAdd);
            }

            return users;
        }
    }
}
