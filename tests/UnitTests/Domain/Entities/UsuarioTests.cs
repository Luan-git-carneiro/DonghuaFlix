
using DonghuaFlix.src.Core.Domain.Exceptions;
using Xunit;
using DonghuaFlix.src.Core.Domain.Enum;

namespace DonghuaFlix.UnitTests.Domain.Entities;

public class UsuarioTests
{
    [Fact]
    public void Instanciar_Usuario_Deve_Retornar_Usuario_Corretamente()
    {
        // Arrange & Act
        var usuario = new Usuario("João", "Ultimo nome", "email@gmail.com", "senha123" , UserType.Admin);
        var usuario2 = new Usuario("João", "Ultimo nome", "email@gmail.com", "senha123" , UserType.Admin);
 
        // Assert
        Assert.NotNull(usuario);
        Assert.Equal("João", usuario.Nome);
        Assert.Equal("Ultimo nome", usuario.Sobrenome);
        Assert.Equal("email@gmail.com", usuario.Email);
        Assert.Equal("senha123", usuario.Senha);
        Assert.NotEqual(Guid.Empty, usuario.IdUsuario);
        Assert.Equal(UserType.Admin, usuario.Type);
        Assert.NotEqual(usuario.IdUsuario, usuario2.IdUsuario);
        Assert.Equals(usuario2, usuario);

    }
}

