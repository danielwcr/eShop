namespace EnShop.Ordering.API.Application.Queries;

public interface IOrderQueries
{
    Task<IEnumerable<DetailsViewModel>> GetCollectionByFilterAsync(string filter);
    Task<DetailsViewModel> GetSingleObjectByFilterAsync(string filter);
}
