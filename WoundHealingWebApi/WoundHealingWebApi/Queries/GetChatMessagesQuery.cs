using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetChatMessagesQuery : IRequest<GetChatMessagesResponse>
    {
        public int ChatId { get; set; }
        public bool IsPatient { get; set; }
    }
}