using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TuCamisa.Areas.Producto.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private static InputModel _input = null;
        private SignInManager<IdentityUser> _signInManager;

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void OnGet()
        {

            if ( _input != null)
            {
                Input = _input;
            }
            else
            {

            }


        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage ="El campo Email es obligatorio")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage ="El campo Contraseña es obligatorio")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            [StringLength(50, ErrorMessage = "El número de caracteres de {0} ser de al menos {2}", MinimumLength =6)]
            public string Password { get; set; }

            [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio")]
            [DataType(DataType.Password)]
            [Display(Name = "Cofirmación de Contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
            public string CPassword { get; set; }

            public string Error { get; set; }
        }

        // Pass Ale-250998
        public async Task<IActionResult> OnPostAsync()
        {

           if(await RegisterUserAsync())
            {
                 return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Usuario/Register");
            }
        }


        private async Task<bool> RegisterUserAsync()
        {
            var run = false;
            if (ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0))
                {
                    var user = new IdentityUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email
                    };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Client");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        run = true;
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            Input = new InputModel
                            {
                                Error = item.Description,
                                Email = Input.Email
                            };
                        }
                        run = false;
                        _input = Input;
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        Error = "El " + Input.Email + " ya esta registrado",
                        Email = Input.Email  
                    };
                    run = false;
                    _input = Input;
                }
            }
            return run;
        }




    }
}
