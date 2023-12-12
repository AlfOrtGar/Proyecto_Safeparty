namespace Proyecto_Safeparty.Models
{
    public class Critica
    {
        public int id_comentario {  get; set; }
        public int id_usuario { get; set; }
        public int id_local { get; set; }
        public int valoracion {  get; set; }
        public string texto { get; set; }
    }
}
