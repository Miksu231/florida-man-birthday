using System.Net;
using FloridaMan.Models;
using FloridaMan.Services;
using Google;
using Microsoft.AspNetCore.Mvc;

namespace FloridaMan.Controllers;

[Route("floridaman")]
public class FloridaManController(ISearchService searchService) : ControllerBase
{
    private readonly ISearchService _searchService = searchService;

    [HttpPost("today")]
    public async Task<IActionResult> GetTodaysFloridaMan()
    {
        try
        {
            var results = await _searchService.CrawlTodaysFloridaMan();
            return Ok(results);
        }
        catch (GoogleApiException e) when (e.HttpStatusCode == HttpStatusCode.TooManyRequests)
        {
            return new ConflictResult();
        }
    }

    [HttpPost("date")]
    public async Task<IActionResult> GetDateFloridaMan([FromBody] DateDto dateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var results = await _searchService.CrawlDateFloridaMan(dateDto.Month, dateDto.Day);
            return Ok(results);
        }
        catch (GoogleApiException e) when (e.HttpStatusCode == HttpStatusCode.TooManyRequests)
        {
            return new ConflictResult();
        }
    }
}