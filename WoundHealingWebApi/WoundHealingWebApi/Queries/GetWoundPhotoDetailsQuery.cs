using MediatR;
using WoundHealingWebApi.Responses;

namespace WoundHealingWebApi.Queries
{
    public class GetWoundPhotoDetailsQuery : IRequest<GetWoundPhotoDetailsResponse>
    {
        public int WoundId { get; set; }
    }
}