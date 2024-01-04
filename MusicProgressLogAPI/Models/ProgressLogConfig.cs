using MusicProgressLogAPI.Models.Domain;
using System.Net;

namespace MusicProgressLogAPI.Models
{
    public class ProgressLogConfig
    {
        public IEnumerable<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
