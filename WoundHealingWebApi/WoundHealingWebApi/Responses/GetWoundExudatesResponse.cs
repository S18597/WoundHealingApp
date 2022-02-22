using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundExudatesResponse
    {
        public List<WoundExudate> WoundExudates { get; set; }
    }
}