using MediatR;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Application.Helpers;
using RunGroops.Application.Services;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class AddClubHandler : IRequestHandler<AddClubCommand, bool>
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubMapper _mapper;
        private readonly IPhotoService _photoService;

        public AddClubHandler(IClubRepository clubRepository, IClubMapper mapper, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<bool> Handle(AddClubCommand request, CancellationToken cancellationToken)
        {

            var clubMapped = _mapper.MapClubRequestToClub(request.ClubRequest);

            return await _clubRepository.AddClubAsync(clubMapped);
        }
    }
}
