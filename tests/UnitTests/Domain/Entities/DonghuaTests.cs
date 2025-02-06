using FluentAssertions;
using DonghuaFlix.src.Core.Domain.Entities;
using Xunit;

namespace DonghuaFlix.UnitTests.Domain.Entities;



    public class DonghuaTests
    {
        [Fact]
        public void CriarDonghua_DeveInstanciarCorretamente()
        {
            // Arrange & Act
            var donghua = new Donghua("Naruto", "Naruto é um jovem ninja que deseja se tornar o mais forte de todos os ninjas e ser reconhecido por todos.", "Studio Pierrot", 2002, DonghuaType.Serie );

            // Assert

            Assert.NotNull(donghua);
            

        }
    }
