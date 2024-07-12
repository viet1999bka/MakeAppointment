using Common.Models;
using ProcessCalendar.API.DTO;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.Repositories
{
    public interface IDoctorRepository
    {
        public Task<List<DoctorModel>> GetListDoctorAsync();
        public Task<int> AddNewDoctorAsync(DoctorModel doctorNew);
        public Task<AppointmentItemDTO> GetAppointOfDoctorAsync(int DoctorId);
    }
}
