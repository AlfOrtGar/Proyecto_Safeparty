using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Proyecto_Safeparty.Models;
using Proyecto_Safeparty.Permissions;
using System.Diagnostics;

namespace Proyecto_Safeparty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        // --- Vistas -------------------------------------------------------------
        public IActionResult Index()
        {
            ViewData["Nombre"] = _httpContextAccessor.HttpContext.Session.GetString("Username");
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

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login", "Acceso");
        }

        // --- FIN Vistas ---------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /*[HttpPost]
        public ActionResult MapaLocales(int id)
        {
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                String MySQLQueryInsert = "INSERT INTO locales_sf (id_usuario,nombre,valoracion,direccion,etiquetas) VALUES (0,@nombre,@valoracion,@direccion,0);";
                String MySQLQuerySelect = "SELECT * FROM locales_sf WHERE id = @id;";

                using (var consulta = new MySqlCommand(MySQLQuerySelect, conexion))
                {

                }

                using (var consulta = new MySqlCommand(MySQLQueryInsert,conexion))
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
            
        }*/

        [HttpPost]
        public ActionResult getReviews(int id_local)
        {
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                String MySQLQuery = "SELECT * FROM criticas_sf WHERE id_local = @local;";

                using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                {
                    consulta.Parameters.Add(new MySqlParameter("local", id_local));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return View();
        }

    }
}