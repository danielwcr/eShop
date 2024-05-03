namespace EnShop.Ordering.API.Application.Queries;

public record Order
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string status { get; init; }
    public string description { get; init; }
}

public record OrderSummary
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string status { get; init; }
    public double total { get; init; }
}
