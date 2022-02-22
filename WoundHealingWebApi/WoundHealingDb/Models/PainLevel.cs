using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class PainLevel
    {
        public int PainLevelId { get; set; }
        public string PainLevelName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}