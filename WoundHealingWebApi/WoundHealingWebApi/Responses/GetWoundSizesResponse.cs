using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundSizesResponse
    {
        public List<WoundSize> WoundSizes { get; set; }
    }
}