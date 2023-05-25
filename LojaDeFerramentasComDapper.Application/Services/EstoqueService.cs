using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueAdapter _estoqueAdapter;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IFerramentaRepository _ferramentaRepository;

        public EstoqueService(IEstoqueAdapter estoqueAdapter, IEstoqueRepository estoqueRepository, IFerramentaRepository ferramentaRepository)
        {
            _estoqueAdapter = estoqueAdapter;
            _estoqueRepository = estoqueRepository;
            _ferramentaRepository = ferramentaRepository;

        }
        public async Task<EstoqueModel> AdicionarFerramentaAoEstoque(RequestEstoqueDTO requestEstoqueDTO)
        {
            
            var toEstoqueModel = _estoqueAdapter.ToEstoqueModel(requestEstoqueDTO);
            var _ = await _ferramentaRepository.BuscarFerramentaPorId(toEstoqueModel.IdFerramenta, toEstoqueModel.NomeDaFerramenta)
                ?? throw new Exception($"A ferramenta: {toEstoqueModel.IdFerramenta} - {toEstoqueModel.NomeDaFerramenta} não existe");
            var estoque = await _estoqueRepository.AdicionarFerramentaAoEstoque(toEstoqueModel);
            return estoque;
        }
    }
}
