using System.Reflection;
using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.UnitTests.Builders;

public class OrderBuilder
{
    private string _userId;

    public static OrderBuilder For<T>() where T : WellKnownOrder, new()
    {
        var wellKnownOrder = new T();
        return new OrderBuilder()
            .WithUserId(wellKnownOrder.UserId);
    }

    public OrderBuilder WithUserId(string userId)
    {
        _userId = userId;
        return this;
    }

    public Order Build()
    {
        var order = (Order)Activator.CreateInstance(typeof(Order), true);

        typeof(Order)
            .GetProperty(nameof(Order.UserId))
            .SetValue(order, _userId);

        return order;
    }
}

public abstract class WellKnownOrder
{
    public abstract string UserId { get; }
}

public class SimpleOrder : WellKnownOrder
{
    public override string UserId => "1";
}

