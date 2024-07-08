using AutoMapper;
using ProcessCalendar.API.Model;

namespace ProcessCalendar.API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
            CreateMap<DoctorItem, DoctorModel>().ReverseMap();
        }
    }
}
