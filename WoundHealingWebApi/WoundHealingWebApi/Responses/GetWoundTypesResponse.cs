using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundTypesResponse
    {
        public List<WoundType> WoundTypes { get; set; }
    }
}