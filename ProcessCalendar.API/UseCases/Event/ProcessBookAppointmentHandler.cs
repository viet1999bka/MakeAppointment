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
            try
            {
                // Lấy dữ liệu từ bảng lịch họp bác sĩ
                // Validate xem trùng dữ liệu không 
                // Thành công thì xếp lịch ngày random 1 2 3 để xếp ngày 
                // return
                // Check xem có lịch bác sĩ có trùng với lịch 
                // THêm vào db
                var doctor = await _context.Doctors.SingleOrDefaultAsync(x => x.Id == request.SelectedDoctorId);

                DateTime selectDate = DateTime.Parse(request.SelectedDate);
                DateTime optDate = DateTime.Parse(request.OptionDate);
                var userInf = await _context.UserAppointInfors.AddAsync(new UserAppointInfor
                {
                    DoctorName = doctor.Name,
                    PatientName = request.NamePatient,
                    Status = "Đăng ký thành công",
                    Note = "Đang tiếp nhận",
                    SelectedDate = selectDate,
                    OptionDate = optDate,
                });
                await _context.SaveChangesAsync();
                await Task.Delay(10000);
                var validSelected = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == selectDate);
                var validOption = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == optDate);
               if (validSelected != null && validOption != null)
                {
                    userInf.Entity.Status = "Không thành công";
                    userInf.Entity.Note = "Bác sĩ đã có hẹn vào khoảng thời gian đã chọn";
                    _context.UserAppointInfors.Update(userInf.Entity);
                    _context.SaveChanges();
                }
                else
                {
                    Random rnd = new Random();
                    int val = rnd.Next(1, 3);
                    if (val == 1)
                    {
                        userInf.Entity.Status = "Đã đăng ký";
                        userInf.Entity.Note = "Đang tiếp nhận";
                        _context.UserAppointInfors.Update(userInf.Entity);
                        _context.SaveChanges();
                    }
                    else
                    {
                        userInf.Entity.Status = "Thành công";
                        userInf.Entity.Note = "Đã lên lịch";
                        _context.UserAppointInfors.Update(userInf.Entity);

                        _context.SaveChanges();

                        // Thêm vào lịch bác sĩ
                        if (validSelected != null)
                        {
                            _context.AppointmentItems.Add(new AppointmentItem
                            {
                                DoctorId = request.SelectedDoctorId,
                                NamePatient = request.NamePatient,
                                SetDate = optDate,
                            });
                            _context.SaveChanges();

                        }
                        else
                        {
                            _context.AppointmentItems.Add(new AppointmentItem
                            {
                                DoctorId = request.SelectedDoctorId,
                                NamePatient = request.NamePatient,
                                SetDate = selectDate,
                            });
                            _context.SaveChanges();

                        }
                    }
                }

                return;
            }
            catch (Exception ex) {
                return;
            }
            
        }
    }
}
