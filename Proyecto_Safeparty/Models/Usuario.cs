namespace Proyecto_Safeparty.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string email { get; set; }

    }
}
