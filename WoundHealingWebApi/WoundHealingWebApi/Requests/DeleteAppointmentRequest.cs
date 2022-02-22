using MediatR;
using Newtonsoft.Json;

namespace WoundHealingWebApi.Requests
{
    public class DeleteAppointmentRequest : IRequest
    {
        public int AppointmentId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}