using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IFerramentaRepository
    {
        Task<FerramentaModel> AdicionarFerramenta(FerramentaModel ferramentaModel);
        Task<FerramentaModel> BuscarFerramentaPorId(int id, string nome);
    }
}
