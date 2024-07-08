using AutoMapper;
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
    }
}
