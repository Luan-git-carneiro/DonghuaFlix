using System.Data.SqlTypes;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;

namespace DonghuaFlix.Backend.src.Core.Domain.Exceptions;

    public class DomainValidationException : DomainException
    {
        public string Field { get; }

        public DomainValidationException( String field , string message ) : base( "VALIDATION_ERROR" , message )
        {
            Field = field;
        }
    
    }

