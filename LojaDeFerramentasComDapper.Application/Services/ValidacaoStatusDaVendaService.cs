using LojaDeFerramentasComDapper.Application.DTOs;
using LojaDeFerramentasComDapper.Application.Interfaces;
using LojaDeFerramentasComDapper.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeFerramentasComDapper.Application.Services
{
    public class ValidacaoStatusDaVendaService
    {
        public static bool ValidaStatusDaVenda(StatusEnum de, StatusEnum para)
        {
            if (de == StatusEnum.AguardandoPagamento)
            {
                if (para != StatusEnum.PagamentoAprovado && para != StatusEnum.Cancelado)
                    throw new Exception("Status inválido, Favor informar status \"Aguardando Pagamento\" ou \"Cancelado\".");
            }
            if (de == StatusEnum.PagamentoAprovado)
            {
                if (para != StatusEnum.EnviadoParaTransportadora && para != StatusEnum.Cancelado)
                    throw new Exception("Status inválido, Favor informar status \"Enviado Para a Transportadora\" ou \"Cancelado\".");
            }
            if (de == StatusEnum.EnviadoParaTransportadora)
            {
                if (para != StatusEnum.Entregue)
                    throw new Exception("Status inválido, Favor informar status \"Cancelado\".");
            }
            return true;
        }
    }
}
