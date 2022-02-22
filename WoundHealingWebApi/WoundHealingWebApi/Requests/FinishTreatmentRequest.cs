using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Requests
{
    public class FinishTreatmentRequest : IRequest
    {
        public FinishTreatmentDto FinishTreatmentDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}