using System;

namespace TransacaoFinanceira.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public int CorrelationId { get; }
        
        public SaldoInsuficienteException(int correlationId)
            : base($"Transação número {correlationId} foi cancelada por falta de saldo.")
        {
            CorrelationId = correlationId;
        }
    }
}