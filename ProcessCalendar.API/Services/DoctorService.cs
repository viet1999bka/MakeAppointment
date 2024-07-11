using Common.Models;
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

            if (data is not null) {
                return MapToCustomerBasketResponse(data);
            }
            return new();
        }

       [AllowAnonymous]
       public override async Task<AddNewDoctorResponse> AddNewDoctor(DoctorInfo request, ServerCallContext context)
        {
            var doctor = new DoctorModel
            {
                Name = request.Name,
                Description = request.Description,
            };
            var res = await respos.AddNewDoctorAsync(doctor);
            var ret = new AddNewDoctorResponse();
            ret.Respone = res;
            return ret;
        }
        private static GetListDoctorResponse MapToCustomerBasketResponse(List<DoctorModel> doctorModels)
        {
            var response = new GetListDoctorResponse();
            if (doctorModels != null) {

                foreach (DoctorModel item in doctorModels)
                {
                    if (item != null) { 
                        response.DoctorInfo.Add(new DoctorInfo()
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
