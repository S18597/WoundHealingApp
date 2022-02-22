using System.Collections.Generic;

namespace WoundHealingWebApi.DTOs
{
    public class StatsDto
    {
        public int DoctorId { get; set; }
        public int PatientsCnt { get; set; }
        public int FinishedTreatmentsCnt { get; set; }
        public int AppointmentsCnt { get; set; }
        public int WoundsCnt { get; set; }
        public int AvgTreatmentDays { get; set; }
        public List<WoundTypesStats> WoundTypesStats { get; set; }
    }
}