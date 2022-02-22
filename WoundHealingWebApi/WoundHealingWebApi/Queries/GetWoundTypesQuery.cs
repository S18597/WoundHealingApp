using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundTypesQuery : IRequest<GetWoundTypesResponse>
    {
    }
}