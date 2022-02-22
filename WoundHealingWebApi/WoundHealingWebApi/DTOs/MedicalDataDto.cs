using Newtonsoft.Json;

namespace WoundHealingWebApi.DTOs
{
    public class MedicalDataDto
    {
        public int UserId { get; set; }
        public string ChronicDiseases { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public bool Pregnancy { get; set; }
        public bool Tobacco { get; set; }
        public bool Alcohol { get; set; }
        public bool Drugs { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}