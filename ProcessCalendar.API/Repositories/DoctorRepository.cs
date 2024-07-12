using AutoMapper;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.DTO;
using ProcessCalendar.API.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalendar.API.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly CalendarDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(CalendarDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<DoctorModel>> GetListDoctorAsync()
        {
            var doctors = await _context.Doctors!.OrderBy(x => x.Id).ToListAsync();
            return _mapper.Map<List<DoctorModel>>(doctors);
        }
        public async Task<int> AddNewDoctorAsync(DoctorModel doctorNew)
        {
            var doctor = new DoctorItem
            {
                Name = doctorNew.Name,
                Description = doctorNew.Description,
            };
            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return 1;
        }
        public async Task<AppointmentItemDTO> GetAppointOfDoctorAsync(int DoctorId)
        {
            var lstApo = await _context.AppointmentItems.Where(x => x.DoctorId == DoctorId).OrderBy(x => x.Id).ToListAsync();
            var nameDoctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == DoctorId);

            return new AppointmentItemDTO
            {
                ListAppoint = lstApo,
                NameDoctor = nameDoctor?.Name ?? "mặc định"
            };
        }
    }
}
