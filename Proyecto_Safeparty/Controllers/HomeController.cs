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
        List<Critica> criticas = new List<Critica>();

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


        [HttpPost]
        public ActionResult Local(int id_local)
        {
            cargaCriticas(id_local);
            var establecimiento = new Local();
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                String MySQLQuery = "SELECT * FROM locales_sf WHERE id_local = @id;";

                using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                {
                    conexion.Open();
                    consulta.Parameters.Add(new MySqlParameter("id", id_local));
                    var lector = consulta.ExecuteReader();

                    while(lector.Read())
                    {
                        establecimiento.nombre = lector["nombre"].ToString();
                        establecimiento.valoracion = (int) lector["valoracion"];
                        establecimiento.direccion = lector["direccion"].ToString();
                    }

                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var resultado = new DatoCompuesto()
            {
                establecimiento = establecimiento,
                comentario = criticas
            };

            return View(resultado);
        }

        public void cargaCriticas(int id_local)
        {
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            if (criticas.Count > 0)
            {
                criticas.Clear();
            }

            try
            {
                String MySQLQuery = "SELECT * FROM criticas_sf WHERE id_local = @local;";

                using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                {
                    conexion.Open();
                    consulta.Parameters.Add(new MySqlParameter("local", id_local));
                    var lector = consulta.ExecuteReader();

                    while(lector.Read())
                    {
                        criticas.Add(new Critica()
                        {
                            username = lector["username"].ToString(),
                            valoracion = (int)lector["valoracion"],
                            texto = lector["texto"].ToString()
                        }) ;
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}