using AutoFixture;
using LojaDeFerramentasComDapper.API.Controllers;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Tests.Controller
{
    public class LojaDeferramentasComDapperControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendaService> _vendaServiceMock;
        private readonly Mock<IVendedorService> _vendedorServiceMock;
        private readonly Mock<IFerramentaService> _ferramentaServiceMock;
        private readonly Mock<IEstoqueService> _estoqueServiceMock;
        private readonly LojaDeFerramentasComDapperController _controller;

        public LojaDeferramentasComDapperControllerTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _vendaServiceMock = new Mock<IVendaService>();
            _vendedorServiceMock = new Mock<IVendedorService>();
            _ferramentaServiceMock = new Mock<IFerramentaService>();
            _estoqueServiceMock = new Mock<IEstoqueService>();
            _controller = new LojaDeFerramentasComDapperController(
                _estoqueServiceMock.Object,
                _ferramentaServiceMock.Object,
                _vendedorServiceMock.Object,
                _vendaServiceMock.Object);
        }
        [Fact]
        public async Task BuscarVendaPorIdTest()
        {
            var id = _fixture.Create<int>();
            var responseVendaDTO = _fixture.Create<ResponseVendaDTO>();
            responseVendaDTO.Id = id;
            
            _vendaServiceMock.Setup(vs => vs.BuscarVendaPorId(id)).Returns(Task.FromResult(responseVendaDTO));
            var retorno = await _controller.BuscarVendaPorId(id);

            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task AdicionarVendedorTest()
        {
            var vendedorModel = new VendedorModel();
            var requestVendedorDTO = _fixture.Create<RequestVendedorDTO>();

            _vendedorServiceMock.Setup(vrs => vrs.AdicionarVendedor(requestVendedorDTO))
                .Returns(Task.FromResult(vendedorModel));

            var retorno = await _controller.AdicionarVendedor(requestVendedorDTO);

            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task AdicionarFerramentaTest()
        {
            var ferramentaModel = new FerramentaModel();
            var requestFerramentaDTO = _fixture.Create<RequestFerramentaDTO>();

            _ferramentaServiceMock.Setup(fs => fs.AdicionarFerramenta(requestFerramentaDTO))
                .Returns(Task.FromResult(ferramentaModel));

            var retorno = await _controller.AdicionarFerramenta(requestFerramentaDTO);

            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task AdicionarFerramentaAoEstoqueTest()
        {
            var estoqueModel = new EstoqueModel();
            var requestEstoqueDTO = _fixture.Create<RequestEstoqueDTO>();

            _estoqueServiceMock.Setup(es => es.AdicionarFerramentaAoEstoque(requestEstoqueDTO))
                .Returns(Task.FromResult(estoqueModel));

            var retorno = await _controller.AdicionarFerramentaAoEstoque(requestEstoqueDTO);
            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task RegistrarVendaTest()
        {
            var vendaModel = new VendaModel();
            var requestVendaDTO = _fixture.Create<RequestVendaDTO>();

            _vendaServiceMock.Setup(vs => vs.RegistrarVenda(requestVendaDTO))
                .Returns(Task.FromResult(vendaModel));

            var retorno = await _controller.RegistrarVenda(requestVendaDTO);
            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task AtualizaStatusDaVendaTest()
        {
            var responseVendaDTO = new ResponseVendaDTO();
            var requestVendaUpdateDTO = _fixture.Create<RequestVendaUpdateDTO>();
            int id = _fixture.Create<int>();

            _vendaServiceMock.Setup(vs => vs.AtualizaStatusDeVenda(id, requestVendaUpdateDTO))
                .Returns(Task.FromResult(responseVendaDTO));

            var retorno = await _controller.AtualizarStatusDaVenda(id, requestVendaUpdateDTO);

            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
    }
}
