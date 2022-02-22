using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundOdor
    {
        public int WoundOdorId { get; set; }
        public string WoundOdorName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}