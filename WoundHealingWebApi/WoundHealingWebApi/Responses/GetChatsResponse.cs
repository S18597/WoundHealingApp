using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetChatsResponse
    {
        public List<ChatDto> Chats { get; set; }
    }
}