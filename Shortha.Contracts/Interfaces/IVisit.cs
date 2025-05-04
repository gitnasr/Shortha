using Shortha.Domain;

namespace Shortha.Application
{
    public interface IVisit
    {
        public Visit? GetVisitById(int id);

        public Task<IEnumerable<Visit>>? GetVisitsByShortUrl(string shortUrl);


        public Task<IEnumerable<Visit>>? GetVisitsByUserId(string userId);

        public bool CreateVisit(Tracker visit, Guid urlId);

    }
}
