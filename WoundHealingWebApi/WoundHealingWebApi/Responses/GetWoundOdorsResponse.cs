using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundOdorsResponse
    {
        public List<WoundOdor> WoundOdors { get; set; }
    }
}