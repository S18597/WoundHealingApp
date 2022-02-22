using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundLocationsQuery : IRequest<GetWoundLocationsResponse>
    {
    }
}