﻿namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateCreatedIntegrationEvent(int OrderId) : IntegrationEvent;
