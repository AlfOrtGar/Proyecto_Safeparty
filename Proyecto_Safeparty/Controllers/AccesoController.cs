using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Proyecto_Safeparty.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Proyecto_Safeparty.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccesoController(IConfiguration configuration, IHttpContextAccessor HttpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = HttpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            String mensajeError = ValidarRegistro(usuario);

            if (mensajeError == "")
            {
                usuario.nombre.Trim();
                usuario.apellidos.Trim();
                usuario.password.Trim();

                var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                try
                {
                    String MySQLQuery = "INSERT INTO usuarios_sf (nombre,apellidos,fecha_nacimiento,genero,password,username,email) VALUES (@nombre,@apellidos,@fecha,@genero,@password,@username,@email);";
                    using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                    {
                        consulta.Parameters.Add(new MySqlParameter("nombre", usuario.nombre));
                        consulta.Parameters.Add(new MySqlParameter("apellidos", usuario.apellidos));
                        consulta.Parameters.Add(new MySqlParameter("fecha", usuario.fecha_nacimiento));
                        consulta.Parameters.Add(new MySqlParameter("genero", usuario.genero));
                        consulta.Parameters.Add(new MySqlParameter("password", usuario.password));
                        consulta.Parameters.Add(new MySqlParameter("username", usuario.username));
                        consulta.Parameters.Add(new MySqlParameter("email", usuario.email));

                        conexion.Open();
                        consulta.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return RedirectToAction("Login","Acceso");
            }
            else
            {
                ViewData["Mensaje"] = mensajeError;
                return View();
            }
        }



        [HttpPost]
        public ActionResult Login(Usuario usuario) 
        {
            if(string.IsNullOrWhiteSpace(usuario.username))
                usuario.username.Trim();

            if(string.IsNullOrWhiteSpace(usuario.password))
                usuario.password.Trim();

            //Si queremos que sea loggeo por usuario o email, se puede cambiar o usar para ambos
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                String MySQLQuery = "SELECT username FROM usuarios_sf WHERE username = @username AND password = @password LIMIT 1";
                using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                {
                    conexion.Open();
                    consulta.Parameters.Add(new MySqlParameter("username", usuario.username));
                    consulta.Parameters.Add(new MySqlParameter("password", usuario.password));

                    if (consulta.ExecuteScalar() != null)
                        _httpContextAccessor.HttpContext.Session.SetString("Username", usuario.username);

                    consulta.Dispose();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (_httpContextAccessor.HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["Mensaje"] = "Nombre de usuario o contraseña incorrectas";
                return View();
            }
        }


        public String ValidarRegistro(Usuario usuario)
        {
            String mensaje = "";
            var conexion = new MySql.Data.MySqlClient.MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            if (string.IsNullOrWhiteSpace(usuario.nombre))
                mensaje += "Por favor introduzca un nombre. ";
            
            if (string.IsNullOrWhiteSpace(usuario.apellidos))
                mensaje += "Por favor introduzca apellidos. ";
            
            if (string.IsNullOrWhiteSpace(usuario.password))
                mensaje += "Por favor introduzca una contraseña. ";
            
            if (string.IsNullOrWhiteSpace(usuario.username))
                mensaje += "Por favor introduzca un nombre de usuario. ";
            else
            {
                try
                {
                    usuario.username.Trim();
                    String MySQLQuery = "SELECT username FROM usuarios_sf WHERE username = @valor";
                    using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                    {
                        conexion.Open();
                        
                        consulta.Parameters.Add(new MySqlParameter("valor", usuario.username));

                        if (consulta.ExecuteScalar() != null)
                            mensaje += "El usuario ya existe en la base de datos. ";

                        consulta.Dispose();
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            if (string.IsNullOrWhiteSpace(usuario.email))
                mensaje += "Por favor introduzca un email. ";
            else
            {
                try
                {
                    usuario.email.Trim();
                    String MySQLQuery = "SELECT email FROM usuarios_sf WHERE email = @valor";
                    using (var consulta = new MySqlCommand(MySQLQuery, conexion))
                    {
                        conexion.Open();

                        consulta.Parameters.Add(new MySqlParameter("valor", usuario.email));

                        if (consulta.ExecuteScalar() != null)
                            mensaje += "El email ya existe en la base de datos. ";

                        consulta.Dispose();
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return mensaje;
        }

    }
}