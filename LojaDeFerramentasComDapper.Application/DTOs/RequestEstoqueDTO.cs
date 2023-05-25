using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.DTOs
{
    public class RequestEstoqueDTO
    {
        public int IdFerramenta { get; set; }
        public string NomeDaFerramenta { get; set; }
        public int Quantidade { get; set; }
    }
}
