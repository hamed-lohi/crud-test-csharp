using Application.Common.Models;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.EventHandlers
{

    public class CustomerCompletedEventHandler : INotificationHandler<DomainEventNotification<CustomerCompletedEvent>>
    {
        private readonly ILogger<CustomerCompletedEventHandler> _logger;

        public CustomerCompletedEventHandler(ILogger<CustomerCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CustomerCompletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}