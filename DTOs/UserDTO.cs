using System.ComponentModel.DataAnnotations;

namespace LeoPasswordManagerUI.DTOs;

public class UserDTO
{
    public string Id {get; set;}
    public string Salt { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}
