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
    public class SendBookApointmentWhenRecieveCommandCosumer : IConsumer<SendBookAppointment>
    {
        private readonly AppointServiceDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public SendBookApointmentWhenRecieveCommandCosumer(AppointServiceDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<SendBookAppointment> context)
        {
            // Nhận command 
            //  Xử lý
            DateTime selectDate = DateTime.Parse(context.Message.SelectedDate);
            DateTime optDate = DateTime.Parse(context.Message.OptionDate);
            var userInf = await _dbContext.UserAppointInfors.AddAsync(new UserAppointInfor
            {
                DoctorId = context.Message.SelectedDoctorId,
                DoctorName = context.Message.NameDoctor,
                PatientName = context.Message.NamePatient,
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
                SelectedDoctorId = context.Message.SelectedDoctorId,
                NamePatient = context.Message.NamePatient,
                Timestamp = DateTime.UtcNow,
                DescribeSymptoms = context.Message.DescribeSymptoms,
                SelectedDate = context.Message.SelectedDate,
                OptionDate = context.Message.OptionDate,
                IdApoint = userInf.Entity.Id,
            });

        }
    }
}
