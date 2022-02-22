using MediatR;
using Newtonsoft.Json;

namespace WoundHealingWebApi.Requests
{
    public class CreateWoundRequest : IRequest
    {
        public int UserId { get; set; }
        public int WoundTypeId { get; set; }
        public int WoundLocationId { get; set; }
        public int WoundSizeId { get; set; }
        public int WoundColorId { get; set; }
        public int WoundOdorId { get; set; }
        public int WoundExudateId { get; set; }
        public int WoundBleedingId { get; set; }
        public int SurroundingSkinId { get; set; }
        public int PainTypeId { get; set; }
        public int PainLevelId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}