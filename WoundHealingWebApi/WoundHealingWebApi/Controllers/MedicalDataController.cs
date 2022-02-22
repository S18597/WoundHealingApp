using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalDataController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("MedicalDataController");

        private readonly IMediator _mediator;

        public MedicalDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("SaveMedicalData")]
        [HttpPost]
        public async Task SaveMedicalData([FromBody] MedicalDataDto request)
        {
            Log.Info($"SaveMedicalData request: {request}");
            await _mediator.Send(new SaveMedicalDataRequest
            {
                MedicalData = request
            });
        }

        [Route("GetMedicalData/{userId}")]
        [HttpGet]
        public async Task<MyMedicalDataDto> GetMedicalData([FromRoute] int userId)
        {
            Log.Info($"GetMedicalData: {userId}");
            return (await _mediator.Send(new GetMyMedicalDataQuery
            {
                UserId = userId
            })).MyMedicalData;
        }
    }
}