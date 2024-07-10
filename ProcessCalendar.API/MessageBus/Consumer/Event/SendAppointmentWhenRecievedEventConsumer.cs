using Common.Abstractions.IntegrationEvents;
using MediatR;
using ProcessCalendar.API.Abtractions.Messages;

namespace ProcessCalendar.API.MessageBus.Consumer.Event
{
    public class SendAppointmentWhenRecievedEventConsumer : Consumer<DomainEvent.BookAppointmentEvent>
    {
        public SendAppointmentWhenRecievedEventConsumer(ISender sender) : base(sender)
        {
        }
    }
}
