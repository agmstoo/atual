namespace Middleware.Domain
{
    public class ManagementRequest
    {
        public int Id { get; set; }

        public string IP { get; set; }

        public string ReceivedTime { get; set; }

        public string ProcessedTime { get; set; }

        public DateTime DateReceived { get; set; } = DateTime.Now;

        public ManagementRequest(int id, string ip, string receivedTime)
        {
            Id = id;
            IP = ip;
            ReceivedTime = receivedTime;
        }

        public bool InProcessing()
        {
            // Se tem data de recebimento, e não tem data de processado, está em processamento ainda
            return !string.IsNullOrEmpty(ReceivedTime) && string.IsNullOrEmpty(ProcessedTime);
        }

        public void SetProcessedTimeTime()
        {
            ProcessedTime = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
