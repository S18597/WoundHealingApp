using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetAppointmentNoteQuery : IRequest<GetAppointmentNoteResponse>
    {
        public int AppointmentId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}