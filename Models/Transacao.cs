namespace TransacaoFinanceira.Models;

public class Transacao
{
    public int CorrelationId { get; set; }
    public string DateTime { get; set; }
    public long ContaOrigem { get; set; }
    public long ContaDestino { get; set; }
    public decimal Valor { get; set; }
    
    public Transacao(int correlationId, string dateTime, long contaOrigem, long contaDestino, decimal valor)
    {
        CorrelationId = correlationId;
        DateTime = dateTime;
        ContaOrigem = contaOrigem;
        ContaDestino = contaDestino;
        Valor = valor;
    }
}