using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyPatientsResponse
    {
        public List<MyPatientDto> MyPatients { get; set; }
    }
}