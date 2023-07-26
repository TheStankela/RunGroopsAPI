using MediatR;
using RunGroops.Application.Commands.RaceCommands;
using RunGroops.Application.Services;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class UpdateRaceHandler : IRequestHandler<UpdateRaceCommand, bool>
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public UpdateRaceHandler(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
        }
        public async Task<bool> Handle(UpdateRaceCommand request, CancellationToken cancellationToken)
        {
            if(!await _raceRepository.RaceExists(request.RaceId, null))
                return false;
            
            var raceToUpdate =await _raceRepository.GetRaceByIdAsync(request.RaceId);

            var deletePhotoResult = await _photoService.DeletePhotoAsync(raceToUpdate.ImageURL);

            if(deletePhotoResult.Error is not null)
                return false;

            var updatePhotoResult = await _photoService.AddPhotoAsync(request.RaceRequest.File);

            if(updatePhotoResult.Error is not null || updatePhotoResult.Uri is null)
                return false;

            raceToUpdate.Name = request.RaceRequest.Name;
            raceToUpdate.RaceCategory = request.RaceRequest.RaceCategory;
            raceToUpdate.Description= request.RaceRequest.Description;
            raceToUpdate.Address.City = request.RaceRequest.Address.City;
            raceToUpdate.Address.Country = request.RaceRequest.Address.Country;
            raceToUpdate.Address.Zip = request.RaceRequest.Address.Zip;
            raceToUpdate.Address.Street = request.RaceRequest.Address.Street;
            raceToUpdate.ImageURL = updatePhotoResult.Uri.ToString();

            return await _raceRepository.UpdateRaceAsync(raceToUpdate);
        }
    }
}
