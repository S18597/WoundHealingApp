using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Requests
{
    public class SaveAppointmentNoteRequest : IRequest
    {
        public AppointmentNoteDto AppointmentNoteDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}