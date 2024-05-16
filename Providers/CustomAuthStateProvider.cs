
using System.Runtime.CompilerServices;
using System.Security.Claims;
using LeoPasswordManagerUI.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(claimsPrincipal);
    }

    public void SetAuthInfo(UserDTO userDTO)
    {
        var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userDTO.Id),
            new Claim(ClaimTypes.Name, userDTO.FirstName + " " + userDTO.LastName),
            new Claim(ClaimTypes.Email, userDTO.Email),
            new Claim(ClaimTypes.Role, userDTO.Role),
        }, "custom_cookie_auth");

        claimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void ClearAuthInfo()
    {
    	claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    	NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}