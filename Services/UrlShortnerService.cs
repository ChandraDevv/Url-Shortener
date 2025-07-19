using System;
using System.Collections.Concurrent;

namespace UrlShortner.Services
{

     public class UrlShortnerService
    {
        private readonly ConcurrentDictionary<string, string> _urlStore = new();
        private const string BaseUrl = "http://anya.ly/";

        public string ShortenUrl(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                throw new ArgumentException("URL cannot be null or empty", nameof(longUrl));
            }

            var shortUrl = GenerateShortUrl(longUrl);
            _urlStore[shortUrl] = longUrl;
            return shortUrl;
        }

        private string GenerateShortUrl(string longUrl)
        {
            var hash = longUrl.GetHashCode().ToString("X");
            return $"{BaseUrl}{hash}";
        }

        public string GetLongUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl) || !shortUrl.StartsWith(BaseUrl))
            {
                throw new ArgumentException("Invalid short URL", nameof(shortUrl));
            }

            var hash = shortUrl.Substring(BaseUrl.Length);
            if (_urlStore.TryGetValue(shortUrl, out var longUrl))
            {
                return longUrl;
            }

            throw new KeyNotFoundException("Short URL not found");
        }
    }
}