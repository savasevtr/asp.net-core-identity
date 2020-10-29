using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentity.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Rol adı gereklidir!")]
        [Display(Name = "Rol Adı:")]
        public string Name { get; set; }
    }
}