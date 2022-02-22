using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Requests
{
    public class SaveMedicalDataRequest : IRequest
    {
        public MedicalDataDto MedicalData { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}