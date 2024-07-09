using Appointment.API.Protos;
using Common.Abstractions.IntegrationEvents;
using Grpc.Core;
using MassTransit;
using Microsoft.AspNetCore.Authorization;

namespace Appointment.API.Services
{
    public class AppointmentService : AppointmentBookingApi.AppointmentBookingApiBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public AppointmentService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [AllowAnonymous]
        public override async Task<SetAppointResponse> SetAppointmentBooking(SetAppointInfor request, ServerCallContext context)
        {
            try
            {
                    await _publishEndpoint.Publish(new DomainEvent.BookAppointmentEvent()
                {
                    Id = Guid.NewGuid(),
                    SelectedDoctorId = request.SelectedId,
                    Timestamp = DateTime.UtcNow,
                    DescribeSymptoms = request.Description,
                    SelectedDate = request.SelectedDate,
                    OptionDate = request.OptionDate,
                });
                return new SetAppointResponse { Response = 1 };
            }
            catch (Exception ex) {
                return new SetAppointResponse { Response = 0 };
            }

        }
    }
}
