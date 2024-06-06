using System;
using TransacaoFinanceira.Data;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Exceptions;

namespace TransacaoFinanceira.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IContaRepository _contaRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly object _lock = new object();

        public TransacaoService(IContaRepository contaRepository, ITransacaoRepository transacaoRepository)
        {
            _contaRepository = contaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public void Transferir(Transacao transacao)
        {
            lock (_lock)
            {
                var contaOrigem = _contaRepository.GetSaldo(transacao.ContaOrigem);
                var contaDestino = _contaRepository.GetSaldo(transacao.ContaDestino);

                if (contaOrigem == null)
                {
                    throw new ContaNaoEncontradaException(transacao.ContaOrigem);
                }

                if (contaDestino == null)
                {
                    throw new ContaNaoEncontradaException(transacao.ContaDestino);
                }

                if (contaOrigem.Saldo < transacao.Valor)
                {
                    throw new SaldoInsuficienteException(transacao.CorrelationId);
                }

                contaOrigem.Saldo -= transacao.Valor;
                contaDestino.Saldo += transacao.Valor;

                _contaRepository.AtualizarSaldo(contaOrigem);
                _contaRepository.AtualizarSaldo(contaDestino);

                _transacaoRepository.AdicionarTransacao(transacao);

                Console.WriteLine($"Transação número {transacao.CorrelationId} foi efetivada com sucesso! Novos saldos: Conta Origem: {contaOrigem.Saldo} | Conta Destino: {contaDestino.Saldo}");
            }
        }
    }
}