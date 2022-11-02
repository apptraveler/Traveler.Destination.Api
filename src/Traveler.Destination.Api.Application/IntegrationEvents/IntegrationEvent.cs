using System;
using MediatR;

namespace Traveler.Destination.Api.Application.IntegrationEvents;

public abstract class IntegrationEvent : INotification
{
    public DateTime TimeStamp { get; }

    protected IntegrationEvent()
    {
        TimeStamp = DateTime.UtcNow;
    }
}