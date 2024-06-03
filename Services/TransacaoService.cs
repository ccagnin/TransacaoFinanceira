using System;
using TransacaoFinanceira.Data;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Services;

public class TransacaoService : ITransacaoService
{
    private readonly IContaRepository _contaRepository;
    
    public TransacaoService(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }
    public void Transferir(Transacao transacao)
    {
        var contaOrigem = _contaRepository.GetSaldo(transacao.ContaOrigem);
        var contaDestino = _contaRepository.GetSaldo(transacao.ContaDestino);
        
        if (contaOrigem == null || contaDestino == null)
        {
            throw new Exception("Conta não encontrada");
        }
        
        if (contaOrigem.Saldo < transacao.Valor)
        {
            throw new Exception("Saldo insuficiente");
        }
        
        contaOrigem.Saldo -= transacao.Valor;
        contaDestino.Saldo += transacao.Valor;
        
        _contaRepository.AtualizarSaldo(contaOrigem);
        _contaRepository.AtualizarSaldo(contaDestino);
        
        Console.WriteLine($"Transferência de {transacao.Valor} realizada com sucesso");
    }
}