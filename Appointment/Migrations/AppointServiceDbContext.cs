using Appointment.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Migrations
{
    public class AppointServiceDbContext : DbContext
    {
        public AppointServiceDbContext(DbContextOptions<AppointServiceDbContext> options) : base(options)
        {
        }

        public DbSet<UserAppointInfor> UserAppointInfors { get; set; }
    }
}
