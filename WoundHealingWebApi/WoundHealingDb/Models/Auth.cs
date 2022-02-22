using Newtonsoft.Json;

namespace WoundHealingDb.Models
{
    public class Auth
    {
        public int UserId { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}