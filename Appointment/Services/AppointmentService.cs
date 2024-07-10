using Appointment.API.Protos;
using Common.Abstractions.IntegrationEvents;
using Grpc.Core;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using System.Net.WebSockets;

namespace Appointment.API.Services
{
    public class AppointmentService : AppointmentBookingApi.AppointmentBookingApiBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        public AppointmentService(IPublishEndpoint publishEndpoint, IBus bus)
        {
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }
        [AllowAnonymous]
        public override async Task<SetAppointResponse> SetAppointmentBooking(SetAppointInfor request, ServerCallContext context)
        {
            try
            {
                //await _publishEndpoint.Publish(new DomainEvent.BookAppointmentEvent()
                //{
                //    Id = Guid.NewGuid(),
                //    SelectedDoctorId = request.SelectedId,
                //    NamePatient = request.NamePatient,
                //    Timestamp = DateTime.UtcNow,
                //    DescribeSymptoms = request.Description,
                //    SelectedDate = request.SelectedDate,
                //    OptionDate = request.OptionDate,
                //});

                var commandMess = new Command.SendBookAppointment()
                {
                    Id = Guid.NewGuid(),
                    SelectedDoctorId = request.SelectedId,
                    NamePatient = request.NamePatient,
                    Timestamp = DateTime.UtcNow,
                    DescribeSymptoms = request.Description,
                    SelectedDate = request.SelectedDate,
                    OptionDate = request.OptionDate,
                };

                var endpint = await _bus.GetSendEndpoint(Addess<Command.SendBookAppointment>());
                await endpint.Send(commandMess);
                //await _publishEndpoint.Publish(new DomainEvent.SmsNotificationEvent()
                //{
                //    Id = Guid.NewGuid(),
                //    Timestamp = DateTime.UtcNow,
                //});
                return new SetAppointResponse { Response = 1 };
            }
            catch (Exception ex) {
                return new SetAppointResponse { Response = 0 };
            }

        }

        private static Uri Addess<T>() => new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
    }
}
