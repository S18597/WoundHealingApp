using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingDb.Models;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Handlers
{
    public class SaveMessageRequestHandler : IRequestHandler<SaveMessageRequest>
    {
        public static Logger Log => LogManager.GetLogger("SaveMessageRequestHandler");

        private readonly WoundHealingContext _context;

        public SaveMessageRequestHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SaveMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if chat exists
                var chat = await _context.Chat.Where(u => u.ChatId == request.MessageDto.ChatId).FirstOrDefaultAsync();
                if (chat == null)
                {
                    Log.Error($"Chat with id: {request.MessageDto.ChatId} does not exists!");
                    throw new Exception("Chat not found!");
                }

                if (chat == null)
                {
                    // create chat
                    var newChat = new Chat
                    {
                        DoctorId = request.MessageDto.DoctorId,
                        PatientId = request.MessageDto.PatientId
                    };

                    await _context.Chat.AddAsync(newChat);
                    await _context.SaveChangesAsync();

                    chat = newChat;

                    Log.Info($"chat created id: {chat.ChatId}");
                }

                // create chat message
                var message = new ChatMessage
                {
                    ChatId = chat.ChatId,
                    UserId = request.MessageDto.IsPatientMessage ? request.MessageDto.PatientId : request.MessageDto.DoctorId,
                    Message = request.MessageDto.Message,
                    MessageDate = DateTime.Now
                };

                await _context.ChatMessage.AddAsync(message);
                await _context.SaveChangesAsync();

                Log.Info($"chat message created id: {message.ChatMessageId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"SaveMessageRequest error: {ex}");
                throw;
            }
        }
    }
}