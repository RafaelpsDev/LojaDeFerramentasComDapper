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
    public class VendedorServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendedorService> _vendedorServiceMock;
        private readonly Mock<IVendedorAdapter> _vendedorAdapterMock;
        private readonly Mock<IVendedorRepository> _vendedorRepositoryMock;

        public VendedorServiceTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _vendedorServiceMock = new Mock<IVendedorService>();
            _vendedorAdapterMock = new Mock<IVendedorAdapter>();
            _vendedorRepositoryMock = new Mock<IVendedorRepository>();
        }
        [Fact]
        public async Task AdicionarVendedorTestSucesso()
        {
            //arrange
            var vendedorModel = new VendedorModel();
            var requestVendedorDTO = _fixture.Create<RequestVendedorDTO>();
            
            _vendedorAdapterMock.Setup(va => va.ToVendedorModel(requestVendedorDTO)).Returns(vendedorModel);

            //Act
            _vendedorRepositoryMock.Setup(vr => vr.AdicionarVendedor(
                It.IsAny<VendedorModel>())).Returns(Task.FromResult(vendedorModel));

            var vendedorService = new VendedorService(_vendedorRepositoryMock.Object, _vendedorAdapterMock.Object);

            var retorno = await vendedorService.AdicionarVendedor(requestVendedorDTO);

            //Assert
            retorno.ShouldBeEquivalentTo(vendedorModel);
            _vendedorRepositoryMock.Verify(vr => vr.AdicionarVendedor(It.IsAny<VendedorModel
                >()), Times.Once);
        }
    }
}
