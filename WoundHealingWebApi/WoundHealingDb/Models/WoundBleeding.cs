using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundBleeding
    {
        public int WoundBleedingId { get; set; }
        public string WoundBleedingName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}