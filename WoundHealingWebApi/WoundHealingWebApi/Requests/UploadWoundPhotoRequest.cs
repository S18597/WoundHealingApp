using MediatR;
using Newtonsoft.Json;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Requests
{
    public class UploadWoundPhotoRequest : IRequest
    {
        public FileUploadDto FileUploadDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}