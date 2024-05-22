using System.Net.Http.Json;
using LeoPasswordManagerUI.DTOs;
using LeoPasswordManagerUI.Models;
using LeoPasswordManagerUI.Utils;


public class PasswordManagerAPIConsumerService : IPasswordManagerAPIConsumerService
{
    private readonly HttpClient httpClient;

    public PasswordManagerAPIConsumerService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient(Constants.HTTP_CLIENT_FACTORY);
    }

    public async Task<ServiceResponse> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        var response = await httpClient.PostAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, $"api/Account/change-password"), changePasswordDTO);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ServiceResponse>() ?? new ServiceResponse(false, "error changing password");
        }

        return new ServiceResponse(false, "couldn't change password");
    }

    public async Task<EditAccountDTO?> GetEditAccountUserAsync(string userId)
    {
        var response = await httpClient.GetAsync(Path.Combine(Constants.SERVER_BASE_URL, $"api/Account/get-edit-profile/{userId}"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<EditAccountDTO>();
        }

        return null;
    }

    public async Task<ServiceResponse> ConfirmEmailAsync(string token, string userId)
    {
        var response = await httpClient.GetAsync(Path.Combine(Constants.SERVER_BASE_URL, $"api/Account/confirm-email/?token={token}&userId={userId}"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ServiceResponse>() ?? new ServiceResponse(false, "error getting delete user result");
        }

        return new ServiceResponse(false, "error deleting user");
    }

    public async Task<PasswordManagerAccountDTO?> CreateAsync(PasswordManagerAccountDTO model)
    {
        System.Console.WriteLine("creating password account");
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
        var response = await httpClient.DeleteAsync(Path.Combine(Constants.SERVER_BASE_URL, $"api/Account/delete-user/{userId}"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ServiceResponse>() ?? new ServiceResponse(false, "error getting delete user result");
        }

        return new ServiceResponse(false, "error deleting user");
    }

    public async Task<IEnumerable<PasswordManagerAccountDTO>> GetAllAccountsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<PasswordManagerAccountDTO>>(Path.Combine(Constants.SERVER_BASE_URL, $"api/Passwords/getaccounts"));


        return response is null ? Enumerable.Empty<PasswordManagerAccountDTO>() : response;
    }

    public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
    {
        var response = await httpClient.GetAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/get-roles"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<RoleDTO>>() ?? Enumerable.Empty<RoleDTO>();
        }

        return Enumerable.Empty<RoleDTO>(); 
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

        var msg = (await response.Content.ReadFromJsonAsync<LoginResponse>()).msg;

        return new LoginResponse(false, msg: msg);
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
        var response = await httpClient.PostAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/register"), registerDTO);

        if (response.IsSuccessStatusCode)
        {
            return new RegistrationResponse(true, "registration success");
        }

        return new RegistrationResponse(false, "failed to register user");
    }

    public async Task<PasswordManagerAccountDTO?> UpdateAsync(PasswordManagerAccountDTO model)
    {
        var response = await httpClient.PutAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Passwords/update"), model);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PasswordManagerAccountDTO?>();
        }

        return null;
    }

    public async Task<ServiceResponse> UpdateUserAsync(EditAccountDTO editAccountDTO)
    {
        var response = await httpClient.PutAsJsonAsync(Path.Combine(Constants.SERVER_BASE_URL, "api/Account/update-user-details"), editAccountDTO);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ServiceResponse>() ?? new ServiceResponse(false, "response was successful, but still couldn't update user for some reason");
        }

        return new ServiceResponse(false, "error updating user");
    }


}