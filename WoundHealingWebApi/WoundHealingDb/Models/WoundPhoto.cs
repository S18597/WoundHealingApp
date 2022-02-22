using System;
namespace WoundHealingDb.Models
{
    public class WoundPhoto
    {
        public int WoundPhotoId { get; set; }
        public int WoundId { get; set; }
        public string Filename { get; set; }
        public byte[] FileData { get; set; }

        public virtual Wound Wound { get; set; }
    }
}