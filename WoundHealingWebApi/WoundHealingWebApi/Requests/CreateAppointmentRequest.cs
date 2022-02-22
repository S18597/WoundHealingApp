using MediatR;
using Newtonsoft.Json;
using System;

namespace WoundHealingWebApi.Requests
{
    public class CreateAppointmentRequest : IRequest
    {
        public int WoundId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}