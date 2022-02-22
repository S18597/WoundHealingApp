namespace WoundHealingWebApi.DTOs
{
    public class ChatDto
    {
        public int ChatId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}