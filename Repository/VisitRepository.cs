using Microsoft.EntityFrameworkCore;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Repository
{
    public class VisitRepository : IVisit
    {
        private readonly AppDB _dbContext;
        public VisitRepository(AppDB dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateVisit(Tracker visit, Guid urlId)
        {

            Visit newVisit = new Visit
            {
                Browser = visit.BrowserName,
                Os = visit.OSName,
                DeviceBrand = visit.DeviceBrand,
                DeviceType = visit.DeviceType,
                IpAddress = visit.IpAddress,
                Country = visit.Country,
                Region = visit.Region,
                City = visit.City,
                UrlId = urlId,
                UserAgent = visit.UserAgent,

            };

            _dbContext.Visits.Add(newVisit);
            int added = _dbContext.SaveChanges();

            return added > 0;

        }

        public Visit? GetVisitById(int id)
        {

            return _dbContext.Visits.FirstOrDefault(v => v.Id == id);

        }

        public async Task<IEnumerable<Visit>>? GetVisitsByShortUrl(string shortUrl)
        {
            return await _dbContext.Visits
                .Include(v => v.Url)
                .Where(v => v.Url.ShortHash == shortUrl).ToListAsync();

        }

        public async Task<IEnumerable<Visit>>? GetVisitsByUserId(string userId)
        {
            return await _dbContext.Visits
                .Include(v => v.Url)
                .Where(v => v.Url.UserId == userId).ToListAsync();
        }
    }
}
