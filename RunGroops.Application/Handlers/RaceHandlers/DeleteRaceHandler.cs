
using MediatR;
using RunGroops.Application.Commands.RaceCommands;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class DeleteRaceHandler : IRequestHandler<DeleteRaceCommand, bool>
    {
        private readonly IRaceRepository _raceRepository;
        public DeleteRaceHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<bool> Handle(DeleteRaceCommand request, CancellationToken cancellationToken)
        {
            if (await _raceRepository.RaceExists(request.raceId, null))
            {
                var raceToDelete = await _raceRepository.GetRaceByIdAsync(request.raceId);

                return await _raceRepository.DeleteRaceAsync(raceToDelete);
            }
            return false;
        }
    }
}
