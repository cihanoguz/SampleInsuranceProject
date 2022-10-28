using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Model.Request.Users
{
    public class SignUpRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password And PasswordRetry Must Be Same")]
        public string PasswordRetry { get; set; }

        public Role Role { get; set; }

    }
}
