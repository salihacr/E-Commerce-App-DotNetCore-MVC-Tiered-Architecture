using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "İsim bilgisi zorunludur.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Soyisim bilgisi zorunludur.")]
        public string LastName { get; set; }


        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre bilgisi zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Şifre bilgisi zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}