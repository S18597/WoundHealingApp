using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetPainTypesQuery : IRequest<GetPainTypesResponse>
    {
    }
}