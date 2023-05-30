using AutoFixture;
using LojaDeFerramentasComDapper.Application.Adapters;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Tests.Utils;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Tests.Adapters
{
    public class VendedorAdapterTests
    {
        private readonly Fixture _fixture;
        private readonly VendedorAdapter _adapter;

        public VendedorAdapterTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _adapter = new VendedorAdapter();
        }

        [Fact]
        public void ToVendedorModelTestAdaptacaoCorreta()
        {
            //Arrange
            var requestVendedorDTO = _fixture.Create<RequestVendedorDTO>();
            var vendedorModel = new VendedorModel
            {
                Nome = requestVendedorDTO.Nome,
                Cpf = requestVendedorDTO.Cpf,
                Email = requestVendedorDTO.Email,
                Telefone = requestVendedorDTO.Telefone
            };
            //Act
            var retorno = _adapter.ToVendedorModel(requestVendedorDTO);

            //Assert
            retorno.ShouldBeEquivalentTo(vendedorModel);

        }
    }
}
