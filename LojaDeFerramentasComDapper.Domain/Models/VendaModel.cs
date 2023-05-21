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
        public DateTime DataDaVenda { get; set; } = DateTime.Now;
        public DateTime? DataDeAlteracao { get; set; } = null;
        public StatusEnum StatusDaVenda { get; set; } = StatusEnum.AguardandoPagamento;
        public int IdFerramenta { get; set; }
        public int IdVendedor { get; set; }
        //public VendedorModel Vendedor { get; set; }
        //public ICollection<FerramentaModel> Ferramentas { get; set;}
    }
}
