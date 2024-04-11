using Google.Apis.CustomSearchAPI.v1.Data;

namespace FloridaMan.Services;

public interface IQueryService
{
    public Task<List<Result>> ExecuteQuery(string query);
}