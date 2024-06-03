namespace TransacaoFinanceira.Models;

public class Transacao
{
    public int CorrelationId { get; set; }
    public string DateTime { get; set; }
    public int ContaOrigem { get; set; }
    public int ContaDestino { get; set; }
    public decimal Valor { get; set; }
    
    public Transacao(int correlationId, string dateTime, int contaOrigem, int contaDestino, decimal valor)
    {
        CorrelationId = correlationId;
        DateTime = dateTime;
        ContaOrigem = contaOrigem;
        ContaDestino = contaDestino;
        Valor = valor;
    }
}