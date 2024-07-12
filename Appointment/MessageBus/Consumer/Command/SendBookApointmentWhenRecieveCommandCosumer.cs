using Appointment.API.Abtractions.Message;
using Appointment.API.Migrations;
using Appointment.API.Model;
using Common.Abstractions.IntegrationEvents;
using Common.Models;
using MassTransit;
using MediatR;
using static Common.Abstractions.IntegrationEvents.Command;

namespace Appointment.API.MessageBus.Consumer.Command
{
    public class SendBookApointmentWhenRecieveCommandCosumer : IRequestHandler<SendBookAppointment>
    {
        private readonly AppointServiceDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public SendBookApointmentWhenRecieveCommandCosumer(IPublishEndpoint publishEndpoint, AppointServiceDbContext dbContext)
        {
            _publishEndpoint = publishEndpoint;
            _dbContext = dbContext;
        }

        public async Task Handle(SendBookAppointment context, CancellationToken cancellationToken)
    {
            // Nhận command 
            //  Xử lý
            DateTime selectDate = DateTime.Parse(context.SelectedDate);
            DateTime optDate = DateTime.Parse(context.OptionDate);
            var userInf = await _dbContext.UserAppointInfors.AddAsync(new UserAppointInfor
            {
                DoctorId = context.SelectedDoctorId,
                DoctorName = context.NameDoctor,
                PatientName = context.NamePatient,
                Status = "Đăng ký thành công",
                Note = "Đang tiếp nhận",
                SelectedDate = selectDate,
                OptionDate = optDate,
            });
            await _dbContext.SaveChangesAsync();
            // Publish lên exchange
            await _publishEndpoint.Publish(new DomainEvent.BookAppointmentEvent()
            {
                Id = Guid.NewGuid(),
                SelectedDoctorId = context.SelectedDoctorId,
                NamePatient = context.NamePatient,
                Timestamp = DateTime.UtcNow,
                DescribeSymptoms = context.DescribeSymptoms,
                SelectedDate = context.SelectedDate,
                OptionDate = context.OptionDate,
                IdApoint = userInf.Entity.Id,
            });

        }
    }
}
