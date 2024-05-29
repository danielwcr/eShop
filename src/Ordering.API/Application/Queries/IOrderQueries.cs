namespace EnShop.Ordering.API.Application.Queries;

public interface IOrderQueries
{
    Task<GetQueryDto> GetQueryAsync(int id);

    Task<IEnumerable<ListQueryDto>> ListQueryAsync(string userId);
}
