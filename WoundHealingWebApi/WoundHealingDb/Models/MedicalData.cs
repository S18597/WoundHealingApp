namespace WoundHealingDb.Models
{
    public class MedicalData
    {
        public int UserId { get; set; }
        public string ChronicDeseases { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public bool Pregnancy { get; set; }
        public bool Tobacco { get; set; }
        public bool Alcohol { get; set; }
        public bool Drugs { get; set; }

        public virtual User User { get; set; }
    }
}