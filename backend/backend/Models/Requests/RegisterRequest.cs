using System.ComponentModel.DataAnnotations;

namespace backend.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; }
    public string Role { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}