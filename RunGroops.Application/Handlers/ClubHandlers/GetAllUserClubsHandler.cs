using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using System.Collections;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.ClubHandlers
{
    public class GetAllUserClubsHandler : IRequestHandler<GetAllUserClubsQuery, ICollection<Club>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClubRepository _clubRepository;

        public GetAllUserClubsHandler(IHttpContextAccessor httpContextAccessor, IClubRepository clubRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _clubRepository = clubRepository;
        }

        public async Task<ICollection<Club>> Handle(GetAllUserClubsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
            {
                return null;
            }
            return await _clubRepository.GetAllUserClubsAsync(userId);
        }
    }
}
