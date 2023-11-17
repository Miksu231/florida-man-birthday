using FloridaMan.Models;
using System.Collections.Generic;


namespace FloridaMan.Services;

public interface ISearchService
{
    public List<DisplayResult> CrawlTodaysFloridaMan();

    public List<DisplayResult> CrawlDateFloridaMan(string month, string day);
}