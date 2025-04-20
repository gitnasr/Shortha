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
                VisitDate = DateTime.UtcNow,

            };

            _dbContext.Visits.Add(newVisit);
            int added = _dbContext.SaveChanges();

            return added > 0;

        }

        public Visit? GetVisitById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Visit>>? GetVisitsByShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Visit>>? GetVisitsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
