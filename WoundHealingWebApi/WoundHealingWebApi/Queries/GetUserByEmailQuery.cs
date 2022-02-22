using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
    {
        public string Email { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}