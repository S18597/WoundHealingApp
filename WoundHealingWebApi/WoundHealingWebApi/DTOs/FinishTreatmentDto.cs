using Newtonsoft.Json;

namespace WoundHealingWebApi.DTOs
{
    public class FinishTreatmentDto
    {
        public int WoundId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}