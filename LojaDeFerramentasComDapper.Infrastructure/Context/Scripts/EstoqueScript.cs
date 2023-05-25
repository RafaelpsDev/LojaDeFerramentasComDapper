using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Context.Scripts
{
    public class EstoqueScript
    {
        public static string SqlAdicionarFerramentaAoEstoque 
        { get 
            {
                return @"INSERT INTO TB_ESTOQUE (ID_FERRAMENTA, NOME_FERRAMENTA, QUANTIDADE) " +
                "VALUES (@IdFerramenta, @NomeDaFerramenta, @Quantidade) " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            } 
        }
        public static string SqlAtualizarEstoque 
        { get 
            {
                return @"UPDATE TB_ESTOQUE SET QUANTIDADE = QUANTIDADE - 1 WHERE ID_FERRAMENTA = @IdFerramenta";
            } 
        }
    }
}
