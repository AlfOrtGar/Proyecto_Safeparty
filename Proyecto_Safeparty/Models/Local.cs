namespace Proyecto_Safeparty.Models
{
    public class Local
    {
        public int id_local {  get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public int valoracion { get; set; }
        public string direccion { get; set; }
        public string etiquetas { get; set; }
        public string descripcion { get; set; }
        public float longitud {  get; set; }
        public float latitud { get; set; }
    }
}
