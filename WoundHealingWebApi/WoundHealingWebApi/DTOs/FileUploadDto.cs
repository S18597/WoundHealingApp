using Microsoft.AspNetCore.Http;

namespace WoundHealingWebApi.DTOs
{
    public class FileUploadDto
    {
        public int WoundId { get; set; }
        public string Filename { get; set; }
        public IFormFile File { get; set; }
    }
}