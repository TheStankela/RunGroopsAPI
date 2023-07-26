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
    public class GetUsersByNameHandler : IRequestHandler<GetUsersByNameQuery, PagedList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<PagedList<UserResponse>> Handle(GetUsersByNameQuery request, CancellationToken cancellationToken)
        {
            var query = await _userRepository.GetUsersByNameAsync(request.UserName);

            var selectQuery = query.Select(user => new UserResponse(
                user.Id,
                user.UserName,
                user.Mileage,
                user.Pace,
                user.ImageURL));

            return await PagedList<UserResponse>.CreateAsync(selectQuery, request.Page, request.PageSize);
        }
    }
}
