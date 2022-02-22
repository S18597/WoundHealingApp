using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingDb.Enums;
using WoundHealingDb.Models;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Handlers
{
    public class AppointmentRequestHandler : IRequestHandler<CreateTreatmentWithAppointmentRequest>,
                                             IRequestHandler<CreateAppointmentRequest>,
                                             IRequestHandler<DeleteAppointmentRequest>,
                                             IRequestHandler<SaveAppointmentNoteRequest>
    {
        public static Logger Log => LogManager.GetLogger("AppointmentRequestHandler");

        private readonly WoundHealingContext _context;

        public AppointmentRequestHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateTreatmentWithAppointmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if doctor exists
                var doctor = await _context.User.Where(u => u.UserId == request.DoctorId).FirstOrDefaultAsync();
                if (doctor == null)
                {
                    Log.Error($"Doctor with id: {request.DoctorId} does not exists!");
                    throw new Exception("Doctor not found!");
                }

                // check if pateint exists
                var patient = await _context.User.Where(u => u.UserId == request.PatientId).FirstOrDefaultAsync();
                if (patient == null)
                {
                    Log.Error($"Patient with id: {request.PatientId} does not exists!");
                    throw new Exception("Patient not found!");
                }

                // check if wound exists
                var wound = await _context.Wound.Where(u => u.WoundId == request.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"Wound with id: {request.WoundId} does not exists!");
                    throw new Exception("Wound not found!");
                }

                // create treatment
                var newTreatment = new Treatment
                {
                    WoundId = wound.WoundId,
                    DoctorId = doctor.UserId,
                    PatientId = patient.UserId,
                    StartDate = DateTime.Now,
                    Status = TreatmentStatus.InProgress
                };

                await _context.Treatment.AddAsync(newTreatment);
                await _context.SaveChangesAsync();

                Log.Info($"Treatment created: {newTreatment}");

                // create appointment
                var newAppointment = new Appointment
                {
                   TreatmentId = newTreatment.TreatmentId,
                   AppointmentDate = request.AppointmentDate
                };

                await _context.Appointment.AddAsync(newAppointment);
                await _context.SaveChangesAsync();

                // check if patient has chat with doctor if not create
                var chat = await _context.Chat.Where(c => c.DoctorId == request.DoctorId && c.PatientId == request.PatientId).FirstOrDefaultAsync();
                if (chat == null)
                {
                    var newChat = new Chat
                    {
                        DoctorId = request.DoctorId,
                        PatientId = request.PatientId
                    };

                    await _context.Chat.AddAsync(newChat, cancellationToken);
                    await _context.SaveChangesAsync();

                    Log.Info($"chat created");
                }

                Log.Info($"Appointment created: {newAppointment}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"CreateUser error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if doctor exists
                var doctor = await _context.User.Where(u => u.UserId == request.DoctorId).FirstOrDefaultAsync();
                if (doctor == null)
                {
                    Log.Error($"Doctor with id: {request.DoctorId} does not exists!");
                    throw new Exception("Doctor not found!");
                }

                // check if pateint exists
                var patient = await _context.User.Where(u => u.UserId == request.PatientId).FirstOrDefaultAsync();
                if (patient == null)
                {
                    Log.Error($"Patient with id: {request.PatientId} does not exists!");
                    throw new Exception("Patient not found!");
                }

                // check if wound exists
                var wound = await _context.Wound.Where(u => u.WoundId == request.WoundId).FirstOrDefaultAsync();
                if (wound == null)
                {
                    Log.Error($"Wound with id: {request.WoundId} does not exists!");
                    throw new Exception("Wound not found!");
                }

                // check if there is already a treatment
                var treatment = await _context.Treatment.Where(t => t.WoundId == wound.WoundId && t.PatientId == patient.UserId).FirstOrDefaultAsync();
                if(treatment == null)
                {
                    // create treatment
                    var newTreatment = new Treatment
                    {
                        WoundId = wound.WoundId,
                        DoctorId = doctor.UserId,
                        PatientId = patient.UserId,
                        StartDate = DateTime.Now,
                        Status = TreatmentStatus.InProgress
                    };

                    await _context.Treatment.AddAsync(newTreatment);
                    await _context.SaveChangesAsync();

                    treatment = newTreatment;

                    Log.Info($"treatment created id: {treatment.TreatmentId}");
                }

                // create appointment
                var appointment = new Appointment
                {
                    TreatmentId = treatment.TreatmentId,
                    AppointmentDate = request.AppointmentDate
                };

                await _context.Appointment.AddAsync(appointment);
                await _context.SaveChangesAsync();

                // check if patient has chat with doctor if not create
                var chat = await _context.Chat.Where(c => c.DoctorId == request.DoctorId && c.PatientId == request.PatientId).FirstOrDefaultAsync();
                if(chat == null)
                {
                    var newChat = new Chat
                    {
                        DoctorId = request.DoctorId,
                        PatientId = request.PatientId
                    };

                    await _context.Chat.AddAsync(newChat, cancellationToken);
                    await _context.SaveChangesAsync();

                    Log.Info($"chat created");
                }

                Log.Info($"appointment created id: {appointment.AppointmentId}");

                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"CreateUser error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(DeleteAppointmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AppointmentId < 1)
                {
                    Log.Error("appointment id is empty!");
                    throw new Exception("appointment id is empty!");
                }

                var appointment = await _context.Appointment.Where(a => a.AppointmentId == request.AppointmentId).FirstOrDefaultAsync();

                if(appointment == null)
                {
                    Log.Error("appointment not found!");
                    throw new Exception("appointment not found!");
                }

                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();

                Log.Info($"appointment with id: {request.AppointmentId} is deleted");

                return await Task.FromResult(Unit.Value);

            }
            catch (Exception ex)
            {
                Log.Error($"DeleteAppointment error: {ex}");
                throw;
            }
        }

        public async Task<Unit> Handle(SaveAppointmentNoteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AppointmentNoteDto.AppointmentId < 1)
                {
                    Log.Error("appointment id is empty!");
                    throw new Exception("appointment id is empty!");
                }

                var appointment = await _context.Appointment.Where(a => a.AppointmentId == request.AppointmentNoteDto.AppointmentId).FirstOrDefaultAsync();

                if (appointment == null)
                {
                    Log.Error("appointment not found!");
                    throw new Exception("appointment not found!");
                }

                appointment.AppointmentNotes = request.AppointmentNoteDto.AppointmentNote;
                await _context.SaveChangesAsync();
               
                return await Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"SaveAppointmentNote error: {ex}");
                throw;
            }
        }
    }
}