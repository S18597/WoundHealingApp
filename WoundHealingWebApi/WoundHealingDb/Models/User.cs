using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WoundHealingDb.Models
{
    public class User
    {
        public User()
        {
            Wounds = new HashSet<Wound>();
            //Treatments = new HashSet<Treatment>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Pesel { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsPatient { get; set; }
        public bool IsDoctor { get; set; }

        public virtual ICollection<Wound> Wounds { get; set; }
        //public virtual ICollection<Treatment> Treatments { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}