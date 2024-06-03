using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransacaoFinanceira.Data;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Services;

namespace TransacaoFinanceira
{
    class Program
    {
        static Task Main(string[] args)
        {
            var transacoes = new List<Transacao>
            {
                new(1, "09/09/2023 14:15:00", 938485762, 2147483649, 150),
                new(2, "09/09/2023 14:15:05", 2147483649, 210385733, 149),
                new(3, "09/09/2023 14:15:29", 347586970, 238596054, 1100),
                new(4, "09/09/2023 14:17:00", 675869708, 210385733, 5300),
                new(5, "09/09/2023 14:18:00", 238596054, 674038564, 478),
                new(6, "09/09/2023 14:18:30", 210385733, 563856300, 10),
                new(7, "09/09/2023 14:18:59", 674038564, 573659065, 400),
                new(8, "09/09/2023 14:19:01", 573659065, 675869708, 150),
            };

            IContaRepository contaRepository = new ContaRepository();
            ITransacaoService transacaoService = new TransacaoService(contaRepository);

            Parallel.ForEach(transacoes, transacao =>
            {
                try
                {
                    transacaoService.Transferir(transacao);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
            return Task.CompletedTask;
        }
    };
}
