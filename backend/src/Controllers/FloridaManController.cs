using FloridaMan.Models;
using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;

namespace FloridaMan.Controllers;

[Route("floridaman")]
public class FloridaManController(ISearchService searchService) : ControllerBase
{
    private readonly ISearchService _searchService = searchService;

    [HttpPost("today")]
    public async Task<IActionResult> GetTodaysFloridaMan()
    {
        var results = await _searchService.CrawlTodaysFloridaMan();
        return Ok(results);
    }

    [HttpPost("date")]
    public async Task<IActionResult> GetDateFloridaMan([FromBody] DateDto dateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var results = await _searchService.CrawlDateFloridaMan(dateDto.Month, dateDto.Day);
        return Ok(results);
    }
}