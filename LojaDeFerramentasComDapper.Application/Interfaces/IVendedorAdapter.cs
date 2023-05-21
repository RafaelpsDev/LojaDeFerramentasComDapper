using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IVendedorAdapter
    {
        public VendedorModel ToVendedorModel(RequestVendedorDTO requestVendedorDTO);
    }
}
