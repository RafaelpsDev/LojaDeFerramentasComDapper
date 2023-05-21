using Dapper;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
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
            // Inserir o Vendedor
            string sql = @"
                    INSERT INTO Vendedor (IdVenda, Nome, Cpf, Email, Telefone)
                    VALUES (@IdVenda, @Nome, @Cpf, @Email, @Telefone)";

            await conn.ExecuteAsync(sql, vendedorModel);
            await conn.CloseAsync();
            return vendedorModel;
        }
    }
}
