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
    public class MedicalDataRequestHandler : IRequestHandler<SaveMedicalDataRequest>
    {
        public static Logger Log => LogManager.GetLogger("MedicalDataRequestHandler");

        private readonly WoundHealingContext _context;

        public MedicalDataRequestHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SaveMedicalDataRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if user exists
                var user = await _context.User.Where(u => u.UserId == request.MedicalData.UserId).FirstOrDefaultAsync();
                if (user == null)
                {
                    Log.Error($"User with id: {request.MedicalData.UserId} not found!");
                    throw new Exception("User not found!");
                }

                var medicalDataDb = await _context.MedicalData.Where(u => u.UserId == request.MedicalData.UserId).FirstOrDefaultAsync();
                if(medicalDataDb == null)
                {
                    var medicalData = new MedicalData
                    {
                        UserId = request.MedicalData.UserId,
                        ChronicDeseases = request.MedicalData.ChronicDiseases,
                        Medication = request.MedicalData.Medication,
                        Allergies = request.MedicalData.Allergies,
                        Alcohol = request.MedicalData.Alcohol,
                        Drugs = request.MedicalData.Drugs,
                        Pregnancy = request.MedicalData.Pregnancy,
                        Tobacco = request.MedicalData.Tobacco
                    };

                    await _context.MedicalData.AddAsync(medicalData);
                    await _context.SaveChangesAsync();

                    Log.Info($"Medical data created: {medicalData}");

                    return await Task.FromResult(Unit.Value);
                }

                medicalDataDb.ChronicDeseases = request.MedicalData.ChronicDiseases;
                medicalDataDb.Allergies = request.MedicalData.Allergies;
                medicalDataDb.Medication = request.MedicalData.Medication;
                medicalDataDb.Alcohol = request.MedicalData.Alcohol;
                medicalDataDb.Drugs = request.MedicalData.Drugs;
                medicalDataDb.Pregnancy = request.MedicalData.Pregnancy;
                medicalDataDb.Tobacco = request.MedicalData.Tobacco;

                await _context.SaveChangesAsync();

                Log.Info($"Medical data updated: {medicalDataDb}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"SaveMedicalData error: {ex}");
                throw;
            }
        }
    }
}