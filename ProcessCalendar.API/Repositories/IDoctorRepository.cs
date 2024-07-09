using Common.Models;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.Repositories
{
    public interface IDoctorRepository
    {
        public Task<List<DoctorModel>> GetListDoctorAsync();
    }
}
