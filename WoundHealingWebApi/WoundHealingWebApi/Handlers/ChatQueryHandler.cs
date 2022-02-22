using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Handlers
{
    public class ChatQueryHandler : IRequestHandler<GetChatsQuery, GetChatsResponse>,
                                    IRequestHandler<GetChatMessagesQuery, GetChatMessagesResponse>
    {
        public static Logger Log => LogManager.GetLogger("ChatQueryHandler");

        private readonly WoundHealingContext _context;

        public ChatQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetChatsResponse> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var chats = new List<ChatDto>();

                if (request.IsPatient)
                {
                    chats = await _context.Chat.Where(c => c.PatientId == request.UserId)
                    .Select(s => new ChatDto
                    {
                        ChatId = s.ChatId,
                        DoctorId = s.DoctorId,
                        DoctorName = s.Doctor.Firstname + " " + s.Doctor.Lastname,
                        PatientId = s.PatientId,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname
                    }).ToListAsync();
                } 
                else
                {
                    chats = await _context.Chat.Where(c => c.DoctorId == request.UserId)
                    .Select(s => new ChatDto
                    {
                        ChatId = s.ChatId,
                        DoctorId = s.DoctorId,
                        DoctorName = s.Doctor.Firstname + " " + s.Doctor.Lastname,
                        PatientId = s.PatientId,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname
                    }).ToListAsync();
                }

                return await Task.FromResult(new GetChatsResponse
                {
                    Chats = chats
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetChatsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetChatMessagesResponse> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var chatMessages = await _context.ChatMessage.Where(c => c.ChatId == request.ChatId)
                    .Join(_context.Chat, m => m.ChatId, c => c.ChatId,
                    (m, c) => new
                    {
                        ChatId = m.ChatId,
                        Chat = c,
                        Messages = m,
                        Doctor = c.Doctor,
                        Patient = c.Patient
                    })
                    .Select(s => new MessageDto
                    {
                        ChatId = s.ChatId,
                        DoctorId = s.Doctor.UserId,
                        DoctorName = s.Doctor.Firstname + " " + s.Doctor.Lastname,
                        PatientId = s.Patient.UserId,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname,
                        IsPatientMessage = s.Patient.UserId == s.Messages.UserId ? true : false,
                        Message = s.Messages.Message,
                        MessageDate = s.Messages.MessageDate
                    })
                    .OrderBy(o => o.MessageDate)
                    .ToListAsync();

                return await Task.FromResult(new GetChatMessagesResponse
                {
                    ChatMessages = chatMessages
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetChatMessagesQuery error: {ex}");
                throw;
            }
        }
    }
}