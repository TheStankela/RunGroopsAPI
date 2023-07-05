using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace RunGroops.Application.Services
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
