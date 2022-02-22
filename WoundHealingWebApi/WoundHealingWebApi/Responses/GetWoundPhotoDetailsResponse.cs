using System.Collections.Generic;
using WoundHealingWebApi.DTOs;

namespace WoundHealingWebApi.Responses
{
    public class GetWoundPhotoDetailsResponse
    {
        public List<WoundPhotoDetailDto> WoundPhotoDetails { get; set; }
    }
}