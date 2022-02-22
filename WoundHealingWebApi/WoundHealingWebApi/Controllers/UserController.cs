using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoundHealingDb.Models;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("UserController");

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task CreateUser([FromBody] CreateUserRequest request)
        {
            Log.Info($"CreateUser request: {request}");
            await _mediator.Send(request);
        }

        [Route("GetUserByEmail/{email}")]
        [HttpGet]
        public async Task<User> GetUserByEmail([FromRoute] string email)
        {
            Log.Info($"GetUserByEmail: {email}");
            return (await _mediator.Send(new GetUserByEmailQuery
            {
                Email = email
            })).User;
        }

        [Route("GetUserAuthByUserId/{userId}")]
        [HttpGet]
        public async Task<Auth> GetUserAuthByUserId([FromRoute] int userId)
        {
            Log.Info($"GetUserAuthByUserId: {userId}");
            return (await _mediator.Send(new GetUserAuthByUserIdQuery
            {
                UserId = userId
            })).UserAuth;
        }

        [Route("GetDoctors")]
        [HttpGet]
        public async Task<List<DoctorDto>> GetDoctors()
        {
            Log.Info($"GetDoctors");
            return (await _mediator.Send(new GetDoctorsQuery
            {
            })).Doctors;
        }

        [Route("GetDoctorStats/{doctorId}")]
        [HttpGet]
        public async Task<StatsDto> GetDoctorStats([FromRoute] int doctorId)
        {
            Log.Info($"GetDoctorStats doctorId: {doctorId}");
            return (await _mediator.Send(new GetDoctorStatsQuery
            {
                DoctorId = doctorId
            })).DoctorStats;
        }

        [Route("GetMyPatients/{doctorId}")]
        [HttpGet]
        public async Task<List<MyPatientDto>> GetMyPatients([FromRoute] int doctorId)
        {
            Log.Info($"GetMyPatients doctorId: {doctorId}");
            return (await _mediator.Send(new GetMyPatientsQuery
            {
                DoctorId = doctorId
            })).MyPatients;
        }

        [Route("GetMyPatientsWounds/{doctorId}")]
        [HttpGet]
        public async Task<List<MyPatientWoundDto>> GetMyPatientsWounds([FromRoute] int doctorId)
        {
            Log.Info($"GetMyPatientsWounds doctorId: {doctorId}");
            return (await _mediator.Send(new GetMyPatientsWoundsQuery
            {
                DoctorId = doctorId
            })).MyPatientsWounds;
        }
    }
}