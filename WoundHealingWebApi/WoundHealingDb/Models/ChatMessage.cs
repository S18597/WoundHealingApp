using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundHealingDb.Models
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }

        [ForeignKey("ChatId")]
        public virtual Chat Chat { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}