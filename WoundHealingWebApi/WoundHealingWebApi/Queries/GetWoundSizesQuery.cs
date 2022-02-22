using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundSizesQuery : IRequest<GetWoundSizesResponse>
    {
    }
}