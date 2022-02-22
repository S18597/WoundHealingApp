using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetPainLevelsQuery : IRequest<GetPainLevelsResponse>
    {
    }
}