using System.ComponentModel.DataAnnotations;


namespace Model.Request.Users
{
    public class RePassword
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password And PasswordRetry Must Be Same")]
        public string PasswordRetry { get; set; }
    }
}
