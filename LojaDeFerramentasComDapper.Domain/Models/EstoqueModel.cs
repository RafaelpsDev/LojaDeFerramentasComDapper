using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Domain.Models
{
    public class EstoqueModel
    {
        public int Id { get; set; }
        public int IdFerramenta { get; set; }
        public string NomeDaFerramenta { get; set; }
        public int Quantidade { get; set; }

    }
}
