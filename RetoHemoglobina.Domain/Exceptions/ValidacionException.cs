namespace RetoHemoglobina.Domain.Exceptions
{
    public class ValidacionException : Exception
    {
        public List<string> Errores { get; }

        public ValidacionException(List<string> errores)
            : base("Se encontraron errores de validación")
        {
            Errores = errores;
        }
    }
}