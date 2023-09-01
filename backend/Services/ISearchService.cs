using System.Collections.Generic;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace FloridaMan;

public interface ISearchService
{
    public List<DisplayResult> CrawlTodaysFloridaMan();
}