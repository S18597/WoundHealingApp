using System;

namespace WoundHealingWebApi.DTOs
{
    public class WoundAppointmentDto
    {
        public int WoundId { get; set; }
        public string WoundType { get; set; }
        public DateTime WoundRegisterDate { get; set; }
        public MyAppointmentDto Appointment { get; set; }
    }
}