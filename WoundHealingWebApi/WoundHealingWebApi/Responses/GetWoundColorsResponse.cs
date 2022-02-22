using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundColorsResponse
    {
        public List<WoundColor> WoundColors { get; set; }
    }
}