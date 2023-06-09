﻿using AutoFixture;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Application.Services;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Tests.Utils;
using Moq;
using Shouldly;

namespace LojaDeFerramentasComDapper.Tests.Services
{
    public class EstoqueServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IEstoqueAdapter> _estoqueAdapterMock;
        private readonly Mock<IEstoqueRepository> _estoqueRepositoryMock;
        private readonly Mock<IFerramentaRepository> _ferramentaRepositoryMock;
        private readonly EstoqueService _service;

        public EstoqueServiceTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _estoqueRepositoryMock = new Mock<IEstoqueRepository>();
            _estoqueAdapterMock = new Mock<IEstoqueAdapter>();
            _ferramentaRepositoryMock = new Mock<IFerramentaRepository>();
            _service = new EstoqueService(
                _estoqueAdapterMock.Object,
                _estoqueRepositoryMock.Object,
                _ferramentaRepositoryMock.Object);
        }

        [Fact]
        public async Task AdicionarFerramentaAoEstoqueTestSucesso()
        {
            //Arrenge
            var estoqueModel = new EstoqueModel();
            var requestEstoqueDTO = _fixture.Create<RequestEstoqueDTO>();
            var ferramentaModel = new FerramentaModel();

            estoqueModel.IdFerramenta = requestEstoqueDTO.IdFerramenta;
            estoqueModel.NomeDaFerramenta = requestEstoqueDTO.NomeDaFerramenta;
            ferramentaModel.Id = estoqueModel.IdFerramenta;
            ferramentaModel.Nome = estoqueModel.NomeDaFerramenta;
            _estoqueAdapterMock.Setup(ea => ea.ToEstoqueModel(requestEstoqueDTO)).Returns(estoqueModel);

            //Act
            _estoqueRepositoryMock.Setup(er => er.AdicionarFerramentaAoEstoque(
                It.IsAny<EstoqueModel>())).Returns(Task.FromResult(estoqueModel));

            _ferramentaRepositoryMock
                .Setup(fr => fr.BuscarFerramentaPorId(estoqueModel.IdFerramenta, estoqueModel.NomeDaFerramenta)).Returns(Task.FromResult(ferramentaModel));
                        
            var retorno = await _service.AdicionarFerramentaAoEstoque(requestEstoqueDTO);

            retorno.ShouldBeEquivalentTo(estoqueModel);
            _estoqueRepositoryMock.Verify(er => er.AdicionarFerramentaAoEstoque(It.IsAny<EstoqueModel>()), Times.Once);
        }

        [Fact]
        public async Task AdicionarFerramentaAoEstoqueTestException()
        {
            //Arrenge
            var idFerramenta = _fixture.Create<int>();
            var nomeFerramenta = _fixture.Create<string>();
            var requestEstoqueDTO = _fixture.Create<RequestEstoqueDTO>();
            var estoqueModel = new EstoqueModel();
            var ferramentaModel = new FerramentaModel();

            _estoqueAdapterMock.Setup(ea => ea.ToEstoqueModel(requestEstoqueDTO)).Returns(estoqueModel);

            //Act
            _estoqueRepositoryMock.Setup(er => er.AdicionarFerramentaAoEstoque(
                It.IsAny<EstoqueModel>())).Returns(Task.FromResult(estoqueModel));

            _ferramentaRepositoryMock
                .Setup(fr => fr.BuscarFerramentaPorId(idFerramenta, nomeFerramenta)).Returns(Task.FromResult(ferramentaModel));

            var execption = await Assert.ThrowsAsync<Exception>(
                async () => await _service.AdicionarFerramentaAoEstoque(requestEstoqueDTO));

            //Assert
            Assert.Equal($"A ferramenta: {estoqueModel.IdFerramenta} - {estoqueModel.NomeDaFerramenta} não existe", execption.Message);
        }
        [Fact]
        public async Task AtualizarEstoqueTestSucesso()
        {
            //Arrange
            var ferramentaModel = _fixture.Create<FerramentaModel>();
            var idFerramenta = _fixture.Create<int>();
            var nomeDaFerramenta = _fixture.Create<string>();
            var retornoEsperado = _fixture.Create<bool>();
            retornoEsperado = true;

            _estoqueRepositoryMock.Setup(er => er.AtualizarEstoque(idFerramenta, nomeDaFerramenta)).Returns(Task.FromResult(retornoEsperado));

            _ferramentaRepositoryMock.Setup(fr => fr.BuscarFerramentaPorId(idFerramenta, nomeDaFerramenta))
                .Returns(Task.FromResult(ferramentaModel));
            //Act
            var retorno = await _service.AtualizarEstoque(idFerramenta, nomeDaFerramenta);

            //Assert
            retorno.ShouldBeEquivalentTo(retornoEsperado);
        }
        [Fact]
        public async Task AtualizarEstoqueTestException()
        {
            //Arrange
            int idFerramenta = 0;
            string nomeDaFerramenta = _fixture.Create<string>();
            var ferramentaModel = new FerramentaModel();

            //Act
            var execption = await Assert.ThrowsAsync<Exception>(async () => await _service.AtualizarEstoque(idFerramenta, nomeDaFerramenta));

            //Assert
            Assert.Equal("A ferramenta informada não existe.", execption.Message);
        }
    }
}
