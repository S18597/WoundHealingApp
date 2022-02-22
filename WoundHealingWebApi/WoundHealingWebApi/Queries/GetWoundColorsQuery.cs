using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundColorsQuery : IRequest<GetWoundColorsResponse>
    {
    }
}