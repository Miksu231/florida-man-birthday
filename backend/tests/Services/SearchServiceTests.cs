using AutoFixture;
using FloridaMan.Services;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace FloridaMan.Tests.Services;

public class SearchServiceTests
{
    private readonly Fixture _fixture = new();
    private readonly ISearchService _searchService;
    private readonly Mock<IQueryService> _mockQueryService = new();

    public SearchServiceTests()
    {
        _searchService = new SearchService(_mockQueryService.Object);
    }

    [Fact]
    public async Task CrawlTodaysFloridaMan_GivenNonFilteredResults_ReturnAllResults()
    {
        var totalResults = 10;
        _mockQueryService.Setup(x => x.ExecuteQuery(It.IsAny<string>())).ReturnsAsync(GetSearchResults(totalResults));

        var results = await _searchService.CrawlTodaysFloridaMan();

        Assert.Equal(totalResults, results.Count);
    }

    [Fact]
    public async Task CrawlTodaysFloridaMan_GivenNonFilteredResults_FilterUnwantedResults()
    {
        var totalResults = 5;
        _mockQueryService.Setup(x => x.ExecuteQuery(It.IsAny<string>())).ReturnsAsync(GetSearchResults(totalResults, true));

        var results = await _searchService.CrawlTodaysFloridaMan();

        Assert.Equal(totalResults, results.Count);
        Assert.True(!results.Any(x => x.Link.Contains("reddit")));
        Assert.True(!results.Any(x => x.Link.Contains("pinterest")));
        Assert.True(!results.Any(x => x.Title.Contains("...")));
        Assert.True(!results.Any(x => x.Title.Contains("Published")));
        Assert.True(!results.Any(x => x.Title.Contains("birthday")));
    }

    private List<Result> GetSearchResults(int amount = 10, bool addFilterableResults = false)
    {
        var originalResults = _fixture
            .Build<Result>()
            .With(x => x.Title, RandomString(10))
            .With(x => x.Snippet, RandomString(30))
            .With(x => x.Link, RandomString(40))
            .With(x => x.Pagemap, new Dictionary<string, object> { { "metatags", new List<object> { new() { } } } })
            .CreateMany(amount);

        if (addFilterableResults)
        {
            var filterResults = new List<Result>{
                new() {
                    Link = "www.website.com",
                    Title = "This title cuts off...",
                    Snippet = "Lorem ipsum",
                    Pagemap = new Dictionary<string, object>{{"metatags", new List<object>{new(){}}}}
                },
                new() {
                    Link = "www.website.com",
                    Title = "Archived article (Published 1997)",
                    Snippet = "Lorem ipsum",
                    Pagemap = new Dictionary<string, object>{{"metatags", new List<object>{new(){}}}}
                },
            };
            originalResults = originalResults.Concat(filterResults);
        }
        return originalResults.ToList();

    }

    private static readonly Random random = new();

    public static string RandomString(int length)
    {
        // Removed i so impossible to generate forbidden words, even though it is astronomically unlikely to happen
        const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}