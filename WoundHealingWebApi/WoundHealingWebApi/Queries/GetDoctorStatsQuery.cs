using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetDoctorStatsQuery : IRequest<GetDoctorStatsResponse>
    {
        public int DoctorId { get; set; }
    }
}