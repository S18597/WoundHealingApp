using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetPainLevelsResponse
    {
        public List<PainLevel> PainLevels { get; set; }
    }
}