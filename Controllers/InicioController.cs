using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuCamisa.Controllers
{

    //[Route("Nombre")]
    public class InicioController : Controller
    {

        //[HttpGet]
        //[Route("/Nombre/Ale")] // Se puede agregar mas de una ruta
        //[Route("/Nombre/Vane/{id}")]
       //[HttpGet("[controller]/[action]/{id:int}")]


        public IActionResult Inicio()
        {
            //var url = Url.Action("Metodo", "Inicio", new {age=34, name="Ale" });
            //return View("Inicio", id);

            var url = Url.RouteUrl("Portada", new { name = "Tecno AKE" } );
            return Redirect(url);
        }

        [HttpGet("[controller]/[action]", Name ="Portada")]
        public IActionResult MetodoPortada( String name)
        {
            var data = $"{name}";
            return View("Inicio", data);
        }

        [HttpGet("[controller]/[action]", Name = "Error")]
        public IActionResult MetodoError(int code)
        {
            var data = $"Codigo de estado{code}";
            return View("Inicio", data);
        }



    }
}
