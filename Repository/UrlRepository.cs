using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Shortha.Helpers;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Repository
{
    public class UrlRepository : IURL
    {
        private readonly AppDB AppDB;
        public UrlRepository(AppDB appDB)
        {

            AppDB = appDB;

        }
        public Url? GetUrl(string url)
        {
            return AppDB.Urls.FirstOrDefault(x => x.OriginalUrl == url);

        }
        public Url? GetUrlById(Guid id)
        {
            return AppDB.Urls.FirstOrDefault(x => x.Id == id);
        }
        public Url? GetUrlByShortUrl(string shortUrl)
        {
            return AppDB.Urls.FirstOrDefault(x => x.ShortHash == shortUrl);
        }
        public async Task<IEnumerable<Url>>? GetUrlsByUserId(string userId)
        {
            return await AppDB.Urls.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Url>>? GetUrlsByUserId(string userId, int pageNumber, int pageSize)
        {

            return await AppDB.Urls.Where(x => x.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        [AllowAnonymous]
        public Url CreateUrl(Url url, string? customHash = null)
        {
            if (customHash == null)
            {

                ShortHash hashService = new();
                string urlHash = hashService.GenerateHash(url.OriginalUrl);

                // Check if the hash already exists: 1/300,000 Probability of collision, but we can handle it
                while (IsHashExists(urlHash))
                {
                    urlHash = hashService.GenerateHash(url.OriginalUrl);
                }

                url.ShortHash = urlHash;
            }

            this.AppDB.Urls.Add(url);
            this.AppDB.SaveChanges();
            return url;
        }



        public bool IsHashExists(string hash)
        {
            return AppDB.Urls.Any(x => x.ShortHash == hash);
        }


    }

}
