using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingDb.Models;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Handlers
{
    public class WoundRequestHandler : IRequestHandler<CreateWoundRequest>,
                                       IRequestHandler<DeleteWoundRequest>,
                                       IRequestHandler<UploadWoundPhotoRequest>,
                                       IRequestHandler<FinishTreatmentRequest>
    {
        public static Logger Log => LogManager.GetLogger("WoundRequestHandler");

        private readonly WoundHealingContext _context;

        public WoundRequestHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateWoundRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if user exists
                var user = await _context.User.Where(u => u.UserId == request.UserId).FirstOrDefaultAsync();
                if (user == null)
                {
                    Log.Error($"User with id: {request.UserId} not found!");
                    throw new Exception("User not found!");
                }

                // create wound
                var wound = new Wound
                {
                    UserId = request.UserId,
                    WoundTypeId = request.WoundTypeId,
                    WoundLocationId = request.WoundLocationId,
                    WoundSizeId = request.WoundSizeId,
                    WoundColorId = request.WoundColorId,
                    WoundOdorId = request.WoundOdorId,
                    WoundExudateId = request.WoundExudateId,
                    WoundBleedingId = request.WoundBleedingId,
                    SurroundingSkinId = request.SurroundingSkinId,
                    PainTypeId = request.PainTypeId,
                    PainLevelId = request.PainLevelId,
                    WoundRegisterDate = DateTime.Now
                };
               
                await _context.Wound.AddAsync(wound);
                await _context.SaveChangesAsync();

                Log.Info($"Wound created id: {wound?.WoundId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"CreateWound error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(DeleteWoundRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if wound exists
                var wound = await _context.Wound.Where(u => u.WoundId == request.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"Wound with id: {request.WoundId} not found!");
                    throw new Exception("Wound not found!");
                }

                _context.Wound.Remove(wound);
                await _context.SaveChangesAsync();

                Log.Info($"Wound deleted id: {wound?.WoundId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteWound error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(UploadWoundPhotoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if wound exists
                var wound = await _context.Wound.Where(u => u.WoundId == request.FileUploadDto.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"Wound with id: {request.FileUploadDto.WoundId} not found!");
                    throw new Exception("Wound not found!");
                }

                if (request.FileUploadDto.File == null)
                {
                    Log.Error($"File is empty!");
                    throw new Exception("File is empty!");
                }
                var file = request.FileUploadDto.File;

                var fileName = Path.GetFileName(file.FileName);
                Log.Info($"fileName: {fileName}");

                var fileExtension = Path.GetExtension(fileName);
                Log.Info($"fileExtension: {fileExtension}");

                var newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                Log.Info($"newFileName: {newFileName}");

                var photo = new WoundPhoto
                {
                    WoundId = request.FileUploadDto.WoundId,
                    Filename = newFileName
                };

                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    photo.FileData = target.ToArray();
                }

                _context.WoundPhoto.Add(photo);
                await _context.SaveChangesAsync();

                Log.Info($"Wound photo uploaded for id: {wound?.WoundId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"UploadWoundPhoto error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(FinishTreatmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if wound exists
                var wound = await _context.Wound.Where(u => u.WoundId == request.FinishTreatmentDto.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"Wound with id: {request.FinishTreatmentDto.WoundId} not found!");
                    throw new Exception("Wound not found!");
                }

                // check if doctor exists
                var doctor = await _context.User.Where(u => u.UserId == request.FinishTreatmentDto.DoctorId).FirstOrDefaultAsync();
                if (doctor == null)
                {
                    Log.Error($"Doctor with id: {request.FinishTreatmentDto.DoctorId} not found!");
                    throw new Exception("Doctor not found!");
                }

                // check if patient exists
                var patient = await _context.User.Where(u => u.UserId == request.FinishTreatmentDto.PatientId).FirstOrDefaultAsync();
                if (patient == null)
                {
                    Log.Error($"Patient with id: {request.FinishTreatmentDto.PatientId} not found!");
                    throw new Exception("Patient not found!");
                }

                // find treatment to finish
                var treatment = await _context.Treatment
                    .Where(
                        u => u.PatientId == patient.UserId &&
                        u.DoctorId == doctor.UserId &&
                        u.WoundId == wound.WoundId &&
                        u.Status != WoundHealingDb.Enums.TreatmentStatus.Healed &&
                        !u.EndDate.HasValue
                    )
                    .FirstOrDefaultAsync();

                if (treatment == null)
                {
                    Log.Error($"Treatment not found!");
                    throw new Exception("Treatment not found!");
                }

                treatment.Status = WoundHealingDb.Enums.TreatmentStatus.Healed;
                treatment.EndDate = DateTime.Now;

                await _context.SaveChangesAsync();

                Log.Info($"Treatment finished id: {treatment?.TreatmentId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"FinishTreatmentRequest error: {ex}");
                throw;
            }
        }
    }
}