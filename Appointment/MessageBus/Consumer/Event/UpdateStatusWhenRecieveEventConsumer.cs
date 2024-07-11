using Appointment.API.Abtractions.Message;
using Common.Abstractions.IntegrationEvents;
using MediatR;

namespace Appointment.API.MessageBus.Consumer.Event
{
    public class UpdateStatusWhenRecieveEventConsumer : Consumer<DomainEvent.ChangeStatusEvent>
    {
        public UpdateStatusWhenRecieveEventConsumer(ISender sender) : base(sender)
        {
        }
    }
}
