using System.ComponentModel.DataAnnotations;

namespace HC.Presentation.API.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "")] public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "")] public string Password { get; set; } = string.Empty; 
        [Required(ErrorMessage = "")] public string ApiSecret { get; set; } = string.Empty;
    }
}