namespace EnShop.Ordering.API.Application.Queries;

public interface IOrderQueries
{
    Task<IEnumerable<DetailsViewModel>> GetDetailsByFilterAsync(string filter);
    Task<DetailsViewModel> GetAggregateAsync(int id);
}
