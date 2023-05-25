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
        private readonly IEstoqueRepository _estoqueRepository;
        public VendaService(IVendaAdapter vendaAdapter, IVendaRepository vendaRepository, IEstoqueRepository estoqueRepository)
        {
            _vendaAdapter = vendaAdapter;
            _vendaRepository = vendaRepository;
            _estoqueRepository = estoqueRepository;

        }
        public async Task<ResponseVendaDTO> BuscarVendaPorId(int id)
        {
            var venda = await _vendaRepository.BuscarVendaPorId(id);
            var toresponse = _vendaAdapter.ToResponseVendaDTO(venda);
            return toresponse;
        }

        public Task<VendaModel> RegistrarVenda(RequestVendaDTO requestVendaDTO)
        {
            var toModel = _vendaAdapter.ToVendaModel(requestVendaDTO);
            var venda = _vendaRepository.RegistrarVenda(toModel);
            return venda;
        }
        public async Task<ResponseVendaDTO> AtualizaStatusDeVenda(int id, StatusEnum statusVenda)
        {
            var vendaAtualizar = await _vendaRepository.BuscarVendaPorId(id)
                ?? throw new Exception("Não existe venda com o Id informado");
          
            ValidacaoStatusDaVendaService.ValidaStatusDaVenda(vendaAtualizar.StatusDaVenda, statusVenda);
            
            await _vendaRepository.AtualizarStatusDaVenda(id, (int)statusVenda);
            
            var retorno = await BuscarVendaPorId(id);
            int idFerramenta = retorno.IdFerramenta;
            if (idFerramenta == 3)
                await _estoqueRepository.AtualizarEstoque(vendaAtualizar.IdFerramenta);
            return retorno;
        }
    }
}
