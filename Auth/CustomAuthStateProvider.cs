using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace PanelUserInventoryManagement.Auth;

public class CustomAuthStateProvider(HttpClient http) : AuthenticationStateProvider
{
    private string? _token;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (string.IsNullOrEmpty(_token))
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(_token), "jwt");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _token);
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void MarkUserAsAuthenticated(string token)
    {
        _token = token;
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        _token = null;
        http.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs!.Select(kvp =>
        {
            var claimType = kvp.Key switch
            {
                "role" => ClaimTypes.Role,
                "sub" => ClaimTypes.NameIdentifier,
                "email" => ClaimTypes.Email,
                "name" => ClaimTypes.Name,
                _ => kvp.Key
            };
            return new Claim(claimType, kvp.Value.ToString()!);
        });
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}