using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Adapters
{
    public class VendedorAdapter : IVendedorAdapter
    {
        public VendedorModel ToVendedorModel(RequestVendedorDTO requestVendedorDTO)
        {
            return new VendedorModel
            {
                Nome = requestVendedorDTO.Nome,
                Cpf = requestVendedorDTO.Cpf,
                Email = requestVendedorDTO.Email,
                Telefone = requestVendedorDTO.Telefone
            };
        }
    }
}
