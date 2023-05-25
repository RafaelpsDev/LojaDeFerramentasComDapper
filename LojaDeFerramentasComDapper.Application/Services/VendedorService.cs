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
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IVendedorAdapter _vendedorAdapter;
        public VendedorService(IVendedorRepository vendedorRepository, IVendedorAdapter vendedorAdapter)
        {
            _vendedorRepository = vendedorRepository;
            _vendedorAdapter = vendedorAdapter;
        }
        public async Task<VendedorModel> AdicionarVendedor(RequestVendedorDTO requestVendedorDTO)
        {
            var toModel = _vendedorAdapter.ToVendedorModel(requestVendedorDTO);

            var retorno = await _vendedorRepository.AdicionarVendedor(toModel);

            return retorno;
        }
    }
}
