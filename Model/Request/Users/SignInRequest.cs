using System.ComponentModel.DataAnnotations;
namespace Model.Request.Users
{
    public class SignInRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
