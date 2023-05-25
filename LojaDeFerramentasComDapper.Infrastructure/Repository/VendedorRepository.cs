using Dapper;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using LojaDeFerramentasComDapper.Infrastructure.Context.Scripts;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace LojaDeFerramentasComDapper.Infrastructure.Repository
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string stringConnection;

        public VendedorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("ConexaoPadrao");
        }


        public async Task<VendedorModel> AdicionarVendedor(VendedorModel vendedorModel)
        {
            using var conn = new SqlConnection(stringConnection);
            await conn.OpenAsync();
            
            int vendedorId = await conn.QuerySingleAsync<int>(VendedorScript.SqlAdicionarVendedor, vendedorModel);
            vendedorModel.Id = vendedorId;
            
            return vendedorModel;
        }
        public async Task<VendedorModel> BuscarVendedorPorId(int id)
        {
            using var conn = new SqlConnection(stringConnection);
            await conn.OpenAsync();
            var vendedor = await conn.QueryFirstOrDefaultAsync<VendedorModel>(VendedorScript
                .SqlBuscarVendedorPorId, new { Id = id });
            
            return vendedor;
        }
    }
}
