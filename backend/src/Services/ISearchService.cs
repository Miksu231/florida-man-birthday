using FloridaMan.Models;

namespace FloridaMan.Services;

public interface ISearchService
{
    public Task<List<DisplayResult>> CrawlTodaysFloridaMan();

    public Task<List<DisplayResult>> CrawlDateFloridaMan(string month, string day);
}