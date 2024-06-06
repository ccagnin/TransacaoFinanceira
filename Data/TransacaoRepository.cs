using System.Collections.Generic;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Data
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> _transacoes;
        private readonly object _lock = new();

        public TransacaoRepository()
        {
            _transacoes = new List<Transacao>();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            lock (_lock)
            {
                _transacoes.Add(transacao);
            }
        }

        public IEnumerable<Transacao> ObterTransacoes()
        {
            lock (_lock)
            {
                return new List<Transacao>(_transacoes);
            }
        }
    }
}