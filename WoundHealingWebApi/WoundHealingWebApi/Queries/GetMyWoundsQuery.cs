using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetMyWoundsQuery : IRequest<GetMyWoundsResponse>
    {
        public int PatientId { get; set; }
    }
}