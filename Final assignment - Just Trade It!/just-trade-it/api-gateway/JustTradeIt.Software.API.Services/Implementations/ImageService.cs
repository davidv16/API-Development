using System;
using System.Threading.Tasks;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        // TODO: implement
        public Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}