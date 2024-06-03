using System.Collections.Generic;
using System.Linq;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Data;

public class ContaRepository : IContaRepository
{
    private readonly List<Conta> _contas;
    
    public ContaRepository()
    {
        _contas =
        [
            new Conta(938485762, 180),
            new Conta(347586970, 1200),
            new Conta(2147483649, 0),
            new Conta(675869708, 4900),
            new Conta(238596054, 478),
            new Conta(573659065, 787),
            new Conta(210385733, 10),
            new Conta(674038564, 400),
            new Conta(563856300, 1200)
        ];
    }
    
    public Conta GetSaldo(long id)
    {
        return _contas.FirstOrDefault(c => c.Id == id);
    }
    
    public void AtualizarSaldo(Conta conta)
    {
        var contaAtual = _contas.FindIndex(c => c.Id == conta.Id);
        if (contaAtual != -1)
        {
            _contas[contaAtual] = conta;
        }
    }
}