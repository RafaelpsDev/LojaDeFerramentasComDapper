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
    public class EstoqueAdapter : IEstoqueAdapter
    {
        public EstoqueModel ToEstoqueModel(RequestEstoqueDTO requestEstoqueDTO)
        {
            return new EstoqueModel
            {
                IdFerramenta = requestEstoqueDTO.IdFerramenta,
                NomeDaFerramenta = requestEstoqueDTO.NomeDaFerramenta,
                Quantidade = requestEstoqueDTO.Quantidade
            };
        }
    }
}
