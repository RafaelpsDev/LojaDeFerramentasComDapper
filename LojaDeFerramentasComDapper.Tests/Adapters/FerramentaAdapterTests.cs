using AutoFixture;
using LojaDeFerramentasComDapper.Application.Adapters;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Tests.Utils;
using Shouldly;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Tests.Adapters
{
    public class FerramentaAdapterTests
    {
        private readonly Fixture _fixture;
        private readonly FerramentaAdapter _adapter;

        public FerramentaAdapterTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _adapter = new FerramentaAdapter();
        }
        [Fact]
        public void ToFerramentaModelTestAdaptacaoCorreta() 
        {
            //Arrange
            var requestFerramentaDTO = _fixture.Create<RequestFerramentaDTO>();
            var ferramentaModel = new FerramentaModel
            {
                Nome = requestFerramentaDTO.Nome,
                Valor = requestFerramentaDTO.Valor
            };

            //Act
            var retorno = _adapter.ToFerramentaModel(requestFerramentaDTO);

            //Assert
            retorno.ShouldBeEquivalentTo(ferramentaModel);
        }
    }
}
