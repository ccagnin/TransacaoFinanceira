using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Services;

public interface ITransacaoService
{
    void Transferir(Transacao transacao);
}