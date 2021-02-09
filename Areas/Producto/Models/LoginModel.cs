using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuCamisa.Areas.Producto.Models
{
    public class LoginModel
    {

        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string Error { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "El campo Email es obligatorio")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            [StringLength(50, ErrorMessage = "El número de caracteres de {0} ser de al menos {2}", MinimumLength = 6)]
            public string Password { get; set; }


   
        }

    }
}
