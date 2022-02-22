using MediatR;
using System;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetFastestAppointmentQuery : IRequest<GetFastestAppointmentResponse>
    {
        public DateTime Date { get; set; }
    }
}