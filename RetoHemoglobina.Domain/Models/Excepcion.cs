namespace RetoHemoglobina.Domain.Models;

//Modelo para manejar las excepciones de la aplicación y retornarlas al cliente
public class Excepcion
{
    public int Id { get; set; }
    public string Mensaje { get; set; }
}