using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyAppointmentsResponse
    {
        public List<MyAppointmentDto> Appointments { get; set; }
    }
}