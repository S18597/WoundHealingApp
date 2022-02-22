using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetChatsQuery : IRequest<GetChatsResponse>
    {
        public int UserId { get; set; }
        public bool IsPatient { get; set; }
    }
}