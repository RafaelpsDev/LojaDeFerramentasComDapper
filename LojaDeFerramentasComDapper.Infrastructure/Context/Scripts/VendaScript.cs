using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Context.Scripts
{
    public class VendaScript
    {
        public static string SqlBuscarVendaPorid 
        { get 
            {
                return @"SELECT DISTINCT
                           VAS.ID AS Id
                          ,VAS.DATA_DA_VENDA AS DataDaVenda
                          ,VAS.STATUS_DA_VENDA AS StatusDaVenda
                          ,VES.ID AS IdVendedor
                          ,VES.NOME AS NomeDoVendedor
                          ,VES.CPF AS CpfDoVendedor
                          ,VES.EMAIL AS EmailDoVendedor
                          ,VES.TELEFONE AS TelefoneDoVendedor
                          ,F.ID AS IdFerramenta
                          ,F.NOME AS NomeDaFerramenta
                          FROM TB_VENDAS VAS
                          INNER JOIN TB_VENDEDORES VES
                          ON VAS.ID_VENDEDOR = VES.ID
                          INNER JOIN TB_FERRAMENTAS F
                          ON VAS.ID_FERRAMENTA = F.ID
                          WHERE VAS.ID = @id";
            } 
        }
        public static string SqlRegistrarVenda 
        { get 
            { 
                return @"INSERT INTO TB_VENDAS (ID_PEDIDO, ID_FERRAMENTA, ID_VENDEDOR, DATA_DE_VENDA, STATUS_DA_VENDA) " +
                        " VALUES (@IdPedido, @IdFerramenta, @IdVendedor, @DataDaVenda, @StatusDaVenda) " +
                                     "SELECT CAST(SCOPE_IDENTITY() as int)";
            } 
        }
        public static string SqlAtualizarStatusDaVenda 
        { get 
            {
                return @"UPDATE TB_VENDAS 
                               SET STATUS_DA_VENDA = @statusDaVenda
                             , DATA_DE_ALTERACAO = GETDATE() 
                         WHERE ID = @id "+
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
            } 
        }
    }
}
