using System;

namespace TransacaoFinanceira.Exceptions
{
    public class ContaNaoEncontradaException : Exception
    {
        public long ContaId { get; }

        public ContaNaoEncontradaException(long contaId)
            : base($"Conta com ID {contaId} não encontrada.")
        {
            ContaId = contaId;
        }
    }
}