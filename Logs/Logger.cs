using System;
using Serilog;

namespace TransacaoFinanceira.Logging
{
    public static class Logger
    {
        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogError(string message)
        {
            Log.Error(message);
        }

        public static void LogError(Exception ex, string message, int correlationId, decimal valor)
        {
            Log.Error(ex, "Erro ao transferir a transação {CorrelationId} com valor {Valor}: {Mensagem}", correlationId, valor, message);
        }
    }
}