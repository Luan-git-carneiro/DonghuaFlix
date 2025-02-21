namespace DonghuaFlix.src.Core.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; protected set; }

    protected void AtualizarDataModificação() => UpdatedAt = DateTime.Now;

    public override bool Equals(object? obj)
    {
        if (obj is null ||  GetType() != obj.GetType())
        {
            return false;
        }
        return Id == ((Entity)obj).Id;
    }
  
    public override int GetHashCode() => Id.GetHashCode();
}
