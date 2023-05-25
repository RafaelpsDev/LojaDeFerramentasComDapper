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
    public class FerramentaService : IFerramentaService
    {
        private readonly IFerramentaRepository _ferramentaRepository;
        private readonly IFerramentaAdapter _ferramentaAdapter;
        public FerramentaService(IFerramentaRepository ferramentaRepository, IFerramentaAdapter ferramentaAdapter)
        {
            _ferramentaRepository = ferramentaRepository;
            _ferramentaAdapter = ferramentaAdapter;
        }
        public async Task<FerramentaModel> AdicionarFerramenta(RequestFerramentaDTO requestFerramentaDTO)
        {
            var toferramentaModel = _ferramentaAdapter.ToFerramentaModel(requestFerramentaDTO);
            var retorno = await _ferramentaRepository.AdicionarFerramenta(toferramentaModel);
            return retorno;
        }
    }
}
