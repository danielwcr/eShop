namespace EnShop.Ordering.API.Application.Queries;

public interface IOrderQueries
{
    Task<IEnumerable<ListQueryDto>> ListQueryAsync(string userId);
}
