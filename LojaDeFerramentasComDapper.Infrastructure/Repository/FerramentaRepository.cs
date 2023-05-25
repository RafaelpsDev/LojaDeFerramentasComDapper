using Dapper;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Infrastructure.Context.Scripts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Infrastructure.Repository
{
    public class FerramentaRepository : IFerramentaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public FerramentaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("ConexaoPadrao");
        }
        public async Task<FerramentaModel> AdicionarFerramenta(FerramentaModel ferramentaModel)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            int ferramentaId = await conn.QuerySingleAsync<int>(
                FerramentaScript.SqlAdicionarFerramenta, ferramentaModel);
            ferramentaModel.Id = ferramentaId;            
            return ferramentaModel;

        }

        public async Task<FerramentaModel> BuscarFerramentaPorId(int id, string nome)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            var ferramenta = await conn.QueryFirstOrDefaultAsync<FerramentaModel>(
                FerramentaScript.SqlBuscarFerramentaPorId, new {Id = id, Nome = nome });
            return ferramenta;
        }
    }
}
