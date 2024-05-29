using System.ComponentModel.DataAnnotations;

namespace EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
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

        var domainEvent = new AggregateCreatedDomainEvent(this);
        this.AddDomainEvent(domainEvent);
    }

    public void ChangeAggregate()
    {
        AddDomainEvent(new AggregateChangedDomainEvent(this));
    }

    public void UpdateAggregate()
    {
        AddDomainEvent(new AggregateUpdatedDomainEvent(this));
    }
}
