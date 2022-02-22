using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class SurroundingSkin
    {
        public int SurroundingSkinId { get; set; }
        public string SurroundingSkinName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}