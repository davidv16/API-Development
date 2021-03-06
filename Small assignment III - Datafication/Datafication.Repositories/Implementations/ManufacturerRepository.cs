using System.Linq;
using Datafication.Models.Dtos;
using Datafication.Repositories.Contexts;
using Datafication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datafication.Repositories.Implementations
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IceCreamDbContext _dbContext;

        public ManufacturerRepository(IceCreamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ManufacturerDetailsDto GetManufacturerById(int id) 
        {
            var manufacturer = _dbContext
                .Manufacturers
                .Include(ic => ic.IceCreams)
                .Include(ca => ca.CategoryOccurrance)
                .Where(n => n.Id == id)
                .Select(n => new ManufacturerDetailsDto
                {
                
                    Id = n.Id,
                    Name = n.Name,
                    Bio = n.Bio,
                    ExternalUrl = n.ExternalUrl,
                    IceCreams = n.IceCreams.Select(ic => new IceCreamDto
                    {
                        Id = ic.Id,
                        Name = ic.Name,
                        Description = ic.Description
                    
                    }),
                    CategoryOccurrance = n.IceCreams.Select(i => i.Categories).Distinct().Count()
                }).FirstOrDefault();
            
            return manufacturer;
        }
    }
}