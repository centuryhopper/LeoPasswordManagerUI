using LeoPasswordManagerUI.DTOs;
using LeoPasswordManagerUI.Models;

public interface IPasswordManagerAPIConsumerService
{
    Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
    Task<ServiceResponse> LogoutAsync();
    Task<ServiceResponse> ConfirmEmailAsync();
    Task<RegistrationResponse> RegisterAsync(RegisterDTO registerDTO);
    Task<EditAccountDTO?> UpdateUserAsync(EditAccountDTO editAccountDTO);
    Task<ServiceResponse> DeleteUserAsync(string userId);
    Task<IEnumerable<RoleDTO>> GetRolesAsync();
    Task<ServiceResponse> ChangePassword(ChangePasswordDTO changePasswordDTO);
    Task<(string Msg, UserDTO? UserDTO)> GetUserProfileAsync();
}

