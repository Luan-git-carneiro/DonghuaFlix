namespace DonghuaFlix.src.Core.Domain.Exceptions;


public class BusinessRulesException : DomainException
{
    public string RulesName { get; }

    public BusinessRulesException( String rulesName , string message ) : base( "BUSINESS_RULES_VIOLATION" ,  message )
    {
        RulesName = rulesName;
    }
    
}