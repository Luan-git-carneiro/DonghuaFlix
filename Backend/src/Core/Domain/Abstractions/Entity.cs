namespace DonghuaFlix.src.Core.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
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


// chatgpt que fez  a parte de baixo
     protected Entity() // Para novas entidades
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id) // Para recuperação de dados
    {
        Id = id;
    }
}
