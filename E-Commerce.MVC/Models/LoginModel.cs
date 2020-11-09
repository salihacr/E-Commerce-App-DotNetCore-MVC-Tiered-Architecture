using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.Models
{
    public class LoginModel
    {
        // [Required]
        // public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}