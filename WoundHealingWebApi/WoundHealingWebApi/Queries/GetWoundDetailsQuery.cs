using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundDetailsQuery : IRequest<GetWoundDetailsResponse>
    {
        public int WoundId { get; set; }
    }
}