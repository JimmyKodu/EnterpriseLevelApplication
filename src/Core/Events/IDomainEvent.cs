using MediatR;

namespace EnterpriseApp.Core.Events;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
