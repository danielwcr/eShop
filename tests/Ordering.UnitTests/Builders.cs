using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace EnShop.Ordering.UnitTests.Domain;

public class OrderBuilder
{
    private readonly Order order;

    public OrderBuilder()
    {
        order = new Order("userId");
    }

    public Order Build()
    {
        return order;
    }
}
