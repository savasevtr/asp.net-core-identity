﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentity.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email alanı gereklidir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Display(Name = "Telefon:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ad:")]
        [Required(ErrorMessage = "Ad alanı gereklidir")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad:")]
        [Required(ErrorMessage = "Soyad alanı gereklidir")]
        public string LastName { get; set; }

        [Display(Name = "Profil Fotoğrafı:")]
        public string PictureUrl { get; set; }

        [Display(Name = "Profil Fotoğrafı:")]
        public IFormFile PictureFile { get; set; }
    }
}