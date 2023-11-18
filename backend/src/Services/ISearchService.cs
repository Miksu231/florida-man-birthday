using FloridaMan.Models;
using Google.Apis.CustomSearchAPI.v1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FloridaMan.Services;

public interface ISearchService
{
    public Task<List<DisplayResult>> CrawlTodaysFloridaMan();

    public Task<List<DisplayResult>> CrawlDateFloridaMan(string month, string day);
}