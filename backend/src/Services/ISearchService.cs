using FloridaMan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FloridaMan.Services;

public interface ISearchService
{
    public Task<List<DisplayResult>> CrawlTodaysFloridaMan();

    public Task<List<DisplayResult>> CrawlDateFloridaMan(string month, string day);
}