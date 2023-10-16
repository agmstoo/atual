using Middleware.Cache;

namespace Middleware.Service
{
    public class ProcessService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Executa em ordem a lista de processamento pelo horário do recebimento
            while (true)
            {
                var managementRequest = CacheManagement.Requests.Where(x => x.InProcessing()).OrderBy(x => x.ReceivedTime).FirstOrDefault();
                if (managementRequest != null)
                    managementRequest.SetProcessedTimeTime();

                // Aguardamos 10 segundo para facilitar no teste da fila de processamento
                await Task.Delay(10000);
            }
        }
    }
}
