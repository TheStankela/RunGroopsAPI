using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Application.Helpers;
using RunGroops.Application.Services;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class AddClubHandler : IRequestHandler<AddClubCommand, bool>
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public AddClubHandler(IClubRepository clubRepository, IClubMapper mapper, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public async Task<bool> Handle(AddClubCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId is null)
                return false;
            
            var clubMapped = _mapper.MapClubRequestToClub(request.ClubRequest, userId);
            
            var uploadResult = _photoService.AddPhotoAsync(request.file);

            clubMapped.ImageURL = uploadResult.Result.Url.ToString();

            return await _clubRepository.AddClubAsync(clubMapped);
        }
    }
}
