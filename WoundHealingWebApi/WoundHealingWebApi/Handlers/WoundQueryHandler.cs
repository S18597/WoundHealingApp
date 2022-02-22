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
    public class WoundQueryHandler : IRequestHandler<GetNewestWoundIdByUserIdQuery, GetNewestWoundIdByUserIdResponse>,
                                     IRequestHandler<GetMyWoundsQuery, GetMyWoundsResponse>,
                                     IRequestHandler<GetWoundDetailsQuery, GetWoundDetailsResponse>,
                                     IRequestHandler<GetWoundPhotoQuery, GetWoundPhotoResponse>,
                                     IRequestHandler<GetWoundPhotoDetailsQuery, GetWoundPhotoDetailsResponse>
    {
        public static Logger Log => LogManager.GetLogger("WoundQueryHandler");

        private readonly WoundHealingContext _context;

        public WoundQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetNewestWoundIdByUserIdResponse> Handle(GetNewestWoundIdByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId < 1)
                {
                    Log.Error($"user id is empty!");
                    throw new Exception("user id is empty!");
                }

                var wound = await _context.Wound.Where(u => u.UserId == request.UserId)
                    .OrderByDescending(o => o.WoundRegisterDate)
                    .FirstOrDefaultAsync();

                if (wound == null)
                {
                    Log.Error($"cound not find newest wound for userId: {request.UserId}");
                    throw new Exception("wound not found!");
                }

                return await Task.FromResult(new GetNewestWoundIdByUserIdResponse
                {
                    WoundId = wound.WoundId
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetNewestWoundIdByUserIdQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyWoundsResponse> Handle(GetMyWoundsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PatientId < 1)
                {
                    Log.Error($"patient id is empty!");
                    throw new Exception("patient id is empty!");
                }

                // get patient
                var patient = await _context.User.Where(u => u.UserId == request.PatientId).FirstOrDefaultAsync();
                if (patient == null)
                {
                    Log.Error($"Patient with userId: {request.PatientId} not found!");
                    throw new Exception("Patient not found!");
                }

                // get wounds
                var wounds = await _context.Wound.Where(w => w.UserId == request.PatientId)
                    .OrderByDescending(o => o.WoundRegisterDate)
                    .Select(s => new MyWoundDto
                    {
                        WoundId = s.WoundId,
                        WoundType = s.WoundType.WoundTypeName,
                        WoundLocation = s.WoundLocation.WoundLocationName,
                        WoundSize = s.WoundSize.WoundSizeName,
                        WoundColor = s.WoundColor.WoundColorName,
                        WoundOdor = s.WoundOdor.WoundOdorName,
                        WoundExudate = s.WoundExudate.WoundExudateName,
                        WoundBleeding = s.WoundBleeding.WoundBleedingName,
                        SurroundingSkin = s.SurroundingSkin.SurroundingSkinName,
                        PainType = s.PainType.PainTypeName,
                        PainLevel = s.PainLevel.PainLevelName,
                        WoundRegisterDate = s.WoundRegisterDate
                    })
                    .ToListAsync();
                
                return await Task.FromResult(new GetMyWoundsResponse
                {
                    MyWounds = wounds
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetMyWoundsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundDetailsResponse> Handle(GetWoundDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.WoundId < 1)
                {
                    Log.Error($"wound id is empty!");
                    throw new Exception("wound id is empty!");
                }

                // get wound details
                var woundDetails = await _context.Wound.Where(w => w.WoundId == request.WoundId).Select(s => new MyWoundDto
                {
                    WoundId = s.WoundId,
                    WoundType = s.WoundType.WoundTypeName,
                    WoundLocation = s.WoundLocation.WoundLocationName,
                    WoundSize = s.WoundSize.WoundSizeName,
                    WoundColor = s.WoundColor.WoundColorName,
                    WoundOdor = s.WoundOdor.WoundOdorName,
                    WoundExudate = s.WoundExudate.WoundExudateName,
                    WoundBleeding = s.WoundBleeding.WoundBleedingName,
                    SurroundingSkin = s.SurroundingSkin.SurroundingSkinName,
                    PainType = s.PainType.PainTypeName,
                    PainLevel = s.PainLevel.PainLevelName,
                    WoundRegisterDate = s.WoundRegisterDate
                }).FirstOrDefaultAsync();

                return await Task.FromResult(new GetWoundDetailsResponse
                {
                    WoundDetails = woundDetails
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundDetailsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundPhotoResponse> Handle(GetWoundPhotoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.WoundPhotoId < 1)
                {
                    Log.Error($"wound photo id is empty!");
                    throw new Exception("wound photo id is empty!");
                }

                var woundPhoto = await _context.WoundPhoto.Where(p => p.WoundPhotoId == request.WoundPhotoId).FirstOrDefaultAsync();

                if (woundPhoto == null)
                {
                    Log.Error($"wound photo not found!");
                    throw new Exception("wound photo not found!");
                }

                Log.Info($"wound photo data: {woundPhoto.FileData}");

                return await Task.FromResult(new GetWoundPhotoResponse
                {
                    WoundPhoto = woundPhoto.FileData
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundDetailsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundPhotoDetailsResponse> Handle(GetWoundPhotoDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.WoundId < 1)
                {
                    Log.Error($"wound id is empty!");
                    throw new Exception("wound id is empty!");
                }

                var wound = await _context.Wound.Where(p => p.WoundId == request.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"wound not found!");
                    throw new Exception("wound not found!");
                }

                var woundPhotos = await _context.WoundPhoto.Where(p => p.WoundId == request.WoundId)
                    .Select(s => new WoundPhotoDetailDto
                    {
                        WoundPhotoId = s.WoundPhotoId,
                        WoundPhotoName = s.Filename
                    }).ToListAsync();


                return await Task.FromResult(new GetWoundPhotoDetailsResponse
                {
                    WoundPhotoDetails = woundPhotos
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundPhotoDetailsQuery error: {ex}");
                throw;
            }
        }
    }
}