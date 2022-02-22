using Newtonsoft.Json;
using System;

namespace WoundHealingWebApi.DTOs
{
    public class FindAppointmentDto
    {
        public DateTime Date { get; set; }
        public int? DoctorId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}