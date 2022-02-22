using Newtonsoft.Json;
using System;

namespace WoundHealingWebApi.DTOs
{
    public class MessageDto
    {
        public int ChatId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsPatientMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}