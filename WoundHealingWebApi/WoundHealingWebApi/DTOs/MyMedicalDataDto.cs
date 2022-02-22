using System;

namespace WoundHealingWebApi.DTOs
{
    public class MyMedicalDataDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public DateTime UserDateOfBirth { get; set; }
        public string Pesel { get; set; }
        public string ChronicDeseases { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public bool Pregnancy { get; set; }
        public bool Tobacco { get; set; }
        public bool Alcohol { get; set; }
        public bool Drugs { get; set; }
    }
}