namespace Middleware.Utils
{
    public static class Ip
    {
        public static string? GetIP(HttpContext context) => context.Connection.RemoteIpAddress?.ToString();
    }
}
