using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeFerramentasComDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LojaDeFerramentasComDapperController : ControllerBase
    {        
        private readonly IEstoqueService _estoqueService;
        private readonly IFerramentaService _ferramentaService;
        private readonly IVendedorService _vendedorService;
        private readonly IVendaService _vendaService;

        public LojaDeFerramentasComDapperController(
              IEstoqueService estoqueService
            , IFerramentaService ferramentaService
            , IVendedorService vendedorService
            , IVendaService vendaService
            )
        {
            _estoqueService = estoqueService;
            _ferramentaService = ferramentaService;
            _vendedorService = vendedorService;
            _vendaService = vendaService;

        }

        [HttpGet]
        [Route("/BuscarVendaPorId")]
        public async Task<ActionResult> BuscarVendaPorId(int id)
        {
            var venda = await _vendaService.BuscarVendaPorId(id);
            return Ok(venda);
        }

        [HttpPost]
        [Route("/AdicionarVendedor")]
        public async Task<ActionResult> AdicionarVendedor(RequestVendedorDTO requestVendedorDTO)
        {
            var vendedor = await _vendedorService.AdicionarVendedor(requestVendedorDTO);
            return Ok(vendedor);
        }

        [HttpPost]
        [Route("/AdicionarFerramenta")]
        public async Task<ActionResult> AdicionarFerramenta(RequestFerramentaDTO requestFerramentaDTO)
        {
            var ferramenta = await _ferramentaService.AdicionarFerramenta(requestFerramentaDTO); 
            return Ok(ferramenta);
        }

        [HttpPost]
        [Route("/AdicionarFerramentaAoEstoque")]
        public async Task<ActionResult> AdicionarFerramentaAoEstoque(RequestEstoqueDTO requestEstoqueDTO)
        {
            var estoque = await _estoqueService.AdicionarFerramentaAoEstoque(requestEstoqueDTO); 
            return Ok(estoque);
        }

        [HttpPost]
        [Route("/RegistrarVenda")]
        public async Task<ActionResult> RegistrarVenda(RequestVendaDTO requestVendaDTO)
        {
            var venda = await _vendaService.RegistrarVenda(requestVendaDTO);
            return Ok(venda);
        }

        [HttpPut]
        [Route("/AtualizarStatusDaVenda")]
        public async Task<ActionResult> AtualizarStatusDaVenda(int id, [FromBody] StatusEnum StatusVenda)
        {
            var venda = await _vendaService.AtualizaStatusDeVenda(id, StatusVenda);
            return Ok(venda);
        }
    }
}
