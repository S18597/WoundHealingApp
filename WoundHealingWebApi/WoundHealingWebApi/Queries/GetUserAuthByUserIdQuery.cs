using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetUserAuthByUserIdQuery : IRequest<GetUserAuthByUserIdResponse>
    {
        public int UserId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}