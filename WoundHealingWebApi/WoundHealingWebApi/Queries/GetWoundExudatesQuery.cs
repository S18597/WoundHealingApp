using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundExudatesQuery : IRequest<GetWoundExudatesResponse>
    {
    }
}