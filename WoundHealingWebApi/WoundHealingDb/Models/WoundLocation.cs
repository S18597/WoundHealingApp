using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundLocation
    {
        public int WoundLocationId { get; set; }
        public string WoundLocationName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}