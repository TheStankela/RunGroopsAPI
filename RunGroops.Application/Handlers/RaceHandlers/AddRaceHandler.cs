using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using RunGroops.Application.Commands.RaceCommands;
using RunGroops.Application.Services;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class AddRaceHandler : IRequestHandler<AddRaceCommand, bool>
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddRaceHandler(IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(AddRaceCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) 
                return false;

            var uploadResult = await _photoService.AddPhotoAsync(request.File);

            if (string.IsNullOrEmpty(uploadResult.Uri.ToString()))
                return false;

            var raceToAdd = new Race
            {
                Name = request.RaceRequest.Name,
                Description = request.RaceRequest.Description,
                RaceCategory = request.RaceRequest.RaceCategory,
                Address = new Address
                {
                    City = request.RaceRequest.Address.City,
                    Country = request.RaceRequest.Address.Country,
                    Zip = request.RaceRequest.Address.Zip,
                    Street = request.RaceRequest.Address.Street
                },
                AppUserId = userId,
                ImageURL = uploadResult.Uri.ToString()
            };

            return await _raceRepository.AddRaceAsync(raceToAdd);
        }
    }
}
