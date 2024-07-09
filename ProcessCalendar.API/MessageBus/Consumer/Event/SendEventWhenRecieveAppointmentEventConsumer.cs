using Common.Abstractions.IntegrationEvents;
using MediatR;
using ProcessCalendar.API.Abtractions.Messages;

namespace ProcessCalendar.API.MessageBus.Consumer.Event
{
    public class SendEventWhenRecieveAppointmentEventConsumer : Consumer<DomainEvent.BookAppointmentEvent>
    {
        public SendEventWhenRecieveAppointmentEventConsumer(ISender sender) : base(sender)
        {
        }
    }
}
