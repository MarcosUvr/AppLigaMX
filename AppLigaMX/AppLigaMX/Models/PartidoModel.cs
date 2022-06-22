namespace AppLigaMX.Models
{
    public class PartidoModel
    {
        public int ID { get; set; }
        public string Teams { get; set; }
        public string Picture { get; set; }
        public string Hour { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
