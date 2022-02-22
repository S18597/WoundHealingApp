using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingDb.Enums;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Handlers
{
    public class UserQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>,
                                    IRequestHandler<GetUserAuthByUserIdQuery, GetUserAuthByUserIdResponse>,
                                    IRequestHandler<GetDoctorsQuery, GetDoctorsResponse>,
                                    IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>,
                                    IRequestHandler<GetDoctorStatsQuery, GetDoctorStatsResponse>,
                                    IRequestHandler<GetMyPatientsQuery, GetMyPatientsResponse>,
                                    IRequestHandler<GetMyPatientsWoundsQuery, GetMyPatientsWoundsResponse>

    {
        public static Logger Log => LogManager.GetLogger("UserQueryHandler");

        private readonly WoundHealingContext _context;

        public UserQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    Log.Error($"email is empty!");
                    throw new Exception("email is empty!");
                }

                var user = await _context.User.Where(u => u.EmailAddress == request.Email).FirstOrDefaultAsync();

                if (user == null)
                {
                    Log.Error($"user not found!");
                    throw new Exception("user not found!");
                }

                return await Task.FromResult(new GetUserByEmailResponse
                {
                   User = user
                });
            }
            catch(Exception ex)
            {
                Log.Error($"GetUserByEmailQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetUserAuthByUserIdResponse> Handle(GetUserAuthByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _context.Auth.Where(u => u.UserId == request.UserId).FirstOrDefaultAsync();

                if (auth == null)
                {
                    Log.Error($"user auth not found!");
                    throw new Exception("user auth not found!");
                }

                return await Task.FromResult(new GetUserAuthByUserIdResponse
                {
                    UserAuth = auth
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetUserAuthByUserIdQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetDoctorsResponse> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var doctors = await _context.User.Where(u => u.IsDoctor)
                    .Select(s => new DoctorDto
                    {
                        DoctorId = s.UserId,
                        DoctorName = s.Firstname + " " + s.Lastname
                    })
                    .ToListAsync();

                doctors = doctors.OrderBy(o => o.DoctorName).ToList();

                return await Task.FromResult(new GetDoctorsResponse
                {
                    Doctors = doctors
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetDoctorsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId < 1)
                {
                    Log.Error($"user id is empty!");
                    throw new Exception("user id is empty!");
                }

                var user = await _context.User.Where(u => u.UserId == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                {
                    Log.Error($"user not found!");
                    throw new Exception("user not found!");
                }

                return await Task.FromResult(new GetUserByIdResponse
                {
                    User = user
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetUserByIdQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetDoctorStatsResponse> Handle(GetDoctorStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.DoctorId < 1)
                {
                    Log.Error($"doctor id is empty!");
                    throw new Exception("doctor id is empty!");
                }

                var doctor = await _context.User.Where(u => u.UserId == request.DoctorId).FirstOrDefaultAsync();

                if (doctor == null)
                {
                    Log.Error($"doctor not found!");
                    //throw new Exception("doctor not found!");
                    return null;
                }

                // main stats
                var patientsCnt = await _context.Treatment.Where(t => t.DoctorId == doctor.UserId)
                    .Select(s => s.PatientId)
                    .Distinct()
                    .CountAsync();

                var finishedTreatmentCnt = await _context.Treatment.Where(t => t.DoctorId == doctor.UserId && t.Status == TreatmentStatus.Healed)
                    .CountAsync();

                var appointmensCnt = await _context.Appointment.Where(a => a.Treatment.DoctorId == doctor.UserId)
                    .CountAsync();

                var woundsCnt = await _context.Treatment.Where(t => t.DoctorId == doctor.UserId)
                    .Select(s => s.WoundId)
                    .Distinct()
                    .CountAsync();

                var treatmentDays = await _context.Treatment.Where(t => t.DoctorId == doctor.UserId && t.EndDate.HasValue)
                    .Select(s => new
                    {
                        TreatmentId = s.TreatmentId,
                        TreatmentDays = (s.EndDate - s.StartDate).Value.TotalDays
                    })
                    .ToListAsync();

                var avgTreatmentDays = treatmentDays.Count > 0 ? (int)treatmentDays.Average(a => a.TreatmentDays) : 0;

                // wound types stats
                var wounds = await _context.Treatment.Where(t => t.DoctorId == doctor.UserId)
                    .Select(s => new
                    {
                        WoundTypeId = s.Wound.WoundType.WoundTypeId,
                        WoundTypeName = s.Wound.WoundType.WoundTypeName
                    })
                    .GroupBy(g => g.WoundTypeName)
                    .Select(s => new WoundTypesStats
                    {
                        WoundType = s.Key,
                        WoundTypeCnt = s.Count()
                    })
                    .ToListAsync();

                var doctorStats = new StatsDto
                {
                    DoctorId = doctor.UserId,
                    PatientsCnt = patientsCnt,
                    AppointmentsCnt = appointmensCnt,
                    FinishedTreatmentsCnt = finishedTreatmentCnt,
                    WoundsCnt = woundsCnt,
                    AvgTreatmentDays = avgTreatmentDays,
                    WoundTypesStats = wounds
                };

                return await Task.FromResult(new GetDoctorStatsResponse
                {
                    DoctorStats = doctorStats
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetDoctorStatsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyPatientsResponse> Handle(GetMyPatientsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.DoctorId < 1)
                {
                    Log.Error($"doctor id is empty!");
                    throw new Exception("doctor id is empty!");
                }

                var doctor = await _context.User.Where(u => u.UserId == request.DoctorId).FirstOrDefaultAsync();

                if (doctor == null)
                {
                    Log.Error($"doctor not found!");
                    //throw new Exception("doctor not found!");
                    return null;
                }

                var myPatients = await _context.Treatment.Where(t => t.DoctorId == request.DoctorId)
                    .Select(s => new MyPatientDto
                    {
                        PatientId = s.PatientId,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname,
                        PatientEmail = s.Patient.EmailAddress
                    })
                    .Distinct()
                    .OrderBy(o => o.PatientName)
                    .ToListAsync();
               

                return await Task.FromResult(new GetMyPatientsResponse
                {
                    MyPatients = myPatients
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetMyPatientsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyPatientsWoundsResponse> Handle(GetMyPatientsWoundsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.DoctorId < 1)
                {
                    Log.Error($"doctor id is empty!");
                    throw new Exception("doctor id is empty!");
                }

                var doctor = await _context.User.Where(u => u.UserId == request.DoctorId).FirstOrDefaultAsync();

                if (doctor == null)
                {
                    Log.Error($"doctor not found!");
                    //throw new Exception("doctor not found!");
                    return null;
                }

                var myPatientsWounds = await _context.Treatment.Where(t => t.DoctorId == request.DoctorId)
                    .Select(s => new MyPatientWoundDto
                    {
                        WoundId = s.Wound.WoundId,
                        WoundType = s.Wound.WoundType.WoundTypeName,
                        PatientId = s.PatientId,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname,
                        PatientEmail = s.Patient.EmailAddress,
                        WoundPhoto = string.Empty
                    })
                    .Distinct()
                    .OrderBy(o => o.PatientName)
                    .ToListAsync();

                foreach(var w in myPatientsWounds)
                {
                    var woundPhoto = await _context.WoundPhoto.Where(p => p.WoundId == w.WoundId).FirstOrDefaultAsync();
                    Log.Info($"found wound photo for woundId {w.WoundId}: {woundPhoto?.WoundPhotoId}");
                    w.WoundPhotoId = woundPhoto != null ? woundPhoto.WoundPhotoId : 0;
                }

                return await Task.FromResult(new GetMyPatientsWoundsResponse
                {
                    MyPatientsWounds = myPatientsWounds
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetMyPatientsWoundsQuery error: {ex}");
                throw;
            }
        }
    }
}