using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PusherServer;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("ChatController");

        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("GetChats/{userId}/{isPatient}")]
        [HttpGet]
        public async Task<List<ChatDto>> GetChats([FromRoute] int userId, [FromRoute] bool isPatient)
        {
            Log.Info($"GetChats userId: {userId}, isPatient: {isPatient}");
            return (await _mediator.Send(new GetChatsQuery
            {
                UserId = userId,
                IsPatient = isPatient
            })).Chats;
        }


        [Route("GetChatMessages/{chatId}/{isPatient}")]
        [HttpGet]
        public async Task<List<MessageDto>> GetChatMessages([FromRoute] int chatId, [FromRoute] bool isPatient)
        {
            Log.Info($"GetChatMessages chatId: {chatId}");
            return (await _mediator.Send(new GetChatMessagesQuery
            {
                ChatId = chatId,
                IsPatient = isPatient
            })).ChatMessages;
        }

        [Route("Message")]
        [HttpPost]
        public async Task<ActionResult> Message(MessageDto message)
        {
            Log.Info($"chat message: {message}");

            await _mediator.Send(new SaveMessageRequest
            {
                MessageDto = message
            });

            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1340108",
              "ec988857c94670f54f9a",
              "a692daade78e25f5843c",
              options);

            await pusher.TriggerAsync(
              "chat",
              "message",
              new 
              { 
                  chatId = message.ChatId,
                  patientId = message.PatientId,
                  doctorId = message.DoctorId,
                  doctorName = message.DoctorName,
                  patientName = message.PatientName,
                  message = message.Message,
                  messageDate = message.MessageDate,
                  isPatientMessage = message.IsPatientMessage
              });

            return Ok(new string[] { });
        }
    }
}