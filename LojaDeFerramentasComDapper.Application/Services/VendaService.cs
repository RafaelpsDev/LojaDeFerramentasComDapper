using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Enums;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaAdapter _vendaAdapter;
        private readonly IVendaRepository _vendaRepository;
        private readonly IEstoqueService _estoqueService;
        public VendaService(IVendaAdapter vendaAdapter, IVendaRepository vendaRepository, IEstoqueService estoqueService)
        {
            _vendaAdapter = vendaAdapter;
            _vendaRepository = vendaRepository;
            _estoqueService = estoqueService;

        }
        public async Task<ResponseVendaDTO> BuscarVendaPorId(int id)
        {
            var venda = await _vendaRepository.BuscarVendaPorId(id)
                ?? throw new Exception("A venda informada não existe.");
            
            var toresponse = _vendaAdapter.ToResponseVendaDTO(venda);
            return toresponse;
        }

        public Task<VendaModel> RegistrarVenda(RequestVendaDTO requestVendaDTO)
        {
            var toModel = _vendaAdapter.ToVendaModel(requestVendaDTO);
            var venda = _vendaRepository.RegistrarVenda(toModel);
            return venda;
        }
        public async Task<ResponseVendaDTO> AtualizaStatusDeVenda(int id, RequestVendaUpdateDTO requestVendaUpdateDTO)
        {
            var vendaAtualizar = await _vendaRepository.BuscarVendaPorId(id)
                ?? throw new Exception("Não existe venda com o Id informado.");

            if (vendaAtualizar.StatusDaVenda == StatusEnum.AguardandoPagamento)
            {
                if (requestVendaUpdateDTO.StatusDaVenda != StatusEnum.PagamentoAprovado 
                    && requestVendaUpdateDTO.StatusDaVenda != StatusEnum.Cancelado)
                    throw new Exception("Status inválido, Favor informar status \"Aguardando Pagamento\" ou \"Cancelado\".");
            }
            if (vendaAtualizar.StatusDaVenda == StatusEnum.PagamentoAprovado)
            {
                if (requestVendaUpdateDTO.StatusDaVenda != StatusEnum.EnviadoParaTransportadora &&
                    requestVendaUpdateDTO.StatusDaVenda != StatusEnum.Cancelado)
                    throw new Exception("Status inválido, Favor informar status \"Enviado Para a Transportadora\" ou \"Cancelado\".");
            }
            if (vendaAtualizar.StatusDaVenda == StatusEnum.EnviadoParaTransportadora)
            {
                if (requestVendaUpdateDTO.StatusDaVenda != StatusEnum.Entregue)
                    throw new Exception("Status inválido, Favor informar status \"Cancelado\".");
            }

            //ValidacaoStatusDaVendaService.ValidaStatusDaVenda(vendaAtualizar.StatusDaVenda, requestVendaUpdateDTO.StatusDaVenda);

            var toModel = _vendaAdapter.ToVendaModelUpdate(requestVendaUpdateDTO);
            await _vendaRepository.AtualizarStatusDaVenda(id, toModel);
            
            var retorno = await BuscarVendaPorId(id);
            int idFerramenta = retorno.IdFerramenta;
            string nomeDaFerramenta = retorno.NomeDaFerramenta;
            if (retorno.StatusDaVenda == "Entregue")
                await _estoqueService.AtualizarEstoque(idFerramenta, nomeDaFerramenta);
            return retorno;
        }        
    }
}
