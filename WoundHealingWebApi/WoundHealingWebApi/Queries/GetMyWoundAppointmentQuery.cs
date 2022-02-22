using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyWoundAppointmentQuery : IRequest<GetMyWoundAppointmentResponse>
    {
        public int PatientId { get; set; }
    }
}