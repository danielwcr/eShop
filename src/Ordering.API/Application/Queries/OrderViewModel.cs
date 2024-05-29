namespace EnShop.Ordering.API.Application.Queries;

public record GetQueryDto
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string status { get; init; }
}

public record ListQueryDto
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string status { get; init; }
}
