using System.ComponentModel.DataAnnotations;


namespace Model.Request.Users
{
    public class ForgetPasswordRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
