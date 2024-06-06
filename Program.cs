using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransacaoFinanceira.Services;
using TransacaoFinanceira.Data;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Exceptions;
using TransacaoFinanceira.Logging;
using Serilog;

namespace TransacaoFinanceira
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var transacoes = new List<Transacao>
                {
                    new Transacao(1, "09/09/2023 14:15:00", 938485762, 2147483649, 150),
                    new Transacao(2, "09/09/2023 14:15:05", 2147483649, 210385733, 149),
                    new Transacao(3, "09/09/2023 14:15:29", 347586970, 238596054, 1100),
                    new Transacao(4, "09/09/2023 14:17:00", 675869708, 210385733, 5300),
                    new Transacao(5, "09/09/2023 14:18:00", 238596054, 674038564, 1489),
                    new Transacao(6, "09/09/2023 14:18:20", 573659065, 563856300, 49),
                    new Transacao(7, "09/09/2023 14:19:00", 938485762, 2147483649, 44),
                    new Transacao(8, "09/09/2023 14:19:01", 573659065, 675869708, 150),
                };

                IContaRepository contaRepository = new ContaRepository();
                ITransacaoRepository transacaoRepository = new TransacaoRepository();
                ITransacaoService transacaoService = new TransacaoService(contaRepository, transacaoRepository);

                Parallel.ForEach(transacoes, transacao =>
                {
                    try
                    {
                        transacaoService.Transferir(transacao);
                    }
                    catch (SaldoInsuficienteException ex)
                    {
                        Logger.LogError(ex, ex.Message, transacao.CorrelationId, transacao.Valor);
                    }
                    catch (ContaNaoEncontradaException ex)
                    {
                        Logger.LogError(ex, ex.Message, transacao.CorrelationId, transacao.Valor);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "Erro inesperado", transacao.CorrelationId, transacao.Valor);
                    }
                });

                var todasTransacoes = transacaoRepository.ObterTransacoes();
                foreach (var t in todasTransacoes)
                {
                    Console.WriteLine($"Transação {t.CorrelationId}: {t.ContaOrigem} -> {t.ContaDestino}, Valor: {t.Valor}");
                }
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
