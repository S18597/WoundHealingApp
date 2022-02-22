using MediatR;
using System;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetAppointmentsPerDateQuery : IRequest<GetAppointmentsPerDateResponse>
    {
        public DateTime Date { get; set; }
        public int? DoctorId { get; set; }
    }
}