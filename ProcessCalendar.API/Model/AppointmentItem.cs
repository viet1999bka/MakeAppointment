namespace ProcessCalendar.API.Model
{
    public class AppointmentItem
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string NamePatient { get; set; }
        public DateTime SetDate { get; set; }

    }
}
