using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuCamisa.Areas.Producto.Models;

namespace TuCamisa.Library
{
    public class LClient : ListObject
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public LClient(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        internal async Task<SignInResult> UserLoginAsync(LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Input.Email,
                model.Input.Password, false, lockoutOnFailure: false); //aqui verifica si puede hacer sesion

            return result;
        }
    }
}
