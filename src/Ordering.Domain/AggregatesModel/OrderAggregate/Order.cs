using System.ComponentModel.DataAnnotations;

namespace EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public string UserId { get; private set; }

    protected Order()
    {
    }

    public Order(string userId) : this()
    {
        UserId = userId;

        AddDomainEvent(new AggregateCreatedDomainEvent(this));
    }

    public void ChangeAggregate()
    {
        AddDomainEvent(new AggregateChangedDomainEvent(this));
    }

    public void UpdateAggregate(string userId)
    {
        UserId = userId;

        AddDomainEvent(new AggregateUpdatedDomainEvent(this));
    }
}
