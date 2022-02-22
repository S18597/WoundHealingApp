using System;

namespace WoundHealingDb.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int TreatmentId { get; set; }
        public string AppointmentNotes { get; set; }
        public DateTime AppointmentDate { get; set; }

        public virtual Treatment Treatment { get; set; }
    }
}