using Middleware.Cache;
using Middleware.Models;
using Middleware.Utils;

namespace Middleware.Service
{
    public class MainService : IMainService
    {
        public List<RequestInfoResponse> GetByIp(HttpContext context)
        {
            var response = new List<RequestInfoResponse>();

            var currentIp = Ip.GetIP(context);
            if (!string.IsNullOrEmpty(currentIp))
            {
                var managementRequest = CacheManagement.Requests.Where(x => x.IP == currentIp).ToList();
                if (managementRequest != null && managementRequest.Any())
                {
                    managementRequest.ForEach(x => 
                    { 
                        response.Add(new RequestInfoResponse(x.Id, x.ReceivedTime, x.ProcessedTime));
                    });
                }
            }

            return response;
        }
    }
}
