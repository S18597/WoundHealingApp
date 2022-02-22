using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundHealingDb.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [ForeignKey("PatientId")]
        public virtual User Patient { get; set; }
        [ForeignKey("DoctorId")]
        public virtual User Doctor { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}