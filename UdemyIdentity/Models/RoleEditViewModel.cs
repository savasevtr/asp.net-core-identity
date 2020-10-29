using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentity.Models
{
    public class RoleEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rol adı gereklidir!")]
        [Display(Name = "Rol Adı:")]
        public string Name { get; set; }
    }
}
