using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _awsS3;

        public ImageService(IAmazonS3 awsS3)
        {
            _awsS3 = awsS3;
        }

        public async Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            var imageLocation = $"uploads/{image.FileName}";

            using (var stream = image.OpenReadStream())
            {
                var putRequest = new PutObjectRequest
                {
                    Key = imageLocation,
                    BucketName = "just-trade-it",
                    InputStream = stream,
                    AutoCloseStream = true,
                    ContentType = image.ContentType
                };

                await _awsS3.PutObjectAsync(putRequest);

                return $"https://just-trade-it.s3.eu-west-1/{imageLocation}";
            }
        }
    }
}