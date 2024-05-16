
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
    public Task<ServiceResponse> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> ConfirmEmailAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoleDTO>> GetRolesAsync()
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

    public Task<RegistrationResponse> RegisterAsync(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }

    public Task<EditAccountDTO?> UpdateUserAsync(EditAccountDTO editAccountDTO)
    {
        throw new NotImplementedException();
    }
}