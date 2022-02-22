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
    public class AppointmentQueryHandler : IRequestHandler<GetFastestAppointmentQuery, GetFastestAppointmentResponse>,
                                           IRequestHandler<GetAppointmentsPerDateQuery, GetAppointmentsPerDateResponse>,
                                           IRequestHandler<GetMyAppointmentsQuery, GetMyAppointmentsResponse>,
                                           IRequestHandler<GetMyAppointmentsDocQuery, GetMyAppointmentsDocResponse>,
                                           IRequestHandler<GetMyWoundAppointmentQuery, GetMyWoundAppointmentResponse>,
                                           IRequestHandler<GetAppointmentSummaryQuery, GetAppointmentSummaryResponse>,
                                           IRequestHandler<GetAppointmentNoteQuery, GetAppointmentNoteResponse>
    {
        public static Logger Log => LogManager.GetLogger("AppointmentQueryHandler");

        private readonly WoundHealingContext _context;

        public AppointmentQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetFastestAppointmentResponse> Handle(GetFastestAppointmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Date == null)
                {
                    Log.Error($"date is empty!");
                    throw new Exception("date is empty!");
                }
                var date = request.Date;

                // get start full hour for date
                var start = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
                if (date.Minute > 0) start.AddHours(1);

                var visitsStart = new DateTime(start.Year, start.Month, start.Day, 8, 0, 0);
                var visitsEnd = new DateTime(start.Year, start.Month, start.Day, 16, 0, 0);
                if(start >= visitsEnd)
                {
                    // go to next day
                    visitsStart = visitsStart.AddDays(1);
                    visitsEnd = visitsEnd.AddDays(1);
                    start = visitsStart;
                }

                // get needed data
                var doctorAppointments = _context.Appointment.Where(a => a.AppointmentDate >= start)
                    .Join(_context.Treatment, a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        DoctorId = t.DoctorId,
                        a
                    })
                    .Select(s => new { s.DoctorId, s.a.AppointmentDate }).ToList();

                var doctors = _context.User.Where(u => u.IsDoctor)
                    .Select(s => new
                    {
                        DoctorId = s.UserId,
                        DoctorName = s.Firstname + " " + s.Lastname
                    }).ToList();

                var appointment = new FastestAppointmentDto();

                var notFound = true;
                while (notFound)
                {
                    foreach(var doc in doctors)
                    {
                        var visits = doctorAppointments.Where(a => a.DoctorId == doc.DoctorId && a.AppointmentDate == start).ToList();
                        if (!visits.Any())
                        {
                            // found appointment
                            notFound = false;

                            appointment.DoctorId = doc.DoctorId;
                            appointment.DoctorFullname = doc.DoctorName;
                            appointment.AppointmentDate = start;

                            break;
                        }
                    }

                    // check next hour
                    start = start.AddHours(1);
                    if (start >= visitsEnd)
                    {
                        // go to next day
                        visitsStart = visitsStart.AddDays(1);
                        visitsEnd = visitsEnd.AddDays(1);
                        start = visitsStart;
                    }
                }

                return await Task.FromResult(new GetFastestAppointmentResponse
                {
                    FastestAppointment = appointment
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetFastestAppointmentQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetAppointmentsPerDateResponse> Handle(GetAppointmentsPerDateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Date == null)
                {
                    Log.Error($"date is empty!");
                    throw new Exception("date is empty!");
                }
                var date = request.Date;

                // get start full hour for date
                var start = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
                if (date.Minute > 0) start.AddHours(1);

                var visitsStart = new DateTime(start.Year, start.Month, start.Day, 8, 0, 0);
                var visitsEnd = new DateTime(start.Year, start.Month, start.Day, 16, 0, 0);
                if (start >= visitsEnd)
                {
                    // go to next day
                    visitsStart = visitsStart.AddDays(1);
                    visitsEnd = visitsEnd.AddDays(1);
                    start = visitsStart;
                }

                // get needed data
                var doctorAppointments = _context.Appointment.Where(a => a.AppointmentDate >= start && a.AppointmentDate <= visitsEnd)
                    .Join(_context.Treatment, a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        DoctorId = t.DoctorId,
                        a
                    })
                    .Select(s => new { s.DoctorId, s.a.AppointmentDate }).ToList();

                var doctors = _context.User.Where(u => u.IsDoctor)
                .Select(s => new
                {
                    DoctorId = s.UserId,
                    DoctorName = s.Firstname + " " + s.Lastname
                }).ToList();

                if (request.DoctorId.HasValue)
                {
                    doctors = doctors.Where(d => d.DoctorId == request.DoctorId).ToList();
                }
                
                var appointments = new List<FastestAppointmentDto>();
                var notFinished = true;
                while (notFinished)
                {
                    foreach (var doc in doctors)
                    {
                        var visits = doctorAppointments.Where(a => a.DoctorId == doc.DoctorId && a.AppointmentDate == start).ToList();
                        if (!visits.Any())
                        {
                            // found available appointment
                            var appointment = new FastestAppointmentDto
                            {
                                DoctorId = doc.DoctorId,
                                DoctorFullname = doc.DoctorName,
                                AppointmentDate = start
                            };
                            appointments.Add(appointment);
                        }
                    }

                    // check next hour
                    start = start.AddHours(1);
                    if (start > visitsEnd)
                    {
                        notFinished = false;
                        break;
                    }
                }

                appointments = appointments.OrderBy(o => o.AppointmentDate).ToList();

                return await Task.FromResult(new GetAppointmentsPerDateResponse
                {
                    Appointments = appointments
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetAppointmentsPerDateQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyAppointmentsResponse> Handle(GetMyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PatientId < 1)
                {
                    Log.Error($"patient id is empty!");
                    throw new Exception("pateint id is empty!");
                }

                var doctors = await _context.User.Where(u => u.IsDoctor)
               .Select(s => new
               {
                   DoctorId = s.UserId,
                   DoctorName = s.Firstname + " " + s.Lastname
               }).ToListAsync();

                var appointments = _context.Appointment
                    .Join(_context.Treatment.Where(tr => tr.PatientId == request.PatientId), a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        DoctorId = t.DoctorId,
                        Doctor = t.Doctor,
                        a
                    })
                    .Select(s => new MyAppointmentDto
                    {
                        AppointmentId = s.a.AppointmentId,
                        AppointmentDate = s.a.AppointmentDate,
                        AppointmentNotes = s.a.AppointmentNotes,
                        DoctorName = s.Doctor.Firstname + " " + s.Doctor.Lastname
                    })
                    .OrderByDescending(o => o.AppointmentDate)
                    .ToList();

                return await Task.FromResult(new GetMyAppointmentsResponse
                {
                    Appointments = appointments
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetAppointmentsPerDateQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyAppointmentsDocResponse> Handle(GetMyAppointmentsDocQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.DoctorId < 1)
                {
                    Log.Error($"doctor id is empty!");
                    throw new Exception("doctor id is empty!");
                }

                var patients = await _context.User.Where(u => u.IsPatient)
               .Select(s => new
               {
                   DoctorId = s.UserId,
                   DoctorName = s.Firstname + " " + s.Lastname
               }).ToListAsync();


                var now = DateTime.Now;
                //var appointments = _context.Appointment.Where(a => a.AppointmentDate >= now)
                    var appointments = _context.Appointment
                    .Join(_context.Treatment.Where(tr => tr.DoctorId == request.DoctorId), a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        PatientId = t.PatientId,
                        Patient = t.Patient,
                        a
                    })
                    .Select(s => new MyAppointmentDocDto
                    {
                        AppointmentId = s.a.AppointmentId,
                        AppointmentDate = s.a.AppointmentDate,
                        AppointmentNotes = s.a.AppointmentNotes,
                        PatientName = s.Patient.Firstname + " " + s.Patient.Lastname
                    })
                    .OrderByDescending(o => o.AppointmentDate)
                    .ToList();

                return await Task.FromResult(new GetMyAppointmentsDocResponse
                {
                    Appointments = appointments
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetMyAppointmentsDocQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetMyWoundAppointmentResponse> Handle(GetMyWoundAppointmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PatientId < 1)
                {
                    Log.Error($"patient id is empty!");
                    throw new Exception("pateint id is empty!");
                }

                var doctors = await _context.User.Where(u => u.IsDoctor)
                   .Select(s => new
                   {
                       DoctorId = s.UserId,
                       DoctorName = s.Firstname + " " + s.Lastname
                   }).ToListAsync();

                var woundAppointments = _context.Appointment
                    .Join(_context.Treatment.Where(tr => tr.PatientId == request.PatientId), a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        WoundId = t.WoundId,
                        Wound = t.Wound,
                        DoctorId = t.DoctorId,
                        Doctor = t.Doctor,
                        a
                    })
                    .Select(s => new WoundAppointmentDto
                    {
                        WoundId = s.WoundId,
                        WoundType = s.Wound.WoundType.WoundTypeName,
                        WoundRegisterDate = s.Wound.WoundRegisterDate,
                        Appointment = new MyAppointmentDto
                        {
                            AppointmentId = s.a.AppointmentId,
                            AppointmentDate = s.a.AppointmentDate,
                            AppointmentNotes = s.a.AppointmentNotes,
                            DoctorName = s.Doctor.Firstname + " " + s.Doctor.Lastname
                        }
                    })
                    .OrderByDescending(o => o.Appointment.AppointmentDate)
                    .ToList();

                return await Task.FromResult(new GetMyWoundAppointmentResponse
                {
                    WoundAppointments = woundAppointments
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetAppointmentsPerDateQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetAppointmentSummaryResponse> Handle(GetAppointmentSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AppointmentId < 1)
                {
                    Log.Error($"appointment id is empty!");
                    throw new Exception("appointment id is empty!");
                }

                var appointmentSummary = await _context.Appointment.Where(a => a.AppointmentId == request.AppointmentId)
                    .Join(_context.Treatment, a => a.TreatmentId, t => t.TreatmentId,
                    (a, t) => new
                    {
                        Wound = t.Wound,
                        Doctor = t.Doctor,
                        Patient = t.Patient,
                        a
                    })
                    .Select(s => new AppointmentSummaryDto
                    {
                        AppointmentId = s.a.AppointmentId,
                        WoundId = s.Wound.WoundId,
                        Doctor = s.Doctor.Firstname + " " + s.Doctor.Lastname,
                        Patient = s.Patient.Firstname + " " + s.Patient.Lastname,
                        Date = s.a.AppointmentDate,
                        WoundType = s.Wound.WoundType.WoundTypeName
                    })
                    .FirstOrDefaultAsync();

                return await Task.FromResult(new GetAppointmentSummaryResponse
                {
                    AppointmentSummary = appointmentSummary
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetAppointmentSummaryResponse error: {ex}");
                throw;
            }
        }

        public async Task<GetAppointmentNoteResponse> Handle(GetAppointmentNoteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AppointmentId < 1)
                {
                    Log.Error($"appointment id is empty!");
                    throw new Exception("appointment id is empty!");
                }

                var appointment = await _context.Appointment.Where(a => a.AppointmentId == request.AppointmentId).FirstOrDefaultAsync();

                if (appointment == null)
                {
                    Log.Error($"appointment not found");
                    throw new Exception("appointment not found!");
                }

                var appointmentNote = new AppointmentNoteDto
                {
                    AppointmentId = request.AppointmentId,
                    AppointmentNote = appointment.AppointmentNotes
                };

                return await Task.FromResult(new GetAppointmentNoteResponse
                {
                    AppointmentNote = appointmentNote
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetAppointmentNote error: {ex}");
                throw;
            }
        }
    }
}