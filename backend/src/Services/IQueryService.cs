using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1.Data;

public interface IQueryService
{
    public Task<List<Result>> ExecuteQuery(string query);
}