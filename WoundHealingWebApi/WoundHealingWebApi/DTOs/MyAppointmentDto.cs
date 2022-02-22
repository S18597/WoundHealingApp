using System;

namespace WoundHealingWebApi.DTOs
{
    public class MyAppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }
        public string DoctorName { get; set; }
    }
}