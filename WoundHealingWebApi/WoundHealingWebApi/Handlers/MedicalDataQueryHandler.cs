using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Handlers
{
    public class MedicalDataQueryHandler : IRequestHandler<GetMyMedicalDataQuery, GetMyMedicalDataResponse>
    {
        public static Logger Log => LogManager.GetLogger("MedicalDataQueryHandler");

        private readonly WoundHealingContext _context;

        public MedicalDataQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetMyMedicalDataResponse> Handle(GetMyMedicalDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId < 1)
                {
                    Log.Error($"user id is empty!");
                    throw new Exception("user id is empty!");
                }

                var medicalData = await _context.MedicalData.Where(m => m.UserId == request.UserId).FirstOrDefaultAsync();
                if(medicalData == null)
                {
                    Log.Error($"medical data for userId: {request.UserId} not found!");
                    throw new Exception("medical data not found!");
                }

                var user = await _context.User.Where(u => u.UserId == request.UserId).FirstOrDefaultAsync();
                if (user == null)
                {
                    Log.Error($"user with id: {request.UserId} not found!");
                    throw new Exception("user not found!");
                }

                var myMedicalData = new MyMedicalDataDto
                {
                    UserId = user.UserId,
                    UserName = user.Firstname,
                    UserSurname = user.Lastname,
                    UserDateOfBirth = user.DateOfBirth,
                    Pesel = user.Pesel,
                    ChronicDeseases = medicalData.ChronicDeseases,
                    Medication = medicalData.Medication,
                    Allergies = medicalData.Allergies,
                    Pregnancy = medicalData.Pregnancy,
                    Alcohol = medicalData.Alcohol,
                    Drugs = medicalData.Drugs,
                    Tobacco = medicalData.Tobacco
                };

                return await Task.FromResult(new GetMyMedicalDataResponse
                {
                    MyMedicalData = myMedicalData
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetMyMedicalDataQuery error: {ex}");
                throw;
            }
        }
    }
}