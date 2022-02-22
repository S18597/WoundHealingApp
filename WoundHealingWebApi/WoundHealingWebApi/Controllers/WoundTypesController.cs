using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WoundHealingDb.Models;
using WoundHealingWebApi.Queries;

namespace WoundHealingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WoundTypesController : ControllerBase
    {
        public static Logger Log => LogManager.GetLogger("WoundTypesController");

        private readonly IMediator _mediator;

        public WoundTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("GetWoundTypes")]
        [HttpGet]
        public async Task<List<WoundType>> GetWoundTypes()
        {
            Log.Info($"GetWoundTypes");
            return (await _mediator.Send(new GetWoundTypesQuery
            {
            })).WoundTypes;
        }

        [Route("GetWoundLocations")]
        [HttpGet]
        public async Task<List<WoundLocation>> GetWoundLocations()
        {
            Log.Info($"GetWoundLocations");
            return (await _mediator.Send(new GetWoundLocationsQuery
            {
            })).WoundLocations;
        }

        [Route("GetWoundSizes")]
        [HttpGet]
        public async Task<List<WoundSize>> GetWoundSizes()
        {
            Log.Info($"GetWoundSizes");
            return (await _mediator.Send(new GetWoundSizesQuery
            {
            })).WoundSizes;
        }

        [Route("GetWoundColors")]
        [HttpGet]
        public async Task<List<WoundColor>> GetWoundColors()
        {
            Log.Info($"GetWoundColors");
            return (await _mediator.Send(new GetWoundColorsQuery
            {
            })).WoundColors;
        }

        [Route("GetWoundOdors")]
        [HttpGet]
        public async Task<List<WoundOdor>> GetWoundOdors()
        {
            Log.Info($"GetWoundOdors");
            return (await _mediator.Send(new GetWoundOdorsQuery
            {
            })).WoundOdors;
        }

        [Route("GetWoundExudates")]
        [HttpGet]
        public async Task<List<WoundExudate>> GetWoundExudates()
        {
            Log.Info($"GetWoundExudates");
            return (await _mediator.Send(new GetWoundExudatesQuery
            {
            })).WoundExudates;
        }

        [Route("GetWoundBleedings")]
        [HttpGet]
        public async Task<List<WoundBleeding>> GetWoundBleedings()
        {
            Log.Info($"GetWoundBleedings");
            return (await _mediator.Send(new GetWoundBleedingsQuery
            {
            })).WoundBleedings;
        }

        [Route("GetSurroundingSkins")]
        [HttpGet]
        public async Task<List<SurroundingSkin>> GetSurroundingSkins()
        {
            Log.Info($"GetSurroundingSkins");
            return (await _mediator.Send(new GetSurroundingSkinsQuery
            {
            })).SurroundingSkins;
        }

        [Route("GetPainTypes")]
        [HttpGet]
        public async Task<List<PainType>> GetPainTypes()
        {
            Log.Info($"GetPainTypes");
            return (await _mediator.Send(new GetPainTypesQuery
            {
            })).PainTypes;
        }

        [Route("GetPainLevels")]
        [HttpGet]
        public async Task<List<PainLevel>> GetPainLevels()
        {
            Log.Info($"GetPainLevels");
            return (await _mediator.Send(new GetPainLevelsQuery
            {
            })).PainLevels;
        }
    }
}