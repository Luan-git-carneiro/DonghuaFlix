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
}
