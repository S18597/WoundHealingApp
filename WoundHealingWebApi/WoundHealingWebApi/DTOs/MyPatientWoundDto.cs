namespace WoundHealingWebApi.DTOs
{
    public class MyPatientWoundDto
    {
        public int WoundId { get; set; }
        public string WoundType { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public int WoundPhotoId { get; set; }
        public string WoundPhoto { get; set; }
    }
}