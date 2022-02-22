using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyPatientsQuery : IRequest<GetMyPatientsResponse>
    {
        public int DoctorId { get; set; }
    }
}