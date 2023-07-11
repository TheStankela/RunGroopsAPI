using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Application.Services;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class UpdateClubHandler : IRequestHandler<UpdateClubCommand, bool>
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateClubHandler(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            if (!await _clubRepository.ClubExists(request.clubId, null)) return false;

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null) return false;

            var clubToUpdate = await _clubRepository.GetClubByIdAsync(request.clubId);
            if(clubToUpdate == null) return false;

            if(userId != clubToUpdate.AppUserId) return false;

            var result = await _photoService.DeletePhotoAsync(clubToUpdate.ImageURL);

            if(result.Error != null) return false;

            var photoResult = _photoService.AddPhotoAsync(request.UpdateClubRequest.File);

            if (!photoResult.IsFaulted || !photoResult.IsCanceled)
            {
                clubToUpdate.Name = request.UpdateClubRequest.Name;
                clubToUpdate.Description = request.UpdateClubRequest.Description;
                clubToUpdate.ClubCategory = request.UpdateClubRequest.ClubCategory;
                clubToUpdate.Address.Country = request.UpdateClubRequest.Address.Country;
                clubToUpdate.Address.City = request.UpdateClubRequest.Address.City;
                clubToUpdate.Address.Street = request.UpdateClubRequest.Address.Street;
                clubToUpdate.Address.Zip = request.UpdateClubRequest.Address.Zip;
                clubToUpdate.ImageURL = photoResult.Result.Uri.ToString();
                return await _clubRepository.UpdateClubAsync(clubToUpdate);
            }
            return false;
        }
    }
}
