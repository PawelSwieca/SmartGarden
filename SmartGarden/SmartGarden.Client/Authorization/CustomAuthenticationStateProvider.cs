using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using SmartGarden.Shared.DTOs;

namespace SmartGarden.Client.Authorization
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _http;

        public CustomAuthenticationStateProvider(HttpClient http)
        {
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userInfo = await _http.GetFromJsonAsync<UserInfo>("api/user/me");

                if (userInfo != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userInfo.UserId ?? ""),
                        new Claim(ClaimTypes.Name, userInfo.UserName ?? ""),
                        new Claim(ClaimTypes.Role, userInfo.Role ?? "User"),
                        new Claim("Nickname", userInfo.Nickname ?? "")
                    };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }
            catch
            {
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void MarkUserAsLoggedOut()
        {

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var new_state = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(new_state);
        }

        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}