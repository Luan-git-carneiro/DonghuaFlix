
using Donghua = DonghuaFlix.src.Core.Domain.Entities.Donghua;
using DonghuaFlix.src.Core.Domain.Exceptions;
using Xunit;
using DonghuaFlix.src.Core.Domain.Enum;

namespace DonghuaFlix.UnitTests.Domain.Entities;
public class DonghuaTests
{
    [Fact]
    public void CriarDonghua_DeveInstanciarCorretamente()
    {
        // Arrange & Act
        var donghua = new Donghua(
        "Naruto",
        "Naruto é um jovem ninja que deseja se tornar o mais forte de todos os ninjas e ser reconhecido por todos.",
        "Studio Pierrot",
        2002,
        DonghuaType.Serie,
        DonghuaStatus.EmAndamento,
        "/images/donghuas/naruto.jpg",
        Genre.Comedia | Genre.Wuxia | Genre.SciFi | Genre.Historico
        );

        // Assert

        Assert.NotNull(donghua);

    }

    [Fact]
    public void Donghua_Deve_Ser_Criado_Corretamente()
    {
        //Arrange : Definir os valores esperados
        string tituloEsperado = "Mo Dao Zu Shi";
        string sinopseEsperado = "Cultivadores enfrentam desafios sobrenaturais.";
        string studioEsperado = "Tencent";
        int anoLancamento = 2018;
        DonghuaType tipoEsperado = DonghuaType.Serie;
        DonghuaStatus statuEsperado = DonghuaStatus.EmAndamento;
        string imagemEsperada = "imagem.jpg";
        Genre generoEsperado = Genre.Wuxia | Genre.Xianxia | Genre.Acao;

        //Act : Criar uma instancia do donghua.
        var donghua = new Donghua(tituloEsperado, sinopseEsperado, studioEsperado, anoLancamento, tipoEsperado, statuEsperado, imagemEsperada, generoEsperado);

        //Assert: Verificar se os valores foram atribuidos corretamente
        Assert.Equal(tituloEsperado, donghua.Title);
        Assert.Equal(sinopseEsperado, donghua.Sinopse);
        Assert.Equal(studioEsperado, donghua.Studio);
        Assert.Equal(new DateTime(anoLancamento, 1, 1), donghua.ReleaseDate);
        Assert.Equal(tipoEsperado, donghua.Type);
        Assert.Equal(statuEsperado, donghua.Status);
        Assert.Equal(imagemEsperada, donghua.Image);
        Assert.Equal(generoEsperado, donghua.Genres);
        Assert.NotEqual(Guid.Empty, donghua.IdDonghua); // Certifica que o ID foi gerado

    }

    [Theory]
    [InlineData(null, "Sinopse válida", "O Título do donghua é obrigatório.")]
    [InlineData("", "Sinopse válida", "O Título do donghua é obrigatório.")]
    [InlineData("abc", "Sinopse válida",  "O Título do donghua deve conter no mínimo 3 caracteres.")]
    public void Lancar_ExcecaoParaTituloInvalido(string tituloInvalido, string sinopseValida, string mensagemEsperada = null)
    {
        //Act
        var ex = Assert.Throws<DonghuaValidationException>( () => new Donghua(tituloInvalido, sinopseValida, DonghuaType.Serie, Genre.Comedia));        

        //Assert
        Assert.Equal(mensagemEsperada, ex.Message);
    }
   

}
