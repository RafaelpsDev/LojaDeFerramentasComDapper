using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Context.Scripts
{
    public class VendedorScript
    {
        public static string SqlBuscarVendedorPorId 
        { get 
            {
                return @"SELECT * FROM TB_VENDEDORES WHERE ID = @id";
            } 
        }
        public static string SqlAdicionarVendedor 
        { get 
            {
                return @"INSERT INTO TB_VENDEDORES (NOME, CPF, EMAIL, TELEFONE) 
                         VALUES (@Nome, @Cpf, @Email, @Telefone)
                         SELECT CAST(SCOPE_IDENTITY() as int)";
            } 
        }
    }
}
