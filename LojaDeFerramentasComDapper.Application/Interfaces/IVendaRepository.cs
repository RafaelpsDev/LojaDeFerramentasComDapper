using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IVendaRepository
    {
        Task<VendaModel> RegistrarVenda(VendaModel vendaModel);
        Task<VendaModel> BuscarVendaPorId(int id);
        Task<VendaModel> AtualizarStatusDaVenda(int id, VendaModel vendaModel);
    }
}
