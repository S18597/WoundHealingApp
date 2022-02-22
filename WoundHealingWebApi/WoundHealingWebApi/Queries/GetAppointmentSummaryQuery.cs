using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetAppointmentSummaryQuery : IRequest<GetAppointmentSummaryResponse>
    {
        public int AppointmentId { get; set; }
    }
}