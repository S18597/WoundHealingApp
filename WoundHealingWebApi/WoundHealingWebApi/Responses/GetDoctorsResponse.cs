using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetDoctorsResponse
    {
        public List<DoctorDto> Doctors { get; set; }
    }
}