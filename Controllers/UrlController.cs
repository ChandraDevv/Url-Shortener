using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        public readonly UrlShortnerService _service;

        public UrlController(UrlShortnerService urlShortnerService)
        {
            _service = urlShortnerService;
        }

        [HttpPost]
        [Route("/shorten")]
        public IActionResult ShortenUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                return BadRequest("URL cannot be null or empty");
            }

            try
            {
                string shortUrl = _service.ShortenUrl(longUrl);
                return Ok(new { ShortUrl = shortUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetPage")]
        public IActionResult GetLongUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                return BadRequest("Short URL cannot be null or empty");
            }

            try
            {
                string longUrl = _service.GetLongUrl(shortUrl);
                return Redirect(longUrl);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Short URL not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}