using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyPatientsWoundsResponse
    {
        public List<MyPatientWoundDto> MyPatientsWounds { get; set; }
    }
}