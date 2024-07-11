using Appointment.API.Migrations;
using Appointment.API.Protos;
using Common.Abstractions.IntegrationEvents;
using Grpc.Core;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Appointment.API.Services
{
    public class AppointmentService : AppointmentBookingApi.AppointmentBookingApiBase
    {
        private readonly AppointServiceDbContext _contextDb;
        private readonly IBus _bus;
        public AppointmentService(AppointServiceDbContext contextDb, IBus bus)
        {
            _contextDb = contextDb;
            _bus = bus;
        }
        [AllowAnonymous]
        public override async Task<SetAppointResponse> SetAppointmentBooking(SetAppointInfor request, ServerCallContext context)
        {
            try
            {

                var commandMess = new Command.SendBookAppointment()
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

                var endpint = await _bus.GetSendEndpoint(Addess<Command.SendBookAppointment>());
                await endpint.Send(commandMess);
                return new SetAppointResponse { Response = 1 };
            }
            catch (Exception ex) {
                return new SetAppointResponse { Response = 0 };
            }

        }

        [AllowAnonymous]
        public override async Task<ListAppointRegistedResponse> GetListAppointRegisted(SetAppointResponse request, ServerCallContext context)
        {
            var lstResult = await _contextDb.UserAppointInfors.ToListAsync();
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
