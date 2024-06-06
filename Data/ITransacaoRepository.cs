using System.Collections.Generic;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Data;

public interface ITransacaoRepository
{
    void AdicionarTransacao(Transacao transacao);
    IEnumerable<Transacao> ObterTransacoes();
}