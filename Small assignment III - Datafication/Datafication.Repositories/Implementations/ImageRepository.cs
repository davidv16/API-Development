using System.Collections.Generic;
using System.Linq;
using Datafication.Models.Dtos;
using Datafication.Models.InputModels;
using Datafication.Repositories.Contexts;
using Datafication.Repositories.Entities;
using Datafication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datafication.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IceCreamDbContext _dbContext;
        public ImageRepository(IceCreamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: Finish
        public ImageDetailsDto GetImageById(int id) 
        {
            var Image = _dbContext.Images.Where(i => i.Id == id).Select(i => new ImageDetailsDto 
            {
                Id = i.Id,
                Url = i.Url,
                //IceCream = i.IceCream
            }).FirstOrDefault();
            return Image; 
        }
        
        public IEnumerable<ImageDto> GetAllImagesByIceCreamId(int iceCreamId) 
        {
            var Images = _dbContext.Images.Where(i => i.IceCreamId == iceCreamId).Select(i => new ImageDto 
            {
                Id = i.Id,
                Url = i.Url,
            }).ToList();
            return Images;
        }
        
        public int CreateNewImage(ImageInputModel image) 
        {
            var nextId = _dbContext.Images.Max(table => table.Id) + 1;
            _dbContext.Images.Add(new Image
            {
                Id = nextId,
                Url = image.Url,
                IceCreamId = image.IceCreamId
            });

            _dbContext.SaveChanges();
            return nextId;
        }
        
    }
}