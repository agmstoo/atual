using Middleware.Models;

namespace Middleware.Service
{
    public interface IMainService
    {
        List<RequestInfoResponse> GetByIp(HttpContext context);
    }
}
