using Microsoft.EntityFrameworkCore;

namespace ProcessCalendar.API.Model
{
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {

        }

        public DbSet<DoctorItem> Doctors { get; set; }
        public DbSet<AppointmentItem> AppointmentItems { get; set; }
        public DbSet<UserAppointInfor> UserAppointInfors { get; set; }
    }
}
