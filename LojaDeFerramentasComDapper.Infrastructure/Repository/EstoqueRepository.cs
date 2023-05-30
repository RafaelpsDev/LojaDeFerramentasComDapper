using Dapper;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Infrastructure.Context.Scripts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Repository
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;   


        public EstoqueRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("ConexaoPadrao");
            
        }
        public async Task<EstoqueModel> AdicionarFerramentaAoEstoque(EstoqueModel estoqueModel)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            int estoqueFerramentaId = await conn.QuerySingleAsync<int>(EstoqueScript.SqlAdicionarFerramentaAoEstoque, estoqueModel);
            estoqueModel.Id = estoqueFerramentaId;

            return estoqueModel;
        }

        public async Task<bool> AtualizarEstoque(int idFerramenta, string nomeDaFerramenta)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            await conn.ExecuteAsync(EstoqueScript.SqlAtualizarEstoque, 
                new { IdFerramenta = idFerramenta, NomeDaFerramenta = nomeDaFerramenta });
            return true;
        }
    }
}
