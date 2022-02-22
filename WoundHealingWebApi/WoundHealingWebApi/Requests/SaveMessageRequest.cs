using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Requests
{
    public class SaveMessageRequest : IRequest
    {
        public MessageDto MessageDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}