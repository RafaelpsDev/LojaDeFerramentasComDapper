using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Context.Scripts
{
    public class FerramentaScript
    {
        public static string SqlAdicionarFerramenta 
        { get 
            {
                return @"INSERT INTO TB_FERRAMENTAS (NOME, VALOR) VALUES (@Nome, @Valor) " +
                       "SELECT CAST(SCOPE_IDENTITY() as int)";
            } 
        }
        public static string SqlBuscarFerramentaPorId 
        { get 
            { 
                return @"SELECT * FROM TB_FERRAMENTAS WHERE ID = @id AND NOME = @nome";
            } 
        }
    }
}
