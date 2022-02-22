using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyWoundsResponse
    {
        public List<MyWoundDto> MyWounds { get; set; }
    }
}