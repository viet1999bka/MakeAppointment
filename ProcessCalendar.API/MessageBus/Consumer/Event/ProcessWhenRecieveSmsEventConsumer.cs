using Common.Abstractions.IntegrationEvents;
using MediatR;
using ProcessCalendar.API.Abtractions.Messages;

namespace ProcessCalendar.API.MessageBus.Consumer.Event
{
    public class ProcessWhenRecieveSmsEventConsumer : Consumer<DomainEvent.SmsNotificationEvent>
    {
        public ProcessWhenRecieveSmsEventConsumer(ISender sender) : base(sender)
        {
        }
    }
}
