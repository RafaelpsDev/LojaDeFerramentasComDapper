using LojaDeFerramentasComDapper.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Domain.Models
{
    public class VendaModel
    {
        public int Id { get; set; }     
        public Guid IdPedido { get; set; } = Guid.NewGuid();
        public DateTime DataDaVenda { get; set; } = DateTime.Now;
        public DateTime? DataDeAlteracao { get; set; } = null;
        public StatusEnum StatusDaVenda { get; set; } = StatusEnum.AguardandoPagamento;
        public int IdFerramenta { get; set; }
        public int IdVendedor { get; set; }
        public string NomeDoVendedor { get; set; }
        public string CpfDoVendedor { get; set; }
        public string EmailDoVendedor { get; set; }
        public string TelefoneDoVendedor { get; set; }
        public string NomeDaFerramenta { get; set; }
    }
     
}
