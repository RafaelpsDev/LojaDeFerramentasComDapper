using AutoFixture;
using LojaDeFerramentasComDapper.Application.Adapters;
using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Enums;
using LojaDeFerramentasComDapper.Domain.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Tests.Adapters
{
    public class VendaAdaptertests
    {
        private readonly Fixture _fixture;
        private readonly VendaAdapter _adapter;

        public VendaAdaptertests()
        {
            _fixture = new Fixture();
            _adapter = new VendaAdapter();
        }

        [Fact]
        public void ToResponseVendaDTOTestAdaptacaoCorreta()
        {
            //Arrange
            var vendaModel = _fixture.Create<VendaModel>();
            StatusEnum status = vendaModel.StatusDaVenda;
            string statusDaVenda = status switch
            {
                StatusEnum.AguardandoPagamento => "Aguardando Pagamento",
                StatusEnum.PagamentoAprovado => "Pagamento Aprovado",
                StatusEnum.EnviadoParaTransportadora => "Enviado para a Transportadora",
                StatusEnum.Entregue => "Entregue",
                StatusEnum.Cancelado => "Cancelado",
                _ => "",
            };
            var responseVendaDTO = new ResponseVendaDTO
            {
                Id = vendaModel.Id,
                DataDaVenda = vendaModel.DataDaVenda.ToString(),
                StatusDaVenda = statusDaVenda,
                IdVendedor = vendaModel.IdVendedor,
                NomeDoVendedor = vendaModel.NomeDoVendedor,
                CpfDoVendedor = vendaModel.CpfDoVendedor,
                EmailDoVendedor = vendaModel.EmailDoVendedor,
                TelefoneDoVendedor = vendaModel.TelefoneDoVendedor,
                IdFerramenta = vendaModel.IdFerramenta,
                NomeDaFerramenta = vendaModel.NomeDaFerramenta
            };

            //Act
            var retorno = _adapter.ToResponseVendaDTO(vendaModel);

            //Assert
            retorno.ShouldBeEquivalentTo(responseVendaDTO);
        }

        [Fact]
        public void ToVendaModelTestAdaptacaoCorreta()
        {
            //Arrange
            var requestVendaDTO = _fixture.Create<RequestVendaDTO>();
            var vendaModel = new VendaModel
            {
                IdVendedor = requestVendaDTO.IdVendedor,
                IdFerramenta = requestVendaDTO.IdFerramenta
            };

            //Act
            var retorno = _adapter.ToVendaModel(requestVendaDTO);

            //Assert
            Assert.Equal(retorno.IdVendedor, vendaModel.IdVendedor);
            Assert.Equal(retorno.IdFerramenta, vendaModel.IdFerramenta);
        }

        [Fact]
        public void ToVendaModelUpdateTestAdaptacaoCorreta()
        {
            //Arrange
            var requestVendaUpdateDTO = _fixture.Create<RequestVendaUpdateDTO>();
            var vendaModel = new VendaModel
            {
                StatusDaVenda = requestVendaUpdateDTO.StatusDaVenda
            };

            //Act
            var retorno = _adapter.ToVendaModelUpdate(requestVendaUpdateDTO);

            //Assert
            Assert.Equal(retorno.StatusDaVenda, vendaModel.StatusDaVenda);
        }
    }
}
