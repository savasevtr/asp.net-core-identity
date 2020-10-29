using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentity.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Kullanıcı Adı:")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }

        [Display(Name = "Şifre:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Adınız:")]
        [Required(ErrorMessage = "Ad boş geçilemez")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadınız:")]
        [Required(ErrorMessage = "Soyad boş geçilemez")]
        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress]
        [Required(ErrorMessage = "Email adresi boş geçilemez")]
        public string Email { get; set; }
    }
}