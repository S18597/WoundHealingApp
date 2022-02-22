using Newtonsoft.Json;
using System;

namespace WoundHealingWebApi.DTOs
{
    public class FastestAppointmentDto
    {
        public int DoctorId { get; set; }
        public string DoctorFullname { get; set; }
        public DateTime AppointmentDate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}