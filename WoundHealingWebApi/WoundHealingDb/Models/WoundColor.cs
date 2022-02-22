using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class WoundColor
    {
        public int WoundColorId { get; set; }
        public string WoundColorName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}