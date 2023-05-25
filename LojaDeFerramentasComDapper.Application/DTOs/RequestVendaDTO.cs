using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.DTOs
{
    public class RequestVendaDTO
    {
        public int IdVendedor { get; set; }
        public int IdFerramenta { get; set; }
    }
}
