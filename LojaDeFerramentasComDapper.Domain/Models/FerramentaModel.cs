﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Domain.Models
{
    public class FerramentaModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set;}
        //public VendaModel Venda { get; set; }        
    }
}
