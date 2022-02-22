using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundPhotoQuery : IRequest<GetWoundPhotoResponse>
    {
        public int WoundPhotoId { get; set; }
    }
}