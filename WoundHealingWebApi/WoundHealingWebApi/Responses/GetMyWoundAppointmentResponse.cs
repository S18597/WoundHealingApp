using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyWoundAppointmentResponse
    {
        public List<WoundAppointmentDto> WoundAppointments { get; set; }
    }
}