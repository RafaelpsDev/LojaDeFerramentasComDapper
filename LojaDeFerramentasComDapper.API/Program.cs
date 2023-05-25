
using LojaDeFerramentasComDapper.Application.Adapters;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Application.Services;
using LojaDeFerramentasComDapper.Infrastructure.Repository;

namespace LojaDeFerramentasComDapper.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IEstoqueAdapter, EstoqueAdapter>();
            builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            builder.Services.AddScoped<IEstoqueService, EstoqueService>();
            builder.Services.AddScoped<IFerramentaAdapter, FerramentaAdapter>();
            builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();
            builder.Services.AddScoped<IFerramentaService, FerramentaService>();
            builder.Services.AddScoped<IVendaAdapter, VendaAdapter>();
            builder.Services.AddScoped<IVendaRepository, VendaRepository>();
            builder.Services.AddScoped<IVendaService, VendaService>();
            builder.Services.AddScoped<IVendedorAdapter, VendedorAdapter>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}