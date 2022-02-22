using System;
using System.Collections.Generic;
using WoundHealingDb.Enums;

namespace WoundHealingDb.Models
{
    public class Treatment
    {
        public Treatment()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int TreatmentId { get; set; }
        public int WoundId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus Status { get; set; } = TreatmentStatus.InProgress;


        public virtual Wound Wound { get; set; }
        public virtual User Doctor { get; set; }
        public virtual User Patient { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}