namespace DonghuaFlix.src.Core.Domain.Abstractions;

public abstract class ValueObject
{

    //Metodo abstrato para definir os componentes usanddo na igualdade
    protected abstract IEnumerable<object> GetEqualityComponents();

    //Sobrescrevendo Equals para comparar objetos pelo valor

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    }

    //// Sobrescrevendo GetHashCode para gerar um código hash baseado nos valores
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(component => component?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    // Operador de igualdade (==)
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    // Operador de diferença (!=)
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

}