using System.ComponentModel.DataAnnotations;

namespace DeveloperHub.Data.DTOs
{
    public class Login
    {
        [Display(Name = "Username_or_Email")]
        [Required(ErrorMessage = "Username or email is required")]
        public string Username_or_Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
