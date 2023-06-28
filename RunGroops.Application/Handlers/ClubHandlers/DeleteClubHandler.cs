using MediatR;
using RunGroops.Application.Commands.ClubCommands;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHandlers
{
    public class DeleteClubHandler : IRequestHandler<DeleteClubCommand, bool>
    {
        private readonly IClubRepository _clubRepository;
        public DeleteClubHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<bool> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            if (!await _clubRepository.ClubExists(request.id, null)) 
                return false;

            var clubToDelete = await _clubRepository.GetClubByIdAsync(request.id);

            return await _clubRepository.DeleteClubAsync(clubToDelete);
        }
    }
}
