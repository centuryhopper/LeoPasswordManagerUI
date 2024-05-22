using LeoPasswordManagerUI.DTOs;
using LeoPasswordManagerUI.Models;
using Microsoft.AspNetCore.Http;

public interface IPasswordManagerAPIConsumerService
{
    Task<EditAccountDTO?> GetEditAccountUserAsync(string userId);
    Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
    Task<ServiceResponse> LogoutAsync();
    Task<ServiceResponse> ConfirmEmailAsync(string token, string userId);
    Task<RegistrationResponse> RegisterAsync(RegisterDTO registerDTO);
    Task<ServiceResponse> UpdateUserAsync(EditAccountDTO editAccountDTO);
    Task<ServiceResponse> DeleteUserAsync(string userId);
    Task<IEnumerable<RoleDTO>> GetRolesAsync();
    Task<ServiceResponse> ChangePassword(ChangePasswordDTO changePasswordDTO);
    Task<(string Msg, UserDTO? UserDTO)> GetUserProfileAsync();


    Task<IEnumerable<PasswordManagerAccountDTO>> GetAllAccountsAsync();
    Task<PasswordManagerAccountDTO?> CreateAsync(PasswordManagerAccountDTO model);
    Task<PasswordManagerAccountDTO?> DeletePasswordManagerAccountAsync(PasswordManagerAccountDTO model);
    Task<PasswordManagerAccountDTO?> UpdateAsync(PasswordManagerAccountDTO model);

}

