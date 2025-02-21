using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Events;
using DonghuaFlix.src.Core.Domain.ValueObjects;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Domain.Exceptions;


namespace DonghuaFlix.src.Core.Domain.Entities;

class User : Entity
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserType UserType { get; private set; }
    public bool Ativo { get; private set; }
    

    private List<Favorite> Favorites { get; set; }
    
    //construtor privado para o EF
    private User() { }

    public User(string nome, Email email, Password senha)
    {
        Name = nome;
        Email = email;
        Password = senha;
        
       // AddDomainEvent(new EventUserRegister(Id, DateTime.UtcNow));
    }

    public void DesativarUsuario()
    {
        Ativo = false;
        AtualizarDataModificação();
    }

    public void AtivarUsuario()
    {
        Ativo = true;
        AtualizarDataModificação();
    }

    public void AddFavorite(Guid idDonghua)
    {
        if(Favorites.Any(f => f.IdDonghua == idDonghua))
        {
            throw new UserValidationException("Donghua já está nos favoritos.");
        }

        if(idDonghua == Guid.Empty)
        {
            throw new UserValidationException("Id do donghua é inválido.");
        }

        Favorites.Add(new Favorite(idDonghua, Id));

    }

    public void RemoveFavorite(Guid idDonghua)
    {
        var favorite = Favorites.FirstOrDefault(f => f.IdDonghua == idDonghua);

        if(favorite is null)
        {
            throw new UserValidationException("Donghua não está nos favoritos.");
        }

        Favorites.Remove(favorite);
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

    public void UpdateType(UserType userType)
    {
        UserType = userType;
        AtualizarDataModificação();
    }

    
}
