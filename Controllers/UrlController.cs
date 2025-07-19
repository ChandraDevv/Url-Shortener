using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    [ApiController]
    class UrlController : ControllerBase
    {
        public readonly UrlShortnerService _service;

        public UrlController(UrlShortnerService urlShortnerService)
        {
            _service = urlShortnerService;
        }

        [HttpPost("shorten")]
        [Route("/shorten")]
        public IActionResult ShortenUrl(string longUrl)
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
    }
}