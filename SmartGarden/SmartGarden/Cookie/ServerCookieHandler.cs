using Microsoft.AspNetCore.Http;
namespace SmartGarden.Cookie
{
    public class ServerCookieHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerCookieHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. Łapiemy oryginalne zapytanie, które przyszło do serwera od użytkownika
            var context = _httpContextAccessor.HttpContext;

            // 2. Jeśli zapytanie posiada nagłówek "Cookie" (zawierający naszą opaskę VIP)...
            if (context != null && context.Request.Headers.ContainsKey("Cookie"))
            {
                var cookies = context.Request.Headers["Cookie"].ToString();

                // 3. ...kopiujemy ten nagłówek i doklejamy go do nowego, wewnętrznego zapytania API
                request.Headers.Add("Cookie", cookies);
            }

            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
