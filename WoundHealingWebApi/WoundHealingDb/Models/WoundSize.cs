using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundSize
    {
        public int WoundSizeId { get; set; }
        public string WoundSizeName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}