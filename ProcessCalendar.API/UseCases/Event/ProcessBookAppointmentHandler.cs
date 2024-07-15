//using Common.Abstractions.IntegrationEvents;
//using MassTransit;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using ProcessCalendar.API.Model;
//using System.Text;
//using System.Text.Json;

//namespace ProcessCalendar.API.UseCases.Event
//{
//    public class ProcessBookAppointmentHandler : IConsumer<DomainEvent.BookAppointmentEvent>
//    {
//        private readonly CalendarDbContext _context;
//        //private readonly IPublishEndpoint _publishEndpoint;

//        public ProcessBookAppointmentHandler(CalendarDbContext context)
//        {
//            _context = context;
//            //_publishEndpoint = publishEndpoint;
//        }

//        public async Task Consume(ConsumeContext< DomainEvent.BookAppointmentEvent> contextConsumer)
//        {
//            try
//            {
//                // Lấy dữ liệu từ bảng lịch họp bác sĩ
//                // Validate xem trùng dữ liệu không 
//                // Thành công thì xếp lịch ngày random 1 2 3 để xếp ngày 
//                // return
//                // Check xem có lịch bác sĩ có trùng với lịch 
//                // THêm vào db
//                var request = contextConsumer.Message;
//                var doctor = await _context.Doctors.SingleOrDefaultAsync(x => x.Id == request.SelectedDoctorId);

//                DateTime selectDate = DateTime.Parse(request.SelectedDate);
//                DateTime optDate = DateTime.Parse(request.OptionDate);
//                var validSelected = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == selectDate);
//                var validOption = await _context.AppointmentItems.FirstOrDefaultAsync(x => x.DoctorId == request.SelectedDoctorId && x.SetDate == optDate);
//                string statusR, note;
//                if (validSelected != null && validOption != null)
//                {
//                    statusR = "Không thành công";
//                    note = "Bác sĩ đã có hẹn vào khoảng thời gian đã chọn";
//                    // gửi lại cho appoint để update service
//                }
//                else
//                {
//                    Random rnd = new Random();
//                    int val = rnd.Next(1, 3);
//                    val = 2;
//                    if (val == 1)
//                    {
//                        statusR = "Đã đăng ký";
//                        note = "Đang tiếp nhận";
//                        // gửi lại cho appoint để update service
//                    }
//                    else
//                    {
//                        statusR = "Thành công";
//                        note = "Đã lên lịch";



//                        // Thêm vào lịch bác sĩ
//                        if (validSelected != null)
//                        {
//                            await _context.AppointmentItems.AddAsync(new AppointmentItem
//                            {
//                                DoctorId = request.SelectedDoctorId,
//                                NamePatient = request.NamePatient,
//                                SetDate = optDate,
//                            });

//                        }
//                        else
//                        {
//                            await _context.AppointmentItems.AddAsync(new AppointmentItem
//                            {
//                                DoctorId = request.SelectedDoctorId,
//                                NamePatient = request.NamePatient,
//                                SetDate = selectDate,
//                            });

//                        }
//                        await _context.SaveChangesAsync();
//                    }
//                }
//                await contextConsumer.RespondAsync(new DomainEvent.BookAppointEventSatus
//                {
//                    status = statusR,
//                    note = note,
//                });

//                return;
//                // gửi lại cho appoint để update service
//                // gửi tiếp cho webhook để email về cho người bệnh
//                //var webhookUrl = "http://localhost:5255/api/RevieceChangStatus"; // Thay thế bằng URL của webhook

//                //var payload = new
//                //{
//                //    EventName = statusR,
//                //    Data = note
//                //};

//                //var jsonPayload = JsonSerializer.Serialize(payload);

//                //using var httpClient = new HttpClient();
//                //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                //try
//                //{
//                //    var response = await httpClient.PostAsync(webhookUrl, content);
//                //    response.EnsureSuccessStatusCode();

//                //    Console.WriteLine("Webhook request sent successfully.");
//                //}
//                //catch (Exception ex)
//                //{
//                //    Console.WriteLine($"An error occurred while sending webhook request: {ex.Message}");
//                //}

//                //// Gửi lên queue để chỗ khác xử lý
//                //await _publishEndpoint.Publish(new DomainEvent.ChangeStatusEvent()
//                //{
//                //    Id = Guid.NewGuid(),
//                //    Timestamp = DateTime.Now,
//                //    IdApoint = request.IdApoint,
//                //    Status = statusR,
//                //    Note = note
//                //});


//            }
//            catch (Exception ex)
//            {
//                return;
//            }
//        } 
//    }
//}
