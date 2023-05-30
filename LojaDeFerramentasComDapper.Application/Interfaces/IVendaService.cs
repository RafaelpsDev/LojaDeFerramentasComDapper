using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Domain.Enums;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Interfaces
{
    public interface IVendaService
    {
        Task<VendaModel> RegistrarVenda(RequestVendaDTO requestVendaDTO);
        Task<ResponseVendaDTO> BuscarVendaPorId(int id);
        Task<ResponseVendaDTO> AtualizaStatusDeVenda(int id, RequestVendaUpdateDTO requestVendaUpdateDTO);
    }
}
