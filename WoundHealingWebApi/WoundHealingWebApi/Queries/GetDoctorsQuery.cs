using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetDoctorsQuery : IRequest<GetDoctorsResponse>
    {
    }
}