using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using datingapp.api.Helpers;
using datingapp.api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace datingapp.api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            _cloudinary = new Cloudinary(new Account
                            (
                                 config.Value.CloudName,
                                 config.Value.ApiKey,
                                 config.Value.ApiSecret
                            ));
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            if (file.Length <= 0) return null;
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName),
                Transformation = new Transformation().Height("500").Width("500")
                                                     .Crop("fill").Gravity("face")
            };
            return await _cloudinary.UploadAsync(uploadParams);
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
