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
                return data;
            }
            return new();
        }

        private static GetListDoctorResponse MapToCustomerBasketResponse(List<DoctorModel> doctorModels)
        {
            var response = new GetListDoctorResponse();

            foreach (var item in doctorModels)
            {
                response.Add(new Doctor()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                });
            }

            return response;
        }
    }
}
