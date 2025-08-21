using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Events;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;



namespace DonghuaFlix.Backend.src.Core.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserRole Role { get; private set; }
    public  AccountStatus Status { get; private set; }
    
    private readonly List<History> _histories = new();
    public IReadOnlyList<History> Histories => _histories.AsReadOnly();
    private readonly List<Favorite> _favorites = new List<Favorite>();
    public IReadOnlyList<Favorite> Favorites => _favorites.AsReadOnly();
    
    //construtor privado para o EF
    public User() { }

    public User(string nome, Email email, Password senha)
    {
        Name = nome;
        Email = email;
        Password = senha;
        Role = UserRole.Regular;
        Status = AccountStatus.Active;
        
       // AddDomainEvent(new EventUserRegister(Id, DateTime.UtcNow));
    }

        public User(string nome, Email email, Password senha, UserRole role)
    {
        Name = nome;
        Email = email;
        Password = senha;
        Role = role;
        Status = AccountStatus.Active;
        
       // AddDomainEvent(new EventUserRegister(Id, DateTime.UtcNow));
    }

    public User(Guid id, string Name , Email email, Password password, UserRole role, AccountStatus status)    : base(id)
    {
        this.Name = Name;
        this.Email = email;
        this.Password = password;
        this.Role = role;
        this.Status = status;

        // AddDomainEvent(new EventUserRegister(Id, DateTime.UtcNow));
    }

    // Com métodos:
    public void PromoteToAdmin() => Role = UserRole.Admin;
    public void DemoteToRegular() => Role = UserRole.Regular;

    // Métodos de consulta
    public bool IsAdmin() => Role == UserRole.Admin;
    public bool IsRegular() => Role == UserRole.Regular;

    public static User CreateAdmin(string name, Email email , Password password)
    {
        var user = new User(name, email, password);
        user.Role = UserRole.Admin;
        user.Status = AccountStatus.Active;
        return user;
    }

    public void Deactive()
    {
        Status = AccountStatus.Inactive;
        // AddDomainEvent(new UserDeactivatedEvent(Id));
        AtualizarDataModificação();
    }

    public void Active()
    {
        Status = AccountStatus.Active;
        // AddDomainEvent(new UseractivatedEvent(Id));
        AtualizarDataModificação();
    }

    public void AddFavorite(Donghua donghua)
    {
        if(donghua is null)
        {
            throw new DomainValidationException( field: nameof(donghua) , message: "Donghua é nulo.");
        }
        if(_favorites.Any(f => f.DonghuaId == donghua.Id))
        {
            throw new BusinessRulesException(rulesName: "DUPLICATE" , message: "Donghua já está nos favoritos.");
        }


        _favorites.Add(new Favorite( this.Id , donghua.Id, DateTime.UtcNow));

    }

    public void RemoveFavorite(Donghua donghua)
    {
        var favorite = _favorites.FirstOrDefault(f => f.DonghuaId == donghua.Id);

        if(favorite is null)
        {
            throw new DomainValidationException( field: nameof(donghua) , message: "Donghua não está nos favoritos.");
        }

        _favorites.Remove(favorite);
    }
    
    public void AddHistory(Guid episodioId, DateTime data )
    {
        if (episodioId == Guid.Empty)
            throw new DomainValidationException(field: nameof(episodioId), message: "Episódio inválido.");
        

        if (data == DateTime.MinValue)
            throw new DomainValidationException(field: nameof(data), message: "Data inválida.");
    
        var existente = _histories.FirstOrDefault(h => h.EpisodeId == episodioId);
        
        if (existente != null)
            _histories.Remove(existente);

        _histories.Add(new History( this.Id , episodioId , data));
    }

    public void UpdatePassword(Password password)
    {
        Password = password;
        AtualizarDataModificação();
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
        AtualizarDataModificação();
    }
    public void UpdateName(string name)
    {
        Name = name;
        AtualizarDataModificação();
    }

    public void UpdateType(UserRole role)
    {
        Role = role;
        AtualizarDataModificação();
    }

    public void AddHistory(Guid episodioId)
    {
        var existente = _histories.FirstOrDefault(h => h.EpisodeId == episodioId);
        
        if (existente != null)
            _histories.Remove(existente);

        _histories.Add(new History( this.Id , episodioId , DateTime.UtcNow));
    }
}
