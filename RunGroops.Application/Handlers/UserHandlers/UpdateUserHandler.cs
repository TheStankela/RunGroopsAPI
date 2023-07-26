using MediatR;
using RunGroops.Application.Commands.UserCommands;
using RunGroops.Application.Models;
using RunGroops.Application.Services;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;

        public UpdateUserHandler(IPhotoService photoService, IUserRepository userRepository)
        {
            _photoService = photoService;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExists(request.UserId))
                return false;

            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user is null)
                return false;

            if(user.ImageURL is not null)
            {
                var imageDeleteResult = await _photoService.DeletePhotoAsync(user.ImageURL);
                
                if (imageDeleteResult.Error is not null)
                    return false;
            }

            var imageUploadResult = await _photoService.AddPhotoAsync(request.Request.File);

            if (imageUploadResult.Error is not null || imageUploadResult.Uri is null)
                return false;
            

            user.UserName = request.Request.UserName;
            user.Pace = request.Request.Pace;
            user.UserCategory = request.Request.UserCategory;
            user.Mileage = request.Request.Mileage;
            user.ImageURL = imageUploadResult.Uri.ToString();

            return await _userRepository.UpdateUser(user);
        }
    }
}
