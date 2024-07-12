using Appointment.API.Migrations;
using Appointment.API.Protos;
using Common.Abstractions.IntegrationEvents;
using Grpc.Core;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using static Common.Abstractions.IntegrationEvents.Command;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Appointment.API.Services
{
    public class AppointmentService : AppointmentBookingApi.AppointmentBookingApiBase
    {
        private readonly AppointServiceDbContext _contextDb;
        private readonly IBus _bus;
        private readonly IMediator _mediator;
        public AppointmentService(AppointServiceDbContext contextDb, IBus bus, IMediator mediator)
        {
            _contextDb = contextDb;
            _bus = bus;
            _mediator = mediator;
        }
        [AllowAnonymous]
        public override async Task<SetAppointResponse> SetAppointmentBooking(SetAppointInfor request, ServerCallContext context)
        {
            try
            {

                var commandMess = new SendBookAppointment()
                {
                    Id = Guid.NewGuid(),
                    SelectedDoctorId = request.SelectedId,
                    NamePatient = request.NamePatient,
                    Timestamp = DateTime.UtcNow,
                    DescribeSymptoms = request.Description,
                    SelectedDate = request.SelectedDate,
                    OptionDate = request.OptionDate,
                    NameDoctor = request.NameDoctor,
                };
                await _mediator.Send(commandMess);
                return new SetAppointResponse { Response = 1 };
            }
            catch (Exception ex) {
                return new SetAppointResponse { Response = 0 };
            }

        }

        [AllowAnonymous]
        public override async Task<ListAppointRegistedResponse> GetListAppointRegisted(SetAppointResponse request, ServerCallContext context)
        {
            var lstResult = await _contextDb.UserAppointInfors.OrderBy(x => x.Id).ToListAsync();
            var result = new ListAppointRegistedResponse();
            foreach (var item in lstResult) {
                result.ListAppointRegisRe.Add(new ListAppointRegisted
                {
                    Id = item.Id,
                    NameDoctor = item.DoctorName,
                    NamePation = item.PatientName,
                    SelectedDate = item.SelectedDate.ToString(),
                    OptionDate = item.OptionDate.ToString(),
                    Status = item.Status,
                });
            }
            return result;
        }

        private static Uri Addess<T>() => new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
    }
}
