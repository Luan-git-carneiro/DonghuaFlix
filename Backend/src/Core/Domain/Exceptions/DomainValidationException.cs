using System.Data.SqlTypes;

namespace DonghuaFlix.src.Core.Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public string Field { get; }

        public DomainValidationException( String field , string message ) : base( "VALIDATION_ERROR" , message )
        {
            Field = field;
        }
    
    }

}