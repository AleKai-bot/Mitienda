using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TuCamisa.Models;
using TuCamisa.Areas.Producto.Models;
using TuCamisa.Library;

namespace TuCamisa.Controllers
{
    public class HomeController : Controller
    {
        
        private static LoginModel _model = null;
        private LClient client;

        public HomeController(
                 UserManager<IdentityUser> userManager,
                 SignInManager<IdentityUser> signInManager,
                 RoleManager<IdentityRole> roleManager,
                 IServiceProvider serviceProvider)
        {
            client = new LClient(userManager, signInManager, roleManager);
        }


        public async Task<IActionResult> Index()
        {
            //await createRole(_serviceProvider);
            if(_model == null)
            {
                return View();
            }
            else
            {
                return View(_model);
            }
        
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
           // await createRole(_serviceProvider);

            if (ModelState.IsValid)
            {
                var result = await client.UserLoginAsync(model);
                if (result.Succeeded)
                {
                    return Redirect("/Home/Index");  
                }
                else
                {
                    return Redirect("/Home/Log");
               
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Log()
        {
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task createRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] relesName = { "Admin", "Client" };
            foreach(var item in relesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }

    }
}
