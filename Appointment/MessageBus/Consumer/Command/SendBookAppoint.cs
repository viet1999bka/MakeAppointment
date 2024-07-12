using Common.Abstractions.IntegrationEvents;
using MassTransit;
using System.Data;
using System.Threading;
using static Common.Abstractions.IntegrationEvents.DomainEvent;

namespace Appointment.API.MessageBus.Consumer.Command
{
    public class SendBookAppoint
    {
        IRequestClient<DomainEvent.BookAppointmentEvent> _client;

        public SendBookAppoint(IRequestClient<DomainEvent.BookAppointmentEvent> client)
        {
            _client = client;
        }

        public async Task UpdateStatus(BookAppointmentEvent evetnInput)
        {
            var response = await _client.GetResponse<BookAppointmentEvent>(evetnInput);
        }

    }
}
