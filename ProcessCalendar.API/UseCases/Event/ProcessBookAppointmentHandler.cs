using Common.Abstractions.IntegrationEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.UseCases.Event
{
    public class ProcessBookAppointmentHandler : IRequestHandler<DomainEvent.BookAppointmentEvent>
    {
        private readonly CalendarDbContext _context;

        public ProcessBookAppointmentHandler(CalendarDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DomainEvent.BookAppointmentEvent request, CancellationToken cancellationToken)
        {
            // Lấy dữ liệu từ bảng lịch họp bác sĩ
            // Validate xem trùng dữ liệu không 
            // Thành công thì xếp lịch ngày random 1 2 3 để xếp ngày 
            // return
            // Check xem có lịch bác sĩ có trùng với lịch 
            // THêm vào db
            var doctor = await _context.Doctors.SingleOrDefaultAsync(x => x.Id == request.SelectedDoctorId);

            var userInf = await _context.UserAppointInfors.AddAsync(new UserAppointInfor
            {
                DoctorName = doctor.Name,
                PatientName = request.NamePatient,
                Status = "Đăng ký thành công",
                Note = ""
            });

            Task.Delay(10000);
            DateTime selectDate = DateTime.Parse(request.SelectedDate);
            DateTime optDate = DateTime.Parse(request.OptionDate);
            var validSelected = await _context.AppointmentItems.SingleOrDefaultAsync(x => x.Id == request.SelectedDoctorId && x.SetDate.Date == selectDate.Date);
            var validOption = await _context.AppointmentItems.SingleOrDefaultAsync(x => x.Id == request.SelectedDoctorId && x.SetDate.Date == optDate.Date);
            if (validSelected != null && validOption != null) {
                _context.UserAppointInfors.Update(new UserAppointInfor
                {
                    Id = userInf.Entity.Id,
                    Status = "Không thành công",
                    DoctorName = userInf.Entity.DoctorName,
                    PatientName = userInf.Entity.PatientName,
                    Note = "Bác sĩ đã có hẹn vào khoảng thời gian đã chọn"
                });
            }
            else
            {
                Random rnd = new Random();
                int val = rnd.Next(1, 3);
                if(val == 1)
                {
                    _context.UserAppointInfors.Update(new UserAppointInfor
                    {
                        Id = userInf.Entity.Id,
                        Status = "Đang tiếp nhận",
                        DoctorName = userInf.Entity.DoctorName,
                        PatientName = userInf.Entity.PatientName,
                        Note = ""
                    });
                }
                else
                {
                    _context.UserAppointInfors.Update(new UserAppointInfor
                    {
                        Id = userInf.Entity.Id,
                        Status = "Thành công",
                        DoctorName = userInf.Entity.DoctorName,
                        PatientName = userInf.Entity.PatientName,
                        Note = ""
                    });

                    // Thêm vào lịch bác sĩ
                    if(validSelected != null)
                    {
                        _context.AppointmentItems.Add(new AppointmentItem
                        {
                            DoctorId = request.SelectedDoctorId,
                            NamePatient = request.NamePatient,
                            SetDate = optDate,
                        });
                    }  
                    else
                    {
                        _context.AppointmentItems.Add(new AppointmentItem
                        {
                            DoctorId = request.SelectedDoctorId,
                            NamePatient = request.NamePatient,
                            SetDate = selectDate,
                        });
                    }
                }
            }

            return;
        }
    }
}
