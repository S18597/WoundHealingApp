using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyAppointmentsDocQuery : IRequest<GetMyAppointmentsDocResponse>
    {
        public int DoctorId { get; set; }
    }
}