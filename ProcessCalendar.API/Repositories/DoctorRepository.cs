using AutoMapper;
using Common.Models;
using Microsoft.EntityFrameworkCore;
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
            var doctors = await _context.Doctors!.ToListAsync();
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
    }
}
