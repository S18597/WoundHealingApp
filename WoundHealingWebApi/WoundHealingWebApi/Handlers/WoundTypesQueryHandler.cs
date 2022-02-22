using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingWebApi.Queries;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Handlers
{
    public class WoundTypesQueryHandler : IRequestHandler<GetPainLevelsQuery, GetPainLevelsResponse>,
                                          IRequestHandler<GetPainTypesQuery, GetPainTypesResponse>,
                                          IRequestHandler<GetSurroundingSkinsQuery, GetSurroundingSkinsResponse>,
                                          IRequestHandler<GetWoundBleedingsQuery, GetWoundBleedingsResponse>,
                                          IRequestHandler<GetWoundColorsQuery, GetWoundColorsResponse>,
                                          IRequestHandler<GetWoundExudatesQuery, GetWoundExudatesResponse>,
                                          IRequestHandler<GetWoundLocationsQuery, GetWoundLocationsResponse>,
                                          IRequestHandler<GetWoundOdorsQuery, GetWoundOdorsResponse>,
                                          IRequestHandler<GetWoundSizesQuery, GetWoundSizesResponse>,
                                          IRequestHandler<GetWoundTypesQuery, GetWoundTypesResponse>
    {
        public static Logger Log => LogManager.GetLogger("WoundTypesQueryHandler");

        private readonly WoundHealingContext _context;

        public WoundTypesQueryHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<GetPainLevelsResponse> Handle(GetPainLevelsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var painLevels = await _context.PainLevel.ToListAsync();

                return await Task.FromResult(new GetPainLevelsResponse
                {
                    PainLevels = painLevels
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetPainLevelsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetPainTypesResponse> Handle(GetPainTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var painTypes = await _context.PainType.ToListAsync();

                return await Task.FromResult(new GetPainTypesResponse
                {
                    PainTypes = painTypes
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetPainTypesQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetSurroundingSkinsResponse> Handle(GetSurroundingSkinsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var surroundingSkins = await _context.SurroundingSkin.ToListAsync();

                return await Task.FromResult(new GetSurroundingSkinsResponse
                {
                    SurroundingSkins = surroundingSkins
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetSurroundingSkinsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundBleedingsResponse> Handle(GetWoundBleedingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundBleedings = await _context.WoundBleeding.ToListAsync();

                return await Task.FromResult(new GetWoundBleedingsResponse
                {
                    WoundBleedings = woundBleedings
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundBleedingsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundColorsResponse> Handle(GetWoundColorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundColors = await _context.WoundColor.ToListAsync();

                return await Task.FromResult(new GetWoundColorsResponse
                {
                    WoundColors = woundColors
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundColorsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundExudatesResponse> Handle(GetWoundExudatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundExudates = await _context.WoundExudate.ToListAsync();

                return await Task.FromResult(new GetWoundExudatesResponse
                {
                    WoundExudates = woundExudates
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundExudatesQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundLocationsResponse> Handle(GetWoundLocationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundLocations = await _context.WoundLocation.ToListAsync();

                return await Task.FromResult(new GetWoundLocationsResponse
                {
                    WoundLocations = woundLocations
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundLocationsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundOdorsResponse> Handle(GetWoundOdorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundOdors = await _context.WoundOdor.ToListAsync();

                return await Task.FromResult(new GetWoundOdorsResponse
                {
                    WoundOdors = woundOdors
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundOdorsQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundSizesResponse> Handle(GetWoundSizesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundSizes = await _context.WoundSize.ToListAsync();

                return await Task.FromResult(new GetWoundSizesResponse
                {
                    WoundSizes = woundSizes
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundSizesQuery error: {ex}");
                throw;
            }
        }

        public async Task<GetWoundTypesResponse> Handle(GetWoundTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var woundTypes = await _context.WoundType.ToListAsync();

                return await Task.FromResult(new GetWoundTypesResponse
                {
                    WoundTypes = woundTypes
                });
            }
            catch (Exception ex)
            {
                Log.Error($"GetWoundTypesQuery error: {ex}");
                throw;
            }
        }
    }
}