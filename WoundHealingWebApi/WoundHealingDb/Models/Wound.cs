using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WoundHealingDb.Models
{
    public class Wound
    {
        public int WoundId { get; set; }
        public int UserId { get; set; }
        public int WoundTypeId { get; set; }
        public int WoundLocationId { get; set; }
        public int WoundSizeId { get; set; }
        public int WoundColorId { get; set; }
        public int WoundOdorId { get; set; }
        public int WoundExudateId { get; set; }
        public int WoundBleedingId { get; set; }
        public int SurroundingSkinId { get; set; }
        public int PainTypeId { get; set; }
        public int PainLevelId { get; set; }
        public DateTime WoundRegisterDate { get; set; }

        public virtual User User { get; set; }
        public virtual WoundType WoundType { get; set; }
        public virtual WoundLocation WoundLocation { get; set; }
        public virtual WoundSize WoundSize { get; set; }
        public virtual WoundColor WoundColor { get; set; }
        public virtual WoundOdor WoundOdor { get; set; }
        public virtual WoundExudate WoundExudate { get; set; }
        public virtual WoundBleeding WoundBleeding { get; set; }
        public virtual SurroundingSkin SurroundingSkin { get; set; }
        public virtual PainType PainType { get; set; }
        public virtual PainLevel PainLevel { get; set; }

        public virtual ICollection<WoundPhoto> WoundPhotos { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}