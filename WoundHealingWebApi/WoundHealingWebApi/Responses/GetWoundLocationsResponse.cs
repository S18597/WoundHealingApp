using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundLocationsResponse
    {
        public List<WoundLocation> WoundLocations { get; set; }
    }
}