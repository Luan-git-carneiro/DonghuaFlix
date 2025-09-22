/*
using Donghua = DonghuaFlix.Backend.src.Core.Domain.Entities.Donghua;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using Xunit;
using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.UnitTests.Domain.Entities;
public class DonghuaTests
{


    [Fact]
    public void Donghua_Deve_Ser_Criado_Corretamente()
    {
        //Arrange : Definir os valores esperados
        string tituloEsperado = "Mo Dao Zu Shi";
        string sinopseEsperado = "Cultivadores enfrentam desafios sobrenaturais.";
        string studioEsperado = "Tencent";
        DateTime anoLancamento = new DateTime(2018);
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
        Assert.Equal(anoLancamento, donghua.ReleaseDate);
        Assert.Equal(tipoEsperado, donghua.Type);
        Assert.Equal(statuEsperado, donghua.Status);
        Assert.Equal(imagemEsperada, donghua.Image);
        Assert.Equal(generoEsperado, donghua.Genres);
        Assert.NotEqual(Guid.Empty, donghua.Id); // Certifica que o ID foi gerado

    }

    [Theory]
    [InlineData(null, "title do donghua é obrigatório.")]
    [InlineData("", "title do donghua é obrigatório.")]
    [InlineData("abc", "title do donghua deve conter no mínimo 4 caracteres.")]
    public void Lancar_ExcecaoParaTituloInvalido(string? tituloInvalido, string? mensagemEsperada = null)
    {
        //Act
        var ex = Assert.Throws<DomainValidationException>( () => new Donghua(tituloInvalido,"Sinopse válida" , DonghuaType.Serie, Genre.Comedia));        

        //Assert
        Assert.Equal(mensagemEsperada, ex.Message);
    }
   
    [Theory]
    [InlineData(null, "sinopse do donghua é obrigatório.")]
    [InlineData("", "sinopse do donghua é obrigatório.")]
    [InlineData("abc", "sinopse do donghua deve conter no mínimo 4 caracteres.")]
    public void Lancar_ExcecaoParaSinopseInvalida(string? sinopseInvalida, string? mensagemEsperada = null)
    {
        //Act
        var ex = Assert.Throws<DomainValidationException>( () => new Donghua("Naruto", sinopseInvalida, DonghuaType.Serie, Genre.Comedia));        

        //Assert
        Assert.Equal(mensagemEsperada, ex.Message);
    }

*/
/*
    [Theory]
    [InlineData(2026)]
    [InlineData(2027)]
    public void AnoLancamento(int anoLancamento)
    {
        //Arange & Act 
        var donghua = Assert.Throws<BusinessRulesException>( () => new Donghua("Naruto", "Naruto é um jovem ninja que deseja se tornar o mais forte de todos os ninjas e ser reconhecido por todos.", "terrent", anoLancamento, DonghuaType.Serie, DonghuaStatus.EmAndamento, "/img/arquivo/" , Genre.Comedia) );

        //Assert
        Assert.Equal("Ano de lançamento do donghua não pode ser maior que o ano atual.", donghua.Message);
    }
    */

}
