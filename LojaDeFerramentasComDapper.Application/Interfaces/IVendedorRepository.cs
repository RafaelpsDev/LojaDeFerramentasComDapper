using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IVendedorRepository
    {
        Task<VendedorModel> AdicionarVendedor(VendedorModel vendedorModel);
    }
}
