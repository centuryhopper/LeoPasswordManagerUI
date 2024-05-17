
using System.Net.Http.Json;
using LeoPasswordManagerUI.DTOs;
using LeoPasswordManagerUI.Models;
using LeoPasswordManagerUI.Utils;
using Microsoft.AspNetCore.Http;

public class PasswordManagerAPIConsumerService : IPasswordManagerAPIConsumerService
{
    private readonly HttpClient httpClient;

    public PasswordManagerAPIConsumerService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient(Constants.HTTP_CLIENT_FACTORY);
    }

    public async Task<ServiceResponse> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse> ConfirmEmailAsync()
    {
        throw new NotImplementedException();
    }

/*
update
*/
    public async Task<PasswordManagerAccountDTO?> CreateAsync(PasswordManagerAccountDTO model)
    {
        var response = await httpClient.PostAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Passwords/create"), model);

        if (response.IsSuccessStatusCode)
        {
            return model;
        }

        return null;
    }

    public async Task<PasswordManagerAccountDTO?> DeletePasswordManagerAccountAsync(PasswordManagerAccountDTO model)
    {
        var response = await httpClient.DeleteAsync(Path.Combine(Constants.SERVER_BASE_URL, $"api/Passwords/delete/?passwordAccountId={model.Id}&userId={model.Userid}"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PasswordManagerAccountDTO?>();
        }

        return null;
    }

    public async Task<ServiceResponse> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PasswordManagerAccountDTO>> GetAllAccountsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<PasswordManagerAccountDTO>>(Path.Combine(Constants.SERVER_BASE_URL, $"api/Passwords/getaccounts"));


        return response is null ? Enumerable.Empty<PasswordManagerAccountDTO>() : response;
    }

    public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(string Msg, UserDTO? UserDTO)> GetUserProfileAsync()
    {
        var userDto = await httpClient.GetAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/get-user-profile"));

        if (userDto.IsSuccessStatusCode)
        {
            return ("success", await userDto.Content.ReadFromJsonAsync<UserDTO>());
        }

        if (userDto.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return ("unauthenticated", null);
        }

        return ("failed", null);

    }

    public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
    {
        var response = await httpClient.PostAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/login"), loginDTO);

        if (response.IsSuccessStatusCode)
        {
            return new LoginResponse(true, "success");
        }

        return new LoginResponse(false, "failed to login");
    }

    public async Task<ServiceResponse> LogoutAsync()
    {
        var response = await httpClient.GetAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/logout"));

        if (response.IsSuccessStatusCode)
        {
            return new ServiceResponse(true, "success");
        }

        return new ServiceResponse(false, "failed to log out");

    }

    public async Task<RegistrationResponse> RegisterAsync(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<PasswordManagerAccountDTO?> UpdateAsync(PasswordManagerAccountDTO model)
    {
        var response = await httpClient.PutAsJsonAsync<PasswordManagerAccountDTO>(Path.Combine(Constants.SERVER_BASE_URL, "api/Passwords/update"), model);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PasswordManagerAccountDTO?>();
        }

        return null;
    }

    public async Task<EditAccountDTO?> UpdateUserAsync(EditAccountDTO editAccountDTO)
    {
        throw new NotImplementedException();
    }

    // public async Task<ServiceResponse> UploadCsvAsync(IFormFile file, string userid)
    // {
    //     var response = await httpClient.GetAsync
    // }
}