using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyAppointmentsQuery : IRequest<GetMyAppointmentsResponse>
    {
        public int PatientId { get; set; }
    }
}