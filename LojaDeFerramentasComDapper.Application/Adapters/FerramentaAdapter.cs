using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Adapters
{
    public class FerramentaAdapter : IFerramentaAdapter
    {
        public FerramentaModel ToFerramentaModel(RequestFerramentaDTO requestFerramentaDTO)
        {
            return new FerramentaModel
            {
                Nome = requestFerramentaDTO.Nome,
                Valor = requestFerramentaDTO.Valor
            };
        }        
    }
}
