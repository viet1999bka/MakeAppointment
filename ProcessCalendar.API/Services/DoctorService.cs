using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using ProcessCalendar.API.Model;
using ProcessCalendar.API.Repositories;

namespace ProcessCalendar.API.Services
{
    public class DoctorService(IDoctorRepository respos) : doctor.doctorBase
    {
        [AllowAnonymous]
        public override async Task<GetListDoctorResponse> GetListDoctor(Empty request, ServerCallContext context)
        {
            var data = await respos.GetListDoctorAsync();

            if (data == null) {
                return MapToCustomerBasketResponse(data);
            }
            return new();
        }

        private static GetListDoctorResponse MapToCustomerBasketResponse(List<DoctorModel> doctorModels)
        {
            var response = new GetListDoctorResponse();
            if (doctorModels != null) {

                foreach (DoctorModel item in doctorModels)
                {
                    if (item != null) { 
                        response.Add(new DoctorInfo()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item?.Description,
                        });
                    }
                }
            }
            

            return response;
        }
    }
}
