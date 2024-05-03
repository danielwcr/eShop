using System.ComponentModel.DataAnnotations;

namespace EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

public class Order
    : Entity, IAggregateRoot
{
    public DateTime OrderDate { get; private set; }

    public string UserId { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public string Description { get; private set; }

    protected Order()
    {
    }

    public Order(string userId, string cardNumber) : this()
    {
        OrderStatus = OrderStatus.Submitted;
        OrderDate = DateTime.UtcNow;
        UserId = userId;

        AddOrderStartedDomainEvent(userId, cardNumber);
    }

    public void SetStockConfirmedStatus()
    {
        if (OrderStatus == OrderStatus.AwaitingValidation)
        {
            AddDomainEvent(new OrderStatusChangedToStockConfirmedDomainEvent(Id));

            OrderStatus = OrderStatus.StockConfirmed;
            Description = "All the items were confirmed with available stock.";
        }
    }

    public void SetCancelledStatus()
    {
        if (OrderStatus == OrderStatus.Paid ||
            OrderStatus == OrderStatus.Shipped)
        {
            StatusChangeException(OrderStatus.Cancelled);
        }

        OrderStatus = OrderStatus.Cancelled;
        Description = $"The order was cancelled.";
        AddDomainEvent(new OrderCancelledDomainEvent(this));
    }

    private void AddOrderStartedDomainEvent(string userId, string cardNumber)
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, cardNumber);

        this.AddDomainEvent(orderStartedDomainEvent);
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus} to {orderStatusToChange}.");
    }
}
