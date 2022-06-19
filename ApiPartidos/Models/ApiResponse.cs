namespace ApiPartidos.Models
{
    //Respuesta de la API para indicar si funciona o no
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
