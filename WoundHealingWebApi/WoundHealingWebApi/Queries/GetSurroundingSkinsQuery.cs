using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetSurroundingSkinsQuery : IRequest<GetSurroundingSkinsResponse>
    {
    }
}