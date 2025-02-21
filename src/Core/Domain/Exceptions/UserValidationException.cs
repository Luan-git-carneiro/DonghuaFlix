namespace DonghuaFlix.src.Core.Domain.Exceptions
{
    public class UserValidationException : Exception
    {
        public UserValidationException(string message) : base(message)
        {
        }
    
        public UserValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserValidationException( string message , string nameof) : base(message)
        {
            message = $"{nameof} do usuário é obrigatório.";
        }
    
    }

}