using Microsoft.EntityFrameworkCore;
using Shortha.DTO;
using Shortha.Infrastructure.Configurations;

namespace Shortha.Infrastructure.Repository
{
    public class PackagesRepository
    {
        private readonly AppDB _context;

        public PackagesRepository(AppDB context)
        {
            _context = context;

        }

        public async Task<IEnumerable<PackageInfo>> GetAllPackages()
        {
            return await _context.Packages
                .Where(p => p.IsActive)
                .Select(p => new PackageInfo()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.price
                })
                .ToListAsync();
        }

        public async Task<PackageInfo?> GetPackageById(int packageId)
        {


            return await _context.Packages
                .Where(p => p.IsActive && p.Id == packageId)
                .Select(p => new PackageInfo()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.price
                })
                .FirstOrDefaultAsync();


        }
    }
}
