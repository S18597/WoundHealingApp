using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public int UserId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}