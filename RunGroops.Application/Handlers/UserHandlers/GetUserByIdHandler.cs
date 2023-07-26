using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.UserQueries;
using RunGroops.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByIdAsync(request.UserId);
            if (result is null)
                return null;

            var user = new UserResponse(
                result.Id,
                result.UserName,
                result.Mileage,
                result.Pace,
                result.ImageURL);

            return user;
        }
    }
}
