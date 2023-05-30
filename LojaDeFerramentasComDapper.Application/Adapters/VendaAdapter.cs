using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Enums;
using LojaDeFerramentasComDapper.Domain.Models;


namespace LojaDeFerramentasComDapper.Application.Adapters
{
    public class VendaAdapter : IVendaAdapter
    {
        public ResponseVendaDTO ToResponseVendaDTO(VendaModel vendaModel)
        {
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
            return new ResponseVendaDTO
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
        }

        public VendaModel ToVendaModel(RequestVendaDTO requestVendaDTO)
        {
            return new VendaModel
            {
                IdVendedor = requestVendaDTO.IdVendedor,
                IdFerramenta = requestVendaDTO.IdFerramenta
            };
        }

        public VendaModel ToVendaModelUpdate(RequestVendaUpdateDTO requestVendaUpdateDTO)
        {
            return new VendaModel
            {
                StatusDaVenda = requestVendaUpdateDTO.StatusDaVenda
            };
        }
    }
}
