using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WoundHealingWebApi.DTOs;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WoundController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("WoundController");

        private readonly IMediator _mediator;

        public WoundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("CreateWound")]
        [HttpPost]
        public async Task CreateUser([FromBody] CreateWoundRequest request)
        {
            Log.Info($"CreateWound request: {request}");
            await _mediator.Send(request);
        }

        [Route("FinishTreatment")]
        [HttpPost]
        public async Task FinishTreatment([FromBody] FinishTreatmentDto request)
        {
            Log.Info($"FinishTreatment request: {request}");
            await _mediator.Send(new FinishTreatmentRequest
            {
                FinishTreatmentDto = request
            });
        }

        [Route("UploadWoundPhoto/{woundId}")]
        [HttpPost]
        public async Task UploadWoundPhoto([FromRoute] int woundId, [FromForm] byte[] file, [FromForm] string filename)
        {
            Log.Info($"UploadWoundPhoto woundId: {woundId}, filename: {filename}");

            var files = Request.Form.Files;
            Log.Info($"files count: {files.Count}, filename: {files[0]?.FileName}");

            await _mediator.Send(new UploadWoundPhotoRequest
            {
                FileUploadDto = new FileUploadDto
                {
                    File = files[0],
                    Filename = filename,
                    WoundId= woundId
                }
            });
        }

        [Route("GetWoundPhoto/{woundPhotoId}")]
        [HttpGet]
        public async Task<byte[]> GetWoundPhoto([FromRoute] int woundPhotoId)
        {
            Log.Info($"GetWoundPhoto: {woundPhotoId}");
            return (await _mediator.Send(new GetWoundPhotoQuery
            {
                WoundPhotoId = woundPhotoId
            })).WoundPhoto;
        }

        [Route("GetWoundPhoto2/{woundPhotoId}")]
        [HttpGet]
        public async Task<object> GetWoundPhoto2([FromRoute] int woundPhotoId)
        {
            Log.Info($"GetWoundPhoto: {woundPhotoId}");
            var bytes = _mediator.Send(new GetWoundPhotoQuery
            {
                WoundPhotoId = woundPhotoId
            }).Result.WoundPhoto;

            var tempFolderLocation = @"C:\Tugba\WoundHealingApp\WoundHealingWebApp\WoundHealingWebApp\src\assets\images";
            if (!Directory.Exists(tempFolderLocation))
            {
                Directory.CreateDirectory(tempFolderLocation);
            }

            var filename = $"{woundPhotoId}.jpeg";
            var fileFullPath = Path.Combine(tempFolderLocation, filename);

            await System.IO.File.WriteAllBytesAsync(fileFullPath, bytes);

            return new { filepath =  fileFullPath };
        }

        [Route("GetWoundPhotoDetails/{woundId}")]
        [HttpGet]
        public async Task<List<WoundPhotoDetailDto>> GetWoundPhotoDetails([FromRoute] int woundId)
        {
            Log.Info($"GetWoundPhotoDetails: {woundId}");
            return (await _mediator.Send(new GetWoundPhotoDetailsQuery
            {
                WoundId = woundId
            })).WoundPhotoDetails;
        }

        [Route("DeleteWound/{woundId}")]
        [HttpPost]
        public async Task DeleteWound([FromRoute] int woundId)
        {
            Log.Info($"DeleteWound id: {woundId}");
            await _mediator.Send(new DeleteWoundRequest
            {
                WoundId = woundId
            });
        }

        [Route("GetNewestWoundIdByUserId/{userId}")]
        [HttpGet]
        public async Task<int> GetNewestWoundIdByUserId([FromRoute] int userId)
        {
            Log.Info($"GetUserAuthByUserId: {userId}");
            return (await _mediator.Send(new GetNewestWoundIdByUserIdQuery
            {
                UserId = userId
            })).WoundId;
        }

        [Route("GetMyWounds/{userId}")]
        [HttpGet]
        public async Task<List<MyWoundDto>> GetMyWounds([FromRoute] int userId)
        {
            Log.Info($"GetMyWounds userId: {userId}");
            return (await _mediator.Send(new GetMyWoundsQuery
            {
                PatientId = userId
            })).MyWounds;
        }

        [Route("GetMyWoundAppointments/{userId}")]
        [HttpGet]
        public async Task<List<WoundAppointmentDto>> GetMyWoundAppointments([FromRoute] int userId)
        {
            Log.Info($"GetMyWoundAppointments userId: {userId}");
            return (await _mediator.Send(new GetMyWoundAppointmentQuery
            {
                PatientId = userId
            })).WoundAppointments;
        }

        [Route("GetWoundDetails/{woundId}")]
        [HttpGet]
        public async Task<MyWoundDto> GetWoundDetails([FromRoute] int woundId)
        {
            Log.Info($"GetWoundDetails woundId: {woundId}");
            return (await _mediator.Send(new GetWoundDetailsQuery
            {
                WoundId = woundId
            })).WoundDetails;
        }
    }
}