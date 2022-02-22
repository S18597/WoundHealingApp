using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetNewestWoundIdByUserIdQuery : IRequest<GetNewestWoundIdByUserIdResponse>
    {
        public int UserId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}