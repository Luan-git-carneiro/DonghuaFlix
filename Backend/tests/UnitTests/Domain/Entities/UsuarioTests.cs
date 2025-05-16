
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using Xunit;
using DonghuaFlix.Backend.src.Core.Domain.Enum;;
using Namespace namespace DonghuaFlix.Backend.src.Core.Domain.Entities;;
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
        Assert.Empty(user.Favorites);

    }

    [Fact]
    public void AdicionarFavorito_ComDonghuaValido_DeveAdicionarNaLista()
    {
        //Arrange
        var usuario = new User("Usuário Teste", new Email("usuario@teste.com"), new Password("SenhaForte123"));
        var donghua = new Donghua("Donghua Teste", "Descrição do Donghua Teste", "Tencent" ,2018 ,  DonghuaType.Serie, DonghuaStatus.EmAndamento,  "https://www.donghua.com.br/cover.jpg", Genre.Wuxia | Genre.Xianxia | Genre.Acao);
        var donghua2 =  new Donghua(title: "A Lenda de Nezha",sinopse: "Nezha, um jovem com poderes divinos, luta contra seu destino para proteger sua família e o mundo.",studio: "Light Chaser Animation",releaseDate: 2019,type: DonghuaType.Movie,status: DonghuaStatus.Concluido,image: "https://exemplo.com/images/nezha.jpg",genres: Genre.Historico | Genre.Romance | Genre.Acao);
        //Act
        usuario.AddFavorite(donghua);
        usuario.AddFavorite(donghua2);

        // Assert
        Assert.True(usuario.Favorites.Any(f => f.IdUser == usuario.Id));
        Assert.True(usuario.Favorites.Any(f => f.IdUser == usuario.Id));
        Assert.Equal(2, usuario.Favorites.Count);

    }

    [Fact]
    public void DonghuaInvalido_DeveLancarExcecao_Duplicado()
    {
        //Arrange
        
        var usuario = new User("Usuário Teste", new Email("usuario@teste.com"), new Password("SenhaForte123"));
        
        Donghua donghua = new Donghua("Donghua Teste", "Descrição do Donghua Teste", "Tencent" ,2018 ,  DonghuaType.Serie, DonghuaStatus.EmAndamento,  "https://www.donghua.com.br/cover.jpg", Genre.Wuxia | Genre.Xianxia | Genre.Acao);
        var donghuaDuplicado = donghua;

        usuario.AddFavorite(donghua);

        //Act
        var ex = Assert.Throws<BusinessRulesException>(() => usuario.AddFavorite(donghuaDuplicado));

        //Assert
        Assert.Equal("Donghua já está nos favoritos.", ex.Message);
        Assert.Equal("DUPLICATE", ex.RulesName);

    }

    [Fact]
    public void DonghuaNulo_DeveLancarExcecao_Nulo()
    {
        //Arrange
       var usuario = new User("Usuário Teste", new Email("usuario@teste.com"), new Password("SenhaForte123"));
    
        //Act
        var ex = Assert.Throws<DomainValidationException>(() => usuario.AddFavorite(null));

        //Assert
        Assert.Equal("Donghua é nulo.", ex.Message);

    
    }

}

