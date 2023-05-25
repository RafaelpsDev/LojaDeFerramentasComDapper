using AutoFixture;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Application.Services;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Tests.Utils;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Tests.Services
{
    public class FerramentaServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IFerramentaService> _ferramentaServiceMock;
        private readonly Mock<IFerramentaAdapter> _ferramentaAdapterMock;
        private readonly Mock<IFerramentaRepository> _ferramentaRepositoryMock;
        public FerramentaServiceTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _ferramentaServiceMock = new Mock<IFerramentaService>();
            _ferramentaAdapterMock = new Mock<IFerramentaAdapter>();
            _ferramentaRepositoryMock = new Mock<IFerramentaRepository>();
        }
        [Fact]
        public async Task AdicionarFerramentaTestSucesso()
        {
            //Arrange
            var ferramentaModel = new FerramentaModel();
            var requesteFerramentaDTO = _fixture.Create<RequestFerramentaDTO>();

            _ferramentaAdapterMock.Setup(fa => fa.ToFerramentaModel(requesteFerramentaDTO));

            //Act
            _ferramentaRepositoryMock.Setup(fr => fr.AdicionarFerramenta(
                It.IsAny<FerramentaModel>())).Returns(Task.FromResult(ferramentaModel));

            var ferramentaService = new FerramentaService(_ferramentaRepositoryMock.Object, _ferramentaAdapterMock.Object);

            var retorno = await ferramentaService.AdicionarFerramenta(requesteFerramentaDTO);

            retorno.ShouldBeEquivalentTo(ferramentaModel);
            _ferramentaRepositoryMock.Verify(fr => fr.AdicionarFerramenta(It.IsAny<FerramentaModel>()), Times.Once());
        }
    }
}
