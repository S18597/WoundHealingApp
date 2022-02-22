using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundBleedingsResponse
    {
        public List<WoundBleeding> WoundBleedings { get; set; }
    }
}