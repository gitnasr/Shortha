﻿
using Shortha.Domain;

namespace Shortha.Application
{
    public interface IURL
    {
        public Url? GetUrl(string url);
        public Url? GetUrlById(Guid id);
        public Url? GetUrlByShortUrl(string shortUrl);
        public Task<IEnumerable<Url>>? GetUrlsByUserId(string userId);
        public Task<IEnumerable<Url>>? GetUrlsByUserId(string userId, int pageNumber, int pageSize);

        public Url CreateUrl(Url url, string? customHash = null);
        public bool IsHashExists(string hash);


    }
}
