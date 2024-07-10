namespace Appointment.API.Model
{
    public class UserAppointInfor
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime OptionDate { get; set; }
    }
}
