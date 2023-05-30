using LojaDeFerramentasComDapper.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.DTOs
{
    public class RequestVendaUpdateDTO
    {
        public StatusEnum StatusDaVenda { get; set; }
    }
}
