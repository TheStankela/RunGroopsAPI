using MediatR;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class UpdateClubHandler : IRequestHandler<UpdateClubCommand, bool>
    {
        private readonly IClubRepository _clubRepository;
        public UpdateClubHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<bool> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            var clubToUpdate = await _clubRepository.GetClubByIdAsync(request.clubId);
            if(clubToUpdate == null) return false;

            clubToUpdate.Name = request.UpdateClubRequest.Name;
            clubToUpdate.Description = request.UpdateClubRequest.Description;
            clubToUpdate.ClubCategory = request.UpdateClubRequest.ClubCategory;
            clubToUpdate.ImageURL = request.UpdateClubRequest.ImageURL;

            return await _clubRepository.UpdateClubAsync(clubToUpdate);
        }
    }
}
