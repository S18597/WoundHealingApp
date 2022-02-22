using MediatR;
using Newtonsoft.Json;

namespace WoundHealingWebApi.Requests
{
    public class DeleteWoundRequest : IRequest
    {
        public int WoundId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}