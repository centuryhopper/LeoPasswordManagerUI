
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Blazored.LocalStorage;
using LeoPasswordManagerUI.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    private const string LocalStorageKey = "currentUser";
    private readonly ILocalStorageService localStorageService;

    public CustomAuthStateProvider(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task<UserDTO?> GetCurrentUserAsync() => await localStorageService.GetItemAsync<UserDTO>(LocalStorageKey);

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var currentUser = await GetCurrentUserAsync();

        if(currentUser == null)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, currentUser.Id),
            new Claim(ClaimTypes.Name, currentUser.FirstName + " " + currentUser.LastName),
            new Claim(ClaimTypes.Email, currentUser.Email),
            new Claim(ClaimTypes.Role, currentUser.Role),
        }, "custom_cookie_auth");

        var authenticationState = new AuthenticationState(new ClaimsPrincipal(identity));

        return authenticationState;
    }

    public async Task SetCurrentUserAsync(UserDTO? currentUser)
    {
        Console.WriteLine("setting current user");
        if (currentUser is null)
        {
            await localStorageService.RemoveItemsAsync(new[] {LocalStorageKey, "isauthenticated", "userid"});
        }
        else
        {
            await localStorageService.SetItemAsync(LocalStorageKey, currentUser);
            await localStorageService.SetItemAsync("isauthenticated", true);
            await localStorageService.SetItemAsStringAsync("userid", currentUser!.Id);
        }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
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