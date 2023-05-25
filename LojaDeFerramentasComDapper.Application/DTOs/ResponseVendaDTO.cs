using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.DTOs
{
    public class ResponseVendaDTO
    {
        public int Id { get; set; }
        public string DataDaVenda { get; set; }
        public string StatusDaVenda { get; set; }
        public int IdVendedor { get; set; }
        public string NomeDoVendedor { get; set; }
        public string CpfDoVendedor { get; set; }
        public string EmailDoVendedor { get; set; }
        public string TelefoneDoVendedor { get; set; }
        public int IdFerramenta { get; set; }
        public string NomeDaFerramenta { get; set; }
    }

}
