using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetChatMessagesResponse
    {
        public List<MessageDto> ChatMessages { get; set; }
    }
}