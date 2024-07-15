using Common.Abstractions.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.UseCases.Event
{
    public class ProcessAppointConsumer : IConsumer<SendAppointmentToProcessRequest>
    {
        private readonly CalendarDbContext _context;

        public ProcessAppointConsumer(CalendarDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<SendAppointmentToProcessRequest> contextConsumer)
        {
            // Lấy dữ liệu từ bảng lịch họp bác sĩ
            // Validate xem trùng dữ liệu không 
            // Thành công thì xếp lịch ngày random 1 2 3 để xếp ngày 
            // return
            // Check xem có lịch bác sĩ có trùng với lịch 
            // THêm vào db
            var request = contextConsumer.Message;
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == request.SelectedDoctorId);

            DateTime selectDate = DateTime.Parse(request.SelectedDate);
            DateTime optDate = DateTime.Parse(request.OptionDate);
            var validSelected = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == selectDate);
            var validOption = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == optDate);
            string statusR, note;
            if (validSelected != null && validOption != null)
            {
                statusR = "Không thành công";
                note = "Bác sĩ đã có hẹn vào khoảng thời gian đã chọn";
                // gửi lại cho appoint để update service
            }
            else
            {
                Random rnd = new Random();
                int val = rnd.Next(1, 3);
                val = 2;
                if (val == 1)
                {
                    statusR = "Đã đăng ký";
                    note = "Đang tiếp nhận";
                    // gửi lại cho appoint để update service
                }
                else
                {
                    statusR = "Thành công";
                    note = "Đã lên lịch";



                    // Thêm vào lịch bác sĩ
                    if (validSelected != null)
                    {
                        await _context.AppointmentItems.AddAsync(new AppointmentItem
                        {
                            DoctorId = request.SelectedDoctorId,
                            NamePatient = request.NamePatient,
                            SetDate = optDate,
                        });

                    }
                    else
                    {
                        await _context.AppointmentItems.AddAsync(new AppointmentItem
                        {
                            DoctorId = request.SelectedDoctorId,
                            NamePatient = request.NamePatient,
                            SetDate = selectDate,
                        });

                    }
                    await _context.SaveChangesAsync();
                }
            }
            var responeMass = new UpdateStatusAppintResponse
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                status = statusR,
                note = note,
            };
            await contextConsumer.RespondAsync(responeMass);
            return;
        }
    }
}
