using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("AppointmentController");

        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("GetFastestAppointment")]
        [HttpGet]
        public async Task<FastestAppointmentDto> GetFastestAppointment()
        {
            Log.Info($"GetFastestAppointment");
            return (await _mediator.Send(new GetFastestAppointmentQuery
            {
                Date = DateTime.Now.AddHours(1)
            })).FastestAppointment;
        }

        [Route("GetMyAppointments/{patientId}")]
        [HttpGet]
        public async Task<List<MyAppointmentDto>> GetMyAppointments([FromRoute] int patientId)
        {
            Log.Info($"GetMyAppointments patientId: {patientId}");
            return (await _mediator.Send(new GetMyAppointmentsQuery
            {
                PatientId = patientId
            })).Appointments;
        }

        [Route("GetMyAppointmentsDoc/{doctorId}")]
        [HttpGet]
        public async Task<List<MyAppointmentDocDto>> GetMyAppointmentsDoc([FromRoute] int doctorId)
        {
            Log.Info($"GetMyAppointmentsDoc doctorId: {doctorId}");
            return (await _mediator.Send(new GetMyAppointmentsDocQuery
            {
                DoctorId = doctorId
            })).Appointments;
        }

        [Route("GetAppointmentSummary/{appointmentId}")]
        [HttpGet]
        public async Task<AppointmentSummaryDto> GetAppointmentSummary([FromRoute] int appointmentId)
        {
            Log.Info($"GetAppointmentSummary appointmentId: {appointmentId}");
            return (await _mediator.Send(new GetAppointmentSummaryQuery
            {
                AppointmentId = appointmentId
            })).AppointmentSummary;
        }

        [Route("GetAppointmentNote/{appointmentId}")]
        [HttpGet]
        public async Task<AppointmentNoteDto> GetAppointmentNote([FromRoute] int appointmentId)
        {
            Log.Info($"GetAppointmentNote appointmentId: {appointmentId}");
            return (await _mediator.Send(new GetAppointmentNoteQuery
            {
                AppointmentId = appointmentId
            })).AppointmentNote;
        }

        [Route("GetAppointmentsPerDate")]
        [HttpPost]
        public async Task<List<FastestAppointmentDto>> GetAppointmentsPerDate([FromBody] FindAppointmentDto request)
        {
            Log.Info($"GetAppointmentsPerDate request: {request}");
            var now = DateTime.Now;
            var date = request.Date;
            if(date.Day > now.Day)
            {
                date = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            }
            else
            {
                date = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
            }
            return (await _mediator.Send(new GetAppointmentsPerDateQuery
            {
                Date = date.AddHours(1),
                DoctorId = request.DoctorId ?? null
            })).Appointments;
        }

        [Route("SaveAppointmentNote")]
        [HttpPost]
        public async Task SaveAppointmentNote([FromBody] AppointmentNoteDto request)
        {
            Log.Info($"SaveAppointmentNote request: {request}");
            await _mediator.Send(new SaveAppointmentNoteRequest
            {
                AppointmentNoteDto = request
            });
        }

        [Route("CreateTreatmentWithAppointment")]
        [HttpPost]
        public async Task CreateTreatmentWithAppointment([FromBody] CreateTreatmentWithAppointmentRequest request)
        {
            Log.Info($"CreateTreatmentWithAppointment request: {request}");
            await _mediator.Send(request);
        }

        [Route("CreateAppointment")]
        [HttpPost]
        public async Task CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            Log.Info($"CreateAppointment request: {request}");
            await _mediator.Send(request);
        }

        [Route("DeleteAppointment/{appointmentId}")]
        [HttpPost]
        public async Task DeleteAppointment([FromRoute] int appointmentId)
        {
            Log.Info($"DeleteAppointment appointmentId: {appointmentId}");
            await _mediator.Send(new DeleteAppointmentRequest
            {
                AppointmentId = appointmentId
            });
        }

        [Route("DeleteAppointmentDoc/{appointmentId}")]
        [HttpPost]
        public async Task DeleteAppointmentDoc([FromRoute] int appointmentId)
        {
            Log.Info($"DeleteAppointmentDoc appointmentId: {appointmentId}");
            await _mediator.Send(new DeleteAppointmentRequest
            {
                AppointmentId = appointmentId
            });
        }
    }
}