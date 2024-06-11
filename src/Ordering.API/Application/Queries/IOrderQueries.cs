namespace EnShop.Ordering.API.Application.Queries;

public interface IOrderQueries
{
    Task<IEnumerable<Details>> GetDetailsByFilterAsync(string filter);
}
