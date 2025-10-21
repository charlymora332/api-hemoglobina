namespace RetoHemoglobina.Domain.Models;


    //datos del que se reciben para procesar el paciente
    public class Paciente
    {
        public required string Nombre { get; set; }
        public byte Genero { get; set; }      // 1 = Femenino, 2 = Masculino
        public float Nivel { get; set; }    
    }

