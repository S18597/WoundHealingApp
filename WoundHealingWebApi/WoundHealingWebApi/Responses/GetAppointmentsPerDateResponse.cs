using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetAppointmentsPerDateResponse
    {
        public List<FastestAppointmentDto> Appointments { get; set; }
    }
}