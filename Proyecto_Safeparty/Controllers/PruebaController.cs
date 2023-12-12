using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Proyecto_Safeparty.Models;
using Microsoft.Extensions.Configuration;

namespace Proyecto_Safeparty.Controllers
{
    public class PruebaController : Controller
    {
        private readonly IConfiguration _configuration;

        public PruebaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("prueba")] 
        public ActionResult Prueba()
        {
            var connection = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                string MySQLQuery = "INSERT INTO criticas_sf (id_usuario,id_local,valoracion,texto) VALUES (1,1,7,'Esto es una prueba de conexion');";
                using (var command = new MySqlCommand(MySQLQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return RedirectToAction("Prueba2");
        }
    }
}
