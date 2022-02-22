using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundType
    {
        public int WoundTypeId { get; set; }
        public string WoundTypeName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}