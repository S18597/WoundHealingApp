using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundExudate
    {
        public int WoundExudateId { get; set; }
        public string WoundExudateName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}