namespace Middleware.Models
{
    public class RequestInfoResponse
    {
        public int Id { get; set; }

        public string ReceivedTime { get; set; }

        public string ProcessedTime { get; set; }

        public RequestInfoResponse(int id, string receivedTime, string processedTime)
        {
            Id = id;
            ReceivedTime = receivedTime;
            ProcessedTime = processedTime;
        }
    }
}
