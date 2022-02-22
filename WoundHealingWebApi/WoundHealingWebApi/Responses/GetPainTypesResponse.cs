using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetPainTypesResponse
    {
        public List<PainType> PainTypes { get; set; }
    }
}