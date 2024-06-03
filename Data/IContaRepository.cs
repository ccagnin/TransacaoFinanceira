using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Data;

public interface IContaRepository
{
    Conta GetSaldo(long id);
    void AtualizarSaldo(Conta conta);
}