using Moq;
using Xunit;
using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.src.Core.Aplication.Commands.Favorites;

namespace DonghuaFlix.UnitTests.Application.Commands.Favoritos;

public class AdicionarFavoritoCommandHandlerTests
{
    [Fact]
    public async Task Handle_ComDadosValidos_DeveAdicionarFavorito()
    {
        // Arrange
        var usuario = new User("test@user.com", "Test User");
        var donghua = new Donghua("Test Donghua");
        
        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.GetByIdAsync(usuario.Id))
            .ReturnsAsync(usuario);
        
        var donghuaRepoMock = new Mock<IDonghuaRepository>();
        donghuaRepoMock.Setup(r => r.GetByIdAsync(donghua.Id))
            .ReturnsAsync(donghua);

        var handler = new AddFavoriteCommandHandler(
            usuarioRepoMock.Object, 
            donghuaRepoMock.Object
        );

        // Act
        await handler.Handle(new AddFavoriteCommand(usuario.Id, donghua.Id), default);

        // Assert
        Assert.Single(User.AddFavorite(donghua));
        usuarioRepoMock.Verify(r => r.UpdateAsync(usuario), Times.Once);
    }
}