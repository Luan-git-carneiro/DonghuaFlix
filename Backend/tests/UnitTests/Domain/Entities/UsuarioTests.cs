
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using Xunit;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using BCryptNet = BCrypt.Net;

namespace DonghuaFlix.UnitTests.Domain.Entities;

public class UsuarioTests
{
    [Fact]
    public void Instanciar_Usuario_Deve_Retornar_Usuario_Corretamente()
    {
        // Arrange 
        var email = new Email("usuario@teste.com");
        var senha = new Password("SenhaForte123");

        // Act
        var user = new User("Usuário Teste", email, senha);

        // Assert
        Assert.Equal("Usuário Teste", user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(senha, user.Password);
        Assert.Equal(UserRole.Regular, user.Role);
        Assert.Equal(AccountStatus.Active, user.Status);


    }

}

