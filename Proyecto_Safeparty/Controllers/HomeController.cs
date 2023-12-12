using Microsoft.AspNetCore.Mvc;
using Proyecto_Safeparty.Models;
using System.Diagnostics;

namespace Proyecto_Safeparty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // --- Vistas -------------------------------------------------------------
        public IActionResult Index()
        {
            //LLamada a modelo
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Configuracion()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Mapa()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Prueba1()
        {
            return View();
        }

        public IActionResult Prueba2()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        // --- FIN Vistas ---------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}