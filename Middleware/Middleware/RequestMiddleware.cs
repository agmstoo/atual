using Middleware.Cache;
using Middleware.Domain;
using Middleware.Utils;

namespace Middleware.Middleware
{
    public class RequestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method == "POST")
            {
                var currentIp = Ip.GetIP(context);
                if (!string.IsNullOrEmpty(currentIp))
                    Action(currentIp);
            }

            await next(context);
        }

        private void Action(string currentIp)
        {
            AddRequest(currentIp);

            var managementRequests = CacheManagement.Requests.Where(x => x.IP == currentIp).ToList();
            if (managementRequests.Any() && managementRequests.Count > 3)
            {
                // Controle de 3 chamadas no intervalo de 10 segundos
                var limit = DateTime.Now.AddSeconds(-10);
                var calls = managementRequests.Where(x => x.IP == currentIp && x.DateReceived >= limit).ToList();

                if (calls.Count > 3)
                    // Utilizei exception porém aqui vale uma discussão do custo dessa exception
                    throw new Exception("Quantidades de chamadas excedidas");
            }

        }

        private void AddRequest(string currentIp)
        {
            var id = GetIdentity();
            var managementRequest = new ManagementRequest(id, currentIp, DateTime.Now.ToString("HH:mm:ss"));
            CacheManagement.Requests.Add(managementRequest);
        }

        private int GetIdentity()
        {
            if (!CacheManagement.Requests.Any())
                return 1;

            var managementRequest = CacheManagement.Requests.Max(x => x.Id);
            var newId = managementRequest += 1;

            return newId;
        }
    }
}
