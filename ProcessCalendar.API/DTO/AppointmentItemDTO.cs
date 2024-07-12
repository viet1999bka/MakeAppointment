using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.DTO
{
    public class AppointmentItemDTO 
    {
        public List<AppointmentItem> ListAppoint { get; set; }
        public string NameDoctor { get; set; }
    }
}
