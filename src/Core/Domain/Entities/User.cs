using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.Entities;

class Users : Entity
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public bool Ativo { get; private set; }
    
    //construtor privado para o EF
    private Usuario() { }

    public Usuario(string nome, Email email, Password senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        
        AddDomainEvent(new UsuarioRegistradoEvent(Id, DateTime.UtcNow));
    }

    public void DesativarUsuario()
    {
        Ativo = false;
        AtualizarDataModificacao();
    }
}