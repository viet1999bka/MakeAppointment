using Common.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.Services
{
    public class AppointRegistedService : AppointRegisted.AppointRegistedBase
    {
        private readonly CalendarDbContext _context;

        public AppointRegistedService(CalendarDbContext context)
        {
            _context = context;
        }

        public override async Task<GetListAppointRegistedResponse> GetListAppointRegisted(GetListAppointRegistedRequest request, ServerCallContext context)
        {
            var data = await _context.UserAppointInfors.ToListAsync();
            if (data is not null) { 
                return MapToAppointRegistedResponse(data);
            }
            return new();
        }

        private static GetListAppointRegistedResponse MapToAppointRegistedResponse(List<UserAppointInfor> list)
        {
            var response = new GetListAppointRegistedResponse();
            if (list != null)
            {

                foreach (var item in list)
                {
                    if (item != null)
                    {
                        response.AppointRegistedInfor.Add(new AppointRegistedInfo()
                        {
                            Id = item.Id,
                            NamePation = item.PatientName,
                            NameDoctor = item.DoctorName,
                            Status = item.Status,
                            OptionDate = item.OptionDate.ToString(),
                            SelectedDate = item.SelectedDate.ToString(), 
                        });
                    }
                }
            }
            return response;
        }
    }
}
