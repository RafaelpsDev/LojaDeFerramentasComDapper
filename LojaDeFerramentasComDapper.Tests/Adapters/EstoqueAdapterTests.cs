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
    public class EstoqueAdapterTests
    {
        private readonly Fixture _fixture;
        private readonly EstoqueAdapter _adapter;
        public EstoqueAdapterTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _adapter = new EstoqueAdapter();
        }
        [Fact]
        public void ToEstoqueModelTestAdaptacaoCorreta()
        {
            //Arrange
            var requestEstoqueDTO = _fixture.Create<RequestEstoqueDTO>();
            var estoqueModel = new EstoqueModel
            {
                IdFerramenta = requestEstoqueDTO.IdFerramenta,
                NomeDaFerramenta = requestEstoqueDTO.NomeDaFerramenta,
                Quantidade = requestEstoqueDTO.Quantidade
            };

            //Act
            var retorno = _adapter.ToEstoqueModel(requestEstoqueDTO);

            //Assert
            retorno.ShouldBeEquivalentTo(estoqueModel);
        }

    }
}
