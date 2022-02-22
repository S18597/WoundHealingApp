using System;

namespace WoundHealingWebApi.DTOs
{
    public class MyWoundDto
    {
        public int WoundId { get; set; }
        public string WoundType { get; set; }
        public string WoundLocation { get; set; }
        public string WoundSize { get; set; }
        public string WoundColor { get; set; }
        public string WoundOdor { get; set; }
        public string WoundExudate { get; set; }
        public string WoundBleeding { get; set; }
        public string SurroundingSkin { get; set; }
        public string PainType { get; set; }
        public string PainLevel { get; set; }
        public DateTime WoundRegisterDate { get; set; }
    }
}