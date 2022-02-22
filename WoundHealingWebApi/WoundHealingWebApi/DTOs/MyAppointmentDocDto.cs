using System;

namespace WoundHealingWebApi.DTOs
{
    public class MyAppointmentDocDto
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }
        public string PatientName { get; set; }
    }
}