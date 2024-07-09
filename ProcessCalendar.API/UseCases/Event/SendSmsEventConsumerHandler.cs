using Common.Abstractions.IntegrationEvents;
using MediatR;

namespace ProcessCalendar.API.UseCases.Event
{
    public class SendSmsEventConsumerHandler : IRequestHandler<DomainEvent.SmsNotificationEvent>
    {
        public async Task Handle(DomainEvent.SmsNotificationEvent request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
