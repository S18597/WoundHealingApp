using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetMyAppointmentsDocResponse
    {
        public List<MyAppointmentDocDto> Appointments { get; set; }
    }
}