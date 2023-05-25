using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IEstoqueService
    {
        Task<EstoqueModel> AdicionarFerramentaAoEstoque(RequestEstoqueDTO requestEstoqueDTO);
    }
}
