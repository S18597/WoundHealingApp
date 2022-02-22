using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundBleedingsQuery : IRequest<GetWoundBleedingsResponse>
    {
    }
}