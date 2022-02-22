using System;

namespace WoundHealingWebApi.DTOs
{
    public class AppointmentSummaryDto
    {
        public int AppointmentId { get; set; }
        public int WoundId { get; set; }
        public string Doctor { get; set; }
        public string Patient { get; set; }
        public DateTime Date { get; set; }
        public string WoundType { get; set; }
    }
}