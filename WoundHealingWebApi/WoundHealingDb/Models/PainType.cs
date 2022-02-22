using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class PainType
    {
        public int PainTypeId { get; set; }
        public string PainTypeName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}