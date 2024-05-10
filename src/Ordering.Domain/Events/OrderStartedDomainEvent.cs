﻿
namespace EnShop.Ordering.Domain.Events;

/// <summary>
/// Event used when an order is created
/// </summary>
public record class OrderStartedDomainEvent(Order Order) : INotification;
