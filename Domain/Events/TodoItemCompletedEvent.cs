using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{

    public class CustomerCompletedEvent : DomainEvent
    {
        public CustomerCompletedEvent(Customer item)
        {
            Item = item;
        }

        public Customer Item { get; }
    }
}