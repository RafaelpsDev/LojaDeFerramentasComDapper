using AutoFixture;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Application.Services;
using LojaDeFerramentasComDapper.Domain.Enums;
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
    public class VendaServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendaAdapter> _vendaAdapterMock;
        private readonly Mock<IVendaRepository> _vendaRepositoryMock;
        private readonly Mock<IEstoqueService> _estoqueServiceMock;
        private readonly VendaService _vendaService;

        public VendaServiceTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _vendaAdapterMock = new Mock<IVendaAdapter>();
            _vendaRepositoryMock = new Mock<IVendaRepository>();
            _estoqueServiceMock = new Mock<IEstoqueService>();
            _vendaService = new VendaService(
                _vendaAdapterMock.Object,
                _vendaRepositoryMock.Object,
                _estoqueServiceMock.Object);
        }

        [Fact]
        public async Task BuscarVendaPorIdTestSucesso()
        {
            //Arrange
            var vendaModel = _fixture.Create<VendaModel>();
            var responseVendaDTO = new ResponseVendaDTO();
            var id = _fixture.Create<int>();

            _vendaAdapterMock.Setup(va => va.ToResponseVendaDTO(vendaModel)).Returns(responseVendaDTO);

            //Act
            _vendaRepositoryMock.Setup(vr => vr.BuscarVendaPorId(It.IsAny<int>()))
                .Returns(Task.FromResult(vendaModel));

            var retorno = await _vendaService.BuscarVendaPorId(id);

            //Assert
            Assert.NotNull(retorno);
            retorno.ShouldBeEquivalentTo(responseVendaDTO);
        }

        [Fact]
        public async Task BuscarVendaPorIdTestException()
        {
            //Arrange
            var vendaModel = _fixture.Create<VendaModel>();
            var id = _fixture.Create<int>();
            var responseVendaDTO = new ResponseVendaDTO();

            _vendaAdapterMock.Setup(va => va.ToResponseVendaDTO(vendaModel)).Returns(responseVendaDTO);

            //Act
            var execption = await Assert.ThrowsAsync<Exception>(async () => await _vendaService.BuscarVendaPorId(id));

            //Assert
            Assert.Equal("A venda informada não existe.", execption.Message);
        }
        [Theory]
        [InlineData(StatusEnum.AguardandoPagamento, StatusEnum.PagamentoAprovado)]
        [InlineData(StatusEnum.AguardandoPagamento, StatusEnum.Cancelado)]
        [InlineData(StatusEnum.PagamentoAprovado, StatusEnum.EnviadoParaTransportadora)]
        [InlineData(StatusEnum.PagamentoAprovado, StatusEnum.Cancelado)]
        [InlineData(StatusEnum.EnviadoParaTransportadora, StatusEnum.Entregue)]
        public async Task AtualizarVendaServiceTestAtualizacaoSucesso(StatusEnum de, StatusEnum para)
        {
            var venda = _fixture.Build<VendaModel>().With(vm => vm.StatusDaVenda, de).Create();
            var requestVendaUpdateDTO = _fixture.Build<RequestVendaUpdateDTO>().With(vr => vr.StatusDaVenda, para).Create();
            var vendaResponse = _fixture.Create<ResponseVendaDTO>();
            int id = venda.Id;
            vendaResponse.Id = id;
            vendaResponse.StatusDaVenda = venda.StatusDaVenda.ToString();
            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(requestVendaUpdateDTO))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToResponseVendaDTO(venda))
                .Returns(vendaResponse);

            _vendaRepositoryMock.Setup(x => x.AtualizarStatusDaVenda(id, venda)).Returns(Task.FromResult(venda));

            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(id)).Returns(Task.FromResult(venda));

            var retornoAtualizacaoDeVenda = await _vendaService.AtualizaStatusDeVenda(id, requestVendaUpdateDTO);
            int idFerramenta = retornoAtualizacaoDeVenda.IdFerramenta;
            string nomeDaFerramenta = retornoAtualizacaoDeVenda.NomeDaFerramenta;

            Assert.NotNull(retornoAtualizacaoDeVenda);
            Assert.IsType<ResponseVendaDTO>(retornoAtualizacaoDeVenda);
        }

        [Fact]
        public async Task AtualizarVendaServiceTestIdNulo()
        {
            var venda = _fixture.Create<VendaModel>();
            var vendaRequest = _fixture.Create<RequestVendaUpdateDTO>();
            var vendaResponse = _fixture.Create<ResponseVendaDTO>();
            vendaResponse.StatusDaVenda = venda.StatusDaVenda.ToString();
            var id = _fixture.Create<int>();

            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(vendaRequest))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToResponseVendaDTO(venda))
                .Returns(vendaResponse);


            _vendaRepositoryMock.Setup(x => x.AtualizarStatusDaVenda(id, venda)).Returns(Task.FromResult(venda));

            var execption = await Assert.ThrowsAsync<Exception>(async () =>
            await _vendaService.AtualizaStatusDeVenda(id, vendaRequest));
            Assert.Equal("Não existe venda com o Id informado.", execption.Message);

        }

        [Theory]
        [InlineData(StatusEnum.AguardandoPagamento, StatusEnum.EnviadoParaTransportadora)]
        [InlineData(StatusEnum.AguardandoPagamento, StatusEnum.Entregue)]
        [InlineData(StatusEnum.PagamentoAprovado, StatusEnum.AguardandoPagamento)]
        [InlineData(StatusEnum.PagamentoAprovado, StatusEnum.Entregue)]
        [InlineData(StatusEnum.EnviadoParaTransportadora, StatusEnum.Cancelado)]
        public async Task AtualizarVendaServiceTestAtualizacaoStatusErrado(StatusEnum de, StatusEnum para)
        {
            var venda = _fixture.Build<VendaModel>().With(vm => vm.StatusDaVenda, de).Create();
            var requestVendaUpdateDTO = _fixture.Build<RequestVendaUpdateDTO>().With(vr => vr.StatusDaVenda, para).Create();
            var vendaResponse = _fixture.Create<ResponseVendaDTO>();
            int id = venda.Id;
            vendaResponse.Id = id;
            vendaResponse.StatusDaVenda = venda.StatusDaVenda.ToString();
            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(requestVendaUpdateDTO))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToResponseVendaDTO(venda))
                .Returns(vendaResponse);

            _vendaRepositoryMock.Setup(x => x.AtualizarStatusDaVenda(id, venda)).Returns(Task.FromResult(venda));

            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(id)).Returns(Task.FromResult(venda));

            await Assert.ThrowsAsync<Exception>(async () => await _vendaService.AtualizaStatusDeVenda(id, requestVendaUpdateDTO));
        }
    }
}
