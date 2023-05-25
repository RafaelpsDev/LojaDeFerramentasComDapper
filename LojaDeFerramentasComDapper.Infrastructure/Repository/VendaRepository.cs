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
    public class VendaRepository : IVendaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string stringConnection;
        public VendaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("ConexaoPadrao");
        }

        public async Task<VendaModel> AtualizarStatusDaVenda(int id, int statusVenda)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var venda = await conn.QuerySingleAsync<VendaModel>(VendaScript.SqlAtualizarStatusDaVenda, new { Id = id, StatusDaVenda = statusVenda }) ;
            return venda;
        }

        public async Task<VendaModel> BuscarVendaPorId(int id)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var venda = await conn.QueryFirstOrDefaultAsync<VendaModel>(VendaScript.SqlBuscarVendaPorid, new {Id = id});
            return venda;
        }
        

        public async Task<VendaModel> RegistrarVenda(VendaModel vendaModel)
        {
            using var conn = new SqlConnection(stringConnection);
            conn.Open();
            var vendaId = await conn.QuerySingleAsync<int>(VendaScript.SqlRegistrarVenda, vendaModel);
            vendaModel.Id = vendaId;
            return vendaModel;
        }
    }
}
