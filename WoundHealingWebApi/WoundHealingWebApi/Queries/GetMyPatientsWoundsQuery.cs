using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyPatientsWoundsQuery : IRequest<GetMyPatientsWoundsResponse>
    {
        public int DoctorId { get; set; }
    }
}